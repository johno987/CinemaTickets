using System.Globalization;
using System.Text.RegularExpressions;
using UglyToad.PdfPig.Core;
using static System.Net.Mime.MediaTypeNames;
EnumerateFiles.readFiles(); //adds all the pdf file paths into a list so we can easily read them later
ReadPDFData.readPDFData(); //reads the strings and stores them in TicketReads, once reading is complete, it calls AddRegion to ticket, which allows us to identify where the ticket/s originate from
ParseData.IterateAndParseInfo(); //iterates the regioned ticket dictionary and calls parse (which in turn parses the data and makes new class instances, stored in SaleInfo.TicketsSold
printAndLog.printTicketSummary(); //temp function to confirm the output before writing to the text file
printAndLog.printToLogTextFile(); //store the data in the text file within the project directory

Console.ReadKey();


