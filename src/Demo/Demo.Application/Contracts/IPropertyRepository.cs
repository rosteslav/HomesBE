using Demo.Domain.Enities;

namespace Demo.Application.Contracts
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAll();
        Task Add(Property property);
        Task Delete(int id);
        Task Update(int id, Property property);
    }
}
