using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.ApplicationLayer.DTOs
{
    public class BaseResult
    {
        public bool IsSuccess { get; set; }
        public string Message {  get; set; }    
    }
}
