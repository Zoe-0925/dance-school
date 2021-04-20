using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace danceschool.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(30)]
        public string? LastName { get; set; }

        [MaxLength(40)]
        public string Email { get; set; }
    }

    public class InstructorIDWithCountDTO
    {
        public int InstructorID { get; set; }
        public int Count { get; set; }
    }
}

