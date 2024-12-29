using System;
using System.Collections.Generic;

namespace HotelSystem.Models;

public partial class Guest
{
    public int GuId { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Service> Servs { get; set; } = new List<Service>();
}
