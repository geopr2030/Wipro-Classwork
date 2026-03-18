using SolidPrinciplesDemo.Interfaces;
using SolidPrinciplesDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDemo.Services
{
    public class ReportService
    {
        private readonly IReportFormatter _formatter;

        public ReportService(IReportFormatter formatter)
        {
            _formatter = formatter;
        }

        public void PrintReport(Report report)
        {
            string formatted = _formatter.Format(report);
            Console.WriteLine(formatted);
        }
    }
}
