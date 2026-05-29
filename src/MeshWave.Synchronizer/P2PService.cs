using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MeshWave.Synchronizer;

public class P2PService
{
    private const int DiscoveryPort = 5555;
    private readonly List<string> _discoveredPeers = new();
    private UdpClient? _udpClient;

    public event Action? PeersChanged;

    public void StartDiscovery()
    {
        _udpClient = new UdpClient(DiscoveryPort);
        Task.Run(async () =>
        {
            while (true)
            {
                var result = await _udpClient.ReceiveAsync();
                var message = Encoding.UTF8.GetString(result.Buffer);
                if (message == "MeshWave-Hello")
                {
                    var peerIp = result.RemoteEndPoint.Address.ToString();
                    if (!_discoveredPeers.Contains(peerIp))
                    {
                        _discoveredPeers.Add(peerIp);
                        PeersChanged?.Invoke();
                    }
                }
            }
        });

        Task.Run(async () =>
        {
            var broadcastIp = new IPEndPoint(IPAddress.Broadcast, DiscoveryPort);
            var data = Encoding.UTF8.GetBytes("MeshWave-Hello");
            while (true)
            {
                await _udpClient.SendAsync(data, data.Length, broadcastIp);
                await Task.Delay(5000);
            }
        });
    }

    public IEnumerable<string> DiscoveredPeers => _discoveredPeers;
}
