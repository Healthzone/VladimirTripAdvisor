using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VladimirTripAdvisor.Components
{
    public class GetUserId : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;
        public GetUserId(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> InvokeAsync()
        {
            var appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return appUser.Id;
        }
    }
}
