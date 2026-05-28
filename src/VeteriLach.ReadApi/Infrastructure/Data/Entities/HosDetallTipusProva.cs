using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosDetallTipusProva
{
    public Guid IdDetallTipusProva { get; set; }

    public Guid IdTipusProva { get; set; }

    public string Nom { get; set; } = null!;

    public int Tipus { get; set; }

    public int Ordre { get; set; }

    public string? Unitats { get; set; }

    public int Decimals { get; set; }

    public string? Formula { get; set; }

    public virtual ICollection<HosDetallProva> HosDetallProvas { get; set; } = new List<HosDetallProva>();

    public virtual ICollection<HosDetallTipusTipusProva> HosDetallTipusTipusProvas { get; set; } = new List<HosDetallTipusTipusProva>();

    public virtual ICollection<HosValoracioTipusProva> HosValoracioTipusProvas { get; set; } = new List<HosValoracioTipusProva>();

    public virtual HosTipusProva IdTipusProvaNavigation { get; set; } = null!;
}
