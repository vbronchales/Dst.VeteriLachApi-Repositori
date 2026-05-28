using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetTaulaPe
{
    public Guid IdTaulaPes { get; set; }

    public string Nom { get; set; } = null!;

    public Guid IdEspecie { get; set; }

    public string? Observacions { get; set; }

    public virtual VetEspecie IdEspecieNavigation { get; set; } = null!;

    public virtual ICollection<VetPesTaula> VetPesTaulas { get; set; } = new List<VetPesTaula>();
}
