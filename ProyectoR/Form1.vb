Imports System.IO

Public Class Form1


    Private Sub Iniciar_Click(sender As Object, e As EventArgs) Handles Iniciar.Click
        'if para el login de usuario y contraseña
        If (user.Text = "migdalia" And pass.Text = "mateo") Then
            'deshabilitamos el panel del login
            Panel1.Enabled = False
            Panel1.Visible = False
            'habilitamos el panel2 del resto de la aplicacion 
            Panel2.Enabled = True
            Panel2.Visible = True

        End If


    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ArchivoQToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ArchivoQToolStripMenuItem.Click
        Dim pagina As TabPage = New TabPage
        'se crean maginas en blanco con titulo predeterminado empty e indice de acuerdo al aumento
        pagina.Text = "empty" + TabControl1.TabPages.Count.ToString
        'agregamos el espacion en blanco para escribir
        Dim RichTextBox1 As RichTextBox = New RichTextBox
        RichTextBox1.Dock = DockStyle.Fill
        pagina.Controls.Add(RichTextBox1)

        'agregamos la nueva tabpagen en el tabcontrol correspondiente
        TabControl1.TabPages.Add(pagina)
        TabControl1.SelectedTab = pagina

    End Sub

    Private Sub AbrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem.Click

        Dim OpenFileDialog1 As OpenFileDialog = New OpenFileDialog
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()

        ' Test result.
        If result = DialogResult.OK Then

            ' Get the file name.
            Dim path As String = OpenFileDialog1.FileName

            Try
                ' Read in text.
                Dim text As String = File.ReadAllText(path)


                'creamos una nueva pestaña para abrir el documento a cargar
                Dim nuevo As TabPage = New TabPage
                'colocamos el nombre del archivo que se abrio
                nuevo.Text = path
                'creamos un nuevo espacio en blanco para colocar el texto
                Dim espacio As RichTextBox = New RichTextBox
                'insertamos el espacion en todo el tab
                espacio.Dock = DockStyle.Fill
                'colocamos el texto en el espacio creado en blanco
                espacio.Text = text

                'agregamos el richtexbox al tabcontrol
                nuevo.Controls.Add(espacio)
                'mostramos en pantalla con la pestaña nueva abierta
                TabControl1.SelectedTab = nuevo
                'agregamos la tabpage en el tabcontrol
                TabControl1.TabPages.Add(nuevo)

            Catch ex As Exception

                ' Report an error.
                Me.Text = "Error"

            End Try
        End If


    End Sub

    'boton de cerrar pestaña
    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        'seleccionamos la pestaña actual y cerramos
        If TabControl1.TabPages.Count.Equals(Nothing) And TabControl1.SelectedIndex < 0 Then

        Else
            TabControl1.TabPages.RemoveAt(TabControl1.SelectedIndex)

        End If





    End Sub
    'los dos botones de guardar como
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        guardar()

    End Sub

    Private Sub GuardarComoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarComoToolStripMenuItem.Click
        guardar()

    End Sub

    Sub guardar()

        Dim datos As String
        datos = ""
        Dim SaveFileDialog1 As SaveFileDialog = New SaveFileDialog
        SaveFileDialog1.Filter = "TXT Files (*.txt*)|*.txt"
        If SaveFileDialog1.ShowDialog = DialogResult.OK _
         Then

            For Each tp As TabPage In TabControl1.TabPages
                For Each ctl In tp.Controls
                    If TypeOf ctl Is RichTextBox Then

                        datos = CType(ctl, RichTextBox).Text
                    End If
                Next
            Next

            My.Computer.FileSystem.WriteAllText _
            (SaveFileDialog1.FileName, datos, True)
        End If

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim FolderBrowserDialog1 As FolderBrowserDialog = New FolderBrowserDialog
        Try
            ' Configuración del FolderBrowserDialog  
            With FolderBrowserDialog1

                .RootFolder = Environment.SpecialFolder.MyComputer

                Dim ret As DialogResult = .ShowDialog ' abre el diálogo  

                ' si se presionó el botón aceptar ...  
                If ret = DialogResult.OK Then
                    MsgBox("Path : " & .SelectedPath, MsgBoxStyle.Information)
                    Dim nFiles As ObjectModel.ReadOnlyCollection(Of String)
                    nFiles = My.Computer.FileSystem.GetFiles(.SelectedPath)


                    For cont As Integer = 0 To nFiles.Count Step 1
                        TreeView1.Nodes(0).Nodes.Add(New TreeNode(nFiles.ElementAt(cont)))

                    Next


                End If

                .Dispose()

            End With

        Catch oe As Exception
            MsgBox(oe.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Class
