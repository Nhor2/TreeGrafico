Public Class Form1

    'TreeGrafico
    'lord.marte@gmail.com
    '28-03-2024
    'creazione di un albero grafico


    Dim Tree_path As String = ""

    'Fonts
    Dim Title_font As Font = New Font("Tahoma", 14)
    Dim Tree_font As Font = New Font("Tahoma", 11)
    Dim Colore_tree As Color = Color.Blue
    Dim Colore_sfondo As Color = Color.White

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inserisci un panel con Autoscroll=True consentirà
        'alla form di generare in automatico le scrollbars.
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'sfoglia
        FolderBrowserDialog1.SelectedPath = ""
        FolderBrowserDialog1.Description = "Scegli una cartella..."

        If FolderBrowserDialog1.ShowDialog(Me) = DialogResult.OK Then
            Tree_path = FolderBrowserDialog1.SelectedPath
            Dim treebitmap As Bitmap = TreeGrafico(Tree_path)
            PictureBox1.Image = treebitmap
            PictureBox1.Width = treebitmap.Width
            PictureBox1.Height = treebitmap.Height
            PictureBox1.Refresh()
        End If
    End Sub

    Private Function TreeGrafico(path As String) As Bitmap
        Dim treebitmap = New Bitmap(850, 450)
        Dim treebitmap_width As Integer = treebitmap.Width
        Dim treebitmap_height As Integer = treebitmap.Height

        Dim x As Integer = 0
        Dim y As Integer = 0

        Dim lines As Integer = 0
        Dim ampiezza As New SizeF
        Dim Textdraw As String = path & vbCrLf

        Using g As Graphics = Graphics.FromImage(treebitmap)
            g.Clear(Colore_sfondo)
            Dim p As New Pen(Colore_tree)
            Dim b As New SolidBrush(Colore_tree)

            'Cartelle
            Dim folders() As String = IO.Directory.GetDirectories(path)
            For Each folder As String In folders
                ' Do work, example
                Try
                    Textdraw = Textdraw & "  |--- " & System.IO.Path.GetFileName(folder) & vbCrLf
                Catch ex As Exception
                    MsgBox("Errore: " & ex.Message)
                End Try
            Next

            'Salva il testo
            'My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\temp.txt", Textdraw, False)
            'System.Threading.Thread.Sleep(200)

            RichTextBox1.Text = Textdraw

            'Misua linee e ampiezza testo
            lines = RichTextBox1.Lines.Count
            ampiezza = g.MeasureString(Textdraw, Tree_font)

        End Using

        'Final Bitmap
        Dim newwidth As Integer = ampiezza.Width + 200 ' + Testo TreeGrafico
        Dim newheight As Integer = ampiezza.Height + 60 ' + Testo TreeGrafico

        Dim treebitmap_new = New Bitmap(newwidth, newheight)
        Dim treebitmap_newwidth As Integer = treebitmap_new.Width
        Dim treebitmap_newheight As Integer = treebitmap_new.Height

        Using g As Graphics = Graphics.FromImage(treebitmap_new)
            g.Clear(Colore_sfondo)
            Dim p As New Pen(Colore_tree)
            Dim b As New SolidBrush(Colore_tree)
            'g.DrawRectangle(p, 3, 3, treebitmap_width - 6, treebitmap_height - 6)
            g.DrawString("TreeGrafico " & DateAndTime.Now.ToString & vbCrLf & vbCrLf, Title_font, b, 13, 13)
            g.DrawString(Textdraw, Tree_font, b, 40, 40)
        End Using

        'Files al momento non richiesti

        'Dim files() As String = IO.Directory.GetFiles(path)
        'For Each file As String In files
        ' Do work, example
        'Try
        'Dim text As String = IO.File.ReadAllText(file)
        'Catch ex As Exception
        'MsgBox("Errore: " & ex.Message)
        'End Try

        'Next

        Return treebitmap_new
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Font

        FontDialog1.Font = New Font("Tahoma", 14)
        If FontDialog1.ShowDialog(Me) = DialogResult.OK Then
            If FontDialog1.Font IsNot Nothing Then
                Title_font = FontDialog1.Font
            End If
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Font tree
        FontDialog1.Font = New Font("Tahoma", 11)
        If FontDialog1.ShowDialog(Me) = DialogResult.OK Then
            If FontDialog1.Font IsNot Nothing Then
                Tree_font = FontDialog1.Font
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Colore..
        If ColorDialog1.ShowDialog(Me) = DialogResult.OK Then
            If ColorDialog1.Color <> Nothing Then
                Colore_tree = ColorDialog1.Color
                Label4.BackColor = ColorDialog1.Color
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Sfondo
        If ColorDialog1.ShowDialog(Me) = DialogResult.OK Then
            If ColorDialog1.Color <> Nothing Then
                Colore_sfondo = ColorDialog1.Color
                Label5.BackColor = ColorDialog1.Color
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'Salva bmp
        SaveFileDialog1.Title = "Salva il TreeGrafico..."
        SaveFileDialog1.FileName = "TreeGrafico.png"
        SaveFileDialog1.Filter = "Files Portable Network Graphics|*.png|Files Bitmap|*.bmp|Files JPEG|*.jpg"

        If SaveFileDialog1.ShowDialog(Me) = DialogResult.OK Then
            If SaveFileDialog1.FileName <> "" Then
                Select Case SaveFileDialog1.FileName.Split(".").Last.ToLower
                    Case "png"
                        PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Png)
                    Case "jpg"
                        PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Jpeg)
                    Case "bmp"
                        PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Bmp)
                End Select
                MsgBox("TreeGrafico salvato")

            End If
        End If
    End Sub
End Class
