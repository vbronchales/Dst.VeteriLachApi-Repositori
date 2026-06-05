namespace VeteriLach.ReadApi.Domain
{
    public abstract record PersonaBaseDto(
        Guid IdPersona, 
        string Nom,
        string? Cognom1,
        string? Cognom2,
        string? Nif,
        DateTime? DataNaixement,
        string? Email,
        bool AmbWhatsApp,
        string? Adresa,
        string? CodiPostal,
        string? Poblacio,
        string? Provincia,
        string? Pais
        )
    {
        public string Cognoms => string.Join(" ", new[] { Cognom1, Cognom2 }.Where(c => !string.IsNullOrWhiteSpace(c)));
        public List<TelefonDto> Telefons { get; set; } = [];
    }
}
