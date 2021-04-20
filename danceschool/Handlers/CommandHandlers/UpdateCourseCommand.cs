using danceschool.Api;
using danceschool.Api.ApiErrors;
using danceschool.Context;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace danceschool.Handlers.CommandHandlers
{
    public class UpdateCourseCommand : IRequest<BaseResponse<int>>
    {

        public int Id { get; set; }
        public decimal Price { get; set; }
        public int InstructorID { get; set; }
        public string Name { get; set; }

        public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;
            public UpdateCourseCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
            {
                var course = _context.Course.Where(a => a.ID == request.Id).FirstOrDefault();
                if (course == null)
                    return new BaseResponse<int>(new NotFoundError("This course is not found."));

                course.Name = request.Name;
                course.Price = request.Price;
                course.InstructorID = request.InstructorID;
                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }

        }
    }
}
