namespace KitchenEquipmentManagement.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SiteEntity
    {
        [Key]
        public int SiteId { get; set; }

        public string Description { get; set; }
        public bool Active { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModifed { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
