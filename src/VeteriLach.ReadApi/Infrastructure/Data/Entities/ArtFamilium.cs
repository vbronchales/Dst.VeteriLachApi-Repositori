using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtFamilium
{
    public Guid IdFamilia { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public int TipusFamilia { get; set; }

    public int? TempsControlEstoc { get; set; }

    public virtual ICollection<ArtArticle> ArtArticles { get; set; } = new List<ArtArticle>();

    public virtual FacFamilium? FacFamilium { get; set; }
}
