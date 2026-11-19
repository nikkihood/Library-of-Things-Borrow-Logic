using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryOfThingsM4.Data;
using LibraryOfThingsM4.Models;

namespace LibraryOfThingsM4.Pages.ItemPages;

public class DeleteModel : PageModel
{
    private readonly LibraryDbContext _context;

    public DeleteModel(LibraryDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Item Item { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? itemid)
    {
        if (itemid is null)
        {
            return NotFound();
        }

        var item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == itemid);
        if (item is null)
        {
            return NotFound();
        }
        else
        {
            Item = item;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? itemid)
    {
        if (itemid is null)
        {
            return NotFound();
        }

        var item = await _context.Items.FindAsync(itemid);
        if (item != null)
        {
            Item = item;
            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
