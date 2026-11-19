using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryOfThingsM4.Data;
using LibraryOfThingsM4.Models;

namespace LibraryOfThingsM4.Pages.ItemPages;

public class IndexModel : PageModel
{
    private readonly LibraryDbContext _context;

    public IndexModel(LibraryDbContext context)
    {
        _context = context;
    }

    public IList<Item> Item { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Item = await _context.Items.ToListAsync();
    }
}
