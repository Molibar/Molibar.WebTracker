using Molibar.WebTracking.Domain.Model;

namespace Molibar.WebTracking.Domain.Repositories
{
    public interface IPageEventRepository
    {
        void Initialize();
        void Insert(PageEvent pageEvent);
    }
}