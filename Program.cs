//using FraudMonitoringSystem.Authentication;
using FraudMonitoringSystem.Authentication;
using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Hub;
using FraudMonitoringSystem.Repositories;
using FraudMonitoringSystem.Repositories.Customer.Implementations;
using FraudMonitoringSystem.Repositories.Customer.Implementations.Admin;
using FraudMonitoringSystem.Repositories.Customer.Implementations.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Implementations.Investigator;
using FraudMonitoringSystem.Repositories.Customer.Implementations.Rules;
using FraudMonitoringSystem.Repositories.Customer.Implementations.Watchlist;
using FraudMonitoringSystem.Repositories.Customer.Interfaces;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Rules;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.Watchlist;
using FraudMonitoringSystem.Repositories.Notification;
using FraudMonitoringSystem.Services.Customer.Implementations;
using FraudMonitoringSystem.Services.Customer.Implementations.Admin;
using FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Implementations.Investigator;
using FraudMonitoringSystem.Services.Customer.Implementations.Rules;
using FraudMonitoringSystem.Services.Customer.Implementations.Watchlist;
using FraudMonitoringSystem.Services.Customer.Implementations.Watchlist.FraudMonitoringSystem.Services.Implementations;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.Admin;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;
using FraudMonitoringSystem.Services.Customer.Interfaces.ComplianceOfficer;
using FraudMonitoringSystem.Services.Customer.Interfaces.Investigator;
using FraudMonitoringSystem.Services.Customer.Interfaces.Rules;
using FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist;
using FraudMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

//using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddDbContext<WebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("webContext")));


builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();




builder.Services.AddScoped<IPersonalDetailsRepository, PersonalDetailsRepository>();
builder.Services.AddScoped<IPersonalDetailsService, PersonalDetailsService>();


builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddScoped<IKYCRepository, KYCRepository>();
builder.Services.AddScoped<IKYCService, KYCService>();

builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();
builder.Services.AddScoped<IEntityLinkRepository, EntityLinkRepository>();

// Register services
builder.Services.AddScoped<IWatchlistService, WatchlistService>();
builder.Services.AddScoped<IEntityLinkService, EntityLinkService>();

//builder.Services.AddScoped<IChatRepository, ChatRepository>();
//builder.Services.AddScoped<IChatService, ChatService>();

//builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
//builder.Services.AddScoped<INotificationService, NotificationService>();


builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();


builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();





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


// Repository Dependency Injection
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<ICaseRepository, CaseRepository>();
builder.Services.AddScoped<IAlertCaseMappingRepository, AlertCaseMappingRepository>();
builder.Services.AddScoped<ICaseAttachmentRepository, CaseAttachmentRepository>();
builder.Services.AddScoped<IInvestigationNoteRepository, InvestigationNoteRepository>();


// Service Dependency Injection
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<IAlertCaseMappingService, AlertCaseMappingService>();
builder.Services.AddScoped<ICaseAttachmentService, CaseAttachmentService>();
builder.Services.AddScoped<IInvestigationNoteService, InvestigationNoteService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddScoped<IControlChecklistRepository, ControlChecklistRepository>();

builder.Services.AddScoped<IControlChecklistService, ControlChecklistService>();





builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<ICaseRepository, CaseRepository>();

builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<ICaseService, CaseService>();

builder.Services.AddScoped<IKYCRequestRepository, KYCRequestRepository>();
builder.Services.AddScoped<IKYCRequestService, KYCRequestService>();

builder.Services.AddScoped<ITransactionPatternRepository, TransactionPatternRepository>();
builder.Services.AddScoped<ITransactionPatternService, TransactionPatternService>();


//builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();
//builder.Services.AddScoped<IWatchlistService, WatchlistService>();


//builder.Services.AddScoped<IEntityResolutionRepository, EntityResolutionRepository>();
//builder.Services.AddScoped<IEntityResolutionService, EntityResolutionService>();


//builder.Services.AddScoped<INotificationService, NotificationService>();
//builder.Services.AddScoped<IChatService, ChatService>();


builder.Services.AddScoped<PersonalDetailsRepository>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<KYCRepository>();
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();





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



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173") // React dev server
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

app.UseMiddleware<FraudMonitoringSystem.Aspects.GlobalExceptionHandler>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend"); // apply the CORS policy
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Uploads"

});


app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
