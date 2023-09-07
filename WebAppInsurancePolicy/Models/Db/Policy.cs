using System;
using System.Collections.Generic;

namespace WebAppInsurancePolicy.Models.Db;

public partial class Policy
{
    public int PolicyId { get; set; }

    public string? PolicyName { get; set; }

    public decimal? CoverageAmount { get; set; }

    public decimal? PremiumAmount { get; set; }


    public int? Num { get; set; }

    public virtual Category? NumNavigation { get; set; }

    public virtual ICollection<PolicySold> PolicySolds { get; set; } = new List<PolicySold>();
    //public object StartDate { get; internal set; }
    //public int PolicyHolderId { get; internal set; }
    //public object PolicyHolder { get; internal set; }
   // public object PolicyHolders { get; internal set; }
    //public object EndDate { get; internal set; }
    //public object Name { get; internal set; }
}
