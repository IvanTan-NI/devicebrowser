using DeviceBrowser.Models;
using DeviceBrowser.Services;
using System.Windows;
using System.Windows.Controls;

namespace DeviceBrowser;

public partial class MainWindow : Window
{
    private readonly IDeviceService _deviceService;

    public MainWindow(IDeviceService deviceService)
    {
        ArgumentNullException.ThrowIfNull(deviceService);

        _deviceService = deviceService;

        InitializeComponent();
        DeviceList.ItemsSource = _deviceService.GetDevices();
    }

    private async void DeviceList_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (DeviceList.SelectedItem is not Device device)
        {
            ShowInitialState();
            return;
        }

        ShowLoading();

        try
        {
            var details = await _deviceService.GetDeviceDetailsAsync(
                device.Id,
                CancellationToken.None);

            DisplayDetails(details);
        }
        catch (DeviceServiceException ex)
        {
            ShowError(ex.Message);
        }
        finally
        {
            HideLoading();
        }
    }

    private void ShowInitialState()
    {
        ClearDetails();
        DetailsPanel.Visibility = Visibility.Collapsed;
        InitialInstructionText.Visibility = Visibility.Visible;
        HideError();
        HideLoading();
    }

    private void ShowLoading()
    {
        ClearDetails();
        DetailsPanel.Visibility = Visibility.Collapsed;
        InitialInstructionText.Visibility = Visibility.Collapsed;
        HideError();
        LoadingPanel.Visibility = Visibility.Visible;
    }

    private void DisplayDetails(DeviceDetails details)
    {
        DeviceNameText.Text = details.Name;
        DeviceModelText.Text = details.Model;
        DeviceSerialNumberText.Text = details.SerialNumber;
        DeviceStatusText.Text = details.Status;
        DeviceAddressText.Text = details.Address;

        InitialInstructionText.Visibility = Visibility.Collapsed;
        DetailsPanel.Visibility = Visibility.Visible;
        HideError();
    }

    private void ShowError(string message)
    {
        ClearDetails();
        DetailsPanel.Visibility = Visibility.Collapsed;
        InitialInstructionText.Visibility = Visibility.Collapsed;
        ErrorText.Text = message;
        ErrorBorder.Visibility = Visibility.Visible;
    }

    private void ClearDetails()
    {
        DeviceNameText.Text = string.Empty;
        DeviceModelText.Text = string.Empty;
        DeviceSerialNumberText.Text = string.Empty;
        DeviceStatusText.Text = string.Empty;
        DeviceAddressText.Text = string.Empty;
    }

    private void HideError()
    {
        ErrorText.Text = string.Empty;
        ErrorBorder.Visibility = Visibility.Collapsed;
    }

    private void HideLoading()
    {
        LoadingPanel.Visibility = Visibility.Collapsed;
    }
}

