using DeviceBrowser.Models;

namespace DeviceBrowser.Services;

public sealed class SimulatedDeviceService : IDeviceService
{
    private const string OfflineError =
        "Unable to retrieve details because the device is offline.";

    private static readonly IReadOnlyList<Device> Devices =
        Array.AsReadOnly(
            new[]
            {
                new Device("slow-device", "Slow Device"),
                new Device("fast-device", "Fast Device"),
                new Device("standard-device", "Standard Device"),
                new Device("offline-device", "Offline Device"),
            });

    private static readonly IReadOnlyDictionary<string, int> Delays =
        new Dictionary<string, int>(StringComparer.Ordinal)
        {
            ["slow-device"] = 2_000,
            ["fast-device"] = 250,
            ["standard-device"] = 750,
            ["offline-device"] = 1_200,
        };

    private static readonly IReadOnlyDictionary<string, DeviceDetails> Details =
        new Dictionary<string, DeviceDetails>(StringComparer.Ordinal)
        {
            ["slow-device"] = new DeviceDetails(
                "slow-device",
                "Slow Device",
                "Model S-200",
                "SLOW-001",
                "Online",
                "192.168.10.20"),
            ["fast-device"] = new DeviceDetails(
                "fast-device",
                "Fast Device",
                "Model F-100",
                "FAST-001",
                "Online",
                "192.168.10.21"),
            ["standard-device"] = new DeviceDetails(
                "standard-device",
                "Standard Device",
                "Model D-500",
                "STD-001",
                "Online",
                "192.168.10.22"),
        };

    public IReadOnlyList<Device> GetDevices()
    {
        return Devices;
    }

    public async Task<DeviceDetails> GetDeviceDetailsAsync(
        string deviceId,
        CancellationToken cancellationToken)
    {
        if (!Delays.TryGetValue(deviceId, out var delay))
        {
            throw new KeyNotFoundException(
                $"No simulated device exists with ID '{deviceId}'.");
        }

        await Task.Delay(delay, cancellationToken);

        if (string.Equals(
                deviceId,
                "offline-device",
                StringComparison.Ordinal))
        {
            throw new DeviceServiceException(OfflineError);
        }

        return Details[deviceId];
    }
}
