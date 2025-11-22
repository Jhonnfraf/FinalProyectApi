using System;
using System.Collections.Generic;

namespace FinalProyectApi.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int CalendarId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Calendar Calendar { get; set; } = null!;
}
