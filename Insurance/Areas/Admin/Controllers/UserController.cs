using System.Linq;
using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public UserController(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        public IActionResult Index()
        {
            var users = this.repository.GetUsers_ThisSite();
            return View(users);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = this.repository.GetUser_Express(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new user());
        }

        [HttpPost]
        public IActionResult Create(user_express user)
        {
            if (ModelState.IsValid)
            {
                var result = repository.AddUser(user);
            }
            return RedirectToAction("Index");
        }

        public IActionResult PaymentTypes(int id, string returnUrl = null)
        {

            var user = this.repository.GetUser_Express(id);
            if (user == null)
            {
                return NotFound();
            }

            user_vm user_vm = new user_vm
            {
                user = user,
                paymentTypes = this.repository.GetPaymentTypes().Where(pt => !pt.showForAll).ToList(),
                selectedPaymentTypes = this.repository.GetAcitvePaymentTypes(id).Select(pt => pt.id).ToList()
            };
            ViewData["ReturnUrl"] = returnUrl;
            return View(user_vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentTypes(int id, int[] paymentTypes, string returnUrl = null)
        {
            var user = this.repository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            this.repository.SetPaymentTypesForUser(id, paymentTypes);
            return RedirectToLocal(returnUrl);
        }

        public IActionResult Role(int id, string returnUrl = null)
        {

            var user = this.repository.GetUser_Express(id);
            if (user == null)
            {
                return NotFound();
            }

            user_vm user_vm = new user_vm
            {
                user = user,
                roles = new SelectList(this.repository.GetAllRoles_ThisSite(), "Name", "actualName"),
                selectedRole = this.repository.GetRole(id)
            };
            ViewData["ReturnUrl"] = returnUrl;
            return View(user_vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Role(int id, string selectedRole, string returnUrl = null)
        {
            repository.AssignRoleToUser(id, selectedRole);
            return RedirectToLocal(returnUrl);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = this.repository.GetUser_Express(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            repository.DeleteUser(id);
            return RedirectToAction("Index");
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

    }

}