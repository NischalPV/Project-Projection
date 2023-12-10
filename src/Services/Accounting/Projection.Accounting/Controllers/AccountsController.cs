using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projection.Accounting.Core;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Infrastructure.Data;

namespace Projection.Accounting;

[ApiVersion("1.0")]
[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AccountingDbContext _context;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(AccountingDbContext context, ILogger<AccountsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/v1/Accounts
    /// <summary>
    /// Get All Accounts
    /// </summary>
    /// <returns>List of Accounts</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        return await _context.Accounts.ToListAsync();
    }

}
