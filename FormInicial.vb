Imports System.IO

Public Class FormInicial
    Dim rowAdmin As DataGridViewRow


    Private Sub UsuariosDataGridView_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) 'Handles UsuariosDataGridView.CellValueChanged
        Dim d As DataGridView
        d = CType(sender, DataGridView)

        Dim s As String
        For Each r As DataGridViewRow In d.Rows
            If Not TypeOf r.Cells(0).Value Is System.DBNull And Not r.Cells(0).Value Is Nothing Then

                s = r.Cells(0).Value
                r.Cells(0).Value = s.ToUpper
            End If
        Next



    End Sub

    Private Sub UsuariosBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsuariosBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.UsuariosBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.UsuariosDataSet)

    End Sub

    Private Sub FormInicial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'UsuariosDataSet.Usuarios' table. You can move, or remove it, as needed.
        Dim fe As New MemoryStream(My.Resources.check)
        Dim sr As New StreamReader(fe)

        Dim cnstring1 As String = My.Settings.UsuariosConnectionString + sr.ReadLine
        UsuariosTableAdapter.Connection.ConnectionString = cnstring1
        Me.UsuariosTableAdapter.Fill(Me.UsuariosDataSet.Usuarios)
        rowAdmin = UsuariosDataGridView.Rows(0)


    End Sub



    Private Sub UsuariosDataGridView_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles UsuariosDataGridView.CellValidating
        ' VB


        If UsuariosDataGridView.Columns(e.ColumnIndex).DataPropertyName = "Usuario" Then
            If e.FormattedValue.ToString = "" Then
                UsuariosDataGridView.Rows(e.RowIndex).ErrorText = "Se requiere un nombre de usuario"
                e.Cancel = True
            Else
                UsuariosDataGridView.Rows(e.RowIndex).ErrorText = ""
                UsuariosDataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = e.FormattedValue.ToString.ToUpper
                UsuariosDataGridView.UpdateCellValue(e.ColumnIndex, e.RowIndex)
                UsuariosDataGridView.RefreshEdit()
            End If
        End If
        If UsuariosDataGridView.Columns(e.ColumnIndex).DataPropertyName = "Clave" Then
            If e.FormattedValue.ToString = "" Then
                UsuariosDataGridView.Rows(e.RowIndex).ErrorText = "Se requiere una contraseña"
                e.Cancel = True
            Else
                UsuariosDataGridView.Rows(e.RowIndex).ErrorText = ""
            End If
        End If

        If e.RowIndex = 0 And e.ColumnIndex = 0 Then
            UsuariosDataGridView.Rows(0).Cells(0).Value = "ADMINISTRADOR"
        End If

    End Sub

    Private Sub UsuariosDataGridView_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles UsuariosDataGridView.DataError
        MsgBox(e.Exception.Message, MsgBoxStyle.Exclamation, "Error en la entrada de datos")
        'e.Cancel 
    End Sub


    Private Sub UsuariosDataGridView_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles UsuariosDataGridView.UserDeletingRow

        ' Check if the starting balance row is included in the selected rows


        If e.Row.Cells(0).Value = "ADMINISTRADOR" Then

            ' Do not allow the user to delete the Starting Balance row.
            MessageBox.Show("No se puede borrar al usuario Administrador!")
            ' Cancel the deletion if the Starting Balance row is included.
            e.Cancel = True

        End If




    End Sub


End Class
