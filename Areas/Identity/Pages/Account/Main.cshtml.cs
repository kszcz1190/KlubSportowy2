using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KlubSportowy.Areas.Identity.Pages.Account
{
    [Authorize]
    public class MainModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
