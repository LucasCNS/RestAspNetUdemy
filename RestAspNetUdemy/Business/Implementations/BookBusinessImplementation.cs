using RestAspNetUdemy.Data.Converter.Implementations;
using RestAspNetUdemy.Model;
using RestAspNetUdemy.Repository;

namespace RestAspNetUdemy.Business.Implementations
{
	public class BookBusinessImplementation : IBookBusiness
	{
		private readonly IRepository<Book> _repository;
		private readonly BookConverter _converter;

		public BookBusinessImplementation(IRepository<Book> repository)
		{
			_repository = repository;
			_converter = new BookConverter();
		}

		public List<BookVO> FindAll()
		{
			var convertedFindAll = _converter.Parse(_repository.FindAll());

			return convertedFindAll;
		}

		public BookVO FindById(int id)
		{
			var convertedFindById = _converter.Parse(_repository.FindById(id));

			return convertedFindById;
		}

		public BookVO Create(BookVO book)
		{
			var bookEntity = _converter.Parse(book);
			bookEntity = _repository.Create(bookEntity);

			var convertedCreate = _converter.Parse(bookEntity);

			return convertedCreate;
		}

		public BookVO Update(BookVO book)
		{
			var bookEntity = _converter.Parse(book);
			bookEntity = _repository.Update(bookEntity);

			var convertedUpdate = _converter.Parse(bookEntity);

			return convertedUpdate;
		}

		public void Delete(int id)
		{
			_repository.Delete(id);
		}
	}
}
