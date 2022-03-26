using System.Text;
using Domain;
using Domain.Commands;
using Domain.Options;
using Domain.Services;
using Domain.Validators;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(behaviorOptions => behaviorOptions.SuppressModelStateInvalidFilter = true)
    .AddNewtonsoftJson();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteFile")));
builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddMediatR(typeof(MediatREntryPoint).Assembly);
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var tokenOptions = builder.Configuration .GetSection(TokenOptions.SectionName);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.ClaimsIssuer = tokenOptions["Issuer"];
        options.RequireHttpsMetadata = false;
        options.Audience = tokenOptions["Audience"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = tokenOptions["Issuer"],
            ValidateAudience = true,
            ValidAudience = tokenOptions["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenOptions["ClientSecret"])),
            ValidateLifetime = false
        };
    });

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection(TokenOptions.SectionName));

builder.Services.AddScoped<IValidator<CreateLockCommand>, CreateLockCommandValidator>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapDefaultControllerRoute();
app.UseAuthentication();
app.UseAuthorization();

app.Run();