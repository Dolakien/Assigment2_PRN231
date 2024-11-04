using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccessObject;
using System.Text.Json;

namespace SilverRazorPage.Pages.Admin.JwelryCategory
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DeleteModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; } = string.Empty;

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            if (Category == null)
            {
                Category = new Category();
            }
            Category.CategoryId = Id;
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5204/api/Category/{Id}");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            Category = JsonSerializer.Deserialize<Category>(data, options);
            return Page();
        }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Call the API for login
            HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5204/api/Category/delete/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/JwelryCategory/Index"); // Redirect on successful login
            }
            return RedirectToPage("/Admin/JwelryCategory/Index"); // Redirect on successful login
        }
    }
}
