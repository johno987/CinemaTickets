using System.Reflection;
using UglyToad.PdfPig.Core;
using static System.Net.Mime.MediaTypeNames;
EnumerateFiles.readFiles(); //adds all the pdf file paths into a list so we can easily read them
ReadPDFData.readPDFData(); //reads the strings and stores them in TicketReads
ReadPDFData.AddRegionToTicketsAndStore();
//generate ticket amounts based on the word title occurrence
ParseData.IterateAndParseInfo();
printAndLog.printTicketSummary();
printAndLog.printToLogTextFile();

//only thing left to do is write to a text file in the project directory
//each time the code is run, the file is overwrote


Console.ReadKey();

internal class SaleInfo
{
    public static List<Ticket> ticketsSold = new List<Ticket>();
    //public static readonly int TicketSaleCount = ReadPDFData.TicketReads
    //.SelectMany(x => x.ToString().Split(new[] { "Title" }, StringSplitOptions.None))
    //.Count() - ReadPDFData.TicketReads.Count();
}

internal class printAndLog
{
    public static void printTicketSummary()
    {
        foreach(var tickets in SaleInfo.ticketsSold)
        {
            Console.WriteLine(tickets);
        }
    }

    public static void printToLogTextFile()
    {
        const string fileName = "Tickets.txt";
        string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(currentDirectory, fileName), false))
        {
            foreach(var line in SaleInfo.ticketsSold)
            {
                outputFile.WriteLine(line);
            }
        }
    }
}
