using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int LeaseId { get; set; }
        public Lease? Lease { get; set; }

        [Column(TypeName="decimal(10,2)")]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
