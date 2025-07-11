using RestAspNetUdemy.Model;
using RestAspNetUdemy.Repository;

namespace RestAspNetUdemy.Business.Implementations
{
	public class BookBusinessImplementation : IBookBusiness
	{
		private readonly IRepository<Book> _repository;

		public BookBusinessImplementation(IRepository<Book> repository)
		{
			_repository = repository;
		}

		public List<Book> FindAll()
		{
			return _repository.FindAll();
		}

		public Book FindById(int id)
		{
			return _repository.FindById(id);
		}

		public Book Create(Book book)
		{
			return _repository.Create(book);
		}

		public Book Update(Book book)
		{
			return _repository.Update(book);
		}

		public void Delete(int id)
		{
			_repository.Delete(id);
		}
	}
}
