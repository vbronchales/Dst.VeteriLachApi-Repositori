using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacArticleAlbara
{
    public Guid IdArticleAlbara { get; set; }

    public Guid IdAlbara { get; set; }

    public Guid IdArticle { get; set; }

    public Guid? IdArticleComanda { get; set; }

    public decimal Unitats { get; set; }

    public int Ordre { get; set; }

    public decimal? Cost { get; set; }

    public decimal? Descompte { get; set; }

    public virtual FacAlbara IdAlbaraNavigation { get; set; } = null!;

    public virtual FacArticleComandum? IdArticleComandaNavigation { get; set; }

    public virtual FacArticle IdArticleNavigation { get; set; } = null!;
}
