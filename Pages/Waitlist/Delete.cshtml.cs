using LibraryOfThingsM4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WaitlistEntity = LibraryOfThingsM4.Models.Waitlist;

namespace LibraryOfThingsM4.Pages.Waitlist;

public class DeleteModel(LibraryDbContext context) : PageModel
{
    [BindProperty]
    public WaitlistEntity Waitlist { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? itemid, int? memberid)
    {
        if (itemid is null || memberid is null)
        {
            return NotFound();
        }

        var entry = await context.Waitlist.AsNoTracking()
            .FirstOrDefaultAsync(row => row.ItemId == itemid && row.MemberId == memberid);
        if (entry is null)
        {
            return NotFound();
        }

        Waitlist = entry;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var entry = await context.Waitlist
            .FirstOrDefaultAsync(row => row.ItemId == Waitlist.ItemId && row.MemberId == Waitlist.MemberId);
        if (entry is null)
        {
            return NotFound();
        }

        context.Waitlist.Remove(entry);
        await context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
