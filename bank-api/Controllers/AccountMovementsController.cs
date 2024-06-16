using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bank_api.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace bank_api.Controllers
{
    [Route("account-movements")]
    [ApiController]
    public class AccountMovementsController : ControllerBase
    {
        private readonly BankApiContext _context;

        public AccountMovementsController(BankApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccountMovement>>> GetAccountMovements()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            return await _context.AccountMovements
                .Where(accountMovement => accountMovement.UserId == userId).ToListAsync();
        }
    }
}
