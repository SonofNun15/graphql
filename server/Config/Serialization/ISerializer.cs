namespace Server.Config.Serialization;

public interface ISerializer
{
    void Serialize<T>(Stream stream, T obj);
    Task SerializeAsync<T>(Stream stream, T obj);
    T? Deserialize<T>(Stream stream);
    Task<T?> DeserializeAsync<T>(Stream stream);
}
