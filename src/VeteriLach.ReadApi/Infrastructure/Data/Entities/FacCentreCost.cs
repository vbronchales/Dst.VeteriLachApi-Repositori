using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacCentreCost
{
    public Guid IdCentreCost { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public virtual ICollection<FacEmpresa> FacEmpresas { get; set; } = new List<FacEmpresa>();

    public virtual ICollection<FacFamilium> FacFamilia { get; set; } = new List<FacFamilium>();

    public virtual ICollection<FacRelAreaNegociCentreCost> FacRelAreaNegociCentreCosts { get; set; } = new List<FacRelAreaNegociCentreCost>();
}
