' @description    Visual Basic form for communicating with Arduino Uno
'                 Arduino communicates with Azoteq IQS316 Capactivie Touch Module
' @author         Matt Popovich (popovivch.matt@gmail.com)
' @version        0.1
' @created on     February 19, 2016
' @last modified  February 19, 2016


' With help from: http://www.instructables.com/id/Using-Visual-Basic-to-control-Arduino-Uno/
Imports System.IO
Imports System.IO.Ports
Imports System.Threading


Public Class MainForm

    Shared _continue As Boolean
    Shared SerialPort1 As SerialPort



    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Have the user select which COM port the ardunio is plugged into via pop-up window
        'MessageBox.Show("Which COM port is the Arduino connected to?", "Input COM Port", MessageBoxButtons.OK, MessageBoxIcon.Question)




        SerialPort1.Close()
        SerialPort1.PortName = "com10" 'change com port to match your Arduino port
        SerialPort1.BaudRate = 9600
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = System.Text.Encoding.Default 'very important!

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        SerialPort1.Open()
        SerialPort1.Write("1")
        SerialPort1.Close()

    End Sub
End Class
