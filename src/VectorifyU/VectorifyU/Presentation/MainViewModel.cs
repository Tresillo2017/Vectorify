using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace VectorifyU.Presentation
{
    public partial class MainViewModel : ObservableObject
    {
        public string? Title { get; }

        [ObservableProperty]
        private string? name;

        public ICommand GoToRectas { get; }
        public ICommand GoToFunciones { get; }

        public MainViewModel(
            INavigator navigator,
            IStringLocalizer localizer)
        {
            _navigator = navigator;
            Title = $"Main - {localizer["ApplicationName"]}";
            GoToRectas = new AsyncRelayCommand(GoToRectasView);
            GoToFunciones = new AsyncRelayCommand(GoToFuncionesView);
        }

        private async Task GoToRectasView()
        {
            await _navigator.NavigateViewModelAsync<RectasViewModel>(this, data: new Entity(Name!));
        }

        private async Task GoToFuncionesView()
        {
            await _navigator.NavigateViewModelAsync<FuncionesViewModel>(this, data: new Entity(Name!));
        }

        private INavigator _navigator;
    }
}