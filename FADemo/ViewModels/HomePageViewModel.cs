using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FADemo.Helpers;
using LyuExtensions.Aspects;

namespace FADemo.ViewModels;

[Singleton]
public partial class HomePageViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string TestString { get; set; } = string.Empty;

    [RelayCommand]
    public async Task MesboxTest()
    {
        MessageBoxHelper.Show($"{await MessageBoxHelper.ShowAsync("这是一个消息框")}");
    }
}
