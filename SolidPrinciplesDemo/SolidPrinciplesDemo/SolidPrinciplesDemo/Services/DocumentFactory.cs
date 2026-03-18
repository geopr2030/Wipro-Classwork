using SolidPrinciplesDemo.Interfaces;
using SolidPrinciplesDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDemo.Services
{
    public class DocumentFactory
    {
        public static IDocument CreateDocument(string type)
        {
            if (type == "pdf")
                return new PdfDocument();

            if (type == "word")
                return new WordDocument();

            throw new Exception("Invalid type");
        }
    }
}
