using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bank_api.Models;

public class AccountMovementDto
{    
    public string? Description { get; set; }
    
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }    
}
