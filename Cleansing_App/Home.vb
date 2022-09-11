Public Class Home

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Form1.Show()
        'Form1.MdiParent = Me
    End Sub

    
    Private Sub m_checking_Click(sender As Object, e As EventArgs) Handles m_checking.Click
        'Checking.Show()
        'Checking.MdiParent = Me
    End Sub

    Private Sub m_join_loan_Click(sender As Object, e As EventArgs)
        Loan.Show()
        Loan.MdiParent = Me
    End Sub

    Private Sub m_cleansing_Click(sender As Object, e As EventArgs) Handles m_cleansing.Click
        Form1.Show()
        Form1.MdiParent = Me
    End Sub


    Private Sub m_exit_Click(sender As Object, e As EventArgs) Handles m_exit.Click
        Dim pesan As MsgBoxResult
        pesan = MsgBox("Do you want close this application.?", MsgBoxStyle.YesNo, " Close App")
        If pesan = MsgBoxResult.Yes Then
            Application.Exit()
        Else
        End If
    End Sub
   
    Private Sub m_generateReport_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub m_cleansingloan_Click(sender As Object, e As EventArgs) Handles m_cleansingloan.Click
        Loan.Show()
        Loan.MdiParent = Me
    End Sub

   
    Private Sub PaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentToolStripMenuItem.Click
        Reconsile_Payment.Show()
        Reconsile_Payment.MdiParent = Me
    End Sub

  
    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem.Click
        Try
            Application.Restart()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub m_report_Click(sender As Object, e As EventArgs) Handles m_report.Click
        Try
            GenerateReport.Show()
            GenerateReport.MdiParent = Me
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub AboutAppToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutAppToolStripMenuItem.Click
        Try
            AboutBox1.Show()
            AboutBox1.MdiParent = Me
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub AddDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBalancedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckBalancedToolStripMenuItem.Click
        Try
            MonitoringBalance.Show()
            MonitoringBalance.MdiParent = Me
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SplitDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SplitDataToolStripMenuItem.Click
        Try
            SplitForm.Show()
            SplitForm.MdiParent = Me
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CheckMutasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckMutasiToolStripMenuItem.Click
        Try
            CheckingMutasi.Show()
            CheckingMutasi.MdiParent = Me
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub MonitoringDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MonitoringDatabaseToolStripMenuItem.Click
        Try
            ImportDatabase.Show()
            ImportDatabase.MdiParent = Me
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class