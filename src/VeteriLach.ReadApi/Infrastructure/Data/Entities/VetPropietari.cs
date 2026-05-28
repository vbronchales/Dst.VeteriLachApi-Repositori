using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetPropietari
{
    public Guid IdPropietari { get; set; }

    public Guid? IdFoto { get; set; }

    public virtual SlcPersona IdPropietari1 { get; set; } = null!;

    public virtual FacClient IdPropietariNavigation { get; set; } = null!;

    public virtual ICollection<VetAnimal> VetAnimals { get; set; } = new List<VetAnimal>();

    public virtual ICollection<VetPropietariAntic> VetPropietariAntics { get; set; } = new List<VetPropietariAntic>();

    public virtual ICollection<SlcNotificacio> IdNotificacios { get; set; } = new List<SlcNotificacio>();
}
