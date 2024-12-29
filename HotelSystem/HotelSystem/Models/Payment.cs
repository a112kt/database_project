using System;
using System.Collections.Generic;

namespace HotelSystem.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string Method { get; set; } = null!;

    public int? ResId { get; set; }

    public virtual Reservation? Res { get; set; }
}
