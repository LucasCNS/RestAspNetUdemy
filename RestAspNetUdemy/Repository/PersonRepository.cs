using RestAspNetUdemy.Model;
using RestAspNetUdemy.Model.Context;
using RestAspNetUdemy.Repository.Generic;
using RestWithASPNETUdemy.Repository;

namespace RestAspNetUdemy.Repository
{
	public class PersonRepository : GenericRepository<Person>, IPersonRepository
	{
		public PersonRepository(MySQLContext context) : base(context) { }

		public Person? Disable(long id)
		{
			if (!_context.People.Any(p => p.Id.Equals(id))) return null;
			var user = _context.People.SingleOrDefault(p => p.Id.Equals(id));

			if (user != null)
			{
				user.Enabled = false;
				try
				{
					_context.Entry(user).CurrentValues.SetValues(user);
					_context.SaveChanges();
				}
				catch(Exception ex)
				{
					throw;
				}
			}
			return user;
		}
	}
}
