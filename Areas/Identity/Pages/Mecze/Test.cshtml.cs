using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KlubSportowy.Areas.Identity.Pages.Mecze
{
    [Authorize]
    public class TestModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
