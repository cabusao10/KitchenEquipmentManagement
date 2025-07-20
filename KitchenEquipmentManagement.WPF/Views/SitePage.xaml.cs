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
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.ViewModels;

namespace KitchenEquipmentManagement.WPF.Views
{
    /// <summary>
    /// Interaction logic for SitePage.xaml
    /// </summary>
    public partial class SitePage : Page
    {

        private readonly AssignSiteEquipment _assignSiteEquipment;
        public SitePage(SiteViewModel viewmodel , INavigateService navigate, AssignSiteEquipment assignSite )
        {
            InitializeComponent();
            DataContext = viewmodel;
            this._assignSiteEquipment = assignSite;
        }

        public async Task ReloadData()
        {
            await((SiteViewModel)DataContext).GetSites();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _assignSiteEquipment.ShowDialog();
        }
    }
}
