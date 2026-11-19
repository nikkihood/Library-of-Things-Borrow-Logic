using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryOfThingsM4.Data;
using LibraryOfThingsM4.Models;

namespace LibraryOfThingsM4.Pages.ItemPages;

public class CreateModel : PageModel
{
    private readonly LibraryDbContext _context;

    public CreateModel(LibraryDbContext context)
    {
        _context = context;
    }

    public List<SelectListItem> DonorOptions { get; private set; } = [];

    public async Task OnGetAsync()
    {
        await LoadDonorOptionsAsync();
    }

    [BindProperty]
    public Item Item { get; set; } = new();

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
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

        _context.Items.Add(Item);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
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
