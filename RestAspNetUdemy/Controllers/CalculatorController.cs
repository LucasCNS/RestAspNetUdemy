using Microsoft.AspNetCore.Mvc;

namespace RestAspNetUdemy.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CalculatorController : ControllerBase
	{
		private readonly ILogger<CalculatorController> _logger;

		public CalculatorController(ILogger<CalculatorController> logger)
		{
			_logger = logger;

		}

		[HttpGet("sum/{firstNumber}/{secondNumber}")]
		public IActionResult Sum(string firstNumber, string secondNumber)
		{
			if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
			{
				return BadRequest("One or both inputs are not valid numbers.");
			}

			decimal firstConvertedNumber = ConvertToNumeric(firstNumber);
			decimal secondConvertedNumber = ConvertToNumeric(secondNumber);

			decimal sum = firstConvertedNumber + secondConvertedNumber;

			return Ok(sum);
		}

		[HttpGet("subtraction/{firstNumber}/{secondNumber}")]
		public IActionResult Subtraction(string firstNumber, string secondNumber)
		{
			if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
			{
				return BadRequest("One or both inputs are not valid numbers.");
			}

			decimal firstConvertedNumber = ConvertToNumeric(firstNumber);
			decimal secondConvertedNumber = ConvertToNumeric(secondNumber);

			decimal subtraction = firstConvertedNumber - secondConvertedNumber;

			return Ok(subtraction);
		}

		private bool IsNumeric(string input)
		{
			bool isNumber = decimal.TryParse(input, out _);

			return isNumber;
		}

		private decimal ConvertToNumeric(string input)
		{
			return Convert.ToDecimal(input);
		}
	}
}
