namespace KitchenEquipmentManagement.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EquipmentEntity
    {
        [Key]
        public int EquipmentId { get; set; }

        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; } // "Working" / "Not Working"

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }

        public virtual ICollection<RegisteredEquipmentEntity> RegisteredEquipments { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModifed { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
