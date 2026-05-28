using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosProva
{
    public Guid IdProva { get; set; }

    public Guid IdTipusProva { get; set; }

    public Guid IdVisita { get; set; }

    public int Ordre { get; set; }

    public string? CodiMostra { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<HosDetallProva> HosDetallProvas { get; set; } = new List<HosDetallProva>();

    public virtual ICollection<HosResultatIdexx> HosResultatIdexxes { get; set; } = new List<HosResultatIdexx>();

    public virtual HosTipusProva IdTipusProvaNavigation { get; set; } = null!;

    public virtual HosVisitum IdVisitaNavigation { get; set; } = null!;
}
