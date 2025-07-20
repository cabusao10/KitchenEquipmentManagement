using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManagement.ApplicationLayer.Models;

namespace KitchenEquipmentManagement.ApplicationLayer.DTOs
{
    public class GetAvailableEquipmentResult :BaseResult
    {
        public AssignSiteEquipmentModel[] Equipments { get; set; }
    }
}
