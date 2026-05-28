using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacFacturaClient
{
    public Guid IdFactura { get; set; }

    public Guid IdClient { get; set; }

    public decimal? RetencioIrpf { get; set; }

    public string? NomClient { get; set; }

    public string? NifClient { get; set; }

    public string? AdresaClient { get; set; }

    public string? PoblacioClient { get; set; }

    public virtual FacClient IdClientNavigation { get; set; } = null!;

    public virtual FacFactura IdFacturaNavigation { get; set; } = null!;
}
