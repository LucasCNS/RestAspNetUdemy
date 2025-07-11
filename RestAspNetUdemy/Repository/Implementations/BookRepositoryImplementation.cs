using RestAspNetUdemy.Model;
using RestAspNetUdemy.Model.Context;

namespace RestAspNetUdemy.Repository.Implementations
{
	public class BookRepositoryImplementation : IBookRepository
	{
		private MySQLContext _context;

		public BookRepositoryImplementation(MySQLContext context)
		{
			_context = context;
		}

		public Book Create(Book book)
		{
			try
			{
				_context.Add(book);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{

				throw;
			}

			return book;
		}

		public void Delete(int id)
		{
			var result = _context.Books.SingleOrDefault(book => book.Id.Equals(id));

			if (result != null)
			{
				try
				{
					_context.Books.Remove(result);
					_context.SaveChanges();
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

		public List<Book> FindAll()
		{
			return _context.Books.ToList();
		}

		public Book FindById(int id)
		{
			return _context.Books.SingleOrDefault(book => book.Id.Equals(id));
		}

		public Book Update(Book book)
		{
			if (!Exists(book.Id)) return new Book();	
			
			var result = _context.Books.SingleOrDefault(b => b.Id.Equals(book.Id));
			
			if (result != null) 
			{
				try
				{
					_context.Entry(result).CurrentValues.SetValues(book);
					_context.SaveChanges();
				}
				catch (Exception)
				{

					throw;
				}
			}

			return book;
		}

		public bool Exists(int id)
		{
			return _context.People.Any(book => book.Id.Equals(id));
		}
	}
}
