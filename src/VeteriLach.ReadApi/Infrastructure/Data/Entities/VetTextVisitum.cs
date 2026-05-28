using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetTextVisitum
{
    public Guid IdVisita { get; set; }

    public int IndexText { get; set; }

    public string TextPla { get; set; } = null!;

    public byte[]? TextRft { get; set; }

    public virtual VetVisitum IdVisitaNavigation { get; set; } = null!;
}
