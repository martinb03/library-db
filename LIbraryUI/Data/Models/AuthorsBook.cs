using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class AuthorsBook
{
    public int AuthorId { get; set; }

    public int BookId { get; set; }

    public bool? IsMainAuthor { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
