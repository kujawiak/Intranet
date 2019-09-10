using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Intranet.Models;

namespace Intranet.Pages.Repo
{
    public class DeleteModel : PageModel
    {
        private readonly Intranet.Models.IntranetContext _context;

        public DeleteModel(Intranet.Models.IntranetContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepoFile = await _context.RepoFile.FindAsync(id);

            if (RepoFile != null)
            {
                _context.RepoFile.Remove(RepoFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
