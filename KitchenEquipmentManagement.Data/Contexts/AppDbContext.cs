using System.Data.Entity;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using KitchenEquipmentManagement.Domain.Entities;

namespace KitchenEquipmentManagement.Data.Contexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext() : base("name=DefaultConnection") // or your connection string name
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SiteEntity> Sites { get; set; }
        public DbSet<EquipmentEntity> Equipments { get; set; }
        public DbSet<RegisteredEquipmentEntity> RegisteredEquipments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EquipmentEntity>().ToTable("Equipment");
            modelBuilder.Entity<SiteEntity>().ToTable("Site");
            modelBuilder.Entity<UserEntity>().ToTable("User");

            modelBuilder.Entity<SiteEntity>()
        .HasOptional(s => s.User)
        .WithMany(u => u.Sites)
        .HasForeignKey(s => s.UserId)
        .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegisteredEquipmentEntity>().ToTable("RegisteredEquipments")
                .HasRequired(re => re.Equipment)
                .WithMany()
                .HasForeignKey(re => re.EquipmentId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<RegisteredEquipmentEntity>()
                .HasRequired(re => re.Site)
                .WithMany()
                .HasForeignKey(re => re.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}
