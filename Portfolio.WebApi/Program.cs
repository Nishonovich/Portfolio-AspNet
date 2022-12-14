using Microsoft.EntityFrameworkCore;
using Portfolio.WebApi.Data;
using Portfolio.WebApi.Interfaces.IRepositories;
using Portfolio.WebApi.Interfaces.Services;
using Portfolio.WebApi.Mappers;
using Portfolio.WebApi.Middlewares;
using Portfolio.WebApi.Repositories;
using Portfolio.WebApi.Services;
using Portfolio.WebApi.Helpers;
using Telegram.Bot;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Portfolio.WebApi.Configurations;

//---->Services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectServices>();

//-----> Cors polisy
builder.Services.AddCors(CorsOptions =>
{
    CorsOptions.AddPolicy("AllowAll", corsAccesses =>
    corsAccesses.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMemoryCache();

builder.Services.ConfigureJwtAuthorize(builder.Configuration);
builder.Services.ConfigureSwaggerAuthorize();


var botToken = builder.Configuration.GetSection("TelegramBotToken")["Production"];
builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(p =>
    new TelegramBotClient(botToken));


//----> Database 
string connectionString = builder.Configuration.GetConnectionString("PostgresProdDb");
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseNpgsql(connectionString);
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});




//----> Middleware
var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseStaticFiles();
app.UseHttpsRedirection();
//-----> corsni chaqirish
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
