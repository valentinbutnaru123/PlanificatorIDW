using Application.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence.Persistence;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace PlanificatorMVC.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly ISpeakerManager _speakerManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ISpeakerRepository speakerRepository,
            ISpeakerManager speakerManager,
            IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _speakerManager = speakerManager;
            _speakerRepository = speakerRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Email { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [StringLength(100, ErrorMessage = "To much characters for Bio")]
            [Display(Name = "Bio")]
            public string Bio { get; set; }

            [StringLength(60, ErrorMessage = "To much characters for Company")]
            [Display(Name = "Company")]
            public string Company { get; set; }

            [Display(Name = "Profile Photo")]
            public string UserPhoto { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            var speakerProfile = await _speakerRepository.GetSpeakerBySpeakerIdAsync(user.Id);

            Email = email;

            Input = new InputModel
            {
                FirstName = speakerProfile.FirstName,
                LastName = speakerProfile.LastName,
                UserPhoto = speakerProfile.PhotoPath,
                Bio = speakerProfile.Bio,
                Company = speakerProfile.Company,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile picture)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var speakerProfile = await _speakerRepository.GetSpeakerBySpeakerIdAsync(user.Id);

            if (picture != null)
            {
                var fileName = Path.Combine(_hostingEnvironment.WebRootPath + @"\images", speakerProfile.Email + Path.GetFileName(picture.FileName));
                picture.CopyTo(new FileStream(fileName, FileMode.Create));
                speakerProfile.PhotoPath = @"\images\" + Path.GetFileName(speakerProfile.Email + picture.FileName);
            }

            if (Input.FirstName != speakerProfile.FirstName)
            {
                speakerProfile.FirstName = Input.FirstName;
            }

            if (Input.LastName != speakerProfile.LastName)
            {
                speakerProfile.LastName = Input.LastName;
            }

            if (Input.Bio != speakerProfile.Bio)
            {
                speakerProfile.Bio = Input.Bio;
            }

            if (Input.Company != speakerProfile.Company)
            {
                speakerProfile.Company = Input.Company;
            }

            await _speakerManager.UpdateSpeakerProfileAsync(speakerProfile);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}