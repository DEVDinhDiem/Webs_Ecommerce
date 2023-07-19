namespace Ecommerce.ViewModels.Catalog.Common
{
	public class PagedResult<T>
    {
        public List<T> Items { set; get; }
        public int TotalRecord { set; get; }
    }
}
