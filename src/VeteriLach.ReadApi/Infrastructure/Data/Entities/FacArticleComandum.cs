using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacArticleComandum
{
    public Guid IdArticleComanda { get; set; }

    public Guid IdComanda { get; set; }

    public Guid? IdArticle { get; set; }

    public string NomArticle { get; set; } = null!;

    public decimal Unitats { get; set; }

    public int Ordre { get; set; }

    public int Urgencia { get; set; }

    public DateTime? DiaPerDemanar { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<FacArticleAlbara> FacArticleAlbaras { get; set; } = new List<FacArticleAlbara>();

    public virtual ICollection<FacArticleCancelat> FacArticleCancelats { get; set; } = new List<FacArticleCancelat>();

    public virtual ICollection<FacArticleReservat> FacArticleReservats { get; set; } = new List<FacArticleReservat>();

    public virtual FacArticle? IdArticleNavigation { get; set; }

    public virtual FacComandum IdComandaNavigation { get; set; } = null!;
}
