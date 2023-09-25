using Microsoft.EntityFrameworkCore;
using TourPackage.Models;
using TourPackage.Models.Context;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Services;

namespace TourPackage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TourContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("CORS", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });




            builder.Services.AddScoped<IRepo<Destination, int>, DestinationRepo>();
            builder.Services.AddScoped<IRepo<Exclusions, int>, ExclusionsRepo>();
            builder.Services.AddScoped<IRepo<Inclusions,int>,InclusionsRepo>(); 
            builder.Services.AddScoped<IRepo<TourDetails, int>, TourDetailsRepo>();
            builder.Services.AddScoped<IRepo<TourDestination, int>, TourDestinationRepo>();
            builder.Services.AddScoped<IRepo<TourExclusion, int>, TourExclusionRepo>();
            builder.Services.AddScoped<IRepo<TourInclusion, int>, TourInclusionRepo>();
            builder.Services.AddScoped<IRepo<TourDate, int>,TourDatesRepo>();
            builder.Services.AddScoped<IManageTourDetails, ManageTourDetailsService>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CORS");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}