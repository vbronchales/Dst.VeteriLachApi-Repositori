using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacRelTarifaFamilium
{
    public Guid IdFamilia { get; set; }

    public Guid IdClient { get; set; }

    public Guid? IdTipusTarifa { get; set; }

    public decimal? Descompte { get; set; }

    public virtual FacClient IdClientNavigation { get; set; } = null!;

    public virtual FacFamilium IdFamiliaNavigation { get; set; } = null!;

    public virtual FacTipusTarifa? IdTipusTarifaNavigation { get; set; }
}
