using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryOfThingsM4.Models;

namespace LibraryOfThingsM4.Pages.DonorPages;

public class CreateModel : PageModel
{
    private readonly LibraryOfThingsM4Context _context;

    public CreateModel(LibraryOfThingsM4Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Donor Donor { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Donor.Add(Donor);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
