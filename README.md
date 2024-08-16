# Patient Analytics - C# Backend

## What is [.NET](https://dotnet.microsoft.com/en-us/)?
.NET is the free, open-source, cross-platform framework for building modern apps and powerful cloud services.

## How to build cross-platform desktop and mobile app with .NET?
### With [.NET Multi-platform App UI (MAUI)](https://dotnet.microsoft.com/en-us/apps/maui). 
Build native, cross-platform desktop and mobile apps all in one framework.

## Documentation
- [Supported platforms](https://learn.microsoft.com/en-gb/dotnet/maui/supported-platforms?view=net-maui-8.0)
- [Comprehensive guide and documentation](https://learn.microsoft.com/en-gb/dotnet/maui/?view=net-maui-8.0)

## Requirements
### 1: A Compatible Integrated Development Environment (IDE)
A compatible IDE is required for .NET MAUI application development, to build and compile application to run on iOS simulators, Android emulators or physical iOS / Android devices.

Compatible IDEs include the following
- [Microsoft Visual Studio](https://visualstudio.microsoft.com/)
- [JetBrains Rider](https://www.jetbrains.com/rider/)

This application is developed on JetBrains Rider [2024.1.2](https://blog.jetbrains.com/dotnet/2024/05/07/rider-2024-1-2/)

### 2: Patient Analytics - C# Backend is Deployed / Ran Locally
Patient Analytics C# Backend provides Web APIs to allow the mobile application to perform core functionalities and communicate with the backend.

If the backend is ran locally, the `PatientAnalyticsMaui/appsettings.json` file should be configured as follow.

#### For iOS Development
```
{
  "Api": {
    "BaseUrl": "http://localhost:5272"
  },
  "HubConnection": {
    "Route": "/patientHub",
    "Url": "http://localhost:5272/patientHub"
  }
}
```

#### For Android Development
```
{
  "Api": {
    "BaseUrl": "http://10.0.2.2:5272"
  },
  "HubConnection": {
    "Route": "/patientHub",
    "Url": "http://10.0.2.2:5272/patientHub"
  }
}
```

## How to setup this project locally?

### Step 1: Install .NET 7 SDK
https://dotnet.microsoft.com/en-us/download/dotnet/7.0

This project is developed on .NET version 7.0.408.

[How to check if .NET is already installed?](https://learn.microsoft.com/en-us/dotnet/core/install/how-to-detect-installed-versions?pivots=os-windows)

Check the current version of .NET runtime with the following command on the `PatientAnalyticsMaui` directory.

`dotnet --version`

### Step 2: Install .NET WebAssembly Build Tools
Install the .NET WebAssembly build tools with the following command on the `PatientAnalyticsMaui` directory to allow .NET MAUI to conduct ahead-of-time (AOT) compilation.

`dotnet workload install wasm-tools-net6`

### Step 3: Install .NET Maui Workload
Install the .NET Maui workload with the following command on the `PatientAnalyticsMaui` directory to allow .NET MAUI to compile the application to iOS and Android.

`dotnet workload install maui`

### Step 4: Install Dependencies
Install dependencies with the following command on the `PatientAnalyticsMaui` directory.

`dotnet restore`

## How to run this project on an iOS Simulator with Rider?
> Only macOS devices can compile and deploy .NET MAUI application to an iOS simulator or device.

### Step 1: Install XCode
Install [XCode](https://developer.apple.com/xcode/). The iOS Simulator app is accompained with the installation of XCode.

This application is developed on XCode Version [15.2](https://xcodereleases.com/).

### Step 2: Setup an iOS Simulator with XCode
Open XCode and setup simulators within XCode. 
> This application is tested with the iPhone 15 Simulator, running iOS 17.2.

### Step 3: Run App with Simulator
The green play arrow button should be located next to the device and run configuration.

## How to run this project on an Android Emulator with Rider?

### Step 1: Install Rider Android Support 
Install the [Rider Android Support](https://plugins.jetbrains.com/plugin/12056-rider-android-support) plugin to provide necessary Android support.

### Step 2: Setup an Android Emulator with Device Manager
Open Device Manager within Rider. Use the manager to create a new emulator by selecting device model and Android version. 
> This application is tested with the Google Pixel 6 Emulator, running Android 12.0 (API 31).

### Step 3: Run App with Emulator
The green play arrow button should be located next to the device and run configuration.
