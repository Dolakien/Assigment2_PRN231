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
using System.Net.Http;

namespace SilverRazorPage.Pages.SilverJwelry
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public IndexModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public List<SilverJewelry> SilverJewelry { get; set; } = new List<SilverJewelry>();
        public List<Category> Categories { get; set; } = new List<Category>(); // Thêm dòng này

        public async Task OnGetAsync()
        {
            // Lấy danh sách SilverJewelry từ API
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5204/api/Jwelry");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            SilverJewelry = JsonSerializer.Deserialize<List<SilverJewelry>>(data, options);

            // Lấy danh sách Category từ API
            var categoryResponse = await _httpClient.GetAsync("http://localhost:5204/api/Category");
            var categoryData = await categoryResponse.Content.ReadAsStringAsync();
            Categories = JsonSerializer.Deserialize<List<Category>>(categoryData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

}
