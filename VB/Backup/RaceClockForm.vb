Public Class RaceClockForm

    Dim Second As Integer = 0

    Public Const XOFF As Integer = 10
    Public Const YOFF As Integer = 10

    Public Const BoxSize = 650
    Public Const CenterX = CInt(BoxSize / 2)
    Public Const CenterY = CInt(BoxSize / 2)

    Public Const RadiusMinor = CInt(0.3032 * BoxSize)
    Public Const RadiusMajor = CInt(0.4813 * BoxSize)
    Public Const PinHeight = CInt(0.025 * BoxSize)
    Public Const PinWidth = CInt(0.008 * BoxSize)

    Dim g As Graphics = CreateGraphics()
    Dim myContext As BufferedGraphicsContext
    Dim RedTopSpeed, GreenTopSpeed, BlueTopSpeed, BlackTopSpeed As Single


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        g.Dispose()                     ' release the graphics context
        Me.Close()
        Dim RaceResults As New RaceResultsForm
        RaceResults.Show()

    End Sub

    Private Sub RaceClockForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        g = MyBase.CreateGraphics()
        myContext = BufferedGraphicsManager.Current

        SystemConfigurationForm.RedTopSpeed = 0
        SystemConfigurationForm.GreenTopSpeed = 0
        SystemConfigurationForm.BlueTopSpeed = 0
        SystemConfigurationForm.BlackTopSpeed = 0

        SerialPort1.Open()
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()
        SerialPort1.Write("r")
        SerialPort1.Close()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim SerialPortString As String
        Dim RemainingString As String
        Dim TabPosition As Integer
        Dim RedDistance, GreenDistance, BlueDistance, BlackDistance As Integer
        Dim RedSpeed, GreenSpeed, BlueSpeed, BlackSpeed As Single

        Dim RenderBuffer As BufferedGraphics
        RenderBuffer = myContext.Allocate(Me.CreateGraphics, Me.DisplayRectangle)

        Second += 1
        SecondsBox.Text = Math.Round(Second / 5)

        SerialPort1.Open()
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()

        '----------------------------------------------------------
        ' Read the number of rotations on each channel
        '----------------------------------------------------------
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

        BlackDistance = Val("&H" & RemainingString)                          ' remember VB starts indexing at 1
        BlackDistance = BlackDistance * SystemConfigurationForm.BlackRotsToMeters

        '----------------------------------------------------------
        ' Read the number of rotations in the last second on each channel
        '----------------------------------------------------------
        SerialPort1.Write("s")
        SerialPortString = SerialPort1.ReadLine()

        TabPosition = SerialPortString.IndexOf(vbTab)
        RedSpeed = Val("&H0" & Mid(SerialPortString, 1, TabPosition))         ' remember VB starts indexing at 1
        RedSpeed = RedSpeed * SystemConfigurationForm.RedDeltaToMPH
        RemainingString = SerialPortString.Substring(TabPosition + 1, SerialPortString.Length - TabPosition - 1)

        TabPosition = RemainingString.IndexOf(vbTab)
        GreenSpeed = Val("&H0" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        GreenSpeed = GreenSpeed * SystemConfigurationForm.GreenDeltaToMPH
        RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

        TabPosition = RemainingString.IndexOf(vbTab)
        BlueSpeed = Val("&H0" & Mid(RemainingString, 1, TabPosition))         ' remember VB starts indexing at 1
        BlueSpeed = BlueSpeed * SystemConfigurationForm.BlueDeltaToMPH
        RemainingString = RemainingString.Substring(TabPosition + 1, RemainingString.Length - TabPosition - 1)

        BlackSpeed = Val("&H0" & RemainingString)         ' remember VB starts indexing at 1
        BlackSpeed = BlackSpeed * SystemConfigurationForm.BlackDeltaToMPH

        SerialPort1.Close()

        '----------------------------------------------------------
        ' Check for top speed
        '----------------------------------------------------------
        If (RedSpeed > SystemConfigurationForm.RedTopSpeed) Then
            SystemConfigurationForm.RedTopSpeed = RedSpeed
        End If

        If (GreenSpeed > SystemConfigurationForm.GreenTopSpeed) Then
            SystemConfigurationForm.GreenTopSpeed = GreenSpeed
        End If

        If (BlueSpeed > SystemConfigurationForm.BlueTopSpeed) Then
            SystemConfigurationForm.BlueTopSpeed = BlueSpeed
        End If

        If (BlackSpeed > BlackTopSpeed) Then
            SystemConfigurationForm.BlackTopSpeed = BlackSpeed
        End If


        '----------------------------------------------------------
        ' Draw the Race Clock to a render buffer
        '----------------------------------------------------------
        RenderBuffer.Graphics.ResetTransform()
        DrawFace(RenderBuffer)
        DrawNeedle(RedDistance, New SolidBrush(Color.Red), RenderBuffer)

        ' The second rider is drawn in Green
        If (SystemConfigurationForm.NumberOfRacers > 1) Then
            DrawNeedle(GreenDistance, New SolidBrush(Color.Green), RenderBuffer)
        End If

        ' The third rider is drawn in Blue
        If (SystemConfigurationForm.NumberOfRacers > 2) Then
            DrawNeedle(BlueDistance, New SolidBrush(Color.Blue), RenderBuffer)
        End If

        ' the fourth rider is drawn in Black
        If (SystemConfigurationForm.NumberOfRacers = 4) Then
            DrawNeedle(BlackDistance, New SolidBrush(Color.Black), RenderBuffer)
        End If

        '----------------------------------------------------------
        ' Render the buffer
        '----------------------------------------------------------
        RenderBuffer.Render()
        RenderBuffer.Dispose()

    End Sub

    Private Sub DrawNeedle(ByVal distance As Integer, ByVal color As SolidBrush, ByRef RenderBuff As BufferedGraphics)

        Const CenterBallDiameter = CInt(0.06 * BoxSize)
        Const CounterBallDiameter = CInt(0.06 * BoxSize)
        Const RearStart = CInt(0.025 * BoxSize)
        Const RearEnd = CInt(0.02 * BoxSize)
        Const FrontStart = CInt(0.025 * BoxSize)
        Const FrontEnd = CInt(0.015 * BoxSize)
        Const RearLength = CInt(0.23 * BoxSize)
        Const FrontLength = CInt(0.35 * BoxSize)
        Const ArrowBase = CInt(0.028 * BoxSize)
        Const ArrowHeight = CInt(0.071 * BoxSize)

        RenderBuff.Graphics.ResetTransform()
        RenderBuff.Graphics.TranslateTransform(CenterX + XOFF, CenterY + YOFF)
        RenderBuff.Graphics.RotateTransform(360 * distance / 500)

        ' Draw the outlining octagon filled with red
        Dim NeedlePath As New System.Drawing.Drawing2D.GraphicsPath()

        ' Draw the front end of the needle
        NeedlePath.StartFigure()
        NeedlePath.AddLine(-FrontStart, 0, -FrontEnd, -FrontLength)
        NeedlePath.AddLine(-FrontEnd, -FrontLength, FrontEnd, -FrontLength)
        NeedlePath.AddLine(FrontEnd, -FrontLength, FrontStart, 0)
        NeedlePath.AddLine(FrontStart, 0, -FrontStart, 0)

        ' Draw the rear end of the needle
        NeedlePath.StartFigure()
        NeedlePath.AddLine(-RearStart, 0, -RearEnd, RearLength)
        NeedlePath.AddLine(-RearEnd, RearLength, RearEnd, RearLength)
        NeedlePath.AddLine(RearEnd, RearLength, RearStart, 0)
        NeedlePath.AddLine(RearStart, 0, -RearStart, 0)

        ' Draw the arrow head of the needle
        NeedlePath.StartFigure()
        NeedlePath.AddLine(-ArrowBase, -FrontLength, 0, -FrontLength - ArrowHeight)
        NeedlePath.AddLine(0, -1 * FrontLength - ArrowHeight, ArrowBase, -FrontLength)
        NeedlePath.AddLine(ArrowBase, -FrontLength, -ArrowBase, -FrontLength)
        NeedlePath.CloseFigure()

        RenderBuff.Graphics.FillPath(color, NeedlePath)

        ' Draw the center ball
        RenderBuff.Graphics.FillEllipse(color, -CenterBallDiameter, -CenterBallDiameter, 2 * CenterBallDiameter, 2 * CenterBallDiameter)

        'Draw the counter weight ball
        RenderBuff.Graphics.FillEllipse(color, -CounterBallDiameter, RearLength - CounterBallDiameter, 2 * CounterBallDiameter, 2 * CounterBallDiameter)

        RenderBuff.Graphics.ResetTransform()

    End Sub

    Private Sub DrawFace(ByRef RenderBuff As BufferedGraphics)

        Const A = CInt(0.414213 * BoxSize)
        Const B = CInt(0.292893 * BoxSize)

        Dim myFont As New Font("Times New Roman", 24)
        Dim mybrush As New SolidBrush(Color.Black)

        RenderBuff.Graphics.ResetTransform()
        RenderBuff.Graphics.TranslateTransform(XOFF, YOFF)

        ' Draw the outlining octagon filled with red
        Dim OctPath As New System.Drawing.Drawing2D.GraphicsPath()
        OctPath.StartFigure()
        OctPath.AddLine(B, 0, A + B, 0)
        OctPath.AddLine(A + B, 0, BoxSize, B)
        OctPath.AddLine(BoxSize, B, BoxSize, A + B)
        OctPath.AddLine(BoxSize, A + B, A + B, BoxSize)
        OctPath.AddLine(A + B, BoxSize, B, BoxSize)
        OctPath.AddLine(B, BoxSize, 0, A + B)
        OctPath.AddLine(0, A + B, 0, B)
        OctPath.AddLine(0, B, B, 0)
        OctPath.CloseFigure()
        RenderBuff.Graphics.FillPath(New SolidBrush(Color.Red), OctPath)

        ' Remove the clock face drawn in white
        RenderBuff.Graphics.FillEllipse(New SolidBrush(Color.White), CenterX - RadiusMajor, CenterY - RadiusMajor, 2 * RadiusMajor, 2 * RadiusMajor)

        ' Outline the white face in black, because it looks nice
        RenderBuff.Graphics.DrawEllipse(New Pen(Color.Black), CenterX - RadiusMajor, CenterY - RadiusMajor, 2 * RadiusMajor, 2 * RadiusMajor)

        'Draw the smaller black circle on the face
        RenderBuff.Graphics.DrawEllipse(New Pen(Color.Black), CenterX - RadiusMinor, CenterY - RadiusMinor, 2 * RadiusMinor, 2 * RadiusMinor)

        ' Draw bottom pin
        Dim BottomPinPath As New System.Drawing.Drawing2D.GraphicsPath()
        BottomPinPath.StartFigure()
        BottomPinPath.AddLine(CenterX - PinWidth, CenterY + RadiusMajor + 1, CenterX + PinWidth, CenterY + RadiusMajor + 1)
        BottomPinPath.AddLine(CenterX + PinWidth, CenterY + RadiusMajor + 1, CenterX, CenterY + RadiusMajor - PinHeight)
        BottomPinPath.AddLine(CenterX, CenterY + RadiusMajor - PinHeight, CenterX - PinWidth, CenterY + RadiusMajor + 1)
        BottomPinPath.CloseFigure()
        RenderBuff.Graphics.FillPath(New SolidBrush(Color.Red), BottomPinPath)

        ' Draw top pin
        Dim TopPinPath As New System.Drawing.Drawing2D.GraphicsPath()
        TopPinPath.StartFigure()
        TopPinPath.AddLine(CenterX - PinWidth, CenterY - RadiusMajor, CenterX + PinWidth, CenterY - RadiusMajor)
        TopPinPath.AddLine(CenterX + PinWidth, CenterY - RadiusMajor, CenterX, CenterY - RadiusMajor + PinHeight)
        TopPinPath.AddLine(CenterX, CenterY - RadiusMajor + PinHeight, CenterX - PinWidth, CenterY - RadiusMajor)
        TopPinPath.CloseFigure()
        RenderBuff.Graphics.FillPath(New SolidBrush(Color.Red), TopPinPath)

        ' Draw left pin
        Dim LeftPinPath As New System.Drawing.Drawing2D.GraphicsPath()
        LeftPinPath.StartFigure()
        LeftPinPath.AddLine(CenterX - RadiusMajor, CenterY - PinWidth, CenterX - RadiusMajor, CenterY + PinWidth)
        LeftPinPath.AddLine(CenterX - RadiusMajor, CenterY + PinWidth, CenterX - RadiusMajor + PinHeight, CenterY)
        LeftPinPath.AddLine(CenterX - RadiusMajor + PinHeight, CenterY, CenterX - RadiusMajor, CenterY - PinWidth)
        LeftPinPath.CloseFigure()
        RenderBuff.Graphics.FillPath(New SolidBrush(Color.Red), LeftPinPath)

        ' Draw right pin
        Dim RightPinPath As New System.Drawing.Drawing2D.GraphicsPath()
        RightPinPath.StartFigure()
        RightPinPath.AddLine(CenterX + RadiusMajor + 1, CenterY - PinWidth, CenterX + RadiusMajor + 1, CenterY + PinWidth)
        RightPinPath.AddLine(CenterX + RadiusMajor + 1, CenterY + PinWidth, CenterX + RadiusMajor - PinHeight, CenterY)
        RightPinPath.AddLine(CenterX + RadiusMajor - PinHeight, CenterY, CenterX + RadiusMajor + 1, CenterY - PinWidth)
        RightPinPath.CloseFigure()
        RenderBuff.Graphics.FillPath(New SolidBrush(Color.Red), RightPinPath)
        
        ' Draw the numbers on the dial
        RenderBuff.Graphics.DrawString("125", myFont, mybrush, CenterX + RadiusMajor - 70, CenterY - 20)
        RenderBuff.Graphics.DrawString("250", myFont, mybrush, CenterX - 30, CenterY + RadiusMajor - 45)
        RenderBuff.Graphics.DrawString("375", myFont, mybrush, CenterX - RadiusMajor + 15, CenterY - 20)
        RenderBuff.Graphics.DrawString("500", myFont, mybrush, CenterX - 30, CenterY - RadiusMajor + 10)
        RenderBuff.Graphics.ResetTransform()
    End Sub
End Class