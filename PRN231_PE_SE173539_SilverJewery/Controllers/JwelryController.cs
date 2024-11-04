using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Contract.Request;
using Repository;
using Repository.Interfaces;

namespace PRN231_PE_SE173539_SilverJewery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwelryController : ODataController
    {
        public readonly IJewlryRepo silverJewelryRepo;
        public readonly IAccountRepo accountRepo;
        public readonly ICatrgoryRepo categoryRepo;

        public JwelryController(IJewlryRepo silverJewelryRepo, IAccountRepo accountRepo, ICatrgoryRepo categoryRepo)
        {
            this.silverJewelryRepo = silverJewelryRepo;
            this.accountRepo = accountRepo;
            this.categoryRepo = categoryRepo;
        }


        [EnableQuery]
        [HttpGet]
        public IActionResult GetAllJwelries()
        {
            return Ok(silverJewelryRepo.GetJwelries());
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetJwelryById([FromODataUri] string id)
        {
            var enitty = silverJewelryRepo.GetJwelry(id);
            if (enitty == null)
            {
                return NotFound();
            }
            return Ok(enitty);
        }

        [EnableQuery]
        [HttpGet("search")]
        public IActionResult SearchJewelries([FromQuery] string? nameSearchTerm, [FromQuery] decimal? metalWeight)
        {
            List<SilverJewelry> results = silverJewelryRepo.SearchSilverJewelry(nameSearchTerm, metalWeight);
            return Ok(results);
        }

        [HttpPost("create")]
        public IActionResult Createjwelry([FromBody] SilverJewelry silverJewelry)
        {
            var Response = silverJewelryRepo.addJwelry(silverJewelry);

            if (Response)
            {
                return Ok("Success!");
            }
            else { 
                return BadRequest("Fail!");
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateSilver([FromBody] SilverJewelry silverJewelry)
        {
            var Response = silverJewelryRepo.updateJwelry(silverJewelry);

            if (Response)
            {
                return Ok("Success!");
            }
            else
            {
                return BadRequest("Fail!");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveSilver(string id)
        {
            var Response = silverJewelryRepo.removeJwelry(id);

            if (Response)
            {
                return Ok("Success!");
            }
            else
            {
                return BadRequest("Fail!");
            }
        }
    }
}
