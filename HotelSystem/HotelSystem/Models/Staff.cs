using System;
using System.Collections.Generic;

namespace HotelSystem.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public double? Salary { get; set; }

    public string? PhoneNumber { get; set; }

    public int? MngrId { get; set; }

    public virtual ICollection<Staff> InverseMngr { get; set; } = new List<Staff>();

    public virtual Staff? Mngr { get; set; }

    public virtual ICollection<WorkOn> WorkOns { get; set; } = new List<WorkOn>();
}
