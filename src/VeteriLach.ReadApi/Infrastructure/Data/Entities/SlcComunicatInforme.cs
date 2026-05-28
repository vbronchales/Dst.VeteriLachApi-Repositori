using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcComunicatInforme
{
    public Guid IdComunicatInforme { get; set; }

    public int CodiPrograma { get; set; }

    public int CodiInforme { get; set; }

    public string? DetallsInforme { get; set; }

    public virtual SlcComunicat IdComunicatInformeNavigation { get; set; } = null!;
}
