' @description    Visual Basic form for communicating with Arduino Uno
'                 Arduino communicates with Azoteq IQS316 Capactivie Touch Module
' @author         Matt Popovich (popovivch.matt@gmail.com)
' @version        0.1
' @created on     February 19, 2016
' @last modified  February 19, 2016


' With help from: http://www.instructables.com/id/Using-Visual-Basic-to-control-Arduino-Uno/
' But mainly from: http://www.martyncurrey.com/arduino-and-visual-basic-part-1-receiving-data-from-the-arduino/


Imports System.IO
Imports System.IO.Ports


Public Class MainForm


    Dim comPort As String
    Dim receivedData As String = ""


    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Have the user select which COM port the ardunio is plugged into via pop-up window
        'MessageBox.Show("Which COM port is the Arduino connected to?", "Input COM Port", MessageBoxButtons.OK, MessageBoxIcon.Question)
        '       look at this article when i can show list boxes in message boxes: https://msdn.microsoft.com/en-us/library/9wahf8t8.aspx

        Dim returnStr As String = ""

        'SerialPort1.Close()

        Try
            'SerialPort1.Open()
            SerialPort1.Close()
            SerialPort1.PortName = "COM31" 'change com port to match your Arduino port
            SerialPort1.BaudRate = 9600
            SerialPort1.DataBits = 8
            SerialPort1.Parity = Parity.None
            SerialPort1.StopBits = StopBits.One
            SerialPort1.Handshake = Handshake.None
            SerialPort1.Encoding = System.Text.Encoding.Default 'very important!
            SerialPort1.ReadTimeout = 1000     'Delay in ms

            SerialPort1.Open()
            MessageBox.Show("COM Port Configured!", "SUCCESS")

        Catch ex As Exception
            MessageBox.Show("Error: Serial Port read timed out.", "ERROR")

        End Try

        'MessageBox.Show("COM Port Configured!", "SUCCESS")


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        writeData("1")


        Dim read As String = receiveData()

        MessageBox.Show(read)


    End Sub



    Function receiveData() As String
        ' Receive strings from a serial port
        '   With help from: https://msdn.microsoft.com/en-us/library/7ya7y41k.aspx

        Dim returnStr As String = ""

        Try
            writeData("1")

            Do
                Dim Incoming As String = SerialPort1.ReadLine()
                If Incoming Is Nothing Then
                    Exit Do
                Else
                    returnStr &= Incoming & vbCrLf
                End If
            Loop

        Catch ex As TimeoutException
            returnStr = "Error: Could not open serial port."

        End Try

        Return returnStr

    End Function


    Sub writeData(ByVal data As String)

        'Send strings to a serial port
        '   With help from: https://msdn.microsoft.com/en-us/library/088fx85y.aspx

        Try
            'SerialPort1.Open()
            SerialPort1.Write(data)
            'SerialPort1.Close()

        Catch ex As Exception
            MessageBox.Show("Error: Could not write to serial port.", "ERROR")
        End Try


    End Sub


    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub
End Class



