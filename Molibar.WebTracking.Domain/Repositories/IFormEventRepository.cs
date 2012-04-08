using Molibar.WebTracking.Domain.Model;

namespace Molibar.WebTracking.Domain.Repositories
{
    public interface IFormEventRepository
    {
        void Initialize();
        void Insert(FormEvent formEvent);
    }
}
