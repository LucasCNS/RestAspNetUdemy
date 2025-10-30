using RestAspNetUdemy.Data.VO;

namespace RestAspNetUdemy.Business
{
	public interface IFileBusiness
	{
		public byte[] GetFile(string filename);
		public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
		public Task<List<FileDetailVO>> SaveFilesToDisk(IFormFile file);

	}
}
