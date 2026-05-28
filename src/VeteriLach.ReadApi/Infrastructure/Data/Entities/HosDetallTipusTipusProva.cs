using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosDetallTipusTipusProva
{
    public Guid IdDetallTipusProva { get; set; }

    public byte Tipus { get; set; }

    public string NomValor { get; set; } = null!;

    public virtual HosDetallTipusProva IdDetallTipusProvaNavigation { get; set; } = null!;
}
