using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bank_api.Models;

public class UserDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) :
        base(options)
    { }
}