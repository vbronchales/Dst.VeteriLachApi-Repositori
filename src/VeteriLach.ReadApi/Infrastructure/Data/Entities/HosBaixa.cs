using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosBaixa
{
    public Guid IdPacient { get; set; }

    public Guid IdTipusBaixa { get; set; }

    public DateTime DiaBaixa { get; set; }

    public string? Observacions { get; set; }

    public virtual HosPacient IdPacientNavigation { get; set; } = null!;

    public virtual HosTipusBaixa IdTipusBaixaNavigation { get; set; } = null!;
}
