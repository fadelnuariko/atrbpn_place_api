using System.ComponentModel.DataAnnotations;

namespace PlacesAPI.Models
{
    public class Place
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Owner name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Owner name must be between 3 and 50 characters")]
        public string? OwnerName { get; set; }

        [Required(ErrorMessage = "Place name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Place name must be between 3 and 50 characters")]
        public string? PlaceName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Address must be between 10 and 100 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Place type is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Place type must be between 3 and 50 characters")]
        public string? PlaceType { get; set; }

        [Required(ErrorMessage = "Coordinates is required")]
        public string? Coordinates { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }
    }
}
