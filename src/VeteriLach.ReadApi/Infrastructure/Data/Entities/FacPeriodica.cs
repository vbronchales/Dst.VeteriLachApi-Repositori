using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacPeriodica
{
    public Guid IdPeriodica { get; set; }

    public Guid IdProveidor { get; set; }

    public DateTime DiaInici { get; set; }

    public int Frequencia { get; set; }

    public decimal Valor { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<FacFactura> FacFacturas { get; set; } = new List<FacFactura>();

    public virtual FacProveidor IdProveidorNavigation { get; set; } = null!;
}
