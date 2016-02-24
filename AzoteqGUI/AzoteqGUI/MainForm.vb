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


    Dim comPort As String = ""
    Dim receivedData As String = ""
    Dim TIMEOUT As Integer = 1000


    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For Each _serialPort As String In My.Computer.Ports.SerialPortNames
            comCOM.Items.Add(_serialPort)
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        writeData("1")


        Dim read As String = receiveData()

        MessageBox.Show(read)


        writeData("2")


        read = receiveData()

        MessageBox.Show(read)


    End Sub


    ' Returns data from the serial port up to the newline character. 
    '   If no data is put on the serial port for 'TIMEOUT', returns "Error: Serial Port time out."
    Function receiveData() As String
        ' Receive strings from a serial port
        '   With help from: https://msdn.microsoft.com/en-us/library/7ya7y41k.aspx

        Dim returnStr As String = ""

        Try

            Dim Incoming As String = SerialPort1.ReadLine()
            Do
                If Incoming Is Nothing Then
                    ' Do nothing and poll for data
                Else
                    returnStr &= Incoming & vbCrLf
                    Exit Do
                End If
            Loop

        Catch ex As TimeoutException
            returnStr = "Error: Serial Port time out."
        Catch ex As InvalidOperationException
            returnStr = "Error: Serial Port is closed."
        End Try

        ' Add returnStr to console
        writeConsole(returnStr)

        Return returnStr

    End Function


    ' Writes 'data' to the serial port without appending a newline character
    Sub writeData(ByVal data As String)

        'Send strings to a serial port
        '   With help from: https://msdn.microsoft.com/en-us/library/088fx85y.aspx

        Try
            SerialPort1.Write(data)     'Does not append newline character
        Catch ex As Exception
            MessageBox.Show("Error: Could not write to serial port.", "ERROR")
        End Try

        ' Add data to console
        writeConsole(data)

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

            Catch ex As Exception
                chkConnected.Checked = False
                MessageBox.Show("Error: Serial Port read timed out.", "ERROR")
            End Try

        End If
    End Sub

    ' Timer tick event every second
    Private Sub tmrConnected_Tick(sender As Object, e As EventArgs) Handles tmrConnected.Tick

        ' Check SerialPort connection every second
        chkConnected.Checked = SerialPort1.IsOpen

    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        'Test functions/interacting with other controls
        writeConsole("Hello")
        writeConsole("hola")
        writeConsole("chaio")
    End Sub

    ' Write to console and automatically scroll
    Sub writeConsole(ByVal data As String)
        lstConsole.Items.Add(data)
        lstConsole.TopIndex = lstConsole.Items.Count - 1 - CInt(lstConsole.ItemHeight / 2)
    End Sub

End Class



