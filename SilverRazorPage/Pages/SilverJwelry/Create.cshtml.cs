﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccessObject;
using Repository.Contract.Response;
using System.Text.Json;

namespace SilverRazorPage.Pages.SilverJwelry
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
        public SilverJewelry SilverJewelry { get; set; } = default!;

        public int? AccountId { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {

            // Fetch Categories from API
            var categoryResponse = await _httpClient.GetAsync("http://localhost:5204/api/Category");
            var categoryData = await categoryResponse.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<Category>>(categoryData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Populate dropdowns with AccountName and CategoryName
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName"); // Adjust "CategoryName" if necessary

            return Page();
        }




        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AccountId = HttpContext.Session.GetInt32("AccountId");
            if (SilverJewelry == null)
            {
                SilverJewelry = new SilverJewelry();
            }
            SilverJewelry.AccountId = AccountId;

            // Serialize LoginRequest to JSON
            var jsonContent = JsonContent.Create(SilverJewelry);

            // Call the API for login
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5204/api/Jwelry/create", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/SilverJwelry/Index"); // Redirect on successful login
            }
            return RedirectToPage("/SilverJwelry/Index"); // Redirect on successful login
        }
    }
}
