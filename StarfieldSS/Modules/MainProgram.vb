Module MainProgram

    <STAThread()>
    Sub Main(args As String())
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

      Dim mode As String = "/s" ' default: start screensaver
      If args.Length > 0 AndAlso args(0).Length >= 2 Then
            mode = args(0).Substring(0, 2).ToLower()
        End If

        Select Case mode
            Case "/s"   ' run the screensaver
                RunScreensaver()
            Case "/c"   ' configuration
                ShowConfig()
            Case "/p"   ' preview in the Settings dialog - ignored for now
                ' do nothing
        End Select
    End Sub

   Private Sub RunScreensaver()
      'For Each monitor As Screen In Screen.AllScreens
      Dim frm As New frmSS
      frm.Show()
      Application.Run()
   End Sub

   Private Sub ShowConfig()
        Application.Run(New frmCfg)
    End Sub

End Module