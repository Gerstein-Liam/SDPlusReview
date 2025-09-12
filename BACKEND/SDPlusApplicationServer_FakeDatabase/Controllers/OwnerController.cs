using DOMAIN;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SDPlusApplicationServer_FakeDatabase.FakeDb;

namespace SDPlusApplicationServer_FakeDatabase.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {


        private readonly ILogger<PropertyController> _logger;

        private readonly FakeDbController _fakeDbContext;

        public OwnerController(ILogger<PropertyController> logger, FakeDbController FakeDb)
        {
            _logger = logger;
            _fakeDbContext = FakeDb;

        }

        [HttpGet]
        public IEnumerable<OwnerDto> GetAll()
        {
            return _fakeDbContext.GetAll();

        }


        [HttpPost]
        public async Task<ActionResult<string>> CreateAllOwners(List<OwnerDto> Item)
        {
            if (!_fakeDbContext.CreateAllOwner(Item)) return BadRequest("Oups");

            return Ok("Owner Created");
        }

        /*  PAS IMPLEMENTE COTE WPF POUR LE MOMENT

        [HttpPost]
        public async Task<ActionResult<string>> CreateOwner(OwnerDto Item)
        {
            if (!_fakeDbContext.CreateOwner(Item)) return BadRequest("Oups");

            return Ok("Owner Created");
        }

        [HttpPut()]
        public async Task<ActionResult<string>> UpdateOwner(OwnerDto Item)
        {
            if (!_fakeDbContext.UpdateOwner(Item)) return BadRequest("Oups");

            return Ok("Owner Updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteOwner(string Id)
        {
            if (!_fakeDbContext.DeleteOwner(Id)) return BadRequest("Oups");

            return Ok("Owner Deleted");
        }
        */

    }
}
