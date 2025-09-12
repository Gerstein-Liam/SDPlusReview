using DOMAIN;
using Microsoft.AspNetCore.Mvc;

using SDPlusApplicationServer_FakeDatabase.FakeDb;

namespace SDPlusApplicationServer_FakeDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController  : ControllerBase
    {
      

        private readonly ILogger<PropertyController> _logger;

        private readonly FakeDbController _fakeDbContext;

        public PropertyController(ILogger<PropertyController> logger, FakeDbController FakeDbContext)
        {
            _logger = logger;
            _fakeDbContext = FakeDbContext;   
            
        }

        [HttpGet]
        public IEnumerable<OwnerDto> GetAll()
        {
            return _fakeDbContext.GetAll();

        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateProperty(PropertyDto Item)
        {
            if (!_fakeDbContext.CreateProperty(Item)) return BadRequest("Oups");

            return Ok("Owner Added succussfuly");
        }

        [HttpPut()]
        public async Task<ActionResult<string>> UpdateProperty(PropertyDto Item)
        {
            if (!_fakeDbContext.UpdateProperty(Item)) return BadRequest("Oups");

            return Ok("Owner Added succussfuly");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteProperty(string Id)
        {
            if (!_fakeDbContext.DeleteOwner(Id)) return BadRequest("Oups");

            return Ok("Owner Added succussfuly");
        }




    }
}
