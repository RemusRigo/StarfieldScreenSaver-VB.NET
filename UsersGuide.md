# Starfield ScreenSaver: User's Guide



## Downloads & Runtimes

Download the latest release from the [Releases page](https://github.com/RemusRigo/StarfieldScreenSaver-VB.NET/releases).

Each release contains one folder per framework and architecture:

|Folder|Target Framework|Architecture|Required Runtime|
|-|-|-|-|
|`net48 x64`|.NET Framework 4.8|64-bit|Included in Windows 10 (May 2019 Update or later) and Windows 11|
|`net48 x86`|.NET Framework 4.8|32-bit|Included in Windows 10 (May 2019 Update or later) and Windows 11|
|`net8.0 x64`|.NET 8.0|64-bit|[.NET 8.0 Desktop Runtime (x64)](https://dotnet.microsoft.com/download/dotnet/8.0)|
|`net8.0 x86`|.NET 8.0|32-bit|[.NET 8.0 Desktop Runtime (x86)](https://dotnet.microsoft.com/download/dotnet/8.0)|
|`net10.0 x64`|.NET 10.0|64-bit|[.NET 10.0 Desktop Runtime (x64)](https://dotnet.microsoft.com/download/dotnet/10.0)|
|`net10.0 x86`|.NET 10.0|32-bit|[.NET 10.0 Desktop Runtime (x86)](https://dotnet.microsoft.com/download/dotnet/10.0)|

> **Which one do I need?** On 64-bit Windows 10/11, use **`net48 x64`**: it needs nothing else installed.
> If you prefer a .NET 8.0 or 10.0 build, pick the version whose **Desktop Runtime** you already have
> installed, or install one from the links above.
> The plain ".NET Runtime" is not enough; these builds need the **Desktop** Runtime.

To check which .NET runtimes are installed, open a command prompt and run:

```
dotnet --list-runtimes
```

Look for a `Microsoft.WindowsDesktop.App 8.x` or `10.x` line.



## Install

1. Download and extract the folder that matches your system (see the table above), e.g. `net48 x64`.
2. Copy the **whole folder** to a permanent location, for example:
   `D:\My Apps\Starfield ScreenSaver\`
   The folder must contain these files; the screensaver won't start if any of them is missing:
   * `net48` builds:
     * `StarfieldSS.scr`
     * `StarfieldSS.scr.config` (without it, the screensaver still runs, but may look blurry on scaled displays)
   * `net8.0` and `net10.0` builds:
     * `StarfieldSS.scr`
     * `StarfieldSS.dll`
     * `StarfieldSS.deps.json`
     * `StarfieldSS.runtimeconfig.json`
3. Right-click `StarfieldSS.scr` and choose **Install**.
   The Windows **Screen Saver Settings** dialog opens with *StarfieldSS* selected.
4. Set the **Wait** time and click **OK**.

> If Windows blocks the file ("Windows protected your PC"), right-click `StarfieldSS.scr` →
> **Properties** → check **Unblock** → **OK**, then try again.



## Test it

* In **Screen Saver Settings**, the small preview shows the starfield, or
* click **Preview**, or
* double-click `StarfieldSS.scr`.

The starfield fills every monitor, taskbar included.
Press any key, click, or move the mouse on any monitor to exit.



## Settings

In **Screen Saver Settings**, click **Settings...** to open the settings dialog
(or right-click `StarfieldSS.scr` → **Configure**).

|Setting|What it does|
|-|-|
|**Stars**|Number of stars on each monitor: 50 to 1000, in steps of 10. Default: 200|
|**Speed**|How fast the stars move: 1 (slow) to 10 (fast). Default: 4|
|**Use trails**|Checked (default): stars are drawn as streaks; unchecked: as dots|

Click **Ok** to save. Closer stars are drawn brighter and bigger.

To open the **About** dialog, click the icon in the top-left corner of the settings dialog (or press **Alt+Space**)
and choose **About...**.

The settings are stored in the registry, under `HKEY_CURRENT_USER\Software\Remus RIGO\StarfieldSS.VB-NET`.



## Uninstall

1. In **Screen Saver Settings**, choose another screensaver (or *None*) and click **OK**.
2. Delete the folder you copied in step 2 of *Install*.
3. Optional: delete the settings key `HKEY_CURRENT_USER\Software\Remus RIGO\StarfieldSS.VB-NET` with **regedit**.
