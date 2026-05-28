using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacTancament
{
    public Guid IdTancament { get; set; }

    public Guid IdCaixa { get; set; }

    public DateTime Dia { get; set; }

    public decimal ValorFinal { get; set; }

    public decimal? ValorDesquadre { get; set; }

    public string? MotiuDesquadre { get; set; }

    public Guid IdVenedor { get; set; }

    public virtual FacCaixa IdCaixaNavigation { get; set; } = null!;

    public virtual FacVenedor IdVenedorNavigation { get; set; } = null!;
}
