using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcMacMaquina
{
    public Guid IdMacMaquina { get; set; }

    public Guid IdMaquina { get; set; }

    public string NomAdresaMac { get; set; } = null!;

    public string? AdresaMac { get; set; }

    public virtual SlcMaquina IdMaquinaNavigation { get; set; } = null!;
}
