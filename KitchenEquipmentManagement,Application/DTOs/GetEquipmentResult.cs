using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.Domain.Entities;

namespace KitchenEquipmentManagement.ApplicationLayer.DTOs
{
    public class GetEquipmentResult :BaseResult
    {
        public EquipmentModel[] Equipments { get; set; }
    }
}
