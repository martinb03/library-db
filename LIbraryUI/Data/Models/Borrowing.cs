using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class Borrowing
{
    public int BorrowingId { get; set; }

    public int? BookId { get; set; }

    public int? CustomerId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Customer? Customer { get; set; }
}
