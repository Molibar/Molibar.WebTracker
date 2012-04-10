using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;

namespace Molibar.WebTracking.Domain.Tracking
{
    public interface IFormEventTrackingService
    {
        FormEvent Add(FormEvent formEvent);
    }

    public class FormEventTrackingService : IFormEventTrackingService
    {
        private IFormEventRepository _formEventRepository;
        
        public FormEventTrackingService(IFormEventRepository formEventRepository)
        {
            _formEventRepository = formEventRepository;
        }

        public FormEvent Add(FormEvent formEvent)
        {
            _formEventRepository.Insert(formEvent);
            return formEvent;
        }
    }
}
