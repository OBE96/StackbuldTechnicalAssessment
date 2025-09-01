using Microsoft.OpenApi.Models;

namespace StackbuldTechnicalAssessment.Web.Extensions
{
    public static class Documentation
    {
        public static void AddSwaggerDocs(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stackbuld Technical Assessment", Version = "v1.0", Description = "API for Stackbuld Technical Assessment" });

            });
        }
    }
}
