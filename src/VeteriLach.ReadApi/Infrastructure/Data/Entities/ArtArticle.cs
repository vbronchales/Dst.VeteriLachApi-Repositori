using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtArticle
{
    public Guid IdArticle { get; set; }

    public Guid IdFamilia { get; set; }

    public Guid? IdMarca { get; set; }

    public string Nom { get; set; } = null!;

    public string CodiPropi { get; set; } = null!;

    public string? CodiBarres { get; set; }

    public string? CodiProveidor { get; set; }

    public decimal Estoc { get; set; }

    public decimal MinimEstoc { get; set; }

    public Guid? IdFoto { get; set; }

    public bool Generic { get; set; }

    public string? Observacions { get; set; }

    public int UnitatsComanda { get; set; }

    public bool Actiu { get; set; }

    public string? NomVenda { get; set; }

    public virtual ICollection<ArtCaixaArticle> ArtCaixaArticleIdArticleArticleNavigations { get; set; } = new List<ArtCaixaArticle>();

    public virtual ICollection<ArtCaixaArticle> ArtCaixaArticleIdArticleCaixaNavigations { get; set; } = new List<ArtCaixaArticle>();

    public virtual ICollection<ArtMoviment> ArtMoviments { get; set; } = new List<ArtMoviment>();

    public virtual ICollection<ArtRelArticleMagatzem> ArtRelArticleMagatzems { get; set; } = new List<ArtRelArticleMagatzem>();

    public virtual ICollection<ArtTraspa> ArtTraspas { get; set; } = new List<ArtTraspa>();

    public virtual FacArticle? FacArticle { get; set; }

    public virtual ArtFamilium IdFamiliaNavigation { get; set; } = null!;

    public virtual ArtMarca? IdMarcaNavigation { get; set; }
}
