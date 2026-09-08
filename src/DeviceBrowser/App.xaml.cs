using DeviceBrowser.Services;
using System.Windows;

namespace DeviceBrowser;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var deviceService = new SimulatedDeviceService();
        var mainWindow = new MainWindow(deviceService);

        MainWindow = mainWindow;
        mainWindow.Show();
    }
}

