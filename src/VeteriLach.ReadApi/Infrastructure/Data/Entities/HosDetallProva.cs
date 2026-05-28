using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosDetallProva
{
    public Guid IdDetallProva { get; set; }

    public Guid IdProva { get; set; }

    public Guid IdDetallTipusProva { get; set; }

    public string Valor { get; set; } = null!;

    public string? Observacions { get; set; }

    public virtual HosDetallTipusProva IdDetallTipusProvaNavigation { get; set; } = null!;

    public virtual HosProva IdProvaNavigation { get; set; } = null!;
}
