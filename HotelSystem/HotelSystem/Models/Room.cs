using System;
using System.Collections.Generic;

namespace HotelSystem.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string Type { get; set; } = null!;

    public string? Status { get; set; }

    public double? Price { get; set; }

    public virtual ICollection<WorkOn> WorkOns { get; set; } = new List<WorkOn>();

    public virtual ICollection<Reservation> Res { get; set; } = new List<Reservation>();
}
