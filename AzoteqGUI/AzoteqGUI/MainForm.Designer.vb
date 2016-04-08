﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.btnTest = New System.Windows.Forms.Button()
        Me.txtWrite = New System.Windows.Forms.TextBox()
        Me.btnWrite = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstConsole
        '
        Me.lstConsole.BackColor = System.Drawing.SystemColors.WindowText
        Me.lstConsole.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstConsole.ForeColor = System.Drawing.SystemColors.Window
        Me.lstConsole.FormattingEnabled = True
        Me.lstConsole.HorizontalScrollbar = True
        Me.lstConsole.Location = New System.Drawing.Point(12, 63)
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
        Me.tmrConnected.Interval = 1000
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(119, 309)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(75, 23)
        Me.btnTest.TabIndex = 6
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
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
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 344)
        Me.Controls.Add(Me.btnWrite)
        Me.Controls.Add(Me.txtWrite)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.chkConnected)
        Me.Controls.Add(Me.comCOM)
        Me.Controls.Add(Me.lblCOM)
        Me.Controls.Add(Me.lstConsole)
        Me.Name = "MainForm"
        Me.Text = "Azoteq IQS 316 GUI"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstConsole As System.Windows.Forms.ListBox
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents lblCOM As System.Windows.Forms.Label
    Friend WithEvents comCOM As System.Windows.Forms.ComboBox
    Friend WithEvents chkConnected As System.Windows.Forms.CheckBox
    Friend WithEvents tmrConnected As System.Windows.Forms.Timer
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents txtWrite As System.Windows.Forms.TextBox
    Friend WithEvents btnWrite As System.Windows.Forms.Button

End Class
