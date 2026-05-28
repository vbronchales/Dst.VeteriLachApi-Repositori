using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosResultatIdexx
{
    public Guid IdResultatIdexx { get; set; }

    public string NomFitxer { get; set; } = null!;

    public DateTime DiaResultat { get; set; }

    public Guid IdProva { get; set; }

    public virtual HosProva IdProvaNavigation { get; set; } = null!;
}
