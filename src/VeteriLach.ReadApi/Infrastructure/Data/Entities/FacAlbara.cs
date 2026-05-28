using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacAlbara
{
    public Guid IdAlbara { get; set; }

    public Guid? IdComanda { get; set; }

    public Guid IdProveidor { get; set; }

    public DateTime DiaAlbara { get; set; }

    public string? CodiAlbara { get; set; }

    public decimal? TotalAlbara { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<FacArticleAlbara> FacArticleAlbaras { get; set; } = new List<FacArticleAlbara>();

    public virtual FacComandum? IdComandaNavigation { get; set; }

    public virtual FacProveidor IdProveidorNavigation { get; set; } = null!;
}
