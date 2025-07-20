using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Models;
using KitchenEquipmentManagement.Domain.Entities;

namespace KitchenEquipmentManagement.ApplicationLayer.Mappings
{
    public class MapperProfile: Profile
    {
        public MapperProfile() {
            CreateMap<UserEntity, UserModel>().ReverseMap() ;
            CreateMap<SiteEntity, SiteModel>().ReverseMap() ;
            CreateMap<EquipmentEntity, EquipmentModel>().ReverseMap() ;
            CreateMap<AssignSiteEquipmentModel, EquipmentEntity>().ReverseMap();
            CreateMap<RegisteredEquipmentModel, RegisteredEquipmentEntity>().ReverseMap();
        }
    }
}
