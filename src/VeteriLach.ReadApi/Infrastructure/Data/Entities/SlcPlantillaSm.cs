using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcPlantillaSm
{
    public Guid IdPlantillaSms { get; set; }

    public string Nom { get; set; } = null!;

    public int CodiPrograma { get; set; }

    public int IndexPlantilla { get; set; }

    public string TextSms { get; set; } = null!;

    public virtual ICollection<SlcComunicatSm> SlcComunicatSms { get; set; } = new List<SlcComunicatSm>();

    public virtual ICollection<SlcPlantillaSmsDefecte> SlcPlantillaSmsDefectes { get; set; } = new List<SlcPlantillaSmsDefecte>();
}
