using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnHoraDisponible
{
    public Guid IdHoraDisponible { get; set; }

    public Guid IdAgenda { get; set; }

    public DateTime HoraInici { get; set; }

    public DateTime HoraFinal { get; set; }

    public int DiaSetmana { get; set; }

    public virtual AgnAgendum IdAgendaNavigation { get; set; } = null!;
}
