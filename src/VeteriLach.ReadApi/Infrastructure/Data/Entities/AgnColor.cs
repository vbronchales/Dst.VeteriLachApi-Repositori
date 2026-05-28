using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnColor
{
    public Guid IdColor { get; set; }

    public string Nom { get; set; } = null!;

    public int Color { get; set; }

    public int? DuradaCita { get; set; }

    public virtual ICollection<AgnAgendum> AgnAgenda { get; set; } = new List<AgnAgendum>();

    public virtual ICollection<AgnCitum> AgnCita { get; set; } = new List<AgnCitum>();
}
