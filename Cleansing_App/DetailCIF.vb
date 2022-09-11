Public Class DetailCIF
    Public multifinance_customer As String
    Public indexRows As Integer
    Private resultCollect As DataRow
    'Private rowLoan() As DataRow
    Private Sub DetailCIF_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btn_success_Click(sender As Object, e As EventArgs) Handles btn_success.Click
        Dim rowLoan(), rowCif() As DataRow
        Dim l As Loan = CType(Me.Owner, Loan)
        If l.cb_type_join.SelectedIndex.ToString = 0 Then
            'join cif
            rowCif = l.tableCIFGagal.Select("[" + l.tableCIFGagal.Columns(79).ColumnName + "] ='" + multifinance_customer.ToString + "'")
            'update value column in datarow
            For Each dr As DataRow In rowCif
                'column index NO CIF : 3
                dr(2) = txt_no_cif.Text.ToString
            Next
            'iport data to data cif final

            l.tableCIFFinal.ImportRow(l.tableCIFGagal.Rows(indexRows))
            'remove rows index in data
            l.tableCIFGagal.Rows.RemoveAt(indexRows)
            'refresh
            l.tableCIFFinal.AcceptChanges()
            l.tableCIFGagal.AcceptChanges()
            'load data table

            l.load_cif_final.DataSource = l.tableCIFFinal
            l.load_cif_gagal.DataSource = l.tableCIFGagal
            l.C_Rows1.Text = l.load_cif_final.Rows.Count - 1
            l.C_Rows2.Text = l.load_cif_gagal.Rows.Count - 1
            Me.Close()
        ElseIf l.cb_type_join.SelectedIndex.ToString = 1 Then
            'join loan
            rowLoan = l.tableSuccessLoan.Select("[" + l.tableSuccessLoan.Columns(98).ColumnName + "] ='" + multifinance_customer.ToString + "'")
            'check data collection for takeout loan
            If Not l.dt_collec.Rows.Count <= 0 Then
                'Expected Column [CIF] ,[Kolektivitas]
                If l.dt_collec.Rows.Count > 0 Then
                    resultCollect = l.dt_collec.Select("[" + l.dt_collec.Columns(0).ColumnName + "] ='" + txt_no_cif.Text.ToString + "' ").FirstOrDefault()
                    'add info
                    'add table takedown                  
                    If Not resultCollect Is Nothing Then
                        'get alert
                        MessageBox.Show("CIF Kolektivitas Found : " + txt_no_cif.Text.ToString)
                    Else
                        'update value column in datarow
                        For Each dr As DataRow In rowLoan
                            'column index NO CIF : 3
                            dr(3) = txt_no_cif.Text.ToString
                        Next
                        'remove rows index in data
                        l.load_cif_gagal.Rows.RemoveAt(indexRows)
                    End If
                End If
            Else
                'update value column in datarow
                For Each dr As DataRow In rowLoan
                    'column index NO CIF : 3
                    dr(3) = txt_no_cif.Text.ToString
                Next
                'remove rows index in data
                l.load_cif_gagal.Rows.RemoveAt(indexRows)
            End If





            'refresh datagridview
            l.tableCIFGagal.AcceptChanges()
            l.tableSuccessLoan.AcceptChanges()
            l.load_loan_success.DataSource = l.tableSuccessLoan
            l.load_cif_gagal.DataSource = l.tableCIFGagal
            'close form
            Me.Close()
        End If
    End Sub
End Class