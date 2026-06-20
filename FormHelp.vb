Imports System.IO

Public Class FormHelp

    Private Sub FormHelp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not File.Exists(Path.Combine(Application.StartupPath, "help.html")) Then
            Me.Close()
        End If
        WebBrowser1.Navigate(Path.Combine(Application.StartupPath, "help.html"))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class