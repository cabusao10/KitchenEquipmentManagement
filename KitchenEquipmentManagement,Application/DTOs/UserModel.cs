﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.Domain.Entities;
using static KitchenEquipmentManagement.Domain.Entities.UserEntity;

namespace KitchenEquipmentManagement.ApplicationLayer.DTOs
{
    public class UserModel
    {
        public int UserId { get; set; }

        public EnumUserType UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
    }
}
