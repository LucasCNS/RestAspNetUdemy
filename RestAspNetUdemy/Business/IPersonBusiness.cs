using RestAspNetUdemy.Model;

namespace RestAspNetUdemy.Business
{
	public interface IPersonBusiness
	{
		List<PersonVO> FindAll();
		PersonVO FindById(long id);
		List<PersonVO> FindByName(string firstName, string lastName);
		PersonVO Create(PersonVO person);
		PersonVO Update(PersonVO person);
		PersonVO Disable(long id);
		void Delete(long id);
	}
}
