using BiblioCore.Data.Repository;
using BiblioCore.Data.Repository.Sql;

namespace BiblioCore
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


			// Enregistrement du repository
			builder.Services.AddScoped<ILivreRepository, SqlLivreRepository>();
			//builder.Services.AddScoped<IAuteurRepository, SqlAuteurRepository>();
			//builder.Services.AddScoped<IRayonRepository, SqlRayonRepository>();
			//builder.Services.AddScoped<IMembreRepository, SqlMembreRepository>();


			var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
