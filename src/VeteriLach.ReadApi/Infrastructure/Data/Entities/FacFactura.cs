using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacFactura
{
    public Guid IdFactura { get; set; }

    public string CodiFactura { get; set; } = null!;

    public DateTime DiaFactura { get; set; }

    public DateTime DiaVenciment { get; set; }

    public DateTime? DiaPagada { get; set; }

    public Guid IdCaixa { get; set; }

    public decimal TotalFactura { get; set; }

    public decimal TotalNet { get; set; }

    public Guid? IdPeriodica { get; set; }

    public Guid IdEmpresa { get; set; }

    public string? Observacions { get; set; }

    public string? NomEmpresa { get; set; }

    public string? DadesEmpresa { get; set; }

    public string? AdresaEmpresa { get; set; }

    public string? PoblacioEmpresa { get; set; }

    public string? TelefonsEmpresa { get; set; }

    public string? NifEmpresa { get; set; }

    public string? EmailEmpresa { get; set; }

    public decimal? ValorIrpf { get; set; }

    public decimal? FactorIrpf { get; set; }

    public int Estat { get; set; }

    public Guid IdUsuari { get; set; }

    public virtual ICollection<FacArticleFactura> FacArticleFacturas { get; set; } = new List<FacArticleFactura>();

    public virtual ICollection<FacFacturaClient> FacFacturaClients { get; set; } = new List<FacFacturaClient>();

    public virtual FacFacturaProveidor? FacFacturaProveidor { get; set; }

    public virtual ICollection<FacIvaFactura> FacIvaFacturas { get; set; } = new List<FacIvaFactura>();

    public virtual ICollection<FacTermini> FacTerminis { get; set; } = new List<FacTermini>();

    public virtual FacCaixa IdCaixaNavigation { get; set; } = null!;

    public virtual FacEmpresa IdEmpresaNavigation { get; set; } = null!;

    public virtual FacPeriodica? IdPeriodicaNavigation { get; set; }

    public virtual SlcUsuari IdUsuariNavigation { get; set; } = null!;
}
