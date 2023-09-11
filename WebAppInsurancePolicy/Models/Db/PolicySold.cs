using System;
using System.Collections.Generic;

namespace WebAppInsurancePolicy.Models.Db;

public partial class PolicySold
{
    public int Id { get; set; }

    public int? PolicyId { get; set; }

    public int? PolicyHolderId { get; set; }

    public string? PolicyName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Policy? Policy { get; set; }

    public virtual PolicyHolder? PolicyHolder { get; set; }
}
