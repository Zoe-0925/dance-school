using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using danceschool.Api;
using danceschool.Api.ApiErrors;

namespace danceschool.Handlers
{
    public class DeleteSubscriptionCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }

        public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public DeleteSubscriptionCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
            {
                var subscription = await _context.Subscription.Where(m => m.ID == request.Id).FirstOrDefaultAsync();

                if (subscription == null)
                    return new BaseResponse<int>(new NotFoundError("This subscription is not found."));

                _context.Subscription.Remove(subscription);
                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }
        }

    }
}