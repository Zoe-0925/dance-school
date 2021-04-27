using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace danceschool.Models
{
    public class DanceClass
    {

        public int ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string? CourseName { get; set; }

        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }

    public class DanceClassDTO //: IMapFrom<DanceClass>
    {
        public int ID { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Name { get; set; }

        public int Count { get; set; }
    }
}
