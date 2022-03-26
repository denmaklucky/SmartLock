using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source = SmartLock.db"));
builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddMediatR(typeof(MediatREntryPoint).Assembly);

var app = builder.Build();
app.MapDefaultControllerRoute();

app.Run();