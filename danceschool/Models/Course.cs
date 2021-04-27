using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using danceschool.Helpers.Mappers;

namespace danceschool.Models
{
    public class Course
    {

        public int ID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [ForeignKey("InstructorID")]
        public Instructor Instructor { get; set; }

        public int InstructorID { get; set; }

        public int BookingLimit { get; set; }

        public ICollection<DanceClass> DanceClasses { get; set; }
    }

    public class CourseDTO //: IMapFrom<Course>
    {

        public int ID { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int BookingLimit { get; set; }
        public int ClassCount { get; set; }
        public int InstructorID { get; set; }
    }

    public class CourseWithCountDTO
    {
        public IEnumerable<CourseDTO> Data;
        public int Count;

    }
}
