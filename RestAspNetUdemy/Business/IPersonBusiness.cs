using RestAspNetUdemy.Model;

namespace RestAspNetUdemy.Business
{
	public interface IPersonBusiness
	{
		List<PersonVO> FindAll();
		PersonVO FindById(long id);
		PersonVO Create(PersonVO person);
		PersonVO Update(PersonVO person);
		PersonVO Disable(long id);
		void Delete(long id);
	}
}
