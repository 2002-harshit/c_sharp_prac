using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank_proj_db_mvc.Models;

public partial class Sbtransaction
{
    [Key]
    public int Transactionid { get; set; }

    public DateOnly? Transactiondate { get; set; }

    public int? Accountnumber { get; set; }

    public decimal? Amount { get; set; }

    public string? Transactiontype { get; set; }

    [ForeignKey(nameof(Accountnumber))]
    public virtual Sbaccount? AccountnumberNavigation { get; set; }
    
    public override string ToString()
    {
        return $"Transaction id: {Transactionid}\nTransaction Date: {Transactiondate}\nAcc No: {Accountnumber}\nAmount: {Amount}\nTransaction Type: {Transactiontype}";
    }
}
