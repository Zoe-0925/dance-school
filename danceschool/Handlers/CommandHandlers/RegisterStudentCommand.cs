using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;
using danceschool.Api;

namespace danceschool.Handlers.CommandHandlers
{
    public class RegisterStudentCommand : IRequest<BaseResponse<int>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public RegisterStudentCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
            {
                var student = new Student();
                student.Email = request.Email;
                student.UserName = request.UserName;
                _context.Student.Add(student);
                await _context.SaveChangesAsync();
                int id = student.ID;

                return new BaseResponse<int>(id);
            }
        }
    }
}
