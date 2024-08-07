using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Projection.Identity.Controllers;

public class ConsentController : Controller
{
    private readonly ILogger<ConsentController> _logger;
    private readonly IClientStore _clientStore;
    private readonly IResourceStore _resourceStore;
    private readonly IIdentityServerInteractionService _interaction;

    public ConsentController(ILogger<ConsentController> logger, IClientStore clientStore, IResourceStore resourceStore, IIdentityServerInteractionService interaction)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _clientStore = clientStore ?? throw new ArgumentNullException(nameof(clientStore));
        _resourceStore = resourceStore ?? throw new ArgumentNullException(nameof(resourceStore));
        _interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
    }

    /// <summary>
    /// Shows the consent screen
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index(string returnUrl)
    {
        var vm = await BuildViewModelAsync(returnUrl);
        ViewData["ReturnUrl"] = returnUrl;
        if (vm != null)
        {
            return View("Index", vm);
        }

        return View("Error");
    }

    /// <summary>
    /// Handles the consent screen postback
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ConsentInputModel model)
    {
        // parse the return URL back to an AuthorizeRequest object
        var request = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
        ConsentResponse response = null;

        // user clicked 'no' - send back the standard 'access_denied' response
        if (model.Button == "no")
        {
            response = new ConsentResponse { Error = AuthorizationError.AccessDenied };
        }
        // user clicked 'yes' - validate the data
        else if (model.Button == "yes" && model != null)
        {
            // if the user consented to some scope, build the response model
            if (model.ScopesConsented != null && model.ScopesConsented.Any())
            {
                response = new ConsentResponse
                {
                    RememberConsent = model.RememberConsent,
                    ScopesValuesConsented = model.ScopesConsented
                };
            }
            else
            {
                ModelState.AddModelError("", "You must pick at least one permission.");
            }
        }
        else
        {
            ModelState.AddModelError("", "Invalid Selection");
        }

        if (response != null)
        {
            // communicate outcome of consent back to identityserver
            await _interaction.GrantConsentAsync(request, response);

            // redirect back to authorization endpoint
            return Redirect(model.ReturnUrl);
        }

        var vm = await BuildViewModelAsync(model.ReturnUrl, model);
        if (vm != null)
        {
            return View("Index", vm);
        }

        return View("Error");
    }

    async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
    {
        var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (request != null)
        {
            var client = await _clientStore.FindEnabledClientByIdAsync(request.Client.ClientId);
            if (client != null)
            {
                var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ValidatedResources.RawScopeValues);
                if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
                {
                    return new ConsentViewModel(model, returnUrl, request, client, resources);
                }
                else
                {
                    _logger.LogError("No scopes matching: {0}", request.ValidatedResources.RawScopeValues.Aggregate((x, y) => x + ", " + y));
                }
            }
            else
            {
                _logger.LogError("Invalid client id: {0}", request.Client.ClientId);
            }
        }
        else
        {
            _logger.LogError("No consent request matching request: {0}", returnUrl);
        }

        return null;
    }
}