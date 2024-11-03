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
using Repository.Contract.Request;
using System.Text.Json;

namespace SilverRazorPage.Pages.Admin.Account
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


        [BindProperty]
        public BranchAccount BranchAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Fetch Accounts from API
            var RoleResponse = await _httpClient.GetAsync("http://localhost:5204/api/Role");
            var RoleData = await RoleResponse.Content.ReadAsStringAsync();
            var Roles = JsonSerializer.Deserialize<List<Role>>(RoleData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Populate dropdowns with AccountName and CategoryName
            ViewData["RoleId"] = new SelectList(Roles, "RoleId", "RoleName"); // Adjust "FullName" if necessary
            return Page();
        }




        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        { 

            // Serialize LoginRequest to JSON
            var jsonContent = JsonContent.Create(BranchAccount);

            // Call the API for login
            HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:5204/api/Account/update", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Account/Index"); // Redirect on successful login
            }
            return RedirectToPage("/Admin/Account/Index"); // Redirect on successful login
        }
    }
}
