using Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Managers
{
    public interface IPresentationManager
    {
        public Task AddPresentation(ICollection<PresentationTag> presentation);

        public Task AssignSpeakerToPresentationAsync(SpeakerProfile speaker, Presentation presentation);
    }
}