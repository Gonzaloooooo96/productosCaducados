Imports MySql.Data.MySqlClient
Public Class Form4
    Dim conexion As New MySqlConnection
    Dim comando As New MySqlCommand
    Dim adaptador As New MySqlDataAdapter
    Dim registro As New DataSet
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conexion.ConnectionString = "Server=localhost; Database=supermercado; Uid=root; Pwd=xhandwyh;"
            conexion.Open()
            'MsgBox("Conexion exitosa", vbInformation, "Conectado")
            Dim consulta As String
            consulta = "SELECT nombre, fecha_vencimiento, descripcion FROM producto WHERE fecha_vencimiento < '" & DateTime.Now.ToString("yyyy-MM-dd") & "'"
            'consulta = "SELECT nombre, fecha_vencimiento, descripcion FROM producto WHERE fecha_vencimiento < '2017-10-30'" si funciona

            adaptador = New MySqlDataAdapter(consulta, conexion)
            registro = New DataSet
            adaptador.Fill(registro, "producto")
            DataGridView1.DataSource = registro
            DataGridView1.DataMember = "producto"

        Catch ex As Exception
            MsgBox("Error al conectarse con la base de datos", vbInformation, "Error")

        End Try
        If DataGridView1.ColumnCount <> 0 Then

            MsgBox("AVISO! HAY PRODUCTOS CADUCADOS", vbExclamation, "IMPORTANTE")

            'Atencion.Show()
            'Me.Close()
            'Form2.Show()
            'Me.Close()

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
    End Sub
End Class