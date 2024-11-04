using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace SilverRazorPage.Pages.SilverJwelry
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DetailsModel(HttpClient httpClient, IConfiguration configuration)
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

            string cateId = SilverJewelry.CategoryId;

            // Fetch Category from API
            var categoryResponse = await _httpClient.GetAsync($"http://localhost:5204/api/Category/{cateId}");
            var categoryData = await categoryResponse.Content.ReadAsStringAsync();
            var category = JsonSerializer.Deserialize<Category>(categoryData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Chỉ lấy CategoryName và truyền vào ViewData
            ViewData["CategoryName"] = category?.CategoryName;

            return Page();
        }
    }
}
