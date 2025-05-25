using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class ViewCustomer
{
    public int? CustomerId { get; set; }

    public string? Name { get; set; }

    public string? ContactInfo { get; set; }

    public DateOnly? MembershipDate { get; set; }

    public long? ActiveBorrowings { get; set; }
}
