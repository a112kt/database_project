using System;
using System.Collections.Generic;

namespace HotelSystem.Models;

public partial class Reservation
{
    public int ResId { get; set; }

    public DateOnly? CheckInDate { get; set; }

    public DateOnly? CheckoutDate { get; set; }

    public DateOnly? ReservationDate { get; set; }

    public int? GuId { get; set; }

    public virtual Guest? Gu { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
