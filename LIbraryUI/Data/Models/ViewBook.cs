using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class ViewBook
{
    public string? Title { get; set; }

    public string? Isbn { get; set; }

    public string? Authors { get; set; }

    public string? Genres { get; set; }

    public string? Status { get; set; }

    public string? Condition { get; set; }
}
