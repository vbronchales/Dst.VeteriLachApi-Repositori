using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcBdConfig
{
    public int TipusPrograma { get; set; }

    public string Versio { get; set; } = null!;
}
