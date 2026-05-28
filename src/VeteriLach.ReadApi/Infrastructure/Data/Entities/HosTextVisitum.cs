using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosTextVisitum
{
    public Guid IdVisita { get; set; }

    public int IndexText { get; set; }

    public string TextPla { get; set; } = null!;

    public byte[]? TextRtf { get; set; }

    public byte[]? XmlText { get; set; }

    public virtual HosVisitum IdVisitaNavigation { get; set; } = null!;
}
