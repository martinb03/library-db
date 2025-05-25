using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class BookCondition
{
    public int ConditionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
