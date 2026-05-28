using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacTermini
{
    public Guid IdTermini { get; set; }

    public Guid IdFactura { get; set; }

    public DateTime DiaTermini { get; set; }

    public DateTime DiaVenciment { get; set; }

    public DateTime? DiaPagat { get; set; }

    public decimal TotalTermini { get; set; }

    public decimal TotalNet { get; set; }

    public string? Observacions { get; set; }

    public virtual FacFactura IdFacturaNavigation { get; set; } = null!;
}
