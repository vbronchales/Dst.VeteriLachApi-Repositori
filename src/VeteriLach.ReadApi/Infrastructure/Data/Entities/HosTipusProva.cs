using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosTipusProva
{
    public Guid IdTipusProva { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public byte Tipus { get; set; }

    public virtual ICollection<HosDetallTipusProva> HosDetallTipusProvas { get; set; } = new List<HosDetallTipusProva>();

    public virtual ICollection<HosProva> HosProvas { get; set; } = new List<HosProva>();

    public virtual ICollection<HosTipusTipusProva> HosTipusTipusProvas { get; set; } = new List<HosTipusTipusProva>();
}
