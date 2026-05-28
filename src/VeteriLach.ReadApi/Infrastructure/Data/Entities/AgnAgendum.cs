using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnAgendum
{
    public Guid IdAgenda { get; set; }

    public Guid? IdUsuari { get; set; }

    public Guid IdColor { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public int DuradaCita { get; set; }

    public virtual ICollection<AgnAgendesVisible> AgnAgendesVisibles { get; set; } = new List<AgnAgendesVisible>();

    public virtual ICollection<AgnAvisAgendum> AgnAvisAgenda { get; set; } = new List<AgnAvisAgendum>();

    public virtual ICollection<AgnCitum> AgnCita { get; set; } = new List<AgnCitum>();

    public virtual ICollection<AgnFestiu> AgnFestius { get; set; } = new List<AgnFestiu>();

    public virtual ICollection<AgnHoraDisponible> AgnHoraDisponibles { get; set; } = new List<AgnHoraDisponible>();

    public virtual AgnColor IdColorNavigation { get; set; } = null!;
}
