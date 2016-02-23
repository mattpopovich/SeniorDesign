Public Class RaceConfigurationForm
    Dim GREENracer As New TextBox()
    Dim GREENlabel As New Label()
    Dim BLUEracer As New TextBox()
    Dim BLUElabel As New Label()
    Dim BLACKracer As New TextBox()
    Dim BLACKlabel As New Label()

    Private Sub RaceConfigurationForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        RaceType.SelectedText = "500m (standing start)"

        GREENlabel.Location = New System.Drawing.Point(16, 168)
        GREENlabel.Text = "Green"
        GREENracer.Location = New System.Drawing.Point(58, 161)
        GREENracer.TabStop = True
        GREENracer.TabIndex = 2

        BLUElabel.Location = New System.Drawing.Point(16, 201)
        BLUElabel.Text = "Blue"
        BLUEracer.Location = New System.Drawing.Point(58, 194)
        BLUEracer.TabStop = True
        BLUEracer.TabIndex = 3

        BLACKlabel.Location = New System.Drawing.Point(16, 234)
        BLACKlabel.Text = "Black"
        BLACKracer.Location = New System.Drawing.Point(58, 227)
        BLACKracer.TabStop = True
        BLACKracer.TabIndex = 4

        If SystemConfigurationForm.NumberRollers.Value > 1 Then
            Me.Controls.Add(GREENracer)
            Me.Controls.Add(GREENlabel)
            If SystemConfigurationForm.NumberRollers.Value > 2 Then
                Me.Controls.Add(BLUEracer)
                Me.Controls.Add(BLUElabel)
                If SystemConfigurationForm.NumberRollers.Value > 3 Then
                    Me.Controls.Add(BLACKracer)
                    Me.Controls.Add(BLACKlabel)
                End If
            End If
        End If
        Me.NumericUpDown1.Value = SystemConfigurationForm.NumberRollers.Value
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SystemConfigurationForm.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        Dim RaceDistance As Single

        SystemConfigurationForm.NumberOfRacers = Me.NumericUpDown1.Value

        '----------------------------------------------------------
        ' Set the distance for the four types of races
        '   500m (flying start)
        '   500m (standing start)
        '   1000m (standing start)
        '   5000m (standing start)
        '----------------------------------------------------------
        If (RaceType.Text = "500m (flying start)") Or _
            (RaceType.Text = "500m (standing start)") Then
            RaceDistance = 500
        ElseIf (RaceType.Text = "1000m (standing start)") Then
            RaceDistance = 1000
        ElseIf (RaceType.Text = "5000m (standing start)") Then
            RaceDistance = 5000
        ElseIf (RaceType.Text = "10m (test)") Then
            RaceDistance = 10
        End If

        '----------------------------------------------------------
        ' Set the length of the race on the embedded controller
        '----------------------------------------------------------
        SerialPort1.Open()
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()

        ' reset any data stored in the embedded controller
        SerialPort1.Write("r")

        ' Write the length of the race (in terms of rotations) onto the 
        ' embedded controller.
        SerialPort1.Write("1")
        SerialPort1.Write(CInt(RaceDistance / SystemConfigurationForm.RedRotsToMeters))
        SerialPort1.Write(vbCrLf)
        SystemConfigurationForm.RedRacerName = RedRacer.Text
        SystemConfigurationForm.RedTopSpeed = 0

        If (Me.NumericUpDown1.Value > 1) Then
            SerialPort1.Write("2")
            SerialPort1.Write(CInt(RaceDistance / SystemConfigurationForm.GreenRotsToMeters))
            SerialPort1.Write(vbCrLf)
            SystemConfigurationForm.GreenRacerName = GREENracer.Text
            SystemConfigurationForm.GreenTopSpeed = 0
        End If

        If (Me.NumericUpDown1.Value > 2) Then
            SerialPort1.Write("3")
            SerialPort1.Write(CInt(RaceDistance / SystemConfigurationForm.BlueRotsToMeters))
            SerialPort1.Write(vbCrLf)
            SystemConfigurationForm.BlueRacerName = BLUEracer.Text
            SystemConfigurationForm.BlueTopSpeed = 0
        End If

        If (Me.NumericUpDown1.Value > 3) Then
            SerialPort1.Write("4")
            SerialPort1.Write(CInt(RaceDistance / SystemConfigurationForm.BlackRotsToMeters))
            SerialPort1.Write(vbCrLf)
            SystemConfigurationForm.BlackRacerName = BLACKracer.Text
            SystemConfigurationForm.BlackTopSpeed = 0
        End If

        SerialPort1.Close()


        If (RaceType.Text = "500m (standing start)") Or _
            (RaceType.Text = "1000m (standing start)") Or _
            (RaceType.Text = "10m (test)") Or _
            (RaceType.Text = "5000m (standing start)") Then

            Dim StandingStart As New StandingStartForm
            Me.Close()
            StandingStart.Show()

        ElseIf (RaceType.Text = "500m (flying start)") Then
            Dim FlyStart As New FlyingStartForm
            Me.Close()
            FlyStart.Show()
        End If

    End Sub

    Private Sub ExitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        Me.Close()
        SystemConfigurationForm.Close()
    End Sub
End Class