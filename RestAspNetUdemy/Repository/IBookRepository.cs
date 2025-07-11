using RestAspNetUdemy.Model;

namespace RestAspNetUdemy.Repository
{
	public interface IBookRepository
	{
		List<Book> FindAll();
		Book FindById(int id);
		Book Create(Book book);
		Book Update(Book book);
		void Delete(int id);
		bool Exists(int id);
	}
}
