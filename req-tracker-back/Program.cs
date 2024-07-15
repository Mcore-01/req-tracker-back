using Microsoft.EntityFrameworkCore;
using req_tracker_back.Data;
using req_tracker_back.Repositories;
using req_tracker_back.Services;
using req_tracker_back.SignalR;
using req_tracker_back.Keycloak;
using req_tracker_back.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RTContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<TicketsService>();
builder.Services.AddScoped<TicketsRepository>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<KeycloakClient>();

builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication();

builder.Services.AddSignalR();
builder.Services.AddControllers();
var app = builder.Build();
app.UseCors("Development");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<TicketHud>("/ticket").RequireCors("Development");
app.Run();