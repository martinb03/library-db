using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class ViewOverdue
{
    public int? BorrowingId { get; set; }

    public int? CustomerId { get; set; }

    public string? Customer { get; set; }

    public string? Book { get; set; }

    public string? Isbn { get; set; }

    public DateOnly? BorrowDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public bool? IsOverdue { get; set; }

    public int? DaysOverdue { get; set; }
}
