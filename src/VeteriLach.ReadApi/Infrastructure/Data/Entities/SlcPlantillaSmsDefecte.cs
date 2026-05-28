using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcPlantillaSmsDefecte
{
    public int IndexPlantilla { get; set; }

    public Guid IdPlantillaSms { get; set; }

    public virtual SlcPlantillaSm IdPlantillaSmsNavigation { get; set; } = null!;
}
