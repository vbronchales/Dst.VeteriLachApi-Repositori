using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtMarca
{
    public Guid IdMarca { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public Guid? IdLogo { get; set; }

    public virtual ICollection<ArtArticle> ArtArticles { get; set; } = new List<ArtArticle>();
}
