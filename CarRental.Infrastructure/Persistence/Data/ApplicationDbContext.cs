using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Persistence.Data;
internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
}
