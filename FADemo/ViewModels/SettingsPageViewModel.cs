using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.Styling;
using LyuExtensions.Aspects;

namespace FADemo.ViewModels;

[Singleton]
public partial class SettingsPageViewModel : ViewModelBase
{
    private readonly FluentAvaloniaTheme _faTheme;

    public SettingsPageViewModel()
    {
        _faTheme = (Application.Current?.Styles[0] as FluentAvaloniaTheme)!;
        CurrentAppTheme = AppThemes[0];
        
        if (_faTheme?.TryGetResource("SystemAccentColor", null, out var currentColor) == true)
        {
            CustomAccentColor = (Color)currentColor;
        }
    }

    public ThemeVariant[] AppThemes { get; } =
        [
            ThemeVariant.Default,
            ThemeVariant.Light,
            ThemeVariant.Dark,
        ];

    [ObservableProperty] 
    public partial ThemeVariant? CurrentAppTheme { get; set; }

    partial void OnCurrentAppThemeChanged(ThemeVariant? value)
    {
        if (Application.Current != null)
        {
            Application.Current.RequestedThemeVariant = value;
            if (value != ThemeVariant.Default)
            {
                _faTheme.PreferSystemTheme = false;
            }
            else
            {
                _faTheme.PreferSystemTheme = true;
            }
        }
    }

    [ObservableProperty] 
    public partial Color CustomAccentColor { get; set; }

    partial void OnCustomAccentColorChanged(Color value)
    {
        _faTheme.CustomAccentColor = value;
    }
}
