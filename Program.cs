using System.Globalization;
using System.Text.RegularExpressions;
using UglyToad.PdfPig.Core;
using static System.Net.Mime.MediaTypeNames;
EnumerateFiles.readFiles(); //adds all the pdf file paths into a list so we can easily read them
ReadPDFData.readPDFData(); //reads the strings and stores them in TicketReads
ReadPDFData.AddRegionToTickets();
//generate ticket amounts based on the word title occurrence
ParseData.iterate();
Console.ReadKey();

internal class SaleInfo
{
    public static List<Ticket> ticketsSold = new List<Ticket>();
    public static readonly int TicketSaleCount = ReadPDFData.TicketReads
    .SelectMany(x => x.ToString().Split(new[] { "Title" }, StringSplitOptions.None))
    .Count() - ReadPDFData.TicketReads.Count();

    public static void PopulateTicketsSold()
    {

    }
}


internal class ParseData
{
    //class for regex parsing
    public static void iterate()
    {
        foreach (var tickets in ReadPDFData.regionedTickets)
        {
            //parse will be called from here using loop
            Parse(tickets.Key, tickets.Value);
            Console.WriteLine("\n");
        }
    }

    public static void Parse(Region region, string ticketInfo) //want to try pass in a region here, and then sort the pattern on the region
    {
        if (region == Region.France)
        {
            string titlePattern = @"Title: (.*?)(?=\s+Date:|$)";
            string datePattern = @"Date: (\d{2}/\d{2}/\d{4})";
            string timePattern = @"Time: (\d{2}:\d{2})";


            var titles = Regex.Matches(ticketInfo, titlePattern);
            var dates = Regex.Matches(ticketInfo, datePattern);
            var times = Regex.Matches(ticketInfo, timePattern);

            var FilmTitle = titles[0].Value;
            var FilmDate = dates[0].Value;
            var FilmTime = times[0].Value;

            //parse the date and time to the relevant Datetime formats to be passed into a new ticket

            Console.WriteLine($"Title: {FilmTime}");
            Console.WriteLine($"Date: {FilmDate}");
            Console.WriteLine($"Time: {FilmTime}");
            Console.WriteLine($"Number of tickets: {titles.Count}");

            //now need to make new class instances using these values
            for ( int i = 0; i < titles.Count; i++ )
            {
                //SaleInfo.ticketsSold.Add(new Ticket(FilmTitle, ))
            }
        }
        if (region == Region.USA)
        {
            string titlePattern = @"Title: (.*?)(?=\s+Date:|$)";
            string datePattern = @"Date: (\d{1,2}/\d{1,2}/\d{4})";
            string timePattern = @"Time: (\d{1,2}:\d{2})"; //pm can be added back in here

            var titles = Regex.Matches(ticketInfo, titlePattern);
            var dates = Regex.Matches(ticketInfo, datePattern);
            var times = Regex.Matches(ticketInfo, timePattern);

            var FilmTitle = titles[0].Groups[1].Value;
            var FilmDate = dates[0].Groups[1].Value;
            var FilmTime = times[0].Groups[1].Value;

            //parse the date and time to the relevant Datetime formats to be passed into a new ticket

            Console.WriteLine($"Title: {FilmTime}");
            Console.WriteLine($"Date: {FilmDate}");
            Console.WriteLine($"Time: {FilmTime}");
            Console.WriteLine($"Number of tickets: {titles.Count}");
        }
        if (region == Region.Japan)
        {
            string titlePattern = @"Title: (.*?)(?=\s+Date:|$)";
            string datePattern = @"Date: (\d{4}/\d{2}/\d{2})";
            string timePattern = @"Time: (\d{2}:\d{2})";

            // Find all matches
            var titles = Regex.Matches(ticketInfo, titlePattern);
            var dates = Regex.Matches(ticketInfo, datePattern);
            var times = Regex.Matches(ticketInfo, timePattern);

            // Print results
            var FilmTitle = titles[0].Groups[1].Value;
            var FilmDate = dates[0].Groups[1].Value;
            var FilmTime = times[0].Groups[1].Value;

            //parse the date and time to the relevant Datetime formats to be passed into a new ticket

            Console.WriteLine($"Title: {FilmTime}");
            Console.WriteLine($"Date: {FilmDate}");
            Console.WriteLine($"Time: {FilmTime}");
            Console.WriteLine($"Number of tickets: {titles.Count}");
        }



    }
}