using System;
using System.Collections.Generic;

namespace bank_proj_db.Models;

public partial class Sbaccount
{
    public int Accountnumber { get; set; }

    public string? Customername { get; set; }

    public string? Customeraddress { get; set; }

    public decimal? Currentbalance { get; set; }

    public virtual ICollection<Sbtransaction> Sbtransactions { get; set; } = new List<Sbtransaction>();
}
