using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosFamArticleHospital
{
    public Guid IdFamArticleHospital { get; set; }

    public int Tipus { get; set; }

    public virtual FacFamilium IdFamArticleHospitalNavigation { get; set; } = null!;
}
