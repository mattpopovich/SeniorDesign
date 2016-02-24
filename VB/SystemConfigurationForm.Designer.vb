<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SystemConfigurationForm
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
        Me.REDdiameter = New System.Windows.Forms.TextBox
        Me.NumberRollers = New System.Windows.Forms.NumericUpDown
        Me.REDmm = New System.Windows.Forms.RadioButton
        Me.REDinch = New System.Windows.Forms.RadioButton
        Me.GREENdiameter = New System.Windows.Forms.TextBox
        Me.GREENmm = New System.Windows.Forms.RadioButton
        Me.GREENinch = New System.Windows.Forms.RadioButton
        Me.BLUEdiameter = New System.Windows.Forms.TextBox
        Me.BLUEmm = New System.Windows.Forms.RadioButton
        Me.BLUEinch = New System.Windows.Forms.RadioButton
        Me.NextButton = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TestButton = New System.Windows.Forms.Button
        Me.BLACKdiameter = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.BLACKinch = New System.Windows.Forms.RadioButton
        Me.BLACKmm = New System.Windows.Forms.RadioButton
        Me.Label7 = New System.Windows.Forms.Label
        CType(Me.NumberRollers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'REDdiameter
        '
        Me.REDdiameter.Location = New System.Drawing.Point(58, 128)
        Me.REDdiameter.Name = "REDdiameter"
        Me.REDdiameter.Size = New System.Drawing.Size(100, 20)
        Me.REDdiameter.TabIndex = 0
        '
        'NumberRollers
        '
        Me.NumberRollers.Location = New System.Drawing.Point(116, 74)
        Me.NumberRollers.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.NumberRollers.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumberRollers.Name = "NumberRollers"
        Me.NumberRollers.Size = New System.Drawing.Size(42, 20)
        Me.NumberRollers.TabIndex = 100
        Me.NumberRollers.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'REDmm
        '
        Me.REDmm.AutoSize = True
        Me.REDmm.Location = New System.Drawing.Point(5, 8)
        Me.REDmm.Name = "REDmm"
        Me.REDmm.Size = New System.Drawing.Size(41, 17)
        Me.REDmm.TabIndex = 2
        Me.REDmm.Text = "mm"
        Me.REDmm.UseVisualStyleBackColor = True
        '
        'REDinch
        '
        Me.REDinch.AutoSize = True
        Me.REDinch.Checked = True
        Me.REDinch.Location = New System.Drawing.Point(50, 8)
        Me.REDinch.Name = "REDinch"
        Me.REDinch.Size = New System.Drawing.Size(45, 17)
        Me.REDinch.TabIndex = 3
        Me.REDinch.TabStop = True
        Me.REDinch.Text = "inch"
        Me.REDinch.UseVisualStyleBackColor = True
        '
        'GREENdiameter
        '
        Me.GREENdiameter.Location = New System.Drawing.Point(58, 161)
        Me.GREENdiameter.Name = "GREENdiameter"
        Me.GREENdiameter.Size = New System.Drawing.Size(100, 20)
        Me.GREENdiameter.TabIndex = 2
        '
        'GREENmm
        '
        Me.GREENmm.AutoSize = True
        Me.GREENmm.Location = New System.Drawing.Point(5, 8)
        Me.GREENmm.Name = "GREENmm"
        Me.GREENmm.Size = New System.Drawing.Size(41, 17)
        Me.GREENmm.TabIndex = 5
        Me.GREENmm.Text = "mm"
        Me.GREENmm.UseVisualStyleBackColor = True
        '
        'GREENinch
        '
        Me.GREENinch.AutoSize = True
        Me.GREENinch.Checked = True
        Me.GREENinch.Location = New System.Drawing.Point(50, 8)
        Me.GREENinch.Name = "GREENinch"
        Me.GREENinch.Size = New System.Drawing.Size(45, 17)
        Me.GREENinch.TabIndex = 6
        Me.GREENinch.TabStop = True
        Me.GREENinch.Text = "inch"
        Me.GREENinch.UseVisualStyleBackColor = True
        '
        'BLUEdiameter
        '
        Me.BLUEdiameter.Location = New System.Drawing.Point(58, 194)
        Me.BLUEdiameter.Name = "BLUEdiameter"
        Me.BLUEdiameter.Size = New System.Drawing.Size(100, 20)
        Me.BLUEdiameter.TabIndex = 4
        '
        'BLUEmm
        '
        Me.BLUEmm.AutoSize = True
        Me.BLUEmm.Location = New System.Drawing.Point(5, 8)
        Me.BLUEmm.Name = "BLUEmm"
        Me.BLUEmm.Size = New System.Drawing.Size(41, 17)
        Me.BLUEmm.TabIndex = 8
        Me.BLUEmm.Text = "mm"
        Me.BLUEmm.UseVisualStyleBackColor = True
        '
        'BLUEinch
        '
        Me.BLUEinch.AutoSize = True
        Me.BLUEinch.Checked = True
        Me.BLUEinch.Location = New System.Drawing.Point(50, 8)
        Me.BLUEinch.Name = "BLUEinch"
        Me.BLUEinch.Size = New System.Drawing.Size(45, 17)
        Me.BLUEinch.TabIndex = 9
        Me.BLUEinch.TabStop = True
        Me.BLUEinch.Text = "inch"
        Me.BLUEinch.UseVisualStyleBackColor = True
        '
        'NextButton
        '
        Me.NextButton.Location = New System.Drawing.Point(184, 260)
        Me.NextButton.Name = "NextButton"
        Me.NextButton.Size = New System.Drawing.Size(75, 23)
        Me.NextButton.TabIndex = 9
        Me.NextButton.Text = "Race"
        Me.NextButton.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(397, 37)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "RollerRace Configuration"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Green"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Red"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 201)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Blue"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Number of Rollers"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(215, 20)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Enter roller diameter(s) below"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.REDmm)
        Me.GroupBox1.Controls.Add(Me.REDinch)
        Me.GroupBox1.Location = New System.Drawing.Point(164, 120)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(100, 28)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GREENmm)
        Me.GroupBox2.Controls.Add(Me.GREENinch)
        Me.GroupBox2.Location = New System.Drawing.Point(164, 153)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(100, 28)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BLUEmm)
        Me.GroupBox3.Controls.Add(Me.BLUEinch)
        Me.GroupBox3.Location = New System.Drawing.Point(164, 186)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(100, 28)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        '
        'TestButton
        '
        Me.TestButton.Location = New System.Drawing.Point(58, 260)
        Me.TestButton.Name = "TestButton"
        Me.TestButton.Size = New System.Drawing.Size(75, 23)
        Me.TestButton.TabIndex = 8
        Me.TestButton.Text = "Test"
        Me.TestButton.UseVisualStyleBackColor = True
        '
        'BLACKdiameter
        '
        Me.BLACKdiameter.Location = New System.Drawing.Point(58, 227)
        Me.BLACKdiameter.Name = "BLACKdiameter"
        Me.BLACKdiameter.Size = New System.Drawing.Size(100, 20)
        Me.BLACKdiameter.TabIndex = 6
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.BLACKinch)
        Me.GroupBox4.Controls.Add(Me.BLACKmm)
        Me.GroupBox4.Location = New System.Drawing.Point(164, 219)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(100, 28)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        '
        'BLACKinch
        '
        Me.BLACKinch.AutoSize = True
        Me.BLACKinch.Checked = True
        Me.BLACKinch.Location = New System.Drawing.Point(50, 8)
        Me.BLACKinch.Name = "BLACKinch"
        Me.BLACKinch.Size = New System.Drawing.Size(45, 17)
        Me.BLACKinch.TabIndex = 1
        Me.BLACKinch.TabStop = True
        Me.BLACKinch.Text = "inch"
        Me.BLACKinch.UseVisualStyleBackColor = True
        '
        'BLACKmm
        '
        Me.BLACKmm.AutoSize = True
        Me.BLACKmm.Location = New System.Drawing.Point(5, 8)
        Me.BLACKmm.Name = "BLACKmm"
        Me.BLACKmm.Size = New System.Drawing.Size(41, 17)
        Me.BLACKmm.TabIndex = 0
        Me.BLACKmm.Text = "mm"
        Me.BLACKmm.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 234)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "Black"
        '
        'SystemConfigurationForm
        '
        Me.AcceptButton = Me.NextButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 316)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.BLACKdiameter)
        Me.Controls.Add(Me.TestButton)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.NextButton)
        Me.Controls.Add(Me.BLUEdiameter)
        Me.Controls.Add(Me.GREENdiameter)
        Me.Controls.Add(Me.NumberRollers)
        Me.Controls.Add(Me.REDdiameter)
        Me.Name = "SystemConfigurationForm"
        Me.Text = "RollerRace Configuration"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.NumberRollers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents REDdiameter As System.Windows.Forms.TextBox
    Friend WithEvents NumberRollers As System.Windows.Forms.NumericUpDown
    Friend WithEvents REDmm As System.Windows.Forms.RadioButton
    Friend WithEvents REDinch As System.Windows.Forms.RadioButton
    Friend WithEvents GREENdiameter As System.Windows.Forms.TextBox
    Friend WithEvents GREENmm As System.Windows.Forms.RadioButton
    Friend WithEvents GREENinch As System.Windows.Forms.RadioButton
    Friend WithEvents BLUEdiameter As System.Windows.Forms.TextBox
    Friend WithEvents BLUEmm As System.Windows.Forms.RadioButton
    Friend WithEvents BLUEinch As System.Windows.Forms.RadioButton
    Friend WithEvents NextButton As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TestButton As System.Windows.Forms.Button
    Friend WithEvents BLACKdiameter As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents BLACKinch As System.Windows.Forms.RadioButton
    Friend WithEvents BLACKmm As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label

End Class
