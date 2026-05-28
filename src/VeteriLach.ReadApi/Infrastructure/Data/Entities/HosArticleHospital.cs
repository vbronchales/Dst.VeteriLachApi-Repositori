using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosArticleHospital
{
    public Guid IdArticleHospital { get; set; }

    public int Tipus { get; set; }

    public virtual FacArticle IdArticleHospitalNavigation { get; set; } = null!;
}
