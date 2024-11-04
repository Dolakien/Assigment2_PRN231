using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccessObject;
using System.Text.Json;

namespace SilverRazorPage.Pages.SilverJwelry
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EditModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; } = string.Empty;

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            if (SilverJewelry == null)
            {
                SilverJewelry = new SilverJewelry();
            }
            SilverJewelry.SilverJewelryId = Id;

            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5204/api/Jwelry/{Id}");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            SilverJewelry = JsonSerializer.Deserialize<SilverJewelry>(data, options);

            // Fetch Categories from API
            var categoryResponse = await _httpClient.GetAsync("http://localhost:5204/api/Category");
            var categoryData = await categoryResponse.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<Category>>(categoryData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName"); // Adjust "CategoryName" if necessary

            return Page();
        }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Serialize LoginRequest to JSON
            var jsonContent = JsonContent.Create(SilverJewelry);

            // Call the API for login
            HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:5204/api/Jwelry/update", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/SilverJwelry/Index"); // Redirect on successful login
            }
            return RedirectToPage("/SilverJwelry/Index"); // Redirect on successful login
        }
    }
}
