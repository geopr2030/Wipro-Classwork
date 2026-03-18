using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RevenueReportViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalRentals { get; set; }
        public decimal AveragePayment { get; set; }
        public decimal RevenueToday { get; set; }
        public decimal RevenueThisMonth { get; set; }
        public int PaymentsCount { get; set; }
    }
}
