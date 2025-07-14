using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestAspNetUdemy.Business;
using RestAspNetUdemy.Hypermedia.Filters;
using RestAspNetUdemy.Model;

namespace RestAspNetUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
	[Route("api/[controller]/v{version:apiVersion}")]
	public class BookController : ControllerBase
	{
		private readonly ILogger<BookController> _logger;
		private IBookBusiness _bookBusiness;

		public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
		{
			_logger = logger;
			_bookBusiness = bookBusiness;
		}

		[HttpGet]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult Get()
		{
			var book = _bookBusiness.FindAll();
			return Ok(book);
		}

		[HttpGet("{id}")]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult Get(int id)
		{
			var book = _bookBusiness.FindById(id);
			if (book == null) return NotFound();

			return Ok(book);
		}

		[HttpPost]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult PostOuCreate([FromBody] BookVO book)
		{
			if (book == null) return BadRequest();

			return Ok(_bookBusiness.Create(book));
		}

		[HttpPut]
		[TypeFilter(typeof(HyperMediaFilter))]
		public IActionResult PutOuUpdate([FromBody] BookVO book)
		{
			if (book == null) return BadRequest();

			return Ok(_bookBusiness.Update(book));
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_bookBusiness.Delete(id);
			return NoContent();
		}
	}
}
