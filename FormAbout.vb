Imports System.IO

Public Class FormAbout

    Private Sub FormAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not File.Exists(Path.Combine(Application.StartupPath, "settings/main.dll")) Then
        Else
            PictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/main.dll"))
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Process.Start("https://github.com/sponsors/arisohandriputra")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start(Path.Combine(Application.StartupPath, "LICENSE.txt"))
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start(Path.Combine(Application.StartupPath, "DiskSpd-LICENSE.txt"))
    End Sub
End Class