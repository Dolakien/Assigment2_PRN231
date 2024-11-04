using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Make sure to include this namespace
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class SilverJewelry
    {
        public string SilverJewelryId { get; set; } = null!;

        [Required(ErrorMessage = "Silver Jewelry Name is required.")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9]*(\s[A-Z][a-zA-Z0-9]*)*$",
            ErrorMessage = "Silver Jewelry Name must start with a capital letter and can only contain letters, digits, and spaces.")]
        public string? SilverJewelryName { get; set; } = null!;

        public string? SilverJewelryDescription { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Metal Weight must be a positive value.")]
        public decimal? MetalWeight { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be at least 0.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Production Year is required.")]
        [Range(1900, int.MaxValue, ErrorMessage = "Production Year must be 1900 or later.")]
        public int? ProductionYear { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public string? CategoryId { get; set; }

        public int? AccountId { get; set; }

        [JsonIgnore]
        public virtual BranchAccount? Account { get; set; }

        [JsonIgnore]
        public virtual Category? Category { get; set; }

        // Custom validation for CreatedDate to check if it's the current date
        public class CreatedDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
            {
                if (value is DateTime createdDate)
                {
                    if (createdDate.Date == DateTime.Now.Date)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Created Date must be the current date.");
                    }
                }
                return new ValidationResult("Created Date is not a valid date.");
            }
        }
    }
}
