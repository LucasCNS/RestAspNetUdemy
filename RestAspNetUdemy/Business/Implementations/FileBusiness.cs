using RestAspNetUdemy.Data.VO;

namespace RestAspNetUdemy.Business.Implementations
{
	public class FileBusiness : IFileBusiness
	{
		private readonly string _basePath;
		private readonly IHttpContextAccessor _context;

		public FileBusiness(IHttpContextAccessor context)
		{
			_context = context;
			_basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
		}

		public byte[] GetFile(string filename)
		{
			throw new NotImplementedException();
		}

		public async Task<FileDetailVO> SaveFileToDiskAsync(IFormFile file)
		{
			FileDetailVO fileDetail = new FileDetailVO();

			var documentType = Path.GetExtension(file.FileName);
			var baseUrl = _context.HttpContext.Request.Host;

			if (documentType.ToLower() == ".pdf" || documentType.ToLower() == ".png" ||
				documentType.ToLower() == ".jpg" || documentType.ToLower() == ".jpeg")
			{
				var documentName = Path.GetFileName(file.FileName);
				if (file != null && file.Length > 0)
				{
					var destination = Path.Combine(_basePath, "", documentName);
					fileDetail.DocumentName = documentName;
					fileDetail.DocumentType = documentType;
					fileDetail.DocumentUrl = Path.Combine(baseUrl + "/api/file/v1" + fileDetail.DocumentName);

					using var stream = new FileStream(destination, FileMode.Create);
					await file.CopyToAsync(stream);

				}
			}
			return fileDetail;
		}

		public Task<List<FileDetailVO>> SaveFilesToDisk(IFormFile file)
		{
			throw new NotImplementedException();
		}
	}
}
