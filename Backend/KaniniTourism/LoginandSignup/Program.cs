using LoginandSignup.Interfaces;
using LoginandSignup.Models.Context;
using LoginandSignup.Models;
using LoginandSignup.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;
using System.Text;
using LoginandRegistration.Services;
using Microsoft.EntityFrameworkCore;

namespace LoginandSignup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UserContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });

            builder.Services.AddScoped<IManageUser, ManageUserService>();
            builder.Services.AddScoped<IManageTravelAgent,ManageTravelAgentService>();
            builder.Services.AddScoped<IManageTraveller,ManageTravellerService>();


            builder.Services.AddScoped<IRepo<User, int>, UserRepo>();
            builder.Services.AddScoped<IRepo<TravelAgent, int>, TravelAgentRepo>();
            builder.Services.AddScoped<IRepo<Traveller, int>, TravellerRepo>();
            builder.Services.AddScoped<IGenerateToken, GenerateTokenService>();
            builder.Services.AddScoped<IRepo<Admin, int>, AdminRepo>();
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("CORS", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CORS");

            app.UseAuthentication();



            app.MapControllers();

            app.Run();
        }
    }
}