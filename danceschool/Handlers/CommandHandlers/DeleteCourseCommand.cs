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
    public class DeleteCourseCommand : IRequest<BaseResponse<int>>
    {

        public int Id { get; set; }

        public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public DeleteCourseCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.Where(m => m.ID == request.Id).FirstOrDefaultAsync();
                _context.Course.Remove(course);

                if (course == null)
                    return new BaseResponse<int>(new NotFoundError("This course is not found."));

                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }
        }
    }
}