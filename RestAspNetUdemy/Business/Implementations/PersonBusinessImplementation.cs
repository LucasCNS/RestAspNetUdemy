using RestAspNetUdemy.Data.Converter.Implementations;
using RestAspNetUdemy.Hypermedia.Utils;
using RestAspNetUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;

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
			return _converter.Parse(_repository.FindById(id));
		}

		public List<PersonVO> FindByName(string firstName, string lastName)
		{
			return _converter.Parse(_repository.FindByName(firstName, lastName));
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

		public PagedSearchVO<PersonVO> FindWithPagedSearch
			(string name, string sortDirection, int pageSize, int page)
		{
			var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
			var size = (pageSize < 1) ? 10 : pageSize;
			var offset = page > 0 ? (page - 1) * pageSize : 0;

			string query =
				@"SELECT 
					* 
				FROM
					person p
				WHERE 1=1";
			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query + $" AND p.first_name LIKE '%{name}%' ";
				query += $"ORDER BY p.first_name {sort} LIMIT {size} OFFSET {offset}";
			}

			var countQuery = 
				@"SELECT COUNT(*)
				FROM 
					person p
				WHERE 1=1";
			if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" AND p.first_name LIKE '%{name}%' ";
			
			var persons = _repository.FindWithPagedSearch(query);
			int totalResults = _repository.GetCount(countQuery);

			return new PagedSearchVO<PersonVO>
			{
				CurrentPage = page,
				List = _converter.Parse(persons),
				PageSize = size,
				SortDirections = sort,
				TotalResults = totalResults,
			};
		}
	}
}

