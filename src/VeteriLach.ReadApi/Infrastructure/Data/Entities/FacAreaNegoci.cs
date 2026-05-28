using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacAreaNegoci
{
    public Guid IdAreaNegoci { get; set; }

    public Guid IdEmpresa { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public virtual ICollection<FacRelAreaNegociCentreCost> FacRelAreaNegociCentreCosts { get; set; } = new List<FacRelAreaNegociCentreCost>();

    public virtual FacEmpresa IdEmpresaNavigation { get; set; } = null!;
}
