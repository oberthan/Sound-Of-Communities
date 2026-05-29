using System;

namespace MeshWave.Wpf.Services;

public interface INavigationService
{
    void NavigateTo<TViewModel>() where TViewModel : class;
    object CurrentViewModel { get; }
    event Action CurrentViewModelChanged;
}

public class NavigationService : INavigationService
{
    private readonly Func<Type, object> _viewModelFactory;
    private object _currentViewModel;

    public NavigationService(Func<Type, object> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public object CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            CurrentViewModelChanged?.Invoke();
        }
    }

    public event Action CurrentViewModelChanged;

    public void NavigateTo<TViewModel>() where TViewModel : class
    {
        CurrentViewModel = _viewModelFactory(typeof(TViewModel));
    }
}
