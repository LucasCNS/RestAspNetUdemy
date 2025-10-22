using RestAspNetUdemy.Data.Converter.Implementations;
using RestAspNetUdemy.Model;
using RestWithASPNETUdemy.Repository;

namespace RestAspNetUdemy.Business.Implementations
{
	public class PersonBusinessImplementation : IPersonBusiness
	{
		private readonly IPersonRepository _repository;
		private readonly PersonConverter _converter;

		public PersonBusinessImplementation(IPersonRepository repository)
		{
			_repository = repository;
			_converter = new PersonConverter();
		}

		public List<PersonVO> FindAll()
		{
			var convertedFindAll = _converter.Parse(_repository.FindAll());

			return convertedFindAll;
		}

		public PersonVO FindById(long id)
		{
			var convertedFindById = _converter.Parse(_repository.FindById(id));

			return convertedFindById;
		}

		public PersonVO Create(PersonVO person)
		{

			return Convert(person, _repository.Create);
		}

		public PersonVO Update(PersonVO person)
		{
			return Convert(person, _repository.Update);
		}

		public PersonVO Disable(long id)
		{
			var personEntity = _repository.Disable(id);
			return _converter.Parse(personEntity);
		}

		public void Delete(long id)
		{
			_repository.Delete(id);
		}

		private PersonVO Convert(PersonVO person, Func<Person, Person> repositoryAction)
		{
			var personEntity = _converter.Parse(person);
			var resultEntity = repositoryAction(personEntity);

			var convertedMethod = _converter.Parse(personEntity);
			return convertedMethod;
		}
	}
}
