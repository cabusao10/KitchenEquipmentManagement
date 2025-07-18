using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.Domain.Entities;

namespace KitchenEquipmentManagement.ApplicationLayer.Models
{
    public class SiteModel
    {
        public int SiteId { get; set; }

        public string Description { get; set; }
        public bool Active { get; set; }

        public int UserId { get; set; }
        public virtual UserModel User { get; set; }

        public virtual ICollection<RegisteredEquipmentEntity> RegisteredEquipments { get; set; }
    }
}
