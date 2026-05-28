using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacReglaTarifa
{
    public Guid IdReglaTarifa { get; set; }

    public string Nom { get; set; } = null!;

    public int Tipus { get; set; }

    public DateTime? DiaInici { get; set; }

    public DateTime? DiaFinal { get; set; }

    public Guid? IdPerfilTarifa { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<FacArticleVenut> FacArticleVenuts { get; set; } = new List<FacArticleVenut>();

    public virtual ICollection<FacRelReglaTarifaArticle> FacRelReglaTarifaArticles { get; set; } = new List<FacRelReglaTarifaArticle>();

    public virtual FacPerfilTarifa? IdPerfilTarifaNavigation { get; set; }
}
