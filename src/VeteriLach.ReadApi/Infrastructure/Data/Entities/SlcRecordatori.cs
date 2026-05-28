using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcRecordatori
{
    public Guid IdRecordatori { get; set; }

    public Guid IdUsuari { get; set; }

    public DateTime HoraCreacio { get; set; }

    public DateTime HoraAvis { get; set; }

    public int Interval { get; set; }

    public string Text { get; set; } = null!;

    public virtual SlcUsuari IdUsuariNavigation { get; set; } = null!;
}
