using KitchenEquipmentManagement.Domain.Entities;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.ApplicationLayer.Interfaces
{
    /// <summary>
    /// Interface for AppDBContext
    /// </summary>
    public interface IAppDbContext
    {
        DbSet<UserEntity> Users { get; }
        DbSet<SiteEntity> Sites { get; }
        DbSet<EquipmentEntity> Equipments { get; }
        DbSet<RegisteredEquipmentEntity> RegisteredEquipments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
