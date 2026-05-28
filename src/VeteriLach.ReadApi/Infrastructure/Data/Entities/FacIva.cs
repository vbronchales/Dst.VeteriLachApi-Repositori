using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacIva
{
    public Guid IdIva { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public decimal Valor { get; set; }

    public decimal RecEquiv { get; set; }

    public virtual ICollection<FacArticleFactura> FacArticleFacturas { get; set; } = new List<FacArticleFactura>();

    public virtual ICollection<FacArticle> FacArticles { get; set; } = new List<FacArticle>();

    public virtual ICollection<FacEmpresa> FacEmpresas { get; set; } = new List<FacEmpresa>();

    public virtual ICollection<FacFamilium> FacFamilia { get; set; } = new List<FacFamilium>();

    public virtual ICollection<FacIvaFactura> FacIvaFacturas { get; set; } = new List<FacIvaFactura>();
}
