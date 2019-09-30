using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LibraryManagement.Models;

using System.Data.Entity;

namespace LibraryManagement.Controllers
{
    public class RoleController : Controller
    {
        // GET: Roles
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            // Populate DropDownList
            var context = new ApplicationDbContext();
            var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;


            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;

            ViewBag.Message = "";
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.Message = "Role Created Successfully !!!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string roleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(thisRole);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Entry(role).State = EntityState.Modified;
                context.SaveChanges();
                ViewBag.Message = "Role Edited Successfully !!!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string roleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            ViewBag.Message = "Role Deleted Successfully !!!";
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            var context = new ApplicationDbContext();
            if (context == null)
            {
                throw new ArgumentNullException("context", "Context not to be null !!!");
            }

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRoles(user.Id, RoleName);


            ViewBag.Message = "Role add to user successfully !!!";

            // Populate DropDownList
            var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;


            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;
            return View("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                var context = new ApplicationDbContext();

                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ViewBag.RolesForThisUser = userManager.GetRoles(user.Id);

                // Populate DropDownList
                var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = roleList;

                var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
                ViewBag.Users = userList;

                ViewBag.Message = "Role retrived successfully !!!";
            }
            return View("Index");
        }




        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {

            var account = new AccountController();
            var context = new ApplicationDbContext();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (userManager.IsInRole(user.Id, RoleName))
            {
                userManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.Message = "Role removed from this user successfully !!!";
            }
            else
            {
                ViewBag.Message = "This user doesn't belong to the selected role !!!";
            }

            // Populate DropDownList
            var roleList = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;

            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;

            return View("Index");
        }
    }
}