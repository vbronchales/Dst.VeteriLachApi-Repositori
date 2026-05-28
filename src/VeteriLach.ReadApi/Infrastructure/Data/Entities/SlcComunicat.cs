using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcComunicat
{
    public Guid IdComunicat { get; set; }

    public DateTime DiaCreacio { get; set; }

    public Guid IdUsuariCreacio { get; set; }

    public DateTime? DiaComunicat { get; set; }

    public Guid? IdUsuariComunicat { get; set; }

    public int Estat { get; set; }

    public Guid IdPersona { get; set; }

    public Guid? IdReferencia { get; set; }

    public virtual SlcPersona IdPersonaNavigation { get; set; } = null!;

    public virtual SlcUsuari? IdUsuariComunicatNavigation { get; set; }

    public virtual SlcUsuari IdUsuariCreacioNavigation { get; set; } = null!;

    public virtual SlcComunicatInforme? SlcComunicatInforme { get; set; }

    public virtual SlcComunicatMail? SlcComunicatMail { get; set; }

    public virtual SlcComunicatSm? SlcComunicatSm { get; set; }
}
