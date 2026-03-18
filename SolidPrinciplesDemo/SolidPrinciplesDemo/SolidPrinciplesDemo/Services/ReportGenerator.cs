using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidPrinciplesDemo.Models;

namespace SolidPrinciplesDemo.Services
{
    public class ReportGenerator
    {
        public Report GenerateReport()
        {
            return new Report
            {
                Title = "Sales Report",
                Content = "Total Sales=50000"
            };
        }
    }
}
