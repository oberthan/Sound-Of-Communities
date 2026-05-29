using System;
using System.IO;
using System.Text.Json;

namespace MeshWave.Core.Configuration;

public class AppConfig
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = "New Artist";
    public string StorageRootPath { get; set; } = string.Empty;

    public string GetUserStoragePath()
    {
        return Path.Combine(StorageRootPath, $"User_{UserId}");
    }

    public void Save()
    {
        var path = GetConfigPath();
        var json = JsonSerializer.Serialize(this);
        File.WriteAllText(path, json);
    }

    public static AppConfig Load()
    {
        var path = GetConfigPath();
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
        }
        return new AppConfig();
    }

    private static string GetConfigPath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var meshWaveData = Path.Combine(appData, "MeshWave");
        if (!Directory.Exists(meshWaveData)) Directory.CreateDirectory(meshWaveData);
        return Path.Combine(meshWaveData, "settings.json");
    }
}
