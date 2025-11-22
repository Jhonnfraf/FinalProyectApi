using System;
using System.Collections.Generic;

namespace FinalProyectApi.Models;

public partial class Reminder
{
    public int ReminderId { get; set; }

    public int CalendarId { get; set; }

    public string Title { get; set; } = null!;

    public string? Note { get; set; }

    public string Frequency { get; set; } = null!;

    public DateOnly? SpecificDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Calendar Calendar { get; set; } = null!;

    public virtual ICollection<ReminderDay> ReminderDays { get; set; } = new List<ReminderDay>();
}
