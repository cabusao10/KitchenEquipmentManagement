using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KitchenEquipmentManagement.WPF.ViewModels;

namespace KitchenEquipmentManagement.WPF.Views
{
    /// <summary>
    /// Interaction logic for AddEditEquipment.xaml
    /// </summary>
    public partial class AddEditEquipment : Page
    {
        public AddEditEquipment(AddEditEquipmentViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }
    }
}
