using AutoMapper;
using VeteriLach.ReadApi.Application.Sales.DTOs;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Application.Sales.Mappings;

public class SalesMappingProfile : Profile
{
    public SalesMappingProfile()
    {
        // FacVendum → SaleDto
        CreateMap<FacVendum, SaleDto>()
            .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.IdVenda))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.IdClient))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.IdClientNavigation.IdClientNavigation.Nom))
            .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.IdVenedor))
            .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.IdVenedorNavigation.IdVenedorNavigation.Nom))
            .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.DiaVenda))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalVenda))
            .ForMember(dest => dest.TotalPaid, opt => opt.MapFrom(src => src.TotalPagat))
            .ForMember(dest => dest.TotalChange, opt => opt.MapFrom(src => src.TotalCanvi))
            .ForMember(dest => dest.PaidDate, opt => opt.MapFrom(src => src.DiaPagat))
            .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.IdCaixa))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.IdCaixaNavigation.Nom))
            .ForMember(dest => dest.IsPaymentCash, opt => opt.MapFrom(src => src.IdCaixaNavigation.Efectiu))
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.IdReferencia))
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.IdReferenciaNavigation != null ? src.IdReferenciaNavigation.IdAnimalNavigation.IdPacient1.Nom : null))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Resum))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Observacions))
            .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.FacArticleVenuts.Count));

        // FacVendum → SaleDetailDto
        CreateMap<FacVendum, SaleDetailDto>()
            .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.IdVenda))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.IdClient))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.IdClientNavigation.IdClientNavigation.Nom))
            .ForMember(dest => dest.CustomerNif, opt => opt.MapFrom(src => src.IdClientNavigation.IdClientNavigation.Nif))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => 
                src.IdClientNavigation.IdClientNavigation.SlcTelefons.OrderBy(t => t.Ordre).FirstOrDefault() != null 
                    ? src.IdClientNavigation.IdClientNavigation.SlcTelefons.OrderBy(t => t.Ordre).FirstOrDefault()!.Numero
                    : null))
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.IdClientNavigation.IdClientNavigation.Email))
            .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.IdVenedor))
            .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.IdVenedorNavigation.IdVenedorNavigation.Nom))
            .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.DiaVenda))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalVenda))
            .ForMember(dest => dest.TotalPaid, opt => opt.MapFrom(src => src.TotalPagat))
            .ForMember(dest => dest.TotalChange, opt => opt.MapFrom(src => src.TotalCanvi))
            .ForMember(dest => dest.PaidDate, opt => opt.MapFrom(src => src.DiaPagat))
            .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.IdCaixa))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.IdCaixaNavigation.Nom))
            .ForMember(dest => dest.IsPaymentCash, opt => opt.MapFrom(src => src.IdCaixaNavigation.Efectiu))
            .ForMember(dest => dest.BankAccount, opt => opt.MapFrom(src => src.IdCaixaNavigation.NumeroCompte))
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.IdReferencia))
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.IdReferenciaNavigation != null ? src.IdReferenciaNavigation.IdAnimalNavigation.IdPacient1.Nom : null))
            .ForMember(dest => dest.AnimalSpecies, opt => opt.MapFrom(src => 
                src.IdReferenciaNavigation != null && src.IdReferenciaNavigation.IdRasaNavigation != null && src.IdReferenciaNavigation.IdRasaNavigation.IdEspecieNavigation != null
                    ? src.IdReferenciaNavigation.IdRasaNavigation.IdEspecieNavigation.Nom 
                    : null))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Resum))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Observacions))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.FacArticleVenuts));

        // FacArticleVenut → SaleItemDto
        CreateMap<FacArticleVenut, SaleItemDto>()
            .ForMember(dest => dest.SaleItemId, opt => opt.MapFrom(src => src.IdArticleVenut))
            .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.IdArticle))
            .ForMember(dest => dest.ArticleName, opt => opt.MapFrom(src => src.NomArticle))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantitat))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.ValorPreu))
            .ForMember(dest => dest.VatAmount, opt => opt.MapFrom(src => src.ValorIva))
            .ForMember(dest => dest.VatRate, opt => opt.MapFrom(src => src.FactorIva))
            .ForMember(dest => dest.VatName, opt => opt.MapFrom(src => src.NomIva))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Descompte))
            .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => src.ValorPagat))
            .ForMember(dest => dest.PaidDate, opt => opt.MapFrom(src => src.DiaPagat))
            .ForMember(dest => dest.NetCost, opt => opt.MapFrom(src => src.CostNet))
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Ordre))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Observacions));

        // FacAcompte → PaymentAdvanceDto
        CreateMap<FacAcompte, PaymentAdvanceDto>()
            .ForMember(dest => dest.PaymentAdvanceId, opt => opt.MapFrom(src => src.IdAcompte))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.IdClient))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.IdClientNavigation.IdClientNavigation.Nom))
            .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.IdVenedor))
            .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.IdVenedorNavigation.IdVenedorNavigation.Nom))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.DiaAcompte))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Valor))
            .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.IdCaixa))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.IdCaixaNavigation != null ? src.IdCaixaNavigation.Nom : null))
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.IdReferencia))
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.IdReferenciaNavigation != null ? src.IdReferenciaNavigation.IdAnimalNavigation.IdPacient1.Nom : null))
            .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.Referencia))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Observacions));

        // FacVendum → DebtDto
        CreateMap<FacVendum, DebtDto>()
            .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.IdVenda))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.IdClient))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.IdClientNavigation.IdClientNavigation.Nom))
            .ForMember(dest => dest.CustomerNif, opt => opt.MapFrom(src => src.IdClientNavigation.IdClientNavigation.Nif))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => 
                src.IdClientNavigation.IdClientNavigation.SlcTelefons.OrderBy(t => t.Ordre).FirstOrDefault() != null 
                    ? src.IdClientNavigation.IdClientNavigation.SlcTelefons.OrderBy(t => t.Ordre).FirstOrDefault()!.Numero
                    : null))
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.IdClientNavigation.IdClientNavigation.Email))
            .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.DiaVenda))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalVenda))
            .ForMember(dest => dest.TotalPaid, opt => opt.MapFrom(src => src.TotalPagat))
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.IdReferencia))
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.IdReferenciaNavigation != null ? src.IdReferenciaNavigation.IdAnimalNavigation.IdPacient1.Nom : null))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Resum))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Observacions));
    }
}
