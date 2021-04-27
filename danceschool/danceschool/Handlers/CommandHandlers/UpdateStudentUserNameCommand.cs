using danceschool.Api;
using danceschool.Api.ApiErrors;
using danceschool.Context;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace danceschool.Handlers.CommandHandlers
{
    public class UpdateStudentUserNameCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public class UpdateStudentUserNameCommandHandler : IRequestHandler<UpdateStudentUserNameCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;
            public UpdateStudentUserNameCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(UpdateStudentUserNameCommand request, CancellationToken cancellationToken)
            {
                var Student = _context.Student.Where(a => a.ID == request.Id).FirstOrDefault();
                if (Student == null)
                    return new BaseResponse<int>(new NotFoundError("This student is not found."));

                Student.UserName = request.UserName;
                int flag = await _context.SaveChangesAsync();
                return new BaseResponse<int>(flag);
            }
        }
    }
}

