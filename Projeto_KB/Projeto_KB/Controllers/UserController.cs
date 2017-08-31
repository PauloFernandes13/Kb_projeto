using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Projeto_KB.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;

namespace Projeto_KB.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        public UserController()
        {

        }
        public UserController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: User
        public ActionResult Index()
        {

            List<UserViewModel> list = new List<UserViewModel>();
            foreach (var role in UserManager.Users)
                list.Add(new UserViewModel(role));
            return View(list);
        }


        public ActionResult ManageUsersRoles()
        {
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(r_user => new SelectListItem
            {
                Value = r_user.Name.ToString(),
                Text = r_user.Name
            }).ToList();

            ViewBag.Roles = list;
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            return View(new UserViewModel(user));

        }
        //Used inicially Async method, but UpdateAsync...not save on context...error: already exist object with that ID.
        //Solution to error: not not use Async Method... 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName,PhoneNumber,AccountName,ContactName")] UserViewModel user)
        {

            ApplicationUser User = UserManager.FindById(user.Id);

            User.Id = user.Id;
            User.Email = user.Email;
            User.UserName = user.UserName;
            User.PhoneNumber = user.PhoneNumber;
            User.AccountName = user.AccountName;
            User.ContactName = user.ContactName;


            IdentityResult result = UserManager.Update(User);


            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details (string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new UserViewModel(user));
        } 

        public async Task<ActionResult> Delete (string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new UserViewModel(user));
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed (string id )
        {
            var user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public ActionResult ManageUsers(string id)
        {
            ApplicationUser User= db.Users.Where(u => u.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(r_user => new SelectListItem
            {
                Value = r_user.Name.ToString(),
                Text = r_user.Name
            }).ToList();
            ViewBag.Roles = list;
            return View(User);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            manager.AddToRole(user.Id, RoleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // populate roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUsers");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

                ViewBag.RolesForThisUser = manager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUsers");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

            ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (manager.IsInRole(user.Id, RoleName))
            {
                manager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown, error like in register in Account
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUsers");
        }


    }
}
