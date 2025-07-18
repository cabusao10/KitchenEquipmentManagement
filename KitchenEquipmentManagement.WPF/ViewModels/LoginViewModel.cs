using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using System.Windows.Input;
using System.Windows;
using KitchenEquipmentManagement.WPF.Helper;
using MediatR;
using KitchenEquipmentManagement.ApplicationLayer.Queries.Login;
using Wpf.Ui;
using KitchenEquipmentManagement.WPF.Views;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly INavigateService _nav;
        private readonly ICurrentUserService _currentService;
        public LoginViewModel(IAppDbContext context, IMediator mediator , INavigateService navigator , ICurrentUserService currentUserService)
        {
            _context = context;
            _mediator = mediator;
            LoginCommand = new RelayCommand(async (_) => await Login());
            RegisterCommand = new RelayCommand(async (_) => await Register());
            _nav = navigator;
            _currentService = currentUserService;

        }

        private string _username;

        /// <summary>
        /// Username
        /// </summary>
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;


        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public bool CanLogin => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        public async Task Register()
        {
            _nav.NavigateTo<RegisterPage>();
        }
        public async Task Login()
        {
            var result = await _mediator.Send(new LoginQuery
            {
                Username = Username,
                Password = Password,
            });

            if (result.Success)
            {
                _currentService.SetCurrentUser(result.User);

                Username = "";
                Password = "";

                _nav.NavigateTo<MainPage>();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
