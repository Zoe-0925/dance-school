using System;
using System.Collections.Generic;

namespace danceschool.Models
{

    public class CountByDate
    {
        public int Count { get; set; }

        public DateTime Date { get; set; }
    }

    public class CountByDateNumber
    {
        public int Count { get; set; }

        public int Date { get; set; }
    }
    public class Dashboard
    {
        public IEnumerable<InstructorIDWithCountDTO> topInstructors { get; set; }
        public IEnumerable<DanceClassDTO> topClasses { get; set; }

        public int totalCourses { get; set; }
        public int totalBookings { get; set; }
        public int totalStudents { get; set; }
        public int totalSubscriptions { get; set; }

        public IEnumerable<MembershipNameWithCountDTO> bookingByMembership { get; set; }
        public IEnumerable<CountByDate> lastWeekbookings { get; set; }

        public IEnumerable<Instructor> instructors { get; set; }

    }
}