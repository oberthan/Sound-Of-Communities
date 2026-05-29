using System;
using Microsoft.Extensions.DependencyInjection;

namespace MeshWave.Wpf.Services;

public interface INavigationService
{
    object? CurrentViewModel { get; }
    void NavigateTo<TViewModel>() where TViewModel : class;
    event Action? CurrentViewModelChanged;
}

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private object? _currentViewModel;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object? CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            CurrentViewModelChanged?.Invoke();
        }
    }

    public event Action? CurrentViewModelChanged;

    public void NavigateTo<TViewModel>() where TViewModel : class
    {
        CurrentViewModel = _serviceProvider.GetRequiredService<TViewModel>();
    }
}
