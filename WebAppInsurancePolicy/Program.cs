
using Microsoft.EntityFrameworkCore;
using WebAppInsurancePolicy.Models;
using WebAppInsurancePolicy.Models.Db;

namespace WebAppInsurancePolicy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            string connectionstring = "Server=localhost;database=InsuranceDb;trusted_connection=yes;trustservercertificate=true";
            builder.Services.AddDbContext<InsuranceDbContext>(config => config.UseSqlServer(connectionstring));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient(typeof(InsuranceService));

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
        }
    }
}