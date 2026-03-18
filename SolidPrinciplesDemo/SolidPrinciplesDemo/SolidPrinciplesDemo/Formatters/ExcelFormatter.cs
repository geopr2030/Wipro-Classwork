using SolidPrinciplesDemo.Interfaces;
using SolidPrinciplesDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDemo.Formatters
{
    public class ExcelFormatter : IReportFormatter
    {
        public string Format(Report report)
        {
            return $"EXCEL FORMAT:\n{report.Title}\n{report.Content}";
        }
    }
}
