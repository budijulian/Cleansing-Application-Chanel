
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

Public Class Payment
    Private notProccess As Boolean
    Private conn As OleDbConnection 'koneksi odb
    Private dts As DataSet ' membuat table datasheet virtual
    Private strFile As String
    ''progress variabel virtual datatable
    Private table_payment, table_temp, table_process, table_not_process, table_pmt_loan As New DataTable
    Private rowTemp, rowProcess, rowNotProcess As DataRow
    'Private SQLConnection As New Data.SqlClient.SqlConnection

    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    '...............SQL SERVER..............
    'Connection SQL Server
    Public SQL As New ConnectionSQL
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    'Private arrString As New ArrayList()
    'Declare variable for PMT Payment
    'Private table_payment As New DataTable
    Private rowPayment As DataRow
    Private st_hours, st_minutes, st_seconds As Double
    Private Sub Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            cb_export.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btn_pmt_Click(sender As Object, e As EventArgs) Handles btn_pmt.Click
        Dim pmt As New PaymentCount()
        pmt.ShowDialog()
    End Sub
    Private Sub HeaderRegulerColumn()


        'column loan
        'Job date	execution date	repayment date	KREDIVO	KREDIVO cif	KREDIVO acct no	shbi cif no	customer name	shbi acct no	execution date	expiry date	repayment schedule count	repayment count	remain repayment count	trx sern	repayment type	repayment schedule date	repayment schedule business date	interest start date	interest end date	interest day count	overdue Y/N	overdue start date	overdue end date	overdue interest calculate start date	overdue interest calculate end date	overdue day count	after repayment balance	repayment principal	repayment interest	overdue interest	repayment sum amount	auto transfer acno	Flag	Date Tunggakan	Tenor	Sisa Tenor

        table_payment.Columns.Add("Job date")
        table_payment.Columns.Add("execution date")
        table_payment.Columns.Add("repayment date")
        table_payment.Columns.Add("KREDIVO")
        table_payment.Columns.Add("KREDIVO cif")
        table_payment.Columns.Add("KREDIVO acct no")
        table_payment.Columns.Add("shbi cif no")
        table_payment.Columns.Add("customer name")
        table_payment.Columns.Add("shbi acct no")
        table_payment.Columns.Add("execution date2")
        table_payment.Columns.Add("expiry date")
        table_payment.Columns.Add("repayment schedule count")
        table_payment.Columns.Add("repayment count")
        table_payment.Columns.Add("remain repayment count")
        table_payment.Columns.Add("trx sern")
        table_payment.Columns.Add("repayment type")
        table_payment.Columns.Add("repayment schedule date")
        table_payment.Columns.Add("repayment schedule business date")
        table_payment.Columns.Add("interest start date")
        table_payment.Columns.Add("interest end date")
        table_payment.Columns.Add("interest day count")
        table_payment.Columns.Add("overdue Y/N")
        table_payment.Columns.Add("overdue start date")
        table_payment.Columns.Add("overdue end date")
        table_payment.Columns.Add("overdue interest calculate start date")
        table_payment.Columns.Add("overdue interest calculate end date")
        table_payment.Columns.Add("overdue day count")
        table_payment.Columns.Add("after repayment balance")
        table_payment.Columns.Add("repayment principal")
        table_payment.Columns.Add("repayment interest")
        table_payment.Columns.Add("overdue interest")
        table_payment.Columns.Add("repayment sum amount")
        table_payment.Columns.Add("auto transfer acno")
        table_payment.Columns.Add("Flag")
        'table_payment.Columns.Add("Range Overdue Date")
        'table_payment.Columns.Add("Tenor")
        'table_payment.Columns.Add("Remain Tenor")
    End Sub
    Private Sub btn_import_Click(sender As Object, e As EventArgs) Handles btn_import.Click
        Try
            Dim ds As New DataSet()
            'before clear datatable
            load_payment_notprocess.Columns.Clear()
            load_payment_process.Columns.Clear()
            table_temp.Reset()
            table_payment.Reset()
            C_rows1.Text = 0
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'create new column in datagridview
                If table_payment.Columns.Count <= 0 Then
                    With load_payment
                        .ColumnCount = 7
                        .Columns(0).Name = "Status*"
                        .Columns(1).Name = "Selisih Principal*"
                        .Columns(2).Name = "Selisih Interest*"
                        .Columns(3).Name = "Payment/Month*"
                        .Columns(4).Name = "Balanced*"
                        .Columns(5).Name = "Total Balanced*"
                        .Columns(6).Name = "Reason*"
                    End With
                    'load_payment.Columns(0).Width = 50
                End If
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
                    table_payment = ds.Tables(0)

                ElseIf extStr = ".txt" Then
                    '(2) reader txt to Datatable
                    Dim SR As StreamReader = New StreamReader(strFile)
                    Dim line As String = SR.ReadLine()
                    Dim strArray As String() = line.Split("|"c)
                    Dim row As DataRow
                    'column
                    'call header Loan Column
                    Call HeaderRegulerColumn()
                    Do
                        line = SR.ReadLine
                        If Not line = String.Empty Then
                            row = table_payment.NewRow()
                            row.ItemArray = line.Split("|"c)
                            table_payment.Rows.Add(row)
                        Else
                            Exit Do
                        End If
                    Loop

                End If

                'load data
                load_payment.DataSource = table_payment
                'close conenction 
                conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            ' Disabled Button before process
            btn_checking.Enabled = True
            ' Show Count Rows in Datagridview
            C_rows1.Text = load_payment.RowCount - 1
            C_Cells1.Text = load_payment.ColumnCount - 1
            'disabled 
            For Each a As DataGridViewColumn In load_payment.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub

    Private Function checkDisbursement(ByRef multifinance_acc As String) As DataTable
        Dim tableDisburement As DataTable = Nothing
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then

        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            cmd = New SqlCommand("SELECT TOP 1 [Date] ,[Transaction] ,[Multifinance CIF],[Multifinance Account],[SHBI CIF],[SHBI Account],[Amount],[Trx Type] FROM [DB_MASTER].[dbo].[DB_DISBURSEMENT_KRD] WHERE [Multifinance Account] = '" + multifinance_acc.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data Disburement
                tableDisburement = checktable.Copy()
            Else
                tableDisburement = checktable.Copy()
            End If
        End If
        Return tableDisburement
    End Function
    Private Function checkCancellationPayment(ByRef multifinance_acc As String) As DataTable
        Dim tableCancellation As DataTable = Nothing
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then
            tableCancellation = Nothing
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)

            cmd = New SqlCommand("SELECT TOP 1 * FROM [DB_MASTER].[dbo].[DB_CANCELLATION_KRD] WHERE [Multifinance Account] = '" + multifinance_acc.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data Occupation
                notProccess = False
                tableCancellation = checktable.Copy()
            Else
                'poinNotProccess += 1
                'set alert if not found
                'load_cif.Rows(x).Cells(2).Value = "Not Pass"
                'set reason
                tableCancellation = checktable.Copy()
            End If

        End If

        Return tableCancellation
    End Function

    Private Function checkCountTotalBalanced(ByRef multifinance_acc As String, ByRef dateUpdate As String) As Double
        Dim total_balanced As Double
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then
            total_balanced = 0
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'check sum balanced today
            If Not dateUpdate = "" Then
                cmd = New SqlCommand("SELECT [Product acc no], [Sum Balanced] FROM [DB_MASTER].[dbo].[DB_TOTAL_BALANCED_PAYMENT] WHERE [Product acc no] = '" + multifinance_acc.ToString + "' AND [Date update] = '" + dateUpdate + "' ", SQLConnection)
            Else
                cmd = New SqlCommand("SELECT [Product acc no], [Sum Balanced] FROM [DB_MASTER].[dbo].[DB_TOTAL_BALANCED_PAYMENT] WHERE [Product acc no] = '" + multifinance_acc.ToString + "'", SQLConnection)
            End If
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data balanced calculation
                'sum data total balanced
                total_balanced = checktable.Rows(0).Item(1)
            Else
                'set alert if not found
                'set reason
                total_balanced = -1
            End If
        End If
        Return total_balanced
    End Function
    Private Function checkCountBalanced(ByRef multifinance_acc As String, ByRef dateUpdate As String) As Double
        Dim checkbalanced As Double
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then
            checkbalanced = 0
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'check sum balanced today
            If Not dateUpdate = "" Then
                cmd = New SqlCommand("SELECT [Balanced] FROM [DB_MASTER].[dbo].[DB_BALANCED_HIST] WHERE [Product acc no] = '" + multifinance_acc.ToString + "' AND [repayment date] = '" + dateUpdate + "' ", SQLConnection)
            Else
                cmd = New SqlCommand("SELECT [Balanced] FROM [DB_MASTER].[dbo].[DB_BALANCED_HIST] WHERE [Product acc no] = '" + multifinance_acc.ToString + "'", SQLConnection)
            End If
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data balanced calculation
                'sum data total balanced
                checkbalanced = checktable.Rows(0).Item(0)
            Else
                'data not found 
                checkbalanced = -1
            End If
        End If
        Return checkbalanced
    End Function
    Private Sub updateTotalBalanced(ByRef multifinance_acc As String, ByRef DataPayments As Array)
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim ad As New Data.SqlClient.SqlDataAdapter
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then

        Else
            'update data in total balanced
            SQLConnection = New SqlConnection(SQLConnectionString)
            Dim query As String = "UPDATE [dbo].[DB_TOTAL_BALANCED_PAYMENT] SET [Date update] = @Date_update, [Sum Balanced] = @Balanced, [Remain Tenor] = @Remain_tenor WHERE [Product acc no] = @Product_acc_no"
            Dim adapter As New SqlDataAdapter(cmd)
            cmd = New SqlCommand(query, SQLConnection)
            cmd.CommandType = CommandType.Text
            'add data in param
            cmd.Parameters.AddWithValue("@Date_update", DataPayments(15).ToString())
            cmd.Parameters.AddWithValue("@Balanced", DataPayments(14).ToString())
            cmd.Parameters.AddWithValue("@Remain_tenor", DataPayments(16).ToString())
            cmd.Parameters.AddWithValue("@Product_acc_no", multifinance_acc.ToString())
        End If
    End Sub
    Private Sub addBalancedHist(ByRef multifinance_acc As String, ByRef DataPayments As Array)
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim ad As New Data.SqlClient.SqlDataAdapter
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then

        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            Dim query As String = "INSERT INTO [dbo].[DB_BALANCED_HIST] ([repayment date],[execution date] ,[Product code],[Product cif],[Product acc no],[shbi cif no],[customer name],[shbi acct no],[Balanced],[auto transfer acno],[Flag],[Tenor no],[Tenor],[Ket]) VALUES (@repayment_date,@execution_date,@Product_code, @Product_cif, @Product_acc_no, @shbi_cif_no, @customer_name, @shbi_acct_no, @Balanced, @auto_transfer_acno, @Flag, @Tenor_no,@Tenor, @Ket)"
            Dim adapter As New SqlDataAdapter(cmd)
            cmd = New SqlCommand(query, SQLConnection)
            cmd.CommandType = CommandType.Text
            ad.InsertCommand = cmd
            'add data in param
            cmd.Parameters.AddWithValue("@repayment_date", DataPayments(0))
            cmd.Parameters.AddWithValue("@execution_date", DataPayments(1))
            cmd.Parameters.AddWithValue("@Product_code", DataPayments(2))
            cmd.Parameters.AddWithValue("@Product_cif", DataPayments(3))
            cmd.Parameters.AddWithValue("@Product_acc_no", DataPayments(4))
            cmd.Parameters.AddWithValue("@shbi_cif_no", DataPayments(5))
            cmd.Parameters.AddWithValue("@customer_name", DataPayments(6))
            cmd.Parameters.AddWithValue("@shbi_acct_no", DataPayments(7))
            cmd.Parameters.AddWithValue("@Balanced", DataPayments(8))
            cmd.Parameters.AddWithValue("@aucto_transfer_acno", DataPayments(9))
            cmd.Parameters.AddWithValue("@Flag", DataPayments(10))
            cmd.Parameters.AddWithValue("@Tenor_no", DataPayments(11))
            cmd.Parameters.AddWithValue("@Tenor", DataPayments(12))
            cmd.Parameters.AddWithValue("@Ket", DataPayments(13))

        End If
    End Sub
    Private Sub btn_checking_Click(sender As Object, e As EventArgs) Handles btn_checking.Click
        Try
            Dim multifinance_acc, flag As String
            Dim date_excute As String
            Dim interest_mounth As Integer
            Dim remain_tenor, max_tenor, n_tenor As String
            Dim n_process As Boolean
            Dim timeStart As Date = Convert.ToDateTime(DateTime.Now)
            Dim spanTime As TimeSpan
            If cb_payment.SelectedIndex.ToString = 0 Then
                MessageBox.Show("Please, Choose a payment.!", "Alert")
            ElseIf cb_product.SelectedIndex.ToString = 0 Then
                MessageBox.Show("Please, Choose a product.!", "Alert")
            Else
                Dim pesan As MsgBoxResult
                'process data
                pesan = MsgBox("Checking Start?", MsgBoxStyle.YesNo, "Alert")
                If pesan = MsgBoxResult.Yes Then
                    table_temp.Reset()
                    'process payment checking
                    'copy column to datatable table temp
                    If table_temp.Rows.Count <= 0 Then
                        For j = 0 To table_payment.Columns.Count - 1
                            table_temp.Columns.Add(table_payment.Columns(j).ColumnName)
                        Next
                        'add new column output process
                        table_temp.Columns.Add("Selisih Principal*")
                        table_temp.Columns.Add("Selisih Interest*")
                        table_temp.Columns.Add("Payment/Month*")
                        table_temp.Columns.Add("Balanced*")
                        table_temp.Columns.Add("Total Balanced*")
                        table_temp.Columns.Add("Reason*")
                        table_temp.Columns.Add("Status*")
                    End If
                    'copy column to datatable table process
                    If table_process.Rows.Count <= 0 Then
                        For j = 0 To table_payment.Columns.Count - 1
                            table_process.Columns.Add(table_payment.Columns(j).ColumnName)
                        Next
                    End If
                    'copy column to datatable table not process
                    If table_not_process.Rows.Count <= 0 Then
                        For j = 0 To table_temp.Columns.Count - 1
                            table_not_process.Columns.Add(table_temp.Columns(j).ColumnName)
                        Next
                    End If
                    '--- DECLRARE VARIABEL COUNTING PMT AND CHECK PAYMENT----
                    Dim checkTableDisburse, table_pmt, checkCancel As New DataTable
                    Dim reason_output As String
                    Dim n_interest, n_principal, n_sum_amount, n_loan As String
                    Dim tb_principal, tb_interest, tb_amount As Double
                    Dim s_principal, s_interest, s_amount As Double
                    Dim tb_tenor As String
                    Dim n_balanced As String
                    Dim s_balanced, total_balanced As Double
                    Dim c_balanced, t_balanced As Double
                    'Dim DataPayments = New String(16) {}
                    Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                    getdate.ToString("ddMMyyyyHHmmss")
                    Dim strdate As String = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd")
                    Dim strDateJob As String
                    'create new table row
                    For x As Integer = 0 To table_payment.Rows.Count - 1
                        n_process = True
                        reason_output = ""
                        checkCancel.Rows.Clear()
                        checkTableDisburse.Rows.Clear()
                        table_pmt.Rows.Clear()
                        'add new rows
                        rowTemp = table_temp.NewRow()
                        table_temp.Rows.Add(rowTemp)

                        'set value for checking
                        multifinance_acc = table_payment.Rows(x).Item(5).ToString()
                        remain_tenor = table_payment.Rows(x).Item(13).ToString()
                        n_tenor = table_payment.Rows(x).Item(14).ToString()
                        flag = table_payment.Rows(x).Item(33).ToString()
                        'reguler payment amount 
                        n_principal = table_payment.Rows(x).Item(28).ToString()
                        n_interest = table_payment.Rows(x).Item(29).ToString()
                        'n_amount_mounth = table_payment.Rows(x).Item(3).ToString()
                        n_sum_amount = table_payment.Rows(x).Item(31).ToString()
                        'date job this payment
                        strDateJob = table_payment.Rows(x).Item(0).ToString()
                        'interest mounth in percent for product
                        interest_mounth = Int(txt_interest.Text)

                        '-----Result Checking Data Payment to Output-----
                        table_temp.Rows(x).Item(0) = table_payment.Rows(x).Item(0).ToString()
                        table_temp.Rows(x).Item(1) = table_payment.Rows(x).Item(1).ToString()
                        table_temp.Rows(x).Item(2) = table_payment.Rows(x).Item(2).ToString()
                        table_temp.Rows(x).Item(3) = table_payment.Rows(x).Item(3).ToString()
                        table_temp.Rows(x).Item(4) = table_payment.Rows(x).Item(4).ToString()
                        table_temp.Rows(x).Item(5) = table_payment.Rows(x).Item(5).ToString()
                        table_temp.Rows(x).Item(6) = table_payment.Rows(x).Item(6).ToString()
                        table_temp.Rows(x).Item(7) = table_payment.Rows(x).Item(7).ToString()
                        table_temp.Rows(x).Item(8) = table_payment.Rows(x).Item(8).ToString()
                        table_temp.Rows(x).Item(9) = table_payment.Rows(x).Item(9).ToString()
                        table_temp.Rows(x).Item(10) = table_payment.Rows(x).Item(10).ToString()

                        table_temp.Rows(x).Item(11) = table_payment.Rows(x).Item(11).ToString()
                        table_temp.Rows(x).Item(12) = table_payment.Rows(x).Item(12).ToString()
                        table_temp.Rows(x).Item(13) = table_payment.Rows(x).Item(13).ToString()
                        table_temp.Rows(x).Item(14) = table_payment.Rows(x).Item(14).ToString()
                        table_temp.Rows(x).Item(15) = table_payment.Rows(x).Item(15).ToString()
                        table_temp.Rows(x).Item(16) = table_payment.Rows(x).Item(16).ToString()
                        table_temp.Rows(x).Item(17) = table_payment.Rows(x).Item(17).ToString()
                        table_temp.Rows(x).Item(18) = table_payment.Rows(x).Item(18).ToString()
                        table_temp.Rows(x).Item(19) = table_payment.Rows(x).Item(19).ToString()
                        table_temp.Rows(x).Item(20) = table_payment.Rows(x).Item(20).ToString()
                        table_temp.Rows(x).Item(21) = table_payment.Rows(x).Item(21).ToString()
                        table_temp.Rows(x).Item(22) = table_payment.Rows(x).Item(22).ToString()
                        table_temp.Rows(x).Item(23) = table_payment.Rows(x).Item(23).ToString()
                        table_temp.Rows(x).Item(24) = table_payment.Rows(x).Item(24).ToString()
                        table_temp.Rows(x).Item(25) = table_payment.Rows(x).Item(25).ToString()
                        table_temp.Rows(x).Item(26) = table_payment.Rows(x).Item(26).ToString()
                        table_temp.Rows(x).Item(27) = table_payment.Rows(x).Item(27).ToString()

                        '----Process Checking Payment ----
                        If Not String.IsNullOrWhiteSpace(multifinance_acc) Then
                            checkCancel = checkCancellationPayment(multifinance_acc)
                            'check table cancellation is not found
                            If checkCancel.Rows.Count() <= 0 Then
                                'check table disbursement
                                checkTableDisburse = checkDisbursement(multifinance_acc)
                                If checkTableDisburse.Rows.Count() > 0 Then
                                    'process check payment here
                                    'get data payment loan and counting in PMT Payment
                                    'amount loan
                                    n_loan = checkTableDisburse.Rows(0).Item(6).ToString()
                                    date_excute = checkTableDisburse.Rows(0).Item(0).ToString()
                                    'Process Output Info In Data Load
                                    'process max tenor
                                    max_tenor = (Convert.ToInt32(n_tenor) + Convert.ToInt32(remain_tenor))
                                    'bunga bulan/tahun,amount loan, max tenor, n_tenor
                                    table_pmt = ProcessPMT(interest_mounth, Convert.ToDouble(n_loan), Convert.ToInt32(max_tenor), n_tenor, date_excute)
                                    'column
                                    'table_pmt.Columns.Add("Month0")0
                                    'table_pmt.Columns.Add("Due Date1")1
                                    'table_pmt.Columns.Add("Start Payment")2
                                    'table_pmt.Columns.Add("End Payment")3
                                    'table_pmt.Columns.Add("Principal")4
                                    'table_pmt.Columns.Add("Interest")5
                                    'table_pmt.Columns.Add("Payment")6
                                    'table_pmt.Columns.Add("OS Balance")7
                                    'table_pmt.Columns.Add("Status")8

                                    'process to found interest and principal for matching with data reguler payment
                                    For j As Integer = 0 To table_pmt.Rows.Count - 1
                                        'if number mouth found
                                        tb_tenor = table_pmt.Rows(j).Item(0).ToString()
                                        'CHECK MONTH IN PAYMENT BETWEEN PMT CALCULATIONS
                                        If Strings.Equals(n_tenor, tb_tenor) Then
                                            'Set value data table principal and interest
                                            tb_principal = table_pmt.Rows(j).Item(4).ToString()
                                            tb_interest = table_pmt.Rows(j).Item(5).ToString()
                                            tb_amount = table_pmt.Rows(j).Item(6).ToString()

                                            '---(1). REGULER PAYMENT -------
                                            If cb_payment.SelectedIndex.ToString = 1 Then
                                                'calculation between data PMT and reguler payment file (Selisih)
                                                s_principal = Convert.ToDouble(n_principal) - tb_principal
                                                s_interest = Convert.ToDouble(n_interest) - tb_interest
                                                s_amount = Convert.ToDouble(n_sum_amount) - tb_amount
                                                'for output principal, interest,balanced for payment
                                                '-----------------------
                                                'For Type 200 in process and save balanced 
                                                'For Type 100 : if more than 1000 not process else process
                                                Dim type_loan As String = checkTableDisburse.Rows(0).Item(7).ToString()
                                                If type_loan = "100" Then
                                                    If s_principal > 1000 Or s_principal < -1000 Then
                                                        load_payment.Rows(x).Cells(1).Value = s_principal.ToString
                                                    Else
                                                        load_payment.Rows(x).Cells(1).Value = s_principal.ToString
                                                    End If
                                                    'for output interest
                                                    If s_interest > 1000 Or s_interest < -1000 Then
                                                        load_payment.Rows(x).Cells(2).Value = s_interest.ToString
                                                    Else
                                                        load_payment.Rows(x).Cells(2).Value = s_interest.ToString
                                                    End If
                                                ElseIf type_loan = "200" Then
                                                    load_payment.Rows(x).Cells(1).Value = s_principal.ToString
                                                    load_payment.Rows(x).Cells(2).Value = s_interest.ToString
                                                End If

                                                'check total balanced in database
                                                '(1) if balanced add to table still less amount in mouth = process with amount and "sisa" include database history (payment parsial)
                                                '(2) if balanced add to table enough in mouth = not process but still amount from database history (payment parsial)
                                                If type_loan = "100" Then
                                                    'for output selisih amount for count balanced amount
                                                    If s_amount < 0 Then
                                                        'Process Calculations PMT with upload (Payment File)
                                                        'column repayment principal
                                                        table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                                                        'column repayment interest
                                                        table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                                                        'column repayment sum amount
                                                        table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()

                                                        'show output load payment
                                                        load_payment.Rows(x).Cells(3).Value = tb_amount.ToString()
                                                        load_payment.Rows(x).Cells(4).Value = s_amount.ToString()
                                                        load_payment.Rows(x).Cells(0).Value = "Not Processed"
                                                        'total balanced in database
                                                        total_balanced = 0
                                                        reason_output = "Balanced not enough"
                                                        n_process = False

                                                        'change color 
                                                        load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                        load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                                    ElseIf s_amount >= 0 And s_amount <= 1000 Then
                                                        'Process Calculations PMT with upload (Payment File)
                                                        'column repayment principal
                                                        table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                                                        'column repayment interest
                                                        table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                                                        'column repayment sum amount
                                                        table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()

                                                        'show output load payment
                                                        load_payment.Rows(x).Cells(3).Value = tb_amount.ToString()
                                                        load_payment.Rows(x).Cells(4).Value = s_amount.ToString()
                                                        load_payment.Rows(x).Cells(0).Value = "Processed"
                                                        'total balanced in database
                                                        total_balanced = 0
                                                        reason_output = ""

                                                        'change color 
                                                        load_payment.Rows(x).Cells(0).Style.BackColor = Color.LightGreen
                                                        load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                                    ElseIf s_amount > 1000 Then
                                                        'Process Calculations PMT with upload (Payment File)
                                                        'column repayment principal
                                                        table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                                                        'column repayment interest
                                                        table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                                                        'column repayment sum amount
                                                        table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()

                                                        'show output load payment
                                                        load_payment.Rows(x).Cells(0).Value = "Not Processed"
                                                        load_payment.Rows(x).Cells(3).Value = tb_amount.ToString()
                                                        load_payment.Rows(x).Cells(4).Value = s_amount.ToString()
                                                        'total balanced in database
                                                        total_balanced = 0
                                                        reason_output = "Balanced Berlebih"
                                                        n_process = False
                                                        'change color 
                                                        load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                        load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                                    End If
                                                ElseIf type_loan = "200" Then
                                                    'For Type 200 
                                                    'Checking to proccess parsial payments
                                                    's_amount = Convert.ToDouble(n_sum_amount) - Convert.ToDouble(tb_amount)
                                                    '---(Selisih antara Amount Reguler(file) dikurang Amount Tenor this Month)---
                                                    'check data balanced last 
                                                    total_balanced = checkCountTotalBalanced(multifinance_acc, "")
                                                    'Console.WriteLine("total balanced :" + total_balanced)
                                                    If s_amount < 0 Then
                                                        'amount less, please check money in balanced database
                                                        load_payment.Rows(x).Cells(3).Value = tb_amount.ToString()
                                                        load_payment.Rows(x).Cells(4).Value = s_amount.ToString()

                                                        '.Columns(0).Name = "Status*"
                                                        '.Columns(1).Name = "Selisih Principal*"
                                                        '.Columns(2).Name = "Selisih Interest*"
                                                        '.Columns(3).Name = "Payments*"
                                                        '.Columns(4).Name = "Balanced*"
                                                        '.Columns(5).Name = "Total Balanced*"
                                                        '.Columns(6).Name = "Reason*"
                                                        'load_payment.Rows(x).Cells(3).Style.BackColor = Color.Green
                                                        'load_payment.Rows(x).Cells(0).Value = "Processed"
                                                        'checking process
                                                        If total_balanced <= 0 Then
                                                            'Process Calculations PMT with upload (Payment File)
                                                            'column repayment principal
                                                            table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                                                            'column repayment interest
                                                            table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                                                            'column repayment sum amount
                                                            table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()
                                                            'less balanced in database balanced
                                                            reason_output = "Balanced not enough"
                                                            n_process = False
                                                            'change color 
                                                            load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                            load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                                        ElseIf total_balanced > 0 Then
                                                            'amount enough in database and will get amount in calculation with payment in month
                                                            'total balanced (before) plus with (selisih amount this mounth and amount pay)
                                                            s_balanced = total_balanced + s_amount
                                                            'check condition (selisih) payment with calculation payment this month
                                                            If s_balanced >= 0 Then
                                                                'Process Calculations PMT with upload (Payment File)
                                                                'column repayment principal
                                                                table_temp.Rows(x).Item(28) = tb_principal.ToString()
                                                                'column repayment interest
                                                                table_temp.Rows(x).Item(29) = tb_interest.ToString()
                                                                'column repayment sum amount
                                                                table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                                                'add data from data array
                                                                'Dim checkBalancedHist As Integer
                                                                ''check data update this payment date
                                                                'checkBalancedHist = checkCountTotalBalanced(multifinance_acc, strDateJob)
                                                                'Console.WriteLine("check baland hist :" + checkBalancedHist + ":" + strDateJob)
                                                                ''not found data
                                                                'If checkBalancedHist < 0 Then
                                                                '    'add  history balanced to doing
                                                                '    addBalancedHist(multifinance_acc, DataPayments)
                                                                '    'update total balanced to doin
                                                                '    updateTotalBalanced(multifinance_acc, DataPayments)
                                                                'End If
                                                                load_payment.Rows(x).Cells(0).Value = "Processed with Balanced"
                                                                'Console.WriteLine("Processed with Balanced")
                                                                reason_output = "Balanced enough"
                                                                'change color 
                                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.LightGreen
                                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                                            ElseIf s_balanced < 0 Then
                                                                'Process Calculations PMT with upload (Payment File)
                                                                'column repayment principal
                                                                table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                                                                'column repayment interest
                                                                table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                                                                'column repayment sum amount
                                                                table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()
                                                                'payment is not process  
                                                                load_payment.Rows(x).Cells(0).Value = "Not Processed"
                                                                reason_output = "Balanced not enough"
                                                                n_process = False
                                                                'change color 
                                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                                            End If
                                                        End If
                                                    ElseIf s_amount = 0 Then
                                                        'Process Calculations PMT with upload (Payment File)
                                                        'column repayment principal
                                                        table_temp.Rows(x).Item(28) = tb_principal.ToString()
                                                        'column repayment interest
                                                        table_temp.Rows(x).Item(29) = tb_interest.ToString()
                                                        'column repayment sum amount
                                                        table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                                        'amount is same, please process it : payment this month
                                                        load_payment.Rows(x).Cells(3).Value = tb_amount.ToString()
                                                        load_payment.Rows(x).Cells(4).Value = s_amount.ToString()
                                                        'load_payment.Rows(x).Cells(3).Style.BackColor = Color.Red
                                                        'load_payment.Rows(x).Cells(0).Value = "Not Processed"
                                                        load_payment.Rows(x).Cells(0).Value = "Processed"
                                                        reason_output = "Balanced enough"
                                                        'change color 
                                                        load_payment.Rows(x).Cells(0).Style.BackColor = Color.LightGreen
                                                        load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                                    ElseIf s_amount > 0 Then
                                                        'Process Calculations PMT with upload (Payment File)
                                                        'column repayment principal
                                                        table_temp.Rows(x).Item(28) = tb_principal.ToString()
                                                        'column repayment interest
                                                        table_temp.Rows(x).Item(29) = tb_interest.ToString()
                                                        'column repayment sum amount
                                                        table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                                        'amount is more, please process it and save and add (selisih in database balanced)
                                                        load_payment.Rows(x).Cells(3).Value = tb_amount.ToString()
                                                        '(sisa) amount after calculation
                                                        load_payment.Rows(x).Cells(4).Value = s_amount.ToString()
                                                        'amount enough in database and will get amount in calculation with payment in month
                                                        'total balanced (before) plus with (selisih amount this mounth and amount pay)
                                                        load_payment.Rows(x).Cells(0).Value = "Processed"
                                                        reason_output = "Balanced enough"
                                                        'change color 
                                                        load_payment.Rows(x).Cells(0).Style.BackColor = Color.LightGreen
                                                        load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                                    End If

                                                End If
                                                '---Save data to table temp for spit output
                                                'addition 5 column datatemp index 34-39
                                                'table_temp.Columns.Add("Selisih Principal*")
                                                'table_temp.Columns.Add("Selisih Interest*")
                                                'table_temp.Columns.Add("Payments*")
                                                'table_temp.Columns.Add("Balanced*")
                                                'table_temp.Columns.Add("Total Balanced*")
                                                'table_temp.Columns.Add("Reason*")

                                                'Process Output from all calculations is done
                                                table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                                table_temp.Rows(x).Item(32) = table_payment.Rows(x).Item(32).ToString()
                                                table_temp.Rows(x).Item(33) = table_payment.Rows(x).Item(33).ToString()
                                                'after Process Calculations PMT with upload (Payment File)
                                                'selisih principal
                                                table_temp.Rows(x).Item(34) = s_principal.ToString()
                                                'selisih interest
                                                table_temp.Rows(x).Item(35) = s_interest.ToString()
                                                'amount payment this month
                                                table_temp.Rows(x).Item(36) = tb_amount.ToString()
                                                '(sisa) balanced from reguler amount (kurang) with payment this amount
                                                'addition 6 column datatemp 
                                                'column balanced
                                                table_temp.Rows(x).Item(37) = s_amount.ToString()
                                                'column total balanced
                                                table_temp.Rows(x).Item(38) = total_balanced.ToString()
                                                'column reason
                                                table_temp.Rows(x).Item(39) = reason_output.ToString()
                                                '-- End process payment in month---



                                            ElseIf cb_payment.SelectedIndex.ToString = 2 Then
                                                '---(2). EARLY TERMINATION (PAYMENT) -------



                                            End If



                                        End If
                                    Next

                                Else
                                    '--data not found in disbursement--
                                    'column repayment principal
                                    table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                                    'column repayment interest
                                    table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                                    table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                    'column repayment sum amount
                                    table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()
                                    table_temp.Rows(x).Item(32) = table_payment.Rows(x).Item(32).ToString()
                                    table_temp.Rows(x).Item(33) = table_payment.Rows(x).Item(33).ToString()
                                    '(sisa) balanced from reguler amount (kurang) with payment this amount
                                    'addition 7 column datatemp 
                                    'selisih principal
                                    table_temp.Rows(x).Item(34) = ""
                                    'selisih interest
                                    table_temp.Rows(x).Item(35) = ""
                                    'selisih principal
                                    table_temp.Rows(x).Item(35) = ""
                                    'selisih interest
                                    table_temp.Rows(x).Item(36) = ""
                                    'column balanced
                                    table_temp.Rows(x).Item(37) = ""
                                    'column total balanced
                                    table_temp.Rows(x).Item(38) = ""
                                    load_payment.Rows(x).Cells(0).Value = "Not Processed"
                                    reason_output = "Disbursement Not Found"
                                    'column reason
                                    table_temp.Rows(x).Item(39) = reason_output.ToString()
                                    'total balanced in database
                                    total_balanced = 0
                                    n_process = False
                                    'change color 
                                    load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                    load_payment.Rows(x).Cells(0).Style.ForeColor = Color.Black
                                End If


                            Else
                                'data found in cancelations transactions
                                'column repayment principal
                                table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                                'column repayment interest
                                table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                                table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                'column repayment sum amount
                                table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()
                                table_temp.Rows(x).Item(32) = table_payment.Rows(x).Item(32).ToString()
                                table_temp.Rows(x).Item(33) = table_payment.Rows(x).Item(33).ToString()
                                'addition 4 column datatemp 
                                'selisih principal
                                table_temp.Rows(x).Item(35) = ""
                                'selisih interest
                                table_temp.Rows(x).Item(36) = ""
                                'column balanced
                                table_temp.Rows(x).Item(37) = ""
                                'column total balanced
                                table_temp.Rows(x).Item(38) = ""
                                load_payment.Rows(x).Cells(0).Value = "Not Processed"
                                reason_output = "Cancelled on " + checkCancel.Rows(0).Item(0).ToString()
                                'column reason
                                table_temp.Rows(x).Item(39) = reason_output.ToString()
                                'total balanced in database
                                total_balanced = 0
                                n_process = False
                                'change color 
                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White

                            End If
                            'column status
                            table_temp.Rows(x).Item(40) = n_process.ToString()
                            'status total balanced 
                            load_payment.Rows(x).Cells(5).Value = total_balanced.ToString()
                            load_payment.Rows(x).Cells(6).Value = reason_output.ToString()
                            reason_output = ""
                            '---Proccess to Output---
                            'give info to process payment or not
                            If n_process = True Then
                                'load_payment.Rows(x).Cells(0).Style.BackColor = Color.Green
                                'load_payment.Rows(x).Cells(4).Style.ForeColor = Color.White
                                table_process.ImportRow(table_temp.Rows(x))
                                'data gridview process
                                load_payment_process.DataSource = table_process

                            ElseIf n_process = False Then
                                'load_payment.Rows(x).Cells(0).Style.BackColor = Color.Red
                                'load_payment.Rows(x).Cells(4).Style.ForeColor = Color.White
                                table_not_process.ImportRow(table_temp.Rows(x))
                                'data gridview not process
                                load_payment_notprocess.DataSource = table_not_process
                            End If

                        End If
                        'process loading persen
                        Dim persen As Integer
                        persen = (x / Int(load_payment.RowCount - 1)) * 100
                        If x = Int(load_payment.RowCount - 2) Then
                            persen = 100
                        End If
                        txt_checking_load.Text = " ( " + Int(persen).ToString + "% ) "
                        'event load data
                        Application.DoEvents()
                    Next
                    'sender output to datatable 
                    'add new rows null
                    If table_not_process.Rows.Count <= 0 Then
                        'insert new blank
                        Dim rowBlank As DataRow
                        rowBlank = table_not_process.NewRow
                        table_not_process.Rows.InsertAt(rowBlank, 0)
                        'send to datagridview
                        load_payment_notprocess.DataSource = table_not_process
                    End If
                    If table_process.Rows.Count <= 0 Then
                        'insert new blank
                        Dim rowBlank2 As DataRow
                        rowBlank2 = table_process.NewRow
                        table_process.Rows.InsertAt(rowBlank2, 0)
                        'send to datagridview
                        load_payment_process.DataSource = table_process
                    End If
                    'Create Time Duration over 
                    Dim timeEnd As Date = Convert.ToDateTime(DateTime.Now)
                    spanTime = timeEnd.Subtract(timeStart)
                    'inisialisasi variabel span time
                    st_hours = spanTime.TotalHours
                    st_minutes = spanTime.TotalMinutes
                    st_seconds = spanTime.TotalSeconds
                    st_seconds = Strings.Left(Int(st_seconds), 2)
                    Dim totalSpan As String = " " + Int(st_hours).ToString + " Jam, " + Int(st_minutes).ToString + " Menit," + Int(st_seconds).ToString + " Detik."
                    'GiveFeedbackAlert MessageBox
                    MessageBox.Show("Duration : " + totalSpan, "Checking Done.")
                End If

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function ProcessPMT(ByRef interest_mounth As Integer, ByRef loan_amount As Double, ByRef max_tenor As Integer, ByRef n_tenor As Integer, ByRef dateDisburse As String) As DataTable
        Dim loan As Double
        Dim table_PMT As New DataTable
        Dim rowPMT As DataRow
        Dim interest_percent As Double = Int(txt_interest.Text)
        Dim instalment_amount As Double = 0
        Dim interest_n As Double = 0
        Dim OS_Balance As Double = 0
        Dim Principal As Double = 0
        Dim sumPrincipal As Double
        Dim sumPayments As Double
        Dim sumInstalment As Double
        Dim datePayment As String
        Dim getdate, setdate As DateTime
        Dim dateDisbure, startDate, endDate As DateTime
        Dim strDateDisbure As String
        'max tenor in duedate looping
        Dim arrDueDate(60) As String
        Dim getNoDays As Integer
        'clear table
        table_PMT.Rows.Clear()
        table_PMT.Columns.Clear()

        interest_percent = Convert.ToDouble(interest_mounth)
        loan = Convert.ToDouble(loan_amount)
        'CountPMT('Payment loan','interest rate','month tenor')
        instalment_amount = CountPMT(loan, interest_percent, max_tenor)

        '------Start set count due date any month------
        'strdate is Date Disburesement
        strDateDisbure = dateDisburse.ToString
        'Dim getdate As Date = Convert.ToDateTime(strdate)
        'strdate = getdate.ToString("dd-MM-yyyy")
        Dim getYear As String = Strings.Left(strDateDisbure, 4)
        Dim getMonth As String = Strings.Mid(strDateDisbure, 5, 2)
        Dim getDay As String = Strings.Right(strDateDisbure, 2)

        strDateDisbure = getYear + "-" + getMonth + "-" + getDay
        ' Dim setDueDate As DateTime = DateTime.Parse(strDateDisbure)

        'Dim dateTime As String = strDateDisbure
        dateDisbure = Convert.ToDateTime(strDateDisbure.ToString)

        'show interest, payment in mothly until payment end tenor
        'add new column
        table_PMT.Columns.Add("Month")
        table_PMT.Columns.Add("Due Date")
        table_PMT.Columns.Add("Start Payment")
        table_PMT.Columns.Add("End Payment")
        table_PMT.Columns.Add("Principal")
        table_PMT.Columns.Add("Interest")
        table_PMT.Columns.Add("Payment")
        table_PMT.Columns.Add("OS Balance")
        table_PMT.Columns.Add("Status")

        'table_PMT.Columns.Add("Month")
        'table_PMT.Columns.Add("Principal")
        'table_PMT.Columns.Add("Interest")
        'table_PMT.Columns.Add("Payment")
        'table_PMT.Columns.Add("OS Balance")
        'table_PMT.Columns.Add("Status")

        'processing table payment
        For x As Integer = 0 To max_tenor
            'add new row
            rowPMT = table_PMT.NewRow()
            table_PMT.Rows.Add(rowPMT)
            'process formula 
            'interest
            If x = 0 Then
                Dim loan_percent As Double = interest_percent * loan
                interest_n = (loan_percent / 12)
                interest_n = Math.Round(interest_n)

            ElseIf x = max_tenor Then
                interest_n = interest_n
            Else
                Dim loan_percent As Double = interest_percent * OS_Balance
                interest_n = (loan_percent / 12)
                interest_n = Math.Round(interest_n)
            End If
            'principal             
            If x = max_tenor - 1 Then
                Principal = instalment_amount - interest_n
            ElseIf x = max_tenor Then
                Principal = Principal + OS_Balance
            Else
                Principal = instalment_amount - interest_n
            End If
            'Payment             
            If x = max_tenor Then
                instalment_amount = Principal + interest_n
            End If
            'OS Balance
            If x = 0 Then
                OS_Balance = loan - Principal
            ElseIf x = max_tenor Then
                OS_Balance = Convert.ToString(0)
            Else
                OS_Balance = OS_Balance - Principal
            End If

            'end formula
            'calculation sum
            If Not x = Int(max_tenor - 1) Then
                sumPrincipal += Principal
                sumInstalment += interest_n
                sumPayments += instalment_amount
            End If
            'row data column Month
            If x = max_tenor Then
                'adjust payment in last tenor
                table_PMT.Rows(x).Item(0) = x
            Else
                table_PMT.Rows(x).Item(0) = x + 1
            End If

            'due date payment any month
            'condition calculations Day (WorksDay)
            'Add 1 Month from Date Disbursement

            If x = max_tenor Then
                setdate = DateAdd(DateInterval.Month, x, dateDisbure)
            Else
                setdate = DateAdd(DateInterval.Month, x + 1, dateDisbure)
            End If
            '--Due Date Month 1 --

            'check max date if not day in month
            'get duedate last month
            If IsError(setdate) Then
                'count last date in month
                setdate = DateSerial(Year(setdate), Month(setdate) + 1, 0)
                'add to array
                arrDueDate(x) = setdate
                table_PMT.Rows(x).Item(1) = setdate.ToString("dd-MM-yyyy")
            Else
                'add to array
                arrDueDate(x) = setdate
                table_PMT.Rows(x).Item(1) = setdate.ToString("dd-MM-yyyy")
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

                Console.WriteLine(getNoDays)
                If getNoDays = 1 Or getNoDays = 7 Then
                    'add next day
                    startDate = DateAdd(DateInterval.Day, 1, startDate)
                    GoTo checkLoopStartPayment1
                Else
                    If x = max_tenor Then
                        startDate = arrDueDate(x)
                        startDate = DateAdd(DateInterval.Month, -1, startDate)
                        table_PMT.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                        'end check wordays
                    Else

                        Console.WriteLine(startDate)
                        'check workdays
                        table_PMT.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                        'end check wordays
                    End If
                End If
            ElseIf x = 0 Then
                startDate = dateDisbure

                'Console.WriteLine(startDate)
checkLoopStartPayment2:
                getNoDays = checkDayofWeeks(startDate)

                Console.WriteLine(getNoDays)
                If getNoDays = 1 Or getNoDays = 7 Then
                    'add next day
                    startDate = DateAdd(DateInterval.Day, 1, startDate)
                    GoTo checkLoopStartPayment2
                Else

                    Console.WriteLine(startDate)
                    table_PMT.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                End If

                ' startDate = arrDueDate(x - 1)
                'startDate = DateAdd(DateInterval.Month, -1, startDate)
            End If

checkLoopEndPayment:
            getNoDays = checkDayofWeeks(setdate)
            If getNoDays = 1 And getNoDays = 7 Then
                'add next day
                endDate = DateAdd(DateInterval.Day, -1, setdate)
                GoTo checkLoopEndPayment
            Else
                endDate = DateAdd(DateInterval.Day, -1, setdate)
                table_PMT.Rows(x).Item(3) = endDate.ToString("dd-MM-yyyy")
            End If

            ' insert data rows in datatable payment
            'column Principal
            table_PMT.Rows(x).Item(4) = Convert.ToString(Principal)
            'column Interest
            table_PMT.Rows(x).Item(5) = Convert.ToString(interest_n)
            'column Instalment /Payment
            table_PMT.Rows(x).Item(6) = Convert.ToString(instalment_amount)
            'column OS Balance
            If x = max_tenor Then
                table_PMT.Rows(x).Item(7) = Convert.ToString(Int(OS_Balance))
            Else
                table_PMT.Rows(x).Item(7) = Convert.ToString(OS_Balance)
            End If
            'match with looping in table : index 0
            'status column in payment PMT
            If Strings.Equals(x, n_tenor - 1) Then
                table_PMT.Rows(x).Item(8) = "On Process"
            ElseIf x < n_tenor - 1 Then
                table_PMT.Rows(x).Item(8) = "Paid"
            Else
                table_PMT.Rows(x).Item(8) = "Not Paid"
            End If
        Next

        'remove row in datatable
        table_PMT.Rows.RemoveAt(Int(max_tenor - 1))

        Return table_PMT

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
    Private Function CountPMT(ByRef PVal As Double, ByRef APR As Double, ByRef TotPmts As Integer) As Double
        Dim Payment As Double
        Dim PayType As DueDate
        'Dim Response As MsgBoxResult
        ' Define money format.
        ' Usually 0 for a loan.
        Dim FVal As Double = 0
        'PVal = CDbl(InputBox("How much do you want to borrow?"))
        'APR = CDbl(InputBox("What is the annual percentage rate of your loan?"))
        If APR > 1 Then APR = APR / 100 ' Ensure proper form.
        'TotPmts = CDbl(InputBox("How many monthly payments will you make?"))
        'Response = MsgBox("Do you make payments at the end of month?", MsgBoxStyle.YesNo)
        'If Response = MsgBoxResult.No Then
        '    PayType = DueDate.BegOfPeriod
        'Else
        '    PayType = DueDate.EndOfPeriod
        'End If
        PayType = DueDate.EndOfPeriod
        Payment = Pmt(APR / 12, TotPmts, -PVal, FVal, PayType)
        'Pembulatan
        Payment = Math.Round(Payment)
        Return Payment
    End Function

    Private Sub load_payment_DoubleClick(sender As Object, e As EventArgs) Handles load_payment.DoubleClick
        Dim multi_acc, str_date_excute, str_date_payment As String
        Dim date_excute, date_payment As Date
        Dim n_tenor, remain_tenor, max_tenor As String
        Dim pmt As New Detail_Payment() With {.Owner = Me}
        Dim checkTable, table_pmt As DataTable
        Dim n_loan As Double
        Dim interest_mounth As Integer = 14
        Dim tb_principal, tb_interest, tb_amount As String
        Dim s_principal, s_interest, s_amount As Double
        Dim tb_tenor As String
        Dim n_balanced, n_principal, n_interest, n_amount As String
        Dim s_balanced, total_balanced As Double
        Dim c_balanced, t_balanced As Double
        Dim rowIndex As Integer = load_payment.CurrentRow.Index
        'send index
        pmt.indexRows = rowIndex
        str_date_payment = table_payment.Rows(rowIndex).Item(0).ToString()
        multi_acc = table_payment.Rows(rowIndex).Item(5).ToString()
        remain_tenor = table_payment.Rows(rowIndex).Item(13).ToString()
        n_tenor = table_payment.Rows(rowIndex).Item(14).ToString()
        'Show output
        'data 
        pmt.txt_multifinance_customer.Text = multi_acc.ToString
        pmt.txt_no_cif.Text = table_payment.Rows(rowIndex).Item(6).ToString()
        pmt.txt_shbi_acc_no.Text = table_payment.Rows(rowIndex).Item(8).ToString()
        'get data loan from disbursement
        checkTable = checkDisbursement(multi_acc)
        If Not checkTable.Rows.Count <= 0 Then
            n_loan = checkTable.Rows(0).Item(6).ToString()
            str_date_excute = checkTable.Rows(0).Item(0).ToString()
            'SELECT TOP 1 [Date] ,[Transaction] ,[Multifinance CIF],[Multifinance Account],[SHBI CIF],[SHBI Account],[Amount],[Trx Type] FROM [DB_MASTER].[dbo].[DB_DISBURSEMENT_KRD] 
            
            'data payment
            n_principal = table_payment.Rows(rowIndex).Item(28).ToString()
            pmt.txt_principal.Text = n_principal.ToString
            'column repayment interest
            n_interest = table_payment.Rows(rowIndex).Item(29).ToString()
            pmt.txt_interest.Text = n_interest.ToString
            'column repayment sum amount
            n_amount = table_payment.Rows(rowIndex).Item(31).ToString()
            pmt.txt_amount.Text = n_amount.ToString

            'Process Output Info In Data Load
            'process n tenor
            'process max tenor
            max_tenor = (Convert.ToInt32(n_tenor) + Convert.ToInt32(remain_tenor)).ToString()
            'proccess count PMT Payment
            table_pmt = ProcessPMT(interest_mounth, Convert.ToDouble(n_loan), Convert.ToInt32(max_tenor), n_tenor, str_date_excute)
            'column
            'table_pmt.Columns.Add("Month0")0
            'table_pmt.Columns.Add("Due Date1")1
            'table_pmt.Columns.Add("Start Payment")2
            'table_pmt.Columns.Add("End Payment")3
            'table_pmt.Columns.Add("Principal")4
            'table_pmt.Columns.Add("Interest")5
            'table_pmt.Columns.Add("Payment")6
            'table_pmt.Columns.Add("OS Balance")7
            'table_pmt.Columns.Add("Status")8
            For j As Integer = 0 To table_pmt.Rows.Count - 1
                'if number mouth found
                tb_tenor = table_pmt.Rows(j).Item(0).ToString()
                'CHECK MONTH IN PAYMENT BETWEEN PMT CALCULATIONS
                If Strings.Equals(n_tenor, tb_tenor) Then
                    'show selisih payment in month
                    tb_principal = table_pmt.Rows(j).Item(4).ToString()
                    tb_interest = table_pmt.Rows(j).Item(5).ToString()
                    tb_amount = table_pmt.Rows(j).Item(6).ToString()
                    s_principal = Convert.ToDouble(n_principal) - Convert.ToDouble(tb_principal)
                    s_interest = Convert.ToDouble(n_interest) - Convert.ToDouble(tb_interest)
                    s_balanced = Convert.ToDouble(n_amount) - Convert.ToDouble(tb_amount)

                End If
            Next
            'convert date to datetime
            Dim getYear1 As String = Strings.Left(str_date_payment, 4)
            Dim getMonth1 As String = Strings.Mid(str_date_payment, 5, 2)
            Dim getDay1 As String = Strings.Right(str_date_payment, 2)

            Dim getYear2 As String = Strings.Left(str_date_excute, 4)
            Dim getMonth2 As String = Strings.Mid(str_date_excute, 5, 2)
            Dim getDay2 As String = Strings.Right(str_date_excute, 2)

            str_date_payment = getYear1 + "-" + getMonth1 + "-" + getDay1
            str_date_excute = getYear2 + "-" + getMonth2 + "-" + getDay2
            'convert to datetime
            date_payment = Convert.ToDateTime(str_date_payment)
            date_excute = Convert.ToDateTime(str_date_excute)

            'data calculation payment 
            pmt.txt_selisih_interest.Text = s_interest.ToString()
            pmt.txt_selisih_principal.Text = s_principal.ToString()
            pmt.txt_balanced.Text = s_balanced.ToString()
            'data nasabah
            pmt.txt_name.Text = table_payment.Rows(rowIndex).Item(7).ToString()
            pmt.txt_date_payment.Text = date_payment.ToString("dd-MM-yyyy")
            pmt.txt_date_disburse.Text = date_excute.ToString("dd-MM-yyyy")
            pmt.txt_tenor.Text = max_tenor.ToString()
            pmt.txt_tenor_no.Text = n_tenor.ToString()
            pmt.txt_remain_tenor.Text = remain_tenor.ToString()
            pmt.txt_amount_disburse.Text = n_loan.ToString()
            pmt.txt_flag.Text = table_payment.Rows(rowIndex).Item(33).ToString()
            pmt.txt_auto_trans_no.Text = table_payment.Rows(rowIndex).Item(32).ToString()
            pmt.txt_reason.Text = load_payment.Rows(rowIndex).Cells(6).Value

            'load data balanced history
            pmt.load_balanced.DataSource = checkBalancedHistAcc(multi_acc)
            'load data payment
            pmt.load_payment.DataSource = table_pmt
            pmt.load_payment.Columns(0).Width = 50
            pmt.load_payment.Columns(1).Width = 70
            pmt.load_payment.Columns(2).Width = 70
            pmt.load_payment.Columns(3).Width = 70
            pmt.load_payment.Columns(4).Width = 70
            pmt.load_payment.Columns(5).Width = 70
            pmt.load_payment.Columns(6).Width = 70
            pmt.load_payment.Columns(7).Width = 70
            'data nasabah
            pmt.ShowDialog()
        Else
            MessageBox.Show("Disburse Not Found.", "Alert")
        End If

    End Sub
    Private Function checkBalancedHistAcc(ByRef multifinance_acc As String) As DataTable
        Dim table_balanced As DataTable
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then
            table_balanced = Nothing
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)

            cmd = New SqlCommand("SELECT [repayment date],[execution date],[Product code],[Product cif],[Product acc no],[shbi cif no] ,[customer name],[shbi acct no],[Balanced],[auto transfer acno],[Flag],[Tenor no],[Tenor],[Ket] FROM [DB_MASTER].[dbo].[DB_BALANCED_HIST] WHERE [Product acc no] = '" + multifinance_acc.ToString + "'", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data balanced calculation
                'sum data total balanced
                table_balanced = checktable.Copy()
            Else
                table_balanced = Nothing
            End If
        End If
        Return table_balanced
    End Function


    Private Sub btn_balanced_Click(sender As Object, e As EventArgs) Handles btn_balanced.Click
        Dim db As New DetailBalanced() With {.Owner = Me}
        'proccess load data payment parsial from table temp
        'copy data table 
        db.table_temp = table_temp.Copy()

        db.ShowDialog()
    End Sub

    Public Sub DataTableToExportFile(table As DataTable, filePath As String)
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
                        Else
                            writer.Write("|")
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
   Private Sub ExportDataToExcel(ByRef gettable As DataTable, ByRef filename As String)
        Using out = New XLWorkbook()
            'out.Worksheets.Add(gettable)
            out.Worksheets.Add(gettable, "Sheet1")
            out.SaveAs(filename)
        End Using
    End Sub
    Private Sub cb_export_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_export.SelectedIndexChanged
        Try
            If Not cb_export.SelectedIndex = 0 Then
                If table_payment.Rows.Count <= 0 Then
                    MessageBox.Show("Data is null.", "Alert")
                Else
                    Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                    Dim strdate As String
                    Dim filename1 As String = ""
                    Dim extention As String
                    getdate.ToString("ddMMyyyyHHmmss")
                    strdate = getdate.ToString("dd") + "" + getdate.ToString("MM") + "" + getdate.ToString("yyyy") + "_" + getdate.ToString("HH") + "" + getdate.ToString("mm") + "" + getdate.ToString("ss")

                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"

                        'type payment : Early Terminations
                        If cb_payment.SelectedIndex.ToString = 1 Then
                            'kredivo product
                            If cb_product.SelectedIndex.ToString = 0 Then
                                'Data Early Kredivo product
                                If tabcontrol1.SelectedIndex.ToString = 0 Then
                                    'data load payment
                                    filename1 = "D:\KRD_Early_Termination_Payment_Load_" + strdate + extention
                                    Call ExportDataToExcel(table_temp, filename1)
                                    'Data payment process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                                    filename1 = "D:\KRD_Early_Termination_Payment_Process_" + strdate + extention
                                    Call ExportDataToExcel(table_process, filename1)
                                    'Data payment not process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                                    filename1 = "D:\KRD_Early_Termination_Payment_Not_Process_" + strdate + extention
                                    Call ExportDataToExcel(table_not_process, filename1)
                                End If
                                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                                'akulaku product
                            ElseIf cb_product.SelectedIndex.ToString = 1 Then
                                'Data Early Akulaku product
                                If tabcontrol1.SelectedIndex.ToString = 0 Then
                                    'data load payment
                                    filename1 = "D:\AKU_Early_Termination_Payment_Load_" + strdate + extention
                                    Call ExportDataToExcel(table_temp, filename1)
                                    'Data payment process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                                    filename1 = "D:\AKU_Early_Termination_Payment_Process_" + strdate + extention
                                    Call ExportDataToExcel(table_process, filename1)
                                    'Data payment not process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                                    filename1 = "D:\AKU_Early_Termination_Payment_Not_Process_" + strdate + extention
                                    Call ExportDataToExcel(table_not_process, filename1)
                                End If
                                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                            End If
                            'type payment : Reguler Payment
                        ElseIf cb_payment.SelectedIndex.ToString = 2 Then
                            'kredivo product
                            If cb_product.SelectedIndex.ToString = 0 Then
                                'Data Early Kredivo product
                                If tabcontrol1.SelectedIndex.ToString = 0 Then
                                    'data load payment
                                    filename1 = "D:\KRD_Reguler_Payment_Load_" + strdate + extention
                                    Call ExportDataToExcel(table_temp, filename1)
                                    'Data payment process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                                    filename1 = "D:\KRD_Reguler_Payment_Process_" + strdate + extention
                                    Call ExportDataToExcel(table_process, filename1)
                                    'Data payment not process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                                    filename1 = "D:\KRD_Reguler_Payment_Not_Process_" + strdate + extention
                                    Call ExportDataToExcel(table_not_process, filename1)
                                End If
                                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                                'akulaku product
                            ElseIf cb_product.SelectedIndex.ToString = 1 Then
                                'Data Early Akulaku product
                                If tabcontrol1.SelectedIndex.ToString = 0 Then
                                    'data load payment
                                    filename1 = "D:\AKU_Reguler_Payment_Load_" + strdate + extention
                                    Call ExportDataToExcel(table_temp, filename1)
                                    'Data payment process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                                    filename1 = "D:\AKU_Reguler_Payment_Process_" + strdate + extention
                                    Call ExportDataToExcel(table_process, filename1)
                                    'Data payment not process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                                    filename1 = "D:\AKU_Reguler_Payment_Not_Process_" + strdate + extention
                                    Call ExportDataToExcel(table_not_process, filename1)
                                End If
                                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                            End If
                        End If

                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".txt"

                        'type payment : Early Terminations
                        If cb_payment.SelectedIndex.ToString = 1 Then
                            'kredivo product
                            If cb_product.SelectedIndex.ToString = 0 Then
                                'Data Early Kredivo product
                                If tabcontrol1.SelectedIndex.ToString = 0 Then
                                    'data load payment
                                    filename1 = "D:\KRD_Early_Termination_Payment_Load_" + strdate + extention
                                    Call DataTableToExportFile(table_temp, filename1)
                                    'Data payment process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                                    filename1 = "D:\KRD_Early_Termination_Payment_Process_" + strdate + extention
                                    Call DataTableToExportFile(table_process, filename1)
                                    'Data payment not process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                                    filename1 = "D:\KRD_Early_Termination_Payment_Not_Process_" + strdate + extention
                                    Call DataTableToExportFile(table_not_process, filename1)
                                End If
                                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                                'akulaku product
                            ElseIf cb_product.SelectedIndex.ToString = 1 Then
                                'Data Early Akulaku product
                                If tabcontrol1.SelectedIndex.ToString = 0 Then
                                    'data load payment
                                    filename1 = "D:\AKU_Early_Termination_Payment_Load_" + strdate + extention
                                    Call DataTableToExportFile(table_temp, filename1)
                                    'Data payment process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                                    filename1 = "D:\AKU_Early_Termination_Payment_Process_" + strdate + extention
                                    Call DataTableToExportFile(table_process, filename1)
                                    'Data payment not process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                                    filename1 = "D:\AKU_Early_Termination_Payment_Not_Process_" + strdate + extention
                                    Call DataTableToExportFile(table_not_process, filename1)
                                End If
                                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                            End If
                            'type payment : Reguler Payment
                        ElseIf cb_payment.SelectedIndex.ToString = 2 Then
                            'kredivo product
                            If cb_product.SelectedIndex.ToString = 0 Then
                                'Data Early Kredivo product
                                If tabcontrol1.SelectedIndex.ToString = 0 Then
                                    'data load payment
                                    filename1 = "D:\KRD_Reguler_Payment_Load_" + strdate + extention
                                    Call DataTableToExportFile(table_temp, filename1)
                                    'Data payment process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                                    filename1 = "D:\KRD_Reguler_Payment_Process_" + strdate + extention
                                    Call DataTableToExportFile(table_process, filename1)
                                    'Data payment not process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                                    filename1 = "D:\KRD_Reguler_Payment_Not_Process_" + strdate + extention
                                    Call DataTableToExportFile(table_not_process, filename1)
                                End If
                                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                                'akulaku product
                            ElseIf cb_product.SelectedIndex.ToString = 1 Then
                                'Data Early Akulaku product
                                If tabcontrol1.SelectedIndex.ToString = 0 Then
                                    'data load payment
                                    filename1 = "D:\AKU_Reguler_Payment_Load_" + strdate + extention
                                    Call DataTableToExportFile(table_temp, filename1)
                                    'Data payment process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                                    filename1 = "D:\AKU_Reguler_Payment_Process_" + strdate + extention
                                    Call DataTableToExportFile(table_process, filename1)
                                    'Data payment not process
                                ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                                    filename1 = "D:\AKU_Reguler_Payment_Not_Process_" + strdate + extention
                                    Call DataTableToExportFile(table_not_process, filename1)
                                End If
                                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                            End If
                        End If

                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn_summary_Click(sender As Object, e As EventArgs) Handles btn_summary.Click

    End Sub
End Class