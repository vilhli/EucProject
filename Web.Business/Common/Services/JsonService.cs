using System.Text.Json;
using System.Text.Json.Serialization;
using Web.Business.Common.Interfaces;

namespace Web.Business.Common.Services;
public sealed class JsonService : IJsonService
{
	private readonly JsonSerializerOptions options = new()
	{
		PropertyNameCaseInsensitive = true,
		Converters = { new JsonStringEnumConverter() }
	};

	public T? DeserializeObject<T>(string? json)
	{
		if (string.IsNullOrEmpty(json))
		{
			return default;
		}

		return JsonSerializer.Deserialize<T>(json, options);
	}

	public string SerializeObject<T>(T obj)
	{
		return JsonSerializer.Serialize(obj, options);
	}
}

