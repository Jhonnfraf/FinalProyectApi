using System;
using System.Collections.Generic;

namespace FinalProyectApi.Models;

public partial class ReminderDay
{
    public int ReminderDayId { get; set; }

    public int ReminderId { get; set; }

    public int DayOfWeek { get; set; }

    public virtual Reminder Reminder { get; set; } = null!;
}
