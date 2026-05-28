using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosTipusTipusProva
{
    public Guid IdTipusProva { get; set; }

    public byte Tipus { get; set; }

    public string NomDispositiu { get; set; } = null!;

    public string? Observacions { get; set; }

    public virtual HosTipusProva IdTipusProvaNavigation { get; set; } = null!;
}
