using System;
using System.Collections.Generic;

namespace LIbraryUI.Data.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string User { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsAdmin { get; set; }
}
