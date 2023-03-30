Public Class Form1
    Dim all18Bytes(17) As Byte
    Dim directModeBool, stabalizeModeBool, offModeBool As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SerialPort1.PortName = "COM5" 'name serial port
        SerialPort1.BaudRate = 57600  'set baud rate 19.2k
        SerialPort1.DataBits = 8 'number of data bits is 8
        SerialPort1.StopBits = IO.Ports.StopBits.One 'one stop bit
        SerialPort1.Parity = IO.Ports.Parity.None 'no parity bits
        SerialPort1.Open() 'intialize and open port
        Timer1.Enabled = True 'enable timer 1 on form

        RadioButton1.Checked = True
        RadioButton2.Checked = False
        RadioButton3.Checked = False

        directionalButtonsOff()

        offModeEnabled()


        all18Bytes(0) = &H55
        all18Bytes(1) = &HAA
        all18Bytes(2) = &H7
        all18Bytes(3) = &H4D
        all18Bytes(4) = &H0
        all18Bytes(5) = &H1
        all18Bytes(6) = &H0
        all18Bytes(7) = &H98
        all18Bytes(8) = &H0
        all18Bytes(8) = &H0
        all18Bytes(10) = &H0
        all18Bytes(11) = &H0
        all18Bytes(12) = &H0
        all18Bytes(13) = &H0
        all18Bytes(14) = &H0
        all18Bytes(15) = &H0
        all18Bytes(16) = &H0
        all18Bytes(17) = &H0

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            RadioButton2.Checked = False
            RadioButton3.Checked = False

            offModeEnabled()
        End If

        upButton.Enabled = False
        downButton.Enabled = False
        leftButton.Enabled = False
        rightButton.Enabled = False
    End Sub
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            RadioButton1.Checked = False
            RadioButton3.Checked = False

            directModeEnabled()
        End If

        directionalButtonsOn()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            RadioButton1.Checked = False
            RadioButton2.Checked = False

            stabalizeModeEnabled()
        End If

        directionalButtonsOn()
    End Sub

    Private Sub directionalButtonsOn()
        upButton.Enabled = True
        downButton.Enabled = True
        leftButton.Enabled = True
        rightButton.Enabled = True
    End Sub

    Private Sub directionalButtonsOff()
        upButton.Enabled = False
        downButton.Enabled = False
        leftButton.Enabled = False
        rightButton.Enabled = False
    End Sub

    Private Sub offModeEnabled()
        offModeBool = True
        directModeBool = False
        stabalizeModeBool = False
        all18Bytes(10) = &H0
    End Sub

    Private Sub directModeEnabled()
        offModeBool = False
        directModeBool = True
        stabalizeModeBool = False
        'all18Bytes(10) = &H10
    End Sub

    Private Sub stabalizeModeEnabled()
        offModeBool = False
        directModeBool = False
        stabalizeModeBool = True
        'all18Bytes(10) = &H20
    End Sub

    Private Sub leftButton_Click(sender As Object, e As EventArgs) Handles leftButton.Click
        controlGimbalByte9()

        If directModeBool = True Then
            directBytes10()
            directLeftBytes1617()
        ElseIf stabalizeModeBool = True Then
            stabalizeBytes10()
            stabalizeLeftBytes1617()
        End If

        controlGimbalBytes11()
        LeftBytes1213()
        LeftRightBytes1415()

        SerialPort1.Write(all18Bytes, 0, 18)
    End Sub

    Private Sub rightButton_Click(sender As Object, e As EventArgs) Handles rightButton.Click
        controlGimbalByte9()

        If directModeBool = True Then
            directBytes10()
            directRightBytes1617()
        ElseIf stabalizeModeBool = True Then
            stabalizeBytes10()
            stabalizeRightBytes1617()
        End If

        controlGimbalBytes11()
        RightBytes1213()
        LeftRightBytes1415()

        SerialPort1.Write(all18Bytes, 0, 18)
    End Sub

    Private Sub upButton_Click(sender As Object, e As EventArgs) Handles upButton.Click
        controlGimbalByte9()

        If directModeBool = True Then
            directBytes10()
            directUpBytes16()
            directUpDownBytes17()
        ElseIf stabalizeModeBool = True Then
            stabalizeBytes10()
            stabalizeUpBytes16()
            stabalizeUpDownBytes17()
        End If
        controlGimbalBytes11()
        upDownBytes1213()
        upBytes1415()


        SerialPort1.Write(all18Bytes, 0, 18)
    End Sub

    Private Sub downButton_Click(sender As Object, e As EventArgs) Handles downButton.Click
        controlGimbalByte9()

        If directModeBool = True Then
            directBytes10()
            directDownBytes16()
            directUpDownBytes17()
        ElseIf stabalizeModeBool = True Then
            stabalizeBytes10()
            stabalizeDownBytes16()
            stabalizeUpDownBytes17()
        End If
        controlGimbalBytes11()
        upDownBytes1213()
        downBytes1415()

        SerialPort1.Write(all18Bytes, 0, 18)
    End Sub

    Private Sub controlGimbalByte9()
        all18Bytes(9) = &H47
    End Sub

    Private Sub directBytes10()
        all18Bytes(10) = &H10
    End Sub

    Private Sub stabalizeBytes10()
        all18Bytes(10) = &H20
    End Sub

    Private Sub controlGimbalBytes11()
        all18Bytes(11) = &H0
    End Sub

    Private Sub LeftBytes1213()
        all18Bytes(12) = &H97
        all18Bytes(13) = &H7C
    End Sub

    Private Sub RightBytes1213()
        all18Bytes(12) = &H69
        all18Bytes(13) = &H83
    End Sub

    Private Sub LeftRightBytes1415()
        all18Bytes(14) = &H0
        all18Bytes(15) = &H80
    End Sub

    Private Sub directLeftBytes1617()
        all18Bytes(16) = &H2F
        all18Bytes(17) = &HFB
    End Sub

    Private Sub stabalizeLeftBytes1617()
        all18Bytes(16) = &HDF
        all18Bytes(17) = &HFE
    End Sub

    Private Sub directRightBytes1617()
        all18Bytes(16) = &HF7
        all18Bytes(17) = &HFA
    End Sub

    Private Sub stabalizeRightBytes1617()
        all18Bytes(16) = &H7
        all18Bytes(17) = &HFF
    End Sub

    Private Sub upDownBytes1213()
        all18Bytes(12) = &H0
        all18Bytes(13) = &H80
    End Sub

    Private Sub upBytes1415()
        all18Bytes(14) = &H97
        all18Bytes(15) = &H7C
    End Sub

    Private Sub directUpBytes16()
        all18Bytes(16) = &H1A
    End Sub

    Private Sub stabalizeUpBytes16()
        all18Bytes(16) = &HEA
    End Sub

    Private Sub downBytes1415()
        all18Bytes(14) = &H69
        all18Bytes(15) = &H83
    End Sub

    Private Sub directDownBytes16()
        all18Bytes(16) = &H3A
    End Sub

    Private Sub stabalizeDownBytes16()
        all18Bytes(16) = &HCA
    End Sub

    Private Sub directUpDownBytes17()
        all18Bytes(17) = &H79
    End Sub

    Private Sub stabalizeUpDownBytes17()
        all18Bytes(17) = &H7C
    End Sub

End Class
