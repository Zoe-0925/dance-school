using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using danceschool.Api;
using danceschool.Api.ApiErrors;

namespace danceschool.Handlers.CommandHandlers
{
    public class BookClassCommand : IRequest<BaseResponse<int>>
    {
        public DateTime BookingDate { get; set; }
        public int StudentId { get; set; }
        public int ClassID { get; set; }

        public class BookClassCommandHandler : IRequestHandler<BookClassCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public BookClassCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(BookClassCommand request, CancellationToken cancellationToken)
            {
                var danceClass = await _context.DanceClass.Where(m => m.ID == request.ClassID).Include(c => c.Course).Include(c => c.Bookings).FirstOrDefaultAsync();

                if (danceClass == null)
                    return new BaseResponse<int>(new NotFoundError("This dance class is not found."));

                int availableSpot = danceClass != null ? danceClass.Course.BookingLimit - danceClass.Bookings.Count : 0;
                if (availableSpot < 0)
                    return new BaseResponse<int>(new BadRequestError("This class is fully booked."));

                var booking = new Booking();
                booking.BookingDate = request.BookingDate;
                booking.StudentID = request.StudentId;
                booking.ClassID = request.ClassID;
                _context.Booking.Add(booking);
                await _context.SaveChangesAsync();

                int id = booking.ID;
                return new BaseResponse<int>(id);
            }
        }
    }

}
