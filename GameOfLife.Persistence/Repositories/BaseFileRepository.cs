using Newtonsoft.Json;

namespace GameOfLife.Persistence.Repositories;

public class BaseJsonFileRepository<TObject, TIdentifier>
    where TObject : class
    where TIdentifier : struct
{
    public async Task<TObject> SaveAsync(TIdentifier id, TObject data)
    {
        var json = JsonConvert.SerializeObject(data);
        await File.WriteAllTextAsync(GetFileName(id), json);
        return data;
    }

    public async Task<TObject> GetAsync(TIdentifier id)
    {
        string json = null;
        if (File.Exists(GetFileName(id)))
            json = await File.ReadAllTextAsync(GetFileName(id));

        if (!string.IsNullOrWhiteSpace(json))
            return JsonConvert.DeserializeObject<TObject>(json);

        return default;
    }

    private string GetFileName(TIdentifier id)
    {
        return $"./file-{id}.json";
    }
}