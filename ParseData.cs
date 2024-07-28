using System.Globalization;
using System.Text.RegularExpressions;

internal class ParseData
{
    //class for regex parsing
    public static void IterateAndParseInfo() //loop through the tickets and associated regions and parse them into the proper format depending on their origin
    {
        foreach (var tickets in ReadPDFData.regionedTickets2) //here regionedTickets is a dictionary of <string, Region> which allows us to identify where the tickets are from
        {
            //parse will be called from here using loop
            ParseByRegion(tickets.region, tickets.TicketInfo);
        }
    }

    public static void ParseByRegion(Region region, string ticketInfo)
    {
        RegionConfig config = GetRegionConfig(region); //returns relevant regex match expressions depending on the location to allow us to process the ticket data

        var titles = Regex.Matches(ticketInfo, config.TitlePattern);
        var dates = Regex.Matches(ticketInfo, config.DatePattern);
        var times = Regex.Matches(ticketInfo, config.TimePattern);

        if (titles.Count == 0 || dates.Count == 0 || times.Count == 0)
        {
            throw new ArgumentException("Invalid ticket information format");
        }

        //values to parse that have been read from the string data within the tickets
        var filmTitle = titles[0].Groups[1].Value; 
        var filmDate = dates[0].Groups[1].Value;
        var filmTime = times[0].Groups[1].Value;

        //parse the data so we can make a new Ticket class instance 
        var parsedDate = DateTime.ParseExact(filmDate, config.DateFormat, CultureInfo.InvariantCulture);
        var parsedTime = DateTime.ParseExact(filmTime, config.TimeFormat, CultureInfo.InvariantCulture);
        var combinedDateTime = parsedDate.Date + parsedTime.TimeOfDay;


        //for the number of tickets within the PDF, loop through and make a new ticket instance for each ticket with the relevant data
        for (int i = 0; i < titles.Count; i++)
        {
            SaleInfo.ticketsSold.Add(new Ticket(filmTitle, combinedDateTime, region));
        }
    }

    private static RegionConfig GetRegionConfig(Region region) //returns the relevant region to allow us to parse the ticket strings
    {
        return region switch
        {
            Region.France => new RegionConfig
            {
                TitlePattern = @"Title: (.*?)(?=\s+Date:|$)",
                DatePattern = @"Date: (\d{2}/\d{2}/\d{4})",
                TimePattern = @"Time: (\d{2}:\d{2})",
                DateFormat = "dd/MM/yyyy",
                TimeFormat = "HH:mm"
            },
            Region.USA => new RegionConfig
            {
                TitlePattern = @"Title: (.*?)(?=\s+Date:|$)",
                DatePattern = @"Date: (\d{1,2}/\d{1,2}/\d{4})",
                TimePattern = @"Time: (\d{1,2}:\d{2} [AP]M)",
                DateFormat = "M/d/yyyy",
                TimeFormat = "h:mm tt"
            },
            Region.Japan => new RegionConfig
            {
                TitlePattern = @"Title: (.*?)(?=\s+Date:|$)",
                DatePattern = @"Date: (\d{4}/\d{2}/\d{2})",
                TimePattern = @"Time: (\d{2}:\d{2})",
                DateFormat = "yyyy/MM/dd",
                TimeFormat = "HH:mm"
            },
            _ => throw new ArgumentException("Unsupported region", nameof(region))
        };
    }

    private class RegionConfig //helper class to store the various strings to allow us to parse the strings
    {
        public string TitlePattern { get; set; }
        public string DatePattern { get; set; }
        public string TimePattern { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
    }

}