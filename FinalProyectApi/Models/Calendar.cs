using System;
using System.Collections.Generic;

namespace FinalProyectApi.Models;

public partial class Calendar
{
    public int CalendarId { get; set; }

    public int UserId { get; set; }

    public string CalendarName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();

    public virtual User User { get; set; } = null!;
}
