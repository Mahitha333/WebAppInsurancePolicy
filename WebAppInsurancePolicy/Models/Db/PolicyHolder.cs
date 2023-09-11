using System;
using System.Collections.Generic;

namespace WebAppInsurancePolicy.Models.Db;

public partial class PolicyHolder
{
    public int PolicyHolderId { get; set; }

    public string? Name { get; set; }

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual User PolicyHolderNavigation { get; set; } = null!;

    public virtual ICollection<PolicySold> PolicySolds { get; set; } = new List<PolicySold>();
}
