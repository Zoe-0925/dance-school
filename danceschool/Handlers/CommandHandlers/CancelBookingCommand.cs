﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using danceschool.Api;
using danceschool.Api.ApiErrors;
<<<<<<< HEAD
using Serilog;
=======
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f

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
<<<<<<< HEAD
=======

>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
                if (booking == null)
                    return new BaseResponse<int>(new NotFoundError("This booking is not found."));

                _context.Booking.Remove(booking);
                int flag = await _context.SaveChangesAsync();
<<<<<<< HEAD
                Log.Information($"Successfully canceled the booking. BookingID: {request.Id}");
=======
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
                return new BaseResponse<int>(flag);
            }
        }
    }

}
