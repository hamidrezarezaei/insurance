using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        #region Constructor
        private readonly repository repository;

        public PostController(repository repository)
        {
            this.repository = repository;
        }
        #endregion
        public IActionResult Index()
        {
            var posts = this.repository.GetPosts();
            return View(posts);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = this.repository.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new post());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(post post, IFormFile image, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                repository.AddEntity(post, image);
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(post);
        }

        // GET: insurances/Edit/5
        public IActionResult Edit(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = this.repository.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, post post, IFormFile image, string returnUrl = null)
        {
            if (id != post.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                post = (post)this.repository.UpdateEntity(post, image);

                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(post);
        }
        public IActionResult Delete(int? id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = this.repository.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(post);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string returnUrl = null)
        {
            var post = this.repository.GetPost(id);
            this.repository.DeleteEntity(post);
            return RedirectToLocal(returnUrl);
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