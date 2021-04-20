using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using danceschool.Models;
using danceschool.Context;
using danceschool.Api;

namespace danceschool.Handlers.CommandHandlers
{
    public class CreateClassCommand : IRequest<BaseResponse<int>>
    {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }

        public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, BaseResponse<int>>
        {
            private readonly ApplicationContext _context;

            public CreateClassCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<int>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
            {
                var danceClass = new DanceClass();
                danceClass.StartTime = request.StartTime;
                danceClass.EndTime = request.EndTime;
                danceClass.CourseID = request.CourseID;
                danceClass.CourseName = request.CourseName;

                _context.DanceClass.Add(danceClass);
                await _context.SaveChangesAsync();
                int id = danceClass.ID;
                return new BaseResponse<int>(id);
            }
        }
    }

}
