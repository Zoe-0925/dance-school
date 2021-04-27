using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using danceschool.Api;
using danceschool.Api.ApiErrors;

namespace danceschool.Handlers.CommandHandlers
{
    public class DeleteMembershipCommand : IRequest<BaseResponse<int>>
    {

        public int Id { get; set; }

        public class DeleteMembershipCommandHandler : IRequestHandler<DeleteMembershipCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public DeleteMembershipCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(DeleteMembershipCommand request, CancellationToken cancellationToken)
            {
                var membership = await _context.Membership.Where(m => m.ID == request.Id).FirstOrDefaultAsync();
                if (membership == null)
                {
                    return new BaseResponse<int>(new NotFoundError("This Membership is not found."));
                }
                _context.Membership.Remove(membership);
                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }
        }
    }

}
