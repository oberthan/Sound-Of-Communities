using System.Collections.ObjectModel;
using MeshWave.Core.Interfaces;
using MeshWave.Wpf.Mvvm;

namespace MeshWave.Wpf.ViewModels;

public class SyncStatusViewModel : ViewModelBase
{
    private readonly ISynchronizer _synchronizer;

    public SyncStatusViewModel(ISynchronizer synchronizer)
    {
        _synchronizer = synchronizer;
        Peers = new ObservableCollection<string>(_synchronizer.ConnectedPeers);

        _synchronizer.NetworkStatusChanged += () => {
            OnPropertyChanged(nameof(PeerCount));
            OnPropertyChanged(nameof(ActiveDownloads));
        };
    }

    public ObservableCollection<string> Peers { get; }
    public int PeerCount => Peers.Count;
    public int ActiveDownloads => _synchronizer.ActiveDownloads;
    public int ActiveUploads => _synchronizer.ActiveUploads;
}
