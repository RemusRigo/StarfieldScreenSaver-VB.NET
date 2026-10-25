'--------------------------------------------------------------------------------------------------
' StarfieldSS: MainProgram.vb: Main()
'    © 2026 Remus Rigo
'       v1.0.20261024
'--------------------------------------------------------------------------------------------------

Module MainProgram

   '-----------------------------------------------------------------------------------------------
   ' Main()
   <STAThread()>
   Sub Main(args As String())
#If Not NETFRAMEWORK Then
      Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)

      Dim mode As String = "/s" ' default: start screensaver
      If args.Length > 0 AndAlso args(0).Length >= 2 Then
         mode = args(0).Substring(0, 2).ToLower()
      End If

      Select Case mode
         Case "/s" : RunScreensaver()
         Case "/c" : ShowConfig()
         Case "/p"
            Dim h As IntPtr = ParsePreviewHandle(args)
            If h <> IntPtr.Zero Then RunPreview(h)
      End Select
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Start ScreenSaver
   Private Sub RunScreensaver()
      For Each monitor As Screen In Screen.AllScreens
         Dim frm As New frmSS
         frm.StartPosition = FormStartPosition.Manual
         frm.TargetBounds = monitor.Bounds
         frm.Show()
      Next
      Application.Run()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Show Config
   Private Sub ShowConfig()
      Application.Run(New frmCfg)
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ParsePreviewHandle
   Private Function ParsePreviewHandle(args As String()) As IntPtr
      Dim s As String = Nothing
      If args(0).Length > 2 Then
         s = args(0).Substring(2).TrimStart(":"c)
      ElseIf args.Length > 1 Then
         s = args(1)
      End If

      Dim v As Long
      If s IsNot Nothing AndAlso Long.TryParse(s, v) Then
         Return New IntPtr(v)
      End If
      Return IntPtr.Zero
   End Function

   '-----------------------------------------------------------------------------------------------
   ' Run Preview
   Private Sub RunPreview(parentHandle As IntPtr)
      Dim frm As New frmSS
      frm.PreviewHandle = parentHandle
      frm.Show()
      Application.Run()
   End Sub

End Module