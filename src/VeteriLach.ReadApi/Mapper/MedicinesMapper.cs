using VeteriLach.ReadApi.Application.Medicines.DTOs;
using VeteriLach.ReadApi.Infrastructure.ExternalServices.Models;

namespace VeteriLach.ReadApi.Mapper
{
    public static class MedicinesMapper
    {
        public static HumanMedicineDto MapSearchResultToDto(this CimaMedicineSearchResult result)
        {
            return new HumanMedicineDto(
                result.RegistrationNumber,
                result.Name,
                result.Vtm?.Name ?? string.Empty,
                result.PharmaceuticalForm?.Name,
                result.Dose,
                result.AdministrationRoutes.FirstOrDefault()?.Name,
                result.Laboratory,
                result.IsCommercialized ? "Comercialitzat" : "No comercialitzat",
                null,
                null,
                result.RequiresPrescription,
                result.IsGeneric,
                null, null,
                result.Documents.FirstOrDefault(d => d.Type == 1)?.UrlHtml,
                result.Documents.FirstOrDefault(d => d.Type == 2)?.UrlHtml,
                DateTime.UtcNow
                );
        }


        /// <summary>
        /// Mapeja un detall complet a DTO
        /// </summary>
        public static HumanMedicineDto MapDetailToDto(this CimaMedicineDetail detail)
        {
            DateTime? authDate = null;
            if (detail.State?.AuthorizationTimestamp.HasValue == true)
            {
                authDate = DateTimeOffset.FromUnixTimeMilliseconds(detail.State.AuthorizationTimestamp.Value).DateTime;
            }

            // Construir cadena d'indicacions a partir dels ATCs si no hi ha camp específic
            var indications = detail.Atcs.Count > 0
                ? string.Join("; ", detail.Atcs.Select(atc => atc.Name))
                : null;

            // Combinar principis actius amb quantitats
            var activeIngredients = detail.ActivePrinciples.Count > 0
                ? string.Join(" + ", detail.ActivePrinciples
                    .OrderBy(p => p.Order)
                    .Select(p => $"{p.Name} {p.Quantity} {p.Unit}"))
                : detail.ActiveIngredients;

            return new HumanMedicineDto(
                detail.RegistrationNumber,
                detail.Name,
                activeIngredients,
                detail.PharmaceuticalForm?.Name,
                detail.Dose,
                string.Join(", ", detail.AdministrationRoutes.Select(r => r.Name)),
                detail.Laboratory,
                detail.IsCommercialized ? "Comercialitzat" : "No comercialitzat",
                authDate,
                indications,
                detail.RequiresPrescription,
                detail.IsGeneric,
                null,
                null,
                detail.Documents.FirstOrDefault(d => d.Type == 1)?.UrlHtml
                                       ?? detail.Documents.FirstOrDefault(d => d.Type == 1)?.Url,
                detail.Documents.FirstOrDefault(d => d.Type == 2)?.UrlHtml
                                   ?? detail.Documents.FirstOrDefault(d => d.Type == 2)?.Url,
                DateTime.UtcNow
                );
        }
    }
}
