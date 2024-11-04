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

namespace SilverRazorPage.Pages.Admin.RoleAccount
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
        public int Id { get; set; }

        [BindProperty]
        public Role Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (Role == null)
            {
                Role = new Role();
            }
            Role.RoleId = Id;
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5204/api/Role/{Id}");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            Role = JsonSerializer.Deserialize<Role>(data, options);
            return Page();
        }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Call the API for login
            HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5204/api/Role/delete/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/RoleAccount/Index"); // Redirect on successful login
            }
            return RedirectToPage("/Admin/RoleAccount/Index"); // Redirect on successful login
        }
    }
}
