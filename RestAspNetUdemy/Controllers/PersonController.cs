using Microsoft.AspNetCore.Mvc;

namespace RestAspNetUdemy.Controllers
{
	public class PersonController : ControllerBase
	{

		private readonly ILogger<PersonController> _logger;

		public PersonController(ILogger<PersonController> logger)
		{
			_logger = logger;

		}
	}
}
