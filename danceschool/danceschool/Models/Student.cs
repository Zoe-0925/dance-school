using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace danceschool.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        public ICollection<Subscription> Subscription { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }

    public class StudentDTO
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
        public string Membership { get; set; }
    }
}
