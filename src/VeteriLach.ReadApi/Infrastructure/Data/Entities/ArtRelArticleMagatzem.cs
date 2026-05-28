using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtRelArticleMagatzem
{
    public Guid IdArticle { get; set; }

    public Guid IdMagatzem { get; set; }

    public decimal Estoc { get; set; }

    public virtual ArtArticle IdArticleNavigation { get; set; } = null!;

    public virtual ArtMagatzem IdMagatzemNavigation { get; set; } = null!;
}
