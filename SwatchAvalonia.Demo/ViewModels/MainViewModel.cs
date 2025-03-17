using SwatchAvalonia.Demo.Core;

namespace SwatchAvalonia.Demo.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        NavigationFactory = new NavigationFactory(this);
    }
    
    public NavigationFactory NavigationFactory { get; }
}
