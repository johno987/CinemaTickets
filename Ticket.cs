internal class Ticket //class to hold the ticket info once parsed
{
    public Ticket(string title, DateTime ticketDate, Region region)
    {
        Title = title;
        this.TicketDateAndTime = ticketDate;
        //this.time = time;
        this.region = region;
    }
    public string Title { get; set; }
    public Region region { get; set; }
    public DateTime TicketDateAndTime { get; set; } //wants to read the date and then log it as invariant culture
    public override string ToString()
    {
        return $"{Title,-20} \t |{TicketDateAndTime.Date.ToShortDateString()} |{TicketDateAndTime.TimeOfDay.ToString("g")}";
    }

}
