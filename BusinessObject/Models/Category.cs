using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class Category
{
    [Required(ErrorMessage = "CategoryId is required.")]
    public string CategoryId { get; set; } = null!;

    [Required(ErrorMessage = "CategoryName is required.")]
    public string CategoryName { get; set; } = null!;

    [Required(ErrorMessage = "CategoryDescription is required.")]
    public string CategoryDescription { get; set; } = null!;

    [Required(ErrorMessage = "FromCountry is required.")]
    public string? FromCountry { get; set; }

    [JsonIgnore]
    public virtual ICollection<SilverJewelry> SilverJewelries { get; set; } = new List<SilverJewelry>();
}
