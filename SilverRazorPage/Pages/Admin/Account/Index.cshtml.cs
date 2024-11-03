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
    public class IndexModel : PageModel
    {
        private readonly HttpClient httpClient;
        public IndexModel() { httpClient = new HttpClient(); }
        public IList<BranchAccount> BranchAccount { get; set; } = default!;
        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5204/api/Account");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            BranchAccount = JsonSerializer.Deserialize<List<BranchAccount>>(data, options);
        }
    }
}
