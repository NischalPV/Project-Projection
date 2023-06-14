using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Projection.Identity.Models;
using Projection.Identity.Services;

namespace Projection.Identity.Controllers;

public class AccountController : Controller
{
    private readonly ILoginService<ApplicationUser> _loginService;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;


    //ctor with null checks
    public AccountController(ILoginService<ApplicationUser> loginService,
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        ILogger<AccountController> logger,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        _interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
        _clientStore = clientStore ?? throw new ArgumentNullException(nameof(clientStore));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    #region Controller Actions

    /// <summary>
    /// Show login page
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (context?.IdP != null)
        {
            // if IdP is passed, then bypass showing the login screen
            throw new NotImplementedException("External login is not implemented.");
        }

        var vm = await BuildLoginViewModelAsync(returnUrl, context);

        return View(vm);
    }

    /// <summary>
    /// Handle postback from username/password login
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Phone);
            if (await _loginService.ValidateCredentials(user, model.Password))
            {
                var tokenLifetime = _configuration.GetValue<int>("TokenLifetimeMinutes", 120);

                //get auth props
                var props = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(tokenLifetime),
                    AllowRefresh = true,
                    RedirectUri = model.ReturnUrl,
                };

                //if remember me is checked, set the expiration to 365 days
                if (model.RememberMe)
                {
                    var permanentTokenLifetime = _configuration.GetValue("PermanentTokenLifetimeDays", 365);

                    props.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(permanentTokenLifetime);
                    props.IsPersistent = true;
                }

                //sign in using login service
                await _loginService.SignInAsync(user, props);

                // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return Redirect("~/");
            }

            // invalid username or password
            ModelState.AddModelError("", "Invalid username or password");

        }

        // something went wrong, show form with error
        var vm = await BuildLoginViewModelAsync(model);

        ViewData["ReturnUrl"] = model.ReturnUrl;

        return View(vm);
    }

    /// <summary>
    /// Show logout page
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId){
        if (User.Identity.IsAuthenticated == false)
        {
            // if the user is not authenticated, then just show logged out page
            return await Logout(new LogoutViewModel { LogoutId = logoutId });
        }

        //Test for Xamarin. 
        var context = await _interaction.GetLogoutContextAsync(logoutId);
        if (context?.ShowSignoutPrompt == false)
        {
            //it's safe to automatically sign-out
            return await Logout(new LogoutViewModel { LogoutId = logoutId });
        }

        // show the logout prompt. this prevents attacks where the user
        // is automatically signed out by another malicious web page.
        var vm = new LogoutViewModel
        {
            LogoutId = logoutId
        };
        return View(vm);
    }

/// <summary>
    /// Handle logout page postback
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(LogoutViewModel model)
    {
        var idp = User?.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

        if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider)
        {
            if (model.LogoutId == null)
            {
                // if there's no current logout context, we need to create one
                // this captures necessary info from the current logged in user
                // before we signout and redirect away to the external IdP for signout
                model.LogoutId = await _interaction.CreateLogoutContextAsync();
            }

            string url = "/Account/Logout?logoutId=" + model.LogoutId;

            try
            {

                // hack: try/catch to handle social providers that throw
                await HttpContext.SignOutAsync(idp, new AuthenticationProperties
                {
                    RedirectUri = url
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LOGOUT ERROR: {ExceptionMessage}", ex.Message);
            }
        }

        // delete authentication cookie
        await HttpContext.SignOutAsync();

        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

        // set this so UI rendering sees an anonymous user
        HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

        // get context information (client name, post logout redirect URI and iframe for federated signout)
        var logout = await _interaction.GetLogoutContextAsync(model.LogoutId);

        return Redirect(logout?.PostLogoutRedirectUri);
    }

    public async Task<IActionResult> DeviceLogOut(string redirectUrl)
    {
        // delete authentication cookie
        await HttpContext.SignOutAsync();

        // set this so UI rendering sees an anonymous user
        HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

        return Redirect(redirectUrl);
    }

    // GET: /Account/Register
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                LastName = model.User.LastName,
                FirstName = model.User.FirstName,
                PhoneNumber = model.User.PhoneNumber,

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Errors.Count() > 0)
            {
                AddErrors(result);
                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }

        if (returnUrl != null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect(returnUrl);
            else
                if (ModelState.IsValid)
                return RedirectToAction("login", "account", new { returnUrl = returnUrl });
            else
                return View(model);
        }

        return RedirectToAction("index", "home");
    }

    [HttpGet]
    public IActionResult Redirecting()
    {
        return View();
    }
    
    #endregion


    #region Helpers
    private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginViewModel model)
    {
        var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
        var vm = await BuildLoginViewModelAsync(model.ReturnUrl, context);
        vm.Phone = model.Phone;
        vm.RememberMe = model.RememberMe;
        return vm;
    }

    private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl, AuthorizationRequest context)
    {
        var allowLocal = true;
        if (context?.Client?.ClientId != null)
        {
            var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
            if (client != null)
            {
                allowLocal = client.EnableLocalLogin;

            }
        }

        return new LoginViewModel
        {

            ReturnUrl = returnUrl,
            Phone = context?.LoginHint,
        };
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    #endregion

}