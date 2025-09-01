using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackbuldTechnicalAssessment.Infrastructure.Context;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;
using StackbuldTechnicalAssessment.Infrastructure.Repository;

namespace StackbuldTechnicalAssessment.Infrastructure
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<DbContext, ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));


            return services;
        }
    }
}
