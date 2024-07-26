internal class Ticket
{
    public Ticket(string title, DateTime ticketDate, DateTime time, Region region)
    {
        Title = title;
        this.ticketDate = ticketDate;
        this.time = time;
        this.region = region;
    }
    public string Title { get; set; }
    public Region region { get; set; }
    public DateTime ticketDate { get; set; } //wants to read the date and then log it as invariant culture
    public DateTime time { get; set; }//could perhaps merge this into ticket date once we have it working

}
