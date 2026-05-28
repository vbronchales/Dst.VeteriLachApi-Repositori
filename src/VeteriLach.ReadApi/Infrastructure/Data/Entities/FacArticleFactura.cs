using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacArticleFactura
{
    public Guid IdArticleFactura { get; set; }

    public Guid IdFactura { get; set; }

    public Guid? IdArticleVenut { get; set; }

    public decimal PreuNet { get; set; }

    public decimal Impost { get; set; }

    public decimal Unitats { get; set; }

    public decimal FactorIva { get; set; }

    public string NomIva { get; set; } = null!;

    public decimal? FactorRecEquivalencia { get; set; }

    public Guid? IdReferencia { get; set; }

    public Guid IdIva { get; set; }

    public string? Observacions { get; set; }

    public Guid? CodiUnic { get; set; }

    public virtual FacArticleVenut? IdArticleVenutNavigation { get; set; }

    public virtual FacFactura IdFacturaNavigation { get; set; } = null!;

    public virtual FacIva IdIvaNavigation { get; set; } = null!;
}
