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
using KitchenEquipmentManagement.ApplicationLayer.Command.Registration;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly INavigateService _nav;
        public RegisterViewModel(IAppDbContext context, IMediator mediator, INavigateService navigator)
        {
            _context = context;
            _mediator = mediator;
            LoginCommand = new RelayCommand(async (_) => await Login());
            RegisterCommand = new RelayCommand(async (_) => await Register());
            _nav = navigator;
        }

        private string _email;
        /// <summary>
        /// Email to register
        /// </summary>
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _lastname;

        /// <summary>
        /// Last name to register
        /// </summary>
        public string LastName
        {
            get => _lastname;
            set { _lastname = value; OnPropertyChanged(); }
        }

        private string _firstname;

        /// <summary>
        /// First name to register
        /// </summary>
        public string FirstName
        {
            get => _firstname;
            set { _firstname = value; OnPropertyChanged(); }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;

        /// <summary>
        /// Password to register
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
           var res =  await _mediator.Send(new RegisterCommand
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Username = Username,
            });

            if (res.Success)
            {
                Email = "";
                FirstName = "";
                Password = "";
                Username = "";
                LastName = "";
                MessageBox.Show("Success Creating new account");
            }
            else
            {
                MessageBox.Show(res.ErrorMessage, "Error creating new account");
            }
        }
        public async Task Login()
        {
            _nav.NavigateTo<LoginPage>();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
