﻿using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Diagnostics.Contracts;
using Repository.Contract;
using Microsoft.AspNetCore.Identity.Data;
using BusinessObject.Models;
using Repository.Contract.Request;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;
namespace PRN231_PE_SE173539_SilverJewery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ODataController
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

        [HttpPut("update")]
        public IActionResult UpdateAccount([FromBody] BranchAccount account)
        {
            var Response = _accountRepo.updateAccount(account);

            return Response != null
                ? Ok(Response)
                : BadRequest(Empty);
        }


        [HttpDelete("delete/{id}")]
        public IActionResult RemoveAccount(int id)
        {
            var Response =  _accountRepo.removeAccount(id);

            return Response != null
                ? Ok(Response)
                : BadRequest(Empty);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetAccountById([FromODataUri] int id)
        {
            var enitty = _accountRepo.GetBranchAccountById(id);
            if (enitty == null)
            {
                return NotFound();
            }
            return Ok(enitty);
        }


        [EnableQuery]
        [HttpGet()]
        public IActionResult GetAllAcount()
        {
            return Ok(_accountRepo.GetAccounts());
        }

    }
}
