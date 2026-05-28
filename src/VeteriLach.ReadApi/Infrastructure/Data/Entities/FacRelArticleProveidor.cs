using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacRelArticleProveidor
{
    public Guid IdArticle { get; set; }

    public Guid IdProveidor { get; set; }

    public string? Codi { get; set; }

    public virtual FacArticle IdArticleNavigation { get; set; } = null!;

    public virtual FacProveidor IdProveidorNavigation { get; set; } = null!;
}
