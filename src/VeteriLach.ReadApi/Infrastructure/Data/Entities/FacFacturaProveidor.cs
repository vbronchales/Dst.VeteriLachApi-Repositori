using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacFacturaProveidor
{
    public Guid IdFactura { get; set; }

    public Guid IdProveidor { get; set; }

    public decimal Consum { get; set; }

    public decimal Descompte { get; set; }

    public DateTime? IniPeriode { get; set; }

    public DateTime? FinPeriode { get; set; }

    public bool AmbRecarreg { get; set; }

    public virtual FacFactura IdFacturaNavigation { get; set; } = null!;

    public virtual FacProveidor IdProveidorNavigation { get; set; } = null!;
}
