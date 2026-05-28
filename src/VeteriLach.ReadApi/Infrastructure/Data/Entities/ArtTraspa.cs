using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtTraspa
{
    public Guid IdTraspas { get; set; }

    public DateTime DiaTraspas { get; set; }

    public string? Observacions { get; set; }

    public Guid IdMagatzemOrigen { get; set; }

    public Guid IdMagatzemDesti { get; set; }

    public decimal Unitats { get; set; }

    public Guid IdArticle { get; set; }

    public virtual ArtArticle IdArticleNavigation { get; set; } = null!;

    public virtual ArtMagatzem IdMagatzemDestiNavigation { get; set; } = null!;

    public virtual ArtMagatzem IdMagatzemOrigenNavigation { get; set; } = null!;
}
