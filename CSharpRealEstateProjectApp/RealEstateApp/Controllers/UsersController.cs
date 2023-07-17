using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RealEstate.Models;
using RealEstate.Services.ServiceInterfaces;
using RealEstateApp.Exceptions;
using RealEstateApp.Helper;
using RealEstateApp.Models;
using RealEstateApp.Models.DTOs;
using RealEstateApp.Models.DTOs.Create;
using RealEstateApp.Models.DTOs.Details;
using RealEstateApp.Models.DTOs.Update;
using RealEstateApp.Services.ServiceInterfaces;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITokenCreationService _jwtService;
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UsersController(IUserService userService,
            UserManager<ApplicationUser> userManager,
            IDateTimeProvider dateTimeProvider,
            ITokenCreationService jwtService,
            ILogger<UsersController> logger,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _userService = userService;
            _userManager = userManager;
            _dateTimeProvider = dateTimeProvider;
            _jwtService = jwtService;
            _logger = logger;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<RegistrationForm>> Register(RegistrationForm registrationForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = registrationForm.UserName,
                Email = registrationForm.Email
            };

            var result = await _userManager.CreateAsync(user, registrationForm.Password);

            if (result.Succeeded)
            {

                var userFromDb = await _userManager.FindByNameAsync(registrationForm.UserName);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var callbackUrl = $"{_configuration["ReturnPaths:ConfirmEmail"]}/confirmEmail?token={token}&userid={userFromDb.Id}";
                var senderEmail = _configuration["ReturnPaths:SenderEmail"];

                await _emailService.SendEmailAsync(senderEmail, userFromDb.Email, "Please confirm your email address: ", HtmlEncoder.Default.Encode(callbackUrl));

                if (userFromDb.EmailConfirmed)
                {
                    IdentityResult userResult = await _userManager.AddToRoleAsync(user, role: "User");
                }

                User normalUser = new User
                {
                    Id = user.Id,
                    Properties = new List<Property>()
                };

                await _userService.SaveUserAsync(normalUser);

                return CreatedAtAction(nameof(AdminGetUserByIdAsync), new { id = user.Id }, null);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest request)
        {
            var identityResult = new IdentityResult();

            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest(identityResult.Errors);       
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest(identityResult.Errors);
            }

            AuthenticationResponse authenticationResponse = await _jwtService.CreateTokensAsync(user);

            return Ok(authenticationResponse);
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmaildto)
        {
            var user = await _userManager.FindByIdAsync(confirmEmaildto.UserId);
            string token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmEmaildto.Token));

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Admin/{id}")]
        public async Task<IActionResult> AdminChangeRoleByIdAsnyc(string id)
        {
            try
            {
                ApplicationUser user = await _userService.GetApplicationUserByIdAsync(id);
                string role = await _userService.GetApplicationUserRoleById(id);
                if (user is not null && role == "User")
                {
                    await _userManager.RemoveFromRoleAsync(user, role: "User");
                    await _userManager.AddToRoleAsync(user, role: "Admin");
                }
                else if (user is not null && role == "Admin")
                {
                    await _userManager.RemoveFromRoleAsync(user, role: "Admin");
                    await _userManager.AddToRoleAsync(user, role: "User");
                }
                return Ok("Role changed.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Admin/{id}")]
        public async Task<IActionResult> AdminDeleteUserAsync(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok($"User with id: {id}, deleted succsessfully ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdminUserDetailsDto>> AdminGetUserByIdAsync(string id)
        {
            try
            {
                AdminUserDetailsDto user = await _userService.AdminGetUserByIdAsync(id);
                if (user is not null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserByIdAsync(string id)
        {
            UserDetailsDto user = await _userService.GetUserByIdAsync(id);
            try
            {
                if (user is not null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserByIdAsync(UpdateUserDetailsDto updateUserDetails)
        {
            try
            {
                string currentUserId = User.GetCurrentUserId();
                await _userService.UpdateUserByIdAsync(currentUserId, updateUserDetails);

                bool isValidCurrentPassword = await _userManager.CheckPasswordAsync(
                    await _userService.GetApplicationUserByIdAsync(currentUserId),
                    updateUserDetails.CurrentPassword);

                if (!isValidCurrentPassword)
                {
                    throw new InvalidPasswordException("Invalid current password.");
                }

                await _userManager.ChangePasswordAsync(
                    await _userService.GetApplicationUserByIdAsync(currentUserId),
                    updateUserDetails.CurrentPassword,
                    updateUserDetails.NewPassword);

                return Ok("Details changed.");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet(nameof(Me)), Authorize]
        public ActionResult<UserInfo> Me()
        {
            UserInfo user = new UserInfo
            {
                UserId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value,
                UserName = User.Claims.First(claim => claim.Type == ClaimTypes.Name).Value,
                Email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value,
                Roles = User.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(c => c.Value)
            };
            if (user is not null)
            {
                return Ok(user);
            }
            else
            {
                throw new InvalidUserException("You are not logged in.");
            }
        }

        [HttpPost(nameof(Refresh))]
        public async Task<ActionResult<JwtToken>> Refresh(RefreshRequest refreshRequest)
        {
            try
            {
                AuthenticationResponse authenticationResponse = await _jwtService.RenewTokensAsync(refreshRequest.RefreshToken);
                return Ok(authenticationResponse);
            }
            catch (JwtException)
            {
                return Forbid();
            }
        }

        [HttpPost(nameof(Logout)), Authorize]
        public ActionResult Logout(LogoutRequest logoutRequest)
        {
            _jwtService.ClearRefreshToken(logoutRequest.RefreshToken);
            return Ok();
        }

    }
}
