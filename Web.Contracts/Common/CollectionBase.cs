namespace Web.Contracts.Common;
public abstract record class CollectionBase<T>
{
	public CollectionBase(IEnumerable<T> data)
	{
		Data = [.. data];
	}

	public CollectionBase()
	{
		Data = [];
	}

	public List<T> Data { get; set; }
}
