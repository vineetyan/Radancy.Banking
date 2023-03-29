using Radancy.BankingSystem.DataAccess;
using Radancy.BankingSystem.DataAccess.Repository;
using Radancy.BankingSystem.Services.Accounts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BankingDatabaseContext>();
builder.Services.AddSingleton<IBankingDatabaseContext, BankingDatabaseContext>();
builder.Services.AddSingleton<IBankingRepository, BankingRepository>();
builder.Services.AddScoped<IAccountsService, AccountsService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
