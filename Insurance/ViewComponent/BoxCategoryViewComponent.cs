using DAL;
using Insurance.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace insurance_new
{
    public class BoxCategoryViewComponent : ViewComponent
    {
        #region Constructor
        private readonly repository repository;
        public BoxCategoryViewComponent(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        #region Invoke
        public async Task<IViewComponentResult> InvokeAsync(
            int id,
            string template = "Default",
            string componentName = "box-group-category",
            bool isShowCategoryTitle = true,
            bool isShowTitle = true,
            bool isShowImage = true,
            bool isShowContent = true,
            int interval = 5000
            )
        {
            ViewData["componentName"] = componentName;
            ViewData["isShowCategoryTitle"] = isShowCategoryTitle;
            ViewData["categoryTitle"] = this.repository.GetBoxCategoryTitle(id);
            ViewData["isShowTitle"] = isShowTitle;
            ViewData["isShowImage"] = isShowImage;
            ViewData["isShowContent"] = isShowContent;
            ViewData["interval"] = interval;
            var items = await repository.GetActiveBoxesOfCategory_Async(id);
            return View(template, items);
        } 
        #endregion
    }
}
