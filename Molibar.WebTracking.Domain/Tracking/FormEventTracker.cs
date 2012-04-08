using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;

namespace Molibar.WebTracking.Domain.Tracking
{
    public class FormEventTracker
    {
        private IFormEventRepository _formEventRepository;
        
        public FormEventTracker(IFormEventRepository formEventRepository)
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
