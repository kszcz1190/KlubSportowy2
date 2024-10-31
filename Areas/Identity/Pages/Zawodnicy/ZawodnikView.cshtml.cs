using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KlubSportowy.Areas.Identity.Pages.Zawodnicy
{
    [Authorize]
    public class ZawodnikViewModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
