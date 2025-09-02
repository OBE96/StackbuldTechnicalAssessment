using Microsoft.EntityFrameworkCore;
using StackbuldTechnicalAssessment.Domain.Entities;


namespace StackbuldTechnicalAssessment.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

    }
}
