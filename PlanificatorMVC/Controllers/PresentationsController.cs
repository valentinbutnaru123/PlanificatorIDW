using Application.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Persistence;
using PlanificatorMVC.Mappers;
using PlanificatorMVC.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PlanificatorMVC.Controllers
{
    [Authorize]
    public class PresentationsController : Controller
    {
        // private readonly PlanificatorDbContext _context;
        private readonly IPresentationManager _presentationManager;

        private readonly IPresentationViewModelMapper _presentationViewModelMapper;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IPresentationRepository _presentationRepository;

        public PresentationsController(PlanificatorDbContext context, IPresentationManager presentationManager, IPresentationViewModelMapper presentationViewModelMapper, ISpeakerRepository speakerRepository, IPresentationRepository presentationRepository)
        {
            //_context = context;
            _presentationManager = presentationManager;
            _presentationViewModelMapper = presentationViewModelMapper;
            _speakerRepository = speakerRepository;
            _presentationRepository = presentationRepository;
        }

        // GET: Presentations
        public async Task<IActionResult> Index()
        {
            var currentSpeaker = await _speakerRepository.GetSpeakerBySpeakerEmailIncludingRelationshipsAsync(HttpContext.User.Identity.Name);
            //var presentations = await _presentationRepository.GetAllPresentationsAsync();
            var presentationViewModels = _presentationViewModelMapper.MapFromPresentations(currentSpeaker.OwnedPresentations);

            if (presentationViewModels == null)
            {
                return View("NoPresentation");
            }

            return View(presentationViewModels);
        }

        //GET: Presentations/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var presentation = await _presentationRepository.GetPresentationByIdAsync(id);
            var user = HttpContext.User.Identity.Name;
            var speaker = await _speakerRepository.GetSpeakerBySpeakerEmailIncludingRelationshipsAsync(user);
            if (!(speaker.OwnedPresentations.Contains(presentation)))
            {
                return Forbid();
            }

            var presentationViewModel = _presentationViewModelMapper.MapPresentationToPresentationViewModel(presentation);
            return View(presentationViewModel);
        }
        public IActionResult Details([FromQuery] PresentationViewModel presentationViewModel)
        {

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var presentation = await _context.Presentations
            //    .FirstOrDefaultAsync(m => m.PresentationId == id);
            //if (presentation == null)
            //{
            //    return NotFound();
            //}



            return View(presentationViewModel);

        }

        // GET: Presentations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Presentations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ShortDescription,LongDescription,Tags")] PresentationViewModel presentationViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentSpeaker = await _speakerRepository.GetSpeakerBySpeakerEmailAsync(HttpContext.User.Identity.Name);
                var presentationTag = _presentationViewModelMapper.MapToPresentationTag(presentationViewModel, currentSpeaker);
                await _presentationManager.AddPresentation(presentationTag);
                //_context.Add(presentation);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //return View(presentation);
            return View();
        }

        // GET: Presentations/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var presentation = await _context.Presentations.FindAsync(id);
        //    if (presentation == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(presentation);
        //}

        // POST: Presentations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("PresentationId,Title,ShortDescription,LongDescription")] Presentation presentation)
        //{
        //    if (id != presentation.PresentationId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(presentation);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PresentationExists(presentation.PresentationId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(presentation);
        //}

        // GET: Presentations/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var presentation = await _context.Presentations
        //        .FirstOrDefaultAsync(m => m.PresentationId == id);
        //    if (presentation == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(presentation);
        //}

        // POST: Presentations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var presentation = await _context.Presentations.FindAsync(id);
        //    _context.Presentations.Remove(presentation);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PresentationExists(int id)
        //{
        //    return _context.Presentations.Any(e => e.PresentationId == id);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}