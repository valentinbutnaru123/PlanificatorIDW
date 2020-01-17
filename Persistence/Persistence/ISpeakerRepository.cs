using Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Persistence
{
    public interface ISpeakerRepository
    {
        public Task<ICollection<SpeakerProfile>> GetAllSpeakersProfilesAsync();

        public int GetSpeakerProfilesCount();

        public Task<SpeakerProfile> GetSpeakerBySpeakerIdAsync(string speakerId);

        public Task<SpeakerProfile> GetSpeakerBySpeakerEmailAsync(string email);

        public Task<SpeakerProfile> GetSpeakerBySpeakerEmailIncludingRelationshipsAsync(string email);
    }
}