using Domain;
using Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(behaviorOptions => behaviorOptions.SuppressModelStateInvalidFilter = true)
    .AddNewtonsoftJson();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source = SmartLock.db"));
builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddMediatR(typeof(MediatREntryPoint).Assembly);
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();
app.MapDefaultControllerRoute();

app.Run();