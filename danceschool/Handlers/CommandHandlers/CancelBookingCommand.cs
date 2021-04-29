using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using danceschool.Api;
using danceschool.Api.ApiErrors;
using Serilog;

namespace danceschool.Handlers.CommandHandlers
{
    public class CancelBookingCommand : IRequest<BaseResponse<int>>
    {

        public int Id { get; set; }

        public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public CancelBookingCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
            {
                var booking = await _context.Booking.Where(m => m.ID == request.Id).FirstOrDefaultAsync();
                if (booking == null)
                    return new BaseResponse<int>(new NotFoundError("This booking is not found."));

                _context.Booking.Remove(booking);
                int flag = await _context.SaveChangesAsync();
                Log.Information($"Successfully canceled the booking. BookingID: {request.Id}");
                return new BaseResponse<int>(flag);
            }
        }
    }

}
