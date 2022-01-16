using System.Text.Json;

namespace Server.Config.Serialization;

public class Json : ISerializer
{
    public void Serialize<T>(Stream stream, T obj)
    {
        JsonSerializer.Serialize(stream, obj);
    }

    public Task SerializeAsync<T>(Stream stream, T obj)
    {
        return JsonSerializer.SerializeAsync(stream, obj);
    }

    public T? Deserialize<T>(Stream stream)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(stream, Options);
        }
        catch (JsonException)
        {
            return default(T);
        }
    }

    public async Task<T?> DeserializeAsync<T>(Stream stream)
    {
        try
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, Options);
        }
        catch (JsonException)
        {
            return default(T);
        }
    }

    private JsonSerializerOptions Options => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };
}
