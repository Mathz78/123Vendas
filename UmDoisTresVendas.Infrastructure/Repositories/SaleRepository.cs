using UmDoisTresVendas.Application.Interfaces;
using UmDoisTresVendas.Domain.Entities;
using UmDoisTresVendas.Infrastructure.Data;

namespace UmDoisTresVendas.Infrastructure.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context) { }
    
    public IQueryable<Sale> Query()
    {
        return _context.Sales.AsQueryable();
    }
}