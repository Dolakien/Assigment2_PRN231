using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class Role
{
    [Required(ErrorMessage = "RoleId is required.")]
    public int RoleId { get; set; }

    [Required(ErrorMessage = "RoleName is required.")]
    public string RoleName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<BranchAccount> BranchAccounts { get; set; } = new List<BranchAccount>();
}
