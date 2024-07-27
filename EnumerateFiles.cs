
//enumerate the files within the directory to find those ending with .pdf and add them to a list to be read
using System.Net.WebSockets;

internal static class EnumerateFiles
{
    public static List<string> pdfFiles = new(); //contains the file path to the 3 sets of tickets to be read
    public static void readFiles()
    {
        try
        {
            var localDirectory = AppDomain.CurrentDomain.BaseDirectory; //gets directory where the exe is placed and subsequently where the pdf files are stored in this instance
            pdfFiles = Directory.GetFiles($"{localDirectory}", "*.pdf", searchOption: SearchOption.AllDirectories).ToList(); //gets the full file path to all the pdf files within the directory we specify above
        }
        catch (AppDomainUnloadedException ex)
        {
            Console.WriteLine("Files could not be read from the list directory", ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Invalid argument passed to GetFiles", ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("The program could not read from the specified file path, ending the program");
            Environment.Exit(-1);
        }

    }
}
