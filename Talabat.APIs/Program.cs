
using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data.BDContext;

namespace Talabat.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            var app = builder.Build();
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
