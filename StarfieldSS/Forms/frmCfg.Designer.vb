<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCfg
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      chkBoxTrails = New CheckBox()
      btnOk = New Button()
      lblSpeed = New Label()
      tbSpeed = New TrackBar()
      lblSpeedValue = New Label()
      lblStars = New Label()
      numUpDownStars = New NumericUpDown()
      CType(tbSpeed, ComponentModel.ISupportInitialize).BeginInit()
      CType(numUpDownStars, ComponentModel.ISupportInitialize).BeginInit()
      SuspendLayout()
      ' 
      ' chkBoxTrails
      ' 
      chkBoxTrails.AutoSize = True
      chkBoxTrails.Location = New Point(3, 78)
      chkBoxTrails.Name = "chkBoxTrails"
      chkBoxTrails.Size = New Size(73, 19)
      chkBoxTrails.TabIndex = 0
      chkBoxTrails.Text = "Use trails"
      chkBoxTrails.UseVisualStyleBackColor = True
      ' 
      ' btnOk
      ' 
      btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
      btnOk.Location = New Point(327, 176)
      btnOk.Name = "btnOk"
      btnOk.Size = New Size(34, 23)
      btnOk.TabIndex = 1
      btnOk.Text = "&Ok"
      btnOk.UseVisualStyleBackColor = True
      ' 
      ' lblSpeed
      ' 
      lblSpeed.AutoSize = True
      lblSpeed.Location = New Point(3, 50)
      lblSpeed.Name = "lblSpeed"
      lblSpeed.Size = New Size(39, 15)
      lblSpeed.TabIndex = 2
      lblSpeed.Text = "Speed"
      ' 
      ' tbSpeed
      ' 
      tbSpeed.Location = New Point(95, 40)
      tbSpeed.Minimum = 1
      tbSpeed.Name = "tbSpeed"
      tbSpeed.Size = New Size(169, 45)
      tbSpeed.TabIndex = 3
      tbSpeed.Value = 4
      ' 
      ' lblSpeedValue
      ' 
      lblSpeedValue.AutoSize = True
      lblSpeedValue.Location = New Point(270, 50)
      lblSpeedValue.Name = "lblSpeedValue"
      lblSpeedValue.Size = New Size(13, 15)
      lblSpeedValue.TabIndex = 4
      lblSpeedValue.Text = "4"
      ' 
      ' lblStars
      ' 
      lblStars.AutoSize = True
      lblStars.Location = New Point(3, 19)
      lblStars.Name = "lblStars"
      lblStars.Size = New Size(32, 15)
      lblStars.TabIndex = 5
      lblStars.Text = "Stars"
      ' 
      ' numUpDownStars
      ' 
      numUpDownStars.Increment = New Decimal(New Integer() {10, 0, 0, 0})
      numUpDownStars.Location = New Point(95, 11)
      numUpDownStars.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
      numUpDownStars.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
      numUpDownStars.Name = "numUpDownStars"
      numUpDownStars.Size = New Size(57, 23)
      numUpDownStars.TabIndex = 6
      numUpDownStars.Value = New Decimal(New Integer() {200, 0, 0, 0})
      ' 
      ' frmCfg
      ' 
      AutoScaleDimensions = New SizeF(7F, 15F)
      AutoScaleMode = AutoScaleMode.Font
      ClientSize = New Size(364, 201)
      Controls.Add(numUpDownStars)
      Controls.Add(lblStars)
      Controls.Add(lblSpeedValue)
      Controls.Add(tbSpeed)
      Controls.Add(lblSpeed)
      Controls.Add(btnOk)
      Controls.Add(chkBoxTrails)
      FormBorderStyle = FormBorderStyle.FixedToolWindow
      Margin = New Padding(3, 2, 3, 2)
      Name = "frmCfg"
      Text = "Settings"
      CType(tbSpeed, ComponentModel.ISupportInitialize).EndInit()
      CType(numUpDownStars, ComponentModel.ISupportInitialize).EndInit()
      ResumeLayout(False)
      PerformLayout()
   End Sub

   Friend WithEvents chkBoxTrails As CheckBox
   Friend WithEvents btnOk As Button
   Friend WithEvents lblSpeed As Label
   Friend WithEvents tbSpeed As TrackBar
   Friend WithEvents lblSpeedValue As Label
   Friend WithEvents lblStars As Label
   Friend WithEvents numUpDownStars As NumericUpDown
End Class
