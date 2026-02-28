using CommunityToolkit.Mvvm.ComponentModel;
using LyuExtensions.Aspects;

namespace FADemo.ViewModels;

[Singleton]
public partial class SettingsPageViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string TestString { get; set; } = string.Empty;
}
