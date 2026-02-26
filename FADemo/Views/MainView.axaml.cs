using Avalonia.Controls;
using FluentAvalonia.UI.Controls;

namespace FADemo.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        NavigateTo("HomePage");
        NavView.SelectedItem = NavView.MenuItems[0];
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
