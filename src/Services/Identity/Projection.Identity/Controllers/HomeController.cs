﻿using Projection.Identity.Models;
using Projection.Identity.Services;
using Duende.IdentityServer.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace Projection.Identity.Controllers;

public class HomeController : Controller
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IOptionsSnapshot<AppSettings> _settings;
    private readonly IRedirectService _redirectSvc;

    public HomeController(IIdentityServerInteractionService interaction, IOptionsSnapshot<AppSettings> settings, IRedirectService redirectSvc)
    {
        _interaction = interaction;
        _settings = settings;
        _redirectSvc = redirectSvc;
    }

    public IActionResult Index(string returnUrl)
    {
        return View();
    }

    public IActionResult ReturnToOriginalApplication(string returnUrl)
    {
        if (returnUrl != null)
            return Redirect(_redirectSvc.ExtractRedirectUriFromReturnUrl(returnUrl));
        else
            return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Shows the error page
    /// </summary>
    public async Task<IActionResult> Error(string errorId)
    {
        var vm = new ErrorViewModel();

        // retrieve error details from identityserver
        var message = await _interaction.GetErrorContextAsync(errorId);
        if (message != null)
        {
            vm.Error = message;
        }

        return View("Error", vm);
    }
}