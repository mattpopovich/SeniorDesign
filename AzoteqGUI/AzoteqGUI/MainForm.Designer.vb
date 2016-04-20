<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Me.lstConsole = New System.Windows.Forms.ListBox()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.lblCOM = New System.Windows.Forms.Label()
        Me.comCOM = New System.Windows.Forms.ComboBox()
        Me.chkConnected = New System.Windows.Forms.CheckBox()
        Me.tmrConnected = New System.Windows.Forms.Timer(Me.components)
        Me.txtWrite = New System.Windows.Forms.TextBox()
        Me.btnWrite = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkScrolling = New System.Windows.Forms.CheckBox()
        Me.chkPrinting = New System.Windows.Forms.CheckBox()
        Me.btnb1 = New System.Windows.Forms.Button()
        Me.btnb2 = New System.Windows.Forms.Button()
        Me.btnb3 = New System.Windows.Forms.Button()
        Me.btnb4 = New System.Windows.Forms.Button()
        Me.btnb = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnt1 = New System.Windows.Forms.Button()
        Me.MaskBox = New System.Windows.Forms.PictureBox()
        Me.btnbLoop = New System.Windows.Forms.Button()
        CType(Me.MaskBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstConsole
        '
        Me.lstConsole.BackColor = System.Drawing.SystemColors.WindowText
        Me.lstConsole.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstConsole.ForeColor = System.Drawing.SystemColors.Window
        Me.lstConsole.FormattingEnabled = True
        Me.lstConsole.HorizontalScrollbar = True
        Me.lstConsole.Location = New System.Drawing.Point(12, 94)
        Me.lstConsole.Name = "lstConsole"
        Me.lstConsole.Size = New System.Drawing.Size(269, 238)
        Me.lstConsole.TabIndex = 1
        '
        'lblCOM
        '
        Me.lblCOM.AutoSize = True
        Me.lblCOM.Location = New System.Drawing.Point(55, 9)
        Me.lblCOM.Name = "lblCOM"
        Me.lblCOM.Size = New System.Drawing.Size(34, 13)
        Me.lblCOM.TabIndex = 2
        Me.lblCOM.Text = "COM:"
        '
        'comCOM
        '
        Me.comCOM.FormattingEnabled = True
        Me.comCOM.Location = New System.Drawing.Point(95, 6)
        Me.comCOM.Name = "comCOM"
        Me.comCOM.Size = New System.Drawing.Size(66, 21)
        Me.comCOM.TabIndex = 3
        '
        'chkConnected
        '
        Me.chkConnected.AutoCheck = False
        Me.chkConnected.AutoSize = True
        Me.chkConnected.Location = New System.Drawing.Point(167, 8)
        Me.chkConnected.Name = "chkConnected"
        Me.chkConnected.Size = New System.Drawing.Size(78, 17)
        Me.chkConnected.TabIndex = 5
        Me.chkConnected.Text = "Connected"
        Me.chkConnected.UseVisualStyleBackColor = True
        '
        'tmrConnected
        '
        Me.tmrConnected.Enabled = True
        Me.tmrConnected.Interval = 1
        '
        'txtWrite
        '
        Me.txtWrite.Location = New System.Drawing.Point(12, 37)
        Me.txtWrite.Name = "txtWrite"
        Me.txtWrite.Size = New System.Drawing.Size(188, 20)
        Me.txtWrite.TabIndex = 7
        '
        'btnWrite
        '
        Me.btnWrite.Location = New System.Drawing.Point(206, 34)
        Me.btnWrite.Name = "btnWrite"
        Me.btnWrite.Size = New System.Drawing.Size(75, 23)
        Me.btnWrite.TabIndex = 9
        Me.btnWrite.Text = "Serial Write"
        Me.btnWrite.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(288, 277)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(82, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chkScrolling
        '
        Me.chkScrolling.AutoSize = True
        Me.chkScrolling.Location = New System.Drawing.Point(12, 67)
        Me.chkScrolling.Name = "chkScrolling"
        Me.chkScrolling.Size = New System.Drawing.Size(66, 17)
        Me.chkScrolling.TabIndex = 11
        Me.chkScrolling.Text = "Scrolling"
        Me.chkScrolling.UseVisualStyleBackColor = True
        '
        'chkPrinting
        '
        Me.chkPrinting.AutoSize = True
        Me.chkPrinting.Location = New System.Drawing.Point(220, 67)
        Me.chkPrinting.Name = "chkPrinting"
        Me.chkPrinting.Size = New System.Drawing.Size(61, 17)
        Me.chkPrinting.TabIndex = 12
        Me.chkPrinting.Text = "Printing"
        Me.chkPrinting.UseVisualStyleBackColor = True
        '
        'btnb1
        '
        Me.btnb1.Location = New System.Drawing.Point(288, 81)
        Me.btnb1.Name = "btnb1"
        Me.btnb1.Size = New System.Drawing.Size(38, 23)
        Me.btnb1.TabIndex = 13
        Me.btnb1.Text = "b1"
        Me.btnb1.UseVisualStyleBackColor = True
        '
        'btnb2
        '
        Me.btnb2.Location = New System.Drawing.Point(288, 110)
        Me.btnb2.Name = "btnb2"
        Me.btnb2.Size = New System.Drawing.Size(38, 23)
        Me.btnb2.TabIndex = 14
        Me.btnb2.Text = "b2"
        Me.btnb2.UseVisualStyleBackColor = True
        '
        'btnb3
        '
        Me.btnb3.Location = New System.Drawing.Point(288, 139)
        Me.btnb3.Name = "btnb3"
        Me.btnb3.Size = New System.Drawing.Size(38, 23)
        Me.btnb3.TabIndex = 15
        Me.btnb3.Text = "b3"
        Me.btnb3.UseVisualStyleBackColor = True
        '
        'btnb4
        '
        Me.btnb4.Location = New System.Drawing.Point(288, 168)
        Me.btnb4.Name = "btnb4"
        Me.btnb4.Size = New System.Drawing.Size(38, 23)
        Me.btnb4.TabIndex = 16
        Me.btnb4.Text = "b4"
        Me.btnb4.UseVisualStyleBackColor = True
        '
        'btnb
        '
        Me.btnb.Location = New System.Drawing.Point(332, 81)
        Me.btnb.Name = "btnb"
        Me.btnb.Size = New System.Drawing.Size(38, 110)
        Me.btnb.TabIndex = 17
        Me.btnb.Text = "b"
        Me.btnb.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(95, 63)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(90, 23)
        Me.btnClear.TabIndex = 18
        Me.btnClear.Text = "Clear Console"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnt1
        '
        Me.btnt1.Location = New System.Drawing.Point(288, 226)
        Me.btnt1.Name = "btnt1"
        Me.btnt1.Size = New System.Drawing.Size(82, 23)
        Me.btnt1.TabIndex = 19
        Me.btnt1.Text = "t1"
        Me.btnt1.UseVisualStyleBackColor = True
        '
        'MaskBox
        '
        Me.MaskBox.Name = "MaskBox"
        Me.MaskBox.TabIndex = 20
        Me.MaskBox.TabStop = False
        '
        'btnbLoop
        '
        Me.btnbLoop.Location = New System.Drawing.Point(288, 197)
        Me.btnbLoop.Name = "btnbLoop"
        Me.btnbLoop.Size = New System.Drawing.Size(82, 23)
        Me.btnbLoop.TabIndex = 21
        Me.btnbLoop.Text = "SS"
        Me.btnbLoop.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnbLoop)
        Me.Controls.Add(Me.MaskBox)
        Me.Controls.Add(Me.btnt1)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnb)
        Me.Controls.Add(Me.btnb4)
        Me.Controls.Add(Me.btnb3)
        Me.Controls.Add(Me.btnb2)
        Me.Controls.Add(Me.btnb1)
        Me.Controls.Add(Me.chkPrinting)
        Me.Controls.Add(Me.chkScrolling)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnWrite)
        Me.Controls.Add(Me.txtWrite)
        Me.Controls.Add(Me.chkConnected)
        Me.Controls.Add(Me.comCOM)
        Me.Controls.Add(Me.lblCOM)
        Me.Controls.Add(Me.lstConsole)
        Me.Name = "MainForm"
        Me.Text = "Azoteq IQS 316 GUI"
        CType(Me.MaskBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstConsole As System.Windows.Forms.ListBox
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents lblCOM As System.Windows.Forms.Label
    Friend WithEvents comCOM As System.Windows.Forms.ComboBox
    Friend WithEvents chkConnected As System.Windows.Forms.CheckBox
    Friend WithEvents tmrConnected As System.Windows.Forms.Timer
    Friend WithEvents txtWrite As System.Windows.Forms.TextBox
    Friend WithEvents btnWrite As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkScrolling As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrinting As System.Windows.Forms.CheckBox
    Friend WithEvents btnb1 As System.Windows.Forms.Button
    Friend WithEvents btnb2 As System.Windows.Forms.Button
    Friend WithEvents btnb3 As System.Windows.Forms.Button
    Friend WithEvents btnb4 As System.Windows.Forms.Button
    Friend WithEvents btnb As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnt1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents btnbLoop As System.Windows.Forms.Button

End Class
