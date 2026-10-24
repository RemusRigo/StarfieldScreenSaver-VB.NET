Imports Microsoft.Win32

Public Class frmCfg

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

   Private Sub LoadSettings()
      Dim cfg As AppSettings = AppSettings.Load()
      numUpDownStars.Value = cfg.StarCount
      tbSpeed.Value = cfg.Speed
      lblSpeedValue.Text = cfg.Speed.ToString
      chkBoxTrails.Checked = cfg.UseTrails
   End Sub


   Private Sub frmCfg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      LoadSettings()
   End Sub

   Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
      Me.DialogResult = DialogResult.OK
      SaveSettings()
      Me.Close()
   End Sub

   Private Sub tbSpeed_Scroll(sender As Object, e As EventArgs) Handles tbSpeed.Scroll
      lblSpeedValue.Text = tbSpeed.Value.ToString
   End Sub
End Class