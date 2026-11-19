using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryOfThingsM4.Models;

namespace LibraryOfThingsM4.Pages.DonorPages;

public class DeleteModel : PageModel
{
    private readonly LibraryOfThingsM4Context _context;

    public DeleteModel(LibraryOfThingsM4Context context)
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
        else
        {
            Donor = donor;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? donorid)
    {
        if (donorid is null)
        {
            return NotFound();
        }

        var donor = await _context.Donor.FindAsync(donorid);
        if (donor != null)
        {
            Donor = donor;
            _context.Donor.Remove(Donor);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
