using KlubSportowy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace KlubSportowy.Areas.Identity.Pages.Account
{
    public class ChangeEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ChangeEmailModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string NewEmail { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // SprawdŸ poprawnoœæ has³a przed zmian¹ e-maila
            var passwordCheck = await _userManager.CheckPasswordAsync(user, Input.Password);
            if (!passwordCheck)
            {
                ModelState.AddModelError(string.Empty, "Incorrect password.");
                return Page();
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
            var changeEmailResult = await _userManager.ChangeEmailAsync(user, Input.NewEmail, token);
            if (!changeEmailResult.Succeeded)
            {
                foreach (var error in changeEmailResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.NewEmail);
            if (!setUserNameResult.Succeeded)
            {
                foreach (var error in setUserNameResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            TempData["StatusMessage"] = "Your email has been changed.";

            return RedirectToPage("./ChangeEmailConfirmation");
        }
    }
}
