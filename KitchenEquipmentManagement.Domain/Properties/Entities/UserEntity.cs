namespace KitchenEquipmentManagement.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public enum EnumUserType
    {
        Admin,
        SuperAdmin
    }
    public class UserEntity
    {
        

        public enum EnumUserStatus
        {
            Created,
            Active,
            Delete
        }
        [Key]
        public int UserId { get; set; }

        public EnumUserType UserType { get; set; } // "SuperAdmin" / "Admin"
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } // Store hashed password
        public EnumUserStatus Status { get; set; }
        public virtual ICollection<SiteEntity> Sites { get; set; }
        public virtual ICollection<EquipmentEntity> Equipments { get; set; }
    }
}
