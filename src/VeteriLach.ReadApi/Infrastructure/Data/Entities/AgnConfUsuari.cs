using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class AgnConfUsuari
{
    public Guid IdUsuari { get; set; }

    public int HoraInici { get; set; }

    public int HoraFinal { get; set; }

    public int TamCeldes { get; set; }

    public int ModeSetmana { get; set; }

    public bool VeureCalendari { get; set; }

    public decimal FontSizeLinia1 { get; set; }

    public decimal FontSizeLinia2 { get; set; }

    public decimal Zoom { get; set; }

    public virtual SlcUsuari IdUsuariNavigation { get; set; } = null!;
}
