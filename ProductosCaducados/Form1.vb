Imports MySql.Data.MySqlClient
Public Class Form2
    Private WithEvents Timer1 As New System.Windows.Forms.Timer()
    Dim conexion As New MySqlConnection
    Dim comando As New MySqlCommand
    Dim adaptador As New MySqlDataAdapter
    Dim registro As New DataSet

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Label5.Text = String.Format("{0:HH:mm:ss}", DateTime.Now)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MsgBox("Atencion hay productos vencidos")


        '---------------------------------------------------------------------------------------
        Label5.Text = String.Format("{0:HH:mm:ss}", DateTime.Now)
        Timer1.Interval = 1000  ' Un segundo
        Timer1.Start()

        Label3.Text = DateTime.Now.ToString("dd/MM/yyyy")
        Try
            conexion.ConnectionString = "Server=localhost; Database=supermercado; Uid=root; Pwd=xhandwyh;"
            conexion.Open()
            'MsgBox("Conexion exitosa", vbInformation, "Conectado")
            Dim consulta As String
            consulta = "SELECT sku, nombre, fecha_vencimiento, descripcion FROM producto WHERE fecha_vencimiento < '" & DateTime.Now.ToString("yyyy-MM-dd") & "'"
            'consulta = "SELECT nombre, fecha_vencimiento, descripcion FROM producto WHERE fecha_vencimiento < '2017-10-30'" si funciona

            adaptador = New MySqlDataAdapter(consulta, conexion)
            registro = New DataSet
            adaptador.Fill(registro, "producto")
            DataGridView1.DataSource = registro
            DataGridView1.DataMember = "producto"
        Catch ex As Exception
            MsgBox("Error al conectarse con la base de datos", vbInformation, "Error")

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dgfila As Integer
        Dim consulta_actualizar As String 'variable para guardar la consult
        Dim dt As New DataTable

        If Me.DataGridView1.Rows.Count > 0 Then

            If MsgBox("¿SEGURO(A) QUE DESEA ELIMINAR EL PRODUCTO SELECCIONADO?", vbOKCancel, "Aviso") = vbOK Then
                dgfila = DataGridView1.CurrentRow.Index
                DataGridView1.Rows.RemoveAt(dgfila)
                consulta_actualizar = "UPDATE producto SET estado = 'inactivo'  WHERE sku = '" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "' "
                'consulta_actualizar = "UPDATE producto SET estado = 'inactivo'  WHERE sku = ' 10' "
                comando = New MySqlCommand(consulta_actualizar, conexion)
                comando.ExecuteNonQuery()

                MsgBox("Eliminado", vbInformation, "Actualizado")

            Else
                Me.Show()
            End If
        Else
            MsgBox("Sin registros para eliminar", vbCritical, "Aviso")
        End If
        '----------------------------------------------------------------------------------------------------





    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.DataGridView1.Rows.Count > 0 Then
            If MsgBox("¿SEGURO(A) QUE DESEA ELIMINAR TODOS LOS PRODUCTOS?", vbOKCancel, "Aviso") = vbOK Then
                DataGridView1.DataSource = Nothing
                MsgBox("Productos eliminados", vbInformation, "Eliminado")
            Else
                Me.Show()
            End If
        Else
            MsgBox("Sin registros para eliminar", vbCritical, "Aviso")
        End If








    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Atencion.Show()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Form3.Show()
    End Sub
End Class
