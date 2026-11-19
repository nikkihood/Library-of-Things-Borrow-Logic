using LibraryOfThingsM4.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WaitlistEntity = LibraryOfThingsM4.Models.Waitlist;

namespace LibraryOfThingsM4.Pages.Waitlist;

public class IndexModel(LibraryDbContext context) : PageModel
{
    public IList<WaitlistEntity> Waitlists { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Waitlists = await context.Waitlist
            .AsNoTracking()
            .OrderByDescending(entry => entry.RequestDate)
            .ThenBy(entry => entry.ItemId)
            .ThenBy(entry => entry.MemberId)
            .ToListAsync();
    }
}
