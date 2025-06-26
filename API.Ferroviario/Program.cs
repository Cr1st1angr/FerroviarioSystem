using API.Ferroviario.Services.Implements;
using API.Ferroviario.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Ferroviario
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<APIFerroviarioContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("APIFerroviarioContext") ?? throw new InvalidOperationException("Connection string 'APIFerroviarioContext' not found.")));

            // Add services to the container.
            
            builder.Services
                .AddControllers()
                .AddNewtonsoftJson(
            options => options.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                   );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<AuthClienteService>();
            builder.Services.AddScoped<CorreoService>();

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
