using UmDoisTresVendas.Domain.Entities;

namespace UmDoisTresVendas.Application.Interfaces;

public interface ISaleRepository : IBaseRepository<Sale>
{
    public IQueryable<Sale> Query();
}
