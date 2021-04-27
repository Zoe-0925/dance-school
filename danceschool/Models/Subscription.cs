using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace danceschool.Models
{
    public class Subscription
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NextBillingDate { get; set; }

        public bool? Canceled { get; set; }

        public int StudentID { get; set; }

        public int MembershipID { get; set; }

        [MaxLength(30)]
        public string MembershipName { get; set; }

        public Student Student { get; set; }

        [ForeignKey("MembershipID")]
        public Membership Membership { get; set; }
    }

    public class SubscriptionDTO
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NextBillingDate { get; set; }

        public bool? Canceled { get; set; }

        [MaxLength(30)]
        public string StudentName { get; set; }

        [MaxLength(30)]
        public string MembershipName { get; set; }
    }
}
