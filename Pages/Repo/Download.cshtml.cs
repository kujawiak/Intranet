using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Pages.Repo
{
    public class DownloadModel : PageModel
    {
        private readonly Intranet.Models.IntranetContext _context;

        public DownloadModel(Intranet.Models.IntranetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RepoFile RepoFile { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepoFile = await _context.RepoFile.FirstOrDefaultAsync(m => m.Id == id);

            if (RepoFile == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", RepoFile.UUID.ToString());
            Stream stream = new FileStream(filePath, FileMode.Open);

            if(stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.

            return File(stream, "application/octet-stream", RepoFile.FullName); // returns a FileStreamResult
        }
    }
}