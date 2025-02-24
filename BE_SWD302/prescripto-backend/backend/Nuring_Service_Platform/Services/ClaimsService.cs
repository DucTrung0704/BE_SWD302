using System.Security.Claims;

namespace Nuring_Service_Platform.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid CurrentUserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }

        public List<string> CurrentUserRoles
        {
            get
            {
                var roles = _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role);
                return roles?.Select(x => x.Value)?.ToList() ?? new List<string>();
            }
        }
    }
}
