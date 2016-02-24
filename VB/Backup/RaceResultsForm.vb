Public Class RaceResultsForm

    Dim RedLabel As New Label()
    Dim GreenLabel As New Label()
    Dim BlueLabel As New Label()
    Dim BlackLabel As New Label()

    Private Sub RaceResultsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim SerialPortString As String
        Dim RemainingString As String
        Dim TabPosition As Integer
        Dim RedTime, GreenTime, BlueTime, BlackTime As Integer
        Dim RedPlace, GreenPlace, BluePlace, BlackPlace As Integer

        SerialPort1.Open()
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()

        '----------------------------------------------------------
        ' Read the number of rotations on each channel
        '----------------------------------------------------------
        SerialPort1.Write("t")
        SerialPortString = SerialPort1.ReadLine()

        TabPosition = SerialPortString.IndexOf(vbTab)
        RedTime = Val("&H00" & Mid(SerialPortString, 1, TabPosition))         ' remember VB starts indexing at 1
        RemainingString = SerialPortString.Substring(TabPosition + 1, SerialPortString.Length - TabPosition - 1)

        TabPosition = RemainingString.IndexOf(vbTab)
        GreenTime = Val("&H00" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

        TabPosition = RemainingString.IndexOf(vbTab)
        BlueTime = Val("&H00" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

        BlackTime = Val("&H00" & RemainingString)         ' remember VB starts indexing at 1

        SerialPort1.Write("f")
        SerialPortString = SerialPort1.ReadLine()

        TabPosition = SerialPortString.IndexOf(vbTab)
        RedPlace = Val("&H" & Mid(SerialPortString, 1, TabPosition))         ' remember VB starts indexing at 1
        RemainingString = SerialPortString.Substring(TabPosition + 1, SerialPortString.Length - TabPosition - 1)

        TabPosition = RemainingString.IndexOf(vbTab)
        GreenPlace = Val("&H" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

        TabPosition = RemainingString.IndexOf(vbTab)
        BluePlace = Val("&H " & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

        BlackPlace = Val("&H" & RemainingString)         ' remember VB starts indexing at 1

        SerialPort1.Close()

        Dim count As Integer
        Dim VerticalPosition As Integer = 100
        Dim myfont As New Font("Courier New", 18, FontStyle.Bold)
        Dim PlaceName(5) As String
        PlaceName(1) = "First   "
        PlaceName(2) = "Second  "
        PlaceName(3) = "Third   "
        PlaceName(4) = "Fourth  "

        For count = 1 To SystemConfigurationForm.NumberOfRacers
            If (RedPlace = count) Then
                RedLabel.Location = New System.Drawing.Point(16, VerticalPosition)
                RedLabel.Text = PlaceName(count) & _
                                    RedTime / 10000 & "sec   " & _
                                    Math.Round(SystemConfigurationForm.RedTopSpeed, 1) & "mph   " & _
                                    SystemConfigurationForm.RedRacerName

                RedLabel.Size = New System.Drawing.Size(700, 40)
                RedLabel.Font = myfont
                RedLabel.ForeColor = Color.Red
                Me.Controls.Add(RedLabel)
                VerticalPosition += 40
            End If

            If (GreenPlace = count) Then
                GreenLabel.Location = New System.Drawing.Point(16, VerticalPosition)
                GreenLabel.Text = PlaceName(count) & _
                                GreenTime / 10000 & "sec   " & _
                                Math.Round(SystemConfigurationForm.GreenTopSpeed, 1) & "mph   " & _
                                SystemConfigurationForm.GreenRacerName
                GreenLabel.Size = New System.Drawing.Size(700, 40)
                GreenLabel.Font = myfont
                GreenLabel.ForeColor = Color.Green
                Me.Controls.Add(GreenLabel)
                VerticalPosition += 40
            End If

            If (BluePlace = count) Then
                BlueLabel.Location = New System.Drawing.Point(16, VerticalPosition)
                BlueLabel.Text = PlaceName(count) & _
                                    BlueTime / 10000 & "sec   " & _
                                    Math.Round(SystemConfigurationForm.BlueTopSpeed, 1) & "mph   " & _
                                    SystemConfigurationForm.BlueRacerName
                BlueLabel.Size = New System.Drawing.Size(700, 40)
                BlueLabel.Font = myfont
                BlueLabel.ForeColor = Color.Blue
                Me.Controls.Add(BlueLabel)
                VerticalPosition += 40
            End If

            If (BlackPlace = count) Then
                BlackLabel.Location = New System.Drawing.Point(16, VerticalPosition)
                BlackLabel.Text = PlaceName(count) & _
                                    BlackTime / 10000 & "sec   " & _
                                    Math.Round(SystemConfigurationForm.BlackTopSpeed, 1) & "mph   " & _
                                    SystemConfigurationForm.BlackRacerName
                BlackLabel.Size = New System.Drawing.Size(700, 40)
                BlackLabel.Font = myfont
                BlackLabel.ForeColor = Color.Black
                Me.Controls.Add(BlackLabel)
                VerticalPosition += 40
            End If
        Next

    End Sub

    Private Sub AnotherRace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnotherRaceButton.Click
        Dim RaceConfiguration As New RaceConfigurationForm
        RaceConfiguration.Show()
        Me.Close()

    End Sub

End Class