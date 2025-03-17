using System;
using Avalonia.Controls;
using SwatchAvalonia.Demo.ViewModels;
using SwatchAvalonia.Demo.Views;
using SwatchAvalonia.Navigation.Controls;

namespace SwatchAvalonia.Demo.Core;

public sealed class NavigationFactory(MainViewModel owner) : INavigationPageFactory
{
    public MainViewModel Owner { get; } = owner;

    public Control? GetPage(Type srcType)
    {
        return null;
    }

    public Control GetPageFromObject(object target)
    {
        if (target is HomeViewModel)
        {
            return new HomeView
            {
                DataContext = target
            };
        }
        else if (target is SettingViewModel)
        {
            return new PlayView()
            {
                DataContext = target
            };
        }
        else if (target is PlayViewModel)
        {
            return new SettingPage()
            {
                DataContext = target
            };
        }
        else
        {
            throw new NullReferenceException();
        }
    }
}