using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;
using System;
using danceschool.Api;

namespace danceschool.Handlers.CommandHandlers
{
    public class SubscribeCommand : IRequest<BaseResponse<int>>
    {
        public int MembershipId { get; set; }
        public int StudentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NextBillingDate { get; set; }
          public string MembershipName { get; set; }

        public class SubscribeCommandHandler : IRequestHandler<SubscribeCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public SubscribeCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(SubscribeCommand request, CancellationToken cancellationToken)
            {
                var subscription = new Subscription();
                subscription.MembershipID = request.MembershipId;
                subscription.StudentID = request.StudentId;
                subscription.Canceled = false;
                subscription.StartDate = request.StartDate;
                subscription.MembershipName = request.MembershipName;
                subscription.NextBillingDate = request.NextBillingDate;

                _context.Subscription.Add(subscription);

                int flag = await _context.SaveChangesAsync();
                int id = subscription.ID;
                return new BaseResponse<int>(id);
            }
        }
    }
}
