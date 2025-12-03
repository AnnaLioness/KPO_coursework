using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;
using Repositories.Implementations;
using Repositories.Interfaces;
using Repositories;
using Services.Services;

namespace DocumetForms
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            ConfigureServices();
            var mainForm = ServiceProvider!.GetRequiredService<FormMain>();
            Application.Run(mainForm);
        }
        static void ConfigureServices()
        {
            var services = new ServiceCollection();

            // --- DbContext ---
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Database=KPODatabase;Username=postgres;Password=1234"));

            // --- Репозитории ---
            services.AddTransient<IRepository<AdminModel>, AdminRepository>();
            services.AddTransient<IRepository<DocumentModel>, DocumentRepository>();

            // --- Сервисы ---
            services.AddTransient<AdminService>();
            services.AddTransient<DocumentService>();

            // --- Формы ---
            services.AddTransient<FormMain>();
            services.AddTransient<FormLogin>();
            services.AddTransient<FormRegister>();
            services.AddTransient<FormAdmin>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}