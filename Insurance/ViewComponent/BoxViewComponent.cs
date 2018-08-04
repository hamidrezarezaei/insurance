using DAL;
using Insurance.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace insurance_new
{
    public class BoxViewComponent : ViewComponent
    {
        #region Constructor
        private readonly repository repository;
        public BoxViewComponent(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        #region Invoke
        public async Task<IViewComponentResult> InvokeAsync(int id,
                                                            string template = "Default",
                                                            string componentName = "box-container",
                                                            bool isShowTitle = true)
        {
            ViewData["componentName"] = componentName;
            ViewData["isShowTitle"] = isShowTitle;

            var items = this.repository.GetBox(id);
            return View(template, items);
        }
        #endregion
    }
}
