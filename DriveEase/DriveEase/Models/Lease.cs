using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Lease
    {
        public int LeaseId { get; set; }
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Type { get; set; }
    }
}
