using System;
using System.Collections.Generic;

namespace HotelSystem.Models;

public partial class WorkOn
{
    public int StaffId { get; set; }

    public int RoomId { get; set; }

    public DateOnly? AssignDate { get; set; }

    public string? Task { get; set; }

    public DateOnly? DueDate { get; set; }

    public string? Status { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
