using Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Persistence
{
    public class PresentationRepository : IPresentationRepository
    {
        private readonly PlanificatorDbContext _dbContext;

        public PresentationRepository(PlanificatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<string> GetAllTagsNames(int presentationId)
        {
            List<string> tags = new List<string>();

            tags = _dbContext.Tags.Where(x => _dbContext.PresentationTags.Any(y => y.PresentationId == presentationId && x.TagId == y.TagId)).Select(x => x.TagName).ToList();

            return tags;
        }

        public async Task<IEnumerable<string>> GetAllTagsNamesAsync(int presentationId)
        {
            List<string> tags = new List<string>();

            tags = await _dbContext.Tags.Where(x => _dbContext.PresentationTags.Any(y => y.PresentationId == presentationId && x.TagId == y.TagId)).Select(x => x.TagName).ToListAsync();

            return tags;
        }

        public IEnumerable<Presentation> GetAllPresentations()
        {
            //if (_dbContext.Presentations.Count() == 0)
            //    return null;

            return _dbContext.Presentations
                .Include(p => p.PresentationTags)
                .ThenInclude(pt => pt.Tag)
                .ToList();
        }

        public async Task<IEnumerable<Presentation>> GetAllPresentationsAsync()
        {
            //if (_dbContext.Presentations.Count() == 0)
            //return null;
            return await _dbContext.Presentations
                .Include(p => p.PresentationTags)
                .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        public int GetPresentationCount()
        {
            return _dbContext.Presentations.Count();
        }

        public Presentation GetPresentationById(int presentationId)
        {
            return _dbContext.Presentations.Include(s => s.PresentationOwner).Include(s => s.PresentationSpeakers).SingleOrDefault(p => p.PresentationId == presentationId);
        }

        public async Task<Presentation> GetPresentationByIdAsync(int presentationId)
        {
            return await _dbContext.Presentations.Include(s => s.PresentationOwner).Include(s => s.PresentationSpeakers).SingleOrDefaultAsync(p => p.PresentationId == presentationId);
        }
    }
}