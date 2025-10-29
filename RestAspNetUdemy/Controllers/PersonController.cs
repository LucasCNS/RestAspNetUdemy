using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAspNetUdemy.Business;
using RestAspNetUdemy.Hypermedia.Filters;
using RestAspNetUdemy.Model;

namespace RestAspNetUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
	[Authorize("Bearer")]
	[Route("api/[controller]/v{version:apiVersion}")]
	public class PersonController : ControllerBase
	{
		private readonly ILogger<PersonController> _logger;
		private IPersonBusiness _personBusiness;

		public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
		{
			_logger = logger;
			_personBusiness = personBusiness;
		}

		[HttpGet("{sortDirection}/{pageSize}/{page}")]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult Get([FromQuery] string name, string sortDirection, int pageSize, int page)
		{
			var people = _personBusiness.FindWithPagedSearch(name, sortDirection, pageSize,page);
			
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

		[HttpGet("findPersonByName")]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult Get([FromQuery] string? firstName, [FromQuery] string? lastName)
		{
			var person = _personBusiness.FindByName(firstName, lastName);
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

		[HttpPatch("{id}")]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult Patch(long id)
		{
			var person = _personBusiness.Disable(id);
			if (person == null) return NotFound();

			return Ok(person);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			_personBusiness.Delete(id);
			return NoContent();
		}
	}
}
