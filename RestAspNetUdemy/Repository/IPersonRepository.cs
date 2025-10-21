using RestAspNetUdemy.Data.VO;
using RestAspNetUdemy.Model;
using RestAspNetUdemy.Repository;

namespace RestWithASPNETUdemy.Repository
{
	public interface IPersonrRepository : IRepository<Person>
	{
		Person? Diable(long id);
	}
}