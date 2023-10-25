using EmployeeAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options) => {
    options.AddPolicy("DevCors", (corsBuilder) => {
       corsBuilder.AllowAnyOrigin()
       .AllowAnyHeader()
       .AllowAnyMethod();
    });
    options.AddPolicy("ProdCors", (corsBuilder) => {
       corsBuilder.WithOrigins("https://myProductionSite.com")
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials(); 
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(5,2,41))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
