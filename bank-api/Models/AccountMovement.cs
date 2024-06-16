using System.ComponentModel;

namespace bank_api.Models;

public class AccountMovement
{
    public int Id { get; internal set;}
    
    public string? Description { get; internal set;}
    
    public decimal Amount { get; internal set;}
    
    public DateTime Date { get; internal set;}
    
    public string UserId { get; internal set;}

    private AccountMovement( string description, decimal amount, string userId)
    {        
        UserId = userId;
        Description = description;
        Amount = amount;
        Date = DateTime.Now;;
    }

    public static AccountMovement CreateDeposit(string description, decimal amount, string userId)
    {
        return new AccountMovement(description, amount, userId);
    }

    public static AccountMovement CreateWithdrawal(string description, decimal amount, string userId)
    {
        return new AccountMovement(description, -amount, userId);
    }
}
