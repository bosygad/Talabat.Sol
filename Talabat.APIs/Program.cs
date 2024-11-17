
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middleware;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.BDContext;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
          

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddApplicationServices();

            #endregion

            var app = builder.Build();
            #region Update-database
            // StoreDbContext dbContext = new StoreDbContext();  //Invalid
            //await dbContext.Database.MigrateAsync();
                using var Scope = app.Services.CreateScope();
                /// Group Of Services LifeTime Scoped 

                var Services = Scope.ServiceProvider;
                //Services It self

            var LoggerFactory = Services.GetService<ILoggerFactory>();

            try
            {
                var dbContext = Services.GetRequiredService<StoreDbContext>();
                await dbContext.Database.MigrateAsync();
                #region Data Seeding
                await StoreContextSeed.SeedAsync(dbContext);
            #endregion

            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex,"An Error Occured During Appling The Migration");
               
            }


            #endregion

            Console.WriteLine();
            #region Configure - Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
                app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
          //  app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
