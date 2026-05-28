using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacFamilium
{
    public Guid IdFamilia { get; set; }

    public Guid IdIva { get; set; }

    public Guid IdEmpresa { get; set; }

    public Guid IdCentreCost { get; set; }

    public byte? TipusEspecial { get; set; }

    public virtual ICollection<FacArticle> FacArticles { get; set; } = new List<FacArticle>();

    public virtual ICollection<FacRelPerfilTarifaFamilium> FacRelPerfilTarifaFamilia { get; set; } = new List<FacRelPerfilTarifaFamilium>();

    public virtual ICollection<FacRelTarifaFamilium> FacRelTarifaFamilia { get; set; } = new List<FacRelTarifaFamilium>();

    public virtual ICollection<HosFamArticleHospital> HosFamArticleHospitals { get; set; } = new List<HosFamArticleHospital>();

    public virtual FacCentreCost IdCentreCostNavigation { get; set; } = null!;

    public virtual FacEmpresa IdEmpresaNavigation { get; set; } = null!;

    public virtual ArtFamilium IdFamiliaNavigation { get; set; } = null!;

    public virtual FacIva IdIvaNavigation { get; set; } = null!;
}
