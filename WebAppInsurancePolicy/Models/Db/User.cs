using System;
using System.Collections.Generic;

namespace WebAppInsurancePolicy.Models.Db;

public partial class User
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public int UserId { get; set; }

    public string? Role { get; set; }

    public virtual PolicyHolder? PolicyHolder { get; set; }
}
