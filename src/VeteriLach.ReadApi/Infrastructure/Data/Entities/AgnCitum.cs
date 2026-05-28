using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnCitum
{
    public Guid IdCita { get; set; }

    public DateTime DiaCita { get; set; }

    public decimal Duracio { get; set; }

    public Guid IdAgenda { get; set; }

    public string Descripcio { get; set; } = null!;

    public Guid? IdColor { get; set; }

    public Guid IdUsuari { get; set; }

    public Guid? Referencia { get; set; }

    public Guid? IdReferencia { get; set; }

    public DateTime? DiaCreacio { get; set; }

    public virtual AgnAgendum IdAgendaNavigation { get; set; } = null!;

    public virtual AgnColor? IdColorNavigation { get; set; }
}
