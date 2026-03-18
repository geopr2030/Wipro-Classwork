using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidPrinciplesDemo.Models;

namespace SolidPrinciplesDemo.Services
{
    public class ReportSaver
    {
        public void SaveToFile(Report report)
        {
            File.WriteAllText("report.txt", report.Title + "/n" + report.Content);
            Console.WriteLine("Report saved to file.");
        }
    }
}
