using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BranchAccount
{
    public int AccountId { get; set; }

    public string AccountPassword { get; set; } = null!;

    public string HmacKey { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<SilverJewelry> SilverJewelries { get; set; } = new List<SilverJewelry>();
}
