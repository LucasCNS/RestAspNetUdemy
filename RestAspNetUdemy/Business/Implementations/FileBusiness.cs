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

		public Task<List<FileDetailVO>> SaveFilesToDisk(IFormFile file)
		{
			throw new NotImplementedException();
		}

		public Task<FileDetailVO> SaveFileToDisk(IFormFile file)
		{
			throw new NotImplementedException();
		}
	}
}
