using DAL;
using Insurance.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace insurance_new
{
    public class MenuViewComponent : ViewComponent
    {
        #region Construcotr
        private readonly repository repository;
        public MenuViewComponent(repository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Invoke
        public async Task<IViewComponentResult> InvokeAsync(int id,
                                                            string template = "Default",
                                                            string componentName = "top-menu")
        {
            var items =  this.repository.GeActiveChildMenus_Async(id);
            ViewData["componentName"] = componentName;
            return View(template, items);
        } 
        #endregion
    }
}
