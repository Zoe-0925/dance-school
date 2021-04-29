using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TinyCsvParser.Mapping;

namespace danceschool.Models
{

    public class Booking
    {
        public int ID { get; set; }

        public DateTime BookingDate { get; set; }

        public int StudentID { get; set; }

        public int InstructorID { get; set; }

        public int? MembershipID { get; set; }

        public int ClassID { get; set; }

        [ForeignKey("ClassID")]
        public DanceClass DanceClass { get; set; }

        [ForeignKey("MembershipID")]
        public Membership Membership { get; set; }

        [ForeignKey("StudentID")]
        public Student Student { get; set; }
    }

    public class BookingDTO
    {
        public int ID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime Date { get; set; }

        public int ClassID { get; set; }
        public string StudentEmail { get; set; }
        public string CourseName { get; set; }
    }

    public class BookingCountDTO
    {
        public int Count;

        public IEnumerable<BookingDTO> Data;
    }

    public class CsvBookingMapping : CsvMapping<Booking>
    {
        public CsvBookingMapping()
            : base()
        {
            MapProperty(0, x => x.ID);
            MapProperty(1, x => x.BookingDate);
            MapProperty(3, x => x.ClassID);
        }
    }
}
