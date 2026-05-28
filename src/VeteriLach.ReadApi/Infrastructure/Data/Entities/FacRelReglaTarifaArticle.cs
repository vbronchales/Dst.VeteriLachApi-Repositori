using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacRelReglaTarifaArticle
{
    public Guid IdReglaTarifa { get; set; }

    public Guid IdArticle { get; set; }

    public decimal? Preu { get; set; }

    public decimal? Descompte { get; set; }

    public virtual FacArticle IdArticleNavigation { get; set; } = null!;

    public virtual FacReglaTarifa IdReglaTarifaNavigation { get; set; } = null!;
}
