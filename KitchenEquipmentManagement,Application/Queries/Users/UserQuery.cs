using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KitchenEquipmentManagement.ApplicationLayer.DTOs;
using KitchenEquipmentManagement.ApplicationLayer.Interfaces;
using MediatR;

namespace KitchenEquipmentManagement.ApplicationLayer.Queries.Login
{
    public class GetUsersQuery : IRequest<UserModel[]>
    {
        public class Handler :BaseService, IRequestHandler<GetUsersQuery, UserModel[]>
        {

            public Handler(IAppDbContext context,IMapper mapper):base(mapper,context)
            {
            }

            public async Task<UserModel[]> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var users = await ApplicationDbContext.Users.ToArrayAsync();
                    return Mapper.Map<UserModel[]>(users);
                }
                catch (Exception ex)
                {
                    return null;
                    
                }

               
            }
        }
    }
}
