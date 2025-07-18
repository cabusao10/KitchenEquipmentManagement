using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace KitchenEquipmentManagement.WPF.Helper
{
    internal class NavigateService : INavigateService
    {
        private readonly IServiceProvider _provider;
        private  Frame _frame;

        public NavigateService(IServiceProvider provider)
        {
            _provider = provider;
        }
        public void SetFrame(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo<TPage>() where TPage : Page
        {
            var page = _provider.GetRequiredService<TPage>();
            _frame.Navigate(page);
        }

    }
}
