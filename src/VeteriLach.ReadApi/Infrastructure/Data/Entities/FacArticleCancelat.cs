using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacArticleCancelat
{
    public Guid IdArticleCancelat { get; set; }

    public Guid IdArticleComanda { get; set; }

    public DateTime DiaCancelacio { get; set; }

    public decimal Unitats { get; set; }

    public string Motiu { get; set; } = null!;

    public virtual FacArticleComandum IdArticleComandaNavigation { get; set; } = null!;
}
