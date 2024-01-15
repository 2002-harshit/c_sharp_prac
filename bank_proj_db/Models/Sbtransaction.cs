using System;
using System.Collections.Generic;

namespace bank_proj_db.Models;

public partial class Sbtransaction
{
    public int Transactionid { get; set; }

    public DateOnly? Transactiondate { get; set; }

    public int? Accountnumber { get; set; }

    public decimal? Amount { get; set; }

    public string? Transactiontype { get; set; }

    public virtual Sbaccount? AccountnumberNavigation { get; set; }
}
