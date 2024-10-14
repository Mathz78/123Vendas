using UmDoisTresVendas.Domain.Entities;

namespace UmDoisTresVendas.Application.Interfaces;

public interface ISaleRepository : IBaseRepository<Sale>
{
    /// <summary>
    /// Retrieves a queryable collection of <see cref="Sale"/> entities.
    /// This allows for further filtering, ordering, and grouping operations on the sales data.
    /// </summary>
    /// <returns>An <see cref="IQueryable{Sale}"/> representing a collection of <see cref="Sale"/> entities.</returns>
    public IQueryable<Sale> Query();
}
