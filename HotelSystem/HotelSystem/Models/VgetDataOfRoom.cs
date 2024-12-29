using System;
using System.Collections.Generic;

namespace HotelSystem.Models;

public partial class VgetDataOfRoom
{
    public int RoomId { get; set; }

    public string Type { get; set; } = null!;

    public string? Status { get; set; }

    public double? Price { get; set; }
}
