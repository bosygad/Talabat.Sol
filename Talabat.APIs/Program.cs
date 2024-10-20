
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Repository;
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
           builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
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

          
            #region Configure - Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
