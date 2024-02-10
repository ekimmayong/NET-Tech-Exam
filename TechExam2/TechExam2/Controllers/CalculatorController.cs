using Microsoft.AspNetCore.Mvc;
using TechExam2.Interface;
using TechExam2.Model;

namespace TechExam2.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _iCalculatorService;
        public CalculatorController(ICalculatorService iCalculatorService)
        {
            _iCalculatorService = iCalculatorService;
        }

        [HttpPost("RoundSumOfTwoNumber")]
        public IActionResult RoundSumOfTwoNumber(CalculateRequest request)
        {
           var result = _iCalculatorService.RoundingSumOf2Int(request.FirstNumber, request.SecondNumber);

           return Ok(result);
        }
    }
}
