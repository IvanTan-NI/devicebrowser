# WPF Device Browser Exercise

## Interview format

- 45 minutes of live coding
- 15 minutes of follow-up discussion after coding
- IDE assistance and official Microsoft documentation are allowed
- Generative AI is not allowed

## Application briefing

You are given a running WPF application called **Device Browser**.

Open `DeviceBrowserInterview\DeviceBrowserInterview.sln` and run the `DeviceBrowser` project. The starter targets .NET 10 for Windows.

The application:

- displays a list of simulated devices;
- loads device details asynchronously when a device is selected;
- displays the selected device's details;
- shows a loading indicator while details are being retrieved;
- displays an error when the documented **Offline Device** is selected.

The current implementation places the device-selection and detail-loading workflow in `MainWindow.xaml.cs`.

## Reported bug

The application can display details that do not match the currently selected device.

### Reproduction steps

1. Select **Slow Device**.
2. Immediately select **Fast Device**.
3. Wait for both loading operations to finish.

### Actual behavior

Fast Device's details appear first. They are later replaced by Slow Device's details even though Fast Device remains selected.

### Expected behavior

The displayed details, loading indicator, and error message must always correspond to the current selection.

How you coordinate overlapping or obsolete loading operations is a design decision. The required outcome is that obsolete work cannot change the current presentation state.

## Assignment

1. Fix the reported bug while keeping the UI responsive.
2. Refactor the affected device-selection and detail-loading workflow using MVVM.
3. Preserve the existing loading indicator behavior.
4. Preserve the documented Offline Device error behavior.

## Constraints

- Do not modify the simulated device service, its configured delays, or its configured failures.
- Do not add an external MVVM framework or dependency injection container.
- You may use the provided `ObservableObject`.
- You do not need to redesign the UI.
- You do not need to convert unrelated application code to MVVM.
- You do not need to add automated tests.

Please explain your observations and design decisions as you work.
