using danceschool.Api;
using danceschool.Api.ApiErrors;
using danceschool.Context;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace danceschool.Handlers.CommandHandlers
{
    public class UpdateInstructorCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;
            public UpdateInstructorCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
            {
                var Instructor = _context.Instructor.Where(a => a.ID == request.Id).FirstOrDefault();
                if (Instructor == null)
                    return new BaseResponse<int>(new NotFoundError("This Instructor is not found."));

                Instructor.FirstName = request.FirstName;
                Instructor.LastName = request.LastName;
                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }
        }
    }
}

