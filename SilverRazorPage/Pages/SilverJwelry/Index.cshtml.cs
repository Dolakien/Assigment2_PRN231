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
        public List<Category> Categories { get; set; } = new List<Category>();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            HttpResponseMessage response;
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                // Kiểm tra nếu SearchTerm là số thì sử dụng cho metalWeight, nếu không thì dùng cho nameSearchTerm
                if (decimal.TryParse(SearchTerm, out var metalWeight))
                {
                    // SearchTerm là số, sử dụng cho metalWeight
                    response = await _httpClient.GetAsync($"http://localhost:5204/api/Jwelry/search?metalWeight={metalWeight}");
                }
                else
                {
                    // SearchTerm không phải là số, sử dụng cho nameSearchTerm
                    response = await _httpClient.GetAsync($"http://localhost:5204/api/Jwelry/search?nameSearchTerm={SearchTerm}");
                }
            }
            else
            {
                // Nếu không có SearchTerm, lấy tất cả SilverJewelry
                response = await _httpClient.GetAsync("http://localhost:5204/api/Jwelry");
            }

            var data = await response.Content.ReadAsStringAsync();
            SilverJewelry = JsonSerializer.Deserialize<List<SilverJewelry>>(data, options);

            // Lấy danh sách Category từ API
            var categoryResponse = await _httpClient.GetAsync("http://localhost:5204/api/Category");
            var categoryData = await categoryResponse.Content.ReadAsStringAsync();
            Categories = JsonSerializer.Deserialize<List<Category>>(categoryData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

}
