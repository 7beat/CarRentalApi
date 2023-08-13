using CarRental.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Identity.Models;
public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }
}
