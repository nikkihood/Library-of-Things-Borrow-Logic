using LibraryOfThingsM4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WaitlistEntity = LibraryOfThingsM4.Models.Waitlist;

namespace LibraryOfThingsM4.Pages.Waitlist;

public class CreateModel(LibraryDbContext context) : PageModel
{
    public List<SelectListItem> ItemOptions { get; private set; } = [];

    [BindProperty]
    public WaitlistEntity Waitlist { get; set; } = new()
    {
        RequestDate = DateOnly.FromDateTime(DateTime.Today)
    };

    public async Task OnGetAsync()
    {
        await LoadItemOptionsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Waitlist.ItemId <= 0 || !await context.Items.AnyAsync(item => item.ItemId == Waitlist.ItemId))
        {
            ModelState.AddModelError("Waitlist.ItemId", "Select an existing item.");
        }

        if (Waitlist.MemberId <= 0)
        {
            ModelState.AddModelError("Waitlist.MemberId", "Enter a valid member ID.");
        }

        if (Waitlist.RequestDate == default)
        {
            ModelState.AddModelError("Waitlist.RequestDate", "Enter a request date.");
        }

        if (await context.Waitlist.AnyAsync(entry =>
                entry.ItemId == Waitlist.ItemId && entry.MemberId == Waitlist.MemberId))
        {
            ModelState.AddModelError(string.Empty, "This member is already waitlisted for this item.");
        }

        if (!ModelState.IsValid)
        {
            await LoadItemOptionsAsync();
            return Page();
        }

        context.Waitlist.Add(Waitlist);
        await context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    private async Task LoadItemOptionsAsync()
    {
        var items = await context.Items
            .AsNoTracking()
            .OrderBy(item => item.ItemId)
            .ToListAsync();

        ItemOptions = items.Select(item => new SelectListItem
        {
            Value = item.ItemId.ToString(),
            Text = $"Item {item.ItemId} — {item.Name}"
        }).ToList();
    }
}
