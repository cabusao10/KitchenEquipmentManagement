using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManagement.ApplicationLayer.Command.Registration;
using KitchenEquipmentManagement.ApplicationLayer.Command.Users;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.ApplicationLayer.Queries.Login;
using KitchenEquipmentManagement.WPF.Helper;
using MediatR;
using static KitchenEquipmentManagement.Domain.Entities.UserEntity;

namespace KitchenEquipmentManagement.WPF.ViewModels
{
    public class UserListViewModel : INotifyPropertyChanged
    {

        private readonly IMainNavigateService _nav;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;

        public ObservableCollection<string> UserTypes { get; set; }

        public ObservableCollection<UserModel> Users { get; set; }
        public bool _canviewusers;
        public bool CanViewUsers
        {
            get { return _canviewusers; }
            set { _canviewusers = value; OnPropertyChanged(); }
        }


        public async Task GetUsers()
        {
            var res = await _mediator.Send(new GetUsersQuery());

            if (res != null)
            {
                Users = new ObservableCollection<UserModel>(res);
            }
        }
        public ICommand DeleteUserCommand { get; }
       public ICommand SaveUserCommand { get; }

        public UserListViewModel(IMediator mediator, IMainNavigateService navigator, ICurrentUserService currentUserService)
        {
            _currentUser = currentUserService;
            _mediator = mediator;
            _nav = navigator;
            this.UserTypes = new ObservableCollection<string>(new string[] { "Admin", "SuperAdmin" });

            SaveUserCommand = new RelayCommand(async (userid) => await SaveUser(userid));
            DeleteUserCommand = new RelayCommand(async (userid) => await DeleteUser(userid));

            CanViewUsers = _currentUser.GetUserRole() == Domain.Entities.EnumUserType.SuperAdmin;
        }

        private async Task DeleteUser(object userid)
        {

            var currentdata = Users.Where(x => x.UserId == (int)userid).FirstOrDefault();
            if (currentdata != null)
            {
                var res = await _mediator.Send(new DeleteUserCommand { User = currentdata });
                if (res.IsSuccess)
                {
                    MessageBox.Show("Success deleting user data!");
                }
                else
                {
                    MessageBox.Show(res.Message, "Failed to delete data", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task SaveUser(object userid)
        {
            var currentdata = Users.Where(x => x.UserId == (int)userid).FirstOrDefault();
            if (currentdata != null)
            {
                var res = await _mediator.Send(new AddNewSitesCommand { User = currentdata });
                if (res.IsSuccess)
                {
                    MessageBox.Show("Success saving user data!");
                }
                else
                {
                    MessageBox.Show(res.Message, "Failed to save data", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
