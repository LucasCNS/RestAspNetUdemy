using RestAspNetUdemy.Hypermedia.Abstract;

namespace RestAspNetUdemy.Hypermedia.Filters
{
	public class HyperMediaFilterOptions
	{
		public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
	}
}
