# ToDo

Project review, 2026-10-24. The build is clean (0 errors, 0 warnings); the items below are logic, behavior and cleanup issues.



## Bugs / logic errors

- [ ] **The settings are never used by the screensaver.** `StarfieldSS/Forms/frmSS.vb:4`: the `cfg` field is declared but never loaded. `StarCount`, `Speed` and `UseTrails` are fixed constants (`frmSS.vb:9-13`), so changing "Use trails" in the config dialog does nothing. The defaults don't match either: `AppSettings.Speed` is 4 (`AppSettings.vb:8`), the constant is 2.0.
- [ ] **`Build.bat` fails on the net8.0 builds.** Lines 9-10 use `-f net8.0-windows`, but the project only targets `net10.0-windows` (`<TargetFrameworks>` is commented out in `StarfieldSS.vbproj:11-12`). The restore has no net8 assets (NETSDK1005-type error).
- [ ] **`Application.myapp` doesn't match its generated file.** `Application.myapp:4` says `<MainForm>Form1</MainForm>` and `<MySubMain>true</MySubMain>`, while `Application.Designer.vb:31` uses `frmSS`. If Visual Studio regenerates the designer file, it writes `Me.MainForm = Form1` and the build breaks. The app starts through `MainProgram` (custom Sub Main), so the application framework isn't used: turn off "Enable application framework" or fix `MainForm`.



## Behavior issues

- [ ] **No arguments starts the screensaver** (`MainProgram.vb:8`). Under Windows' screensaver rules, no arguments usually means showing the settings dialog. The user's guide says double-clicking starts it, so this may be intended.
- [ ] **Only the primary monitor is covered** (`MainProgram.vb:24`, the loop over monitors is commented out). The user's guide says it "fills every monitor".
- [ ] **The config dialog only shows "Use trails".** `Speed` and `StarCount` exist in `AppSettings` but have no controls.
- [ ] **`SaveSettings()`'s return value is ignored** (`frmCfg.vb:31`). `DialogResult = OK` is set even when saving fails.
- [ ] **No DPI mode is set.** Without the application framework nothing calls `Application.SetHighDpiMode`, so on scaled displays the form may be bitmap-stretched and blurry.
- [ ] **The output isn't renamed to `.scr`.** The user's guide refers to the `.scr` file, but the build only produces an `.exe`.



## Leftovers / cleanup

- [ ] `frmCfg.vb:1,5`: `Imports Microsoft.Win32` and the public `regAppPath` field are unused and duplicate the constant in `AppSettings`.
- [ ] `AppSettings.vb:5` declares `regAppPath`, but lines 14 and 28 use `RegAppPath`. VB ignores case, so it works, but the names should match.
- [ ] `Build.bat:16` copies `..\..\JSON\ScreenSaver Messages\Messages`, and `UsersGuide.md` is entirely about the Terminal Messages screensaver. Both are left over from that project.
- [ ] Repository names don't match: the README badge points to `StarfieldScreenSaver-VB.NET`, but `Upload.ps1` builds the remote URL from the folder name (`StarfieldSS-VB.NET`).
- [ ] `StarfieldSS.slnx` declares an `Any CPU` solution platform, but the project only defines x64 and x86.
- [ ] The `Timer` in `frmSS` is never disposed. Minor: the app exits when the form closes.
