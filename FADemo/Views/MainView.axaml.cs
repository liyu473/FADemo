using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using FADemo.ViewModels;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Navigation;
using FluentAvalonia.UI.Windowing;
using LyuExtensions.Aspects;
using System;
using System.Threading.Tasks;

namespace FADemo.Views;

[Transient]
public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = App.GetService<MainViewModel>();

        FrameView.Navigated += OnFrameViewNavigated;
        NavView.BackRequested += OnNavigationViewBackRequested;
        NavigateTo("HomePage");
        NavView.SelectedItem = NavView.MenuItems[0];
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (VisualRoot is AppWindow aw)
        {
            TitleBarHost.ColumnDefinitions[3].Width = new GridLength(aw.TitleBar.RightInset, GridUnitType.Pixel);
        }
    }

    private void OnNavigationViewBackRequested(object? sender, NavigationViewBackRequestedEventArgs e)
    {
        FrameView.GoBack();
    }

    private void OnFrameViewNavigated(object? sender, NavigationEventArgs e)
    {
        if (FrameView.BackStackDepth > 0 && !NavView.IsBackButtonVisible)
        {
            _ = AnimateTitleForBackButton(show: true);
        }
        else if (FrameView.BackStackDepth == 0 && NavView.IsBackButtonVisible)
        {
            _ = AnimateTitleForBackButton(show: false);
        }
    }

    /// <summary>
    /// 导航控件可以返回时，创建过渡动画
    /// </summary>
    /// <param name="show"></param>
    /// <returns></returns>
    private async Task AnimateTitleForBackButton(bool show)
    {
        var from = show ? new Thickness(12, 8, 12, 8) : new Thickness(48, 8, 12, 8);
        var to = show ? new Thickness(48, 8, 12, 8) : new Thickness(12, 8, 12, 8);

        if (!show)
            NavView.IsBackButtonVisible = false;

        var ani = new Animation
        {
            Duration = TimeSpan.FromMilliseconds(220),
            FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0d),
                    Setters = { new Setter(MarginProperty, from) }
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    KeySpline = new KeySpline(0, 0, 0, 1),
                    Setters = { new Setter(MarginProperty, to) }
                }
            }
        };

        await ani.RunAsync(WindowIcon);

        if (show)
            NavView.IsBackButtonVisible = true;
    }

    private void OnNavViewItemInvoked(object? sender, NavigationViewItemInvokedEventArgs e)
    {
        if (e.IsSettingsInvoked)
        {
            NavigateTo("SettingsPage");
            return;
        }

        if (e.InvokedItemContainer is NavigationViewItem nvi && nvi.Tag is string tag)
        {
            NavigateTo(tag);
        }
    }

    private void NavigateTo(string tag)
    {
        switch (tag)
        {
            case "HomePage":
                FrameView.Navigate(typeof(HomePage));
                break;
            case "SettingsPage":
                FrameView.Navigate(typeof(SettingsPage));
                break;
        }
    }
}
