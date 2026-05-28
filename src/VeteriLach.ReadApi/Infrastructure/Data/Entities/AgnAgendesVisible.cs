using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnAgendesVisible
{
    public Guid IdUsuari { get; set; }

    public Guid IdAgenda { get; set; }

    public int Ordre { get; set; }

    public int? DiesVisualitzacio { get; set; }

    public virtual AgnAgendum IdAgendaNavigation { get; set; } = null!;

    public virtual SlcUsuari IdUsuariNavigation { get; set; } = null!;
}
