using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcConnexio
{
    public Guid IdConnexio { get; set; }

    public DateTime DiaConnexio { get; set; }

    public DateTime? DiaDesconnexio { get; set; }

    public Guid IdUsuari { get; set; }

    public Guid? IdMaquina { get; set; }

    public int Programa { get; set; }

    public int TipusLlicencia { get; set; }

    public string? UsuariSistema { get; set; }

    public virtual SlcMaquina? IdMaquinaNavigation { get; set; }

    public virtual SlcUsuari IdUsuariNavigation { get; set; } = null!;

    public virtual ICollection<SlcElementAlocat> SlcElementAlocats { get; set; } = new List<SlcElementAlocat>();
}
