using ECommerceApp.Core.Entities;

namespace ECommerceApp.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetSubcategoriesAsync(int parentId);
    }
}
