using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacVendum
{
    public Guid IdVenda { get; set; }

    public Guid IdClient { get; set; }

    public Guid IdVenedor { get; set; }

    public DateTime DiaVenda { get; set; }

    public decimal TotalVenda { get; set; }

    public decimal TotalPagat { get; set; }

    public decimal TotalCanvi { get; set; }

    public Guid? IdReferencia { get; set; }

    public string? Referencia { get; set; }

    public Guid IdCaixa { get; set; }

    public string? Resum { get; set; }

    public string? Observacions { get; set; }

    public DateTime? DiaPagat { get; set; }

    public string? ComunicarFacturaMail { get; set; }

    public string? ComunicarFacturaUsusari { get; set; }

    public DateTime? ComunicarFacturaDiaPeticio { get; set; }

    public DateTime? ComunicarFacturaDiaExecucio { get; set; }

    public virtual ICollection<FacArticleVenut> FacArticleVenuts { get; set; } = new List<FacArticleVenut>();

    public virtual FacCaixa IdCaixaNavigation { get; set; } = null!;

    public virtual FacClient IdClientNavigation { get; set; } = null!;

    public virtual VetAnimal? IdReferenciaNavigation { get; set; }

    public virtual FacVenedor IdVenedorNavigation { get; set; } = null!;
}
