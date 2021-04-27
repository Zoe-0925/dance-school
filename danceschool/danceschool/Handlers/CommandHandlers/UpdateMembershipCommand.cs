using danceschool.Context;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Api;
using danceschool.Api.ApiErrors;

namespace danceschool.Handlers.CommandHandlers
{
    public class UpdateMembershipCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }

        public class UpdateMembershipCommandHandler : IRequestHandler<UpdateMembershipCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;
            public UpdateMembershipCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(UpdateMembershipCommand request, CancellationToken cancellationToken)
            {
                var Membership = _context.Membership.Where(a => a.ID == request.Id).FirstOrDefault();
                if (Membership == null)
                    return new BaseResponse<int>(new NotFoundError("This Membership is not found."));
                else
                {
                    Membership.Name = request.Name;
                    Membership.Duration = request.Duration;
                    Membership.Price = request.Price;
                    int flag = await _context.SaveChangesAsync();
                    return new BaseResponse<int>(flag);
                }
            }
        }
    }
}
