using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insurance_new
{
    public class BoxGroupCategoryViewComponent : ViewComponent
    {
        #region Constructor
        private readonly repository repository;

        public BoxGroupCategoryViewComponent(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        #region Invoke
        public async Task<IViewComponentResult> InvokeAsync(int id,
                                                            string template = "Default",
                                                            string componentName = "box-category",
                                                            bool isShowCategoryTitle = true,
                                                            bool isShowImage = false)
        {
            ViewData["componentName"] = componentName;
            ViewData["isShowCategoryTitle"] = isShowCategoryTitle;
            ViewData["categoryTitle"] = this.repository.GetBoxCategoryTitle(id);
            ViewData["isShowImage"] = isShowImage;

            var items = this.repository.GetActiveBoxCategories_Async(id);
            return View(template, items);
        }

        #endregion
    }
}
