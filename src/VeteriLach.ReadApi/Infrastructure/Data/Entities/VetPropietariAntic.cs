using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetPropietariAntic
{
    public Guid IdPropietariAntic { get; set; }

    public Guid IdPropietari { get; set; }

    public Guid IdAnimal { get; set; }

    public DateTime DiaCanvi { get; set; }

    public string MotiuCanvi { get; set; } = null!;

    public virtual VetAnimal IdAnimalNavigation { get; set; } = null!;

    public virtual VetPropietari IdPropietariNavigation { get; set; } = null!;
}
