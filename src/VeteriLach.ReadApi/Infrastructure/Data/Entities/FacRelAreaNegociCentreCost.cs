using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacRelAreaNegociCentreCost
{
    public Guid IdAreaNegoci { get; set; }

    public Guid IdCentreCost { get; set; }

    public decimal? Percentantge { get; set; }

    public string? Observacions { get; set; }

    public virtual FacAreaNegoci IdAreaNegociNavigation { get; set; } = null!;

    public virtual FacCentreCost IdCentreCostNavigation { get; set; } = null!;
}
