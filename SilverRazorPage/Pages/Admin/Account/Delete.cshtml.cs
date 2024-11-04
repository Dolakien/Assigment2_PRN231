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

namespace SilverRazorPage.Pages.Admin.Account
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
        public BranchAccount BranchAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (BranchAccount == null)
            {
                BranchAccount = new BranchAccount();
            }
            BranchAccount.AccountId = Id;
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5204/api/Account/{Id}");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            BranchAccount = JsonSerializer.Deserialize<BranchAccount>(data, options);
            return Page();
        }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Call the API for login
            HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5204/api/Account/delete/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Account/Index"); // Redirect on successful login
            }
            return RedirectToPage("/Admin/Account/Index"); // Redirect on successful login
        }
    }
}
