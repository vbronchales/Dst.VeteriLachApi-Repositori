using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosTipusBaixa
{
    public Guid IdTipusBaixa { get; set; }

    public string Nom { get; set; } = null!;

    public bool Mort { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<HosBaixa> HosBaixas { get; set; } = new List<HosBaixa>();
}
