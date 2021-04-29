using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace danceschool.Handlers.CommandHandlers
{
    public class UnsubscribeCommand : IRequest<int>
    {

        public int Id { get; set; }

        public class UnsubscribeCommandHandler : IRequestHandler<UnsubscribeCommand, int>
        {
            private readonly ApplicationContext _context;

            public UnsubscribeCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UnsubscribeCommand request, CancellationToken cancellationToken)
            {
                var Subscription = await _context.Subscription.Where(m => m.ID == request.Id).FirstOrDefaultAsync();
                _context.Subscription.Remove(Subscription);
                int flag = await _context.SaveChangesAsync();
                return flag;
            }
        }
    }

}
