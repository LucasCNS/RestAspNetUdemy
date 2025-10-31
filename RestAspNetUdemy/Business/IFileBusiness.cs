using RestAspNetUdemy.Data.VO;

namespace RestAspNetUdemy.Business
{
	public interface IFileBusiness
	{
		public byte[] GetFile(string filename);
		public Task<FileDetailVO> SaveFileToDiskAsync(IFormFile file);
		public Task<List<FileDetailVO>> SaveFilesToDiskAsync(IList<IFormFile> file);
	}
}
