using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;

namespace KitchenEquipmentManagement.ApplicationLayer
{
    public class BaseService
    {
        protected internal IMapper Mapper { get; set; }
        protected internal IAppDbContext ApplicationDbContext { get; set; }
        /// <summary>
        /// Initialize a new of the <see cref="BaseService"/> class.
        /// </summary>
        public BaseService(IMapper mapper, IAppDbContext appDbContext)
        {
            Mapper = mapper;
            ApplicationDbContext = appDbContext;
        }
    }
}
