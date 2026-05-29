using System.Windows.Controls;

namespace MeshWave.Wpf.Views;

public partial class PlayerView : UserControl
{
    public PlayerView()
    {
        InitializeComponent();
    }

    private void Waveform_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (DataContext is ViewModels.PlayerViewModel vm)
        {
            var pos = e.GetPosition(WaveformCanvas);
            var progress = pos.X / WaveformCanvas.ActualWidth;
            // vm.SeekToProgress(progress);
        }
    }
}
