using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.Data.Contexts;
using KitchenEquipmentManagement.Infrastructure.Implementations;
using KitchenEquipmentManagement.WPF.Extensions;
using KitchenEquipmentManagement.WPF.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static KitchenEquipmentManagement.ApplicationLayer.Extensions.ServiceExtension;
namespace KitchenEquipmentManagement.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: false)
           .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");


            // Register DbContext
            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IApplicationMarker>());

            //// Register ViewModels, Services, etc.

            services.AddSingleton<MainWindow>();
            services.AddManagementLayer();
            services.AddSingleton<ICurrentUserService,CurrentUserService>();

            services.InjectViewModels();

            services.AddSingleton<INavigateService>(sp =>
            {
                return new NavigateService(sp);
            });

            services.AddSingleton<IMainNavigateService>(sp =>
            {
                return new MainNavigateService(sp);
            });
            services.AddLogging();
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
