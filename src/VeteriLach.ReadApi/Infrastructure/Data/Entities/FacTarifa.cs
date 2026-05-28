using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacTarifa
{
    public Guid IdArticle { get; set; }

    public Guid IdTipusTarifa { get; set; }

    public decimal Preu { get; set; }

    public virtual FacArticle IdArticleNavigation { get; set; } = null!;

    public virtual FacTipusTarifa IdTipusTarifaNavigation { get; set; } = null!;
}
