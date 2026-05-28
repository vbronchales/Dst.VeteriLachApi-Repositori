using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacConfiguracio
{
    public int IdConfiguracio { get; set; }

    public Guid IdEmpresa { get; set; }

    public Guid? IdMostrador { get; set; }

    public int FormatTiquets { get; set; }

    public bool CobrarAmbTiquet { get; set; }

    public bool DemanarVenedor { get; set; }

    public bool ImprimirLpd { get; set; }

    public int DiesValidPressupost { get; set; }

    public int GenerarFactura { get; set; }

    public virtual FacEmpresa IdEmpresaNavigation { get; set; } = null!;

    public virtual FacClient? IdMostradorNavigation { get; set; }
}
