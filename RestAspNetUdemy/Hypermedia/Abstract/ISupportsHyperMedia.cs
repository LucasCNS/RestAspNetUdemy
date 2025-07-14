namespace RestAspNetUdemy.Hypermedia.Abstract
{
	public interface ISupportsHyperMedia
	{
		List<HyperMediaLink> Links { get; set; }
	}
}
