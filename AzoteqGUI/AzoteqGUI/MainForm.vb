﻿' @description    Visual Basic form for communicating with Arduino Uno
'                 Arduino communicates with Azoteq IQS316 Capactivie Touch Module
' @author         Matt Popovich (popovivch.matt@gmail.com)
' @version        0.1
' @created on     February 19, 2016
' @last modified  February 23, 2016


' With help from: http://www.instructables.com/id/Using-Visual-Basic-to-control-Arduino-Uno/
' But mainly from: http://www.martyncurrey.com/arduino-and-visual-basic-part-1-receiving-data-from-the-arduino/


Option Strict On    'So Visual Basic won't automatically convert variables

Imports System.IO
Imports System.IO.Ports




Public Class MainForm

    ' Global Variables
    Dim comPort As String = ""
    Dim receivedData As String = ""
    Dim TIMEOUT As Integer = 1000
    Dim scrolling As Boolean = True
    Dim printing As Boolean = True
    Dim reading As Boolean = True
    Dim channels(20) As Integer
    Friend WithEvents MaskBox As System.Windows.Forms.PictureBox

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Populate COMs combo box
        For Each _serialPort As String In My.Computer.Ports.SerialPortNames
            comCOM.Items.Add(_serialPort)
        Next

        ' Set checkboxes
        chkScrolling.Checked = scrolling
        chkPrinting.Checked = printing

        ' Initialize array
        For i As Integer = 0 To 19
            channels(i) = 0
        Next

    End Sub

    ' Writes 'data' to the serial port without appending a newline character
    Sub writeData(ByVal data As String)

        'Send strings to a serial port
        '   With help from: https://msdn.microsoft.com/en-us/library/088fx85y.aspx

        Try
            SerialPort1.Write(data)     'Does not append newline character
        Catch ex As Exception
            MessageBox.Show("Could not write to serial port: " & ex.Message & ".", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Add data to console
        writeConsoleNewline(data)
        writeConsoleNewline("")

    End Sub

    ' When the user selects a new COM Port
    Private Sub comCOM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comCOM.SelectedIndexChanged

        If (CStr(comCOM.SelectedItem) = "") Then
            chkConnected.Checked = False
        Else
            comPort = CStr(comCOM.SelectedItem)

            Try
                SerialPort1.Close()
                SerialPort1.PortName = comPort          'Try user selected port for Arduino
                SerialPort1.BaudRate = 9600
                SerialPort1.DataBits = 8
                SerialPort1.Parity = Parity.None
                SerialPort1.StopBits = StopBits.One
                SerialPort1.Handshake = Handshake.None
                SerialPort1.Encoding = System.Text.Encoding.Default
                SerialPort1.ReadTimeout = TIMEOUT       'Delay in ms

                SerialPort1.Open()
                chkConnected.Checked = True
                writeConsoleNewline("Connected via " & comPort)
                writeConsoleNewline("")

                SerialPort1.DtrEnable = True            ' Reset the Arduino (hardware reset)

            Catch ex As Exception
                chkConnected.Checked = False
                writeConsoleNewline("Connection via " & comPort & " has failed: timeout.")
                MessageBox.Show("Serial Port read timed out.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
    End Sub

    ' Timer tick event every second
    Private Sub tmrConnected_Tick(sender As Object, e As EventArgs) Handles tmrConnected.Tick

        ' Check SerialPort connection every second
        If ((SerialPort1.IsOpen = False) And (chkConnected.Checked = True)) Then
            ' If serial port drops connection
            writeConsoleNewline("ERROR: Connection on " & comPort & " has been lost.")
            chkConnected.Checked = False
        ElseIf ((SerialPort1.IsOpen = True) And (chkConnected.Checked = False)) Then
            ' If serial port gains connection
            chkConnected.Checked = SerialPort1.IsOpen
        End If

        If (chkConnected.Checked And reading) Then
            ' COM Port is connected and user wants it printed to console
            '   Let's see if it has any newdata

            Try

                While (SerialPort1.BytesToRead > 0)

                    Dim readByte As Byte = CByte(SerialPort1.ReadByte)
                    If (readByte = 10) Then
                        ' Read byte is a newline
                        writeConsoleNewline("")
                    Else
                        ' Read byte is not a newline, append it to the last line in the listbox
                        writeConsole(ChrW(readByte))
                    End If

                End While

            Catch ex As TimeoutException
                writeConsoleNewline("Error: Serial Port time out.")
            Catch ex As InvalidOperationException
                writeConsoleNewline("Error: Serial Port is closed.")
            Catch ex As Exception
                writeConsoleNewline("Error: Unknown error: " & ex.Message)
            End Try

        End If

    End Sub

    ' Automatically scroll the list box
    Sub scrollConsole()
        If (chkScrolling.Checked) Then
            lstConsole.TopIndex = lstConsole.Items.Count - 1 - CInt(lstConsole.ItemHeight / 2)
        End If
    End Sub

    ' Write to console with a newline and automatically scroll
    Sub writeConsoleNewline(ByVal data As String)

        If (chkPrinting.Checked) Then
            lstConsole.Items.Add(data)
            scrollConsole()
        End If

        ' TODO: Write all data received via serial port to console (think interrupt)
        '       (not just stuff I want to read and write)
    End Sub

    ' Write to console without a newline and automatically scroll
    Sub writeConsole(ByVal data As String)
        If (chkPrinting.Checked) Then
            Dim last As String
            last = CStr(lstConsole.Items(lstConsole.Items.Count - 1))
            ' Remove the last item in the listbox
            lstConsole.Items.RemoveAt(lstConsole.Items.Count - 1)
            ' Add the last line back to listbox + new byte
            lstConsole.Items.Add(CStr(last) + data)

            scrollConsole()
        End If
    End Sub

    ' When the user clicks on the 'Write' button
    Private Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
        If (CStr(comCOM.SelectedItem) = "") Then
            MessageBox.Show("Please select a COM Port first", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            writeData(txtWrite.Text)
            txtWrite.Text = ""
            'receiveData()
        End If

        'TODO: Add disconnect button for COM
    End Sub

    ' When the user presses enter in the 'Write' text box
    Private Sub txtWrite_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWrite.KeyDown
        ' Make sure user pressed the enter key and not any key
        If (e.KeyCode = Keys.Return) Then
            btnWrite.PerformClick()
        End If
    End Sub

    Private Sub drawMask()
        Dim maskImg As Image = My.Resources.mask

        Dim g As System.Drawing.Graphics
        MaskBox.Refresh()
        g = MaskBox.CreateGraphics()

        'Draw the mask base image
        Dim maskWidth As Integer = 200
        Dim maskHeight As Integer = CInt(maskImg.Height * (maskWidth / maskImg.Width))
        g.DrawImage(maskImg, 0, 0, maskWidth, maskHeight)
    End Sub

    Private Sub drawPads()
        Dim MAPPING() As Integer = {14, 15, 12, 13, 10, 11, 17, 16, 19, 18, 8, 9, 6, 7, 4, 5}

        Dim maskImg As Image = My.Resources.mask

        Dim g As System.Drawing.Graphics
        g = MaskBox.CreateGraphics()

        'Draw the mask base image
        Dim maskWidth As Integer = 200
        Dim maskHeight As Integer = CInt(maskImg.Height * (maskWidth / maskImg.Width))

        'The center coordinate
        Dim x As Integer = CInt(maskWidth / 2)
        Dim y As Integer = CInt(maskHeight / 2)

        ' sensor(#, 0 = x; 1 = y)
        Dim sensors(,) As Integer = {
            {x - 25, y - 80},
            {x - 30, y - 55},
            {x - 40, y - 30},
            {x - 60, y - 5},
            {x - 75, y + 20},
            {x - 85, y + 45},
            {x - 75, y + 80},
            {x - 30, y + 100},
            {x + 30, y + 100},
            {x + 75, y + 80},
            {x + 85, y + 45},
            {x + 75, y + 20},
            {x + 60, y - 5},
            {x + 40, y - 30},
            {x + 30, y - 55},
            {x + 25, y - 80}
        }

        Dim maxDim0 As Integer = UBound(sensors, 1)
        Dim maxDim1 As Integer = UBound(sensors, 2)

        For i As Integer = 0 To maxDim0
            ' Credit: https://www.youtube.com/watch?v=EqIXayguHUc 
            ' 0 <= Rnd() <= 1
            'Color.FromArgb(red, green, blue)

            ' Count = 0 - 1000
            Dim count As Integer = channels(MAPPING(i))
            Dim red, green As Integer

            If (count > 1000) Then
                count = 1000
            End If
            red = CInt(count / 392)
            green = CInt((count - 275) / 2.84)
            If (green < 0) Then
                green = 0
            End If


            Dim c As Color = Color.FromArgb(green, 255 - green, 0)
            Dim br As New SolidBrush(c)

            g.FillEllipse(br, sensors(i, 0) - 10, sensors(i, 1) - 10, 20, 20)
        Next
    End Sub

    Private Sub chkScrolling_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrolling.CheckedChanged
        scrolling = chkScrolling.Checked
    End Sub

    Private Sub chkPrinting_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrinting.CheckedChanged
        printing = chkPrinting.Checked
    End Sub

    Sub b(ByVal i As Integer)
        While (SerialPort1.BytesToRead <> 0) ' aka !=
            'Wait here until the buffer is empty
        End While

        Dim command As String = "b" & CStr(i)
        writeData(command)
        ' b1 returns channels 4,5,6,7
        ' b2 returns channels 8,9,10,11
        ' b3 returns channels 12,13,14,15
        ' b4 returns channels 16,17,18,19

        ' Verify group number
        Dim group As Integer = SerialPort1.ReadByte
        writeConsoleNewline(CStr(group))
        If (group <> i) Then    ' aka !=
            writeConsoleNewline("ERROR: Received " & CStr(group) & " when expecting " & CStr(i) & ".")
            writeConsoleNewline("       Aborting this command.")
            Exit Sub
        End If

        'Read next 8 bytes
        For j As Integer = (4 * i) To ((4 * i) + 3)
            channels(j) = (SerialPort1.ReadByte)          ' shift HI one byte left
            channels(j) = channels(j) << 8
            channels(j) = channels(j) Or SerialPort1.ReadByte ' bitwise or
            writeConsoleNewline(CStr(channels(j)))              ' print to lstConsole
        Next
    End Sub


    Private Sub btnb1_Click(sender As Object, e As EventArgs) Handles btnb1.Click
        b(1)
    End Sub

    Private Sub btnb2_Click(sender As Object, e As EventArgs) Handles btnb2.Click
        b(2)
    End Sub

    Private Sub btnb3_Click(sender As Object, e As EventArgs) Handles btnb3.Click
        b(3)
    End Sub

    Private Sub btnb4_Click(sender As Object, e As EventArgs) Handles btnb4.Click
        b(4)
    End Sub

    Private Sub btnb_Click(sender As Object, e As EventArgs) Handles btnb.Click
        For i As Integer = 1 To 4
            b(i)
        Next
        drawPads()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lstConsole.Items.Clear()
    End Sub

    Private Sub btnt1_Click(sender As Object, e As EventArgs) Handles btnt1.Click
        While (SerialPort1.BytesToRead <> 0) ' aka !=
            'Wait here until the buffer is empty
        End While

        printing = False    'Pause printing so we can read the next 9 bytes
        writeData("t1")

        'Resume printing
        printing = True
    End Sub

    Private Sub MainForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        ' Initialize mask picture
        drawMask()
    End Sub

    Private Sub btnbLoop_Click(sender As Object, e As EventArgs) Handles btnbLoop.Click
        lstConsole.Items.Add("Started sampling...")
        ' Runs for about 15 seconds. 
        Try
            For j As Integer = 0 To 80
                For i As Integer = 1 To 4
                    b(i)
                Next
                drawPads()
            Next
        Catch ex As TimeoutException
            writeConsoleNewline("Error: Serial Port time out.")
        Catch ex As InvalidOperationException
            writeConsoleNewline("Error: Serial Port is closed.")
        Catch ex As System.IO.IOException
            writeConsoleNewline("Error: USB Cable has become unplugged.")
        Catch ex As Exception
            writeConsoleNewline("Error: Exception " & ex.Message & " occurred in b() function.")
        End Try
        lstConsole.Items.Add("done!")
    End Sub
End Class



