using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.Domain.Entities;

namespace KitchenEquipmentManagement.Infrastructure.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        private UserEntity CurrentUser { get; set; }



        public UserEntity GetCurrentUser()
        {
            return CurrentUser;
        }

        public void SetCurrentUser(UserEntity user)
        {
           CurrentUser = user;
        }

        public bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public EnumUserType GetUserRole()
        {
           if(this.CurrentUser!= null)
            {
                return this.CurrentUser.UserType;
            }
            return EnumUserType.Admin;
        }
    }
}
