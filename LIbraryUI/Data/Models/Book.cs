using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Isbn { get; set; }

    public int StatusId { get; set; }

    public int ConditionId { get; set; }

    public virtual ICollection<AuthorsBook> AuthorsBooks { get; set; } = new List<AuthorsBook>();

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual BookCondition Condition { get; set; } = null!;

    public virtual ICollection<GenresBook> GenresBooks { get; set; } = new List<GenresBook>();

    public virtual BookStatus Status { get; set; } = null!;
}
