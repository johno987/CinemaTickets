using System.Reflection;

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


