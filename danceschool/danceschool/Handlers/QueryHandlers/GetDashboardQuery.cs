using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using danceschool.Models;
using danceschool.Context;
using System.Linq;
using System;
using danceschool.Api;

namespace danceschool.Handlers.QueryHandlers
{
    public class GetDashboardQuery : IRequest<BaseResponse<Dashboard>>
    {
        public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, BaseResponse<Dashboard>>
        {
            private readonly ApplicationContext _context;

            public GetDashboardQueryHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<Dashboard>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
            {


                var totalBookings = await _context.Booking.Select(x => x.ID).CountAsync();

                var bookingByMembership = await _context.Booking
                .Include(b => b.Membership)
                .Select(b => new
                {
                    ID = b.ID,
                    MembershipName = b.Membership.Name
                })
                .GroupBy(b => b.MembershipName)
                    .Select(b => new MembershipNameWithCountDTO
                    {
                        MembershipName = b.Key,
                        Count = b.Count()
                    })
                .ToListAsync();

                var topInstructors = await _context.Booking
                 .Select(b => new
                 {
                     ID = b.ID,
                     InstructorID = b.InstructorID
                 })
                   .GroupBy(b => b.InstructorID)
                   .Select(b => new InstructorIDWithCountDTO
                   {
                       InstructorID = b.Key,
                       Count = b.Count()
                   })
                   .OrderByDescending(s => s.Count)
                   .Take(3)
                   .ToListAsync();

                var topClasses = await _context.DanceClass
                .Include(d => d.Bookings)
                .OrderByDescending(d => d.Bookings.Count)
                .Take(3)
                .Select(d => new DanceClassDTO
                {
                    ID = d.ID,
                    Name = d.CourseName,
                    StartTime = d.StartTime,
                    EndTime = d.EndTime,
                    Count = d.Bookings.Count
                }).ToListAsync();

                var totalStudents = await _context.Student.Select(x => x.ID).CountAsync();

                var totalSubscriptions = await _context.Subscription.Select(x => x.ID).CountAsync();

                var totalCourses = await _context.Course.Select(x => x.ID).CountAsync();

                var bookings = await _context.Booking
                .Include(b => b.DanceClass)
                .Where(x => x.DanceClass.StartTime >= DateTime.Today.AddYears(-1)).ToListAsync();

                var lastWeekbookings = await _context.Booking
                             .Include(b => b.DanceClass)
                             .Select(b => new
                             {
                                 ID = b.ID,
                                 Date = b.DanceClass.StartTime
                             })
                             .Where(x => x.Date >= DateTime.Today.AddDays(-7))
                             .GroupBy(x => x.Date.Date)
                                .Select(g => new CountByDate
                                {
                                    Date = g.Key,
                                    Count = g.Count()
                                })
                             .ToListAsync();

                var InstructorList = await _context.Instructor.ToListAsync();

                var result = new Dashboard
                {
                    totalCourses = totalCourses,
                    topInstructors = topInstructors,
                    topClasses = topClasses,
                    totalBookings = totalBookings,
                    totalStudents = totalStudents,
                    bookingByMembership = bookingByMembership,
                    lastWeekbookings = lastWeekbookings,
                    totalSubscriptions = totalSubscriptions,
                    instructors = InstructorList
                };
                return new BaseResponse<Dashboard>(result);
            }

        }
    }
}

