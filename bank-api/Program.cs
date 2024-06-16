using bank_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DevPolicy",
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:5173");
        });
});

builder.Services.AddControllers();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<UserDbContext>();   

builder.Services.AddDbContext<BankApiContext>(options => options.UseInMemoryDatabase("BankApiDb"));
builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("UsersDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevPolicy");
    app.MapSwagger().RequireAuthorization();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
