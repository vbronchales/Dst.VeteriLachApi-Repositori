using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnAvi
{
    public Guid IdAvis { get; set; }

    public Guid IdUsuari { get; set; }

    public string Descripcio { get; set; } = null!;

    public string? Observacions { get; set; }

    public DateTime DiaInici { get; set; }

    public decimal Frequencia { get; set; }
}
