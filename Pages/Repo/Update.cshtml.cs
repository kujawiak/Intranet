using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Intranet.Models;
using System.IO;

namespace Intranet.Pages.Repo
{
    public class UpdateModel : PageModel
    {
        private readonly Intranet.Models.IntranetContext _context;

        public UpdateModel(Intranet.Models.IntranetContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (RepoFile.File.Length > 0)
            {
                RepoFile UpdatedFile = new RepoFile();
                UpdatedFile.Version = RepoFile.Version+1;
                UpdatedFile.Size = (int)RepoFile.File.Length;
                UpdatedFile.Date = DateTime.Now;
                UpdatedFile.GUID = RepoFile.GUID;
                UpdatedFile.ShownName = RepoFile.ShownName;
                var strsplit = RepoFile.File.FileName.Split(".");
                UpdatedFile.Extension = strsplit[strsplit.Length-1];
                UpdatedFile.Name = RepoFile.File.FileName.Replace(UpdatedFile.Extension, String.Empty);
                var writePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", UpdatedFile.GUID.ToString());
                if (!Directory.Exists(writePath))
                    throw new Exception("Directory should exists.");
                var filePath = Path.Combine(writePath, UpdatedFile.Version.ToString());
                var fs = new FileStream(filePath, FileMode.Create);
                RepoFile.File.CopyTo(fs);
                fs.Close();

                _context.RepoFile.Add(UpdatedFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private bool RepoFileExists(int id)
        {
            return _context.RepoFile.Any(e => e.Id == id);
        }
    }
}
