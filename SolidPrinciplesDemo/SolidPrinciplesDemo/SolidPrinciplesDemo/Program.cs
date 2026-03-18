using SolidPrinciplesDemo.Services;
using SolidPrinciplesDemo.Formatters;

var generator = new ReportGenerator();
var report = generator.GenerateReport();

// choose formatter
var formatter = new PdfFormatter();
//var formatter = new ExcelFormatter();

var reportService = new ReportService(formatter);
reportService.PrintReport(report);

var saver = new ReportSaver();
saver.SaveToFile(report);

Console.WriteLine("\nFactory Pattern Demo:");

var doc = DocumentFactory.CreateDocument("pdf");
doc.Open();