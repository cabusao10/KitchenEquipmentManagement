using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KitchenEquipmentManagement.ApplicationLayer.Command.Registration;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.WPF.Helper;
using KitchenEquipmentManagement.WPF.Views;
using MediatR;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        private readonly IMainNavigateService _nav;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;
        private readonly UserListViewModel userListViewModel;
        private readonly EquipmentViewModel _equipmentview;
        private readonly SiteViewModel _siteView;
        private readonly INavigateService _windowNavigate;

        public bool _canviewusers;
        public bool CanViewUsers
        {
            get { return _canviewusers; }
            set { _canviewusers = value; OnPropertyChanged(); }
        }


        public MainPageViewModel(IMediator mediator, IMainNavigateService navigator,
            ICurrentUserService currentUserService, UserListViewModel userListViewModel
            , SiteViewModel siteViewModel
            ,EquipmentViewModel equipmentview
            , INavigateService navigateService)
        {
            _currentUser = currentUserService;
            _mediator = mediator;
            _nav = navigator;
            _equipmentview = equipmentview;

            CanViewUsers = _currentUser.GetUserRole() == Domain.Entities.EnumUserType.SuperAdmin;

            ViewUsersCommand = new RelayCommand(async (_) => await ViewUsers());
            ViewSitesCommand = new RelayCommand(async (_) => await ViewSites());
            LogoutCommannd = new RelayCommand(async (_) => await Logout());
            ViewEquipmentCommand = new RelayCommand(async (_) => await ViewEquipments());
            
            this.userListViewModel = userListViewModel;
            this._siteView = siteViewModel;
            this._windowNavigate = navigateService;
        }
        private async Task ViewUsers()
        {
            await userListViewModel.GetUsers();
            _nav.NavigateTo<UserListPage>();

        }
        public ICommand ViewUsersCommand { get; }
        public ICommand ViewEquipmentCommand { get; }
        public ICommand ViewSitesCommand { get; }
        public ICommand LogoutCommannd { get; }

        private async Task ViewEquipments()
        {
            await _equipmentview.GetEquipments();
            _nav.NavigateTo<EquipmentsPage>();
        }

        private async Task ViewSites()
        {
            await _siteView.GetSites();

            _nav.NavigateTo<SitePage>();
        }
        private async Task Logout()
        {
            _windowNavigate.NavigateTo<LoginPage>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
