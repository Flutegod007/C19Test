﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C19Test.Data;
using C19Test.Models;

namespace C19Test.Pages.Cases
{
    public class EditModel : PageModel
    {
        private readonly C19Test.Data.C19TestContext _context;

        public EditModel(C19Test.Data.C19TestContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Case Case { get; set; }
        public IList<Status> StatusSL { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StatusSL = await _context.Status.ToListAsync();
            Case = await _context.Cases.FirstOrDefaultAsync(m => m.CaseID == id);
            
            if (Case == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Case).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(Case.CaseID))
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

        private bool CaseExists(int id)
        {
            return _context.Cases.Any(e => e.CaseID == id);
        }
    }
}
