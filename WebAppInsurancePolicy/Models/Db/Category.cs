using System;
using System.Collections.Generic;

namespace WebAppInsurancePolicy.Models.Db;

public partial class Category
{
    public int CatId { get; set; }

    public string? Cat { get; set; }

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
