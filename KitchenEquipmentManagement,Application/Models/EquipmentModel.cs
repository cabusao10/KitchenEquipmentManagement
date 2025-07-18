using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.Domain.Entities;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;

namespace KitchenEquipmentManagement.ApplicationLayer.Models
{
    public class EquipmentModel
    {
        public int EquipmentId { get; set; }

        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; } // "Working" / "Not Working"

        public int UserId { get; set; }
        public virtual UserModel User { get; set; }

        public virtual ICollection<RegisteredEquipmentModel> RegisteredEquipments { get; set; }
    }
}
