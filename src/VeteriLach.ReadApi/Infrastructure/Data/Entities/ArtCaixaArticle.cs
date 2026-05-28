using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtCaixaArticle
{
    public Guid IdCaixaArticle { get; set; }

    public Guid IdArticleArticle { get; set; }

    public Guid IdArticleCaixa { get; set; }

    public DateTime DiaMoviment { get; set; }

    public int Unitats { get; set; }

    public virtual ArtArticle IdArticleArticleNavigation { get; set; } = null!;

    public virtual ArtArticle IdArticleCaixaNavigation { get; set; } = null!;
}
