namespace LibraryOfThingsM4.Models
{
    public class Donor
    {
        public int DonorId { get; set; }

        public DateOnly DonationDate { get; set; }

        public string? Description { get; set; }
    }
}
