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
    public class UnregisterInstructorCommand : IRequest<BaseResponse<int>>
    {

        public int Id { get; set; }

        public class UnregisterInstructorCommandHandler : IRequestHandler<UnregisterInstructorCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public UnregisterInstructorCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(UnregisterInstructorCommand request, CancellationToken cancellationToken)
            {
                var Instructor = _context.Instructor.Where(a => a.ID == request.Id).FirstOrDefault();

                if (Instructor == null)
                    return new BaseResponse<int>(new NotFoundError("This Instructor is not found."));

                _context.Instructor.Remove(Instructor);

                int flag = await _context.SaveChangesAsync();

                return new BaseResponse<int>(flag);
            }
        }
    }

}
