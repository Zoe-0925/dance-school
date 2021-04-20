using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace danceschool.Models
{
    public class Membership
    {
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Duration { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public ICollection<Subscription> Subscription { get; set; }
    }

    public class MembershipDTO
    {
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Count { get; set; }
    }

    public class MembershipNameWithCountDTO
    {
        public string MembershipName { get; set; }
        public int Count { get; set; }
    }
}
