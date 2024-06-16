using Microsoft.EntityFrameworkCore;

namespace bank_api.Models;

public class BankApiContext : DbContext
{
    public BankApiContext(DbContextOptions<BankApiContext> options)
        : base(options)
    {
    }

    public DbSet<AccountMovement> AccountMovements { get; set; } = null!;
}