using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacProveidor
{
    public Guid IdProveidor { get; set; }

    public int DiesComanda { get; set; }

    public int Tipus { get; set; }

    public int DiaVenciment { get; set; }

    public int DiesVenciment { get; set; }

    public Guid IdCaixa { get; set; }

    public bool AmbRecEquiv { get; set; }

    public decimal Descompte { get; set; }

    public string? NomFiscal { get; set; }

    public virtual ICollection<FacAlbara> FacAlbaras { get; set; } = new List<FacAlbara>();

    public virtual ICollection<FacComandum> FacComanda { get; set; } = new List<FacComandum>();

    public virtual ICollection<FacFacturaProveidor> FacFacturaProveidors { get; set; } = new List<FacFacturaProveidor>();

    public virtual ICollection<FacPeriodica> FacPeriodicas { get; set; } = new List<FacPeriodica>();

    public virtual ICollection<FacRelArticleProveidor> FacRelArticleProveidors { get; set; } = new List<FacRelArticleProveidor>();

    public virtual FacCaixa IdCaixaNavigation { get; set; } = null!;

    public virtual SlcPersona IdProveidorNavigation { get; set; } = null!;
}
