using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class Author
{
    public int AuthorsId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AuthorsBook> AuthorsBooks { get; set; } = new List<AuthorsBook>();
}
