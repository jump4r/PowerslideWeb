using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PowerslideWebsite.Models;
using PowerslideWebsite.Utilities;
using Microsoft.AspNetCore.Identity;
using PowerslideWebsite.Data;

namespace PowerslideWebsite.Pages.Beatmaps
{
    public class IndexModel : PageModel
    {
        private readonly PowerslideWebsite.Models.SiteContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager, SiteContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        public IList<Submission> Submission { get; set; }

        public async Task OnGetAsync()
        {
            Submission = await _context.Submission.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Submission = await _context.Submission.AsNoTracking().ToListAsync();
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var _submission = new Submission()
            {
                User = user.UserName,
                Artist = FileUpload.Artist,
                Song = FileUpload.Song
            };

            
            _context.Submission.Add(_submission);
            await _context.SaveChangesAsync();

            string _webRoot = Startup.WebRoot;

            var beatmapDirectory = Directory.CreateDirectory(_webRoot + "\\b\\" + _submission.ID);

            var downloadDirectoryPath = Directory.CreateDirectory(_webRoot + "\\downloads\\" + _submission.ID);
            var downloadDirectory = Path.Combine(_webRoot, "downloads", _submission.ID.ToString());

            // Upload Beatmaps to server.
            await FileUploadHelper.ProcessFormFile(FileUpload.BeatmapSong, beatmapDirectory.FullName);
            await FileUploadHelper.ProcessBeatmapUpload(FileUpload.BeatmapFiles, beatmapDirectory.FullName);

            // Broken (Broken, No Files in the Archive...)
            // 
            FileUploadHelper.CreateZipFromDirectory(beatmapDirectory.FullName, downloadDirectory);

            var _beatmap = new Beatmap()
            {
                SumbissionID = _submission.ID,
                Name = FileUpload.User // For now I guess
            };

            _context.Beatmap.Add(_beatmap);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
