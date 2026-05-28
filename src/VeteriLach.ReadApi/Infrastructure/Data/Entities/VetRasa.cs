using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetRasa
{
    public Guid IdRasa { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public Guid IdEspecie { get; set; }

    public int TamanyRelatiu { get; set; }

    public virtual VetEspecie IdEspecieNavigation { get; set; } = null!;

    public virtual ICollection<VetAnimal> VetAnimals { get; set; } = new List<VetAnimal>();
}
