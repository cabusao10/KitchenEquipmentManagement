using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.Domain.Entities;

namespace KitchenEquipmentManagement.ApplicationLayer.Models
{
    public class RegisteredEquipmentModel
    {
        public int Id { get; set; }

        public int EquipmentId { get; set; }
        public virtual EquipmentEntity Equipment { get; set; }

        public int SiteId { get; set; }
    }
}
