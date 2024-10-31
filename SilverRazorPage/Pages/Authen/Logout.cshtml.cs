using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SilverRazorPage.Pages.Authen
{
    public class logoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Xóa thông tin đăng nhập khỏi session
            HttpContext.Session.Remove("JwtToken");
            HttpContext.Session.Remove("role");

            return RedirectToPage("/Authen/Login"); // Chuyển hướng về trang Index
        }
    }
}
