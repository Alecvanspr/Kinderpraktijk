using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace src.Areas.Profile.Pages.Tabs
{
    [Authorize(Roles = "Pedagoog")]
    public class AddAssistentModel : PageModel
    {
        private MijnContext _context;
        private readonly SignInManager<srcUser> _signInManager;
        private readonly UserManager<srcUser> _userManager;
        private readonly IUserStore<srcUser> _userStore;
        //private readonly IUserEmailStore<srcUser> _emailStore;
        private readonly ILogger<AanmeldenModel> _logger;
        private readonly IEmailSender _emailSender;

        public AddAssistentModel(
            UserManager<srcUser> userManager,
            IUserStore<srcUser> userStore,
            SignInManager<srcUser> signInManager,
            ILogger<AanmeldenModel> logger,
            IEmailSender emailSender,
            MijnContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            //_emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel4 Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel4
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = "Temp1234#";

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(50)]
            [DataType(DataType.Text)]
            [Display(Name = "Voornaam")]
            public string FirstName { get; set; }
            [Required]
            [StringLength(50)]
            [DataType(DataType.Text)]
            [Display(Name = "Achternaam")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Geboortedatum")]
            public DateTime Age { get; set; }

            [Required]
            [StringLength(450)]
            [DataType(DataType.Text)]
            [Display(Name = "Beschrijving")]
            public string Description { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var currentUser = _userManager.GetUserAsync(User);

            if(currentUser.Result.AssistentId == null)
            {            
                if (ModelState.IsValid)
                {
                    var user = CreateUser();
                    currentUser.Result.AssistentId = user.Id;
                    await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                    //await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    await _userManager.UpdateAsync(currentUser.Result);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");
                        if (await SetRoleAsync(user))
                        {
                            _logger.LogInformation("Role has been added to the User.");
                        }
                        else
                        {
                            _logger.LogInformation("Adding role to user failed.");
                        }

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            return RedirectToPage("/Tabs/AddAssistent", new { Area = "Profile" });
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        public async Task<bool> SetRoleAsync(srcUser user)
        {
            await _userManager.AddToRoleAsync(user, "Assistent");
            return await _userManager.IsInRoleAsync(user, "Assistent");
        }

        public  srcUser CreateUser()
        {
            try
            {
                return new srcUser
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Age = Input.Age,
                    Email = Input.Email,
                    Description = Input.Description,
                    SpecialistId = _userManager.GetUserId(User)
                };
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(srcUser)}'. " +
                    $"Ensure that '{nameof(srcUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<srcUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<srcUser>)_userStore;
        }
    }
}