using DeviceBrowser.Models;

namespace DeviceBrowser.Services;

public interface IDeviceService
{
    IReadOnlyList<Device> GetDevices();

    Task<DeviceDetails> GetDeviceDetailsAsync(
        string deviceId,
        CancellationToken cancellationToken);
}

