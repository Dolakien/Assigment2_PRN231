using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
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
        public IActionResult GetAllCategories()
        {
            return Ok(categoryRepo.GetCategories());
        }

        [EnableQuery]
        [HttpGet("id")]
        public IActionResult GetCategoryById([FromODataUri] string id)
        {
            var enitty = categoryRepo.GetCategory(id);
            if (enitty == null)
            {
                return NotFound();
            }
            return Ok(enitty);
        }








    }
}
