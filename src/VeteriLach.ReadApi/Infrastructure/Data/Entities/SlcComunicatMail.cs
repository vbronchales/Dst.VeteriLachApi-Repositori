using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcComunicatMail
{
    public Guid IdComunicatMail { get; set; }

    public Guid? IdPlantillaMail { get; set; }

    public string AdresaMail { get; set; } = null!;

    public string TextMail { get; set; } = null!;

    public virtual SlcComunicat IdComunicatMailNavigation { get; set; } = null!;

    public virtual SlcPlantillaMail? IdPlantillaMailNavigation { get; set; }
}
