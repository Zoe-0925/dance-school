using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;

namespace danceschool.Handlers.CommandHandlers
{
    public class RegisterInstructorCommand : IRequest<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
      
        public class RegisterInstructorCommandHandler : IRequestHandler<RegisterInstructorCommand, int>
        {
            private readonly ApplicationContext _context;

            public RegisterInstructorCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(RegisterInstructorCommand request, CancellationToken cancellationToken)
            {
                var instructor = new Instructor();
                instructor.FirstName = request.FirstName;
                instructor.LastName = request.LastName;
                instructor.Email = request.Email;

                _context.Instructor.Add(instructor);
    
                int flag = await _context.SaveChangesAsync();
                int id = instructor.ID;
                return id;
            }
        }
    }

}
