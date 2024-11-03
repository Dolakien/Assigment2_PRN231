using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccessObject;
using System.Text.Json;

namespace SilverRazorPage.Pages.Admin.RoleAccount
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CreateModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }


        [BindProperty]
        public Role Role { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Serialize LoginRequest to JSON
            var jsonContent = JsonContent.Create(Role);

            // Call the API for login
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5204/api/Role/create", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/RoleAccount/Index"); // Redirect on successful login
            }
            return RedirectToPage("/Admin/RoleAccount/Index"); // Redirect on successful login
        }
    }
}
