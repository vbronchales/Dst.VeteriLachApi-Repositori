using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacDespesa
{
    public Guid IdDespesa { get; set; }

    public string Nom { get; set; } = null!;

    public decimal Valor { get; set; }

    public DateTime DiaDespesa { get; set; }

    public string? Observacions { get; set; }

    public Guid IdCaixa { get; set; }

    public virtual FacCaixa IdCaixaNavigation { get; set; } = null!;
}
