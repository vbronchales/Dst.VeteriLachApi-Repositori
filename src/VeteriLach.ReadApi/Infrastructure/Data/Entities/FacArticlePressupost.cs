using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacArticlePressupost
{
    public Guid IdArticlePressupost { get; set; }

    public Guid IdPressupost { get; set; }

    public Guid IdArticle { get; set; }

    public string NomArticle { get; set; } = null!;

    public decimal Quantitat { get; set; }

    public decimal ValorPreu { get; set; }

    public decimal ValorIva { get; set; }

    public decimal Descompte { get; set; }

    public int Ordre { get; set; }

    public string? Observacions { get; set; }

    public virtual FacArticle IdArticleNavigation { get; set; } = null!;

    public virtual FacPressupost IdPressupostNavigation { get; set; } = null!;
}
