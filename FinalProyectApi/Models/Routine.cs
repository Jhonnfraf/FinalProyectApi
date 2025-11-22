using System;
using System.Collections.Generic;

namespace FinalProyectApi.Models;

public partial class Routine
{
    public int RoutineId { get; set; }

    public int CalendarId { get; set; }

    public string Title { get; set; } = null!;

    public string? Note { get; set; }

    public string Frequency { get; set; } = null!;

    public DateOnly? SpecificDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Calendar Calendar { get; set; } = null!;

    public virtual ICollection<RoutineDay> RoutineDays { get; set; } = new List<RoutineDay>();
}
