using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;
<<<<<<< HEAD
using danceschool.Api;

namespace danceschool.Handlers.CommandHandlers
{
    public class RegisterInstructorCommand : IRequest<BaseResponse<int>>
=======

namespace danceschool.Handlers.CommandHandlers
{
    public class RegisterInstructorCommand : IRequest<int>
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
<<<<<<< HEAD

        public class RegisterInstructorCommandHandler : IRequestHandler<RegisterInstructorCommand, BaseResponse<int>>
=======
      
        public class RegisterInstructorCommandHandler : IRequestHandler<RegisterInstructorCommand, int>
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
        {
            private readonly ApplicationContext _context;

            public RegisterInstructorCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
<<<<<<< HEAD
            public async Task<BaseResponse<int>> Handle(RegisterInstructorCommand request, CancellationToken cancellationToken)
=======
            public async Task<int> Handle(RegisterInstructorCommand request, CancellationToken cancellationToken)
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
            {
                var instructor = new Instructor();
                instructor.FirstName = request.FirstName;
                instructor.LastName = request.LastName;
                instructor.Email = request.Email;

                _context.Instructor.Add(instructor);
<<<<<<< HEAD

                int flag = await _context.SaveChangesAsync();
                int id = instructor.ID;
                return new BaseResponse<int>(id);
=======
    
                int flag = await _context.SaveChangesAsync();
                int id = instructor.ID;
                return id;
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
            }
        }
    }

}
