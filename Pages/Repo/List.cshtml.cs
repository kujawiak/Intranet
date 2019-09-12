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
    public class ListModel : PageModel
    {
        private readonly Intranet.Models.IntranetContext _context;

        public ListModel(Intranet.Models.IntranetContext context)
        {
            _context = context;
        }

        public IList<RepoFile> RepoFile { get;set; }

        public async Task OnGetAsync()
        {
            RepoFile = await _context.RepoFile.ToListAsync();

        }
    }
}