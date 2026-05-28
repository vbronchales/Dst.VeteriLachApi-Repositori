using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacIvaFactura
{
    public Guid IdFactura { get; set; }

    public Guid IdIva { get; set; }

    public decimal TotalNet { get; set; }

    public decimal TotalIva { get; set; }

    public decimal FactorIva { get; set; }

    public decimal RecEquivalencia { get; set; }

    public virtual FacFactura IdFacturaNavigation { get; set; } = null!;

    public virtual FacIva IdIvaNavigation { get; set; } = null!;
}
