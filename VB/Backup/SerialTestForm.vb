Public Class SerialTestForm

    Dim GREENsensor As New TextBox()
    Dim GREENrots As New TextBox()
    Dim GREENdelta As New TextBox()
    Dim GREENlabel As New Label()

    Dim BLUEsensor As New TextBox()
    Dim BLUErots As New TextBox()
    Dim BLUEdelta As New TextBox()
    Dim BLUElabel As New Label()

    Dim BLACKsensor As New TextBox()
    Dim BLACKrots As New TextBox()
    Dim BLACKdelta As New TextBox()
    Dim BLACKlabel As New Label()

    Private Sub SerialTestForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GREENlabel.Location = New System.Drawing.Point(16, 168)
        GREENlabel.Text = "Green"
        GREENlabel.Width = 50
        GREENsensor.Location = New System.Drawing.Point(78, 161)
        GREENsensor.Size = New System.Drawing.Size(24, 20)
        GREENrots.Location = New System.Drawing.Point(138, 161)
        GREENrots.Size = New System.Drawing.Size(48, 20)
        GREENdelta.Location = New System.Drawing.Point(218, 161)
        GREENdelta.Size = New System.Drawing.Size(48, 20)

        BLUElabel.Location = New System.Drawing.Point(16, 201)
        BLUElabel.Text = "Blue"
        BLUElabel.Width = 50
        BLUEsensor.Location = New System.Drawing.Point(78, 194)
        BLUEsensor.Size = New System.Drawing.Size(24, 20)
        BLUErots.Location = New System.Drawing.Point(138, 194)
        BLUErots.Size = New System.Drawing.Size(48, 20)
        BLUEdelta.Location = New System.Drawing.Point(218, 194)
        BLUEdelta.Size = New System.Drawing.Size(48, 20)

        BLACKlabel.Location = New System.Drawing.Point(16, 234)
        BLACKlabel.Text = "Black"
        BLACKlabel.Width = 50
        BLACKsensor.Location = New System.Drawing.Point(78, 227)
        BLACKsensor.Size = New System.Drawing.Size(24, 20)
        BLACKrots.Location = New System.Drawing.Point(138, 227)
        BLACKrots.Size = New System.Drawing.Size(48, 20)
        BLACKdelta.Location = New System.Drawing.Point(218, 227)
        BLACKdelta.Size = New System.Drawing.Size(48, 20)

        If SystemConfigurationForm.NumberRollers.Value > 1 Then
            Me.Controls.Add(GREENlabel)
            Me.Controls.Add(GREENsensor)
            Me.Controls.Add(GREENrots)
            Me.Controls.Add(GREENdelta)
            If SystemConfigurationForm.NumberRollers.Value > 2 Then
                Me.Controls.Add(BLUElabel)
                Me.Controls.Add(BLUEsensor)
                Me.Controls.Add(BLUErots)
                Me.Controls.Add(BLUEdelta)
                If SystemConfigurationForm.NumberRollers.Value > 3 Then
                    Me.Controls.Add(BLACKlabel)
                    Me.Controls.Add(BLACKsensor)
                    Me.Controls.Add(BLACKrots)
                    Me.Controls.Add(BLACKdelta)
                End If
            End If
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim SerialPortString As String
        Dim RemainingString As String
        Dim TabPosition As Integer

        SerialPort1.Open()
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()

        '----------------------------------------------------------
        ' Read the state of the sensors
        '----------------------------------------------------------
        SerialPort1.Write("v")
        SerialPortString = SerialPort1.ReadLine()

        If SerialPortString <> "" Then

            TabPosition = SerialPortString.IndexOf(vbTab)
            Me.REDsensor.Text = Val("&H" & Mid(SerialPortString, 1, TabPosition))        ' remember VB starts indexing at 1
            RemainingString = SerialPortString.Substring(TabPosition + 1, SerialPortString.Length - TabPosition - 1)

            TabPosition = RemainingString.IndexOf(vbTab)
            Me.GREENsensor.Text = Val("&H" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
            RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

            TabPosition = RemainingString.IndexOf(vbTab)
            Me.BLUEsensor.Text = Val("&H" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
            RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

            Me.BLACKsensor.Text = Val("&H" & RemainingString)         ' remember VB starts indexing at 1

        Else
            Me.REDsensor.Text = "empty"
        End If


        '----------------------------------------------------------
        ' Read the number of rotations
        '----------------------------------------------------------
        SerialPort1.Write("d")
        SerialPortString = SerialPort1.ReadLine()

        If SerialPortString <> "" Then

            TabPosition = SerialPortString.IndexOf(vbTab)
            Me.REDrots.Text = Val("&H" & Mid(SerialPortString, 1, TabPosition))         ' remember VB starts indexing at 1
            RemainingString = SerialPortString.Substring(TabPosition + 1, SerialPortString.Length - TabPosition - 1)

            TabPosition = RemainingString.IndexOf(vbTab)
            Me.GREENrots.Text = Val("&H" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
            RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

            TabPosition = RemainingString.IndexOf(vbTab)
            Me.BLUErots.Text = Val("&H " & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
            RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

            Me.BLACKrots.Text = Val("&H" & RemainingString)         ' remember VB starts indexing at 1

        Else
            Me.REDrots.Text = "empty"
        End If


        '----------------------------------------------------------
        ' Read the number of rotations in the last second
        '----------------------------------------------------------
        SerialPort1.Write("s")
        SerialPortString = SerialPort1.ReadLine()

        If SerialPortString <> "" Then

            TabPosition = SerialPortString.IndexOf(vbTab)
            Me.REDdelta.Text = Val("&H" & Mid(SerialPortString, 1, TabPosition))        ' remember VB starts indexing at 1
            RemainingString = SerialPortString.Substring(TabPosition + 1, SerialPortString.Length - TabPosition - 1)

            TabPosition = RemainingString.IndexOf(vbTab)
            Me.GREENdelta.Text = Val("&H" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
            RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

            TabPosition = RemainingString.IndexOf(vbTab)
            Me.BLUEdelta.Text = Val("&H" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
            RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

            Me.BLACKdelta.Text = Val("&H" & RemainingString)         ' remember VB starts indexing at 1

        Else
            Me.REDdelta.Text = "empty"
        End If

        SerialPort1.Close()

    End Sub

    Private Sub BackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackButton.Click
        SystemConfigurationForm.Show()
        Me.Close()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        SerialPort1.Open()
        SerialPort1.Write("r")
        SerialPort1.Close()

    End Sub
End Class