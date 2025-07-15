using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestAspNetUdemy.Business;
using RestAspNetUdemy.Hypermedia.Filters;
using RestAspNetUdemy.Model;

namespace RestAspNetUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
	[Route("swagger/api/[controller]/v{version:apiVersion}")]
	public class PersonController : ControllerBase
	{
		private readonly ILogger<PersonController> _logger;
		private IPersonBusiness _personBusiness;

		public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
		{
			_logger = logger;
			_personBusiness = personBusiness;
		}

		[HttpGet]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult Get()
		{
			var people = _personBusiness.FindAll();
			return Ok(people);
		}

		[HttpGet("{id}")]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult Get(long id)
		{
			var person = _personBusiness.FindById(id);
			if (person == null) return NotFound();

			return Ok(person);
		}

		[HttpPost]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult PostOuCreate([FromBody] PersonVO person)
		{
			if (person == null) return BadRequest();

			return Ok(_personBusiness.Create(person));
		}

		[HttpPut]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult PutOuUpdate([FromBody] PersonVO person)
		{
			if (person == null) return BadRequest();

			return Ok(_personBusiness.Update(person));
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			_personBusiness.Delete(id);
			return NoContent();
		}
	}
}
