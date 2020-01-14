Imports System
Imports System.Diagnostics

Module Program
    ' the functions needed for the hiddden library
    Private Declare Auto Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    Private Declare Auto Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private Const SW_HIDE As Integer = 0
    Sub Main(args As String())
        ' Hides the console
        Dim hWndConsole As IntPtr
        hWndConsole = GetConsoleWindow()
        ShowWindow(hWndConsole, SW_HIDE)
        ' Retrieve the parameters
        Dim fileName As String = String.Empty
        Dim clArgs() As String = Environment.GetCommandLineArgs()
        If clArgs.Count < 2 Then
            Return
        Else
            fileName = clArgs(1)
        End If
        ' Starts the proccess
        Dim psi As New ProcessStartInfo()
        With psi
            .Verb = "print"
            .WindowStyle = ProcessWindowStyle.Hidden
            .FileName = fileName
            .UseShellExecute = True
        End With
        Process.Start(psi).WaitForExit()
    End Sub
End Module
