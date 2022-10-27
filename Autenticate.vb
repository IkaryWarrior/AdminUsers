Imports System.IO

Public Class Login
    Private count As Integer = 0
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim fe As New MemoryStream(My.Resources.check)
            Dim sr As New StreamReader(fe)

            Dim cnstring1 As String = My.Settings.UsuariosConnectionString + sr.ReadLine
            sr.Close()
            fe.Close()
            Dim usersTa As New UsuariosDataSetTableAdapters.UsuariosTableAdapter
            usersTa.Connection.ConnectionString = cnstring1
            Dim usuarios As UsuariosDataSet.UsuariosDataTable = usersTa.GetDataByUser(TextBox1.Text.ToUpper)
            If usuarios.Count < 1 Then
                MsgBox("Password o Usuario erroneos", MsgBoxStyle.Exclamation, "Error al auenticar")
                count += 1
            ElseIf Not TextBox1.Text.ToUpper = "ADMINISTRADOR" Then
                MsgBox("Password o Usuario erroneos", MsgBoxStyle.Exclamation, "Error al auenticar")
                count += 1
            ElseIf usuarios.Item(0).Clave = TextBox2.Text Then
                Me.Hide()
                FormInicial.Show()
                Me.Close()
            Else
                MsgBox("Password o Usuario erroneos", MsgBoxStyle.Exclamation, "Error al auenticar")
                count += 1
            End If
            If count >= 3 Then
                Me.Close()
            End If
        Catch ex As Exception
            Me.Close()
        End Try
    End Sub


End Class