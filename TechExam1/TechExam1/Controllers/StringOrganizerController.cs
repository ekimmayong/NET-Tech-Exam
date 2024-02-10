using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechExam1.Interface;
using TechExam1.Models;
using TechExam1.Services;

namespace TechExam1.Controllers
{

    [ApiController]
    [Route("api/")]
    public class StringOrganizerController : ControllerBase
    {
        private readonly IStringOrganizerService _iStringService;
        public StringOrganizerController(IStringOrganizerService iStringService)
        {
            _iStringService = iStringService;
        }
        // POST: api/countuniquecharacters
        [HttpPost("countuniquecharacters")]
        public async Task<ActionResult> CountUniqueCharacters(UniqueCharacterRequest request)
        {
            var response = await _iStringService.GetNumberOfUniqueCharacterFromString(request.input);

            return Ok(response);
        }
    }
}
