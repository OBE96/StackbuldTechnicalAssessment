using Microsoft.EntityFrameworkCore;

namespace StackbuldTechnicalAssessment.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
    }
}
