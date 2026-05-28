using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcArxiu
{
    public Guid IdArxiu { get; set; }

    public int Tipus { get; set; }

    public string? NomOriginal { get; set; }

    public DateTime? DiaInsercio { get; set; }

    public string? Extensio { get; set; }

    public string? Observacions { get; set; }

    public int? IndexBinari { get; set; }

    public virtual ICollection<HosArxiuVisitum> HosArxiuVisita { get; set; } = new List<HosArxiuVisitum>();

    public virtual SlcArxiuBinari? SlcArxiuBinari { get; set; }
}
