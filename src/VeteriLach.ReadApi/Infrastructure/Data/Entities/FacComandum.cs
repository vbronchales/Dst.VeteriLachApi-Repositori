using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacComandum
{
    public Guid IdComanda { get; set; }

    public DateTime DiaInici { get; set; }

    public DateTime? DiaComanda { get; set; }

    public Guid IdProveidor { get; set; }

    public string? Observacions { get; set; }

    public string? Operador { get; set; }

    public bool PreComanda { get; set; }

    public virtual ICollection<FacAlbara> FacAlbaras { get; set; } = new List<FacAlbara>();

    public virtual ICollection<FacArticleComandum> FacArticleComanda { get; set; } = new List<FacArticleComandum>();

    public virtual FacProveidor IdProveidorNavigation { get; set; } = null!;
}
