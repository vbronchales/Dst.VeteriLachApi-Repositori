using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnAvisAgendum
{
    public Guid IdAvisAgenda { get; set; }

    public Guid IdAgenda { get; set; }

    public DateTime DiaAvis { get; set; }

    public int Estat { get; set; }

    public string TextAvis { get; set; } = null!;

    public virtual AgnAgendum IdAgendaNavigation { get; set; } = null!;
}
