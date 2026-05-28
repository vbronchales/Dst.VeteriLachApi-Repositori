using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacTipusTarifa
{
    public Guid IdTipusTarifa { get; set; }

    public string Nom { get; set; } = null!;

    public int Ordre { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<FacClient> FacClients { get; set; } = new List<FacClient>();

    public virtual ICollection<FacRelTarifaFamilium> FacRelTarifaFamilia { get; set; } = new List<FacRelTarifaFamilium>();

    public virtual ICollection<FacTarifa> FacTarifas { get; set; } = new List<FacTarifa>();

    public virtual ICollection<HosConfiguracio> HosConfiguracios { get; set; } = new List<HosConfiguracio>();
}
