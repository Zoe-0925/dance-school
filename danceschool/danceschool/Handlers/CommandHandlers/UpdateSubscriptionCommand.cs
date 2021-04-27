using danceschool.Api;
using danceschool.Api.ApiErrors;
using danceschool.Context;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace danceschool.Handlers.CommandHandlers
{
    public class UpdateSubscriptionCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
        public Boolean Canceled { get; set; }
        public DateTime NextBillingDate { get; set; }

        public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;
            public UpdateSubscriptionCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
            {
                var Subscription = _context.Subscription.Where(a => a.ID == request.Id).FirstOrDefault();
                if (Subscription == null)
                    return new BaseResponse<int>(new NotFoundError("This subscription is not found."));

                Subscription.Canceled = request.Canceled;
                Subscription.NextBillingDate = request.NextBillingDate;
                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }
        }
    }
}

