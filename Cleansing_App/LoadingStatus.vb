Public Class LoadingStatus
    Public progressbar As Integer
    Public maxProgress As Integer
    Public minProgress As Integer
    Private Sub LoadingStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = Form1.load_cif.RowCount - 1
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        ProgressBar1.Value = ProgressBar1.Value + 1
        Label2.Text = ProgressBar1.Value & " % " & " completed"

        If ProgressBar1.Value >= Form1.load_cif.RowCount - 1 Then
            Timer1.Enabled = False
            ProgressBar1.Value = 0
        End If


    End Sub
End Class