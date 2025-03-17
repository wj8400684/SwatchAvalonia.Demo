using System.Threading;
using Avalonia;
using Avalonia.Animation;
using SwatchAvalonia.Navigation.Media.Animation;

namespace SwatchAvalonia.Demo.Core;

public class SuppressNavigationTransitionInfo: NavigationTransitionInfo
{
    public override void RunAnimation(Animatable ctrl, CancellationToken cancellationToken)
    {
        //Do nothing
        (ctrl as Visual).Opacity = 1;
    }
}