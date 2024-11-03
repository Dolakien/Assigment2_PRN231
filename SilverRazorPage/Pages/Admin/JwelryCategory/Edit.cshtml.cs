using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Models;
using System.Text.Json;

namespace SilverRazorPage.Pages.Admin.JwelryCategory
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; } = string.Empty;

        [BindProperty]
        public Category Category { get; set; } = default!;

        public IActionResult OnGet()
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

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Chuẩn bị dữ liệu JSON cho Category để gửi đi
            var jsonContent = JsonContent.Create(Category);

            // Gọi API PUT để cập nhật Category
            HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:5204/api/Category/update", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/JwelryCategory/Index"); // Chuyển trang nếu cập nhật thành công
            }

            return Page(); // Ở lại trang nếu cập nhật thất bại
        }
    }
}
