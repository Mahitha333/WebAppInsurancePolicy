
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebAppInsurancePolicy.Models;
using WebAppInsurancePolicy.Models.Db;
using static WebAppInsurancePolicy.Models.SecurityService;

namespace WebAppInsurancePolicy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(config =>
            {
                config.AddPolicy("policy2", configurePolicy =>
                {
                    configurePolicy.AllowAnyHeader();
                    configurePolicy.AllowAnyMethod();
                    configurePolicy.WithOrigins("http://localhost:5269", "http://localhost:4200");
                });
            });


            //Add authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:audience"],
                    ValidIssuer = builder.Configuration["Jwt:issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
                };
            });

            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                /*config.AddPolicy(Policies.Manager, Policies.ManagerPolicy());
                config.AddPolicy(Policies.Executive, Policies.ExecutivePolicy());*/
            });


            // Add services to the container.


            builder.Services.AddControllers();
            string connectionstring = "Server=localhost;database=InsuranceDb;trusted_connection=yes;trustservercertificate=true";
            builder.Services.AddDbContext<InsuranceDbContext>(config => config.UseSqlServer(connectionstring));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //add Insuranceserice as a service for dependency injection
            builder.Services.AddTransient(typeof(InsuranceService));
            builder.Services.AddTransient(typeof(SecurityService));
            builder.Services.AddTransient(typeof(DataService));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("policy2");
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

