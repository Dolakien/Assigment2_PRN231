using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class BranchAccount
{
    public int AccountId { get; set; }

    public string? AccountPassword { get; set; } = null!;

    public string? HmacKey { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public int? RoleId { get; set; }

    [JsonIgnore]
    public virtual Role? Role { get; set; }

    [JsonIgnore]
    public virtual ICollection<SilverJewelry> SilverJewelries { get; set; } = new List<SilverJewelry>();
}
