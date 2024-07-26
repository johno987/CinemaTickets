//read all the tickets from within the bin file
//treat the varying cultures to ensure they are all formatted correctly
//write the results of all the reads to a text file

//enumerate the files within the directory to find those ending with .pdf and add them to a list to be read
using System.Net.WebSockets;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

internal static class ReadPDFData
{
    //list of string builders, read each data in turn and add it to the string builder list at relevant index
    public static List<StringBuilder> TicketReads = new List<StringBuilder>(EnumerateFiles.pdfFiles.Count);
    public static Dictionary<Region, string> regionedTickets = new Dictionary<Region, string>();
    public static void readPDFData()
    {
        foreach (var file in EnumerateFiles.pdfFiles)
        {
            using (PdfDocument document = PdfDocument.Open(file))
            {
                var stringBuilder = new StringBuilder();
                foreach (Page page in document.GetPages())
                {
                    foreach (Word word in page.GetWords())
                    {
                        stringBuilder.Append($"{word.ToString()} ");
                    }
                }
                //Console.WriteLine($"{stringBuilder}\n\n");
                TicketReads.Add(stringBuilder);
            }
        }

    }
    public static void AddRegionToTicketsAndStore()
    {
        foreach(var words in TicketReads)
        {
            if(words.ToString().Contains("www.ourCinema.com"))
            {
                regionedTickets[Region.USA] = words.ToString();
            }
            else if (words.ToString().Contains("www.ourCinema.jp"))
            {
                regionedTickets[Region.Japan] = words.ToString();
            }
            else if (words.ToString().Contains("www.ourCinema.fr"))
            {
                regionedTickets[Region.France] = words.ToString();
            }
        }
    }
}





//make relevant class instance with tickets
//foreach (var region in ReadPDFData.TicketReads)
//{
//    if (region.ToString().Contains("www.ourCinema.com"))
//    {
//        var USAticket = new Ticket();
//    }
//    else if (region.ToString().Contains("www.ourCinema.jp"))
//    {
//        var JapanTicket = new Ticket();
//    }
//    else if (region.ToString().Contains("www.ourCinema.fr"))
//    {
//        var FrandeTicket = new Ticket();
//    }

//}