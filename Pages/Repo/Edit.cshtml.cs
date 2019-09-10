using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Intranet.Models;

namespace Intranet.Pages.Repo
{
    public class EditModel : PageModel
    {
        private readonly Intranet.Models.IntranetContext _context;

        public EditModel(Intranet.Models.IntranetContext context)
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

            _context.Attach(RepoFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepoFileExists(RepoFile.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RepoFileExists(int id)
        {
            return _context.RepoFile.Any(e => e.Id == id);
        }
    }
}
