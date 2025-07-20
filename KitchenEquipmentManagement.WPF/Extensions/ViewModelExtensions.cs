using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.WPF.ViewModels;
using KitchenEquipmentManagement.WPF.Views;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenEquipmentManagement.WPF.Extensions
{
    public static class ViewModelExtensions
    {
        public static ServiceCollection InjectViewModels(this ServiceCollection services)
        {
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<LoginPage>();

            services.AddSingleton<RegisterPage>();
            services.AddSingleton<RegisterViewModel>();

            services.AddSingleton<MainPage>();
            services.AddScoped<MainPageViewModel>();

            services.AddSingleton<UserListPage>();
            services.AddSingleton<UserListViewModel>();

            services.AddSingleton<SitePage>();
            services.AddSingleton<SiteViewModel>();
            services.AddSingleton<AddEditSitePage>();
            services.AddSingleton<AddEditViewModel>();

            services.AddSingleton<AddEditEquipment>();
            services.AddSingleton<AddEditEquipmentViewModel>();

            services.AddSingleton<EquipmentsPage>();
            services.AddSingleton<EquipmentViewModel>();

            services.AddSingleton<SiteEquipmentPage>();
            services.AddSingleton<SiteEquipmentViewModel>();
            services.AddSingleton<AssignSiteEquipment>();
            services.AddSingleton<AssignSiteEquipmentViewModel>();


            return services;

        }
    }
}
