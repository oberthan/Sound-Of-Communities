using System;

namespace MeshWave.Core.Configuration;

public class AppConfig
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = "New Artist";
    public string StorageRootPath { get; set; } = string.Empty;

    public string GetUserStoragePath()
    {
        return System.IO.Path.Combine(StorageRootPath, $"User_{UserId}");
    }
}
