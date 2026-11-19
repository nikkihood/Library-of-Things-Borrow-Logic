using LibraryOfThingsM4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WaitlistEntity = LibraryOfThingsM4.Models.Waitlist;

namespace LibraryOfThingsM4.Pages.Waitlist;

public class DetailsModel(LibraryDbContext context) : PageModel
{
    public WaitlistEntity Waitlist { get; private set; } = default!;

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
}
