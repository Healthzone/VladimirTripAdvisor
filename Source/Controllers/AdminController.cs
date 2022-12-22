using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult AllUsers()
        {
            var users = _db.Users.ToList();

            IList<UserViewModel> userList = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userRoles = _userManager.GetRolesAsync(user).Result;
                string mainRole;
                if (userRoles.Contains(WC.OwnerRole))
                {
                    mainRole = WC.OwnerRole;
                }
                mainRole = userRoles.FirstOrDefault();

                UserViewModel userViewModel = new UserViewModel
                {
                    User = (UserModel)user,
                    AccountType = mainRole
                };
                userList.Add(userViewModel);
            }
            return View(userList);
        }

        public IActionResult DeleteUser(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            var user = _db.User.Find(userId);

            if (user == null)
            {
                return NotFound();
            }
            _db.User.Remove(user);
            _db.SaveChanges();
            return View("AllUsers");
        }

        [Authorize(Roles = WC.AdminRole)]
        public IActionResult CheckApplication()
        {
            IEnumerable<ApplicationModelDB> apps = _db.Application.Include(x => x.User);
            return View(apps);
        }
    }
}
