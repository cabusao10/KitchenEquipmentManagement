using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.Domain.Entities;

namespace KitchenEquipmentManagement.ApplicationLayer.DTOs
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public UserEntity User { get; set; } 
    }
}
