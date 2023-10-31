using Demo.Domain.Enities;

namespace Demo.Application.Contracts
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> Get();
        Task Add(Item item);
        Task Delete(int id);
    }
}
