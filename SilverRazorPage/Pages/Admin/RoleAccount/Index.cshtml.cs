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
    public class IndexModel : PageModel
    {
        private readonly HttpClient httpClient;
        public IndexModel() { httpClient = new HttpClient(); }
        public IList<Role> Role { get; set; } = default!;
        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5204/api/Role");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            Role = JsonSerializer.Deserialize<List<Role>>(data, options);
        }
    }
}
