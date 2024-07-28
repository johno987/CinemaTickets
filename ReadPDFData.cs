using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

internal static class ReadPDFData
{
    public static List<StringBuilder> TicketReads = new List<StringBuilder>(EnumerateFiles.pdfFiles.Count);   //list of string builders, read each data in turn and add it to the string builder list, this then contains the strings from
    //each ticket
    public static Dictionary<Region, string> regionedTickets = new Dictionary<Region, string>(); //regioned tickets, string data combined with the region so we can properly parse the date and time expressions later
    public static List<RegionAndString> regionedTickets2 = new List<RegionAndString>();
    public static void readPDFData() //iterates the list of file paths one by one, adds all the strings from each file into the ticketReads list, where [0] = ticket 1, [1] = ticket 2 etc..
    {
        foreach (var file in EnumerateFiles.pdfFiles)
        {
            using PdfDocument document = PdfDocument.Open(file);
            var stringBuilder = new StringBuilder();
            foreach (Page page in document.GetPages())
            {
                foreach (Word word in page.GetWords())
                {
                    stringBuilder.Append($"{word.ToString()} "); //add each individual word from the pdf ticket into the string
                }
            }
            TicketReads.Add(stringBuilder); //once we reach the end of the strings within the file, we add the full stirng into the ticket list
        }

        AddRegionToTicketsAndStore();
    }
    private static void AddRegionToTicketsAndStore() //here we add the relevant region to the ticket, do this by checking what email address each ticket contains
    {
        foreach(var words in TicketReads) //here word is an individual PDF of tickets, therefore we can check if it contains specific email addreses to identify the regions
        {
            if(words.ToString().Contains("www.ourCinema.com"))
            {
                regionedTickets[Region.USA] = words.ToString();
                regionedTickets2.Add(new RegionAndString(words.ToString(), Region.USA));
            }
            else if (words.ToString().Contains("www.ourCinema.jp"))
            {
                regionedTickets[Region.Japan] = words.ToString();
                regionedTickets2.Add(new RegionAndString(words.ToString(), Region.Japan));
            }
            else if (words.ToString().Contains("www.ourCinema.fr"))
            {
                regionedTickets[Region.France] = words.ToString();
                regionedTickets2.Add(new RegionAndString(words.ToString(), Region.France));
            }
        }
    }
}

internal class RegionAndString
{
    public string TicketInfo { get; set; }
    public Region region { get; set; }
    public RegionAndString(string ticketInfo, Region region)
    {
        TicketInfo = ticketInfo;
        this.region = region;
    }
}
