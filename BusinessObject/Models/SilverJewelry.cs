﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class SilverJewelry
{
    public string SilverJewelryId { get; set; } = null!;

    public string? SilverJewelryName { get; set; } = null!;

    public string? SilverJewelryDescription { get; set; }

    public decimal? MetalWeight { get; set; }

    public decimal? Price { get; set; }

    public int? ProductionYear { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CategoryId { get; set; }

    public int? AccountId { get; set; }

    [JsonIgnore]
    public virtual BranchAccount? Account { get; set; }

    [JsonIgnore]
    public virtual Category? Category { get; set; }
}
