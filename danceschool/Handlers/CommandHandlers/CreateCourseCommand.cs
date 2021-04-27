
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;
using danceschool.Api;

namespace danceschool.Handlers.CommandHandlers
{
    public class CreateCourseCommand : IRequest<BaseResponse<int>>
    {

        public decimal Price { get; set; }
        public int InstructorID { get; set; }
        public string Name { get; set; }
        public int BookingLimit { get; set; }

        public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public CreateCourseCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
            {
                var course = new Course();
                course.Price = request.Price;
                course.InstructorID = request.InstructorID;
                course.Name = request.Name;
                course.BookingLimit = request.BookingLimit;

                _context.Course.Add(course);

                await _context.SaveChangesAsync();
                int id = course.ID;
                return new BaseResponse<int>(id);
            }
        }
    }

}
