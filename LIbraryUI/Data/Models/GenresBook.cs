using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class GenresBook
{
    public int GenreId { get; set; }

    public int BookId { get; set; }

    public bool? IsMainGenre { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
