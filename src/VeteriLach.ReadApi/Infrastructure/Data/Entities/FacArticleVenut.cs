using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacArticleVenut
{
    public Guid IdArticleVenut { get; set; }

    public Guid IdArticle { get; set; }

    public Guid IdVenda { get; set; }

    public string NomArticle { get; set; } = null!;

    public decimal Quantitat { get; set; }

    public decimal ValorPreu { get; set; }

    public decimal ValorIva { get; set; }

    public decimal? Descompte { get; set; }

    public int Ordre { get; set; }

    public decimal? ValorPagat { get; set; }

    public DateTime? DiaPagat { get; set; }

    public decimal FactorIva { get; set; }

    public string NomIva { get; set; } = null!;

    public decimal? CostNet { get; set; }

    public Guid? IdReglaTarifa { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<FacArticleFactura> FacArticleFacturas { get; set; } = new List<FacArticleFactura>();

    public virtual FacArticle IdArticleNavigation { get; set; } = null!;

    public virtual FacReglaTarifa? IdReglaTarifaNavigation { get; set; }

    public virtual FacVendum IdVendaNavigation { get; set; } = null!;
}
