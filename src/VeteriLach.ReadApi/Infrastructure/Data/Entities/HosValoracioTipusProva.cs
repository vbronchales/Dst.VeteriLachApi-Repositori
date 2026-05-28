using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosValoracioTipusProva
{
    public Guid IdValoracioTipusProva { get; set; }

    public Guid IdDetallTipusProva { get; set; }

    public decimal ValorNormalDesde { get; set; }

    public decimal ValorNormalFins { get; set; }

    public Guid? IdReferencia { get; set; }

    public virtual HosDetallTipusProva IdDetallTipusProvaNavigation { get; set; } = null!;

    public virtual VetEspecie? IdReferenciaNavigation { get; set; }
}
