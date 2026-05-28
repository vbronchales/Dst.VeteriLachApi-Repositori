using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosVacuna
{
    public Guid IdVacuna { get; set; }

    public Guid IdPacient { get; set; }

    public Guid IdTipusVacuna { get; set; }

    public Guid? IdVisita { get; set; }

    public DateTime DiaVacuna { get; set; }

    public string? Observacions { get; set; }

    public bool NoRevacunar { get; set; }

    public Guid? IdArticle { get; set; }

    public virtual FacArticle? IdArticleNavigation { get; set; }

    public virtual HosPacient IdPacientNavigation { get; set; } = null!;

    public virtual HosTipusVacuna IdTipusVacunaNavigation { get; set; } = null!;

    public virtual HosVisitum? IdVisitaNavigation { get; set; }
}
