using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Contract.Response;
using Repository.Contract.Request;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace SilverRazorPage.Pages.Authen
{
    public class LogincshtmlModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public LogincshtmlModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; }

        public LoginResponse LoginResponse { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Serialize LoginRequest to JSON
            var jsonContent = JsonContent.Create(LoginRequest);

            // Call the API for login
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5204/api/Account/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var data = await response.Content.ReadAsStringAsync();
                LoginResponse = JsonSerializer.Deserialize<LoginResponse>(data, options);

                // Store the JWT token in session
                if (!string.IsNullOrEmpty(LoginResponse.JwtToken)) // Kiểm tra token có hợp lệ không
                {
                    HttpContext.Session.SetString("JwtToken", LoginResponse.JwtToken);

                    // Giải mã token
                    DecodeJwtToken(LoginResponse.JwtToken);
                }
                else
                {
                    ErrorMessage = "Failed to retrieve JWT token.";
                    return Page();
                }

                return RedirectToPage("/SilverJwelry/Index"); // Redirect on successful login
            }
            else
            {
                // Set error message if login fails
                ErrorMessage = "Login failed. Please check your credentials and try again.";
                return Page();
            }
        }

        private void DecodeJwtToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            // Lấy thông tin từ các claim
            var claims = token.Claims;

            // Ví dụ: Lấy các giá trị cần thiết từ claims
            var account = claims.FirstOrDefault(c => c.Type == "nameid")?.Value; 
            var role = claims.FirstOrDefault(c => c.Type == "role")?.Value; 
            var email = claims.FirstOrDefault(c => c.Type == "email")?.Value;

            HttpContext.Session.SetString("role", role);

        }
    }
}
