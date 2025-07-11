using RestAspNetUdemy.Data.Converter.Implementations;
using RestAspNetUdemy.Model;
using RestAspNetUdemy.Repository;

namespace RestAspNetUdemy.Business.Implementations
{
	public class PersonBusinessImplementation : IPersonBusiness
	{
		private readonly IRepository<Person> _repository;

		private readonly PersonConverter _converter;

		public PersonBusinessImplementation(IRepository<Person> repository)
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
			var personEntity = _converter.Parse(person);
			personEntity = _repository.Create(personEntity);

			var covertedCreate = _converter.Parse(personEntity);

			return covertedCreate;
		}
		public PersonVO Update(PersonVO person)
		{
			var personEntity = _converter.Parse(person);
			personEntity = _repository.Update(personEntity);

			var covertedUpdate = _converter.Parse(personEntity);

			return covertedUpdate;
		}

		public void Delete(long id)
		{
			_repository.Delete(id);
		}
	}
}
