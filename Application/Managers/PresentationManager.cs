using Domain.Core;
using Persistence.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Managers
{
    public class PresentationManager : IPresentationManager
    {
        private readonly PlanificatorDbContext _planificatorDbContext;

        public PresentationManager(PlanificatorDbContext planificatorDbContext)
        {
            _planificatorDbContext = planificatorDbContext;
        }

        public async Task AddPresentation(ICollection<PresentationTag> presentationTags)
        {
            foreach (var presantationTag in presentationTags)
            {
                _planificatorDbContext.PresentationTags.Add(presantationTag);
            }
            await _planificatorDbContext.SaveChangesAsync();
        }

        public async Task AssignSpeakerToPresentationAsync(SpeakerProfile speaker, Presentation presentation)
        {
            PresentationSpeaker presentationSpeaker = new PresentationSpeaker
            {
                SpeakerProfile = speaker,
                Presentation = presentation,
            };
            _planificatorDbContext.PresentationSpeakers.Add(presentationSpeaker);
            await _planificatorDbContext.SaveChangesAsync();
        }
    }
}