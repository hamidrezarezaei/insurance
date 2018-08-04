using DAL;
using Insurance.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Areas.Admin
{
    public class AdminMenuViewComponent : ViewComponent
    {
        #region Constructor
        private readonly repository repository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AdminMenuViewComponent(
            repository repository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Invoke
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var role = repository.GetRoleOfCurrentUser();
            var items =  repository.GetAdminMenusOfRole(role).OrderBy(am=>am.orderIndex).ToList();
            var currentController = this.httpContextAccessor.HttpContext.Request;
            return View("Default", items);
        }
        #endregion
    }
}
