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

		[HttpPost("uploadMultipleFiles")]
		public async Task<IActionResult> UploadMultiplesFileAsync(List<IFormFile> files)
		{
			List<FileDetailVO> details = await _fileBusiness.SaveFilesToDiskAsync(files);
			return new OkObjectResult(details);
		}

		[HttpGet("downloadFile/{fileName}")]
		public async Task<IActionResult> GetFileAsync(string fileName)
		{
			byte[] buffer = _fileBusiness.GetFile(fileName);

			if (buffer != null)
			{
				HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";
				HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
				await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);	
			}

			return new ContentResult();
		}
	}
}
