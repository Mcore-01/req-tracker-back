using Microsoft.AspNetCore.Cors.Infrastructure;

namespace req_tracker_back.Config
{
    public static class CorsConfig
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(ConfigureCorsOptions);
        }
        
        private static void ConfigureCorsOptions(CorsOptions options)
        {
            options.AddPolicy("Development", builder => {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        }
    }
}