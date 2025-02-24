namespace Nuring_Service_Platform.Services
{
    public interface IClaimsService
    {
        public Guid CurrentUserId { get; }
        public List<string>? CurrentUserRoles { get; }
    }
}
