using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Registro.Ponto.BLL;
using Registro.Ponto.DAL;

namespace Registro.Ponto.WebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ICadastroBLL, CadastroBLL>();
            builder.Services.AddScoped<ICadastroDAL, CadastroDAL>();
            builder.Services.AddScoped<IRegistroBLL, RegistroBLL>();
            builder.Services.AddScoped<IRegistroDAL, RegistroDAL>();

            builder.Services.AddDbContext<Context>(op => op.UseSqlServer(
                builder.Configuration.GetConnectionString("ConexaoBancoSQL")));
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=RegistroPonto}/{id?}");

            app.Run();
        }

    }


}
