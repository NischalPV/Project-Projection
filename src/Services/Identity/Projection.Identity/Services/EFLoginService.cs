using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projection.Identity.Models;
using Microsoft.AspNetCore.Authentication;
namespace Projection.Identity.Services;

public class EFLoginService : ILoginService<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public EFLoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ApplicationUser> FindByUsername(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public Task SignIn(ApplicationUser user)
    {
        return _signInManager.SignInAsync(user, true);
    }

    public Task SignInAsync(ApplicationUser user, AuthenticationProperties properties, string authenticationMethod = null)
    {
        return _signInManager.SignInAsync(user, properties, authenticationMethod);
    }
}