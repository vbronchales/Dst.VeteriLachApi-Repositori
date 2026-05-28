using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcNotificacio
{
    public Guid IdNotificacio { get; set; }

    public Guid IdTipusNotificacio { get; set; }

    public DateTime DiaCreacio { get; set; }

    public DateTime? DiaNotificada { get; set; }

    public string? Observacions { get; set; }

    public virtual SlcTipusNotificacio IdTipusNotificacioNavigation { get; set; } = null!;

    public virtual SlcTextNotificacio? SlcTextNotificacio { get; set; }

    public virtual ICollection<VetAnimal> IdAnimals { get; set; } = new List<VetAnimal>();

    public virtual ICollection<VetAnimal> IdAnimalsNavigation { get; set; } = new List<VetAnimal>();

    public virtual ICollection<HosPacient> IdPacients { get; set; } = new List<HosPacient>();

    public virtual ICollection<VetPropietari> IdPropietaris { get; set; } = new List<VetPropietari>();
}
