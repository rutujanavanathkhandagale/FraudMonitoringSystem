using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer;
using FraudMonitoringSystem.Repositories.Customer.Implementations;
using FraudMonitoringSystem.Repositories.Customer.Implementations.Admin;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Repositories.Implementations;
using FraudMonitoringSystem.Repositories.Interfaces;
using FraudMonitoringSystem.Services.Customer;
using FraudMonitoringSystem.Services.Customer.Implementations;
using FraudMonitoringSystem.Services.Customer.Implementations.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Implementations;
using FraudMonitoringSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<WebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("webContext")));


builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IPersonalDetailsRepository, PersonalDetailsRepository>();
builder.Services.AddScoped<IPersonalDetailsService, PersonalDetailsService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IKYCRepository, KYCRepository>();
builder.Services.AddScoped<IKYCService, KYCService>();

builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatService, ChatService>();




builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();


// ________________________________________________________________________________________________________
//_______________________________________________________________________________________________
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();




builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();


app.MapHub<NotificationHub>("/notificationHub");

app.Run();
