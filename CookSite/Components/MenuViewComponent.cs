using CookSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookSite.Components
{
    public class MenuViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            CookSiteContext db = new CookSiteContext();
            var cookTypes = db.CookTypes.ToList();
            return View(cookTypes);
        }

    }
}
