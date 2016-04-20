' @description    Visual Basic form for communicating with Arduino Uno
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
    Dim channels(20) As Integer


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
            MessageBox.Show("Could not write to serial port.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        If ((SerialPort1.IsOpen = False) And (CBool(chkConnected.CheckState) = True)) Then
            ' If serial port drops connection
            writeConsoleNewline("ERROR: Connection on " & comPort & " has been lost.")
        ElseIf ((SerialPort1.IsOpen = True) And (CBool(chkConnected.CheckState) = False)) Then
            ' If serial port gains connection
            chkConnected.Checked = SerialPort1.IsOpen
        End If

        If (chkConnected.Checked And chkPrinting.Checked) Then
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
            End Try

        End If

    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs)
        'Test functions/interacting with other controls
        writeConsoleNewline(CStr(Keys.Enter))
        writeConsoleNewline(CStr(Keys.Return))
    End Sub

    ' Automatically scroll the list box
    Sub scrollConsole()
        If (chkScrolling.Checked) Then
            lstConsole.TopIndex = lstConsole.Items.Count - 1 - CInt(lstConsole.ItemHeight / 2)
        End If
    End Sub

    ' Write to console with a newline and automatically scroll
    Sub writeConsoleNewline(ByVal data As String)
        lstConsole.Items.Add(data)
        scrollConsole()

        ' TODO: Write all data received via serial port to console (think interrupt)
        '       (not just stuff I want to read and write)
    End Sub

    ' Write to console without a newline and automatically scroll
    Sub writeConsole(ByVal data As String)
        Dim last As String
        last = CStr(lstConsole.Items(lstConsole.Items.Count - 1))
        ' Remove the last item in the listbox
        lstConsole.Items.RemoveAt(lstConsole.Items.Count - 1)
        ' Add the last line back to listbox + new byte
        lstConsole.Items.Add(CStr(last) + data)

        scrollConsole()
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
        If (e.KeyCode = Keys.Enter) Then
            btnWrite.PerformClick()
        End If
    End Sub

    'Sub animate()
    Private Sub MainForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

        ' Fill the circle with the same color as its border.
        'e.Graphics.FillEllipse(Brushes.Black, Sensors.one.x, Sensors.one.y, 20, 20)
        'e.Graphics.FillEllipse(Brushes.Black, Sensors.two.x, Sensors.two.y, 20, 20)

        'The center coordinate
        Const x = 530
        Const y = 200

        ' sensor(#, 0 = x; 1 = y)
        Dim sensors(,) As Integer = {
            {x - 110, y - 140},
            {x - 110, y - 110},
            {x - 110, y - 80},
            {x - 110, y - 50},
            {x - 110, y - 20},
            {x - 110, y + 10},
            {x - 110, y + 40},
            {x - 60, y + 40},
            {x, y + 40},
            {x + 50, y + 40},
            {x + 50, y + 10},
            {x + 50, y - 20},
            {x + 50, y - 50},
            {x + 50, y - 80},
            {x + 50, y - 110},
            {x + 50, y - 140}
        }

        Dim maxDim0 As Integer = UBound(sensors, 1)
        Dim maxDim1 As Integer = UBound(sensors, 2)

        For i As Integer = 0 To maxDim0
            ' Credit: https://www.youtube.com/watch?v=EqIXayguHUc 
            ' 0 <= Rnd() <= 1
            'Color.FromArgb(red, green, blue)

            ' Count = 0 - 1000
            Dim c As Color = Color.FromArgb(CInt(255 / (16 / i)), CInt(255 / (16 / i)), 0)
            Dim br As New SolidBrush(c)

            e.Graphics.FillEllipse(br, sensors(i, 0), sensors(i, 1), 20, 20)

        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        writeConsoleNewline("This button does nothing.")
        writeConsoleNewline("Curiosity killed the cat?")

    End Sub

    Private Sub chkScrolling_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrolling.CheckedChanged
        chkScrolling.Checked = scrolling
        scrolling = Not scrolling
    End Sub

    Private Sub chkPrinting_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrinting.CheckedChanged
        chkPrinting.Checked = printing
        printing = Not printing
    End Sub

    Sub b(ByVal i As Integer)

        While (SerialPort1.BytesToRead <> 0) ' aka !=
            'Wait here until the buffer is empty
        End While

        printing = False    'Pause printing so we can read the next 9 bytes
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
            Try

                channels(j) = (SerialPort1.ReadByte)          ' shift HI one byte left
                channels(j) = channels(j) << 8
                channels(j) = channels(j) Or SerialPort1.ReadByte ' bitwise or
                writeConsoleNewline(CStr(channels(j)))              ' print to lstConsole

            Catch ex As TimeoutException
                writeConsoleNewline("Error: Serial Port time out.")
            Catch ex As InvalidOperationException
                writeConsoleNewline("Error: Serial Port is closed.")
            End Try
        Next

        ' TODO: Refresh form

        'Resume printing
        printing = True

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
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lstConsole.Items.Clear()
    End Sub

End Class



