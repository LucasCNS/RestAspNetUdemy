using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
			if (IsNumeric(firstNumber) || IsNumeric(secondNumber))
			{
				decimal firstConvertedNumber = ConvertToNumeric(firstNumber);
				decimal secondConvertedNumber = ConvertToNumeric(secondNumber);

				decimal sum = firstConvertedNumber + secondConvertedNumber;

				return Ok(sum);
			}

			return BadRequest("One or both inputs are not valid numbers.");
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

		[HttpGet("multiplication/{firstNumber}/{secondNumber}")]
		public IActionResult Multiplication(string firstNumber, string secondNumber)
		{
			if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
			{
				return BadRequest("One or both inputs are not valid numbers.");
			}

			decimal firstConvertedNumber = ConvertToNumeric(firstNumber);
			decimal secondConvertedNumber = ConvertToNumeric(secondNumber);

			decimal multiplication = firstConvertedNumber * secondConvertedNumber;

			return Ok(multiplication);
		}

		[HttpGet("division/{firstNumber}/{secondNumber}")]
		public IActionResult Division(string firstNumber, string secondNumber)
		{
			if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber))
			{
				return BadRequest("One or both inputs are not valid numbers.");
			}

			decimal firstConvertedNumber = ConvertToNumeric(firstNumber);
			decimal secondConvertedNumber = ConvertToNumeric(secondNumber);

			decimal division = firstConvertedNumber / secondConvertedNumber;

			return Ok(division);
		}

		[HttpGet("average/{firstNumber}/{secondNumber}/{thirdNumber}")]
		public IActionResult Average(string firstNumber, string secondNumber, string thirdNumber)
		{
			if (!IsNumeric(firstNumber) || !IsNumeric(secondNumber) || !IsNumeric(thirdNumber))
			{
				return BadRequest("One or both inputs are not valid numbers.");
			}

			decimal firstConvertedNumber = ConvertToNumeric(firstNumber);
			decimal secondConvertedNumber = ConvertToNumeric(secondNumber);
			decimal thirdConvertedNumber = ConvertToNumeric(thirdNumber);

			decimal[] values = { firstConvertedNumber, secondConvertedNumber, thirdConvertedNumber };

			decimal average = values.Average();

			return Ok(average);
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
