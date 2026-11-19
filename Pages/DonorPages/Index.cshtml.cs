using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryOfThingsM4.Models;

namespace LibraryOfThingsM4.Pages.DonorPages;

public class IndexModel : PageModel
{
    private readonly LibraryOfThingsM4Context _context;

    public IndexModel(LibraryOfThingsM4Context context)
    {
        _context = context;
    }

    public IList<Donor> Donor { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Donor = await _context.Donor.ToListAsync();
    }
}
