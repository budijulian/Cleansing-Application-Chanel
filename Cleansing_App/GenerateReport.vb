Imports System.Data.OleDb
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System.Data.OleDb.OleDbConnection
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Collections
Imports Microsoft.Office.Interop
Imports System.Text
Imports System.IO.Directory
Imports ClosedXML.Excel

Public Class GenerateReport
    Private cmd, cmd2 As New Data.SqlClient.SqlCommand
    Private ad As New Data.SqlClient.SqlDataAdapter
    Public SQL As New ConnectionSQL
    Private conn As New OleDbConnection 'koneksi odb
    Private tableReject, tableLoanTemp, table2361, tableLoanFinal, tableTakeout, table_DT, tableReport, tableLoanFile As New DataTable
    Private tableTemp, tableTemp2, tableTemp3 As New DataTable
    Private excel As String
    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    Private rowTemp, rowTemp2, rowTemp3, rowDT As DataRow
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Public getPdt As New accountPayments
    Private loanTemp As New DataTable()
    Private tableCIFTakedown As New DataTable
    Private Sub GenerateReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'get data product
            Dim getProducts As New DataTable
            Dim strProduct As String
            getProducts = getPdt.getAllProducts()
            For a = 0 To getProducts.Rows.Count - 1
                strProduct = "[" + (a + 1).ToString + "] " + getProducts.Rows(a).Item(1).ToString
                cb_product.Items.Add(strProduct.ToString)
            Next
            btn_save_balance.Hide()
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            C_data4.Text = ""
            cb_export.SelectedIndex = 0
            cb_product.SelectedIndex = 0
            btn_save_balance.Hide()
            cb_takedown.Items.Add("Import")
            cb_takedown.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub cb_report_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_report.SelectedIndexChanged
        Try
            Call clearDatable()
            If cb_report.SelectedIndex.ToString = 0 Then
                'report reject
                'hidden button and text
                btn_import_loan_final.Show()
                cb_takedown.Show()
                btn_import_loan.Show()
                btn_import_approved.Show()
                btn_save_balance.Hide()
                'hide text
                l_ciftakeout.Show()
                l_loan2361.Show()
                     l_loanfinal.Show()
                l_loan.Show()
                cb_takedown.Items.Clear()
                l_loan.Text = "Loan"
                cb_takedown.Items.Add("Import")
                cb_takedown.Items.Add("CIF Takeout")
                l_ciftakeout.Text = "CIF Takeout"
                l_loanfinal.Text = "Loan Final*"
                C_data1.Text = ""
                C_data2.Text = ""
                C_data3.Text = ""
                C_data4.Text = ""
                cb_takedown.SelectedIndex = 0
            ElseIf cb_report.SelectedIndex.ToString = 1 Then
                'report early
                btn_import_loan_final.Hide()
                btn_import_approved.Hide()
                btn_import_loan.Hide()
                btn_save_balance.Hide()
                cb_takedown.Show()
                'hide text
                l_loan2361.Hide()
                l_loanfinal.Hide()
                l_loan.Hide()
                l_ciftakeout.Text = "Early File*"
                C_data1.Text = ""
                C_data2.Text = ""
                C_data3.Text = ""
                C_data4.Text = ""
                cb_takedown.Items.Clear()
                cb_takedown.Items.Add("Import")
                cb_takedown.Items.Add("Early File")
                cb_takedown.SelectedIndex = 0
            ElseIf cb_report.SelectedIndex.ToString = 2 Then
                'report reguler
                btn_import_loan_final.Hide()
                btn_import_approved.Hide()
                btn_import_loan.Hide()
                btn_save_balance.Hide()
                'hide text
                l_loan2361.Hide()
                l_loanfinal.Hide()
                l_loan.Hide()
                l_ciftakeout.Text = "Reguler File*"
                C_data1.Text = ""
                C_data2.Text = ""
                C_data3.Text = ""
                C_data4.Text = ""
                cb_takedown.Items.Clear()
                cb_takedown.Items.Add("Import")
                cb_takedown.Items.Add("Regular File")
                cb_takedown.SelectedIndex = 0
            ElseIf cb_report.SelectedIndex.ToString = 3 Then
                'report Cancellation
                btn_import_loan_final.Hide()
                btn_import_approved.Hide()
                btn_import_loan.Hide()
                btn_save_balance.Hide()
                'hide text
                l_loan2361.Hide()
                l_loanfinal.Hide()
                l_loan.Hide()
                l_ciftakeout.Text = "Cancel File*"
                C_data1.Text = ""
                C_data2.Text = ""
                C_data3.Text = ""
                C_data4.Text = ""
                cb_takedown.Items.Clear()
                cb_takedown.Items.Add("Import")
                cb_takedown.Items.Add("Cancel File")
                cb_takedown.SelectedIndex = 0
            ElseIf cb_report.SelectedIndex.ToString = 4 Then
                'report Buyback
                btn_import_loan_final.Hide()
                btn_import_approved.Hide()
                btn_import_loan.Hide()
                btn_save_balance.Hide()
                'hide text
                l_loan2361.Hide()
                l_loanfinal.Hide()
                l_loan.Hide()
                l_ciftakeout.Text = "Buyback Final*"
                C_data1.Text = ""
                C_data2.Text = ""
                C_data3.Text = ""
                C_data4.Text = ""
                cb_takedown.Items.Clear()
                cb_takedown.Items.Add("Import")
                cb_takedown.Items.Add("Buyback File")
                cb_takedown.SelectedIndex = 0
            ElseIf cb_report.SelectedIndex.ToString = 5 Then
                'report Partial
                btn_import_loan_final.Show()
                btn_import_approved.Hide()
                btn_import_loan.Show()
                'hide text
                l_loan2361.Hide()
                l_loanfinal.Show()
                l_loan.Show()
                l_loan.Text = "COA Balance"
                l_ciftakeout.Text = "Regular Final"
                l_loanfinal.Text = "1200 Query"
                C_data1.Text = ""
                C_data2.Text = ""
                C_data3.Text = ""
                C_data4.Text = ""
                cb_takedown.Items.Clear()
                cb_takedown.Items.Add("Import")
                cb_takedown.Items.Add("Regular Final")
                cb_takedown.SelectedIndex = 0
            ElseIf cb_report.SelectedIndex.ToString = 6 Then
                'report data ostanding
                btn_import_loan_final.Hide()
                btn_import_approved.Hide()
                btn_import_loan.Show()
                btn_save_balance.Hide()
                'hide text
                l_loan2361.Hide()
                l_loanfinal.Hide()
                l_loan.Show()
                l_loan.Text = "COA Balance"
                l_ciftakeout.Text = "Ostanding File"
                C_data1.Text = ""
                C_data2.Text = ""
                C_data3.Text = ""
                C_data4.Text = ""
                cb_takedown.Items.Clear()
                cb_takedown.Items.Add("Import")
                cb_takedown.Items.Add("9850 File")
                cb_takedown.Items.Add("DT File")
                cb_takedown.SelectedIndex = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    'Private Function checkCountBalancedHist(ByRef shbi_acct_no As String, ByRef datepayment As String, ByRef flag As String) As Integer
    '    Dim checkbalanced As Integer
    '    Dim cmd As New Data.SqlClient.SqlCommand
    '    Dim checktable As New DataTable()
    '    If String.IsNullOrWhiteSpace(shbi_acct_no) Then
    '        checkbalanced = 0
    '    Else
    '        Dim getYear As String = Strings.Left(datepayment, 4)
    '        Dim getMonth As String = Strings.Mid(datepayment, 5, 2)
    '        Dim getDay As String = Strings.Right(datepayment, 2)
    '        Dim str_date_payment As String
    '        Dim get_date_payment As DateTime
    '        str_date_payment = getYear + "-" + getMonth + "-" + getDay
    '        'convert to datetime
    '        get_date_payment = Convert.ToDateTime(str_date_payment)
    '        SQLConnection = New SqlConnection(SQLConnectionString)
    '        'check sum balanced today
    '        If Not IsNothing(get_date_payment) Then
    '            cmd = New SqlCommand("SELECT COUNT(*) AS Count_Rows FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] WHERE [shbi acct no] = '" + shbi_acct_no.ToString + "' AND [Repayment date] = '" + get_date_payment.ToString("yyyy-MM-dd") + "' AND [Flag] = '" + flag.ToString + "' ", SQLConnection)
    '        End If

    '        Dim adapter As New SqlDataAdapter(cmd)
    '        adapter.Fill(checktable)
    '        'Checking data to any rows in table "Occupation
    '        If checktable.Rows.Count() > 0 Then
    '            'get data balanced calculation
    '            'sum data total balanced
    '            checkbalanced = checktable.Rows(0).Item(0)
    '        Else
    '            'data not found 
    '            checkbalanced = 0
    '        End If
    '    End If
    '    Return checkbalanced
    'End Function
    '    Private Sub btn_save_balance_Click(sender As Object, e As EventArgs) Handles btn_save_balance.Click
    '        Try
    '            Dim n_tenor As String
    '            Dim status, flag As String
    '            Dim max_tenor As Integer
    '            Dim datepayment As String
    '            Dim shbi_acct_no As String
    '            Dim balance As Double
    '            Dim amount As Double
    '            Dim n_rowsTBal As DataTable
    '            Dim rowDataRegular As DataRow
    '            Dim n_rowsAddHist, n_rowsUpdHist, n_rowsUpdTBal As Integer
    '            n_rowsTBal = Nothing
    '            n_rowsAddHist = 0
    '            balance = 0
    '            Dim checkBalancedHist As Integer
    '            'create new table row
    '            For x As Integer = 0 To tableTemp.Rows.Count - 1
    '                'check data true for save balanced
    '                'set parameter
    '                shbi_acct_no = tableTemp.Rows(x).Item(5).ToString
    '                amount = tableTemp.Rows(x).Item(6).ToString
    '                balance = tableTemp.Rows(x).Item(9).ToString
    '                status = tableTemp.Rows(x).Item(12).ToString
    '                datepayment = tableTemp.Rows(x).Item(0).ToString
    '                'check data balanced in history database table balanced
    '                'check data history
    '                checkBalancedHist = checkCountBalancedHist(shbi_acct_no.ToString, datepayment.ToString, "DEBIT")
    '                Console.WriteLine(checkBalancedHist)
    '                'data not found
    '                If checkBalancedHist <= 0 Then
    '                    'get data from repayment file 
    '                    'check account has running as regular payment
    '                    Dim filterRegular As String = String.Format("[" + tableCIFTakedown.Columns(8).ColumnName + "] ='" + shbi_acct_no.ToString + "'")
    '                    rowDataRegular = tableCIFTakedown.Select(filterRegular).FirstOrDefault()
    '                    ' Date|Multifinance CIF|Multifinance Account|SHBI CIF|Amount|Trx Type|Reason
    '                    If Not rowDataRegular Is Nothing Then
    '                        'data found
    '                        Console.WriteLine(rowDataRegular.Item(1))
    '                        'add new data balanced history
    '                        ' ByRef shbi_acct_no As String, ByRef balance As Double, ByRef dt_payment As DataRow, ByRef index As Integer
    '                        If status = "Processed" Then
    '                            'process with 2X insert to database history (KREDIT & DEBET)
    '                            If addBalancedHist(shbi_acct_no.ToString, amount, "DEBIT", rowDataRegular) > 0 Then
    '                                'process balance from amount processed
    '                                n_rowsAddHist += addBalancedHist(shbi_acct_no.ToString, balance, "DEBET", rowDataRegular)
    '                            End If
    '                        Else
    '                            'process with 1X insert to database history (KREDIT)
    '                            n_rowsAddHist += addBalancedHist(shbi_acct_no.ToString, amount, "DEBIT", rowDataRegular)
    '                        End If

    '                    End If
    '                    'tableTemp.Rows(index2).Item(0) = strdate.ToString
    '                    'tableTemp.Rows(index2).Item(1) = "Partial"
    '                    'tableTemp.Rows(index2).Item(2) = tableCIFTakedown.Rows(x).Item(4).ToString
    '                    'tableTemp.Rows(index2).Item(3) = tableCIFTakedown.Rows(x).Item(5).ToString
    '                    'tableTemp.Rows(index2).Item(4) = tableCIFTakedown.Rows(x).Item(6).ToString
    '                    'tableTemp.Rows(index2).Item(5) = tableCIFTakedown.Rows(x).Item(8).ToString
    '                    'tableTemp.Rows(index2).Item(6) = total_amount.ToString
    '                    'tableTemp.Rows(index2).Item(7) = n_tenor.ToString
    '                    'tableTemp.Rows(index2).Item(8) = tableCIFTakedown.Rows(x).Item(32).ToString
    '                    'tableTemp.Rows(index2).Item(9) = balance_amount.ToString
    '                    'tableTemp.Rows(index2).Item(10) = get_total_balance.ToString
    '                    'tableTemp.Rows(index2).Item(11) = str_reason.ToString
    '                    'tableTemp.Rows(index2).Item(12) = str_process.ToString

    '                ElseIf checkBalancedHist > 0 Then
    '                    GoTo Quit
    '                End If
    '                'take events
    '                Application.DoEvents()
    '            Next
    'Quit:
    '            MessageBox.Show("Data inserted : " + n_rowsAddHist.ToString, "Alert")
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End Sub
    'Private Function addBalancedHist(ByRef shbi_acct_no As String, ByRef balance As Double, ByRef flag As String, ByRef dt_payment As DataRow) As Integer
    '    Dim n_addRows As Integer
    '    Dim addRows As Integer
    '    Dim checktable As New DataTable()
    '    If Not String.IsNullOrWhiteSpace(shbi_acct_no) Then
    '        Using con As New SqlConnection(SQLConnectionString)
    '            con.Open()
    '            Dim query As String = "INSERT INTO [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] ([Date],[Repayment date],[Execute date],[Product code],[Product cif],[Product acc no],[shbi cif no],[shbi acct no],[Balanced],[auto transfer acno],[Flag],[Tenor no],[Remain Tenor]) VALUES(@date,@repayment_date,@execution_date,@Product_code,@Product_cif,@Product_acc_no,@shbi_cif_no,@shbi_acct_no,@Balanced,@auto_transfer_acno,@Flag,@Tenor_no,@remain_Tenor)"

    '            Using cmd As New SqlCommand(query, con)

    '                Dim date1 As String = dt_payment.Item(0).ToString
    '                Dim getYear1 As String = Strings.Left(date1, 4)
    '                Dim getMonth1 As String = Strings.Mid(date1, 5, 2)
    '                Dim getDay1 As String = Strings.Right(date1, 2)
    '                Dim str_date_payment1 As String
    '                Dim get_date_payment1 As DateTime
    '                str_date_payment1 = getYear1 + "-" + getMonth1 + "-" + getDay1
    '                'convert to datetime
    '                get_date_payment1 = Convert.ToDateTime(str_date_payment1)
    '                cmd.Parameters.AddWithValue("@date", get_date_payment1)

    '                cmd.Parameters.AddWithValue("@repayment_date", get_date_payment1)

    '                Dim date3 As String = dt_payment.Item(1).ToString
    '                Dim getYear3 As String = Strings.Left(date3, 4)
    '                Dim getMonth3 As String = Strings.Mid(date3, 5, 2)
    '                Dim getDay3 As String = Strings.Right(date3, 2)
    '                Dim str_date_payment3 As String
    '                Dim get_date_payment3 As DateTime
    '                str_date_payment3 = getYear3 + "-" + getMonth3 + "-" + getDay3
    '                'convert to datetime
    '                get_date_payment3 = Convert.ToDateTime(str_date_payment3)
    '                cmd.Parameters.AddWithValue("@execution_date", get_date_payment3)

    '                cmd.Parameters.AddWithValue("@Product_code", dt_payment.Item(3).ToString)
    '                cmd.Parameters.AddWithValue("@Product_cif", dt_payment.Item(4).ToString)
    '                cmd.Parameters.AddWithValue("@Product_acc_no", dt_payment.Item(5).ToString)
    '                cmd.Parameters.AddWithValue("@shbi_cif_no", dt_payment.Item(6).ToString)
    '                cmd.Parameters.AddWithValue("@shbi_acct_no", shbi_acct_no.ToString)
    '                cmd.Parameters.AddWithValue("@Balanced", balance.ToString)
    '                cmd.Parameters.AddWithValue("@auto_transfer_acno", dt_payment.Item(32).ToString)
    '                cmd.Parameters.AddWithValue("@Flag", flag.ToString)
    '                cmd.Parameters.AddWithValue("@Tenor_no", dt_payment.Item(14).ToString)
    '                cmd.Parameters.AddWithValue("@remain_Tenor", dt_payment.Item(13).ToString)
    '                cmd.ExecuteNonQuery()

    '            End Using
    '        End Using
    '        n_addRows = 1
    '    Else
    '        n_addRows = 0
    '    End If
    '    Return n_addRows
    'End Function

    'Private Function addBalancedOnDueHist(ByRef shbi_acct_no As String, ByRef tb_query_process As Double, ByRef flag As String, ByRef dt_payment As String) As Integer
    '    Dim n_addRows As Integer
    '    Dim addRows As Integer
    '    Dim checktable As New DataTable()
    '    If Not String.IsNullOrWhiteSpace(shbi_acct_no) Then
    '        Using con As New SqlConnection(SQLConnectionString)
    '            con.Open()
    '            Dim query As String = "INSERT INTO [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] ([Date],[Repayment date],[Execute date],[Product code],[Product cif],[Product acc no],[shbi cif no],[shbi acct no],[Balanced],[auto transfer acno],[Flag],[Tenor no],[Remain Tenor]) VALUES(@date,@repayment_date,@execution_date,@Product_code,@Product_cif,@Product_acc_no,@shbi_cif_no,@shbi_acct_no,@Balanced,@auto_transfer_acno,@Flag,@Tenor_no,@remain_Tenor)"

    '            Using cmd As New SqlCommand(query, con)

    '                Dim date1 As String = dt_payment.ToString
    '                Dim getYear1 As String = Strings.Left(date1, 4)
    '                Dim getMonth1 As String = Strings.Mid(date1, 5, 2)
    '                Dim getDay1 As String = Strings.Right(date1, 2)
    '                Dim str_date_payment1 As String
    '                Dim get_date_payment1 As DateTime
    '                str_date_payment1 = getYear1 + "-" + getMonth1 + "-" + getDay1
    '                'convert to datetime
    '                get_date_payment1 = Convert.ToDateTime(str_date_payment1)
    '                cmd.Parameters.AddWithValue("@date", get_date_payment1)

    '                cmd.Parameters.AddWithValue("@repayment_date", get_date_payment1)

    '                Dim date3 As String = dt_payment.ToString
    '                Dim getYear3 As String = Strings.Left(date3, 4)
    '                Dim getMonth3 As String = Strings.Mid(date3, 5, 2)
    '                Dim getDay3 As String = Strings.Right(date3, 2)
    '                Dim str_date_payment3 As String
    '                Dim get_date_payment3 As DateTime
    '                str_date_payment3 = getYear3 + "-" + getMonth3 + "-" + getDay3
    '                'convert to datetime
    '                get_date_payment3 = Convert.ToDateTime(str_date_payment3)
    '                cmd.Parameters.AddWithValue("@execution_date", get_date_payment3)

    '                cmd.Parameters.AddWithValue("@Product_code", tb_query_process.Item(3).ToString)
    '                cmd.Parameters.AddWithValue("@Product_cif", tb_query_process.Item(4).ToString)
    '                cmd.Parameters.AddWithValue("@Product_acc_no", tb_query_process.Item(5).ToString)
    '                cmd.Parameters.AddWithValue("@shbi_cif_no", tb_query_process.Item(6).ToString)
    '                cmd.Parameters.AddWithValue("@shbi_acct_no", shbi_acct_no.ToString)
    '                cmd.Parameters.AddWithValue("@Balanced", tb_query_process.ToString)
    '                cmd.Parameters.AddWithValue("@auto_transfer_acno", tb_query_process.Item(32).ToString)
    '                cmd.Parameters.AddWithValue("@Flag", flag.ToString)
    '                cmd.Parameters.AddWithValue("@Tenor_no", tb_query_process.Item(14).ToString)
    '                cmd.Parameters.AddWithValue("@remain_Tenor", tb_query_process.Item(13).ToString)
    '                cmd.ExecuteNonQuery()

    '            End Using
    '        End Using
    '        n_addRows = 1
    '    Else
    '        n_addRows = 0
    '    End If
    '    Return n_addRows
    'End Function
    Private Sub cb_export_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_export.SelectedIndexChanged

        Try
            If Not cb_export.SelectedIndex = 0 Then
                Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                Dim strdate As String
                Dim strproduct As String = ""
                Dim filename1 As String = ""
                Dim extention As String
                getdate.ToString("ddMMyyyyHHmmss")
                strdate = getdate.ToString("yyyy") + getdate.ToString("MM") + getdate.ToString("dd") + getdate.ToString("hh") + getdate.ToString("mm") + getdate.ToString("ss")
                strproduct = cb_product.SelectedItem.ToString
                'kredivo : type report
                If cb_report.SelectedIndex.ToString = 0 Then
                    'report reject
                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Confirmation_Rejected_[" + strdate + "]" + extention
                        Call ExportDataToExcel(tableTemp, filename1)
                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".txt"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Confirmation_Rejected_[" + strdate + "]" + extention
                        Call DataTableToExportText(tableTemp, filename1)
                    End If
                ElseIf cb_report.SelectedIndex.ToString = 1 Then
                    'report early
                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Confirmation_Early_Termination_Payment_[" + strdate + "]" + extention
                        Call ExportDataToExcel(tableTemp, filename1)
                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".txt"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Confirmation_Early_Termination_Payment_[" + strdate + "]" + extention
                        Call DataTableToExportText(tableTemp, filename1)
                    End If
                ElseIf cb_report.SelectedIndex.ToString = 2 Then
                    'report reguler
                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Confirmation_Payment_[" + strdate + "]" + extention
                        Call ExportDataToExcel(tableTemp, filename1)
                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".txt"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Confirmation_Payment_[" + strdate + "]" + extention
                        Call DataTableToExportText(tableTemp, filename1)
                    End If
                ElseIf cb_report.SelectedIndex.ToString = 3 Then
                    'report cancellation
                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Confirmation_Cancellation_[" + strdate + "]" + extention
                        Call ExportDataToExcel(tableTemp, filename1)
                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".txt"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Confirmation_Cancellation_[" + strdate + "]" + extention
                        Call DataTableToExportText(tableTemp, filename1)
                    End If
                ElseIf cb_report.SelectedIndex.ToString = 4 Then
                    'report buyback
                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Buyback_Payment_[" + strdate + "]" + extention
                        Call ExportDataToExcel(tableTemp, filename1)
                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".txt"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Buyback_Payment_[" + strdate + "]" + extention
                        Call DataTableToExportText(tableTemp, filename1)
                    End If
                ElseIf cb_report.SelectedIndex.ToString = 5 Then
                    'report buyback
                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"
                        'for regular payment report
                        filename1 = "D:\OUTPUT\" + strproduct + "_Regular_Payment_[" + strdate + "]" + extention
                        Call ExportDataToExcel(tableTemp2, filename1)
                        'for partial payment report
                        filename1 = "D:\OUTPUT\" + strproduct + "_Partial_Payment_[" + strdate + "]" + extention
                        Call ExportDataToExcel(tableTemp, filename1)
                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".txt"
                        'for partial payment report
                        filename1 = "D:\OUTPUT\" + strproduct + "_Partial_Payment_[" + strdate + "]" + extention
                        Call DataTableToExportText(tableTemp, filename1)
                        'for regular payment report
                        filename1 = "D:\OUTPUT\" + strproduct + "_Regular_Payment_[" + strdate + "]" + extention
                        Call DataTableToExportText(tableTemp2, filename1)
                    End If
                ElseIf cb_report.SelectedIndex.ToString = 6 Then
                    'report data osta ostanding
                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Outstanding_[" + strdate + "]" + extention
                        Call ExportDataToExcel(tableTemp, filename1)
                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".txt"
                        filename1 = "D:\OUTPUT\" + strproduct + "_Outstanding_[" + strdate + "]" + extention
                        Call DataTableToExportText(tableTemp, filename1)
                    End If
                End If

                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub ExportDataToExcel(ByRef tableOutput As DataTable, ByRef filename As String)
        Using out = New XLWorkbook()
            out.Worksheets.Add(tableOutput, "Sheet1")
            out.SaveAs(filename)
        End Using

    End Sub

    Private Sub cb_takedown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_takedown.SelectedIndexChanged
        If Not cb_takedown.SelectedIndex = 0 Then
            Try
                Dim ds As New DataSet()
                'before clear datatable
                OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                OpenFileDialog1.Filter = "Excel Files (*.xls)|*.xls|Excel Files (*.xlsx)|*.xlsx|Text Files (*.txt)|*.txt"
                If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    'load event start
                    Application.DoEvents()
                    Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                    Dim filename As String = OpenFileDialog1.FileName
                    excel = fi.FullName

                    Dim extStr As String = Path.GetExtension(OpenFileDialog1.FileName)
                    'fomat extenstions
                    If extStr = ".xls" Or extStr = ".xlsx" Then
                        '(2) reader excel to Datatable
                        conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
                        conn.Open()
                        Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                        Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                        Dim da As New OleDbDataAdapter(sqlquery, conn)
                        da.Fill(ds)
                        tableCIFTakedown = ds.Tables(0)
                        conn.Close()
                    ElseIf extStr = ".txt" Then
                        '(2) reader txt to Datatable
                        Dim SR As StreamReader = New StreamReader(excel)
                        Dim line As String = SR.ReadLine()
                        Dim strArray As String() = line.Split("|"c)
                        Dim row As DataRow
                        'call header Loan Column
                        For Each s As String In strArray
                            tableCIFTakedown.Columns.Add(New DataColumn())
                        Next
                        Do
                            line = SR.ReadLine
                            If Not line = String.Empty Then
                                row = tableCIFTakedown.NewRow()
                                row.ItemArray = line.Split("|"c)
                                tableCIFTakedown.Rows.Add(row)
                            Else
                                Exit Do
                            End If
                        Loop

                    End If

                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                'event load data
                Application.DoEvents()
                ' Disabled Button before process
                ' Show Count Rows in Datagridview
                If Not tableCIFTakedown Is Nothing Then
                    C_data3.Text = "(OK)"
                Else
                    C_data3.Text = "(False)"
                End If
            End Try
        End If
    End Sub

    Private Sub btn_import_approved_Click(sender As Object, e As EventArgs) Handles btn_import_approved.Click
        Try
            Dim ds As New DataSet()
            'before clear datatable
            C_data1.Text = ""
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xls)|*.xls|Excel Files (*.xlsx)|*.xlsx"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'clear datable
                tableReject.Rows.Clear()
                tableReject.Columns.Clear()

                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                excel = fi.FullName

                Dim extStr As String = Path.GetExtension(OpenFileDialog1.FileName)
                'fomat extenstions
                If extStr = ".xls" Or extStr = ".xlsx" Then
                    '(2) reader excel to Datatable
                    conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
                    conn.Open()
                    Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                    Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                    Dim da As New OleDbDataAdapter(sqlquery, conn)
                    da.Fill(ds)
                    'Filter the rows using Select() method of DataTable
                    table2361 = ds.Tables(0)
                    'filter rows
                    Dim filterReject As String = String.Format("[" + table2361.Columns(4).ColumnName + "] LIKE '*{0}*'", "REJECT")
                    Dim Filter As DataRow() = table2361.Select(filterReject)
                    If Filter.Length > 0 Then
                        'load data reject filter
                        tableReject = Filter.CopyToDataTable()
                    Else
                        tableReject = Nothing
                    End If
                    'close oldbconnection
                    conn.Close()
                End If


            End If
        Catch ex As Exception
        MessageBox.Show(ex.Message)
        Finally
        'event load data
        Application.DoEvents()
        ' Show Count Rows in Datagridview
        If Not table2361 Is Nothing Then
            C_data1.Text = "(OK)"
        Else
            C_data1.Text = "(False)"
        End If

        End Try
    End Sub

    Private Sub clearDatable()
        'clear table
        tableTemp.Columns.Clear()
        tableTemp.Rows.Clear()
        tableTemp2.Columns.Clear()
        tableTemp2.Rows.Clear()
        tableTemp3.Columns.Clear()
        tableTemp3.Rows.Clear()
    End Sub
    Private Sub btn_generate_Click(sender As Object, e As EventArgs) Handles btn_generate.Click
        Try '
            Call clearDatable()
            If cb_report.SelectedIndex.ToString = 0 Then
                'report reject
                Dim noCIF, amountLoan, reason, multtifinance, reasonTakeout As String
                Dim rowRejectLoan As DataRow()
                Dim rowTakeoutLoan As DataRow()
                Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                Dim strdate As String
                Dim trx_type As String
                Dim multifinance_acc As String
                Dim arrReason As New ArrayList()
                Dim y As Integer = 0
                getdate.ToString("ddMMyyyyHHmmss")
                strdate = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd")
                'Add new column in loan 
                'copy column to datatable 
                If tableTemp.Columns.Count <= 0 Then
                    'Date|Multifinance CIF|Multifinance Account|SHBI CIF|Amount|Trx Type|Reason
                    tableTemp.Columns.Add("Date")
                    tableTemp.Columns.Add("Multifinance CIF")
                    tableTemp.Columns.Add("Multifinance Account")
                    tableTemp.Columns.Add("SHBI CIF")
                    tableTemp.Columns.Add("Amount")
                    tableTemp.Columns.Add("Trx Type")
                    tableTemp.Columns.Add("Reason")
                End If
                If tableReport.Columns.Count <= 0 Then
                    For j = 0 To tableTemp.Columns.Count - 1
                        tableReport.Columns.Add(tableTemp.Columns(j).ColumnName)
                    Next
                End If
                If Not IsNothing(tableReject) Then
                    'code key for loan and 2361 report
                    'Table 2361 Report
                    For x As Integer = 0 To tableLoanFinal.Rows.Count - 1
                        'clear rows
                        tableTakeout.Rows.Clear()
                        tableTakeout.Columns.Clear()
                        amountLoan = 0
                        noCIF = 0
                        reason = ""
                        multifinance_acc = ""
                        multtifinance = ""
                        trx_type = ""
                        arrReason.Clear()
                        'column 5 : CIF
                        'column 8 : Loan Amount

                        'Group 2 Columns
                        '===LOAN FINAL===
                        '5 : cif
                        '26 : loan amount
                        noCIF = Trim(tableLoanFinal.Rows(x).Item(3).ToString)
                        amountLoan = Trim(tableLoanFinal.Rows(x).Item(26).ToString)
                        multtifinance = Trim(tableLoanFinal.Rows(x).Item(98).ToString)
                        multifinance_acc = Trim(tableLoanFinal.Rows(x).Item(99).ToString)
                        trx_type = Trim(tableLoanFinal.Rows(x).Item(84).ToString)
                        'clean symbol
                        amountLoan = Replace(amountLoan, ".00", "")

                        'filter rows reject
                        If Not String.IsNullOrWhiteSpace(noCIF) Then
                            Dim filterReject As String = String.Format("[" + tableReject.Columns(5).ColumnName + "] ='" + noCIF.ToString + "' AND [" + tableReject.Columns(8).ColumnName + "] ='" + amountLoan.ToString + "' AND [" + tableReject.Columns(4).ColumnName + "] LIKE '*{0}*'", "Reject")
                            rowRejectLoan = tableReject.Select(filterReject)
                            ' Date|Multifinance CIF|Multifinance Account|SHBI CIF|Amount|Trx Type|Reason
                            If Not rowRejectLoan.Length <= 0 Then
                                tableTakeout = rowRejectLoan.CopyToDataTable()
                                'load data reject filter
                                For a As Integer = 0 To tableTakeout.Rows.Count - 1
                                    reason = tableTakeout.Rows(a).Item(19).ToString()
                                    'add new rows in table
                                    rowTemp2 = tableTemp.NewRow()
                                    tableTemp.Rows.Add(rowTemp2)
                                    'add data table temp from filter
                                    tableTemp.Rows(y).Item(0) = strdate
                                    tableTemp.Rows(y).Item(1) = multtifinance.ToString()
                                    tableTemp.Rows(y).Item(2) = multifinance_acc.ToString()
                                    tableTemp.Rows(y).Item(3) = noCIF.ToString()
                                    tableTemp.Rows(y).Item(4) = amountLoan.ToString()
                                    tableTemp.Rows(y).Item(5) = trx_type.ToString()
                                    If a = tableTakeout.Rows.Count - 1 Then
                                        arrReason.Add(reason.ToString)
                                        tableTemp.Rows(y).Item(6) = String.Join(",", TryCast(arrReason.ToArray(GetType(String)), String()))
                                    Else
                                        arrReason.Add(reason.ToString)
                                    End If
                                    'take events
                                    Application.DoEvents()

                                    tableReport.ImportRow(tableTemp.Rows(y))
                                    'show report configurations
                                    load_result_report.DataSource = tableReport
                                    'next rows
                                    y += 1
                                    'set status rows column
                                Next
                            End If

                        End If
                    Next
                End If
                arrReason.Clear()
                'clear rows
                'tableTakeout.Rows.Clear()
                'tableTakeout.Columns.Clear()
                If Not IsNothing(tableCIFTakedown) Then
                    'code key for loan and 2361 report
                    'Table 2361 Report
                    For b As Integer = 0 To tableLoanFile.Rows.Count - 1
                        amountLoan = 0
                        noCIF = ""
                        reasonTakeout = ""
                        multifinance_acc = ""
                        multtifinance = ""
                        trx_type = ""

                        amountLoan = Trim(tableLoanFile.Rows(b).Item(26).ToString)
                        multtifinance = Trim(tableLoanFile.Rows(b).Item(98).ToString)
                        multifinance_acc = Trim(tableLoanFile.Rows(b).Item(99).ToString)
                        trx_type = Trim(tableLoanFile.Rows(b).Item(84).ToString)
                        'clean symbol
                        amountLoan = Replace(amountLoan, ".00", "")
                        'filter rows takeout
                        If Not tableCIFTakedown Is Nothing Then
                            Dim filterTakeout As String = String.Format("[" + tableCIFTakedown.Columns(79).ColumnName + "] ='" + multtifinance.ToString + "'")
                            rowTakeoutLoan = tableCIFTakedown.Select(filterTakeout)
                            ' Date|Multifinance CIF|Multifinance Account|SHBI CIF|Amount|Trx Type|Reason
                            If Not rowTakeoutLoan.Length <= 0 Then
                                'load data reject filter
                                tableTakeout = rowTakeoutLoan.CopyToDataTable()
                                For z As Integer = 0 To tableTakeout.Rows.Count - 1
                                    reasonTakeout = tableTakeout.Rows(z).Item(88).ToString
                                    'add new rows in table
                                    rowTemp2 = tableTemp.NewRow()
                                    tableTemp.Rows.Add(rowTemp2)
                                    'add data table temp from filter
                                    tableTemp.Rows(y).Item(0) = strdate
                                    tableTemp.Rows(y).Item(1) = multtifinance.ToString()
                                    tableTemp.Rows(y).Item(2) = multifinance_acc.ToString()
                                    tableTemp.Rows(y).Item(3) = noCIF.ToString()
                                    tableTemp.Rows(y).Item(4) = amountLoan.ToString()
                                    tableTemp.Rows(y).Item(5) = trx_type.ToString()
                                    tableTemp.Rows(y).Item(6) = reasonTakeout.ToString()
                                    'take events
                                    Application.DoEvents()

                                    tableReport.ImportRow(tableTemp.Rows(y))
                                    'show report configurations
                                    load_result_report.DataSource = tableReport
                                    'next rows
                                    y += 1
                                    'set status rows column
                                    Application.DoEvents()
                                Next
                                'clear rows
                                tableTakeout.Rows.Clear()
                                tableTakeout.Columns.Clear()

                            End If
                        End If
                    Next
                End If

            ElseIf cb_report.SelectedIndex.ToString = 1 Then
                'Report Early
                'copy column to datatable 
                If tableTemp.Columns.Count <= 0 Then
                    'Date|Transaction|Multifinance CIF|Multifinance Account|SHBI CIF|SHBI Account|Amount|Repayment Account
                    '20211215|Early Termination|5023930|127686267|2931350361|730007483013|555738|701000068059
                    tableTemp.Columns.Add("Date")
                    tableTemp.Columns.Add("Transaction")
                    tableTemp.Columns.Add("Multifinance CIF")
                    tableTemp.Columns.Add("Multifinance Account")
                    tableTemp.Columns.Add("SHBI CIF")
                    tableTemp.Columns.Add("SHBI Account")
                    tableTemp.Columns.Add("Amount")
                    tableTemp.Columns.Add("Repayment Account")
                End If
                If tableReport.Columns.Count <= 0 Then
                    For j = 0 To tableTemp.Columns.Count - 1
                        tableReport.Columns.Add(tableTemp.Columns(j).ColumnName)
                    Next
                End If
                'get data and set any column in datatable
                If Not IsNothing(tableCIFTakedown) Then
                    Dim z As Integer = 0
                    For x As Integer = 0 To tableCIFTakedown.Rows.Count - 1
                        Dim amount As String = ""
                        rowTemp2 = tableTemp.NewRow()
                        tableTemp.Rows.Add(rowTemp2)
                        tableTemp.Rows(z).Item(0) = tableCIFTakedown.Rows(x).Item(0).ToString()
                        tableTemp.Rows(z).Item(1) = "Early Termination"
                        tableTemp.Rows(z).Item(2) = tableCIFTakedown.Rows(x).Item(4).ToString()
                        tableTemp.Rows(z).Item(3) = tableCIFTakedown.Rows(x).Item(5).ToString()
                        tableTemp.Rows(z).Item(4) = tableCIFTakedown.Rows(x).Item(6).ToString()
                        tableTemp.Rows(z).Item(5) = tableCIFTakedown.Rows(x).Item(8).ToString()
                        Dim arrSymbol() As String = {".00", "."}
                        'clean symbol
                        For i As Integer = 0 To arrSymbol.Length - 1
                            amount = Replace(tableCIFTakedown.Rows(x).Item(31).ToString(), arrSymbol(i), "")
                        Next i
                        tableTemp.Rows(z).Item(6) = amount
                        tableTemp.Rows(z).Item(7) = tableCIFTakedown.Rows(x).Item(32).ToString()
                        'take events
                        Application.DoEvents()

                        tableReport.ImportRow(tableTemp.Rows(z))
                        'show report configurations
                        load_result_report.DataSource = tableReport
                        'next rows
                        z += 1
                        'set status rows column
                        C_rows1.Text = load_result_report.Rows.Count - 1
                    Next

                End If

            ElseIf cb_report.SelectedIndex.ToString = 2 Then
                'Report Reguler
                'copy column to datatable 
                If tableTemp.Columns.Count <= 0 Then
                    'Date|Transaction|Multifinance CIF|Multifinance Account|SHBI CIF|SHBI Account|Amount|Repayment Account
                    '20211213|Reoayment|34676236|127523925|2935803501|730007482573|506339|701000068059
                    tableTemp.Columns.Add("Date")
                    tableTemp.Columns.Add("Transaction")
                    tableTemp.Columns.Add("Multifinance CIF")
                    tableTemp.Columns.Add("Multifinance Account")
                    tableTemp.Columns.Add("SHBI CIF")
                    tableTemp.Columns.Add("SHBI Account")
                    tableTemp.Columns.Add("Amount")
                    tableTemp.Columns.Add("Repayment Account")
                End If
                If tableReport.Columns.Count <= 0 Then
                    For j = 0 To tableTemp.Columns.Count - 1
                        tableReport.Columns.Add(tableTemp.Columns(j).ColumnName)
                    Next
                End If
                'get data and set any column in datatable
                If Not IsNothing(tableCIFTakedown) Then
                    Dim z As Integer = 0
                    For x As Integer = 0 To tableCIFTakedown.Rows.Count - 1
                        rowTemp2 = tableTemp.NewRow()
                        tableTemp.Rows.Add(rowTemp2)
                        tableTemp.Rows(z).Item(0) = tableCIFTakedown.Rows(x).Item(0).ToString()
                        tableTemp.Rows(z).Item(1) = "Repayment"
                        tableTemp.Rows(z).Item(2) = tableCIFTakedown.Rows(x).Item(4).ToString()
                        tableTemp.Rows(z).Item(3) = tableCIFTakedown.Rows(x).Item(5).ToString()
                        tableTemp.Rows(z).Item(4) = tableCIFTakedown.Rows(x).Item(6).ToString()
                        tableTemp.Rows(z).Item(5) = tableCIFTakedown.Rows(x).Item(8).ToString()
                        Dim arrSymbol() As String = {".00", "."}
                        'clean symbol
                        Dim amount As String = ""
                        For i As Integer = 0 To arrSymbol.Length - 1
                            amount = Replace(tableCIFTakedown.Rows(x).Item(31).ToString(), arrSymbol(i), "")
                        Next i
                        tableTemp.Rows(z).Item(6) = amount
                        tableTemp.Rows(z).Item(7) = tableCIFTakedown.Rows(x).Item(32).ToString()
                        'take events
                        Application.DoEvents()

                        tableReport.ImportRow(tableTemp.Rows(z))
                        'show report configurations
                        load_result_report.DataSource = tableReport
                        'next rows
                        z += 1
                        'set status rows column
                    Next

                End If


            ElseIf cb_report.SelectedIndex.ToString = 3 Then
                Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                Dim strdate As String
                Dim name_product As String
                Dim symbol As String
                Dim y As Integer = 0
                Dim amount As String = ""
                getdate.ToString("ddMMyyyyHHmmss")
                strdate = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd")
                'Report Cancel
                'copy column to datatable 
                If tableTemp.Columns.Count <= 0 Then
                    'Date|Transaction|Multifinance Account|SHBI CIF|SHBI Account|Amount|Cancellation Account
                    '20211119|Cancellation|127469666|2935730083|730007481852|1680246|701000068040

                    tableTemp.Columns.Add("Date")
                    tableTemp.Columns.Add("Transaction")
                    tableTemp.Columns.Add("Multifinance Account")
                    tableTemp.Columns.Add("SHBI CIF")
                    tableTemp.Columns.Add("SHBI Account")
                    tableTemp.Columns.Add("Amount")
                    tableTemp.Columns.Add("Cancellation Account")
                End If
                If tableReport.Columns.Count <= 0 Then
                    For j = 0 To tableTemp.Columns.Count - 1
                        tableReport.Columns.Add(tableTemp.Columns(j).ColumnName)
                    Next
                End If
                'get data and set any column in datatable
                If Not IsNothing(tableCIFTakedown) Then
                    Dim z As Integer = 0
                    For x As Integer = 0 To tableCIFTakedown.Rows.Count - 1
                        rowTemp2 = tableTemp.NewRow()
                        tableTemp.Rows.Add(rowTemp2)
                        tableTemp.Rows(z).Item(0) = strdate.ToString()
                        tableTemp.Rows(z).Item(1) = "Cancellation"
                        tableTemp.Rows(z).Item(2) = tableCIFTakedown.Rows(x).Item(0).ToString()
                        tableTemp.Rows(z).Item(3) = tableCIFTakedown.Rows(x).Item(1).ToString()
                        tableTemp.Rows(z).Item(4) = tableCIFTakedown.Rows(x).Item(2).ToString()
                        'clean symbol
                        amount = Replace(tableCIFTakedown.Rows(x).Item(4).ToString(), ".00", "")
                        tableTemp.Rows(z).Item(5) = amount
                        'get account in table
                        symbol = Strings.Left(cb_product.Text.ToString, 4)
                        '  Console.WriteLine(symbol)
                        name_product = Replace(cb_product.Text.ToString, symbol, "")
                        'Console.WriteLine(name_product)
                        tableTemp.Rows(z).Item(6) = getNoAccountsType("DISBURSEMENT", tableCIFTakedown.Rows(x).Item(3).ToString).ToString()
                        'take events
                        Application.DoEvents()

                        tableReport.ImportRow(tableTemp.Rows(z))
                        'show report configurations
                        load_result_report.DataSource = tableReport
                        'next rows
                        z += 1
                        'set status rows column
                    Next

                End If

            ElseIf cb_report.SelectedIndex.ToString = 4 Then
                'Report Buyback
                'copy column to datatable 
                If tableTemp.Columns.Count <= 0 Then
                    tableTemp.Columns.Add("Date")
                    tableTemp.Columns.Add("Transaction")
                    tableTemp.Columns.Add("Multifinance CIF")
                    tableTemp.Columns.Add("Multifinance Account")
                    tableTemp.Columns.Add("SHBI CIF")
                    tableTemp.Columns.Add("SHBI Account")
                    tableTemp.Columns.Add("Amount")
                    tableTemp.Columns.Add("Repayment Account")
                End If

                If tableReport.Columns.Count <= 0 Then
                    For j = 0 To tableTemp.Columns.Count - 1
                        tableReport.Columns.Add(tableTemp.Columns(j).ColumnName)
                    Next
                End If

                'get data and set any column in datatable
                If Not IsNothing(tableCIFTakedown) Then
                    Dim z As Integer = 0
                    For x As Integer = 0 To tableCIFTakedown.Rows.Count - 1
                        rowTemp2 = tableTemp.NewRow()
                        tableTemp.Rows.Add(rowTemp2)
                        tableTemp.Rows(z).Item(0) = tableCIFTakedown.Rows(x).Item(0).ToString()
                        tableTemp.Rows(z).Item(1) = "Buyback"
                        tableTemp.Rows(z).Item(2) = tableCIFTakedown.Rows(x).Item(4).ToString()
                        tableTemp.Rows(z).Item(3) = tableCIFTakedown.Rows(x).Item(5).ToString()
                        tableTemp.Rows(z).Item(4) = tableCIFTakedown.Rows(x).Item(6).ToString()
                        tableTemp.Rows(z).Item(5) = tableCIFTakedown.Rows(x).Item(8).ToString()
                        Dim arrSymbol() As String = {".00", "."}
                        'clean symbol
                        Dim amount As String = ""
                        For i As Integer = 0 To arrSymbol.Length - 1
                            amount = Replace(tableCIFTakedown.Rows(x).Item(31).ToString(), arrSymbol(i), "")
                        Next i
                        tableTemp.Rows(z).Item(6) = amount
                        tableTemp.Rows(z).Item(7) = tableCIFTakedown.Rows(x).Item(32).ToString()

                        'take events
                        Application.DoEvents()

                        tableReport.ImportRow(tableTemp.Rows(z))
                        'show report configurations
                        load_result_report.DataSource = tableReport
                        'next rows
                        z += 1
                        'set status rows column
                    Next

                End If


            ElseIf cb_report.SelectedIndex.ToString = 5 Then
                'Note :
                ' tableTemp : table report partial payment
                ' tableTemp2 : table report regular payment
                'partial report 
                'declare variabel
                Dim SHBI_Account, n_key1, n_key2, n_principal, n_interest As String
                Dim total_amount, total_amount_process, balance_amount, get_total_balance, sum_total_balance, total_balance As Double
                Dim rowDataRegular, rowDataPartial, rowDataBalance, rowDataOnDuedate1, rowDataOnDuedate2 As DataRow
                Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                Dim strdate, strdatePayment, str_process, str_reason As String
                Dim arrReason As New ArrayList()
                Dim tb_Balance_COA As New DataTable
                Dim y As Integer = 0
                Dim index As Integer = 0
                Dim index2 As Integer = 0
                Dim max_tenor, n_tenor, remain_tenor As Integer
                n_tenor = 0
                remain_tenor = 0
                max_tenor = 0
                balance_amount = 0
                total_balance = 0
                sum_total_balance = 0
                get_total_balance = 0
                total_amount_process = 0
                n_principal = 0
                n_interest = 0
                'inisialisasi variabel
                getdate.ToString("ddMMyyyyHHmmss")
                strdate = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd")
                'strdateGenerate = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd") + getdate.ToString("hh") + getdate.ToString("mm") + getdate.ToString("ss")
                rowDataPartial = Nothing
                rowDataRegular = Nothing
                str_process = ""
                str_reason = ""
                'for table partial report
                If tableTemp.Columns.Count <= 0 Then
                    'Date	Flag	Multifinance	Multifinance Account	SHBI CIF	SHBI Account	Amount	Term No	Repayment Account	Balance Amount	Total Balance	Reason	Status
                    tableTemp.Columns.Add("Date")
                    tableTemp.Columns.Add("Flag")
                    tableTemp.Columns.Add("Multifinance CIF")
                    tableTemp.Columns.Add("Multifinance Account")
                    tableTemp.Columns.Add("SHBI CIF")
                    tableTemp.Columns.Add("SHBI Account")
                    tableTemp.Columns.Add("Amount")
                    tableTemp.Columns.Add("TermNo")
                    tableTemp.Columns.Add("Repayment Account")
                    tableTemp.Columns.Add("Balance Amount")
                    tableTemp.Columns.Add("Total Balance")
                    tableTemp.Columns.Add("Reason")
                    tableTemp.Columns.Add("Status")
                End If
                'for table regular report
                If tableTemp2.Columns.Count <= 0 Then
                    'Date	Transaction	Multifinance CIF	Multifinance Account	SHBI CIF	SHBI Account	Amount	Repayment Account
                    tableTemp2.Columns.Add("Date")
                    tableTemp2.Columns.Add("Transaction")
                    tableTemp2.Columns.Add("Multifinance CIF")
                    tableTemp2.Columns.Add("Multifinance Account")
                    tableTemp2.Columns.Add("SHBI CIF")
                    tableTemp2.Columns.Add("SHBI Account")
                    tableTemp2.Columns.Add("Amount")
                    tableTemp2.Columns.Add("Repayment Account")
                End If
                'FIRST : GENERATE PARTIAL & PAYMENT (BALANCE 0)
                If Not IsNothing(tableCIFTakedown) Then
                    'code key for loan and 2361 report
                    'Table 2361 Report
                    'create new table row
                    For x As Integer = 0 To tableCIFTakedown.Rows.Count - 1

                        'split data report : 
                        '- Regular report in condition : if amount  file is same with 1200 process and the output as regular report
                        '- Partial report in condition : if amoount more or not enough with 1200 process and not found also in 1200 query,it  will be as partial report
                        'filter rows file regular
                        '(1) SHBI Account Number
                        strdatePayment = Trim(tableCIFTakedown.Rows(x).Item(0).ToString)
                        SHBI_Account = Trim(tableCIFTakedown.Rows(x).Item(8).ToString)
                        n_principal = Trim(tableCIFTakedown.Rows(x).Item(28))
                        n_interest = Trim(tableCIFTakedown.Rows(x).Item(29))
                        n_tenor = Trim(tableCIFTakedown.Rows(x).Item(14).ToString)
                        remain_tenor = Trim(tableCIFTakedown.Rows(x).Item(13).ToString)
                        max_tenor = Convert.ToInt32(n_tenor) + Convert.ToInt32(remain_tenor)
                        'check account has running as regular payment
                        Dim filterRegular As String = String.Format("[" + tableLoanFinal.Columns(0).ColumnName + "] ='" + SHBI_Account.ToString + "' AND [" + tableLoanFinal.Columns(7).ColumnName + "] ='" + n_principal.ToString + "' AND [" + tableLoanFinal.Columns(8).ColumnName + "] ='" + n_interest.ToString + "' ")
                        rowDataRegular = tableLoanFinal.Select(filterRegular).FirstOrDefault()
                        ' Date|Multifinance CIF|Multifinance Account|SHBI CIF|Amount|Trx Type|Reason
                        If Not rowDataRegular Is Nothing Then
                            ' rows count is more 0
                            'Date	Transaction	Multifinance CIF	Multifinance Account	SHBI CIF	SHBI Account	Amount	Repayment Account
                            'get data total amount from file upload
                            total_amount = Convert.ToDouble(n_principal) + Convert.ToDouble(n_interest)
                            'add data new table
                            rowTemp = tableTemp2.NewRow()
                            tableTemp2.Rows.Add(rowTemp)
                            tableTemp2.Rows(index).Item(0) = strdatePayment.ToString
                            tableTemp2.Rows(index).Item(1) = "Repayment"
                            tableTemp2.Rows(index).Item(2) = tableCIFTakedown.Rows(x).Item(4).ToString
                            tableTemp2.Rows(index).Item(3) = tableCIFTakedown.Rows(x).Item(5).ToString
                            tableTemp2.Rows(index).Item(4) = tableCIFTakedown.Rows(x).Item(6).ToString
                            tableTemp2.Rows(index).Item(5) = tableCIFTakedown.Rows(x).Item(8).ToString
                            tableTemp2.Rows(index).Item(6) = total_amount.ToString
                            tableTemp2.Rows(index).Item(7) = tableCIFTakedown.Rows(x).Item(32).ToString
                            index += 1
                        Else
                            'get data balance in database COA
                            'inisialisasi variabel balance from COA
                            'tb_Balance_COA = getDataTotalBalanceCOA(SHBI_Account.ToString)
                            'If tb_Balance_COA.Rows.Count > 0 Then
                            '    total_balance = tb_Balance_COA.Rows(0).Item(1)
                            'Else
                            '    total_balance = 0
                            'End If
                            'get data COA Balance from Query File COA Balance (before payment)
                            If Not tableLoanFile Is Nothing Then
                                'filter data balance
                                Dim filterCOABalance As String = String.Format("[" + tableLoanFile.Columns(0).ColumnName + "] ='" + SHBI_Account.ToString + "'")
                                rowDataBalance = tableLoanFile.Select(filterCOABalance).FirstOrDefault()
                                If Not rowDataBalance Is Nothing Then
                                    If Not String.IsNullOrWhiteSpace(rowDataBalance.Item(1).ToString) Then
                                        total_balance = Convert.ToDouble(rowDataBalance.Item(1).ToString)
                                    Else
                                        total_balance = 0
                                    End If
                                Else
                                    total_balance = 0
                                End If
                            Else
                                total_balance = 0
                            End If


                            'data direct to partial payment 
                            Dim filterPartial As String = String.Format("[" + tableLoanFinal.Columns(0).ColumnName + "] ='" + SHBI_Account.ToString + "'")
                            rowDataPartial = tableLoanFinal.Select(filterPartial).FirstOrDefault()
                            If Not rowDataPartial Is Nothing Then
                                'have balance and save COA : processss  (balance > 0)
                                'Date0	Flag1	Multifinance2	Multifinance Account3	SHBI CIF4	SHBI Account5	Amount6	Term No6	Repayment Account7	Balance Amount8	Total Balance9	Reason10	Status11

                                'calculation balance for COA
                                'total_amount = Convert.ToDouble(n_principal) + Convert.ToDouble(n_interest)
                                total_amount = Convert.ToDouble(Trim(tableCIFTakedown.Rows(x).Item(31).ToString))
                                total_amount_process = rowDataPartial.Item(7) + rowDataPartial.Item(8)
                                'get total balance
                                sum_total_balance = (total_balance + total_amount)
                                'second : total balance - process amount
                                If sum_total_balance >= total_amount_process Then
                                    'first : balance amount + total balance
                                    balance_amount = (total_amount - total_amount_process)
                                    get_total_balance = (sum_total_balance - total_amount_process)
                                    str_process = "Processed"
                                    str_reason = "Balance Enough"
                                Else
                                    balance_amount = total_amount
                                    get_total_balance = sum_total_balance
                                    str_process = "Processed with Partial"
                                    str_reason = "Balance Not Enough"
                                End If
                            Else
                                'just partial and not processed and saving to COA
                                'calculation balance for COA
                                'total_amount = Convert.ToDouble(n_principal) + Convert.ToDouble(n_interest)
                                total_amount = Convert.ToDouble(Trim(tableCIFTakedown.Rows(x).Item(31).ToString))
                                balance_amount = total_amount
                                get_total_balance = (total_balance + total_amount)
                                str_process = "Processed with Partial"
                                str_reason = "Not In Due"
                            End If


                            'add data new table
                            rowTemp2 = tableTemp.NewRow()
                            tableTemp.Rows.Add(rowTemp2)
                            tableTemp.Rows(index2).Item(0) = strdatePayment.ToString
                            tableTemp.Rows(index2).Item(1) = "Partial"
                            tableTemp.Rows(index2).Item(2) = tableCIFTakedown.Rows(x).Item(4).ToString
                            tableTemp.Rows(index2).Item(3) = tableCIFTakedown.Rows(x).Item(5).ToString
                            tableTemp.Rows(index2).Item(4) = tableCIFTakedown.Rows(x).Item(6).ToString
                            tableTemp.Rows(index2).Item(5) = tableCIFTakedown.Rows(x).Item(8).ToString
                            tableTemp.Rows(index2).Item(6) = total_amount.ToString
                            tableTemp.Rows(index2).Item(7) = n_tenor.ToString
                            tableTemp.Rows(index2).Item(8) = tableCIFTakedown.Rows(x).Item(32).ToString
                            tableTemp.Rows(index2).Item(9) = balance_amount.ToString
                            tableTemp.Rows(index2).Item(10) = get_total_balance.ToString
                            tableTemp.Rows(index2).Item(11) = str_reason.ToString
                            tableTemp.Rows(index2).Item(12) = str_process.ToString
                            index2 += 1
                        End If
                        'clear variabel
                        balance_amount = 0
                        total_balance = 0
                        n_principal = 0
                        n_interest = 0
                        n_tenor = 0
                        str_reason = ""
                        str_process = ""
                        'take events
                        Application.DoEvents()
                        'show data to datagrdview
                        load_result_report.DataSource = tableTemp
                        load_result_report2.DataSource = tableTemp2
                        T_Rows1.Text = load_result_report.Rows.Count - 1
                        T_Rows2.Text = load_result_report2.Rows.Count - 1
                        'event load data
                        Application.DoEvents()
                    Next

                End If
                ''clear account number
                SHBI_Account = ""
                n_principal = ""
                n_interest = ""

            ElseIf cb_report.SelectedIndex.ToString = 6 Then
                Dim z As Integer = 0
                Dim SHBI_Account, str_total_balance As String
                Dim interest_product, overdue_day As Integer
                Dim loan_amount, dt_principalPMT, dt_interestPMT, ostanding_amount, get_selisih_principal As Double
                Dim max_tenor, n_tenor As Integer
                Dim total_balance As Double
                Dim rowDataBalance As DataRow
                Dim dueDateDisburseDT, endDueDate, nextPaymentDT, lastPaymentDate As Date
                Dim dt_calculationPMT, table_report As New DataTable
                'inisialisasi 
                SHBI_Account = ""
                n_tenor = 0
                str_total_balance = ""
                dt_principalPMT = 0
                If tableTemp.Columns.Count <= 0 Then
                    'Add new column for n tenor 
                    tableTemp.Columns.Add("Multifinance Account")
                    tableTemp.Columns.Add("SHBI Account")
                    tableTemp.Columns.Add("Date Disburse")
                    tableTemp.Columns.Add("End DueDate")
                    tableTemp.Columns.Add("Last Payment Date")
                    tableTemp.Columns.Add("Pokok Ostanding")
                    tableTemp.Columns.Add("Bunga Ostanding")
                    'tableTemp.Columns.Add("Loan Amount")
                    'tableTemp.Columns.Add("Max Tenor")
                    tableTemp.Columns.Add("Overdue Days")
                    tableTemp.Columns.Add("Term")
                    tableTemp.Columns.Add("COA Balance")
                End If
                'add new column 
                If table_report.Rows.Count <= 0 Then
                    For j = 0 To tableTemp.Columns.Count - 1
                        table_report.Columns.Add(tableTemp.Columns(j).ColumnName)
                    Next
                End If

                For x As Integer = 0 To tableCIFTakedown.Rows.Count - 1
                    rowDT = tableTemp.NewRow()
                    tableTemp.Rows.Add(rowDT)
                    loan_amount = 0
                    max_tenor = 0
                    n_tenor = 0
                    interest_product = 0
                    dt_interestPMT = 0
                    dt_calculationPMT = Nothing
                    'add rows data multifinance account from database disbusement
                    If cb_takedown.SelectedIndex = 1 Then
                        'add new rows for 9850 query
                        If Not checkDisbursement(tableCIFTakedown.Rows(x).Item(3).ToString) Is Nothing Then
                            tableTemp.Rows(z).Item(0) = checkDisbursement(tableCIFTakedown.Rows(x).Item(3).ToString).Rows(0).Item(3).ToString
                        Else
                            tableTemp.Rows(z).Item(0) = ""
                        End If
                        ''SHBI Account
                        SHBI_Account = tableCIFTakedown.Rows(x).Item(3).ToString
                        tableTemp.Rows(z).Item(1) = SHBI_Account.ToString
                    ElseIf cb_takedown.SelectedIndex = 2 Then
                        'add new rows for DT query
                        If Not checkDisbursement(tableCIFTakedown.Rows(x).Item(5).ToString) Is Nothing Then
                            tableTemp.Rows(z).Item(0) = checkDisbursement(tableCIFTakedown.Rows(x).Item(5).ToString).Rows(0).Item(3).ToString
                        Else
                            tableTemp.Rows(z).Item(0) = ""
                        End If
                        SHBI_Account = tableCIFTakedown.Rows(x).Item(5).ToString
                        tableTemp.Rows(z).Item(1) = SHBI_Account.ToString
                    End If

                    'create n tenor from date in next duedate and disburse date
                    'Date Disbursement
                    'check based on file query upload
                    If cb_takedown.SelectedIndex = 1 Then
                        'add new rows for 9850 query
                        If Not String.IsNullOrWhiteSpace(tableCIFTakedown.Rows(x).Item(27).ToString) Then
                            Dim getYear1 As String = Strings.Left(tableCIFTakedown.Rows(x).Item(27).ToString, 4)
                            Dim getMonth1 As String = Strings.Mid(tableCIFTakedown.Rows(x).Item(27).ToString, 5, 2)
                            Dim getDay1 As String = Strings.Right(tableCIFTakedown.Rows(x).Item(27).ToString, 2)
                            dueDateDisburseDT = Convert.ToDateTime(getYear1 + "-" + getMonth1 + "-" + getDay1)
                            tableTemp.Rows(z).Item(2) = dueDateDisburseDT.ToString("dd-MM-yyyy")
                        Else
                            tableTemp.Rows(z).Item(2) = ""
                        End If

                    ElseIf cb_takedown.SelectedIndex = 2 Then
                        'add new rows for DT query
                        If Not String.IsNullOrWhiteSpace(tableCIFTakedown.Rows(x).Item(51).ToString) Then
                            Dim getYear1 As String = Strings.Left(tableCIFTakedown.Rows(x).Item(51).ToString, 4)
                            Dim getMonth1 As String = Strings.Mid(tableCIFTakedown.Rows(x).Item(51).ToString, 5, 2)
                            Dim getDay1 As String = Strings.Right(tableCIFTakedown.Rows(x).Item(51).ToString, 2)
                            dueDateDisburseDT = Convert.ToDateTime(getYear1 + "-" + getMonth1 + "-" + getDay1)
                            tableTemp.Rows(z).Item(2) = dueDateDisburseDT.ToString("dd-MM-yyyy")
                        Else
                            tableTemp.Rows(z).Item(2) = ""
                        End If
                    End If

                    'Date End Duedate 
                    'check based on file query upload
                    If cb_takedown.SelectedIndex = 1 Then
                        'add new rows for 9850 query
                        If Not String.IsNullOrWhiteSpace(tableCIFTakedown.Rows(x).Item(19).ToString) Then
                            Dim getYear2 As String = Strings.Left(tableCIFTakedown.Rows(x).Item(19).ToString, 4)
                            Dim getMonth2 As String = Strings.Mid(tableCIFTakedown.Rows(x).Item(19).ToString, 5, 2)
                            Dim getDay2 As String = Strings.Right(tableCIFTakedown.Rows(x).Item(19).ToString, 2)
                            endDueDate = Convert.ToDateTime(getYear2 + "-" + getMonth2 + "-" + getDay2)
                            tableTemp.Rows(z).Item(3) = endDueDate.ToString("dd-MM-yyyy")
                        Else
                            tableTemp.Rows(z).Item(3) = ""
                        End If
                    ElseIf cb_takedown.SelectedIndex = 2 Then
                        'add new rows for DT query

                        If Not String.IsNullOrWhiteSpace(tableCIFTakedown.Rows(x).Item(52).ToString) Then
                            Dim getYear2 As String = Strings.Left(tableCIFTakedown.Rows(x).Item(52).ToString, 4)
                            Dim getMonth2 As String = Strings.Mid(tableCIFTakedown.Rows(x).Item(52).ToString, 5, 2)
                            Dim getDay2 As String = Strings.Right(tableCIFTakedown.Rows(x).Item(52).ToString, 2)
                            endDueDate = Convert.ToDateTime(getYear2 + "-" + getMonth2 + "-" + getDay2)
                            tableTemp.Rows(z).Item(3) = endDueDate.ToString("dd-MM-yyyy")
                        Else
                            tableTemp.Rows(z).Item(3) = ""
                        End If
                    End If

                    'Last Payment Date
                    'check based on file query upload
                    If cb_takedown.SelectedIndex = 1 Then
                        'add new rows for 9850 query
                        If Not String.IsNullOrWhiteSpace(tableCIFTakedown.Rows(x).Item(29).ToString) Then
                            Dim getYear3 As String = Strings.Left(tableCIFTakedown.Rows(x).Item(29).ToString, 4)
                            Dim getMonth3 As String = Strings.Mid(tableCIFTakedown.Rows(x).Item(29).ToString, 5, 2)
                            Dim getDay3 As String = Strings.Right(tableCIFTakedown.Rows(x).Item(29).ToString, 2)
                            lastPaymentDate = Convert.ToDateTime(getYear3 + "-" + getMonth3 + "-" + getDay3)
                            tableTemp.Rows(z).Item(4) = lastPaymentDate.ToString("dd-MM-yyyy")
                        Else
                            tableTemp.Rows(z).Item(4) = ""
                        End If
                    ElseIf cb_takedown.SelectedIndex = 2 Then
                        'add new rows for DT query
                        If Not String.IsNullOrWhiteSpace(tableCIFTakedown.Rows(x).Item(25).ToString) Then
                            Dim getYear3 As String = Strings.Left(tableCIFTakedown.Rows(x).Item(25).ToString, 4)
                            Dim getMonth3 As String = Strings.Mid(tableCIFTakedown.Rows(x).Item(25).ToString, 5, 2)
                            Dim getDay3 As String = Strings.Right(tableCIFTakedown.Rows(x).Item(25).ToString, 2)
                            lastPaymentDate = Convert.ToDateTime(getYear3 + "-" + getMonth3 + "-" + getDay3)
                            tableTemp.Rows(z).Item(4) = lastPaymentDate.ToString("dd-MM-yyyy")
                        Else
                            tableTemp.Rows(z).Item(4) = ""
                        End If
                    End If

                    'get data ostanding amount
                    'check based on file query upload
                    If cb_takedown.SelectedIndex = 1 Then
                        'add new rows for 9850 query
                        If Not String.IsNullOrWhiteSpace(tableCIFTakedown.Rows(x).Item(17).ToString) Then
                            ostanding_amount = Convert.ToDouble(tableCIFTakedown.Rows(x).Item(17).ToString)
                            tableTemp.Rows(z).Item(5) = ostanding_amount.ToString
                        Else
                            ostanding_amount = 0
                            tableTemp.Rows(z).Item(5) = ostanding_amount.ToString
                        End If
                    ElseIf cb_takedown.SelectedIndex = 2 Then
                        'add new rows for DT query
                        If Not String.IsNullOrWhiteSpace(tableCIFTakedown.Rows(x).Item(11).ToString) Then
                            ostanding_amount = Convert.ToDouble(tableCIFTakedown.Rows(x).Item(11).ToString)
                            tableTemp.Rows(z).Item(5) = ostanding_amount.ToString
                        Else
                            ostanding_amount = 0
                            tableTemp.Rows(z).Item(5) = ostanding_amount.ToString
                        End If
                    End If

                    'get data max tenor from date disburse until end duedate 
                    max_tenor = DateDiff(DateInterval.Month, dueDateDisburseDT, endDueDate)
                    'calculation instalment for payment
                    'get interest data from database type product

                    'check based on file query upload
                    If cb_takedown.SelectedIndex = 1 Then
                        'add new rows for 9850 query
                        interest_product = Convert.ToInt32(tableCIFTakedown.Rows(x).Item(20).ToString)
                        'get data loan application for payment
                        loan_amount = Convert.ToDouble(tableCIFTakedown.Rows(x).Item(28).ToString)
                    ElseIf cb_takedown.SelectedIndex = 2 Then
                        'add new rows for DT query
                        interest_product = Convert.ToInt32(tableCIFTakedown.Rows(x).Item(20).ToString)
                        'get data loan application for payment
                        loan_amount = Convert.ToDouble(tableCIFTakedown.Rows(x).Item(12).ToString)
                    End If

                    'get data loan amount for disburse application per accounts
                    'tableTemp.Rows(z).Item(6) = loan_amount.ToString
                    'tableTemp.Rows(z).Item(7) = max_tenor.ToString
                    dt_calculationPMT = CountInstalmentPMT(interest_product, loan_amount, max_tenor, dueDateDisburseDT)

                    'for count n tenor from calculation and principal ostanding of DT
                    For y As Integer = 0 To dt_calculationPMT.Rows.Count - 1
                        'calculation data from principal
                        dt_principalPMT = dt_calculationPMT.Rows(y).Item(4)
                        If y = 0 Then
                            get_selisih_principal = (loan_amount - ostanding_amount)
                            If get_selisih_principal >= -10 AndAlso get_selisih_principal <= 10 Then
                                n_tenor = y + 1
                            End If
                            loan_amount -= dt_principalPMT
                        Else
                            get_selisih_principal = (loan_amount - ostanding_amount)
                            If get_selisih_principal >= -10 AndAlso get_selisih_principal <= 10 Then
                                n_tenor = y + 1
                            End If
                            loan_amount -= dt_principalPMT
                        End If
                    Next
                    'total interest calculation from PMT
                    'check based on file query upload
                    If cb_takedown.SelectedIndex = 1 Then
                        'add new rows for 9850 query
                        dt_interestPMT = Convert.ToDouble(tableCIFTakedown.Rows(x).Item(35).ToString)
                    ElseIf cb_takedown.SelectedIndex = 2 Then
                        'add new rows for DT query
                        For a As Integer = n_tenor - 1 To dt_calculationPMT.Rows.Count - 1
                            'calculation data from principal
                            dt_interestPMT += Convert.ToDouble(dt_calculationPMT.Rows(a).Item(5))
                        Next
                    End If
                    'overdue day from DT and 9850 query
                    If cb_takedown.SelectedIndex = 1 Then
                        'add new rows for 9850 query
                        overdue_day = Convert.ToInt32(tableCIFTakedown.Rows(x).Item(33).ToString)
                    ElseIf cb_takedown.SelectedIndex = 2 Then
                        'add new rows for DT query
                        'add new rows for 9850 query
                        overdue_day = Convert.ToInt32(tableCIFTakedown.Rows(x).Item(58).ToString)
                    End If

                    'for COA Balance from File COA Balance update
                    If Not tableLoanFile Is Nothing Then
                        'filter data balance
                        Dim filterCOABalance As String = String.Format("[" + tableLoanFile.Columns(0).ColumnName + "] ='" + SHBI_Account.ToString + "'")
                        rowDataBalance = tableLoanFile.Select(filterCOABalance).FirstOrDefault()
                        If Not rowDataBalance Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(rowDataBalance.Item(1).ToString) Then
                                total_balance = Convert.ToDouble(rowDataBalance.Item(1).ToString)
                                str_total_balance = total_balance.ToString
                            Else
                                str_total_balance = ""
                            End If
                        Else
                            str_total_balance = ""
                        End If
                    Else
                        str_total_balance = ""
                    End If
                    tableTemp.Rows(z).Item(6) = dt_interestPMT.ToString
                    tableTemp.Rows(z).Item(7) = overdue_day.ToString
                    'get selisih date disbure and next payment in month
                    tableTemp.Rows(z).Item(8) = n_tenor.ToString
                    'coa balance 
                    tableTemp.Rows(z).Item(9) = str_total_balance.ToString
                    z += 1
                    table_report.ImportRow(tableTemp.Rows(x))
                    load_result_report.DataSource = table_report
                    'event load data
                    Application.DoEvents()
                    T_Rows1.Text = load_result_report.Rows.Count - 1
                Next
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

            MessageBox.Show("Processing is Done.", "Alert")
            'disabled sort
            For Each a As DataGridViewColumn In load_result_report.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            For Each b As DataGridViewColumn In load_result_report2.Columns
                b.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            'get total rows
            T_Rows1.Text = load_result_report.Rows.Count - 1
            T_Rows2.Text = load_result_report2.Rows.Count - 1
        End Try

    End Sub

    Private Function checkDisbursement(ByRef get_shbi_acct_no As String) As DataTable
        Dim tableDisburement As DataTable = Nothing
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(get_shbi_acct_no) Then
            tableDisburement = Nothing
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            cmd = New SqlCommand("SELECT TOP 1 [Date] ,[Transaction] ,[Multifinance CIF],[Multifinance Account],[SHBI CIF],[SHBI Account],[Amount],[Trx Type] FROM [DB_MASTER].[dbo].[DB_DISBURSEMENT_KRD] WHERE [SHBI Account] = '" + get_shbi_acct_no.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "disburesement
            If checktable.Rows.Count() > 0 Then
                'get data Disburement
                tableDisburement = checktable.Copy()
            Else
                tableDisburement = Nothing
            End If
        End If
        Return tableDisburement
    End Function

    Private Function CountPMT(ByRef PVal As Double, ByRef APR As Double, ByRef TotPmts As Integer) As Double
        Dim Payment As Double
        Dim PayType As DueDate
        'Dim Response As MsgBoxResult
        ' Define money format.
        ' Usually 0 for a loan.
        Dim FVal As Double = 0
        'PVal ="How much do you want to borrow?"
        'APR = "What is the annual percentage rate of your loan?"
        If APR > 1 Then APR = APR / 100 ' Ensure proper form.
        'TotPmts = "How many monthly payments will you make?"
        'Response ="Do you make payments at the end of month?"

        PayType = DueDate.EndOfPeriod
        Payment = Pmt(APR / 12, TotPmts, -PVal, FVal, PayType)
        'Pembulatan
        Payment = Math.Round(Payment)
        Return Payment
    End Function
    Private Function CountInstalmentPMT(ByRef interest_mounth As Integer, ByRef loan_amount As Double, ByRef max_tenor As Integer, ByRef dateDisburse As Date) As DataTable
        Dim rowPayment As DataRow
        Dim dt_instalmentPMT As New DataTable
        Dim loan As Double
        Dim totalPrincipal, totalInterest, sumInterest As Double
        Dim interest_percent As Double
        Dim tenor As Double
        Dim instalment_amount As Double
        Dim interest_n, interest_not_round As Double
        Dim OS_Balance As Double
        Dim Principal As Double
        Dim setdate As DateTime
        Dim getdateDisburse, yesterdayDate, startDate, endDate As DateTime
        'Dim strDateDisbure As String
        Dim arrDueDate(60) As String
        loan = 0
        interest_percent = 0
        tenor = 0
        instalment_amount = 0
        interest_n = 0
        interest_not_round = 0
        OS_Balance = 0
        Principal = 0
        dt_instalmentPMT.Columns.Clear()
        dt_instalmentPMT.Rows.Clear()
        'start process
        interest_percent = Convert.ToDouble(interest_mounth)
        getdateDisburse = dateDisburse
        loan = Convert.ToDouble(loan_amount)
        tenor = Convert.ToDouble(max_tenor)
        instalment_amount = CountPMT(loan, interest_percent, tenor)
        '------Start set count due date any month------
        'strdate is Date Disburesement
        '------End set count due date any month------
        'show interest, payment in mothly until payment end tenor
        'add new column
        dt_instalmentPMT.Columns.Add("Month")
        dt_instalmentPMT.Columns.Add("Due Date")
        dt_instalmentPMT.Columns.Add("Start Payment")
        dt_instalmentPMT.Columns.Add("End Payment")
        dt_instalmentPMT.Columns.Add("Principal")
        dt_instalmentPMT.Columns.Add("Interest")
        dt_instalmentPMT.Columns.Add("Payment")
        'add datarow
        For x As Integer = 0 To tenor
            Dim getNoDays As Integer
            'add new row
            rowPayment = dt_instalmentPMT.NewRow()
            dt_instalmentPMT.Rows.Add(rowPayment)

            'row data column Month
            If x = tenor Then
                dt_instalmentPMT.Rows(x).Item(0) = Convert.ToString(x)
            Else
                dt_instalmentPMT.Rows(x).Item(0) = x + 1
            End If

            'due date payment any month
            'condition calculations Day (WorksDay)
            'Add 1 Month from Date Disbursement
            If x = tenor Then
                'last rows
                setdate = DateAdd(DateInterval.Month, x, getdateDisburse)
            Else
                setdate = DateAdd(DateInterval.Month, x + 1, getdateDisburse)
            End If

            '--Due Date Month 1 --
            'check max date if not day in month
            'get duedate last month
            If IsError(setdate) Then
                setdate = DateSerial(Year(setdate), Month(setdate) + 1, 0)
                'add to array
                arrDueDate(x) = setdate

                dt_instalmentPMT.Rows(x).Item(1) = setdate.ToString("dd-MM-yyyy")
            Else
                'add to array
                arrDueDate(x) = setdate
                dt_instalmentPMT.Rows(x).Item(1) = setdate.ToString("dd-MM-yyyy")
            End If

            'start payment any month
            'Start payment is same the duedate on month
            'inisialisasi value variabel startdate from last dutdate month
            If Not x = 0 Then
                'get duedate last month
                startDate = arrDueDate(x - 1)
                'Console.WriteLine(startDate)
checkLoopStartPayment1:
                getNoDays = checkDayofWeeks(startDate)

                If getNoDays = 1 Or getNoDays = 7 Then
                    'add next day
                    startDate = DateAdd(DateInterval.Day, 1, startDate)
                    GoTo checkLoopStartPayment1
                Else
                    If x = tenor Then
                        startDate = arrDueDate(x)
                        startDate = DateAdd(DateInterval.Month, -1, startDate)
                        dt_instalmentPMT.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                        'end check wordays
                    Else
                        'check workdays
                        dt_instalmentPMT.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                        'end check wordays
                    End If
                End If
            ElseIf x = 0 Then
                startDate = getdateDisburse
checkLoopStartPayment2:
                getNoDays = checkDayofWeeks(startDate)

                'Console.WriteLine(getNoDays)
                If getNoDays = 1 Or getNoDays = 7 Then
                    'add next day
                    startDate = DateAdd(DateInterval.Day, 1, startDate)
                    GoTo checkLoopStartPayment2
                Else
                    dt_instalmentPMT.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                End If

            End If
checkLoopEndPayment:
            getNoDays = checkDayofWeeks(setdate)
            If getNoDays = 1 And getNoDays = 7 Then
                'add next day
                endDate = DateAdd(DateInterval.Day, -1, setdate)
                GoTo checkLoopEndPayment
            Else
                endDate = DateAdd(DateInterval.Day, -1, setdate)
                dt_instalmentPMT.Rows(x).Item(3) = endDate.ToString("dd-MM-yyyy")
            End If

            'process formula 
            'interest 
            If x = 0 Then
                Dim loan_percent As Double = interest_percent * loan
                interest_n = (loan_percent / 12)
                interest_not_round = interest_n
                interest_n = Math.Round(interest_n, 0)

            ElseIf x = tenor Then
                interest_n = interest_n
                interest_not_round = interest_n
            Else
                Dim loan_percent As Double = interest_percent * OS_Balance
                interest_n = (loan_percent / 12)
                interest_not_round = interest_n
                interest_n = Math.Round(interest_n, 0)
            End If

            'principal             
            If x = tenor - 1 Then
                Principal = instalment_amount - interest_n
            ElseIf x = tenor Then
                Principal = Principal + OS_Balance
            Else
                Principal = instalment_amount - interest_n
            End If
            'Payment             
            If x = tenor Then
                instalment_amount = Principal + interest_n
            End If
            'OS Balance
            If x = 0 Then
                OS_Balance = loan - Principal
            ElseIf x = tenor Then
                OS_Balance = Convert.ToString(0)
            Else
                OS_Balance = OS_Balance - Principal
            End If
            'column Principal
            dt_instalmentPMT.Rows(x).Item(4) = Convert.ToString(Principal)
            'column Interest
            dt_instalmentPMT.Rows(x).Item(5) = Convert.ToString(interest_n)
            'column Instalment /Payment
            dt_instalmentPMT.Rows(x).Item(6) = Convert.ToString(instalment_amount)
        Next

        'remove row in datatable
        dt_instalmentPMT.Rows.RemoveAt(Int(max_tenor - 1))
        Return dt_instalmentPMT

    End Function
    Private Function checkDayofWeeks(ByRef getdate As Date) As Integer
        Dim noDay As Integer
        'Count Condition Day tobe Workday or Weekend
        Select Case Weekday(getdate)
            Case 1
                noDay = 1
            Case 7
                noDay = 7
            Case Else
                noDay = Weekday(getdate)
        End Select
        Return noDay
    End Function
    Private Function getDataTotalBalanceCOA(ByRef SHBI_Account As String) As DataTable
        Dim table_total_balanced As New DataTable()
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(SHBI_Account) Then
            table_total_balanced = Nothing
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            cmd = New SqlCommand("SELECT [shbi acct no], SUM([Balanced]) AS Total_Balance FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] WHERE [shbi acct no] LIKE  '%" + SHBI_Account.ToString + "%' GROUP BY [shbi acct no] ", SQLConnection)
            'End If
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            table_total_balanced = checktable.Copy()
        End If
        Return table_total_balanced
    End Function
    Private Function getNoAccountsType(ByRef type As String, ByRef shbi_acct As String) As String
        Dim No_Acc As String = ""
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(shbi_acct) Then
            No_Acc = ""
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            cmd = New SqlCommand("/****** Script for SelectTopNRows command from SSMS  ******/
                SELECT A.[Date]
                      ,A.[SHBI Account]
                      ,A.[Trx Type]
	                  ,B.[NO_ACCOUNT]
                      FROM [DB_MASTER].[dbo].[DB_DISBURSEMENT_KRD] AS A
                      FULL JOIN [DB_MASTER].[dbo].[DB_NO_ACC_PRODUCTS] AS B
                      ON A.[Trx Type] = B.[CODE]
                      WHERE A.[SHBI Account] ='" + shbi_acct.ToString + "' AND B.[TYPE] ='" + type.ToString + "'", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            ' Console.WriteLine(Trim(Strings.Right(name_product.ToString, 4)))
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data Disburement
                No_Acc = checktable.Rows(0).Item(3).ToString()
            Else
                No_Acc = ""
            End If
        End If
        Return No_Acc
    End Function
    '"SELECT DISTINCT(B.[Multifinance Customer key no]) AS 'Multifinance CIF', A.[BRW_APRV_NO] AS 'SHBI Account', B.[Multifinance ACCOUNT key no] AS 'Multifinance Account',B.[NO CIF] AS 'SHBI CIF', B.[Loan Application] AS 'Amount', A.[BRW_APLCT_DT] AS 'Date',B.[Representive loan application no] AS 'Trx Type',A.[REASON_CODE] AS 'Reason', A.[APPA_PRGR_C] FROM [DB_MASTER].[dbo].[DB_REPORT_DISTBUR] AS A INNER JOIN [DB_MASTER].[dbo].[DB_REPORT_LOAN_TEMP] AS B ON B.[NO CIF] = A.[CUSNO] UNION SELECT DISTINCT(B.[Multifinance Customer key no]) AS 'Multifinance CIF', A.[BRW_APRV_NO] AS 'SHBI Account', B.[Multifinance ACCOUNT key no] AS 'Multifinance Account', B.[NO CIF] AS 'SHBI CIF', B.[Loan Application] AS 'Amount',C.[Approval Application Date] AS 'Date',B.[Representive loan application no] AS 'Trx Type', C.[REASON] AS 'Reason', 'REJECT' AS '[APPA_PRGR_C]' FROM [DB_MASTER].[dbo].[DB_REPORT_DISTBUR] AS A INNER JOIN [DB_MASTER].[dbo].[DB_REPORT_LOAN_TEMP] AS B  ON B.[NO CIF] = A.[CUSNO] INNER JOIN [DB_MASTER].[dbo].[DB_TAKEDOWN_TEMP] AS C ON C.[Multifinance Customer key no] = B.[Multifinance Customer key no]", SQLConnection)

    Public Sub DataTableToExportText(table As DataTable, filePath As String)
        If Not table Is Nothing Then
            Using writer As New IO.StreamWriter(filePath)
                Dim lastColIndex As Integer = table.Columns.Count
                Dim cellvalue, cellvalue1 As String
                Dim firstField, firstRow As Boolean
                firstRow = True
                'lopping column header
                If Not firstRow Then writer.WriteLine()
                firstField = True
                For i = 0 To lastColIndex - 1
                    If Not firstField Then writer.Write("|")
                    cellvalue1 = table.Columns.Item(i).ToString()
                    If cellvalue1.Contains("|") Then
                        writer.Write(ControlChars.Quote)
                        writer.Write(cellvalue1)
                        writer.Write(ControlChars.Quote)
                    Else
                        writer.Write(cellvalue1)
                    End If
                    'For space "," In any word 
                    firstField = False
                Next
                'give pipe last header
                writer.Write("|")
                'For next rows in data
                firstRow = False
                'lopping column rows
                For Each row As DataRow In table.Rows
                    If Not firstRow Then writer.WriteLine()
                    firstField = True
                    For i = 0 To lastColIndex - 1
                        If Not firstField Then writer.Write("|")
                        If Not row.IsNull(i) Then
                            cellvalue = row.Item(i).ToString
                            If cellvalue.Contains("|") Then
                                writer.Write(ControlChars.Quote)
                                writer.Write("'" + cellvalue)
                                writer.Write(ControlChars.Quote)
                            Else
                                writer.Write(cellvalue)
                            End If
                        End If
                        firstField = False
                    Next

                    'give pipe last header
                    writer.Write("|")
                    firstRow = False
                Next
            End Using
        End If

    End Sub

    'Private Sub HeaderLoanTakedownColumn()
    '    'column loan 
    '    tableLoanTakedown.Columns.Add("BSI Code")
    '    tableLoanTakedown.Columns.Add("Approval Application Date")
    '    tableLoanTakedown.Columns.Add("Transaction Sequence No")
    '    tableLoanTakedown.Columns.Add("No Cif")
    '    tableLoanTakedown.Columns.Add("Scoring Model Type")
    '    tableLoanTakedown.Columns.Add("Application Type")
    '    tableLoanTakedown.Columns.Add("ID type")
    '    tableLoanTakedown.Columns.Add("ID NO")
    '    tableLoanTakedown.Columns.Add("Name")
    '    tableLoanTakedown.Columns.Add("Nationality")
    '    tableLoanTakedown.Columns.Add("Birth of Date")
    '    tableLoanTakedown.Columns.Add("Address")
    '    tableLoanTakedown.Columns.Add("Work Period")
    '    tableLoanTakedown.Columns.Add("Cell Number")
    '    tableLoanTakedown.Columns.Add("Marital Status Code")
    '    tableLoanTakedown.Columns.Add("Housing Type")
    '    tableLoanTakedown.Columns.Add("House Ownership")
    '    tableLoanTakedown.Columns.Add("Occupation Code 1 (large)")
    '    tableLoanTakedown.Columns.Add("Occupation Code 2 (middle)")
    '    tableLoanTakedown.Columns.Add("Occupation Code 3 (small)")
    '    tableLoanTakedown.Columns.Add("Position")
    '    tableLoanTakedown.Columns.Add("Division")
    '    tableLoanTakedown.Columns.Add("Company Establishment Date")
    '    tableLoanTakedown.Columns.Add("Employee Contract Type")
    '    tableLoanTakedown.Columns.Add("Product Type")
    '    tableLoanTakedown.Columns.Add("Currency")
    '    tableLoanTakedown.Columns.Add("Loan Application")
    '    tableLoanTakedown.Columns.Add("USD Converted Amount")
    '    tableLoanTakedown.Columns.Add("Loan Periode Code")
    '    tableLoanTakedown.Columns.Add("Period")
    '    tableLoanTakedown.Columns.Add("Expected Booking Date")
    '    tableLoanTakedown.Columns.Add("Grace Period")
    '    tableLoanTakedown.Columns.Add("Maturity Date")
    '    tableLoanTakedown.Columns.Add("Payment Method")
    '    tableLoanTakedown.Columns.Add("Multifinance companys BSI Customer Number")
    '    tableLoanTakedown.Columns.Add("Loan Plafond Type")
    '    tableLoanTakedown.Columns.Add("Loan Revolving Yn")
    '    tableLoanTakedown.Columns.Add("Interest Type")
    '    tableLoanTakedown.Columns.Add("Payment Method2")
    '    tableLoanTakedown.Columns.Add("Interest Type2")
    '    tableLoanTakedown.Columns.Add("Interest Period Type")
    '    tableLoanTakedown.Columns.Add("Interest Rate")
    '    tableLoanTakedown.Columns.Add("Loan Condition Changes")
    '    tableLoanTakedown.Columns.Add("Fixed Term Loan Maturity Code")
    '    tableLoanTakedown.Columns.Add("Fixed Loan Maturity Number")
    '    tableLoanTakedown.Columns.Add("First Fixed Interest Rate")
    '    tableLoanTakedown.Columns.Add("Restructuring YN")
    '    tableLoanTakedown.Columns.Add("Take Over YN")
    '    tableLoanTakedown.Columns.Add("Reference Source")
    '    tableLoanTakedown.Columns.Add("Economy Sector LBU")
    '    tableLoanTakedown.Columns.Add("LBU Use Reference")
    '    tableLoanTakedown.Columns.Add("LBU Loan Type")
    '    tableLoanTakedown.Columns.Add("LBU Debtor Group")
    '    tableLoanTakedown.Columns.Add("Loan Type Loan Application (LBU)")
    '    tableLoanTakedown.Columns.Add("LBU Use Type")
    '    tableLoanTakedown.Columns.Add("LBU Rating Agency")
    '    tableLoanTakedown.Columns.Add("Debtor Rating")
    '    tableLoanTakedown.Columns.Add("LBU Measurement Category")
    '    tableLoanTakedown.Columns.Add("LBU Portfolio Category")
    '    tableLoanTakedown.Columns.Add("LBU Debtor Category")
    '    tableLoanTakedown.Columns.Add("SID Economy Sector")
    '    tableLoanTakedown.Columns.Add("SID Use Type")
    '    tableLoanTakedown.Columns.Add("SID Guarantor Type")
    '    tableLoanTakedown.Columns.Add("SID Use Orientation")
    '    tableLoanTakedown.Columns.Add("SID Loan Characteristic")
    '    tableLoanTakedown.Columns.Add("SID Relationship with Bank")
    '    tableLoanTakedown.Columns.Add("SID Loan Group")
    '    tableLoanTakedown.Columns.Add("SID Debtor Group")
    '    tableLoanTakedown.Columns.Add("SID Debtor Status")
    '    tableLoanTakedown.Columns.Add("Company Project Location")
    '    tableLoanTakedown.Columns.Add("SID Debtor Group2")
    '    tableLoanTakedown.Columns.Add("LBU Relationship with Bank")
    '    tableLoanTakedown.Columns.Add("Total Income")
    '    tableLoanTakedown.Columns.Add("Employee Income")
    '    tableLoanTakedown.Columns.Add("Other Employee Income")
    '    tableLoanTakedown.Columns.Add("Total Expense")
    '    tableLoanTakedown.Columns.Add("Loan Installment Credit Card")
    '    tableLoanTakedown.Columns.Add("Loan Installment")
    '    tableLoanTakedown.Columns.Add("Credit Card Payment")
    '    tableLoanTakedown.Columns.Add("Daily Expense")
    '    tableLoanTakedown.Columns.Add("Other Expenses")
    '    tableLoanTakedown.Columns.Add("Net Income")
    '    tableLoanTakedown.Columns.Add("Loan Authority")
    '    tableLoanTakedown.Columns.Add("Credit Officer ID")
    '    tableLoanTakedown.Columns.Add("Representive Loan Application No")
    '    tableLoanTakedown.Columns.Add("Loan Application No")
    '    tableLoanTakedown.Columns.Add("Branch Code")
    '    tableLoanTakedown.Columns.Add("KPO Branch Head ID")
    '    tableLoanTakedown.Columns.Add("Master Status")
    '    tableLoanTakedown.Columns.Add("Teller ID")
    '    tableLoanTakedown.Columns.Add("Registered Date")
    '    tableLoanTakedown.Columns.Add("Registered Time")
    '    tableLoanTakedown.Columns.Add("Information Changer")
    '    tableLoanTakedown.Columns.Add("Information Changes Date")
    '    tableLoanTakedown.Columns.Add("Information Changes Time")
    '    tableLoanTakedown.Columns.Add("information Register Date")
    '    tableLoanTakedown.Columns.Add("Lastest Information Change Date")
    '    tableLoanTakedown.Columns.Add("Information Approval")
    '    tableLoanTakedown.Columns.Add("Multifinance Customer Key No")
    '    tableLoanTakedown.Columns.Add("Multifinance ACCOUNT Key No")
    '    tableLoanTakedown.Columns.Add("No Of Deed")
    '    tableLoanTakedown.Columns.Add("Multifinance Loan Starting Date")
    '    tableLoanTakedown.Columns.Add("Status")
    'End Sub

    Private Sub btn_import_loan_final_Click(sender As Object, e As EventArgs) Handles btn_import_loan_final.Click
        Try
            Dim ds As New DataSet()
            Dim strFile As String

            C_rows1.Text = 0
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xls)|*.xls|Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'before clear datatable
                tableLoanFinal.Columns.Clear()
                tableLoanFinal.Rows.Clear()
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                strFile = fi.FullName
                Dim extStr As String = Path.GetExtension(OpenFileDialog1.FileName)
                'fomat extenstions
                If extStr = ".xls" Or extStr = ".xlsx" Then
                    '(2) reader excel to Datatable
                    conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFile + ";Extended Properties=Excel 12.0;")
                    conn.Open()
                    Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                    Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                    Dim da As New OleDbDataAdapter(sqlquery, conn)
                    da.Fill(ds)
                    tableLoanFinal = ds.Tables(0)
                    'close conenction 
                    conn.Close()
                ElseIf extStr = ".csv" Then
                    '(2) reader txt to Datatable
                    Dim SR As StreamReader = New StreamReader(strFile)
                    Dim line As String = SR.ReadLine()
                    Dim strArray As String() = line.Split(","c)
                    Dim row As DataRow
                    'column
                    'call header Loan Column
                    For Each s As String In strArray
                        tableLoanFinal.Columns.Add(New DataColumn())
                    Next
                    Do
                        line = SR.ReadLine
                        If Not line = String.Empty Then
                            row = tableLoanFinal.NewRow()
                            row.ItemArray = line.Split(","c)
                            tableLoanFinal.Rows.Add(row)
                        Else
                            Exit Do
                        End If
                    Loop

                End If

                'for report partial payment
                If cb_report.SelectedIndex.ToString = 5 Then
                    If tableLoanFinal.Columns.Count > 0 Then
                        tableLoanFinal.Columns.Add("Amount")
                    End If
                    For i As Integer = 0 To tableLoanFinal.Rows.Count - 1
                        If Not String.IsNullOrWhiteSpace(tableLoanFinal.Rows(i).Item(7)) And Not String.IsNullOrWhiteSpace(tableLoanFinal.Rows(i).Item(8)) Then
                            tableLoanFinal.Rows(i).Item(11) = Convert.ToDouble(tableLoanFinal.Rows(i).Item(7)) + Convert.ToDouble(tableLoanFinal.Rows(i).Item(8))
                        End If

                    Next i
                ElseIf Not cb_report.SelectedIndex.ToString = 5 Or Not cb_report.SelectedIndex.ToString = 6 Then
                    'for others
                    'cleansing amount 
                    For i As Integer = 0 To tableLoanFinal.Rows.Count - 1
                        tableLoanFinal.Rows(i).Item(26) = Trim(Replace(tableLoanFinal.Rows(i).Item(26).ToString, ".00", ""))
                    Next i
                End If
                tableLoanFinal.AcceptChanges()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            If Not tableLoanFinal Is Nothing Then
                C_data2.Text = "(OK)"
            Else
                C_data2.Text = "(False)"
            End If
        End Try
    End Sub


    Private Sub btn_import_loan_Click(sender As Object, e As EventArgs) Handles btn_import_loan.Click
        Try

            Dim ds As New DataSet()
            Dim strFile As String
            strFile = ""
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx*"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'before clear datatable
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                strFile = fi.FullName
                Dim extStr As String = Path.GetExtension(OpenFileDialog1.FileName)
                'fomat extenstions
                If extStr = ".xls" Or extStr = ".xlsx" Then
                    '(2) reader excel to Datatable
                    conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFile + ";Extended Properties=Excel 12.0;")
                    conn.Open()
                    Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                    Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                    Dim da As New OleDbDataAdapter(sqlquery, conn)
                    da.Fill(ds)
                    tableLoanFile = ds.Tables(0)
                    'close conenction 
                    conn.Close()
                ElseIf extStr = ".csv" Then
                    '(2) reader txt to Datatable
                    Dim SR As StreamReader = New StreamReader(strFile)
                    Dim line As String = SR.ReadLine()
                    Dim strArray As String() = line.Split(","c)
                    Dim row As DataRow
                    'column
                    'call header Loan Column
                    For Each s As String In strArray
                        tableLoanFile.Columns.Add(New DataColumn())
                    Next
                    Do
                        line = SR.ReadLine
                        If Not line = String.Empty Then
                            row = tableLoanFile.NewRow()
                            row.ItemArray = line.Split(","c)
                            tableLoanFile.Rows.Add(row)
                        Else
                            Exit Do
                        End If
                    Loop

                End If
                If cb_report.SelectedIndex.ToString = 0 Then
                    'cleansing amount 
                    For i As Integer = 0 To tableLoanFile.Rows.Count - 1
                        tableLoanFile.Rows(i).Item(26) = Trim(Replace(tableLoanFile.Rows(i).Item(26).ToString, ".00", ""))
                    Next i
                End If
                tableLoanFile.AcceptChanges()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            If Not tableLoanFile Is Nothing Then
                C_data4.Text = "(OK)"
            Else
                C_data4.Text = "(False)"
            End If
        End Try
    End Sub
End Class