using System;
using System.Collections.Generic;

namespace FinalProyectApi.Models;

public partial class RoutineDay
{
    public int RoutineDayId { get; set; }

    public int RoutineId { get; set; }

    public int DayOfWeek { get; set; }

    public virtual Routine Routine { get; set; } = null!;
}
