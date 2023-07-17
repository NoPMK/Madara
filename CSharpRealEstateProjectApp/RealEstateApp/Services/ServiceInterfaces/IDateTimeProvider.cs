namespace RealEstate.Services.ServiceInterfaces
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}