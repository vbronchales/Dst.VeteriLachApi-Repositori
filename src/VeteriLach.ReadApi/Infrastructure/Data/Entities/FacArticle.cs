using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacArticle
{
    public Guid IdArticle { get; set; }

    public Guid IdIva { get; set; }

    public Guid IdFamilia { get; set; }

    public decimal Preu { get; set; }

    public decimal? Cost { get; set; }

    public int FerPreComanda { get; set; }

    public byte? TipusEspecial { get; set; }

    public virtual ICollection<FacArticleAlbara> FacArticleAlbaras { get; set; } = new List<FacArticleAlbara>();

    public virtual ICollection<FacArticleComandum> FacArticleComanda { get; set; } = new List<FacArticleComandum>();

    public virtual ICollection<FacArticlePressupost> FacArticlePressuposts { get; set; } = new List<FacArticlePressupost>();

    public virtual ICollection<FacArticleVenut> FacArticleVenuts { get; set; } = new List<FacArticleVenut>();

    public virtual ICollection<FacRelArticleProveidor> FacRelArticleProveidors { get; set; } = new List<FacRelArticleProveidor>();

    public virtual ICollection<FacRelReglaTarifaArticle> FacRelReglaTarifaArticles { get; set; } = new List<FacRelReglaTarifaArticle>();

    public virtual ICollection<FacTarifa> FacTarifas { get; set; } = new List<FacTarifa>();

    public virtual ICollection<HosArticleHospital> HosArticleHospitals { get; set; } = new List<HosArticleHospital>();

    public virtual ICollection<HosDetallHospitalitzacio> HosDetallHospitalitzacios { get; set; } = new List<HosDetallHospitalitzacio>();

    public virtual ICollection<HosVacuna> HosVacunas { get; set; } = new List<HosVacuna>();

    public virtual ArtArticle IdArticleNavigation { get; set; } = null!;

    public virtual FacFamilium IdFamiliaNavigation { get; set; } = null!;

    public virtual FacIva IdIvaNavigation { get; set; } = null!;
}
