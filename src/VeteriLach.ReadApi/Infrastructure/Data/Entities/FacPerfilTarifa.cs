using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacPerfilTarifa
{
    public Guid IdPerfilTarifa { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public virtual ICollection<FacReglaTarifa> FacReglaTarifas { get; set; } = new List<FacReglaTarifa>();

    public virtual ICollection<FacRelPerfilTarifaFamilium> FacRelPerfilTarifaFamilia { get; set; } = new List<FacRelPerfilTarifaFamilium>();

    public virtual ICollection<FacClient> IdClients { get; set; } = new List<FacClient>();
}
