using KlubSportowy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace KlubSportowy.Areas.Identity.Pages.Account
{
    public class EditAccountModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EditAccountModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public ChangePasswordInputModel ChangePasswordInput { get; set; }

        [BindProperty]
        public ChangeEmailInputModel ChangeEmailInput { get; set; }

        public class ChangePasswordInputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class ChangeEmailInputModel
        {
            [Required]
            [EmailAddress]
            public string NewEmail { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        // Zmiana has³a
        public async Task<IActionResult> OnPostChangePasswordAsync()
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

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, ChangePasswordInput.CurrentPassword, ChangePasswordInput.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            // Automatyczne ponowne zalogowanie po zmianie has³a
            await _signInManager.RefreshSignInAsync(user);
            TempData["StatusMessage"] = "Your password has been changed.";

            return RedirectToPage();
        }

        // Zmiana e-maila
        public async Task<IActionResult> OnPostChangeEmailAsync()
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
            var passwordCheck = await _userManager.CheckPasswordAsync(user, ChangeEmailInput.Password);
            if (!passwordCheck)
            {
                ModelState.AddModelError(string.Empty, "Incorrect password.");
                return Page();
            }

            // Generowanie tokena zmiany e-maila
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, ChangeEmailInput.NewEmail);
            var changeEmailResult = await _userManager.ChangeEmailAsync(user, ChangeEmailInput.NewEmail, token);
            if (!changeEmailResult.Succeeded)
            {
                foreach (var error in changeEmailResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            // Aktualizacja nazwy u¿ytkownika (jeœli dotyczy)
            var setUserNameResult = await _userManager.SetUserNameAsync(user, ChangeEmailInput.NewEmail);
            if (!setUserNameResult.Succeeded)
            {
                foreach (var error in setUserNameResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            // Odœwie¿enie logowania po zmianie e-maila
            await _signInManager.RefreshSignInAsync(user);
            TempData["StatusMessage"] = "Your email has been changed.";

            return RedirectToPage();
        }
    }
}
