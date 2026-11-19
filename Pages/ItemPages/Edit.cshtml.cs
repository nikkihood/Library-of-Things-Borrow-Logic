using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryOfThingsM4.Data;
using LibraryOfThingsM4.Models;

namespace LibraryOfThingsM4.Pages.ItemPages;

public class EditModel : PageModel
{
    private readonly LibraryDbContext _context;

    public EditModel(LibraryDbContext context)
    {
        _context = context;
    }

    public List<SelectListItem> DonorOptions { get; private set; } = [];

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
        Item = item;
        await LoadDonorOptionsAsync();
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!await _context.Donors.AnyAsync(donor => donor.DonorId == Item.DonorId))
        {
            ModelState.AddModelError("Item.DonorId", "Select an existing donor.");
        }

        if (!ModelState.IsValid)
        {
            await LoadDonorOptionsAsync();
            return Page();
        }

        _context.Attach(Item).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ItemExists(Item.ItemId))
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

    private bool ItemExists(int itemid)
    {
        return _context.Items.Any(e => e.ItemId == itemid);
    }

    private async Task LoadDonorOptionsAsync()
    {
        var donors = await _context.Donors
            .AsNoTracking()
            .OrderBy(donor => donor.DonorId)
            .ToListAsync();

        DonorOptions = donors.Select(donor => new SelectListItem
        {
            Value = donor.DonorId.ToString(),
            Text = string.IsNullOrWhiteSpace(donor.Description)
                ? $"Donor {donor.DonorId} — {donor.DonationDate:yyyy-MM-dd}"
                : $"Donor {donor.DonorId} — {donor.Description}"
        }).ToList();
    }
}
