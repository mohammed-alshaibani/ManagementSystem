using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Pharam_System___V6.ViewModel;

namespace Pharam_System___V6.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> IndexAsync(RegisterLoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "اسم المستخدم او كلمة السر غير صحيح");
                    return View();
                }

            }
            else
            {
                Console.WriteLine("in else in login user");
                return View();
            }

        }
        public IActionResult List()
        {
            var UserList = new RegisterListViewModel
            {
                Users = _userManager.Users.ToList()
            };
            return View(UserList);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            var UserEdit = new RegisterEditViewModel
            {
                Id = User.Id,
                Email = User.Email
            };
            return View(UserEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegisterEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByIdAsync(model.Id);
                User.Email = model.Email;
                User.Id = model.Id;
                var result = await _userManager.UpdateAsync(User);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Account");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            var UserEdit = new RegisterEditViewModel
            {
                Id = User.Id,
                Email = User.Email
            };
            return View(UserEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RegisterEditViewModel model)
        {
            var User = _userManager.Users.FirstOrDefault(x => x.Id == model.Id);
            var result = await _userManager.DeleteAsync(User);
            if (result.Succeeded)
            {
                return RedirectToAction("List", "Account");
            }
            return View(model);
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /*Email Code*/
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // Generate password reset token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Send confirmation email
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);
                var emailBody = $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.";
                try
                {
                    await _emailSender.SendEmailAsync(model.Email, "Reset Password Confirmation", emailBody);
                    _logger.LogInformation($"Sent password reset email to {model.Email}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send password reset email");
                    ModelState.AddModelError(string.Empty, "Failed to send password reset email");
                    return View(model);
                }

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed; redisplay form
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
