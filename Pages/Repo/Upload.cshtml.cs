using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Intranet.Models;
using System.IO;

namespace Intranet.Pages.Repo
{
    public class UploadModel : PageModel
    {
        private readonly Intranet.Models.IntranetContext _context;

        public UploadModel(Intranet.Models.IntranetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RepoFile RepoFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (RepoFile.File.Length > 0)
            {
                RepoFile.Version = 1;
                RepoFile.Size = (int)RepoFile.File.Length;
                RepoFile.Date = DateTime.Now;
                RepoFile.GUID = Guid.NewGuid();
                if (RepoFile.ShownName == null)
                    RepoFile.ShownName = RepoFile.File.FileName;
                var strsplit = RepoFile.File.FileName.Split(".");
                RepoFile.Extension = strsplit[strsplit.Length-1];
                RepoFile.Name = RepoFile.File.FileName.Replace("."+RepoFile.Extension, String.Empty);
                var writePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", RepoFile.GUID.ToString());
                if (!Directory.Exists(writePath))
                    Directory.CreateDirectory(writePath);
                var filePath = Path.Combine(writePath, RepoFile.Version.ToString());
                var fs = new FileStream(filePath, FileMode.Create);
                RepoFile.File.CopyTo(fs);
                fs.Close();

                _context.RepoFile.Add(RepoFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}