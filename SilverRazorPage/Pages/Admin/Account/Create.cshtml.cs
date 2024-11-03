using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Contract.Request;
using System.Text.Json;

namespace SilverRazorPage.Pages.Admin.Account
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
        public CreateAccountRequest BranchAccount { get; set; } = default!;

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Serialize LoginRequest to JSON
            var jsonContent = JsonContent.Create(BranchAccount);

            // Call the API for login
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5204/api/Account/register", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Account/Index"); // Redirect on successful login
            }
            return RedirectToPage("/Admin/Account/Index"); // Redirect on successful login
        }
    }
}
