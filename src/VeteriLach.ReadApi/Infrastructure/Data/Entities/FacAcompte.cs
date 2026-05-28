using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacAcompte
{
    public Guid IdAcompte { get; set; }

    public Guid IdClient { get; set; }

    public Guid IdVenedor { get; set; }

    public DateTime DiaAcompte { get; set; }

    public decimal Valor { get; set; }

    public Guid? IdCaixa { get; set; }

    public string? Observacions { get; set; }

    public Guid? IdReferencia { get; set; }

    public string? Referencia { get; set; }

    public virtual FacCaixa? IdCaixaNavigation { get; set; }

    public virtual FacClient IdClientNavigation { get; set; } = null!;

    public virtual VetAnimal? IdReferenciaNavigation { get; set; }

    public virtual FacVenedor IdVenedorNavigation { get; set; } = null!;
}
