using Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Persistence
{
    public interface IPresentationRepository
    {
        public IEnumerable<Presentation> GetAllPresentations();

        public Task<IEnumerable<Presentation>> GetAllPresentationsAsync();

        public int GetPresentationCount();

        public IEnumerable<string> GetAllTagsNames(int presentationId);

        public Task<IEnumerable<string>> GetAllTagsNamesAsync(int presentationId);

        public Presentation GetPresentationById(int presentationId);

        public Task<Presentation> GetPresentationByIdAsync(int presentationId);
    }
}