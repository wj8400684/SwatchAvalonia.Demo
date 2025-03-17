using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Styling;
using SwatchAvalonia.Demo.Core;
using SwatchAvalonia.Demo.ViewModels;
using SwatchAvalonia.Navigation.Controls;
using SwatchAvalonia.Navigation.Core;
using SwatchAvalonia.Navigation.Media.Animation;

namespace SwatchAvalonia.Demo.Views;

public partial class MainView : UserControl
{
    private MainWindow? _mainWindow;
    
    public MainView()
    {
        InitializeComponent();
    }
        
    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _mainWindow?.BeginMoveDrag(e);
    }
    
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime app) return;
        if (app.MainWindow is not MainWindow w)
            return;

        _mainWindow = w;
        
        var vm = new MainViewModel();
        FrameView.NavigationPageFactory = vm.NavigationFactory;
        DataContext = vm;
        NavigationService.Instance.SetFrame(FrameView);
        InitializeNavigationPages();

        FrameView.Navigated += OnFrameViewNavigated;
        NavView.ItemInvoked += OnNavigationViewItemInvoked;
        NavView.BackRequested += OnNavigationViewBackRequested;
    }

    private void OnNavigationViewItemInvoked(object sender, NavigationViewItemInvokedEventArgs e)
    {
        // Change the current selected item back to normal
        // SetNVIIcon(sender as NavigationViewItem, false);

        if (e.InvokedItemContainer is NavigationViewItem nvi)
        {
            NavigationTransitionInfo info;

            // Keep the frame navigation when not using connected animation but suppress it
            // if we have a connected animation binding two pages
            // if (FrameView.Content is ControlsPageBase cpb &&
            //     ((cpb.TargetType == null && nvi.Tag is CoreControlsPageViewModel) ||
            //      (cpb.TargetType != null && nvi.Tag is FAControlsOverviewPageViewModel)))
            // {
            //     info = new SuppressNavigationTransitionInfo();
            // }
            // else
            // {
            //   
            // }
            info = e.RecommendedNavigationTransitionInfo;
            NavigationService.Instance.NavigateFromContext(nvi.Tag, info);
        }
    }

    
    private void OnNavigationViewBackRequested(object sender, NavigationViewBackRequestedEventArgs e)
    {
        FrameView.GoBack();
    }
    
    private void InitializeNavigationPages()
    {
        var mainPages = new MainPageViewModelBase[]
        {
            new HomeViewModel
            {
                NavHeader = "Home",
                IconKey = "HomeIcon"
            },
            new PlayViewModel
            {
                NavHeader = "Play",
                IconKey = "PlayIcon"
            },
            new SettingViewModel
            {
                NavHeader = "Settings",
                IconKey = "SettingsIcon",
                ShowsInFooter = true
            }
        };

        var menuItems = new List<NavigationViewItemBase>(1);
        var footerItems = new List<NavigationViewItemBase>(1);

        bool inDesign = Design.IsDesignMode;

        for (int i = 0; i < mainPages.Length; i++)
        {
            var pg = mainPages[i];
            var nvi = new NavigationViewItem
            {
                Content = pg.NavHeader,
                Tag = pg,
                IconSource = (IconSource)this.FindResource(pg.IconKey)
            };

            //ToolTip.SetTip(nvi, pg.NavHeader);

            nvi.Classes.Add("SampleAppNav");

            if (pg.ShowsInFooter.HasValue && pg.ShowsInFooter.Value)
                footerItems.Add(nvi);
            else
                menuItems.Add(nvi);
        }

        NavView.MenuItemsSource = menuItems;
        NavView.FooterMenuItemsSource = footerItems;

        NavView.Classes.Add("SampleAppNav");

        FrameView.NavigateFromObject((NavView.MenuItemsSource.ElementAt(0) as Control).Tag);
    }

    private void OnFrameViewNavigated(object sender, NavigationEventArgs e)
    {
        var page = e.Content as Control;
        var dc = page.DataContext;

        MainPageViewModelBase mainPage = null;

        if (dc is MainPageViewModelBase mpvmb)
        {
            mainPage = mpvmb;
        }
        else
        {
        }

        foreach (NavigationViewItem nvi in NavView.MenuItemsSource)
        {
            if (nvi.Tag == mainPage)
            {
                NavView.SelectedItem = nvi;
                SetNVIIcon(nvi, true);
            }
            else
            {
                SetNVIIcon(nvi, false);
            }
        }

        foreach (NavigationViewItem nvi in NavView.FooterMenuItemsSource)
        {
            if (nvi.Tag == mainPage)
            {
                NavView.SelectedItem = nvi;
                SetNVIIcon(nvi, true);
            }
            else
            {
                SetNVIIcon(nvi, false);
            }
        }

        // if (FrameView.BackStackDepth > 0 && !NavView.IsBackButtonVisible)
        // {
        //     AnimateContentForBackButton(true);
        // }
        // else if (FrameView.BackStackDepth == 0 && NavView.IsBackButtonVisible)
        // {
        //     AnimateContentForBackButton(false);
        // }
    }

    private void SetNVIIcon(NavigationViewItem? item, bool selected)
    {
        // Technically, yes you could set up binding and converters and whatnot to let the icon change
        // between filled and unfilled based on selection, but this is so much simpler 

        if (item == null)
            return;

        var t = item.Tag;

        if (t is HomeViewModel)
        {
            item.IconSource = this.TryFindResource(selected ? "HomeIconFilled" : "HomeIcon", out var value)
                ? (IconSource)value
                : null;
        }
        if (t is PlayViewModel)
        {
            item.IconSource = this.TryFindResource(selected ? "PlayIconFilled" : "PlayIcon", out var value)
                ? (IconSource)value
                : null;
        }
        else if (t is SettingViewModel)
        {
            item.IconSource = this.TryFindResource(selected ? "SettingsIconFilled" : "SettingsIcon", out var value)
                ? (IconSource)value
                : null;
        }
    }

}