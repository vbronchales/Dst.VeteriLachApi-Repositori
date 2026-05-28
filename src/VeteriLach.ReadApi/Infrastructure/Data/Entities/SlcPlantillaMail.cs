using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcPlantillaMail
{
    public Guid IdPlantillaMail { get; set; }

    public string Nom { get; set; } = null!;

    public int CodiPrograma { get; set; }

    public int IndexPlantilla { get; set; }

    public string AdresaFrom { get; set; } = null!;

    public string NomFrom { get; set; } = null!;

    public string TextMail { get; set; } = null!;

    public virtual ICollection<SlcComunicatMail> SlcComunicatMails { get; set; } = new List<SlcComunicatMail>();

    public virtual ICollection<SlcPlantillaMailDefecte> SlcPlantillaMailDefectes { get; set; } = new List<SlcPlantillaMailDefecte>();
}
