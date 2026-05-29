using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MeshWave.Wpf.Services;

public interface IWaveformService
{
    Task<List<float>> GetWaveformDataAsync(string filePath, int points);
}

public class WaveformService : IWaveformService
{
    public Task<List<float>> GetWaveformDataAsync(string filePath, int points)
    {
        return Task.Run(() =>
        {
            var data = new List<float>();
            var random = new Random(filePath.GetHashCode());

            // Mock waveform generation since we don't have a full audio decoder here
            // In a real app we'd use NAudio or similar to read samples
            for (int i = 0; i < points; i++)
            {
                data.Add((float)random.NextDouble());
            }
            return data;
        });
    }
}
