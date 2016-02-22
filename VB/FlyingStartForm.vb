Public Class FlyingStartForm
    Dim CountDown As Integer

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If (CountDown > 1) Then
            CountDown = CountDown - 1
            Label1.Location = New System.Drawing.Point(230, 50)
            Label1.Text = CountDown
            Beep()
        Else
            Dim RaceClock As New RaceClockForm
            RaceClock.Show()
            Me.Close()
        End If

    End Sub

    Private Sub FlyingStartForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CountDown = 10
        Label1.Location = New System.Drawing.Point(120, 50)
        Label1.Text = "Start Peddling"
    End Sub
End Class