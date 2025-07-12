using Microsoft.AspNetCore.Mvc.Filters;

namespace RestAspNetUdemy.Hypermedia.Abstract
{
	public interface IResponseEnricher
	{
		bool CanEnrich(ResultExecutedContext context);

		Task Enrich(ResultExecutedContext context);
	}
}
