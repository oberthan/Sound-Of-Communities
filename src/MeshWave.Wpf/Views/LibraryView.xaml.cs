using System.Windows.Controls;
using System.Windows.Input;
using MeshWave.Core.Models;

namespace MeshWave.Wpf.Views;

public partial class LibraryView : UserControl
{
    public LibraryView()
    {
        InitializeComponent();
    }

    private void PlayItem_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is TextBlock tb && tb.DataContext is Track track)
        {
            if (DataContext is ViewModels.LibraryViewModel vm)
            {
                vm.PlayCommand.Execute(track);
            }
        }
    }
}
