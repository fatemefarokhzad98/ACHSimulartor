using ACHSimulartor.Data.Context;
using ACHSimulartor.Ioc;
using Microsoft.EntityFrameworkCore;

namespace EndPoint.Api.Extentions
{
    public static class HostingExtension
    {
        #region Services
        public static WebApplication ConfigServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Db Context

            builder.Services.AddDbContext<ACHSimulartorDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
            );

            #endregion

            #region Inject Dependencies

            DependencyContainer.RegisterServices(builder.Services);

            #endregion
            return builder.Build();
        }
        #endregion
        #region Pipelines
        public static void ConfigPipLines(this WebApplication app)
        {
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
        #endregion
    }
}
