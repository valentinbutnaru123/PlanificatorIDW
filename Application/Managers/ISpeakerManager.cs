using Domain.Core;
using System.Threading.Tasks;

namespace Application.Managers
{
    public interface ISpeakerManager
    {
        public Task AddSpeakerProfileAsync(SpeakerProfile speaker);

        public Task UpdateSpeakerProfileAsync(SpeakerProfile speaker);
    }
}