using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace req_tracker_back.Config
{
    public static class AuthenticationConfig
    {
        private static readonly TokenValidationParameters _validationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:8080/realms/rtrealm",
            ValidateAudience = false,
            ValidateLifetime = true,
        };

        private static readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager = new(
            "http://localhost:8080/realms/rtrealm/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever { RequireHttps = false }
        );

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(ConfigureJWTOptions);
        }

        private static void ConfigureJWTOptions(JwtBearerOptions options)
        {
            options.Authority = "http://localhost:8080/realms/rtrealm";

            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = _validationParameters;

            options.ConfigurationManager = _configurationManager;
        }
    }
}