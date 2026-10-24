
Public Class frmSS

   Private cfg As AppSettings

   Private lastMousePos As Point

   Private ReadOnly stars As New List(Of Star)
   Private Const MaxDepth As Single = 1000.0F
   Private Const FocalLength As Single = 256.0F
   Private Const UseTrails As Boolean = True
   Private Const TrailLength As Single = 3.0F     ' trail = this many ticks of movement

   Private ReadOnly rnd As New Random()

   Private WithEvents tmr As New Timer With {.Interval = 20}

   Private Sub InitializeSS()
      Me.BackColor = Color.Black
      Me.FormBorderStyle = FormBorderStyle.None
      Me.WindowState = FormWindowState.Maximized
      Me.ShowInTaskbar = False
      Me.TopMost = True
      Me.KeyPreview = True
      Me.DoubleBuffered = True

      lastMousePos = Cursor.Position
      Cursor.Hide()
      tmr.Start()
   End Sub

   Private Sub TerminateSS()
      tmr.Stop()
      Cursor.Show()
   End Sub

   Private Sub ResetStar(s As Star, randomDepth As Boolean)
      s.X = CSng(rnd.NextDouble() * 2 - 1) * MaxDepth
      s.Y = CSng(rnd.NextDouble() * 2 - 1) * MaxDepth
      If randomDepth Then
         s.Z = CSng(rnd.NextDouble() * (MaxDepth - 1)) + 1
      Else
         s.Z = MaxDepth
      End If
   End Sub

   Private Sub InitStars()
      stars.Clear()
      For i As Integer = 1 To cfg.StarCount
         Dim s As New Star
         ResetStar(s, True)
         stars.Add(s)
      Next
   End Sub

   Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
      Dim cx As Single = Me.ClientSize.Width / 2.0F
      Dim cy As Single = Me.ClientSize.Height / 2.0F

      For Each s As Star In stars
         s.Z -= cfg.Speed

         If s.Z < 1 Then
            ResetStar(s, False)
         Else
            Dim sx As Single = cx + s.X / s.Z * FocalLength
            Dim sy As Single = cy + s.Y / s.Z * FocalLength

            If sx < 0 OrElse sx >= Me.ClientSize.Width OrElse
                   sy < 0 OrElse sy >= Me.ClientSize.Height Then
               ResetStar(s, False)
            End If
         End If
      Next

      Me.Invalidate()
   End Sub

   Private Sub frmSS_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
      Dim cx As Single = Me.ClientSize.Width / 2.0F
      Dim cy As Single = Me.ClientSize.Height / 2.0F

      Using br As New SolidBrush(Color.White)
         Using pen As New Pen(Color.White)
            For Each s As Star In stars
               Dim sx As Single = cx + s.X / s.Z * FocalLength
               Dim sy As Single = cy + s.Y / s.Z * FocalLength

               ' Closer stars (small Z) are brighter and bigger
               Dim depth As Single = s.Z / MaxDepth          ' 0 = near, 1 = far
               Dim v As Integer = CInt(255 - depth * 200)
               Dim c As Color = Color.FromArgb(v, v, v)
               Dim size As Integer = 1 + CInt((1 - depth) * 2)   ' 1 to 3 px

               Dim zPrev As Single = s.Z + cfg.Speed * TrailLength
               If cfg.UseTrails AndAlso zPrev <= MaxDepth Then
                  Dim px As Single = cx + s.X / zPrev * FocalLength
                  Dim py As Single = cy + s.Y / zPrev * FocalLength
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

   Private Sub frmSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      InitializeSS()
      InitStars()
   End Sub

   Private Sub frmSS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
      Me.Close()
   End Sub

   Private Sub frmSS_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
      Me.Close()
   End Sub

   Private Sub frmSS_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
      Dim p As Point = Cursor.Position
      If Math.Abs(p.X - lastMousePos.X) > 10 OrElse Math.Abs(p.Y - lastMousePos.Y) > 10 Then
         Me.Close()
      End If
   End Sub

   Private Sub frmSS_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
      TerminateSS()
      Application.Exit()   ' needed because Main uses Application.Run() with no main form
   End Sub

End Class