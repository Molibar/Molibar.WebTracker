using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;

namespace Molibar.WebTracking.Domain.Tracking
{
    public interface IPageEventTrackingService
    {
        PageEvent Add(PageEvent pageEvent);
    }

    public class PageEventTrackingService : IPageEventTrackingService
    {
        private IPageEventRepository _pageEventRepository;

        public PageEventTrackingService(IPageEventRepository pageEventRepository)
        {
            _pageEventRepository = pageEventRepository;
        }

        public PageEvent Add(PageEvent pageEvent)
        {
            _pageEventRepository.Insert(pageEvent);
            return pageEvent;
        }
    }
}