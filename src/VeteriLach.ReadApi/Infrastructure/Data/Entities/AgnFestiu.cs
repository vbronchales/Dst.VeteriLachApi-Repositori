using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnFestiu
{
    public Guid IdFestiu { get; set; }

    public Guid IdAgenda { get; set; }

    public DateTime Dia { get; set; }

    public string Descripcio { get; set; } = null!;

    public int TipusFestiu { get; set; }

    public virtual AgnAgendum IdAgendaNavigation { get; set; } = null!;
}
