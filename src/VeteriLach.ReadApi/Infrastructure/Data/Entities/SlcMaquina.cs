using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcMaquina
{
    public Guid IdMaquina { get; set; }

    public string Nom { get; set; } = null!;

    public string? AdresaIp { get; set; }

    public virtual ICollection<SlcConnexio> SlcConnexios { get; set; } = new List<SlcConnexio>();

    public virtual ICollection<SlcMacMaquina> SlcMacMaquinas { get; set; } = new List<SlcMacMaquina>();

    public virtual ICollection<SlcUsuari> IdUsuaris { get; set; } = new List<SlcUsuari>();
}
