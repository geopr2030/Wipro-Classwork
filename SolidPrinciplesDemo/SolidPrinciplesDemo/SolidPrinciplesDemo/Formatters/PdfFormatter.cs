using SolidPrinciplesDemo.Interfaces;
using SolidPrinciplesDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDemo.Formatters
{
    public class PdfFormatter:IReportFormatter
    {
        public string Format(Report report)
        {
            return $"PDF FORMAT:\n{report.Title}\n{report.Content}";
        }
    }
}
