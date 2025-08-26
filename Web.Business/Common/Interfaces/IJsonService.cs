namespace Web.Business.Common.Interfaces;
public interface IJsonService
{
	T? DeserializeObject<T>(string? json);
	string SerializeObject<T>(T obj);
}
