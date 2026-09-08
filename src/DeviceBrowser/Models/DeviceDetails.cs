namespace DeviceBrowser.Models;

public sealed record DeviceDetails(
    string Id,
    string Name,
    string Model,
    string SerialNumber,
    string Status,
    string Address);

