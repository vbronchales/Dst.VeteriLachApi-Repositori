using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacCaixa
{
    public Guid IdCaixa { get; set; }

    public string Nom { get; set; } = null!;

    public string Codi { get; set; } = null!;

    public bool Efectiu { get; set; }

    public Guid? IdEntitatBancaria { get; set; }

    public string? NumeroCompte { get; set; }

    public bool Visible { get; set; }

    public string? Observacions { get; set; }

    public decimal Comissio { get; set; }

    public virtual ICollection<FacAcompte> FacAcomptes { get; set; } = new List<FacAcompte>();

    public virtual ICollection<FacDespesa> FacDespesas { get; set; } = new List<FacDespesa>();

    public virtual ICollection<FacFactura> FacFacturas { get; set; } = new List<FacFactura>();

    public virtual ICollection<FacProveidor> FacProveidors { get; set; } = new List<FacProveidor>();

    public virtual ICollection<FacTancament> FacTancaments { get; set; } = new List<FacTancament>();

    public virtual ICollection<FacTraspa> FacTraspaIdCaixaDestiNavigations { get; set; } = new List<FacTraspa>();

    public virtual ICollection<FacTraspa> FacTraspaIdCaixaOrigenNavigations { get; set; } = new List<FacTraspa>();

    public virtual ICollection<FacVendum> FacVenda { get; set; } = new List<FacVendum>();

    public virtual FacEntitatBancarium? IdEntitatBancariaNavigation { get; set; }
}
