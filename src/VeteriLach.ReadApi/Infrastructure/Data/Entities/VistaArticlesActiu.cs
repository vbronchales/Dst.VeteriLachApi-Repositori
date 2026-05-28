using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VistaArticlesActiu
{
    public string? NomEmpresa { get; set; }

    public string NomFamilia { get; set; } = null!;

    public string NomArticle { get; set; } = null!;

    public string CodiPropi { get; set; } = null!;

    public string? CodiBarres { get; set; }

    public decimal PreuNet { get; set; }

    public decimal Iva { get; set; }

    public decimal? Pvp { get; set; }
}
