using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacTraspa
{
    public Guid IdTraspas { get; set; }

    public Guid IdVenedor { get; set; }

    public DateTime DiaTraspas { get; set; }

    public decimal Valor { get; set; }

    public Guid IdCaixaOrigen { get; set; }

    public Guid IdCaixaDesti { get; set; }

    public string? Observacions { get; set; }

    public virtual FacCaixa IdCaixaDestiNavigation { get; set; } = null!;

    public virtual FacCaixa IdCaixaOrigenNavigation { get; set; } = null!;

    public virtual FacVenedor IdVenedorNavigation { get; set; } = null!;
}
