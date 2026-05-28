using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetVistaPerruquery
{
    public DateTime DiaVenda { get; set; }

    public string? NomMascota { get; set; }

    public string? Cognoms { get; set; }

    public string Concepte { get; set; } = null!;

    public decimal Preu { get; set; }

    public string Torn { get; set; } = null!;
}
