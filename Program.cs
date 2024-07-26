//read all the tickets from within the bin file
//treat the varying cultures to ensure they are all formatted correctly
//write the results of all the reads to a text file

//enumerate the files within the directory to find those ending with .pdf and add them to a list to be read
using System.ComponentModel.DataAnnotations;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

EnumerateFiles.readFiles(); //adds all the pdf file paths into a list so we can easily read them

foreach(var file in EnumerateFiles.pdfFiles)
{
    ReadPDFData.readPDFData(file);
}


Console.ReadKey();

internal static class EnumerateFiles
{
    public static List<string> pdfFiles = new(); //contains the file path to the 3 sets of tickets to be read
    public static void readFiles()
    {
        var localDirectory = AppDomain.CurrentDomain.BaseDirectory; //gets directory where the exe is placed
        pdfFiles = Directory.GetFiles($"{localDirectory}", "*.pdf", searchOption: SearchOption.AllDirectories).ToList();
    }
}

internal static class ReadPDFData
{
    //list of string builders, read each data in turn and add it to the string builder list at relevant index
    public static List<StringBuilder> TicketReads = new List<StringBuilder>(EnumerateFiles.pdfFiles.Count);
    public static void readPDFData(string filePath)
    {
        using (PdfDocument document = PdfDocument.Open(filePath))
        {
            var stringBuilder = new StringBuilder();
            foreach (Page page in document.GetPages())
            {
                foreach (Word word in page.GetWords())
                {
                    stringBuilder.Append($"{word.ToString()} ");
                }
            }
            Console.WriteLine(stringBuilder);
            TicketReads.Add(stringBuilder);
        }
        Console.ReadKey();
    }
}