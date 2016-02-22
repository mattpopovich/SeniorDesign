Public Class StandingStartForm
    Dim CountDown As Integer
    Const FalseStartThreshold As Single = 1.0
    Dim RedFalseStart, GreenFalseStart, BlueFalseStart, BlackFalseStart As Boolean
    Public Label1 As New Label()



    Private Sub StandingStartForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        FalseStartButton.Visible = False

        CountDown = 4
        RedFalseStart = False
        GreenFalseStart = False
        BlueFalseStart = False
        BlackFalseStart = False

        SerialPort1.Open()
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()
        SerialPort1.Write("r")
        SerialPort1.Close()

        Label2.Location = New System.Drawing.Point(120, 50)
        Label2.Text = "Get Ready"

    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim SerialPortString As String
        Dim RedDistance, GreenDistance, BlueDistance, BlackDistance As Single
        Dim TabPosition As Integer
        Dim RemainingString As String

        SerialPort1.Open()
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()

        SerialPort1.Write("d")
        SerialPortString = SerialPort1.ReadLine()

        TabPosition = SerialPortString.IndexOf(vbTab)
        RedDistance = Val("&H" & Mid(SerialPortString, 1, TabPosition))         ' remember VB starts indexing at 1
        RedDistance = RedDistance * SystemConfigurationForm.RedRotsToMeters
        RemainingString = SerialPortString.Substring(TabPosition + 1, SerialPortString.Length - TabPosition - 1)

        TabPosition = RemainingString.IndexOf(vbTab)
        GreenDistance = Val("&H" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        GreenDistance = GreenDistance * SystemConfigurationForm.GreenRotsToMeters
        RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

        TabPosition = RemainingString.IndexOf(vbTab)
        BlueDistance = Val("&H " & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        BlueDistance = BlueDistance * SystemConfigurationForm.BlueRotsToMeters
        RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

        BlackDistance = Val("&H" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        BlackDistance = BlackDistance * SystemConfigurationForm.BlackRotsToMeters

        SerialPort1.Close()

        If RedDistance > FalseStartThreshold Then
            RedFalseStart = True
            FalseStartButton.Visible = True
            Label2.Location = New System.Drawing.Point(10, 50)
            Label2.Text = "Red False Start"
        End If

        If GreenDistance > FalseStartThreshold Then
            GreenFalseStart = True
            FalseStartButton.Visible = True
            Label2.Location = New System.Drawing.Point(10, 50)
            Label2.Text = "Green False Start"
        End If

        If BlueDistance > FalseStartThreshold Then
            BlueFalseStart = True
            FalseStartButton.Visible = True
            Label2.Location = New System.Drawing.Point(10, 50)
            Label2.Text = "Blue False Start"
        End If

        If BlackDistance > FalseStartThreshold Then
            BlackFalseStart = True
            FalseStartButton.Visible = True
            Label2.Location = New System.Drawing.Point(10, 50)
            Label2.Text = "Black False Start"
        End If

        If (RedFalseStart = False) And _
            (GreenFalseStart = False) And _
            (BlueFalseStart = False) And _
            (BlackFalseStart = False) Then
            If (CountDown > 1) Then
                CountDown = CountDown - 1
                Label2.Location = New System.Drawing.Point(230, 50)
                Label2.Text = CountDown
                Beep()
            Else
                Dim RaceClock As New RaceClockForm
                RaceClock.Show()
                Me.Close()
            End If
        End If

    End Sub

    Private Sub FalseStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FalseStartButton.Click
        Dim RaceConfiguration As New RaceConfigurationForm
        RaceConfigurationForm.Show()
        Me.Close()
    End Sub
End Class