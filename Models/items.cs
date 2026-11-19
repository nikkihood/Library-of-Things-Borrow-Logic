using System.ComponentModel.DataAnnotations;

namespace LibraryOfThingsM4.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "99999999.99", ErrorMessage = "Replacement value must be between 0 and 99,999,999.99.")]
        public decimal ReplacementValue { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a donor.")]
        public int DonorId { get; set; }
    }
}
