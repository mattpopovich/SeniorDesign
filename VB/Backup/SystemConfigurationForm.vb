Public Class SystemConfigurationForm
    Public NumberOfRacers As Integer
    Public RedRotsToMeters, GreenRotsToMeters, BlueRotsToMeters, BlackRotsToMeters As Single
    Public RedDeltaToMPH, GreenDeltaToMPH, BlueDeltaToMPH, BlackDeltaToMPH As Single
    Public RedRacerName, GreenRacerName, BlueRacerName, BlackRacerName As String
    Public RedTopSpeed, GreenTopSpeed, BlueTopSpeed, BlackTopSpeed As Single

    Const PI As Single = 3.1415

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextButton.Click

        Dim ValidConfiguration As Boolean
        Dim RaceConfig As New RaceConfigurationForm

        ValidConfiguration = CheckDiameters()
        If ValidConfiguration = True Then
            Me.Hide()
            RaceConfig.Show()

            If (REDmm.Checked = True) Then
                RedRotsToMeters = (PI * Val(REDdiameter.Text)) / 1000
                RedDeltaToMPH = (PI * Val(REDdiameter.Text) * 3600) / (25.4 * 12 * 5280)
            Else
                RedRotsToMeters = (PI * Val(REDdiameter.Text) * 2.54) / 100
                RedDeltaToMPH = (PI * Val(REDdiameter.Text) * 3600) / (12 * 5280)
            End If

            ' If the Diameter field is not empty then calculate conversion constants
            If (GREENdiameter.Text <> "") Then
                If (GREENmm.Checked = True) Then
                    GreenRotsToMeters = (PI * Val(GREENdiameter.Text)) / 1000
                    GreenDeltaToMPH = (PI * Val(GREENdiameter.Text) * 3600) / (25.4 * 12 * 5280)
                Else
                    GreenRotsToMeters = (PI * Val(GREENdiameter.Text) * 2.54) / 100
                    GreenDeltaToMPH = (PI * Val(GREENdiameter.Text) * 3600) / (12 * 5280)
                End If
            Else
                GreenRotsToMeters = 0
                GreenDeltaToMPH = 0
            End If

            ' If the Diameter field is not empty then calculate conversion constants
            If (BLUEdiameter.Text <> "") Then
                If (BLUEmm.Checked = True) Then
                    BlueRotsToMeters = (PI * Val(BLUEdiameter.Text)) / 1000
                    BlueDeltaToMPH = (PI * Val(BLUEdiameter.Text) * 3600) / (25.4 * 12 * 5280)
                Else
                    BlueRotsToMeters = (PI * Val(BLUEdiameter.Text) * 2.54) / 100
                    BlueDeltaToMPH = (PI * Val(BLUEdiameter.Text) * 3600) / (12 * 5280)
                End If
            Else
                BlueRotsToMeters = 0
                BlueDeltaToMPH = 0
            End If

            ' If the Diameter field is not empty then calculate conversion constants
            If (BLACKdiameter.Text <> "") Then
                If (BLACKmm.Checked = True) Then
                    BlackRotsToMeters = (PI * Val(BLACKdiameter.Text)) / 1000
                    BlackDeltaToMPH = (PI * Val(BLACKdiameter.Text) * 3600) / (25.4 * 12 * 5280)
                Else
                    BlackRotsToMeters = (PI * Val(BLACKdiameter.Text) * 2.54) / 100
                    BlackDeltaToMPH = (PI * Val(BLACKdiameter.Text) * 3600) / (12 * 5280)
                End If
            Else
                BlackRotsToMeters = 0
                BlackDeltaToMPH = 0
            End If

        End If

    End Sub

    Private Sub TestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestButton.Click
        Dim SerialTest As New SerialTestForm
        Dim ValidConfiguration As Boolean

        ValidConfiguration = CheckDiameters()
        If ValidConfiguration = True Then
            Me.Hide()
            SerialTest.Show()
        End If

    End Sub

    Private Function CheckDiameters() As Boolean
        CheckDiameters = True
        If (NumberRollers.Value = 1) Then
            If (REDdiameter.Text = "") Then
                MsgBox("Enter a RED roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
        ElseIf (NumberRollers.Value = 2) Then
            If (REDdiameter.Text = "") Then
                MsgBox("Enter a RED roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
            If (GREENdiameter.Text = "") Then
                MsgBox("Enter a GREEN roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
        ElseIf (NumberRollers.Value = 3) Then
            If (REDdiameter.Text = "") Then
                MsgBox("Enter a RED roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
            If (GREENdiameter.Text = "") Then
                MsgBox("Enter a GREEN roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
            If (BLUEdiameter.Text = "") Then
                MsgBox("Enter a BLUE roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
        ElseIf (NumberRollers.Value = 4) Then
            If (REDdiameter.Text = "") Then
                MsgBox("Enter a RED roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
            If (GREENdiameter.Text = "") Then
                MsgBox("Enter a GREEN roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
            If (BLUEdiameter.Text = "") Then
                MsgBox("Enter a BLUE roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
            If (BLACKdiameter.Text = "") Then
                MsgBox("Enter a BLACK roller diameter", , "Configuration Error")
                CheckDiameters = False
            End If
        End If
    End Function

End Class
