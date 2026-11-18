namespace LibraryOfThingsM4.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SerialNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public decimal ReplacementValue { get; set; }

        public int DonorId { get; set; }
    }
}
