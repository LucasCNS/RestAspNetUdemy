using Microsoft.AspNetCore.Mvc.Filters;

namespace RestAspNetUdemy.Hypermedia.Abstract
{
	public interface IResponseEnricher
	{
		bool CanEnrich(ResultExecutingContext context);

		Task Enrich(ResultExecutingContext context);
	}
}
