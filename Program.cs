using System.Globalization;
using System.Text.RegularExpressions;
EnumerateFiles.readFiles(); //adds all the pdf file paths into a list so we can easily read them
ReadPDFData.readPDFData(); //reads the strings and stores them in TicketReads
SaleInfo.PopulateTicketsSold();
//generate ticket amounts based on the word title occurrence

Console.ReadKey();

internal class SaleInfo
{
    public static List<Ticket> ticketsSold = new List<Ticket>();
    public static readonly int TicketSaleCount = ReadPDFData.TicketReads
    .SelectMany(x => x.ToString().Split(new[] { "Title" }, StringSplitOptions.None))
    .Count() - ReadPDFData.TicketReads.Count();

    public static void PopulateTicketsSold()
    {
        string FrenchPattern = @"Title: (?<title>.*?) Date: (?<date>\d{2}/\d{2}/\d{4}) Time: (?<time>\d{2}:\d{2})";
        string USAPattern = @"Title: (?<title>.*?) Date: (?<date>\d{1,2}/\d{1,2}/\d{4}) Time: (?<time>\d{1,2}:\d{2})";
        string JapanPattern = @"Title: (?<title>.*?) Date: (?<date>\d{4}/\d{2}/\d{2}) Time: (?<time>\d{2}:\d{2})";

        List<Match> FrenchMatches = new List<Match>();
        List<Match> USAMatches = new List<Match>();
        List<Match> JapanMatches = new List<Match>();

        foreach (var ticket in ReadPDFData.TicketReads)
        {
            var ticketString = ticket.ToString();
            FrenchMatches.AddRange(Regex.Matches(ticketString, FrenchPattern));
            USAMatches.AddRange(Regex.Matches(ticketString, USAPattern));
            JapanMatches.AddRange(Regex.Matches(ticketString, JapanPattern));
        }

        // Process the matches as needed, for example:
        foreach (var match in FrenchMatches)
        {
            Console.WriteLine($"French Title: {match.Groups["title"].Value}, Date: {match.Groups["date"].Value}, Time: {match.Groups["time"].Value}");
            ticketsSold.Add(new Ticket(match.Groups["title"].Value, DateTime.Parse(match.Groups["date"].Value, CultureInfo.InvariantCulture)
                ,DateTime.ParseExact(match.Groups["time"].Value, "HH:mm", null, System.Globalization.DateTimeStyles.None)));
        }

        foreach (var match in USAMatches)
        {
            Console.WriteLine($"USA Title: {match.Groups["title"].Value}, Date: {match.Groups["date"].Value}, Time: {match.Groups["time"].Value}");
            ticketsSold.Add(new Ticket(match.Groups["title"].Value, DateTime.Parse(match.Groups["date"].Value, CultureInfo.InvariantCulture)
                ,DateTime.ParseExact(match.Groups["time"].Value, "HH:mm", null, System.Globalization.DateTimeStyles.None)));
        }

        foreach (var match in JapanMatches)
        {
            Console.WriteLine($"Japan Title: {match.Groups["title"].Value}, Date: {match.Groups["date"].Value}, Time: {match.Groups["time"].Value}");
            ticketsSold.Add(new Ticket(match.Groups["title"].Value, DateTime.Parse(match.Groups["date"].Value, CultureInfo.InvariantCulture)
                ,DateTime.ParseExact(match.Groups["time"].Value, "HH:mm", null, System.Globalization.DateTimeStyles.None)));
        }

        Console.ReadKey();
    }
}
