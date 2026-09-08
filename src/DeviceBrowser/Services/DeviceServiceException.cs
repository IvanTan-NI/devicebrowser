namespace DeviceBrowser.Services;

public sealed class DeviceServiceException : Exception
{
    public DeviceServiceException(string message)
        : base(message)
    {
    }
}

