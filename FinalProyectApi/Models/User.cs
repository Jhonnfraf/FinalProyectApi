using System;
using System.Collections.Generic;

namespace FinalProyectApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = "";

    public string PasswordSalt { get; set; } = "";

    public string Email { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();
}
