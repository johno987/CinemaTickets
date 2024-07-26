//read all the tickets from within the bin file
//treat the varying cultures to ensure they are all formatted correctly
//write the results of all the reads to a text file

//enumerate the files within the directory to find those ending with .pdf and add them to a list to be read
internal static class EnumerateFiles
{
    public static List<string> pdfFiles = new(); //contains the file path to the 3 sets of tickets to be read
    public static void readFiles()
    {
        var localDirectory = AppDomain.CurrentDomain.BaseDirectory; //gets directory where the exe is placed
        pdfFiles = Directory.GetFiles($"{localDirectory}", "*.pdf", searchOption: SearchOption.AllDirectories).ToList();
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