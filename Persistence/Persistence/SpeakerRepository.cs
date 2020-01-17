using Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Persistence
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly PlanificatorDbContext _dbContext;

        public SpeakerRepository(PlanificatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetSpeakerProfilesCount()
        {
            return _dbContext.SpeakerProfiles.Count();
        }

        public async Task<SpeakerProfile> GetSpeakerBySpeakerIdAsync(string speakerId)
        {
            return await _dbContext.SpeakerProfiles.SingleOrDefaultAsync(s => s.SpeakerId == speakerId);
        }

        public async Task<SpeakerProfile> GetSpeakerBySpeakerEmailAsync(string email)
        {
            return await _dbContext.SpeakerProfiles
                .SingleOrDefaultAsync(s => s.Email == email);
        }

        public async Task<SpeakerProfile> GetSpeakerBySpeakerEmailIncludingRelationshipsAsync(string email)
        {
            return await _dbContext.SpeakerProfiles
                .Include(s => s.OwnedPresentations)
                .ThenInclude(p => p.PresentationTags)
                .ThenInclude(pt => pt.Tag)
                .SingleOrDefaultAsync(s => s.Email == email);
        }

        public async Task<ICollection<SpeakerProfile>> GetAllSpeakersProfilesAsync()
        {
            return await _dbContext.SpeakerProfiles.ToListAsync();
        }
    }
}