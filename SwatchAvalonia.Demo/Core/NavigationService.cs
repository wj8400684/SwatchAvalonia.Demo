using System;
using Avalonia.Controls;
using SwatchAvalonia.Navigation.Controls;
using SwatchAvalonia.Navigation.Media.Animation;

namespace SwatchAvalonia.Demo.Core;

public class NavigationService
{
    public static NavigationService Instance { get; } = new NavigationService();

    public Control? PreviousPage { get; set; }

    public void SetFrame(Frame f)
    {
        _frame = f;
    }

    public void SetOverlayHost(Panel p)
    {
        _overlayHost = p;
    }

    public void Navigate(Type t)
    {
        _frame.Navigate(t);
    }

    public void NavigateFromContext(object dataContext, NavigationTransitionInfo transitionInfo = null)
    {
        _frame.NavigateFromObject(dataContext,
            new FrameNavigationOptions
            {
                IsNavigationStackEnabled = true,
                TransitionInfoOverride = transitionInfo ?? new SuppressNavigationTransitionInfo()
            });
    }

    public void ShowControlDefinitionOverlay(Type targetType)
    {
        if (_overlayHost != null)
        {
            // (_overlayHost.Children[0] as ControlDefinitionOverlay).TargetType = targetType;
            // (_overlayHost.Children[0] as ControlDefinitionOverlay).Show();
        }
    }

    public void ClearOverlay()
    {
        _overlayHost?.Children.Clear();

    }

    private Frame? _frame;
    private Panel? _overlayHost;
}