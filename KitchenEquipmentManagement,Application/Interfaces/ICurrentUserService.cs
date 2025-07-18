using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.Domain.Entities;

namespace KitchenEquipmentManagement.ApplicationLayer.Interfaces
{
    public interface ICurrentUserService
    {
        bool IsLoggedIn();
        UserEntity GetCurrentUser();
        EnumUserType GetUserRole();
        void SetCurrentUser(UserEntity user);
    }
}
