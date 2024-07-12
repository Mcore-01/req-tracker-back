using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using req_tracker_back.Data;
using req_tracker_back.Repositories;
using req_tracker_back.Services;
using req_tracker_back.SignalR;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RTContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<TicketsService>();
builder.Services.AddScoped<TicketsRepository>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<UsersRepository>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Development",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                ;
        });
});
builder.Services.AddSignalR();
builder.Services.AddControllers();
var app = builder.Build();
app.UseCors("Development");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<TicketHud>("/ticket").RequireCors("Development");
app.Run();

public class AuthOptions
{
    public const string ISSUER = "RTBack";
    public const string AUDIENCE = "RTClient";
    const string KEY = "prostokeyfor1234231dsfsdq3shiphrovki014345";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}