using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GenresBook> GenresBooks { get; set; } = new List<GenresBook>();
}
