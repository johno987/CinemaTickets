using System.Globalization;
using System.Text.RegularExpressions;

internal class ParseData
{
    //class for regex parsing
    public static void IterateAndParseInfo()
    {
        foreach (var tickets in ReadPDFData.regionedTickets)
        {
            //parse will be called from here using loop
            ParseByRegion(tickets.Key, tickets.Value);
        }
    }

    public static void ParseByRegion(Region region, string ticketInfo) //want to try pass in a region here, and then sort the pattern on the region
    {
        if (region == Region.France)
        {
            string titlePattern = @"Title: (.*?)(?=\s+Date:|$)";
            string datePattern = @"Date: (\d{2}/\d{2}/\d{4})";
            string timePattern = @"Time: (\d{2}:\d{2})";


            var titles = Regex.Matches(ticketInfo, titlePattern);
            var dates = Regex.Matches(ticketInfo, datePattern);
            var times = Regex.Matches(ticketInfo, timePattern);

            var FilmTitle = titles[0].Groups[1].Value;
            var FilmDate = dates[0].Groups[1].Value;
            var FilmTime = times[0].Groups[1].Value;

            //parse the date and time to the relevant Datetime formats to be passed into a new ticket
            var ParsedDate = DateTime.ParseExact(FilmDate, "dd/MM/yyyy", CultureInfo.InvariantCulture );
            var ParsedTime = DateTime.ParseExact(FilmTime,"HH:mm", null, System.Globalization.DateTimeStyles.None);
            var CombinedData = ParsedDate.Date + ParsedTime.TimeOfDay;
            

            //Console.WriteLine($"Title: {FilmTitle}");
            //Console.WriteLine($"Date: {FilmDate}");
            //Console.WriteLine($"Time: {FilmTime}");
            //Console.WriteLine($"Number of tickets: {titles.Count}");

            //Console.WriteLine($"Date and time parsed to datetime format for france = " +
            //    $"{ParsedDate} and {ParsedTime}");

            //now need to make new class instances using these values
            for ( int i = 0; i < titles.Count; i++ )
            {
                SaleInfo.ticketsSold.Add(new Ticket(FilmTitle, CombinedData, Region.France));
            }

        }
        if (region == Region.USA)
        {
            string titlePattern = @"Title: (.*?)(?=\s+Date:|$)";
            string datePattern = @"Date: (\d{1,2}/\d{1,2}/\d{4})";
            string timePattern = @"Time: (\d{1,2}:\d{2} [AP]M)"; //pm can be added back in here

            var titles = Regex.Matches(ticketInfo, titlePattern);
            var dates = Regex.Matches(ticketInfo, datePattern);
            var times = Regex.Matches(ticketInfo, timePattern);

            var FilmTitle = titles[0].Groups[1].Value;
            var FilmDate = dates[0].Groups[1].Value;
            var FilmTime = times[0].Groups[1].Value;

            //parse the date and time to the relevant Datetime formats to be passed into a new ticket
            var ParsedDate = DateTime.ParseExact(FilmDate, "M/d/yyyy", CultureInfo.InvariantCulture);
            var ParsedTime = DateTime.ParseExact(FilmTime, "h:mm tt", null, System.Globalization.DateTimeStyles.None);
            var CombinedData = ParsedDate.Date + ParsedTime.TimeOfDay;

            //Console.WriteLine($"Title: {FilmTitle}");
            //Console.WriteLine($"Date: {FilmDate}");
            //Console.WriteLine($"Time: {FilmTime}");
            //Console.WriteLine($"Number of tickets: {titles.Count}");


            //Console.WriteLine($"Date and time parsed to datetime format for france = " +
            //    $"{ParsedDate} and {ParsedTime}");

            for (int i = 0; i < titles.Count; i++)
            {
                SaleInfo.ticketsSold.Add(new Ticket(FilmTitle,CombinedData, Region.USA));
            }

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
            var ParsedDate = DateTime.ParseExact(FilmDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            var ParsedTime = DateTime.ParseExact(FilmTime, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            var CombinedData = ParsedDate.Date + ParsedTime.TimeOfDay;

            //Console.WriteLine($"Title: {FilmTitle}");
            //Console.WriteLine($"Date: {FilmDate}");
            //Console.WriteLine($"Time: {FilmTime}");
            //Console.WriteLine($"Number of tickets: {titles.Count}");

            //Console.WriteLine($"Date and time parsed to datetime format for france = " +
            //    $"{ParsedDate} and {ParsedTime}");

            for (int i = 0; i < titles.Count; i++)
            {
                SaleInfo.ticketsSold.Add(new Ticket(FilmTitle,CombinedData, Region.Japan));
            }
        }

    }
    
}