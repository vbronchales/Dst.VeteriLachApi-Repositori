using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacPressupost
{
    public Guid IdPressupost { get; set; }

    public DateTime DiaPressupost { get; set; }

    public Guid IdClient { get; set; }

    public Guid IdVenedor { get; set; }

    public decimal Total { get; set; }

    public string? Observacions { get; set; }

    public DateTime Caducitat { get; set; }

    public virtual ICollection<FacArticlePressupost> FacArticlePressuposts { get; set; } = new List<FacArticlePressupost>();

    public virtual FacClient IdClientNavigation { get; set; } = null!;

    public virtual FacVenedor IdVenedorNavigation { get; set; } = null!;
}
