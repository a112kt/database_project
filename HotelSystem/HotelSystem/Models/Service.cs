using System;
using System.Collections.Generic;

namespace HotelSystem.Models;

public partial class Service
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public virtual ICollection<Guest> Gus { get; set; } = new List<Guest>();
}
