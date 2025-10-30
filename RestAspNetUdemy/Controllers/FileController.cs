using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAspNetUdemy.Business;
using RestAspNetUdemy.Data.VO;

namespace RestAspNetUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
	[Authorize("Bearer")]
	[Route("api/[controller]/v{version:apiVersion}")]
	public class FileController : Controller
	{
		private readonly IFileBusiness _fileBusiness;

		public FileController(IFileBusiness fileBusiness)
		{
			_fileBusiness = fileBusiness;
		}

		[HttpPost("uploadFile")]
		public async Task<IActionResult> UploadOneFileAsync(IFormFile file)
		{
			FileDetailVO detail = await _fileBusiness.SaveFileToDiskAsync(file);
			return new OkObjectResult(detail);
		}
	}
}
