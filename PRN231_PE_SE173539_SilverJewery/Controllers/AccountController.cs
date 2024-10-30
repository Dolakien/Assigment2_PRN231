using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Diagnostics.Contracts;
using Repository.Contract;
using Microsoft.AspNetCore.Identity.Data;
using BusinessObject.Models;
using Repository.Contract.Request;
namespace PRN231_PE_SE173539_SilverJewery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IAccountRepo _accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Repository.Contract.Request.LoginRequest loginRequest)
        {
            var loginResponse = await _accountRepo.CheckLogin(loginRequest);

            return loginResponse != null
                ? Ok(loginResponse)
                : BadRequest(Empty);
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountRequest createAccountRequest)
        {
            var loginResponse = await _accountRepo.addAccount(createAccountRequest);

            return loginResponse != null
                ? Ok(loginResponse)
                : BadRequest(Empty);
        }



    }
}
