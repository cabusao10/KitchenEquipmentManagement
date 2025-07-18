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

namespace KitchenEquipmentManagement.ApplicationLayer.Command.Users
{
    public class DeleteUserCommand : IRequest<BaseResult>
    {
        public UserModel User { get; set; }


        public class Handler : BaseService, IRequestHandler<DeleteUserCommand, BaseResult>
        {
            public Handler(IMapper mapper, IAppDbContext context) : base(mapper, context)
            {

            }
            public async Task<BaseResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {

                // find userdata
                var user = await ApplicationDbContext.Users.FirstOrDefaultAsync(x => x.UserId == request.User.UserId);
                if (user == null)
                {
                    return new BaseResult
                    {
                        IsSuccess = false,
                        Message = "User not found"
                    };
                }

                user.Status = Domain.Entities.UserEntity.EnumUserStatus.Delete;

                await ApplicationDbContext.SaveChangesAsync();

                return new BaseResult
                {
                    IsSuccess = true,
                    Message = "Success deleting user data!"
                };

            }
        }
    }
}
