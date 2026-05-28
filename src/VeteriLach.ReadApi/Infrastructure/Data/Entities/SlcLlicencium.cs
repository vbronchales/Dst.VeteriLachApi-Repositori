using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcLlicencium
{
    public int TipusLlicencia { get; set; }

    public int TipusPrograma { get; set; }

    public int? NumLlicencies { get; set; }

    public DateTime? DiaCaducitat { get; set; }

    public int CodiSistema { get; set; }

    public string NomSistema { get; set; } = null!;

    public bool Demo { get; set; }

    public string Checksum { get; set; } = null!;

    public string? DiscId { get; set; }
}
