using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetPesTaula
{
    public Guid IdPesTaula { get; set; }

    public Guid IdTaulaPes { get; set; }

    public int Edat { get; set; }

    public decimal Pes { get; set; }

    public virtual VetTaulaPe IdTaulaPesNavigation { get; set; } = null!;
}
