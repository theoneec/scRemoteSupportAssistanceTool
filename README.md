# scRemoteSupportAssistanceTool

# scRemoteSupportAssistanceTool

A lightweight C# WinForms tool built with .NET 8, designed to act as a WebView2 wrapper for a ScreenConnect support page. It automatically handles downloads and executes installation packages for remote support tools.

---

## 🛠 Requirements

- **Visual Studio** (2022 or later)
- **.NET 8 SDK**  
  📥 Download: [https://dotnet.microsoft.com/en-us/download/dotnet/8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

---

## 🧑‍💻 Setup Instructions

1. Clone this repository or download the source.
2. Open the project in **Visual Studio**.
3. Edit the `Form1.cs` file:

   Locate the following line:

   ```csharp
   webView21.Source = new Uri("http:\\YourScreenConnect.url");
🔁 Replace "http:\\YourScreenConnect.url" with your actual ScreenConnect URL.

🚀 What It Does
Loads your ScreenConnect support site in a WebView2 browser control.

Monitors all file downloads.

If the download is:

A .zip: it extracts it and auto-runs the .exe inside.

A .exe or .msi: it launches it directly.

This simplifies technician-assisted support sessions by automating the installer launch process.

🏗 Build Instructions
You can build and publish a self-contained, single-file executable using the following command:

powershell

dotnet publish "C:\theprojectlocation" `
  -c Release `
  -r win-x64 `
  --self-contained true `
  /p:PublishSingleFile=true `
  /p:IncludeNativeLibrariesForSelfExtract=true `
  /p:EnableCompressionInSingleFile=true `
  /p:PublishReadyToRun=true `
  /p:DebugType=none


📝 Replace "C:\theprojectlocation" with the actual path to your project directory.

📦 Output
The published output will be a single .exe file located in:

python
Copy
Edit
bin\Release\net8.0-windows\win-x64\publish\
This .exe can be distributed directly and run without requiring .NET to be pre-installed.
