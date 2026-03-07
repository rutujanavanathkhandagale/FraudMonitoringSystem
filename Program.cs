using FraudMonitoringSystem.Authentication;
using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Repositories;
using FraudMonitoringSystem.Repositories.Customer.Implementations;
using FraudMonitoringSystem.Repositories.Customer.Implementations.Admin;
using FraudMonitoringSystem.Repositories.Customer.Implementations.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Implementations.ComplianceOfficer;
using FraudMonitoringSystem.Repositories.Customer.Implementations.Investigator;
using FraudMonitoringSystem.Repositories.Customer.Implementations.Rules;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase.FraudMonitoringSystem.Services.AlertCase.Interfaces;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Rules;
using FraudMonitoringSystem.Repositories.Implementations;
using FraudMonitoringSystem.Repositories.Interfaces;
using FraudMonitoringSystem.Services.Customer.Implementations;
using FraudMonitoringSystem.Services.Customer.Implementations.Admin;
using FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Implementations.ComplianceOfficer;
using FraudMonitoringSystem.Services.Customer.Implementations.Investigator;
using FraudMonitoringSystem.Services.Customer.Implementations.Rules;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;
using FraudMonitoringSystem.Services.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.Rules;
using FraudMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

    options.JsonSerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter()
    );
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


builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();


builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();


builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();


builder.Services.AddScoped<ISystemUserRepository, SystemUserRepository>();
builder.Services.AddScoped<ISystemUserService, SystemUserService>();


builder.Services.AddScoped<IScenarioRepository, ScenarioRepository>();
builder.Services.AddScoped<IScenarioService, ScenarioService>();


builder.Services.AddScoped<IDetectionRuleRepository, DetectionRuleRepository>();
builder.Services.AddScoped<IDetectionRuleService, DetectionRuleService>();


builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();


builder.Services.AddScoped<IRiskScoreRepository, RiskScoreRepository>();
builder.Services.AddScoped<IRiskScoreService, RiskScoreService>();

builder.Services.AddScoped<IRegulatoryReportRepository, RegulatoryReportRepository>();
builder.Services.AddScoped<IRegulatoryReportService, RegulatoryReportService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();


builder.Services.AddScoped<IControlChecklistRepository,ControlChecklistRepository>();
builder.Services.AddScoped<IControlChecklistService, ControlChecklistService>();




builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<ICaseRepository, CaseRepository>();

builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<ICaseService, CaseService>();

builder.Services.AddScoped<ISanctionRepository, SanctionRepository>();
builder.Services.AddScoped<ISanctionService, SanctionService>();

builder.Services.AddScoped<IPEPListRepository, PEPListRepository>();
builder.Services.AddScoped<IPEPListService, PEPListService>();

builder.Services.AddScoped<IKYCRequestRepository, KYCRequestRepository>();
builder.Services.AddScoped<IKYCRequestService, KYCRequestService>();

builder.Services.AddScoped<ITransactionPatternRepository, TransactionPatternRepository>();
builder.Services.AddScoped<ITransactionPatternService, TransactionPatternService>();


builder.Services.AddScoped<IAuth, Auth>();



var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});





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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
