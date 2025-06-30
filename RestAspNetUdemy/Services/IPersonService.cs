using RestAspNetUdemy.Model;

namespace RestAspNetUdemy.Services
{
	public interface IPersonService
	{
		Person Create(Person person);
		Person FindById(long id);
		Person Update(Person person);
		Person Delete(Person person);
		List<Person> FindAll();
	}
}
