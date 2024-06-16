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
    [Route("deposit")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly BankApiContext _context;

        public DepositController(BankApiContext context)
        {
            _context = context;
        }       

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountMovement>> DepositMoney(AccountMovementDto accountMovementDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            var accountMovement = AccountMovement.CreateDeposit(
                accountMovementDto.Description, 
                accountMovementDto.Amount, 
                userId
            );

            _context.AccountMovements.Add(accountMovement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("DepositMoney", new { id = accountMovement.Id }, accountMovement);
        }       

    }
}
