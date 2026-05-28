using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcRelacionsExterne
{
    public string IdRelacioExterna { get; set; } = null!;

    public Guid IdRelacioInterna { get; set; }

    public string TipusElem { get; set; } = null!;
}
