using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank_proj_db_mvc.Models;

public partial class Sbaccount
{
    // In your Sbaccount creation scenario, it's completely fine to use an object initializer and omit the Accountnumber property. Entity Framework will recognize that this field is auto-incremented by the database and will not attempt to insert a value for it. After the record is saved, Entity Framework will automatically update your object with the generated Accountnumber.
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Account Number")]
    public int Accountnumber { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100,ErrorMessage = "Name cannot exceed 100 characters")]
    [Display(Name = "Customer Name")]
    public string Customername { get; set; }

    
    [Required(ErrorMessage = "Address is required.")]
    [StringLength(255,ErrorMessage = "Address cannot exceed 255 characters")]
    [Display(Name = "Customer Address")]
    public string Customeraddress { get; set; }

    [Required]
    [Display(Name = "Current Balance")]
    public decimal Currentbalance { get; set; }
    // Many things are dependednt on the data type, like if you dont accept a value from frontend, it is given a default value from c#, and decimal? defaults to null and decimal to 0.
    
    public virtual ICollection<Sbtransaction> Sbtransactions { get; set; } = new List<Sbtransaction>();
    
    public override string ToString()
    {
        return $"Acc No: {Accountnumber}\nName: {Customername}\nAddres: {Customeraddress}\nBalance: {Currentbalance}\n";
    }
    
}


