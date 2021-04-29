using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;
<<<<<<< HEAD
using danceschool.Api;


namespace danceschool.Handlers.CommandHandlers
{
    public class CreateMembershipCommand : IRequest<BaseResponse<int>>
=======

namespace danceschool.Handlers.CommandHandlers
{
    public class CreateMembershipCommand : IRequest<int>
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
    {

        public string Name { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }

<<<<<<< HEAD
        public class CreateMembershipCommandHandler : IRequestHandler<CreateMembershipCommand, BaseResponse<int>>
=======
        public class CreateMembershipCommandHandler : IRequestHandler<CreateMembershipCommand, int>
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
        {
            private readonly ApplicationContext _context;

            public CreateMembershipCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
<<<<<<< HEAD
            public async Task<BaseResponse<int>> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
=======
            public async Task<int> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
            {
                var membership = new Membership();
                membership.Name = request.Name;
                membership.Duration = request.Duration;
                membership.Price = request.Price;

                _context.Membership.Add(membership);
                int flag = await _context.SaveChangesAsync();
                int id = membership.ID;
<<<<<<< HEAD
                return new BaseResponse<int>(id);
=======
                return id;
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
            }
        }
    }

}
