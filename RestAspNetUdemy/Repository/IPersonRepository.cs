using RestAspNetUdemy.Model;
using RestAspNetUdemy.Repository;

namespace RestWithASPNETUdemy.Repository
{
	public interface IPersonRepository : IRepository<Person>
	{
		Person Disable(long id);
	}
}