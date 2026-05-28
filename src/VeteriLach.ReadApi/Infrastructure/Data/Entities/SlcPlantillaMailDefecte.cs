using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcPlantillaMailDefecte
{
    public int IndexPlantilla { get; set; }

    public Guid IdPlantillaMail { get; set; }

    public virtual SlcPlantillaMail IdPlantillaMailNavigation { get; set; } = null!;
}
