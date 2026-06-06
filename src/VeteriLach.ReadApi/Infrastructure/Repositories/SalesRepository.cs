using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Application.Common.Models;
using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure.Data;
using VeteriLach.ReadApi.Mapper;

namespace VeteriLach.ReadApi.Infrastructure
{
    public partial class SalesRepository(VeteriLachDbContext context, ILogger<SalesRepository> logger) : ISalesRepository
    {
    }
}
