'--------------------------------------------------------------------------------------------------
' StarfieldSS: AppSettings.vb: App Settings class
'    © 2026 Remus Rigo
'       v1.0.20261024
'--------------------------------------------------------------------------------------------------

Imports Microsoft.Win32

Public Class AppSettings

   Private Const regAppPath As String = "Software\Remus RIGO\StarfieldSS.VB-NET"

   Public Property UseTrails As Boolean = True
   Public Property Speed As Integer = 4         ' Z units per tick
   Public Property StarCount As Integer = 200

   '-----------------------------------------------------------------------------------------------
   ' Load Settings
   Public Shared Function Load() As AppSettings
      Dim cfg As New AppSettings
      Try
         Using key As RegistryKey = Registry.CurrentUser.OpenSubKey(regAppPath)
            If key IsNot Nothing Then
               cfg.UseTrails = CInt(key.GetValue("UseTrails", 1)) <> 0
               cfg.Speed = CheckInterval(CInt(key.GetValue("Speed", 4)), 1, 10)
               cfg.StarCount = CheckInterval(CInt(key.GetValue("StarCount", 200)), 50, 1000)
            End If
         End Using
      Catch
         ' Corrupt or inaccessible registry: keep the defaults
      End Try
      Return cfg
   End Function

   '-----------------------------------------------------------------------------------------------
   ' Save Settings
   Public Sub Save()
      Using key As RegistryKey = Registry.CurrentUser.CreateSubKey(regAppPath)
         key.SetValue("UseTrails", If(UseTrails, 1, 0), RegistryValueKind.DWord)
         key.SetValue("Speed", Speed, RegistryValueKind.DWord)
         key.SetValue("StarCount", StarCount, RegistryValueKind.DWord)
      End Using
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' CheckInterval
   Private Shared Function CheckInterval(value As Integer, min As Integer, max As Integer) As Integer
      Return Math.Max(min, Math.Min(max, value))
   End Function

End Class