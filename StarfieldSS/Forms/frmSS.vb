'--------------------------------------------------------------------------------------------------
' StarfieldSS: frmSS.vb: ScreenSaver/Preview form
'    © 2026 Remus Rigo
'       v1.0.20261024
'--------------------------------------------------------------------------------------------------

Imports StarfieldSS.API

Public Class frmSS

   Private cfg As AppSettings = AppSettings.Load()
   Public PreviewHandle As IntPtr = IntPtr.Zero
   Private isPreview As Boolean = False
   Private focal As Single = FocalLength   ' scaled down in preview mode

   <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
   Public Property TargetBounds As Rectangle

   Private lastMousePos As Point

   Private ReadOnly stars As New List(Of Star)
   Private Const MaxDepth As Single = 1000.0F
   Private Const FocalLength As Single = 256.0F
   Private Const TrailLength As Single = 3.0F     ' trail = this many ticks of movement

   Private ReadOnly rnd As New Random()

   Private WithEvents tmr As New Timer With {.Interval = 20}

   '-----------------------------------------------------------------------------------------------
   ' Initialize ScreenSaver
   Private Sub InitializeSS()
      Me.BackColor = Color.Black
      Me.FormBorderStyle = FormBorderStyle.None
      'Me.WindowState = FormWindowState.Maximized
      Me.ShowInTaskbar = False
      Me.TopMost = True
      Me.KeyPreview = True
      Me.DoubleBuffered = True

      If PreviewHandle <> IntPtr.Zero Then
         isPreview = True

         Dim r As RECT
         GetClientRect(PreviewHandle, r)

         Me.WindowState = FormWindowState.Normal
         Me.TopMost = False
         Me.ShowInTaskbar = False

         SetParent(Me.Handle, PreviewHandle)
         Dim style As Integer = GetWindowLong(Me.Handle, GWL_STYLE)
         SetWindowLong(Me.Handle, GWL_STYLE, (style And Not WS_POPUP) Or WS_CHILD)

         Me.Bounds = New Rectangle(0, 0, r.Right - r.Left, r.Bottom - r.Top)

         ' Keep the same proportions as full screen
         focal = FocalLength * Me.ClientSize.Width / Screen.PrimaryScreen.Bounds.Width
      Else
         lastMousePos = Cursor.Position
         Cursor.Hide()
         If Not TargetBounds.IsEmpty Then Me.Bounds = TargetBounds
      End If
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Terminate ScreenSaver
   Private Sub TerminateSS()
      tmr.Stop()
      If Not isPreview Then Cursor.Show()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Reset Star
   Private Sub ResetStar(s As Star, randomDepth As Boolean)
      s.X = CSng(rnd.NextDouble() * 2 - 1) * MaxDepth
      s.Y = CSng(rnd.NextDouble() * 2 - 1) * MaxDepth
      If randomDepth Then
         s.Z = CSng(rnd.NextDouble() * (MaxDepth - 1)) + 1
      Else
         s.Z = MaxDepth
      End If
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Initialize Star
   Private Sub InitStars()
      stars.Clear()
      Dim count As Integer = If(isPreview, Math.Min(cfg.StarCount, 60), cfg.StarCount)
      For i As Integer = 1 To count
         Dim s As New Star
         ResetStar(s, True)
         stars.Add(s)
      Next
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' tmr: OnTick
   Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
      If isPreview AndAlso Not IsWindow(PreviewHandle) Then
         Me.Close()
         Return
      End If

      Dim cx As Single = Me.ClientSize.Width / 2.0F
      Dim cy As Single = Me.ClientSize.Height / 2.0F

      For Each s As Star In stars
         s.Z -= cfg.Speed

         If s.Z < 1 Then
            ResetStar(s, False)
         Else
            Dim sx As Single = cx + s.X / s.Z * focal
            Dim sy As Single = cy + s.Y / s.Z * focal

            If sx < 0 OrElse sx >= Me.ClientSize.Width OrElse
                   sy < 0 OrElse sy >= Me.ClientSize.Height Then
               ResetStar(s, False)
            End If
         End If
      Next

      Me.Invalidate()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: OnPaint
   Private Sub frmSS_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
      Dim cx As Single = Me.ClientSize.Width / 2.0F
      Dim cy As Single = Me.ClientSize.Height / 2.0F

      Using br As New SolidBrush(Color.White)
         Using pen As New Pen(Color.White)
            For Each s As Star In stars
               Dim sx As Single = cx + s.X / s.Z * focal
               Dim sy As Single = cy + s.Y / s.Z * focal

               ' Closer stars (small Z) are brighter and bigger
               Dim depth As Single = s.Z / MaxDepth          ' 0 = near, 1 = far
               Dim v As Integer = CInt(255 - depth * 200)
               Dim c As Color = Color.FromArgb(v, v, v)
               Dim size As Integer = 1 + CInt((1 - depth) * 2)   ' 1 to 3 px

               Dim zPrev As Single = s.Z + cfg.Speed * TrailLength
               If cfg.UseTrails AndAlso zPrev <= MaxDepth Then
                  Dim px As Single = cx + s.X / zPrev * focal
                  Dim py As Single = cy + s.Y / zPrev * focal
                  pen.Color = c
                  pen.Width = size
                  e.Graphics.DrawLine(pen, px, py, sx, sy)
               Else
                  br.Color = c
                  e.Graphics.FillRectangle(br, sx, sy, size, size)
               End If
            Next
         End Using
      End Using
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: OnLoad
   Private Sub frmSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      InitializeSS()
      InitStars()
      tmr.Start()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: OnKeyDown & OnMouseDown
   Private Sub frmSS_KeyMouseDown() Handles MyBase.KeyDown, MyBase.MouseDown
      If isPreview Then Return
      Me.Close()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: OnMouseMove
   Private Sub frmSS_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
      If isPreview Then Return
      Dim p As Point = Cursor.Position
      If Math.Abs(p.X - lastMousePos.X) > 10 OrElse Math.Abs(p.Y - lastMousePos.Y) > 10 Then
         Me.Close()
      End If
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: OnFormClosed
   Private Sub frmSS_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
      TerminateSS()
      Application.Exit()   ' needed because Main uses Application.Run() with no main form
   End Sub

End Class