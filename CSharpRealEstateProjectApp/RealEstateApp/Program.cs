using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstate.Services.ServiceInterfaces;
using RealEstate.Services;
using RealEstateApp.Data;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using RealEstateApp.Repositories.RepositoryInterfaces;
using RealEstateApp.Services;
using RealEstateApp.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using NLog;
using NLog.Web;
using RealEstateApp.Models.Options;
using RealEstateApp.Models.DTOs.Details;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("No Connection String");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

    string identityString = builder.Configuration.GetConnectionString("Identity") ?? throw new InvalidOperationException("No Identity Connection String");
    builder.Services.AddDbContext<IdentityUserDbContext>(options => options.UseSqlServer(identityString));

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
    })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<IdentityUserDbContext>().AddDefaultTokenProviders();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidAudience = builder.Configuration["JwtTokensOptions:AccessTokenOptions:Audience"],
                            ValidIssuer = builder.Configuration["JwtTokensOptions:AccessTokenOptions:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                 builder.Configuration["JwtTokensOptions:AccessTokenOptions:Key"]
                    ?? throw new InvalidOperationException("Access token key is missing.")))
                        };
                    });

    builder.Services.AddControllers(o => o.SuppressAsyncSuffixInActionNames = false);

    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
    builder.Services.AddScoped<IPropertyService, PropertyService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
    builder.Services.AddScoped<IPhotoService, PhotoService>();
    builder.Services.AddScoped<ITokenCreationService, JwtService>();
    builder.Services.AddTransient<IEmailService, EmailService>();
    builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    builder.Services.AddMemoryCache();

    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Property, PropertyDetailsDto>();
    });

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Standar Authorization header using the Bearer scheme (\"bearer {key}\")",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
    });

    builder.Services.AddCors(options => options.AddDefaultPolicy(
        configurePolicy => configurePolicy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            ));

    builder.Services
        .AddOptions<JwtTokensOptions>()
        .BindConfiguration(nameof(JwtTokensOptions))
        .ValidateDataAnnotations();

    var app = builder.Build();

    app.UseCors();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
    using (var identityContext = scope.ServiceProvider.GetRequiredService<IdentityUserDbContext>())
    {
        await context.Database.MigrateAsync();
        await identityContext.Database.MigrateAsync();
    }

    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roleExist = await roleManager.RoleExistsAsync("User");
        if (!roleExist)
        {
            _ = await roleManager.CreateAsync(new IdentityRole("User"));
        }
        roleExist = await roleManager.RoleExistsAsync("Admin");
        if (!roleExist)
        {
            _ = await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
    }

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}