using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Context;
using System.Linq;
using danceschool.Models;


namespace danceschool.Handlers.QueryHandlers
{
    public class GetStudentEmailByBookingIdQuery : IRequest<string>
    {
        public int Id { get; set; }

        public class GetStudentEmailByBookingIdQueryHandler : IRequestHandler<GetStudentEmailByBookingIdQuery, string>
        {
            private readonly ApplicationContext _context;


            public GetStudentEmailByBookingIdQueryHandler(ApplicationContext context)
            {
                _context = context;

            }

            public async Task<string> Handle(GetStudentEmailByBookingIdQuery request, CancellationToken cancellationToken)
            {
                var booking = await _context.Booking
                .Where(m => m.ID == request.Id)
                .Include(m => m.Student)
                .FirstOrDefaultAsync();

                return booking.Student.Email;
            }
        }
    }
}
