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
    [Route("withdraw")]
    [ApiController]
    public class WithdrawController : ControllerBase
    {
        private readonly BankApiContext _context;

        public WithdrawController(BankApiContext context)
        {
            _context = context;
        }       

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountMovement>> WithdrawnMoney(AccountMovementDto accountMovementDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            var accountMovement = AccountMovement.CreateWithdrawal(
                accountMovementDto.Description, 
                accountMovementDto.Amount, 
                userId
            );

            var userMovements = await _context.AccountMovements
                .Where(accountMovement => accountMovement.UserId == userId).ToListAsync();
            
            var userBalance = userMovements.Sum(movement => movement.Amount);

            if (userBalance < accountMovementDto.Amount)
            {
                return BadRequest("Insufficient funds");
            }

            _context.AccountMovements.Add(accountMovement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("WithdrawnMoney", new { id = accountMovement.Id }, accountMovement);
        }       

    }
}
