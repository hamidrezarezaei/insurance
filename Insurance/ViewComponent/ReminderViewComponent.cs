using DAL;
using Entities;
using Insurance.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace insurance_new
{
    public class ReminderViewComponent : ViewComponent
    {
        #region Construcotr
        private readonly repository repository;
        public ReminderViewComponent(repository repository)
        {
            this.repository = repository;
        }
        #endregion

        #region Invoke
        public async Task<IViewComponentResult> InvokeAsync(string template = "Default",
                                                            string componentName = "reminder",

                                                            string fullNameClass = "col-12 col-md-6",
                                                            string mobileClass = "col-12 col-md-6",
                                                            string dayClass = "col-12 col-md-6",
                                                            string monthClass = "col-12 col-md-6",
                                                            string emailClass = "col-12 col-md-6",
                                                            string insuranceTypeClass = "col-12 col-md-6",
                                                            string commentClass = "col-12",

                                                            string submitText = "ثبت"


            )
        {
            ViewData["componentName"] = componentName;

            ViewData["fullNameClass"] = fullNameClass;
            ViewData["mobileClass"] = mobileClass;
            ViewData["dayClass"] = dayClass;
            ViewData["monthClass"] = monthClass;
            ViewData["emailClass"] = emailClass;
            ViewData["insuranceTypeClass"] = insuranceTypeClass;
            ViewData["commentClass"] = commentClass;

            ViewData["submitText"] = submitText;

            return View(template, new reminder());
        } 
        #endregion
    }
}
