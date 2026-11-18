namespace LibraryOfThingsM4.Models
{
    public class Waitlist
    {
        public int ItemId { get; set; }

        public int MemberId { get; set; }

        public DateOnly RequestDate { get; set; }

        public bool Nortified { get; set; }

        public bool Recieved { get; set; }
    }
}
