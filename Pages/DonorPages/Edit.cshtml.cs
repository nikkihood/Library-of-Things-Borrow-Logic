using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryOfThingsM4.Models;

namespace LibraryOfThingsM4.Pages.DonorPages;

public class EditModel : PageModel
{
    private readonly LibraryOfThingsM4Context _context;

    public EditModel(LibraryOfThingsM4Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Donor Donor { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? donorid)
    {
        if (donorid is null)
        {
            return NotFound();
        }

        var donor = await _context.Donor.FirstOrDefaultAsync(m => m.DonorId == donorid);
        if (donor is null)
        {
            return NotFound();
        }
        Donor = donor;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Donor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DonorExists(Donor.DonorId))
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

    private bool DonorExists(int donorid)
    {
        return _context.Donor.Any(e => e.DonorId == donorid);
    }
}
