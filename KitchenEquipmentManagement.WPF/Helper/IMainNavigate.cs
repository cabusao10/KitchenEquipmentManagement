using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KitchenEquipmentManagement.WPF.Helper
{
    public interface IMainNavigateService
    {
        void NavigateTo<TPage>() where TPage : Page;

        void SetFrame(Frame frame);
    }
}
