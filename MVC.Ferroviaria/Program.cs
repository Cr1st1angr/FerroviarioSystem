using Ferroviaria.Consumer;
using MVC.Ferroviaria.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Modelos.Ferroviario.DTOS;
using Modelos.Ferroviario.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MVC.Ferroviaria
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Crud<Client>.EndPoint = "https://localhost:7160/api/Clientes";
            Crud<Ticket>.EndPoint = "https://localhost:7160/api/Tickets";
            Crud<Train>.EndPoint = "https://localhost:7160/api/Trains";
            Crud<ClientesRoles>.EndPoint = "https://localhost:7160/api/ClientesRoles";
            Crud<Roles>.EndPoint = "https://localhost:7160/api/Roles";
            Crud<Seat>.EndPoint = "https://localhost:7160/api/Seats";
            Crud<TrainCar>.EndPoint = "https://localhost:7160/api/TrainCars";
            Crud<PasswordResetToken>.EndPoint = "https://localhost:7160/api/PasswordResetTokens";
            Crud<Ruta>.EndPoint = "https://localhost:7160/api/Rutas";
            Crud<Schedule>.EndPoint = "https://localhost:7160/api/Schedules";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<AuthClienteConsumer>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7160/api/");
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
             .AddCookie("Cookies", options =>
             {
                 options.LoginPath = "/Auth/Login";
                 options.LogoutPath = "/Auth/Logout";
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                 options.SlidingExpiration = true;
                 options.Cookie.IsEssential = true; // Importante para cumplir con políticas de privacidad
                 options.Cookie.HttpOnly = true;
                 options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Mejor seguridad en HTTPS
             });

            builder.Services.AddAuthorization();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
