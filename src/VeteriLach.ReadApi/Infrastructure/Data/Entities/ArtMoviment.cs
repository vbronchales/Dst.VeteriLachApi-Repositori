using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtMoviment
{
    public Guid IdMoviment { get; set; }

    public Guid IdMagatzem { get; set; }

    public Guid IdArticle { get; set; }

    public decimal Unitats { get; set; }

    public int TipusMoviment { get; set; }

    public DateTime DiaMoviment { get; set; }

    public Guid IdUsuari { get; set; }

    public string? Observacions { get; set; }

    public Guid IdReferencia { get; set; }

    public string? NumeroLot { get; set; }

    public DateTime? DiaCaducitat { get; set; }

    public virtual ICollection<ArtMovimentInventari> ArtMovimentInventaris { get; set; } = new List<ArtMovimentInventari>();

    public virtual ArtArticle IdArticleNavigation { get; set; } = null!;

    public virtual ArtMagatzem IdMagatzemNavigation { get; set; } = null!;

    public virtual SlcUsuari IdUsuariNavigation { get; set; } = null!;
}
