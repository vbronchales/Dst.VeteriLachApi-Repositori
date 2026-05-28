using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcComunicatSm
{
    public Guid IdComunicatSms { get; set; }

    public Guid? IdPlantillaSms { get; set; }

    public string NumTelefon { get; set; } = null!;

    public string TextSms { get; set; } = null!;

    public Guid? IdSms { get; set; }

    public virtual SlcComunicat IdComunicatSmsNavigation { get; set; } = null!;

    public virtual SlcPlantillaSm? IdPlantillaSmsNavigation { get; set; }
}
