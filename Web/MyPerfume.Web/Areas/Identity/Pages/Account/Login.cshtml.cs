﻿namespace MyPerfume.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using MyPerfume.Data.Models;

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LoginModel> logger;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string UserNameOrEmail { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                this.ModelState.AddModelError(string.Empty, this.ErrorMessage);
            }

            returnUrl = returnUrl ?? this.Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");

            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                Microsoft.AspNetCore.Identity.SignInResult inputUserName;
                Microsoft.AspNetCore.Identity.SignInResult inputEmail;
                Microsoft.AspNetCore.Identity.SignInResult result = Microsoft.AspNetCore.Identity.SignInResult.Failed;

                var userByEmail = await this.userManager.FindByEmailAsync(this.Input.UserNameOrEmail);
                if (userByEmail != null)
                {
                    inputEmail = await this.signInManager.PasswordSignInAsync(userByEmail, this.Input.Password, this.Input.RememberMe, lockoutOnFailure: false);
                    if (inputEmail.Succeeded)
                    {
                        result = inputEmail;
                    }
                }
                else
                {
                    inputUserName = await this.signInManager.PasswordSignInAsync(this.Input.UserNameOrEmail, this.Input.Password, this.Input.RememberMe, lockoutOnFailure: false);
                    if (inputUserName.Succeeded)
                    {
                        result = inputUserName;
                    }
                }

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User logged in.");
                    return this.LocalRedirect(returnUrl);
                }

                var userWithUsername = await this.userManager.FindByNameAsync(this.Input.UserNameOrEmail);
                var isPassOk = await this.userManager.CheckPasswordAsync(userWithUsername, this.Input.Password);
                if (userWithUsername != null && userWithUsername.EmailConfirmed == false && isPassOk)
                {
                    return this.RedirectToPage("RegisterConfirmation", new { email = userWithUsername.Email });
                }

                var userWithEmail = await this.userManager.FindByEmailAsync(this.Input.UserNameOrEmail);
                if (userWithEmail != null && userWithEmail.EmailConfirmed == false && result.Succeeded && isPassOk)
                {
                    return this.RedirectToPage("RegisterConfirmation", new { email = userWithEmail.Email });
                }

                if (result.RequiresTwoFactor)
                {
                    return this.RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = this.Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    this.logger.LogWarning("User account locked out.");
                    return this.RedirectToPage("./Lockout");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
