
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using Avalonia.Media.Immutable;
using Avalonia.Media;
using Avalonia.VisualTree;
using Ursa.Controls;

namespace SwatchAvalonia.Demo.Views
{
    public partial class MainWindow : UrsaWindow
    {


        public MainWindow()
        {
            InitializeComponent();
            IDisposable? topLevelBackgroundSideSetter = null, sideBarBackgroundSetter = null, paneBackgroundSetter = null;

            topLevelBackgroundSideSetter?.Dispose();
            sideBarBackgroundSetter?.Dispose();
            paneBackgroundSetter?.Dispose();

            var topLevel = (TopLevel)this.GetVisualRoot()!;
            topLevel.TransparencyLevelHint = [WindowTransparencyLevel.Mica];

            var transparentBrush = new ImmutableSolidColorBrush(Colors.White, 0);
            var semiTransparentBrush = new ImmutableSolidColorBrush(Colors.Gray, 0.2);
            topLevelBackgroundSideSetter = topLevel.SetValue(BackgroundProperty, transparentBrush, Avalonia.Data.BindingPriority.Style);
            //sideBarBackgroundSetter = sideBar.SetValue(BackgroundProperty, semiTransparentBrush, Avalonia.Data.BindingPriority.Style);
            //paneBackgroundSetter = sideBar.SetValue(SplitView.PaneBackgroundProperty, semiTransparentBrush, Avalonia.Data.BindingPriority.Style);
        }


    }
}