using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacRelPerfilTarifaFamilium
{
    public Guid IdPerfilTarifa { get; set; }

    public Guid IdFamilia { get; set; }

    public Guid? IdTipusTarifa { get; set; }

    public decimal? Descompte { get; set; }

    public virtual FacFamilium IdFamiliaNavigation { get; set; } = null!;

    public virtual FacPerfilTarifa IdPerfilTarifaNavigation { get; set; } = null!;
}
