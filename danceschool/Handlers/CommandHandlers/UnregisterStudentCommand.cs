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
    public class UnregisterStudentCommand : IRequest<BaseResponse<int>>
    {
        public int StudentID { get; set; }

        public class UnregisterStudentCommandHandler : IRequestHandler<UnregisterStudentCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public UnregisterStudentCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(UnregisterStudentCommand request, CancellationToken cancellationToken)
            {
                var Student = await _context.Student.Where(m => m.ID == request.StudentID)
                .Include(m => m.Bookings)
                .Include(m => m.Subscription)
                .FirstOrDefaultAsync();
                if (Student == null)
                    return new BaseResponse<int>(new NotFoundError("This Student is not found."));

                _context.Student.Remove(Student);

                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }
        }
    }

}
