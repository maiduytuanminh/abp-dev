using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Docs.Pages.Shared.Components.Head
{
    public class HeadViewComponent : SmartSoftwareViewComponent
    {
        public virtual IViewComponentResult Invoke()
        {
            return View("/Pages/Shared/Components/Head/Default.cshtml");
        }
    }
}
