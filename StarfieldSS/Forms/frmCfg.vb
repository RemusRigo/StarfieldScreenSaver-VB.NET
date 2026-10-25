'--------------------------------------------------------------------------------------------------
' StarfieldSS: frmCfg.vb: Settings form
'    © 2026 Remus Rigo
'       v1.0.20261024
'--------------------------------------------------------------------------------------------------

Imports StarfieldSS.API

Public Class frmCfg

   Private Const SYSMENU_ABOUT_ID As UInteger = 1000

   '-----------------------------------------------------------------------------------------------
   ' create menu
   Protected Overrides Sub OnHandleCreated(e As EventArgs)
      MyBase.OnHandleCreated(e)
      Dim hSysMenu As IntPtr = GetSystemMenu(Me.Handle, False)
      ' Add a separator and then your custom item
      AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty)
      AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "About...")
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' append menu to app system menu
   Protected Overrides Sub WndProc(ByRef m As Message)
      MyBase.WndProc(m)
      If m.Msg = WM_SYSCOMMAND Then
         If CUInt(m.WParam) = SYSMENU_ABOUT_ID Then
            frmAbout.ShowDialog()
         End If
      End If
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Save Settings
   Private Function SaveSettings() As Boolean
      Try
         Dim cfg As AppSettings = AppSettings.Load()
         cfg.StarCount = CInt(numUpDownStars.Value)
         cfg.Speed = tbSpeed.Value
         cfg.UseTrails = chkBoxTrails.Checked
         cfg.Save()
         Return True
      Catch ex As Exception
         MessageBox.Show($"Error occurred while saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
         Return False
      End Try
   End Function

   '-----------------------------------------------------------------------------------------------
   ' Load Settings
   Private Sub LoadSettings()
      Dim cfg As AppSettings = AppSettings.Load()
      numUpDownStars.Value = cfg.StarCount
      tbSpeed.Value = cfg.Speed
      lblSpeedValue.Text = cfg.Speed.ToString
      chkBoxTrails.Checked = cfg.UseTrails
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmCfg: onLoad
   Private Sub frmCfg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      LoadSettings()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' btnOk: onClick
   Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
      If SaveSettings() Then Me.Close()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' tbSpeed: onScroll
   Private Sub tbSpeed_Scroll(sender As Object, e As EventArgs) Handles tbSpeed.Scroll
      lblSpeedValue.Text = tbSpeed.Value.ToString
   End Sub
End Class