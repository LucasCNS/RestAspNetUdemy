using RestAspNetUdemy.Model;

namespace RestAspNetUdemy.Business
{
	public interface IBookBusiness
	{
		List<BookVO> FindAll();
		BookVO FindById(int id);
		BookVO Create(BookVO book);
		BookVO Update(BookVO book);
		void Delete(int id);
	}
}
