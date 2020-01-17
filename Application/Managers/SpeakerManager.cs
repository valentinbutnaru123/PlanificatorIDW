using Domain.Core;

using Persistence.Persistence;
using System.Threading.Tasks;

namespace Application.Managers
{
    public class SpeakerManager : ISpeakerManager
    {
        private readonly PlanificatorDbContext _planificatorDbContext;

        public SpeakerManager(PlanificatorDbContext planificatorDbContext)
        {
            _planificatorDbContext = planificatorDbContext;
        }

        public async Task AddSpeakerProfileAsync(SpeakerProfile speaker)
        {
            _planificatorDbContext.SpeakerProfiles.Add(speaker);
            await _planificatorDbContext.SaveChangesAsync();
        }

        public async Task UpdateSpeakerProfileAsync(SpeakerProfile speaker)
        {
            _planificatorDbContext.SpeakerProfiles.Update(speaker);
            await _planificatorDbContext.SaveChangesAsync();
        }
    }
}