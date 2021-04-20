using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using danceschool.Api.ApiErrors;
using danceschool.Api;

namespace danceschool.Handlers.CommandHandlers
{
    public class DeleteClassCommand : IRequest<BaseResponse<int>>
    {

        public int Id { get; set; }

        public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public DeleteClassCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
            {
                var danceClass = await _context.DanceClass.Where(m => m.ID == request.Id).Include(e => e.Bookings).FirstOrDefaultAsync();

                if (danceClass == null)
                    return new BaseResponse<int>(new NotFoundError("This dance class is not found."));

                _context.DanceClass.Remove(danceClass);


                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }
        }
    }

}
