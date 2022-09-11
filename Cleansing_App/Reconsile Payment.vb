
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
Imports System.Linq

Public Class Reconsile_Payment

    Private notProccess As Boolean
    Private conn As OleDbConnection 'connection odb
    Private dts As DataSet ' create table datasheet virtual
    Private strFile As String
    ''progress variabel virtual datatable
    Private table_payment, table_DT, table_DT_temp, table_payment_temp, table_oustanding, table_oustanding_temp, table_temp, table_process, table_not_process, table_DueDate, interest_day As New DataTable
    Private checkTableDisburse, table_pmt, checkCancel As New DataTable
    Private table_ostanding_group, table_payment_group As New DataTable
    Private rowTemp, rowDT, rowProcess, rowNotProcess As DataRow
    Dim openfiledialog As New OpenFileDialog ' open file dialog
    '...............SQL SERVER..............
    'Connection SQL Server
    Public SQL As New ConnectionSQL
    Public typeAccounts As New accountPayments
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Public getPdt As New accountPayments
    'Declare variable for PMT Payment
    Private rowPayment As DataRow
    Private st_hours, st_minutes, st_seconds, interest_daily As Double
    Private n_days, t_days As Integer
    Private wordsFullName = New String() {}
    Private arrWords As New ArrayList()
    Private tableCOABalance As DataTable = Nothing
    Private Sub Reconsile_Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim getProducts As New DataTable
            Dim strProduct As String
            cb_export.SelectedIndex = 0
            cb_product.SelectedIndex = 0
            cb_import_dt.SelectedIndex = 0
            'default
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            C_data4.Text = ""
            cb_import_dt.Hide()
            btn_coa_balance.Hide()
            l_payment3.Hide()
            l_payment4.Hide()
            'get data product
            getProducts = getPdt.getAllProducts()
            For a = 0 To getProducts.Rows.Count - 1
                strProduct = "[" + (a + 1).ToString + "] " + getProducts.Rows(a).Item(1).ToString
                cb_product.Items.Add(strProduct.ToString)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub clearDatable()
        'clear table
        table_temp.Columns.Clear()
        table_temp.Rows.Clear()
    End Sub
    Private Sub btn_import_ostanding_Click(sender As Object, e As EventArgs) Handles btn_import_ostanding.Click
        Try
            Dim ds As New DataSet()
            Dim myTableName As String
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls|Text Files (*.txt)|*.txt"
            'OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'before clear datatable
                table_oustanding_temp.Columns.Clear()
                table_oustanding_temp.Rows.Clear()
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                strFile = fi.FullName

                Dim extStr As String = Path.GetExtension(OpenFileDialog1.FileName)
                'fomat extenstions
                If extStr = ".xls" Or extStr = ".xlsx" Then
                    '(2) reader excel to Datatable
                    conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFile + ";Extended Properties=Excel 12.0;")
                    conn.Open()
                    myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                    Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                    Dim da As New OleDbDataAdapter(sqlquery, conn)
                    da.Fill(ds)
                    If cb_payment.SelectedIndex = 1 Then
                        If table_oustanding_temp.Rows.Count > 0 Then
                            table_oustanding_temp = ds.Tables(0)
                            'for reguler prosess
                            'change column name by index
                            table_oustanding_temp.Columns(3).ColumnName = "No_Rekening"
                            table_oustanding_temp.Columns(12).ColumnName = "Pokok_Terhutang"
                            table_oustanding_temp.Columns(13).ColumnName = "Bunga_Terhutang"
                            table_oustanding_temp.Columns(14).ColumnName = "StartDuedate"
                            table_oustanding_temp.Columns(15).ColumnName = "EndDuedate"
                            table_oustanding_temp.Columns(16).ColumnName = "Duration"
                            table_oustanding_temp.AcceptChanges()
                            'load data sort by DESC for get interest amount
                            table_oustanding = createNewDataOstanding(table_oustanding_temp)
                            table_oustanding.DefaultView.Sort = "[" + table_oustanding.Columns(3).ColumnName + "] DESC"
                            table_oustanding = table_oustanding.DefaultView.ToTable()
                            table_oustanding.AcceptChanges()
                            'create group by sum principal and interest
                            groupbyDataOstanding(table_oustanding)
                        End If
                    Else
                        'for early prosess
                        table_oustanding_temp = ds.Tables(0)
                    End If

                    'close conenction 
                    conn.Close()
                ElseIf extStr = ".txt" Then
                    '(2) reader txt to Datatable
                    Dim SR As StreamReader = New StreamReader(strFile)
                    Dim line As String = SR.ReadLine()
                    Dim strArray As String() = line.Split("|"c)
                    Dim row As DataRow
                    'Call Header Loan Column
                    For Each s As String In strArray
                        table_oustanding_temp.Columns.Add(New DataColumn())
                    Next
                    Do
                        line = SR.ReadLine
                        If Not line = String.Empty Then
                            row = table_oustanding_temp.NewRow()
                            row.ItemArray = line.Split("|"c)
                            table_oustanding_temp.Rows.Add(row)
                        Else
                            Exit Do
                        End If
                    Loop

                    If cb_payment.SelectedIndex = 1 Then
                        If table_oustanding_temp.Rows.Count > 0 Then
                            'for reguler prosess
                            'change column name by index
                            table_oustanding_temp.Columns(3).ColumnName = "No_Rekening"
                            table_oustanding_temp.Columns(12).ColumnName = "Pokok_Terhutang"
                            table_oustanding_temp.Columns(13).ColumnName = "Bunga_Terhutang"
                            table_oustanding_temp.Columns(14).ColumnName = "StartDuedate"
                            table_oustanding_temp.Columns(15).ColumnName = "EndDuedate"
                            table_oustanding_temp.Columns(16).ColumnName = "Duration"
                            table_oustanding_temp.AcceptChanges()
                            'load data sort by DESC for get interest amount
                            table_oustanding = createNewDataOstanding(table_oustanding_temp)
                            table_oustanding.DefaultView.Sort = "[" + table_oustanding.Columns(3).ColumnName + "] DESC"
                            table_oustanding = table_oustanding.DefaultView.ToTable()
                            table_oustanding.AcceptChanges()
                            'create group by sum principal and interest
                            groupbyDataOstanding(table_oustanding)
                        End If
                    End If

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            'alert after upload
            If Not table_oustanding_temp Is Nothing Then
                C_data1.Text = "(OK)"
            Else
                C_data1.Text = "(False)"
            End If
        End Try
    End Sub
    Protected Function groupbyDataReguler(ByRef table As DataTable) As DataTable
        'groub by no rekening
        If table_payment_group.Columns.Count <= 0 Then
            table_payment_group.Columns.Add("shbi acct no", GetType(String))
            table_payment_group.Columns.Add("repayment principal", GetType(Double))
            table_payment_group.Columns.Add("repayment interest", GetType(Double))
        End If
        Dim groupByQuery = From row In table.AsEnumerable()
                           Group row By Name = New With {
             Key .shbi_acct_no = row("shbi acct no")
             } Into NameGroup = Group
                           Select New With {
             .shbi_acct_no = Name.shbi_acct_no,
             .repayment_principal = NameGroup.Sum(Function(r) r("repayment principal")),
             .repayment_interest = NameGroup.Sum(Function(r) r("repayment interest"))
         }
        Dim val
        For Each val In groupByQuery
            'add new rows data after group (sum)
            table_payment_group.Rows.Add(val.shbi_acct_no, val.repayment_principal, val.repayment_interest)
        Next
        Return table_payment_group
    End Function
    Protected Function groupbyDataOstanding(ByRef table As DataTable) As DataTable
        'groub by no rekening
        table_ostanding_group.Columns.Add("No_Rekening", GetType(String))
        table_ostanding_group.Columns.Add("Pokok_Terhutang", GetType(Double))
        'table_ostanding_group.Columns.Add("Bunga_Terhutang", GetType(Double))
        Dim groupByQuery = From row In table.AsEnumerable()
                           Group row By Name = New With {
             Key .No_Rekening = row("No_Rekening")
             } Into NameGroup = Group
                           Select New With {
             .No_Rekening = Name.No_Rekening,
             .Pokok_Terhutang = NameGroup.Sum(Function(r) r("Pokok_Terhutang"))
         }
        Dim val
        For Each val In groupByQuery
            'add new rows data after group (sum)
            table_ostanding_group.Rows.Add(val.No_Rekening, val.Pokok_Terhutang)
        Next
        Return table_ostanding_group
    End Function
    Protected Function createNewDataOstanding(ByRef table As DataTable) As DataTable
        'creat new datable ostanding
        table_oustanding.Columns.Add("No_Rekening", GetType(String))
        table_oustanding.Columns.Add("Pokok_Terhutang", GetType(Double))
        table_oustanding.Columns.Add("Bunga_Terhutang", GetType(Double))
        table_oustanding.Columns.Add("N_Days", GetType(Integer))
        table_oustanding.Columns.Add("Duedate", GetType(Date))
        table_oustanding.Columns.Add("StartDuedate", GetType(Date))
        table_oustanding.Columns.Add("EndDuedate", GetType(Date))
        Dim str_duedate, str_startdate, str_enddate As String
        'create new table row
        For x As Integer = 0 To table.Rows.Count - 1
            Dim reNoRekening As String
            Dim reInterest, rePrincipal, n_days As String

            '-------NeW data row-------
            rowTemp = table_oustanding.NewRow()
            table_oustanding.Rows.Add(rowTemp)
            'cleansing no rekening
            If Not String.IsNullOrWhiteSpace(table.Rows(x).Item(3).ToString()) Then
                reNoRekening = table.Rows(x).Item(3)
                reNoRekening = reNoRekening.Replace("-", "")
            Else
                reNoRekening = ""
            End If

            table_oustanding.Rows(x).Item(0) = reNoRekening
            'cleansing column pokok and bunga 
            Dim arrSymbol() As String = {".", ",00"}
            'cleansing principal and bunga
            rePrincipal = table.Rows(x).Item(12)
            reInterest = table.Rows(x).Item(13)
            n_days = table.Rows(x).Item(16)
            For i As Integer = 0 To arrSymbol.Length - 1
                rePrincipal = Replace(rePrincipal, arrSymbol(i), "")
            Next i
            table_oustanding.Rows(x).Item(1) = Convert.ToDouble(Trim(rePrincipal))

            For i As Integer = 0 To arrSymbol.Length - 1
                reInterest = Replace(reInterest, arrSymbol(i), "")
            Next i
            table_oustanding.Rows(x).Item(2) = Convert.ToDouble(reInterest)
            'column days
            table_oustanding.Rows(x).Item(3) = Convert.ToInt32(n_days)
            'convert date to datetime
            '20220322 -> 17-03-2022
            Dim getYear1 As String = Strings.Right(Replace(table.Rows(x).Item(0).ToString(), "-", ""), 4)
            Dim getMonth1 As String = Strings.Mid(Replace(table.Rows(x).Item(0).ToString(), "-", ""), 3, 2)
            Dim getDay1 As String = Strings.Left(Replace(table.Rows(x).Item(0).ToString(), "-", ""), 2)

            Dim getYear2 As String = Strings.Right(Replace(table.Rows(x).Item(14).ToString(), "-", ""), 4)
            Dim getMonth2 As String = Strings.Mid(Replace(table.Rows(x).Item(14).ToString(), "-", ""), 3, 2)
            Dim getDay2 As String = Strings.Left(Replace(table.Rows(x).Item(14).ToString(), "-", ""), 2)

            Dim getYear3 As String = Strings.Right(Replace(table.Rows(x).Item(15).ToString(), "-", ""), 4)
            Dim getMonth3 As String = Strings.Mid(Replace(table.Rows(x).Item(15).ToString(), "-", ""), 3, 2)
            Dim getDay3 As String = Strings.Left(Replace(table.Rows(x).Item(15).ToString(), "-", ""), 2)

            str_duedate = getYear1 + "-" + getMonth1 + "-" + getDay1
            str_startdate = getYear2 + "-" + getMonth2 + "-" + getDay2
            str_enddate = getYear3 + "-" + getMonth3 + "-" + getDay3
            'column Duedate
            table_oustanding.Rows(x).Item(4) = Convert.ToDateTime(str_duedate)
            'column StartDuedate
            table_oustanding.Rows(x).Item(5) = Convert.ToDateTime(str_startdate)
            'column EndDuedate
            table_oustanding.Rows(x).Item(6) = Convert.ToDateTime(str_enddate)

        Next
        Return table_oustanding
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

    Private Sub btn_checking_Click(sender As Object, e As EventArgs) Handles btn_checking.Click

        ' Try
        '--- DECLRARE VARIABEL ----
        Dim multifinance_acc, shbi_acct_no, flag, auto_acc_no, str_total_balanced As String
            Dim str_date_excute, str_date_payment, str_last_payment_date As String
            Dim date_payment, date_excute, os_duedate As DateTime
            Dim interest_mounth, remain_tenor, max_tenor, n_tenor, tb_tenor, n_overdue_days As Integer
            Dim process As String
            Dim n_process As Boolean
            Dim timeStart As Date = Convert.ToDateTime(DateTime.Now)
            Dim spanTime As TimeSpan
            '--- DECLRARE VARIABEL COUNTING PMT AND CHECK PAYMENT----
            Dim table_pmt As New DataTable
            Dim checkRowOstanding, checkRowRegulerGroupAmount, checkRowOstandingGroupAmount, checkRowDuedate As DataRow
            Dim reason_output, full_name, final_FullName As String
            Dim n_interest, n_principal, n_sum_amount, n_sum_interest, n_sum_principal As Double
        Dim tb_principal, tb_interest, tb_amount, coa_balance_remain As Double
        Dim s_principal, s_interest, s_amount As Double
            Dim s_balanced, s_balanced_COA, total_balanced As Double
            Dim coa_balance, n_amount_loan, interest_product, sum_interest, sum_principal, sisa_interest, sum_interest_daily As Double
            Dim countRowsOstanding As Integer
            Dim table_hist_balanced As New DataTable()
        Dim resultFilterDT, resultFilterCOA As DataRow
        Dim resultFilterRegularOverdue As DataRow()
        Dim checkEarly As DataTable
            Dim strcheckDate_Disbure As String
        Dim dt_principal, dt_interest_daily, dt_interest, repayment_file_amount, total_selisih_amount, coa_debet As Double
        Dim checkRegulerProcess As DataRow
            Dim pesan As MsgBoxResult
            checkEarly = Nothing
        checkRegulerProcess = Nothing
        resultFilterRegularOverdue = Nothing

        'excute program
        If cb_payment.SelectedIndex.ToString = 0 Then
            MessageBox.Show("Please, Choose a payment.!", "Alert")
        ElseIf cb_product.SelectedIndex.ToString = 0 Then
            MessageBox.Show("Please, Choose a product.!", "Alert")
        Else
            'process data
            pesan = MsgBox("Checking Start?", MsgBoxStyle.YesNo, "Alert")
            If pesan = MsgBoxResult.Yes Then
                table_temp.Reset()
                'process payment checking
                'copy column to datatable table temp
                If table_temp.Rows.Count <= 0 Then
                    'selecting column for repayment type
                    If cb_payment.SelectedIndex = 7 Then
                        'add new column output process
                        table_temp.Columns.Add("SHBI Account")
                        table_temp.Columns.Add("Principal Amount")
                        table_temp.Columns.Add("Interest Amount")
                        table_temp.Columns.Add("Repayment Amount")
                        table_temp.Columns.Add("Auto Transfer Acno")
                        table_temp.Columns.Add("COA Balance")
                        table_temp.Columns.Add("COA Debet")
                        table_temp.Columns.Add("COA Balance Remain")
                        table_temp.Columns.Add("Overdue Month")
                        table_temp.Columns.Add("Repayment Bussines Date")
                        table_temp.Columns.Add("Status")
                    Else
                        For j = 0 To table_payment.Columns.Count - 1
                            table_temp.Columns.Add(table_payment.Columns(j).ColumnName)
                        Next
                        'add new column output process
                        table_temp.Columns.Add("Selisih Principal")
                        table_temp.Columns.Add("Selisih Interest")
                        table_temp.Columns.Add("Selisih Amount")
                        table_temp.Columns.Add("Remain Interest")
                        table_temp.Columns.Add("Interest Daily")
                        table_temp.Columns.Add("Overdue Days")
                        table_temp.Columns.Add("Ostanding DT")
                        table_temp.Columns.Add("Repayment Amount File")
                        table_temp.Columns.Add("COA Balance")
                        table_temp.Columns.Add("(COA Balance+Selisih Amount)")
                        table_temp.Columns.Add("COA Debet")
                        table_temp.Columns.Add("Last Payment Date")
                        table_temp.Columns.Add("Flag")
                        table_temp.Columns.Add("Reason")
                        table_temp.Columns.Add("Status")
                        table_temp.Columns.Add("SHBI Account")
                    End If

                End If

                'especially process for regular payment overdue
                If cb_payment.SelectedIndex = 7 Then
                    'calculation payment overdue for split repayment because file is duplicate account
                    'reguler payment amount after group by amount 
                    For k As Integer = 0 To table_payment.Rows.Count - 1
                        'shbi account form payment
                        Dim n_month_overdue As Integer
                        Dim n_rows As DataRow
                        Dim symbol, name_product As String
                        n_month_overdue = 0
                        coa_balance = 0
                        tb_principal = 0
                        tb_interest = 0
                        tb_amount = 0
                        coa_debet = 0
                        coa_balance_remain = 0
                        str_date_payment = ""
                        reason_output = ""
                        shbi_acct_no = table_payment.Rows(k).Item(0).ToString
                        coa_balance = Convert.ToDouble(table_payment.Rows(k).Item(7).ToString)
                        'table_temp.Columns.Add("SHBI Account")
                        'table_temp.Columns.Add("Principal Amount")
                        'table_temp.Columns.Add("Interest Amount")
                        'table_temp.Columns.Add("Repayment Amount")
                        'table_temp.Columns.Add("Auto Transfer Acno")
                        'table_temp.Columns.Add("COA Balance")
                        'table_temp.Columns.Add("COA Debet")
                        'table_temp.Columns.Add("COA Balance Remain")
                        'table_temp.Columns.Add("Overdue Month")
                        'table_temp.Columns.Add("Repayment Bussines Date")
                        'table_temp.Columns.Add("Status")
                        resultFilterRegularOverdue = table_payment_group.Select("[" + table_payment_group.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "'")
                        If Not resultFilterRegularOverdue Is Nothing Then
                            'checking payment for coa balance is enough or not enough
                            'table payment group : duplicate
                            'table payment : no duplicate
                            rowTemp = table_temp.NewRow()
                            table_temp.Rows.Add(rowTemp)
                            'add new rows for table temp
                            table_temp.Rows(k).Item(0) = shbi_acct_no.ToString
                            Console.WriteLine("SHBI Account {0}", shbi_acct_no)
                            Console.WriteLine("Account {0}", resultFilterRegularOverdue.Length)
                            Console.WriteLine("Coa Balance {0}", coa_balance)
                            'checking and calculation amount for payment balance from COA
                            coa_balance_remain = coa_balance
                            For Each n_rows In resultFilterRegularOverdue

                                If coa_balance_remain >= tb_amount Then
                                    n_month_overdue += 1
                                    'checking amount enough or not
                                    If coa_balance_remain >= tb_amount Then
                                        str_date_payment = Convert.ToDateTime(n_rows.Item(3)).ToString("dd-MM-yyyy")
                                        coa_balance_remain -= Convert.ToDouble(n_rows.Item(6))
                                    End If
                                    'checking remain coa balance after looping amount
                                    tb_principal += Convert.ToDouble(n_rows.Item(4))
                                    tb_interest += Convert.ToDouble(n_rows.Item(5))
                                    tb_amount = tb_principal + tb_interest
                                Else
                                    n_month_overdue += 0
                                    GoTo ProcessFinish
                                End If

                            Next
ProcessFinish:
                            tb_amount = tb_principal + tb_interest
                            'Console.WriteLine("Processing total amount {0}", tb_amount)
                            'Console.WriteLine("Processing n {0}", n_month_overdue)
                            'Console.WriteLine("Processing remain amount {0}", coa_balance_remain)
                        End If
                        'check reason from calculation 
                        If n_month_overdue > 0 Then
                            reason_output = "Balance Enough"
                        Else
                            reason_output = "Balance Not Enough"
                        End If

                        'get account in table
                        'symbol = Strings.Left(cb_product.Text.ToString, 4)
                        'Console.WriteLine(symbol)
                        'name_product = Replace(cb_product.Text.ToString, symbol, "")
                        table_temp.Rows(k).Item(1) = tb_principal.ToString
                        table_temp.Rows(k).Item(2) = tb_interest.ToString
                        table_temp.Rows(k).Item(3) = tb_amount.ToString

                        table_temp.Rows(k).Item(4) = getNoAccountsType("REPAYMENT", shbi_acct_no.ToString).ToString()
                        table_temp.Rows(k).Item(5) = coa_balance.ToString
                        table_temp.Rows(k).Item(6) = tb_amount.ToString
                        table_temp.Rows(k).Item(7) = coa_balance_remain.ToString
                        table_temp.Rows(k).Item(8) = n_month_overdue.ToString
                        table_temp.Rows(k).Item(9) = str_date_payment.ToString
                        table_temp.Rows(k).Item(10) = reason_output.ToString


                    Next
                    'checking payment amount for COA Balance and ostanding 

                Else

                    'copy column to datatable table process
                    If table_process.Rows.Count <= 0 Then
                        For j = 0 To table_payment.Columns.Count - 1
                            table_process.Columns.Add(table_payment.Columns(j).ColumnName)
                        Next
                    End If
                    'copy column to datatable table not process
                    If table_not_process.Rows.Count <= 0 Then
                        For j = 0 To table_temp.Columns.Count - 2
                            table_not_process.Columns.Add(table_temp.Columns(j).ColumnName)
                        Next
                    End If
                    'date now
                    Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                    getdate.ToString("ddMMyyyyHHmmss")
                    Dim strdate As String = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd")

                    'create new table row
                    For x As Integer = 0 To table_payment.Rows.Count - 1
                        'INISIALISASI VARIABEL IN DEFAULT VALUE
                        countRowsOstanding = 0
                        tb_interest = 0
                        tb_principal = 0
                        tb_amount = 0
                        n_principal = 0
                        n_interest = 0
                        total_balanced = 0
                        n_sum_amount = 0
                        s_amount = 0
                        s_balanced = 0
                        sum_interest = 0
                        s_interest = 0
                        s_principal = 0
                        n_sum_interest = 0
                        n_sum_principal = 0
                        interest_product = 0
                        n_amount_loan = 0
                        n_tenor = 0
                        max_tenor = 0
                        remain_tenor = 0
                        n_process = True
                        sum_interest_daily = 0
                        dt_interest_daily = 0
                        dt_interest = 0
                        sum_principal = 0
                        str_last_payment_date = ""
                        sum_interest = 0
                        repayment_file_amount = 0
                        total_selisih_amount = 0
                        coa_debet = 0
                        n_overdue_days = 0
                        process = ""
                        flag = ""
                        str_date_payment = ""
                        str_total_balanced = ""
                        strcheckDate_Disbure = ""
                        final_FullName = ""
                        reason_output = ""
                        full_name = ""
                        auto_acc_no = ""
                        arrWords.Clear()
                        table_pmt.Rows.Clear()
                        'add new rows
                        rowTemp = table_temp.NewRow()
                        table_temp.Rows.Add(rowTemp)

                        'set value for checking
                        multifinance_acc = Trim(table_payment.Rows(x).Item(5).ToString())
                        shbi_acct_no = Trim(table_payment.Rows(x).Item(8).ToString())
                        remain_tenor = Convert.ToInt32(table_payment.Rows(x).Item(13).ToString())
                        n_tenor = Convert.ToInt32(table_payment.Rows(x).Item(14).ToString())

                        'date job this payment
                        str_date_payment = Trim(table_payment.Rows(x).Item(0).ToString())
                        str_date_excute = Trim(table_payment.Rows(x).Item(1).ToString())
                        'get trx type payment 
                        auto_acc_no = Trim(table_payment.Rows(x).Item(32).ToString())

                        'convert date to datetime
                        Dim getYear2 As String = Strings.Left(str_date_excute, 4)
                        Dim getMonth2 As String = Strings.Mid(str_date_excute, 5, 2)
                        Dim getDay2 As String = Strings.Right(str_date_excute, 2)

                        Dim getYear1 As String = Strings.Left(str_date_payment, 4)
                        Dim getMonth1 As String = Strings.Mid(str_date_payment, 5, 2)
                        Dim getDay1 As String = Strings.Right(str_date_payment, 2)

                        str_date_payment = getYear1 + "-" + getMonth1 + "-" + getDay1
                        str_date_excute = getYear2 + "-" + getMonth2 + "-" + getDay2
                        'convert to datetime
                        date_payment = Convert.ToDateTime(str_date_payment)
                        date_excute = Convert.ToDateTime(str_date_excute)

                        '-------------Send Data to Table Temp -----------------
                        '--------Result Checking Data Payment to Output--------
                        table_temp.Rows(x).Item(0) = table_payment.Rows(x).Item(0).ToString()
                        table_temp.Rows(x).Item(1) = table_payment.Rows(x).Item(1).ToString()
                        table_temp.Rows(x).Item(2) = table_payment.Rows(x).Item(2).ToString()
                        table_temp.Rows(x).Item(3) = table_payment.Rows(x).Item(3).ToString()
                        table_temp.Rows(x).Item(4) = table_payment.Rows(x).Item(4).ToString()
                        table_temp.Rows(x).Item(5) = table_payment.Rows(x).Item(5).ToString()
                        table_temp.Rows(x).Item(6) = table_payment.Rows(x).Item(6).ToString()
                        'filter long name in file : cut if more 30 character
                        full_name = Trim(table_payment.Rows(x).Item(7).ToString())
                        'filter name : cut name just 3 words
                        If Len(full_name) > 30 Then
                            wordsFullName = full_name.Split(New Char() {" "c})
                            For j As Integer = 0 To 1
                                arrWords.Add(wordsFullName(j).ToString())
                            Next
                            final_FullName = String.Join(" ", TryCast(arrWords.ToArray(GetType(String)), String()))
                            table_temp.Rows(x).Item(7) = final_FullName.ToString()
                        Else
                            table_temp.Rows(x).Item(7) = table_payment.Rows(x).Item(7).ToString()
                        End If

                        table_temp.Rows(x).Item(8) = table_payment.Rows(x).Item(8).ToString()
                        table_temp.Rows(x).Item(9) = table_payment.Rows(x).Item(9).ToString()
                        table_temp.Rows(x).Item(10) = table_payment.Rows(x).Item(10).ToString()

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
                        table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()

                        '--(0). CHECKING EARLY AND CANCELATION REPORT FROM DATABASE BEFORE PROCESS CHECKING PAYMMENT---
                        'data not found in ostanding file
                        'should check the date is overdue or not or range duedate is not matched or cancellation on last day
                        'check cancelatiion file
                        checkCancel = checkCancellationPayment(Trim(shbi_acct_no.ToString))
                        'check table cancellation is not found
                        If Not IsNothing(checkCancel) Then
                            'data found in cancelations transactions
                            'column total balanced
                            total_balanced = 0
                            'column flag
                            flag = "Cancel"
                            'column reason
                            reason_output = "Cancelled on " + checkCancel.Rows(0).Item(0).ToString()
                            'coumn status
                            process = "Not Processed"
                            'column save balanced in database if amount is more
                            'not process
                            n_process = False
                            str_total_balanced = total_balanced.ToString
                            'tenor and remain tenor from file
                            remain_tenor = table_payment.Rows(x).Item(13).ToString()
                            n_tenor = table_payment.Rows(x).Item(14).ToString()
                            'column repayment principal
                            table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                            'column repayment interest
                            table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                            'column overdue
                            table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                            'column repayment sum amount
                            table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()
                            'process status 
                            load_payment.Rows(x).Cells(0).Value = process.ToString
                            'change color 
                            load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                            load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                        Else
                            'for checking early database
                            checkEarly = checkEarlyPayment(Trim(shbi_acct_no.ToString))
                            If Not checkEarly Is Nothing Then
                                'selisih principal
                                s_principal = 0
                                'selisih interest
                                s_interest = 0
                                'amount payment this month
                                tb_amount = 0
                                'column balanced
                                s_balanced = 0
                                'total interest
                                n_sum_interest = 0
                                'total balance 
                                total_balanced = 0
                                'column flag
                                flag = "Paid"
                                'column reason
                                reason_output = checkEarly.Rows(0).Item(2).ToString() + " on " + checkEarly.Rows(0).Item(0).ToString()
                                'coumn status
                                process = "Not Processed"
                                'not process
                                n_process = False
                                str_total_balanced = total_balanced.ToString
                                'tenor and remain tenor from file
                                remain_tenor = table_payment.Rows(x).Item(13).ToString()
                                n_tenor = table_payment.Rows(x).Item(14).ToString()
                                'column repayment principal
                                table_temp.Rows(x).Item(28) = table_payment.Rows(x).Item(28).ToString()
                                'column repayment interest
                                table_temp.Rows(x).Item(29) = table_payment.Rows(x).Item(29).ToString()
                                'column overdue
                                table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                'column repayment sum amount
                                table_temp.Rows(x).Item(31) = table_payment.Rows(x).Item(31).ToString()
                                load_payment.Rows(x).Cells(0).Value = process.ToString
                                'change color 
                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                            Else
                                'NOT FOUND FOR EARLY PAYMENT AND CHECKING PAYMENT PROCESS
                                '---(1). REGULER PAYMENT -------
                                If cb_payment.SelectedIndex.ToString = 1 Then
                                    If Not IsNothing(table_payment_group) Then
                                        'reguler payment amount after group by amount 
                                        checkRowRegulerGroupAmount = table_payment_group.Select("[" + table_payment_group.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "'").FirstOrDefault()
                                        'check row regulergroupAmount is not nothing
                                        If Not IsNothing(checkRowRegulerGroupAmount) Then
                                            n_principal = Convert.ToDouble(checkRowRegulerGroupAmount.Item(1).ToString)
                                            n_interest = Convert.ToDouble(checkRowRegulerGroupAmount.Item(2).ToString)
                                            'get sum amount
                                            n_sum_amount = (n_principal + n_interest)
                                        Else
                                            n_principal = 0
                                            n_interest = 0
                                            n_sum_amount = 0
                                        End If
                                        '----CHECKING REGULER PAYMENT IN LOAN OSTANDING (MAAPING BY SHBI ACC NO)----
                                        'check data reguler in ostanding file : principal
                                        checkRowOstanding = table_oustanding.Select("[" + table_oustanding.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "'").FirstOrDefault()
                                        'check rows after filtering table ostanding
                                        If Not IsNothing(checkRowOstanding) Then
                                            countRowsOstanding = 0
                                            'looping to check overdue : if rows more than 1 is overdue amount
                                            For z As Integer = 0 To table_oustanding.Rows.Count - 1
                                                If table_oustanding.Rows(z).Item(0).ToString = shbi_acct_no.ToString Then
                                                    countRowsOstanding += 1
                                                End If
                                            Next
                                            'get data first rows for interest in amount ostanding file
                                            tb_interest = Convert.ToDouble(checkRowOstanding.Item(2).ToString)
                                        End If
                                        'process data ostandings
                                        If Not IsNothing(checkRowOstanding) Then
                                            'data more 1 data : data duplicate is overdue
                                            checkRowOstandingGroupAmount = table_ostanding_group.Select("[" + table_ostanding_group.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "'").FirstOrDefault()
                                            'process and maaping in reguler file and ostanding file : for principal
                                            If Not IsNothing(checkRowRegulerGroupAmount) Then
                                                tb_principal = Convert.ToDouble(checkRowOstandingGroupAmount.Item(1).ToString)
                                                'get sum amount
                                                tb_amount = (tb_principal + tb_interest)
                                            Else
                                                tb_interest = 0
                                                tb_principal = 0
                                                tb_amount = 0
                                            End If
                                            'Calculations Payment (Reguler file and ostanding)
                                            s_interest = (n_interest - tb_interest)
                                            s_principal = (n_principal - tb_principal)
                                            s_amount = (n_sum_amount - tb_amount)
                                            s_balanced = s_amount
                                            total_selisih_amount = s_balanced
                                            'Check condition balanced after calculations payment
                                            If s_balanced = 0 Then
                                                ' balanced is enough
                                                'check data overdue in ostanding 1410 file
                                                If countRowsOstanding = 2 Then
                                                    reason_output = "Overdue " + countRowsOstanding.ToString() + " Month (" + checkRowOstanding.Item(3).ToString + "Day)"
                                                    process = "Processed"
                                                    n_overdue_days = checkRowOstanding.Item(3)
                                                    'condition type allow partial to save balance
                                                    flag = "OK"
                                                    total_balanced = 0
                                                ElseIf countRowsOstanding = 1 Then
                                                    'Not Overdue but proccessed 
                                                    process = "Processed"
                                                    flag = "OK"
                                                    n_process = False
                                                    total_balanced = 0
                                                ElseIf countRowsOstanding > 2 Then
                                                    'not proccessed must buyback
                                                    process = "Processed"
                                                    reason_output = "Overdue " + countRowsOstanding.ToString() + " Month (" + checkRowOstanding.Item(3).ToString + "Day)"
                                                    n_overdue_days = checkRowOstanding.Item(3)
                                                    flag = "OK"
                                                    total_balanced = 0
                                                    n_process = False
                                                End If
                                                reason_output = ""
                                                'not data in total balanced DB
                                                str_total_balanced = total_balanced.ToString
                                                'send data ostanding payment to table temp
                                                'column repayment principal ostanding payment
                                                table_temp.Rows(x).Item(28) = tb_principal.ToString()
                                                'column repayment interest ostanding payment
                                                table_temp.Rows(x).Item(29) = tb_interest.ToString()
                                                'column overdue
                                                table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                                'column repayment sum amount ostanding payment
                                                table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.LightGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            ElseIf s_balanced < 0 Then
                                                'check amount in total balanced
                                                ' data is not found in 
                                                If countRowsOstanding = 2 Then
                                                    reason_output = "Overdue " + countRowsOstanding.ToString() + " Month (" + checkRowOstanding.Item(3).ToString + "Day)"
                                                    process = "Not Processed"
                                                    n_overdue_days = checkRowOstanding.Item(3)
                                                    flag = "Buyback"
                                                    total_balanced = 0
                                                ElseIf countRowsOstanding = 1 Then
                                                    'Not Overdue but proccessed 
                                                    process = "Not Processed"
                                                    flag = "Takeout"
                                                    reason_output = "Balance Not Enough"
                                                ElseIf countRowsOstanding > 2 Then
                                                    'Not Overdue but proccessed 
                                                    reason_output = "Overdue " + countRowsOstanding.ToString() + " Month (" + checkRowOstanding.Item(3).ToString + "Day)"
                                                    process = "Not Processed"
                                                    n_overdue_days = checkRowOstanding.Item(3)
                                                    flag = "Buyback"
                                                    total_balanced = 0
                                                End If
                                                n_process = False
                                                str_total_balanced = total_balanced.ToString
                                                'send data ostanding payment to table temp
                                                'column repayment principal ostanding payment
                                                table_temp.Rows(x).Item(28) = tb_principal.ToString()
                                                'column repayment interest ostanding payment
                                                table_temp.Rows(x).Item(29) = tb_interest.ToString()
                                                'column overdue
                                                table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                                'column repayment sum amount ostanding payment
                                                table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White

                                            ElseIf s_balanced > 0 And s_balanced <= 1000 Then
                                                'balanced is enough
                                                'check data overdue in ostanding 1410 file
                                                If countRowsOstanding > 1 Then
                                                    flag = "Overdue"
                                                    reason_output = "Overdue " + countRowsOstanding.ToString() + " Month (" + checkRowOstanding.Item(3).ToString + "Day)"
                                                    n_overdue_days = checkRowOstanding.Item(3)
                                                    process = "Processed"
                                                ElseIf countRowsOstanding = 1 Then
                                                    'Not Overdue but proccessed 
                                                    process = "Processed"
                                                    flag = "OK"
                                                    reason_output = ""
                                                End If
                                                total_balanced = 0
                                                str_total_balanced = total_balanced.ToString()
                                                'send data ostanding payment to table temp
                                                'column repayment principal ostanding payment
                                                table_temp.Rows(x).Item(28) = tb_principal.ToString()
                                                'column repayment interest ostanding payment
                                                table_temp.Rows(x).Item(29) = tb_interest.ToString()
                                                'column overdue
                                                table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                                'column repayment sum amount ostanding payment
                                                table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.LightGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            ElseIf s_balanced > 1000 Then
                                                'balanced is more 
                                                'maybe because overdue or more amount payment : (sisa) send to database
                                                'data found in oustanding table
                                                'check data overdue in ostanding 1410 file
                                                If countRowsOstanding > 1 Then
                                                    process = "Processed"
                                                    reason_output = "Overdue " + countRowsOstanding.ToString() + " Month (" + checkRowOstanding.Item(3).ToString + "Day)"
                                                    flag = "Overdue"
                                                    n_overdue_days = checkRowOstanding.Item(3)
                                                ElseIf countRowsOstanding = 1 Then
                                                    flag = "OK"
                                                    process = "Processed"
                                                    'Not Overdue but more balanced 
                                                End If
                                                'if not found in database total balanced
                                                total_balanced = total_balanced + s_balanced
                                                str_total_balanced = total_balanced.ToString
                                                'send data ostanding payment to table temp
                                                'column repayment principal ostanding payment
                                                table_temp.Rows(x).Item(28) = tb_principal.ToString()
                                                'column repayment interest ostanding payment
                                                table_temp.Rows(x).Item(29) = tb_interest.ToString()
                                                'column overdue
                                                table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                                'column repayment sum amount ostanding payment
                                                table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.LightGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            End If
                                        Else
                                            'data not found in ostanding file
                                            'not ostanding found
                                            process = "Not Processed"
                                            reason_output = "Ostanding Not Found"
                                            flag = "Takeout"
                                            str_total_balanced = total_balanced.ToString
                                            'column repayment principal ostanding payment
                                            table_temp.Rows(x).Item(28) = n_principal.ToString()
                                            'column repayment interest ostanding payment
                                            table_temp.Rows(x).Item(29) = n_interest.ToString()
                                            'column overdue
                                            table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                            'column repayment sum amount ostanding payment
                                            table_temp.Rows(x).Item(31) = n_sum_amount.ToString()
                                            'change color 
                                            load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                            load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                        End If
                                        'output
                                        'tenor and remain tenor from file
                                        remain_tenor = table_payment.Rows(x).Item(13).ToString()
                                        n_tenor = table_payment.Rows(x).Item(14).ToString()
                                        'status from any account
                                        load_payment.Rows(x).Cells(0).Value = process.ToString()
                                    End If
                                ElseIf cb_payment.SelectedIndex.ToString = 2 Then
                                    'Regular Payment With DT or 9850 query
                                    '---(4). REGULAR PAYMENT WITH DT OR 9850 QUERY -------
                                    'proses regular payment without calculation
                                    resultFilterDT = table_DT.Select("[" + table_DT.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                    If Not resultFilterDT Is Nothing Then
                                        'get data from DT or 9850 query
                                        'amount loan
                                        dt_principal = Convert.ToDouble(resultFilterDT.Item(4).ToString)
                                        n_overdue_days = Convert.ToDouble(resultFilterDT.Item(9).ToString)
                                        str_last_payment_date = Convert.ToString(resultFilterDT.Item(3).ToString)
                                        n_tenor = Convert.ToInt32(resultFilterDT.Item(10).ToString)
                                        max_tenor = Convert.ToInt32(resultFilterDT.Item(8).ToString())

                                        'get data n_ interest & principal fil early
                                        n_principal = Convert.ToDouble(Trim(table_payment.Rows(x).Item(28).ToString()))
                                        n_interest = Convert.ToDouble(Trim(table_payment.Rows(x).Item(29).ToString()))
                                        n_sum_amount = n_principal + n_interest
                                        'repayment amount from file 
                                        repayment_file_amount = n_sum_amount
                                        'get interest full from n tenor from DT to calculation PMT
                                        'remain count 
                                        remain_tenor = max_tenor - n_tenor
                                        n_amount_loan = Convert.ToDouble(resultFilterDT.Item(6).ToString)
                                        'dd-mm-yyyy : date dirsburse
                                        'yyyy-mm-dd
                                        strcheckDate_Disbure = Trim(resultFilterDT.Item(1).ToString)
                                        'convert date to datetime
                                        Dim getYear3 As String = Strings.Right(strcheckDate_Disbure, 4)
                                        Dim getMonth3 As String = Strings.Mid(strcheckDate_Disbure, 4, 2)
                                        Dim getDay3 As String = Strings.Left(strcheckDate_Disbure, 2)
                                        strcheckDate_Disbure = getYear3 + "-" + getMonth3 + "-" + getDay3
                                        'Add new column for n tenor 
                                        'bunga bulan/tahun,amount loan, max tenor, n_tenor
                                        ' Private Function ProcessPMT(ByRef interest_mounth As Integer, ByRef loan_amount As Double, ByRef max_tenor As Integer, ByRef n_tenor As Integer, ByRef dateDisburse As Date) As DataTable
                                        interest_product = getInterestProduct(Int(cb_product.SelectedIndex.ToString))
                                        table_pmt = ProcessPMT(interest_product, Convert.ToDouble(n_amount_loan), max_tenor, n_tenor, Convert.ToDateTime(strcheckDate_Disbure), date_payment)
                                        If table_pmt.Rows.Count > 0 Then
                                            If max_tenor = n_tenor Then
                                                sum_interest = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(9).ToString)
                                            Else
                                                'check principal and interest for regular payment
                                                sum_interest = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(9).ToString)

                                            End If
                                        Else
                                            sum_principal = 0
                                            sum_interest = 0
                                            sum_interest_daily = 0
                                        End If

                                        'check filter Nominal COA
                                        If Not tableCOABalance Is Nothing Then
                                            'get COA Balance From COA
                                            resultFilterCOA = tableCOABalance.Select("[" + tableCOABalance.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                            If Not resultFilterCOA Is Nothing Then
                                                If Not String.IsNullOrWhiteSpace(resultFilterCOA.Item(1).ToString) Then
                                                    total_balanced = Convert.ToDouble(resultFilterCOA.Item(1).ToString)
                                                Else
                                                    total_balanced = 0
                                                End If
                                            Else
                                                total_balanced = 0
                                            End If
                                        Else
                                            total_balanced = 0
                                        End If
                                        'Full OStanding Payment (Tersisa)
                                        sum_principal = dt_principal
                                        tb_amount = sum_principal + sum_interest

                                        'selisih principal amount file and ostanding principal
                                        s_principal = n_principal - sum_principal
                                        'selisih interest amount file and ostanding interest
                                        s_interest = n_interest - sum_interest

                                        'get information COA Enough or Not for Full OStanding
                                        s_balanced = (n_sum_amount - tb_amount)

                                        If s_balanced <= 0 Then
                                            'amount not enough and mor than nominal from OStanding so, please process 
                                            'but please check COA if plus and more than OStanding and please not process
                                            s_balanced_COA = total_balanced - tb_amount
                                            If s_balanced_COA >= 0 Then
                                                'please not save and running the payment because COA have enough amount for Full OStanding
                                                str_total_balanced = total_balanced.ToString
                                                'column flag
                                                flag = "Takeout"
                                                'coumn status
                                                process = "Not Processed"
                                                reason_output = "COA Balance Enough"
                                                n_process = False
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            Else
                                                str_total_balanced = total_balanced.ToString
                                                n_process = True
                                                'column flag
                                                flag = "OK"
                                                'coumn status
                                                process = "Processed"
                                                reason_output = "COA Balance Not Enough"
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            End If
                                        Else
                                            s_balanced_COA = total_balanced - tb_amount
                                            If s_balanced_COA >= 0 Then
                                                'please not save and running the payment because COA have enough amount for Full OStanding
                                                str_total_balanced = total_balanced.ToString
                                                'column flag
                                                flag = "Takeout"
                                                'coumn status
                                                process = "Not Processed"
                                                reason_output = "COA Balance Enough"
                                                n_process = False
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            Else
                                                str_total_balanced = total_balanced.ToString
                                                n_process = True
                                                'column flag
                                                flag = "OK"
                                                'coumn status
                                                process = "Processed"
                                                reason_output = "COA Balance Not Enough"
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            End If
                                        End If
                                        'round interest daily
                                        'get total (sisa) interest
                                        sisa_interest = 0
                                        'column repayment principal
                                        table_temp.Rows(x).Item(28) = n_principal.ToString()
                                        'column repayment interest
                                        table_temp.Rows(x).Item(29) = n_interest.ToString()
                                        'column overdue
                                        table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                        'column repayment sum amount
                                        table_temp.Rows(x).Item(31) = n_sum_amount.ToString()
                                        'status
                                        load_payment.Rows(x).Cells(0).Value = process.ToString

                                    Else
                                        'check filter Nominal COA
                                        If Not tableCOABalance Is Nothing Then
                                            'get COA Balance From COA
                                            resultFilterCOA = tableCOABalance.Select("[" + tableCOABalance.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                            If Not resultFilterCOA Is Nothing Then
                                                If Not String.IsNullOrWhiteSpace(resultFilterCOA.Item(1).ToString) Then
                                                    total_balanced = Convert.ToDouble(resultFilterCOA.Item(1).ToString)
                                                    sum_interest = 0
                                                Else
                                                    total_balanced = 0
                                                    sum_interest = 0
                                                End If
                                            Else
                                                total_balanced = 0
                                                sum_interest = 0
                                            End If
                                        Else
                                            total_balanced = 0
                                            sum_interest = 0
                                        End If
                                        'tidak ada data
                                        n_tenor = Convert.ToInt32(table_payment.Rows(x).Item(14).ToString())
                                        remain_tenor = Convert.ToInt32(table_payment.Rows(x).Item(13).ToString())
                                        'amount loan
                                        'coumn status
                                        process = "Not Processed"
                                        reason_output = "Ostanding Not Found"
                                        'column flag
                                        flag = "Takeout"
                                        'not process
                                        n_process = False
                                        str_total_balanced = total_balanced
                                        'selisih COA with ostanding
                                        total_selisih_amount = total_balanced + s_balanced
                                        'column repayment principal
                                        table_temp.Rows(x).Item(28) = n_principal.ToString()
                                        'column repayment interest
                                        table_temp.Rows(x).Item(29) = n_interest.ToString()
                                        'column overdue
                                        table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                        'column repayment sum amount
                                        table_temp.Rows(x).Item(31) = n_sum_amount.ToString()
                                        load_payment.Rows(x).Cells(0).Value = process.ToString
                                        'change color 
                                        load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                        load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                    End If

                                ElseIf cb_payment.SelectedIndex.ToString = 3 Then
                                    '---(3). EARLY TERMINATION (PAYMENT) WITHOUT DT OR 9850 QUERY -------
                                    If Not IsNothing(table_payment) Then
                                        If Not String.IsNullOrWhiteSpace(Trim(shbi_acct_no.ToString)) Then
                                            'check proses reguler file (today) is not process if no acc is true
                                            checkRegulerProcess = table_oustanding_temp.Select("[" + table_oustanding_temp.Columns(8).ColumnName + "] ='" + shbi_acct_no.ToString + "'").FirstOrDefault()
                                            If IsNothing(checkRegulerProcess) Then
                                                'check table disbursement
                                                checkTableDisburse = checkDisbursement(Trim(shbi_acct_no.ToString))
                                                If Not IsNothing(checkTableDisburse) Then
                                                    'process max tenor
                                                    max_tenor = (n_tenor + remain_tenor)
                                                    'amount loan
                                                    n_amount_loan = checkTableDisburse.Rows(0).Item(6).ToString()
                                                    strcheckDate_Disbure = checkTableDisburse.Rows(0).Item(0).ToString()
                                                    'column flag
                                                    flag = "OK"
                                                    'coumn status
                                                    process = "Processed"
                                                    'get data n_ interest & principal fil early
                                                    n_principal = Convert.ToDouble(Trim(table_payment.Rows(x).Item(28).ToString()))
                                                    n_interest = Convert.ToDouble(Trim(table_payment.Rows(x).Item(29).ToString()))
                                                    n_sum_amount = n_principal + n_interest
                                                Else
                                                    '--data not found in disbursement--
                                                    'column repayment principal
                                                    n_principal = Convert.ToDouble(table_payment.Rows(x).Item(28).ToString)
                                                    'column repayment interest
                                                    n_interest = Convert.ToDouble(table_payment.Rows(x).Item(29).ToString)
                                                    'column overdue
                                                    n_sum_amount = (n_principal + n_interest)
                                                    'tenor
                                                    max_tenor = n_tenor + remain_tenor
                                                    'column total balanced
                                                    str_total_balanced = total_balanced
                                                    'overdue day
                                                    n_overdue_days = 0
                                                    'column flag
                                                    flag = "Takeout"
                                                    'coumn status
                                                    process = "Not Processed"
                                                    reason_output = "Ostanding Not Found"
                                                    sisa_interest = 0
                                                    'not process
                                                    n_process = False
                                                    'column repayment principal ostanding payment
                                                    table_temp.Rows(x).Item(28) = n_principal.ToString()
                                                    'column repayment interest ostanding payment
                                                    table_temp.Rows(x).Item(29) = n_interest.ToString()
                                                    'column overdue
                                                    table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                                    'column repayment sum amount ostanding payment
                                                    table_temp.Rows(x).Item(31) = n_sum_amount.ToString()
                                                    'column save balanced in database if amount is more
                                                    load_payment.Rows(x).Cells(0).Value = process.ToString
                                                    'change color 
                                                    load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                    load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                                End If
                                            Else
                                                'tenor
                                                max_tenor = n_tenor + remain_tenor
                                                'data found in reguler file (today)
                                                'after Process Calculations PMT with upload (Payment File)
                                                'column flag
                                                flag = "Reguler"
                                                'column reason
                                                reason_output = "Reguler Payment on " + date_payment.ToString("yyyyMMdd")
                                                'coumn status
                                                process = "Not Processed"
                                                n_overdue_days = 0
                                                'column save balanced in database if amount is more
                                                'not process
                                                n_process = False
                                                'column save balanced in database if amount is more
                                                load_payment.Rows(x).Cells(0).Value = process.ToString
                                                'column repayment principal ostanding payment
                                                table_temp.Rows(x).Item(28) = n_principal.ToString()
                                                'column repayment interest ostanding payment
                                                table_temp.Rows(x).Item(29) = n_interest.ToString()
                                                'sum repayment amount based file
                                                repayment_file_amount = n_sum_amount
                                                'column overdue
                                                table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                                'column repayment sum amount ostanding payment
                                                table_temp.Rows(x).Item(31) = n_sum_amount.ToString()
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            End If

                                            'Convert Date to datetime
                                            Dim getYear3 As String = Strings.Left(strcheckDate_Disbure, 4)
                                            Dim getMonth3 As String = Strings.Mid(strcheckDate_Disbure, 5, 2)
                                            Dim getDay3 As String = Strings.Right(strcheckDate_Disbure, 2)
                                            'convert string to date
                                            strcheckDate_Disbure = getYear3 + "-" + getMonth3 + "-" + getDay3

                                            'bunga bulan/tahun,amount loan, max tenor, n_tenor
                                            ' Private Function ProcessPMT(ByRef interest_mounth As Integer, ByRef loan_amount As Double, ByRef max_tenor As Integer, ByRef n_tenor As Integer, ByRef dateDisburse As Date) As DataTable

                                            interest_product = getInterestProduct(Int(cb_product.SelectedIndex.ToString))
                                            table_pmt = ProcessPMT(interest_product, Convert.ToDouble(n_amount_loan), max_tenor, n_tenor, Convert.ToDateTime(strcheckDate_Disbure), date_payment)
                                            '-----SUM TOTAL ALL ROWS FROM N TENOR FROM PAYMENT-----
                                            'set value data interest daily from calculations

                                            If table_pmt.Rows.Count > 0 Then
                                                If max_tenor = n_tenor Then
                                                    sum_principal = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(8).ToString)
                                                    sum_interest = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(9).ToString)
                                                    n_overdue_days = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(10).ToString)
                                                    sum_interest_daily = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(11).ToString)
                                                Else
                                                    'get data principal and interest for early 
                                                    'get sum calcultaion principal dan interest for early termination
                                                    sum_principal = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(8).ToString)
                                                    sum_interest = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(9).ToString)

                                                    For j = n_tenor - 1 To table_pmt.Rows.Count - 1
                                                        'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                                                        'Set value data table principal and interest
                                                        If Not String.IsNullOrWhiteSpace(table_pmt.Rows(j).Item(10).ToString) Then
                                                            n_overdue_days += Convert.ToDouble(table_pmt.Rows(j).Item(10).ToString)
                                                        End If
                                                        If Not String.IsNullOrWhiteSpace(table_pmt.Rows(j).Item(11).ToString) Then
                                                            sum_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(11).ToString)
                                                        End If
                                                    Next

                                                End If
                                            Else
                                                sum_principal = 0
                                                sum_interest = 0
                                                sum_interest_daily = 0
                                            End If

                                            'check filter Nominal COA Balance
                                            If Not tableCOABalance Is Nothing Then
                                                'get COA Balance From COA
                                                resultFilterCOA = tableCOABalance.Select("[" + tableCOABalance.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                                If Not resultFilterCOA Is Nothing Then
                                                    If Not String.IsNullOrWhiteSpace(resultFilterCOA.Item(1).ToString) Then
                                                        total_balanced = Convert.ToDouble(resultFilterCOA.Item(1).ToString)
                                                    Else
                                                        total_balanced = 0
                                                    End If
                                                Else
                                                    total_balanced = 0
                                                End If
                                            Else
                                                total_balanced = 0
                                            End If

                                            'amount payment this month
                                            tb_amount = (sum_principal + sum_interest)
                                            'column balanced
                                            s_balanced = (n_sum_amount - tb_amount)
                                            'please check COA for adding early termination
                                            s_balanced_COA = total_balanced + s_balanced
                                            'check balance for process early payment
                                            If s_balanced_COA >= 0 Then
                                                If s_balanced < 0 Then
                                                    'coumn status
                                                    process = "Processed  COA Balance"
                                                    coa_debet = (0 - s_balanced)
                                                Else
                                                    'coumn status
                                                    process = "Processed"
                                                    coa_debet = 0
                                                End If
                                                str_total_balanced = total_balanced.ToString
                                                reason_output = "Balance Enough"
                                                'column flag
                                                flag = "OK"
                                                n_process = True
                                                'repayment amount based on file
                                                repayment_file_amount = n_sum_amount
                                                'column save balanced in database if amount is more
                                                load_payment.Rows(x).Cells(0).Value = process.ToString
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            Else
                                                If s_balanced_COA >= -1000 AndAlso s_balanced_COA < 0 Then
                                                    'adjust repayment amount for if s_balance_COA is minus and need addtional amount from file repayment
                                                    repayment_file_amount = n_sum_amount - s_balanced_COA
                                                    coa_debet = total_balanced
                                                    'coumn status
                                                    process = "Processed with COA Balance"
                                                    reason_output = "Balance Enough"
                                                    'column flag
                                                    flag = "OK"
                                                    n_process = True
                                                    'coa balance from file
                                                    str_total_balanced = total_balanced.ToString
                                                    'column save balanced in database if amount is more
                                                    load_payment.Rows(x).Cells(0).Value = process.ToString
                                                    'change color 
                                                    load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                                    load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                                Else
                                                    total_balanced = total_balanced
                                                    str_total_balanced = total_balanced.ToString
                                                    'repayment amount based on file
                                                    repayment_file_amount = n_sum_amount
                                                    n_process = False
                                                    'coumn status
                                                    process = "Not Processed"
                                                    reason_output = "Balance Not Enough"
                                                    'column flag
                                                    flag = "Takeout"
                                                    'column save balanced in database if amount is more
                                                    load_payment.Rows(x).Cells(0).Value = process.ToString
                                                    'change color 
                                                    load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                    load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                                End If
                                            End If
                                            'round interest daily
                                            'get total (sisa) interest
                                            sisa_interest = (sum_interest - sum_interest_daily)
                                            'selisih principal
                                            s_principal = (n_principal - sum_principal)
                                            'selisih interest
                                            s_interest = (n_interest - sum_interest)
                                            'total interest
                                            n_sum_interest = sum_interest
                                            'selisih COA with ostanding
                                            total_selisih_amount = s_balanced_COA
                                            'tenor and remain tenor from file
                                            remain_tenor = table_payment.Rows(x).Item(13).ToString()
                                            n_tenor = table_payment.Rows(x).Item(14).ToString()
                                            'status from any account
                                            load_payment.Rows(x).Cells(0).Value = process.ToString()
                                            'column repayment principal
                                            table_temp.Rows(x).Item(28) = sum_principal.ToString()
                                            'column repayment interest
                                            table_temp.Rows(x).Item(29) = n_sum_interest.ToString()
                                            'column overdue
                                            table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                            'column repayment sum amount
                                            table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                        End If
                                    End If

                                ElseIf cb_payment.SelectedIndex.ToString = 4 Then
                                    '---(4). EARLY TERMINATION (PAYMENT) WITH DT OR 9850 QUERY -------
                                    'proses early termination calculation
                                    resultFilterDT = table_DT.Select("[" + table_DT.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                    If Not resultFilterDT Is Nothing Then
                                        'get data from DT or 9850 query
                                        n_tenor = Convert.ToInt32(resultFilterDT.Item(10).ToString)
                                        max_tenor = Convert.ToInt32(resultFilterDT.Item(8).ToString())
                                        If n_tenor > max_tenor Then
                                            n_tenor = max_tenor
                                        End If
                                        'remain count 
                                        remain_tenor = max_tenor - n_tenor
                                        'OStanding principal
                                        dt_principal = Convert.ToDouble(resultFilterDT.Item(4).ToString)
                                        dt_interest = Convert.ToDouble(resultFilterDT.Item(5).ToString)
                                        n_amount_loan = Convert.ToDouble(resultFilterDT.Item(6).ToString)
                                        dt_interest_daily = Convert.ToDouble(resultFilterDT.Item(7).ToString)
                                        'dd-mm-yyyy : date dirsburse
                                        'yyyy-mm-dd
                                        strcheckDate_Disbure = Trim(resultFilterDT.Item(1).ToString)
                                        str_last_payment_date = Convert.ToString(resultFilterDT.Item(3).ToString)
                                        'get data n_ interest & principal fil early
                                        n_principal = Convert.ToDouble(Trim(table_payment.Rows(x).Item(28).ToString()))
                                        n_interest = Convert.ToDouble(Trim(table_payment.Rows(x).Item(29).ToString()))
                                        n_sum_amount = n_principal + n_interest
                                        'convert date to datetime
                                        Dim getYear3 As String = Strings.Right(strcheckDate_Disbure, 4)
                                        Dim getMonth3 As String = Strings.Mid(strcheckDate_Disbure, 4, 2)
                                        Dim getDay3 As String = Strings.Left(strcheckDate_Disbure, 2)
                                        strcheckDate_Disbure = getYear3 + "-" + getMonth3 + "-" + getDay3
                                        'Add new column for n tenor 
                                        'bunga bulan/tahun,amount loan, max tenor, n_tenor
                                        ' Private Function ProcessPMT(ByRef interest_mounth As Integer, ByRef loan_amount As Double, ByRef max_tenor As Integer, ByRef n_tenor As Integer, ByRef dateDisburse As Date) As DataTable
                                        interest_product = getInterestProduct(Int(cb_product.SelectedIndex.ToString))
                                        table_pmt = ProcessPMT(interest_product, Convert.ToDouble(n_amount_loan), max_tenor, n_tenor, Convert.ToDateTime(strcheckDate_Disbure), date_payment)
                                        If table_pmt.Rows.Count > 0 Then
                                            If max_tenor = n_tenor Then
                                                sum_principal = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(8).ToString)
                                                sum_interest = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(9).ToString)
                                                n_overdue_days = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(10).ToString)
                                                sum_interest_daily = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(11).ToString)
                                            Else
                                                'check principal and interest for early payment and buyback
                                                sum_principal = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(8).ToString)
                                                sum_interest = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(9).ToString)
                                                For j = n_tenor - 1 To table_pmt.Rows.Count - 1
                                                    'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                                                    'Set value data table principal and interest
                                                    If Not String.IsNullOrWhiteSpace(table_pmt.Rows(j).Item(10).ToString) Then
                                                        n_overdue_days += Convert.ToDouble(table_pmt.Rows(j).Item(10).ToString)
                                                    End If
                                                    If Not String.IsNullOrWhiteSpace(table_pmt.Rows(j).Item(11).ToString) Then
                                                        sum_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(11).ToString)
                                                    End If
                                                Next
                                            End If
                                        Else
                                            sum_principal = 0
                                            sum_interest = 0
                                            sum_interest_daily = 0
                                        End If
                                        'check filter Nominal COA
                                        If Not tableCOABalance Is Nothing Then
                                            'get COA Balance From COA
                                            resultFilterCOA = tableCOABalance.Select("[" + tableCOABalance.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                            If Not resultFilterCOA Is Nothing Then
                                                If Not String.IsNullOrWhiteSpace(resultFilterCOA.Item(1).ToString) Then
                                                    total_balanced = Convert.ToDouble(resultFilterCOA.Item(1).ToString)
                                                Else
                                                    total_balanced = 0
                                                End If
                                            Else
                                                total_balanced = 0
                                            End If
                                        Else
                                            total_balanced = 0
                                        End If

                                        'check principal counting from PMT with table DT 
                                        'matching 
                                        If Double.Equals(sum_principal, dt_principal) Then
                                            n_sum_principal = sum_principal
                                            'selisih principal
                                            s_principal = (n_principal - sum_principal)
                                        Else
                                            'not matching
                                            'get principal from DT
                                            n_sum_principal = dt_principal
                                            'selisih principal
                                            s_principal = (n_principal - sum_principal)
                                        End If

                                        'check interest daily & interest total counting from PMT with table DT
                                        If cb_import_dt.SelectedIndex.ToString = 1 Then
                                            'from 9850 query file
                                            'matching
                                            If Double.Equals(dt_interest_daily, sum_interest_daily) Then
                                                'round interest daily
                                                'selisih interest
                                                s_interest = (n_interest - dt_interest)
                                                'get total (sisa) interest
                                                sisa_interest = (dt_interest - sum_interest_daily)
                                            Else
                                                'different is last tenor from 9850 with calculation and over 30 days
                                                If remain_tenor = 0 AndAlso n_overdue_days >= 30 Then
                                                    'not matching
                                                    'selisih interest
                                                    s_interest = (n_interest - dt_interest)
                                                    'get total (sisa) interest
                                                    sisa_interest = (dt_interest - sum_interest_daily)
                                                Else
                                                    'change interest daily from 9850
                                                    sum_interest_daily = dt_interest_daily
                                                    'get total (sisa) interest
                                                    sisa_interest = (dt_interest - sum_interest_daily)
                                                End If
                                            End If
                                            'total interest 
                                            n_sum_interest = dt_interest
                                        ElseIf cb_import_dt.SelectedIndex.ToString = 2 Then
                                            'from DT query
                                            'selisih interest
                                            s_interest = (n_interest - sum_interest)
                                            'get total (sisa) interest
                                            sisa_interest = (sum_interest - sum_interest_daily)
                                            'total interest 
                                            n_sum_interest = sum_interest
                                        End If
                                        'amount payment this month
                                        tb_amount = (n_sum_principal + n_sum_interest)
                                        'column balanced
                                        s_balanced = (n_sum_amount - tb_amount)
                                        'please check COA for adding early termination
                                        s_balanced_COA = total_balanced + s_balanced
                                        'check balance for process early payment
                                        If s_balanced_COA >= 0 Then
                                            If s_balanced < 0 Then
                                                If total_balanced > 0 Then
                                                    'coumn status
                                                    process = "Processed with COA Balance"
                                                    coa_debet = (0 - s_balanced)
                                                Else
                                                    'coumn status
                                                    process = "Processed"
                                                    coa_debet = 0
                                                End If
                                            Else
                                                'coumn status
                                                process = "Processed"
                                                coa_debet = 0
                                            End If
                                            str_total_balanced = total_balanced.ToString
                                            reason_output = "Balance Enough"
                                            str_total_balanced = total_balanced.ToString
                                            'column flag
                                            flag = "OK"
                                            n_process = True
                                            'repayment amount based on file
                                            repayment_file_amount = n_sum_amount
                                            'column save balanced in database if amount is more
                                            load_payment.Rows(x).Cells(0).Value = process.ToString
                                            'change color 
                                            load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                            load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                        Else
                                            If s_balanced_COA >= -1000 AndAlso s_balanced_COA < 0 Then
                                                'adjust repayment amount for if s_balance_COA is minus and need addtional amount from file repayment
                                                repayment_file_amount = n_sum_amount - s_balanced_COA
                                                coa_debet = total_balanced
                                                'check status 
                                                If total_balanced > 0 Then
                                                    'coumn status
                                                    process = "Processed with COA Balance"
                                                Else
                                                    'coumn status
                                                    process = "Processed"
                                                End If
                                                reason_output = "Balance Enough"
                                                'column flag
                                                flag = "OK"
                                                n_process = True
                                                'coa balance from file
                                                str_total_balanced = total_balanced.ToString
                                                'column save balanced in database if amount is more
                                                load_payment.Rows(x).Cells(0).Value = process.ToString
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            Else
                                                total_balanced = total_balanced
                                                str_total_balanced = total_balanced.ToString
                                                'repayment amount based on file
                                                repayment_file_amount = n_sum_amount
                                                n_process = False
                                                'coumn status
                                                process = "Not Processed"
                                                reason_output = "Balance Not Enough"
                                                'column flag
                                                flag = "Takeout"
                                                'column save balanced in database if amount is more
                                                load_payment.Rows(x).Cells(0).Value = process.ToString
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            End If
                                        End If

                                        'column repayment principal
                                        table_temp.Rows(x).Item(28) = n_sum_principal.ToString()
                                        'column repayment interest
                                        table_temp.Rows(x).Item(29) = n_sum_interest.ToString()
                                        'column overdue
                                        table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                        'column repayment sum amount
                                        table_temp.Rows(x).Item(31) = tb_amount.ToString()

                                    Else
                                        'check filter Nominal COA
                                        If Not tableCOABalance Is Nothing Then
                                            'get COA Balance From COA
                                            resultFilterCOA = tableCOABalance.Select("[" + tableCOABalance.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                            If Not resultFilterCOA Is Nothing Then
                                                If Not String.IsNullOrWhiteSpace(resultFilterCOA.Item(1).ToString) Then
                                                    total_balanced = Convert.ToDouble(resultFilterCOA.Item(1).ToString)
                                                Else
                                                    total_balanced = 0
                                                End If
                                            Else
                                                total_balanced = 0
                                            End If
                                        Else
                                            total_balanced = 0
                                        End If
                                        'tidak ada data ostanding
                                        n_tenor = Convert.ToInt32(table_payment.Rows(x).Item(14).ToString())
                                        remain_tenor = Convert.ToInt32(table_payment.Rows(x).Item(13).ToString())
                                        strcheckDate_Disbure = Nothing
                                        'get data n_ interest & principal fil early
                                        n_principal = Convert.ToDouble(Trim(table_payment.Rows(x).Item(28).ToString()))
                                        n_interest = Convert.ToDouble(Trim(table_payment.Rows(x).Item(29).ToString()))
                                        n_sum_amount = n_principal + n_interest
                                        n_process = False
                                        'coumn status
                                        process = "Not Processed"
                                        reason_output = "Ostanding Not Found"
                                        'column flag
                                        flag = "Takeout"
                                        'not process
                                        n_process = False
                                        str_total_balanced = total_balanced
                                        'column save balanced in database if amount is more
                                        load_payment.Rows(x).Cells(0).Value = process.ToString
                                        'column repayment principal ostanding payment
                                        table_temp.Rows(x).Item(28) = n_principal.ToString()
                                        'column repayment interest ostanding payment
                                        table_temp.Rows(x).Item(29) = n_interest.ToString()
                                        'column repayment sum amount ostanding payment
                                        table_temp.Rows(x).Item(31) = n_sum_amount.ToString()
                                        load_payment.Rows(x).Cells(0).Value = process.ToString
                                        'change color 
                                        load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                        load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                    End If

                                ElseIf cb_payment.SelectedIndex.ToString = 5 Or cb_payment.SelectedIndex.ToString = 6 Then
                                    '---(6). BUYBACK (PAYMENT) WITH DT OR 9850 QUERY BASED ON FILE AND 2 MONTH CALCULATION-------
                                    'proses early termination calculation
                                    resultFilterDT = table_DT.Select("[" + table_DT.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "'").FirstOrDefault()
                                    If Not resultFilterDT Is Nothing Then
                                        'get data from DT or 9850 query
                                        n_tenor = Convert.ToInt32(resultFilterDT.Item(10).ToString)
                                        max_tenor = Convert.ToInt32(resultFilterDT.Item(8).ToString())
                                        If n_tenor > max_tenor Then
                                            n_tenor = max_tenor
                                        End If
                                        'remain count 
                                        remain_tenor = max_tenor - n_tenor
                                        'OStanding principal
                                        dt_principal = Convert.ToDouble(resultFilterDT.Item(4).ToString)
                                        dt_interest = Convert.ToDouble(resultFilterDT.Item(5).ToString)
                                        n_amount_loan = Convert.ToDouble(resultFilterDT.Item(6).ToString)
                                        dt_interest_daily = Convert.ToDouble(resultFilterDT.Item(7).ToString)
                                        'dd-mm-yyyy : date dirsburse
                                        'yyyy-mm-dd
                                        strcheckDate_Disbure = Trim(resultFilterDT.Item(1).ToString)
                                        str_last_payment_date = Convert.ToString(resultFilterDT.Item(3).ToString)
                                        'get data n_ interest & principal fil early
                                        n_principal = Convert.ToDouble(Trim(table_payment.Rows(x).Item(28).ToString()))
                                        n_interest = Convert.ToDouble(Trim(table_payment.Rows(x).Item(29).ToString()))
                                        n_sum_amount = n_principal + n_interest
                                        'convert date to datetime
                                        Dim getYear3 As String = Strings.Right(strcheckDate_Disbure, 4)
                                        Dim getMonth3 As String = Strings.Mid(strcheckDate_Disbure, 4, 2)
                                        Dim getDay3 As String = Strings.Left(strcheckDate_Disbure, 2)
                                        strcheckDate_Disbure = getYear3 + "-" + getMonth3 + "-" + getDay3
                                        'Add new column for n tenor 
                                        'bunga bulan/tahun,amount loan, max tenor, n_tenor
                                        ' Private Function ProcessPMT(ByRef interest_mounth As Integer, ByRef loan_amount As Double, ByRef max_tenor As Integer, ByRef n_tenor As Integer, ByRef dateDisburse As Date) As DataTable
                                        interest_product = getInterestProduct(Int(cb_product.SelectedIndex.ToString))
                                        table_pmt = ProcessPMT(interest_product, Convert.ToDouble(n_amount_loan), max_tenor, n_tenor, Convert.ToDateTime(strcheckDate_Disbure), date_payment)
                                        If table_pmt.Rows.Count > 0 Then
                                            If max_tenor = n_tenor Then
                                                sum_principal = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(8).ToString)
                                                n_overdue_days = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(10).ToString)
                                                sum_interest_daily = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(11).ToString)
                                                If cb_payment.SelectedIndex.ToString = 5 Then
                                                    'for calculation from 2 month interest
                                                    sum_interest = Convert.ToDouble(table_pmt.Rows(max_tenor - 1).Item(9).ToString)
                                                ElseIf cb_payment.SelectedIndex.ToString = 6 Then
                                                    'for calculation from based on file (interest)
                                                    sum_interest = n_interest
                                                End If

                                            Else
                                                'check principal and interest for early payment and buyback
                                                sum_principal = Convert.ToDouble(table_pmt.Rows(n_tenor - 1).Item(8).ToString)
                                                'for calculation from interest buyback : 2 month and based on file (interest amount)
                                                If cb_payment.SelectedIndex.ToString = 5 Then
                                                    'for calculation from 2 month interest
                                                    For h = n_tenor - 1 To n_tenor
                                                        'Set value data table principal and interest
                                                        If Not String.IsNullOrWhiteSpace(table_pmt.Rows(h).Item(6).ToString) Then
                                                            sum_interest += Convert.ToDouble(table_pmt.Rows(h).Item(6).ToString)
                                                        End If
                                                    Next
                                                ElseIf cb_payment.SelectedIndex.ToString = 6 Then
                                                    'for calculation from based on file (interest)
                                                    sum_interest = n_interest
                                                End If
                                                'calculaton interest daily and overdue day from calculation 
                                                For j = n_tenor - 1 To table_pmt.Rows.Count - 1
                                                    'Set value data table principal and interest
                                                    If Not String.IsNullOrWhiteSpace(table_pmt.Rows(j).Item(10).ToString) Then
                                                        n_overdue_days += Convert.ToDouble(table_pmt.Rows(j).Item(10).ToString)
                                                    End If
                                                    If Not String.IsNullOrWhiteSpace(table_pmt.Rows(j).Item(11).ToString) Then
                                                        sum_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(11).ToString)
                                                    End If
                                                Next
                                            End If
                                        Else
                                            sum_principal = 0
                                            sum_interest = 0
                                            sum_interest_daily = 0
                                        End If
                                        'check filter Nominal COA
                                        If Not tableCOABalance Is Nothing Then
                                            'get COA Balance From COA
                                            resultFilterCOA = tableCOABalance.Select("[" + tableCOABalance.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                            If Not resultFilterCOA Is Nothing Then
                                                If Not String.IsNullOrWhiteSpace(resultFilterCOA.Item(1).ToString) Then
                                                    total_balanced = Convert.ToDouble(resultFilterCOA.Item(1).ToString)
                                                Else
                                                    total_balanced = 0
                                                End If
                                            Else
                                                total_balanced = 0
                                            End If
                                        Else
                                            total_balanced = 0
                                        End If

                                        'check principal counting from PMT with table DT 
                                        'matching 
                                        If Double.Equals(sum_principal, dt_principal) Then
                                            'if same amount or matching principal
                                            n_sum_interest = sum_interest
                                            n_sum_principal = sum_principal
                                            'selisih principal
                                            s_principal = (n_principal - sum_principal)
                                            'selisih interest
                                            s_interest = (n_interest - sum_interest)
                                        Else
                                            'not matching
                                            n_sum_interest = sum_interest
                                            'get principal from DT
                                            n_sum_principal = dt_principal
                                            'selisih principal
                                            s_principal = (n_principal - sum_principal)
                                            'selisih interest
                                            s_interest = (n_interest - sum_interest)
                                        End If
                                        'full amount payment 
                                        tb_amount = (n_sum_principal + n_sum_interest)
                                        'column balanced
                                        s_balanced = (n_sum_amount - tb_amount)
                                        'please check COA for adding early termination
                                        s_balanced_COA = total_balanced + s_balanced
                                        'check balance for process early payment
                                        If s_balanced_COA >= 0 Then
                                            If s_balanced < 0 Then
                                                If total_balanced > 0 Then
                                                    'coumn status
                                                    process = "Processed with COA Balance"
                                                    coa_debet = (0 - s_balanced)
                                                Else
                                                    'coumn status
                                                    process = "Processed"
                                                    coa_debet = 0
                                                End If
                                            Else
                                                'coumn status
                                                process = "Processed"
                                                coa_debet = 0
                                            End If
                                            str_total_balanced = total_balanced.ToString
                                            reason_output = "Balance Enough"
                                            'column flag
                                            flag = "OK"
                                            n_process = True
                                            'process debet coa from column debet coa
                                            If s_balanced < 0 Then
                                                coa_debet = (0 - s_balanced)
                                            Else
                                                coa_debet = 0
                                            End If
                                            'repayment amount based on file
                                            repayment_file_amount = n_sum_amount
                                            'column save balanced in database if amount is more
                                            load_payment.Rows(x).Cells(0).Value = process.ToString
                                            'change color 
                                            load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                            load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                        Else
                                            If s_balanced_COA >= -1000 AndAlso s_balanced_COA < 0 Then
                                                'adjust repayment amount for if s_balance_COA is minus and need addtional amount from file repayment
                                                repayment_file_amount = n_sum_amount - s_balanced_COA
                                                coa_debet = total_balanced
                                                'coumn status
                                                process = "Processed With COA Balance"
                                                reason_output = "Balance Enough"
                                                'column flag
                                                flag = "OK"
                                                n_process = True
                                                'coa balance from file
                                                str_total_balanced = total_balanced.ToString
                                                'column save balanced in database if amount is more
                                                load_payment.Rows(x).Cells(0).Value = process.ToString
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.DarkGreen
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            Else
                                                total_balanced = total_balanced
                                                str_total_balanced = total_balanced.ToString
                                                'repayment amount based on file
                                                repayment_file_amount = n_sum_amount
                                                n_process = False
                                                'coumn status
                                                process = "Not Processed"
                                                reason_output = "Balance Not Enough"
                                                'column flag
                                                flag = "Takeout"
                                                'column save balanced in database if amount is more
                                                load_payment.Rows(x).Cells(0).Value = process.ToString
                                                'change color 
                                                load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                                load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                            End If
                                        End If

                                        'column repayment principal
                                        table_temp.Rows(x).Item(28) = n_sum_principal.ToString()
                                        'column repayment interest
                                        table_temp.Rows(x).Item(29) = n_sum_interest.ToString()
                                        'column overdue
                                        table_temp.Rows(x).Item(30) = table_payment.Rows(x).Item(30).ToString()
                                        'column repayment sum amount
                                        table_temp.Rows(x).Item(31) = tb_amount.ToString()
                                    Else
                                        'check filter Nominal COA
                                        If Not tableCOABalance Is Nothing Then
                                            'get COA Balance From COA
                                            resultFilterCOA = tableCOABalance.Select("[" + tableCOABalance.Columns(0).ColumnName + "] ='" + shbi_acct_no.ToString + "' ").FirstOrDefault()
                                            If Not resultFilterCOA Is Nothing Then
                                                If Not String.IsNullOrWhiteSpace(resultFilterCOA.Item(1).ToString) Then
                                                    total_balanced = Convert.ToDouble(resultFilterCOA.Item(1).ToString)
                                                Else
                                                    total_balanced = 0
                                                End If
                                            Else
                                                total_balanced = 0
                                            End If
                                        Else
                                            total_balanced = 0
                                        End If
                                        sisa_interest = 0
                                        'tidak ada data
                                        n_tenor = Convert.ToInt32(table_payment.Rows(x).Item(14).ToString())
                                        remain_tenor = Convert.ToInt32(table_payment.Rows(x).Item(13).ToString())
                                        strcheckDate_Disbure = Nothing
                                        'get data n_ interest & principal fil early
                                        n_principal = Convert.ToDouble(Trim(table_payment.Rows(x).Item(28).ToString()))
                                        n_interest = Convert.ToDouble(Trim(table_payment.Rows(x).Item(29).ToString()))
                                        n_sum_amount = n_principal + n_interest
                                        n_process = False
                                        'coumn status
                                        process = "Not Processed"
                                        reason_output = "Ostanding Not Found"
                                        'column flag
                                        flag = "Takeout"
                                        'not process
                                        n_process = False
                                        str_total_balanced = total_balanced
                                        'column save balanced in database if amount is more
                                        load_payment.Rows(x).Cells(0).Value = process.ToString
                                        'column repayment principal ostanding payment
                                        table_temp.Rows(x).Item(28) = n_principal.ToString()
                                        'column repayment interest ostanding payment
                                        table_temp.Rows(x).Item(29) = n_interest.ToString()
                                        'column repayment sum amount ostanding payment
                                        table_temp.Rows(x).Item(31) = n_sum_amount.ToString()
                                        load_payment.Rows(x).Cells(0).Value = process.ToString
                                        'change color 
                                        load_payment.Rows(x).Cells(0).Style.BackColor = Color.Tomato
                                        load_payment.Rows(x).Cells(0).Style.ForeColor = Color.White
                                    End If

                                ElseIf cb_payment.SelectedIndex.ToString = 7 Then

                                    'END CHOOSE PAYMENT
                                End If


                            End If

                        End If

                        'add new column output process
                        'table_temp.Columns.Add("Selisih Principal")
                        'table_temp.Columns.Add("Selisih Interest")
                        'table_temp.Columns.Add("Selisih Amount")
                        'table_temp.Columns.Add("Remain Interest")
                        'table_temp.Columns.Add("Interest Daily")
                        'table_temp.Columns.Add("Overdue Days")
                        'table_temp.Columns.Add("Ostanding DT")
                        'table_temp.Columns.Add("Repayment Amount File")
                        'table_temp.Columns.Add("COA Balance")
                        'table_temp.Columns.Add("(COA Balance+Selisih Amount)")
                        'table_temp.Columns.Add("COA Debet")
                        'table_temp.Columns.Add("Last Payment Date")
                        'table_temp.Columns.Add("Flag")
                        'table_temp.Columns.Add("Reason")
                        'table_temp.Columns.Add("Status")
                        'table_temp.Columns.Add("SHBI Account")



NextLoopingRows:
                        '-------------Send Data to Output -----------------
                        'send to output load payment load
                        load_payment.Rows(x).Cells(1).Value = s_principal.ToString()
                        load_payment.Rows(x).Cells(2).Value = s_interest.ToString()
                        load_payment.Rows(x).Cells(3).Value = s_balanced.ToString()
                        load_payment.Rows(x).Cells(4).Value = str_total_balanced.ToString()
                        load_payment.Rows(x).Cells(5).Value = reason_output.ToString()

                        'tenor and remain tenor
                        table_temp.Rows(x).Item(11) = n_tenor.ToString()
                        table_temp.Rows(x).Item(12) = n_tenor.ToString()
                        table_temp.Rows(x).Item(13) = remain_tenor.ToString()
                        table_temp.Rows(x).Item(14) = n_tenor.ToString()

                        table_temp.Rows(x).Item(32) = table_payment.Rows(x).Item(32).ToString()
                        'add new column output process
                        'after Process Calculations PMT with upload (Payment File)
                        'selisih principal
                        table_temp.Rows(x).Item(33) = s_principal.ToString()
                        'selisih interest
                        table_temp.Rows(x).Item(34) = s_interest.ToString()
                        '(sisa) balanced from reguler amount (kurang) with payment this amount
                        'addition 6 column datatemp 
                        'column balanced
                        table_temp.Rows(x).Item(35) = s_balanced.ToString()
                        ' total sisa interest (harus dibayar)
                        table_temp.Rows(x).Item(36) = sisa_interest.ToString()
                        ' total  interest (harus dibayar)
                        table_temp.Rows(x).Item(37) = sum_interest_daily.ToString()
                        'overdue day 
                        table_temp.Rows(x).Item(38) = n_overdue_days.ToString()
                        'column OStanding 
                        table_temp.Rows(x).Item(39) = tb_amount.ToString()
                        'column repayment amount file
                        table_temp.Rows(x).Item(40) = repayment_file_amount.ToString()
                        'column selisih coa balance plus total selisih
                        table_temp.Rows(x).Item(41) = str_total_balanced.ToString()
                        'column coa balance 
                        table_temp.Rows(x).Item(42) = s_balanced_COA.ToString()
                        'column coa debet from debet from COA amount
                        table_temp.Rows(x).Item(43) = coa_debet.ToString()
                        'column last payment date
                        table_temp.Rows(x).Item(44) = str_last_payment_date.ToString()
                        'column flag
                        table_temp.Rows(x).Item(45) = flag.ToString()
                        'column reason
                        table_temp.Rows(x).Item(46) = reason_output.ToString()
                        'column status
                        table_temp.Rows(x).Item(47) = process.ToString()
                        'SHBI Account
                        table_temp.Rows(x).Item(48) = shbi_acct_no.ToString()
                        '-- End process payment in month---

                        '---Proccess to Output---
                        'give info to process payment or not
                        If n_process = True Then
                            table_process.ImportRow(table_temp.Rows(x))
                            'check duplicate
                            'data gridview process
                            load_payment_process.DataSource = DeleteDuplicateFromDataTable(table_process, "shbi acct no")
                        ElseIf n_process = False Then
                            table_not_process.ImportRow(table_temp.Rows(x))
                            'check duplicate
                            'data gridview not process
                            load_payment_notprocess.DataSource = table_not_process
                        End If

                        If load_payment.RowCount > 0 Then
                            'process loading persen
                            Dim persen As Integer
                            persen = (x / Int(load_payment.RowCount - 1)) * 100
                            If x = Int(load_payment.RowCount - 2) Then
                                persen = 100
                            End If
                            txt_checking_load.Text = " ( " + Int(persen).ToString + "% ) "
                            'event load data
                            Application.DoEvents()
                        End If
                        C_rows1.Text = load_payment.RowCount - 1
                        ' Show Count Rows in Datagridview
                        C_rows2.Text = load_payment_process.RowCount - 1
                        ' Show Count Rows in Datagridview
                        C_rows3.Text = load_payment_notprocess.RowCount - 1
                    Next

                End If

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


        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'Finally
        'disabled 
        For Each a As DataGridViewColumn In load_payment_process.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            'disabled 
            For Each a As DataGridViewColumn In load_payment_notprocess.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next

        ' End Try


    End Sub
    Private Function checkRegulerPayment(ByRef get_shbi_acc As String) As Integer
        Dim countRegulerHistory As Integer
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(get_shbi_acc) Then
            countRegulerHistory = 0
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            cmd = New SqlCommand("SELECT COUNT(*) AS TENOR FROM [DB_MASTER].[dbo].[DB_REGULER_PAYMENT_KRD] WHERE [SHBI Account] = '" + get_shbi_acc.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data cancelations
                countRegulerHistory = checktable.Rows(0).Item(0)
            Else
                'set alert if not found
                countRegulerHistory = 0
            End If
        End If
        Return countRegulerHistory
    End Function
    'early termination checking
    Private Function checkEarlyPayment(ByRef get_shbi_acc As String) As DataTable
        Dim countEarlyHistory As DataTable
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(get_shbi_acc) Then
            countEarlyHistory = Nothing
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)

            cmd = New SqlCommand("SELECT TOP 1 [Date],[Multifinance Account],[Transaction] FROM [DB_MASTER].[dbo].[DB_EARLY_PAYMENT_KRD] WHERE [SHBI Account] = '" + get_shbi_acc.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data cancelations
                countEarlyHistory = checktable.Copy()
            Else
                'set alert if not found
                countEarlyHistory = Nothing
            End If

        End If

        Return countEarlyHistory
    End Function

    Protected Function DeleteDuplicateFromDataTable(dtDuplicate As DataTable, columnName As String) As DataTable
        Dim hashT As New Hashtable()
        Dim arrDuplicate As New ArrayList()
        For Each row As DataRow In dtDuplicate.Rows
            If hashT.Contains(row(columnName)) Then
                arrDuplicate.Add(row)
            Else
                hashT.Add(row(columnName), String.Empty)
            End If
        Next
        For Each row As DataRow In arrDuplicate
            dtDuplicate.Rows.Remove(row)
        Next
        Return dtDuplicate
    End Function

    Private Function getInterestProduct(ByRef no_product As String) As String
        Dim no As String
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(no_product) Then
            no = "0"
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'check sum balanced today
            cmd = New SqlCommand("SELECT [NAME_PRODUCT], [INTEREST] FROM [DB_MASTER].[dbo].[DB_PRODUCTS] WHERE [NO] = '" + no_product.ToString + "'", SQLConnection)
            'End If
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data balanced calculation
                'sum data total balanced
                no = Trim(checktable.Rows(0).Item(1).ToString)
            Else
                'set alert if not found
                no = "0"
            End If
        End If
        Return no
    End Function

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

    Private Function checkCancellationPayment(ByRef multifinance_acc As String) As DataTable
        Dim tableCancellation As DataTable = Nothing
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then
            tableCancellation = Nothing
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)

            cmd = New SqlCommand("SELECT TOP 1 * FROM [DB_MASTER].[dbo].[DB_CANCELLATION_KRD] WHERE [SHBI Account] = '" + multifinance_acc.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data cancelations
                notProccess = False
                tableCancellation = checktable.Copy()
            Else
                'set alert if not found
                tableCancellation = Nothing
            End If

        End If

        Return tableCancellation
    End Function


    Private Sub btn_coa_balance_Click(sender As Object, e As EventArgs) Handles btn_coa_balance.Click

        Try
            Dim ds As New DataSet()
            Dim a As Integer = 0
            Dim n_coa_balance As Double
            Dim max_tenor, n_tenor As Integer
            Dim shbi_account_coa As String
            Dim getDay1, getMonth1, getYear1 As String
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls|Text Files (*.txt)|*.txt"
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
                    tableCOABalance = ds.Tables(0)
                    RowCountCOA += ds.Tables(0).Rows.Count
                    C_data4.Text = "(" + RowCountCOA.ToString + ")"
                    'close conenction 
                    conn.Close()
                ElseIf extStr = ".txt" Then
                    '(2) reader txt to Datatable
                    Dim SR As StreamReader = New StreamReader(strFile)
                    Dim line As String = SR.ReadLine()
                    Dim strArray As String() = line.Split("|"c)
                    Dim row As DataRow
                    'Call Header Loan Column
                    For Each s As String In strArray
                        tableCOABalance.Columns.Add(New DataColumn())
                    Next
                    Do
                        line = SR.ReadLine
                        If Not line = String.Empty Then
                            row = tableCOABalance.NewRow()
                            row.ItemArray = line.Split("|"c)
                            tableCOABalance.Rows.Add(row)
                        Else
                            Exit Do
                        End If
                    Loop
                    RowCountCOA += tableCOABalance.Rows.Count
                    C_data4.Text = "(" + RowCountCOA.ToString + ")"
                End If

                tableCOABalance.AcceptChanges()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
        End Try


    End Sub


    Private Sub cb_import_dt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_import_dt.SelectedIndexChanged
        If Not cb_import_dt.SelectedIndex.ToString = 0 Then
            Try
                Dim ds As New DataSet()
                'For calculation intalament Payment
                Dim z As Integer = 0
                Dim interest_product As Integer
                Dim loan_amount, dt_principalPMT, ostanding_amount, get_selisih_principal, dt_interest, interest_daily As Double
                Dim max_tenor, n_tenor As Integer
                Dim dueDateDisburseDT, endDueDate, nextPaymentDT, lastPaymentDate As Date
                Dim dt_calculationPMT As New DataTable
                Dim overdue_days As String
                Dim getDay1, getMonth1, getYear1 As String
                Dim getDay2, getMonth2, getYear2 As String
                Dim getDay3, getMonth3, getYear3 As String
                OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls|Text Files (*.txt)|*.txt"
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
                        If table_DT_temp.Rows.Count > 0 Then
                            'add data file cif to new row  
                            For x As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                'import row from another datatable
                                table_DT_temp.ImportRow(ds.Tables(0).Rows(x))
                            Next
                        Else
                            table_DT_temp = ds.Tables(0)
                        End If

                        RowCountDT += ds.Tables(0).Rows.Count
                        C_data3.Text = "(" + RowCountDT.ToString + ")"
                        'close conenction 
                        conn.Close()
                    ElseIf extStr = ".txt" Then
                        '(2) reader txt to Datatable
                        Dim SR As StreamReader = New StreamReader(strFile)
                        Dim line As String = SR.ReadLine()
                        Dim strArray As String() = line.Split("|"c)
                        Dim row As DataRow
                        'Call Header Loan Column
                        For Each s As String In strArray
                            table_DT_temp.Columns.Add(New DataColumn())
                        Next
                        Do
                            line = SR.ReadLine
                            If Not line = String.Empty Then
                                row = table_DT_temp.NewRow()
                                row.ItemArray = line.Split("|"c)
                                table_DT_temp.Rows.Add(row)
                            Else
                                Exit Do
                            End If
                        Loop
                        RowCountDT += table_DT_temp.Rows.Count
                        C_data3.Text = "(" + RowCountDT.ToString + ")"
                    End If

                    If table_DT.Columns.Count <= 0 Then
                        'Add new column for n tenor 
                        table_DT.Columns.Add("SHBI Account")
                        table_DT.Columns.Add("Date Disburse")
                        table_DT.Columns.Add("End DueDate")
                        table_DT.Columns.Add("Last Payment Date")
                        table_DT.Columns.Add("Pokok Ostanding")
                        table_DT.Columns.Add("Bunga Ostanding")
                        table_DT.Columns.Add("Loan Amount")
                        table_DT.Columns.Add("Interest Daily")
                        table_DT.Columns.Add("Max Tenor")
                        table_DT.Columns.Add("Overdue Days")
                        table_DT.Columns.Add("Term")
                    End If

                    For x As Integer = 0 To table_DT_temp.Rows.Count - 1
                        rowDT = table_DT.NewRow()
                        table_DT.Rows.Add(rowDT)
                        loan_amount = 0
                        max_tenor = 0
                        n_tenor = 0
                        interest_daily = 0
                        interest_product = 0
                        overdue_days = 0
                        dt_interest = 0
                        dt_principalPMT = 0
                        dt_calculationPMT = Nothing

                        If cb_import_dt.SelectedIndex.ToString = 1 Then
                            '9850 File
                            table_DT.Rows(z).Item(0) = table_DT_temp.Rows(x).Item(3).ToString
                            'create n tenor from date in next duedate and disburse date
                            'Date Disbursement
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(27).ToString) Then
                                getYear1 = Strings.Left(table_DT_temp.Rows(x).Item(27).ToString, 4)
                                getMonth1 = Strings.Mid(table_DT_temp.Rows(x).Item(27).ToString, 5, 2)
                                getDay1 = Strings.Right(table_DT_temp.Rows(x).Item(27).ToString, 2)
                                dueDateDisburseDT = Convert.ToDateTime(getYear1 + "-" + getMonth1 + "-" + getDay1)
                                table_DT.Rows(z).Item(1) = dueDateDisburseDT.ToString("dd-MM-yyyy")
                            Else
                                table_DT.Rows(z).Item(1) = ""
                            End If
                            'Date End Duedate
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(19).ToString) Then
                                getYear2 = Strings.Left(table_DT_temp.Rows(x).Item(19).ToString, 4)
                                getMonth2 = Strings.Mid(table_DT_temp.Rows(x).Item(19).ToString, 5, 2)
                                getDay2 = Strings.Right(table_DT_temp.Rows(x).Item(19).ToString, 2)
                                endDueDate = Convert.ToDateTime(getYear2 + "-" + getMonth2 + "-" + getDay2)
                                table_DT.Rows(z).Item(2) = endDueDate.ToString("dd-MM-yyyy")
                            Else
                                table_DT.Rows(z).Item(2) = ""
                            End If
                            'Last Payment Date
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(29).ToString) Then
                                getYear3 = Strings.Left(table_DT_temp.Rows(x).Item(29).ToString, 4)
                                getMonth3 = Strings.Mid(table_DT_temp.Rows(x).Item(29).ToString, 5, 2)
                                getDay3 = Strings.Right(table_DT_temp.Rows(x).Item(29).ToString, 2)
                                lastPaymentDate = Convert.ToDateTime(getYear3 + "-" + getMonth3 + "-" + getDay3)
                                table_DT.Rows(z).Item(3) = lastPaymentDate.ToString("dd-MM-yyyy")
                            Else
                                table_DT.Rows(z).Item(3) = ""
                            End If
                            'get data ostanding amount
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(17).ToString) Then
                                ostanding_amount = Convert.ToDouble(table_DT_temp.Rows(x).Item(17).ToString)
                            Else
                                ostanding_amount = 0
                            End If
                            'interest total ostanding
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(35).ToString) Then
                                dt_interest = Convert.ToDouble(table_DT_temp.Rows(x).Item(35).ToString)
                            Else
                                dt_interest = 0
                            End If


                            'get interest data from database type product
                            interest_product = Convert.ToInt32(table_DT_temp.Rows(x).Item(20).ToString)
                            'get data loan application for payment
                            loan_amount = Convert.ToDouble(table_DT_temp.Rows(x).Item(28).ToString)
                            'max tenor
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(31).ToString) Then
                                max_tenor = table_DT_temp.Rows(x).Item(31).ToString
                            Else
                                max_tenor = 0
                            End If
                            'get interest daily 
                            interest_daily = Convert.ToDouble(table_DT_temp.Rows(x).Item(12).ToString)
                            'overdue day from  accurate day of 9850
                            overdue_days = table_DT_temp.Rows(x).Item(23).ToString
                            'get data loan amount for disburse application per accounts
                            table_DT.Rows(z).Item(6) = loan_amount.ToString
                        ElseIf cb_import_dt.SelectedIndex.ToString = 2 Then
                            'DT File
                            'add new rows
                            'SHBI Account
                            table_DT.Rows(z).Item(0) = table_DT_temp.Rows(x).Item(5).ToString
                            'create n tenor from date in next duedate and disburse date
                            'Date Disbursement
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(51).ToString) Then
                                getYear1 = Strings.Left(table_DT_temp.Rows(x).Item(51).ToString, 4)
                                getMonth1 = Strings.Mid(table_DT_temp.Rows(x).Item(51).ToString, 5, 2)
                                getDay1 = Strings.Right(table_DT_temp.Rows(x).Item(51).ToString, 2)
                                dueDateDisburseDT = Convert.ToDateTime(getYear1 + "-" + getMonth1 + "-" + getDay1)
                                table_DT.Rows(z).Item(1) = dueDateDisburseDT.ToString("dd-MM-yyyy")
                            Else
                                table_DT.Rows(z).Item(1) = ""
                            End If
                            'Date End Duedate
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(52).ToString) Then
                                getYear2 = Strings.Left(table_DT_temp.Rows(x).Item(52).ToString, 4)
                                getMonth2 = Strings.Mid(table_DT_temp.Rows(x).Item(52).ToString, 5, 2)
                                getDay2 = Strings.Right(table_DT_temp.Rows(x).Item(52).ToString, 2)
                                endDueDate = Convert.ToDateTime(getYear2 + "-" + getMonth2 + "-" + getDay2)
                                table_DT.Rows(z).Item(2) = endDueDate.ToString("dd-MM-yyyy")
                            Else
                                table_DT.Rows(z).Item(2) = ""
                            End If
                            'Last Payment Date
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(25).ToString) Then
                                getYear3 = Strings.Left(table_DT_temp.Rows(x).Item(25).ToString, 4)
                                getMonth3 = Strings.Mid(table_DT_temp.Rows(x).Item(25).ToString, 5, 2)
                                getDay3 = Strings.Right(table_DT_temp.Rows(x).Item(25).ToString, 2)
                                lastPaymentDate = Convert.ToDateTime(getYear3 + "-" + getMonth3 + "-" + getDay3)
                                table_DT.Rows(z).Item(3) = lastPaymentDate.ToString("dd-MM-yyyy")
                            Else
                                table_DT.Rows(z).Item(3) = ""
                            End If
                            'get data ostanding amount
                            If Not String.IsNullOrWhiteSpace(table_DT_temp.Rows(x).Item(11).ToString) Then
                                ostanding_amount = Convert.ToDouble(table_DT_temp.Rows(x).Item(11).ToString)
                            Else
                                ostanding_amount = 0
                            End If

                            'get data max tenor from date disburse until end duedate 
                            max_tenor = DateDiff(DateInterval.Month, dueDateDisburseDT, endDueDate)
                            'get interest data from database type product
                            ' interest_product = getInterestProduct(Int(cb_product.SelectedIndex.ToString) - 1)
                            interest_product = Convert.ToInt32(table_DT_temp.Rows(x).Item(20).ToString)
                            'get data loan application for payment
                            loan_amount = Convert.ToDouble(table_DT_temp.Rows(x).Item(12).ToString)
                            'get overdue day from any account
                            overdue_days = table_DT_temp.Rows(x).Item(58).ToString
                            'total interest 
                            dt_interest = 0
                            'get interest daily 
                            interest_daily = 0
                            'get data loan amount for disburse application per accounts
                            table_DT.Rows(z).Item(6) = loan_amount.ToString
                        End If
                        'generate tenor
                        'calculation instalment for payment
                        dt_calculationPMT = CountInstalmentPMT(interest_product, loan_amount, max_tenor, dueDateDisburseDT)
                        'for count n tenor from calculation and principal ostanding of DT
                        For y As Integer = 0 To dt_calculationPMT.Rows.Count - 1
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

                        table_DT.Rows(z).Item(4) = ostanding_amount
                        table_DT.Rows(z).Item(5) = dt_interest
                        'not must
                        table_DT.Rows(z).Item(7) = interest_daily.ToString
                        table_DT.Rows(z).Item(8) = max_tenor.ToString
                        table_DT.Rows(z).Item(9) = overdue_days.ToString
                        'get selisih date disbure and next payment in month
                        table_DT.Rows(z).Item(10) = n_tenor
                        z += 1
                    Next
                    table_DT.AcceptChanges()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                '    'event load data
                Application.DoEvents()
            End Try
        End If
    End Sub

    Private RowCountReguler, RowCountDT, RowCountCOA As Integer

    Private Sub btn_import_payment_Click(sender As Object, e As EventArgs) Handles btn_import_payment.Click
        Try
            Dim ds As New DataSet()
            C_rows1.Text = 0
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls|Text Files (*.txt)|*.txt"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                If Not cb_payment.SelectedIndex = 7 Then
                    If load_payment.Columns.Count <= 0 Then
                        With load_payment
                            .ColumnCount = 6
                            .Columns(0).Name = "Status*"
                            .Columns(1).Name = "Selisih Principal*"
                            .Columns(2).Name = "Selisih Interest*"
                            .Columns(3).Name = "SUM Selisih*"
                            .Columns(4).Name = "COA Balance*"
                            .Columns(5).Name = "Reason*"
                        End With
                    End If
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
                    If table_payment.Rows.Count > 0 Then
                        'add data file cif to new row  
                        For x As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            'import row from another datatable
                            table_payment.ImportRow(ds.Tables(0).Rows(x))
                        Next
                    Else
                        table_payment = ds.Tables(0)
                    End If
                    'close conenction 
                    conn.Close()
                ElseIf extStr = ".txt" Then
                    '(2) reader txt to Datatable
                    Dim SR As StreamReader = New StreamReader(strFile)
                    Dim line As String = SR.ReadLine()
                    Dim strArray As String() = line.Split("|"c)
                    Dim row As DataRow
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
                If cb_payment.SelectedIndex = 1 Then
                    'create group sum Amount reguler payment
                    Call groupbyDataReguler(table_payment)
                End If
                'split payment for regular overdue and other payments
                If cb_payment.SelectedIndex = 7 Then
                    'sort payment ASC based on Schedule Bussiness Date 
                    'date job this payment
                    Dim str_date_schedule, str_date_schedule_business, multi_acc As String
                    Dim date_schedule, date_schedule_business As Date
                    'change format datetime for repayment schedule date and repayment schedule business date 
                    For z As Integer = 0 To table_payment.Rows.Count - 1
                        str_date_schedule = Trim(table_payment.Rows(z).Item(2).ToString())
                        str_date_schedule_business = Trim(table_payment.Rows(z).Item(3).ToString())
                        'convert date to datetime
                        Dim getYear1 As String = Strings.Left(str_date_schedule, 4)
                        Dim getMonth1 As String = Strings.Mid(str_date_schedule, 5, 2)
                        Dim getDay1 As String = Strings.Right(str_date_schedule, 2)

                        Dim getYear2 As String = Strings.Left(str_date_schedule_business, 4)
                        Dim getMonth2 As String = Strings.Mid(str_date_schedule_business, 5, 2)
                        Dim getDay2 As String = Strings.Right(str_date_schedule_business, 2)

                        str_date_schedule = getYear1 + "-" + getMonth1 + "-" + getDay1
                        str_date_schedule_business = getYear2 + "-" + getMonth2 + "-" + getDay2
                        'convert to datetime
                        date_schedule = Convert.ToDateTime(str_date_schedule)
                        date_schedule_business = Convert.ToDateTime(str_date_schedule_business)

                        table_payment.Rows(z).Item(2) = date_schedule
                        table_payment.Rows(z).Item(3) = date_schedule_business

                    Next
                    'sorting for schedule payment date ASC
                    table_payment.DefaultView.Sort = "[" + table_payment.Columns(3).ColumnName + "] ASC"
                    table_payment = table_payment.DefaultView.ToTable()
                    table_payment.AcceptChanges()
                    'copy column to datatable table payment_group for overdue payment
                    If table_payment_group.Rows.Count <= 0 Then
                        table_payment_group = table_payment.Copy()
                    End If
                    'filter column for not duplicate from datable
                    table_payment = DeleteDuplicateFromDataTable(table_payment, "" + table_payment.Columns(0).ColumnName + "")
                    load_payment.DataSource = table_payment
                Else
                    'load data with sort by tenor except regular overdue 
                    table_payment.DefaultView.Sort = "[" + table_payment.Columns(14).ColumnName + "] ASC"
                    table_payment = table_payment.DefaultView.ToTable()
                    table_payment = DeleteDuplicateFromDataTable(table_payment, "" + table_payment.Columns(8).ColumnName + "")
                    table_payment.AcceptChanges()
                    load_payment.DataSource = table_payment
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            If table_payment Is Nothing Then
                C_data2.Text = "(False)"
            End If
            'information rows
            RowCountReguler += table_payment.Rows.Count
            C_data2.Text = "(" + RowCountReguler.ToString + ")"
            ' Show Count Rows in Datagridview
            C_rows1.Text = load_payment.RowCount - 1
            'disabled 
            For Each a As DataGridViewColumn In load_payment.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub
    Private Sub HeaderRegulerColumn()
        'column reguler payment
        table_payment.Columns.Add("Job date")
        table_payment.Columns.Add("execution date")
        table_payment.Columns.Add("repayment date")
        table_payment.Columns.Add("KREDIVO")
        table_payment.Columns.Add("KREDIVO cif")
        table_payment.Columns.Add("KREDIVO acct no")
        table_payment.Columns.Add("shbi cif no")
        table_payment.Columns.Add("customer name")
        table_payment.Columns.Add("shbi acct no")
        table_payment.Columns.Add("execution date1")
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
    End Sub

    Private Sub load_payment_DoubleClick(sender As Object, e As EventArgs) Handles load_payment.DoubleClick
        Try
            Dim str_date_excute, str_date_payment, multi_acc As String
            Dim date_excute, date_payment As Date
            Dim n_tenor, remain_tenor As String
            Dim max_tenor As Integer
            Dim pmt As New Detail_Payment() With {.Owner = Me}
            Dim table_pmt As DataTable
            Dim interest_product As String
            Dim n_amount_loan As Double
            Dim rowIndex As Integer = load_payment.CurrentRow.Index
            'send index
            pmt.indexRows = rowIndex
            ' column +6

            interest_day.Rows.Clear()
            interest_day.Columns.Clear()

            pmt.txt_multifinance_customer.Text = Trim(table_temp.Rows(rowIndex).Item(4).ToString())
            multi_acc = Trim(table_temp.Rows(rowIndex).Item(5).ToString())
            remain_tenor = Int(Trim(table_temp.Rows(rowIndex).Item(13).ToString()))
            n_tenor = Int(Trim(table_temp.Rows(rowIndex).Item(14).ToString()))
            'date job this payment
            str_date_payment = Trim(table_temp.Rows(rowIndex).Item(0).ToString())
            str_date_excute = Trim(table_temp.Rows(rowIndex).Item(1).ToString())

            'convert date to datetime
            Dim getYear2 As String = Strings.Left(str_date_excute, 4)
            Dim getMonth2 As String = Strings.Mid(str_date_excute, 5, 2)
            Dim getDay2 As String = Strings.Right(str_date_excute, 2)

            Dim getYear1 As String = Strings.Left(str_date_payment, 4)
            Dim getMonth1 As String = Strings.Mid(str_date_payment, 5, 2)
            Dim getDay1 As String = Strings.Right(str_date_payment, 2)

            str_date_payment = getYear1 + "-" + getMonth1 + "-" + getDay1
            str_date_excute = getYear2 + "-" + getMonth2 + "-" + getDay2
            'convert to datetime
            date_payment = Convert.ToDateTime(str_date_payment)
            date_excute = Convert.ToDateTime(str_date_excute)

            'get interest data from database product
            checkTableDisburse = checkDisbursement(Trim(table_payment.Rows(rowIndex).Item(8).ToString()))
            If Not IsNothing(checkTableDisburse) Then
                n_amount_loan = checkTableDisburse.Rows(0).Item(6).ToString()
            Else
                n_amount_loan = 0
            End If

            If cb_payment.SelectedIndex.ToString = 1 Then
                'proccess count DueDate Payment
                'remove last rows in datatable
                max_tenor = Convert.ToInt32(remain_tenor) + Convert.ToInt32(n_tenor)
                'table_pmt = ProcessDueDate(Convert.ToInt32(remain_tenor), Convert.ToInt32(n_tenor), date_excute)
                interest_product = getInterestProduct(Int(cb_product.SelectedIndex.ToString))
                table_pmt = ProcessPMT(Convert.ToInt32(interest_product), Convert.ToDouble(n_amount_loan), max_tenor, Convert.ToInt32(n_tenor), date_excute, date_payment)

                'table_pmt.Rows.RemoveAt(max_tenor)
                'load data balanced history
                '   pmt.load_balanced.DataSource = checkBalancedHistAcc(multi_acc.ToString)
                'load data payment
                pmt.load_payment.DataSource = table_pmt
                pmt.load_payment.Columns(0).Width = 50
                pmt.load_payment.Columns(1).Width = 100
                pmt.load_payment.Columns(2).Width = 100
                pmt.load_payment.Columns(3).Width = 100
            Else
                'bunga bulan/tahun,amount loan, max tenor, n_tenor
                'Private Function ProcessPMT(ByRef interest_mounth As Integer, ByRef loan_amount As Double, ByRef max_tenor As Integer, ByRef n_tenor As Integer, ByRef dateDisburse As Date) As DataTable
                interest_product = getInterestProduct(Int(cb_product.SelectedIndex.ToString))
                max_tenor = Convert.ToInt32(remain_tenor) + Convert.ToInt32(n_tenor)
                table_pmt = ProcessPMT(Convert.ToInt32(interest_product), Convert.ToDouble(n_amount_loan), max_tenor, Convert.ToInt32(n_tenor), date_excute, date_payment)
                'load data balanced history
                '   pmt.load_balanced.DataSource = checkBalancedHistAcc(multi_acc.ToString)
                'remove last rows in datatable
                'table_pmt.Rows.RemoveAt(max_tenor - 1)
                pmt.load_payment.DataSource = table_pmt
                pmt.load_payment.Columns(0).Width = 50
            End If
            'show data detail 
            pmt.txt_no_cif.Text = Trim(table_temp.Rows(rowIndex).Item(6).ToString())
            pmt.txt_shbi_acc_no.Text = Trim(table_temp.Rows(rowIndex).Item(8).ToString())
            pmt.txt_name.Text = Trim(table_temp.Rows(rowIndex).Item(7).ToString())
            pmt.txt_date_payment.Text = date_payment.ToString("dd-MM-yyyy")
            pmt.txt_date_disburse.Text = date_excute.ToString("dd-MM-yyyy")
            pmt.txt_flag.Text = table_temp.Rows(rowIndex).Item(42).ToString()
            pmt.txt_amount_disburse.Text = n_amount_loan.ToString()
            pmt.txt_tenor.Text = max_tenor.ToString()
            pmt.txt_tenor_no.Text = n_tenor.ToString()
            pmt.txt_remain_tenor.Text = remain_tenor.ToString()
            pmt.txt_auto_trans_no.Text = Trim(table_temp.Rows(rowIndex).Item(32).ToString())

            'data payment file kredivo
            pmt.txt_principal.Text = Trim(load_payment.Rows(rowIndex).Cells(34).Value)
            pmt.txt_interest.Text = Trim(load_payment.Rows(rowIndex).Cells(35).Value)
            pmt.txt_amount.Text = Trim(load_payment.Rows(rowIndex).Cells(37).Value)

            pmt.txt_selisih_principal.Text = Trim(table_temp.Rows(rowIndex).Item(33).ToString())
            pmt.txt_selisih_interest.Text = Trim(table_temp.Rows(rowIndex).Item(34).ToString())
            pmt.txt_balanced.Text = Trim(table_temp.Rows(rowIndex).Item(35).ToString())
            pmt.txt_reason.Text = Trim(table_temp.Rows(rowIndex).Item(46).ToString())
            pmt.txt_interest_daily.Text = Trim(table_temp.Rows(rowIndex).Item(37).ToString())
            'pmt.txt_days.Text = Trim(t_days).ToString()
            'show data bunga harian
            pmt.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try


    End Sub
    'Private Function checkBalancedHistAcc(ByRef multifinance_acc As String) As DataTable
    '    Dim table_balanced As DataTable
    '    Dim cmd As New Data.SqlClient.SqlCommand
    '    Dim checktable As New DataTable()
    '    If String.IsNullOrWhiteSpace(multifinance_acc) Then
    '        table_balanced = Nothing
    '    Else
    '        SQLConnection = New SqlConnection(SQLConnectionString)

    '        cmd = New SqlCommand("SELECT [Repayment date],[Product cif],[Product acc no],[shbi cif no],[shbi acct no],[Balanced] ,[auto transfer acno],[Flag],[Tenor no],[Remain Tenor] FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST]  WHERE [Product acc no] = '" + multifinance_acc.ToString + "' ORDER BY [Repayment date] ASC ", SQLConnection)
    '        Dim adapter As New SqlDataAdapter(cmd)
    '        adapter.Fill(checktable)
    '        'Checking data to any rows in table "Occupation
    '        If checktable.Rows.Count() > 0 Then
    '            'get data balanced calculation
    '            'sum data total balanced
    '            table_balanced = checktable.Copy()
    '        Else
    '            table_balanced = checktable.Copy()
    '        End If
    '    End If
    '    Return table_balanced
    'End Function

    Private Function ProcessDueDate(ByRef remain_tenor As Integer, ByRef n_tenor As Integer, ByRef dateDisburse As Date) As DataTable
        Dim table_PMT As New DataTable
        Dim rowPMT As DataRow
        Dim getdate, setdate As DateTime
        Dim startDate, endDate As DateTime
        Dim strDateDisbure As String
        'max tenor in duedate looping
        Dim arrDueDate(60) As String
        Dim getNoDays, max_tenor As Integer
        'clear table
        table_PMT.Rows.Clear()
        table_PMT.Columns.Clear()
        max_tenor = remain_tenor + n_tenor
        '------Start set count due date any month------
        'show interest, payment in mothly until payment end tenor
        'add new column
        table_PMT.Columns.Add("Month")
        table_PMT.Columns.Add("Due Date")
        table_PMT.Columns.Add("Start Payment")
        table_PMT.Columns.Add("End Payment")
        'processing table payment
        For x As Integer = 0 To max_tenor
            'add new row
            rowPMT = table_PMT.NewRow()
            table_PMT.Rows.Add(rowPMT)

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
                setdate = DateAdd(DateInterval.Month, x + 1, dateDisburse)
            Else
                setdate = DateAdd(DateInterval.Month, x + 1, dateDisburse)
            End If
            '--Due Date Month 1 --

            'check max date if not day in month
            'get duedate last month
            If IsError(setdate) Then
                'count last date in month
                setdate = DateSerial(Year(setdate), Month(setdate) + 1, 0)
                'add to array
                arrDueDate(x) = setdate
                table_PMT.Rows(x).Item(1) = setdate
            Else
                'add to array
                arrDueDate(x) = setdate
                table_PMT.Rows(x).Item(1) = setdate
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
                    If x = max_tenor Then
                        startDate = arrDueDate(x)
                        startDate = DateAdd(DateInterval.Month, -1, startDate)
                        table_PMT.Rows(x).Item(2) = startDate
                        'end check wordays
                    Else
                        'check workdays
                        table_PMT.Rows(x).Item(2) = startDate
                        'end check wordays
                    End If
                End If
            ElseIf x = 0 Then
                startDate = dateDisburse
checkLoopStartPayment2:
                getNoDays = checkDayofWeeks(startDate)
                If getNoDays = 1 Or getNoDays = 7 Then
                    'add next day
                    startDate = DateAdd(DateInterval.Day, 1, startDate)
                    GoTo checkLoopStartPayment2
                Else
                    table_PMT.Rows(x).Item(2) = startDate
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
                table_PMT.Rows(x).Item(3) = endDate
            End If
        Next

        'remove row in datatable
        'table_PMT.Rows.RemoveAt(Int(max_tenor - 1))
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
    Private Function ProcessPMT(ByRef interest_mounth As Integer, ByRef loan_amount As Double, ByRef max_tenor As Integer, ByRef n_tenor As Integer, ByRef dateDisburse As Date, ByRef datePayment As Date) As DataTable
        Dim rowPayment, rowInterestDay As DataRow
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
        Dim n, t As Integer
        'Dim strDateDisbure As String
        Dim arrDueDate(60) As String
        sumInterest = 0
        totalInterest = 0
        totalPrincipal = 0
        interest_daily = 0
        loan = 0
        interest_percent = 0
        tenor = 0
        instalment_amount = 0
        interest_n = 0
        interest_not_round = 0
        OS_Balance = 0
        Principal = 0
        n_days = 0
        t_days = 0
        n = 0
        t = 0
        table_pmt.Columns.Clear()
        table_pmt.Rows.Clear()
        interest_day.Rows.Clear()
        interest_day.Columns.Clear()
        'start process
        interest_percent = Convert.ToDouble(interest_mounth)
        loan = Convert.ToDouble(loan_amount)

        'loan = 3028674
        tenor = Convert.ToDouble(max_tenor)
        'CountPMT('Payment loan','interest rate','month tenor')
        instalment_amount = CountPMT(loan, interest_percent, tenor)
        '------Start set count due date any month------
        'strdate is Date Disburesement
        getdateDisburse = dateDisburse
        '------End set count due date any month------

        'show interest, payment in mothly until payment end tenor
        'add new column
        table_pmt.Columns.Add("Status")
        table_pmt.Columns.Add("Month")
        table_pmt.Columns.Add("Due Date")
        table_pmt.Columns.Add("Start Payment")
        table_pmt.Columns.Add("End Payment")
        table_pmt.Columns.Add("Principal")
        table_pmt.Columns.Add("Interest")
        table_pmt.Columns.Add("Payment")

        table_pmt.Columns.Add("Total Principal")
        table_pmt.Columns.Add("Total Interest")
        table_pmt.Columns.Add("N Days")
        table_pmt.Columns.Add("Interest Daily")

        'new column interest daily

        'interest_day.Columns.Add("Month")
        'interest_day.Columns.Add("Day")
        'interest_day.Columns.Add("Interest")

        'add datarow
        For x As Integer = 0 To tenor
            Dim getNoDays As Integer
            'add new row
            rowPayment = table_pmt.NewRow()
            table_pmt.Rows.Add(rowPayment)

            'column status 
            If cb_payment.SelectedIndex.ToString = 1 Then
                'reguler payment 
                If Strings.Equals(x, n_tenor - 1) Then
                    table_pmt.Rows(x).Item(0) = "On Process"
                ElseIf x < n_tenor - 1 Then
                    table_pmt.Rows(x).Item(0) = "Paid"
                Else
                    table_pmt.Rows(x).Item(0) = "Not Paid"
                End If

            ElseIf cb_payment.SelectedIndex.ToString = 2 Then
                'early payment
                If Strings.Equals(x, n_tenor - 1) Then
                    table_pmt.Rows(x).Item(0) = "On Process"
                ElseIf x < n_tenor - 1 Then
                    table_pmt.Rows(x).Item(0) = "Paid"
                Else
                    table_pmt.Rows(x).Item(0) = "On Process"
                End If

            End If

            'row data column Month
            If x = tenor Then
                table_pmt.Rows(x).Item(1) = Convert.ToString(x)
            Else
                table_pmt.Rows(x).Item(1) = x + 1
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

                table_pmt.Rows(x).Item(2) = setdate.ToString("dd-MM-yyyy")
            Else
                'add to array
                arrDueDate(x) = setdate
                table_pmt.Rows(x).Item(2) = setdate.ToString("dd-MM-yyyy")
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
                        table_pmt.Rows(x).Item(3) = startDate.ToString("dd-MM-yyyy")
                        'end check wordays
                    Else
                        'check workdays
                        table_pmt.Rows(x).Item(3) = startDate.ToString("dd-MM-yyyy")
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
                    table_pmt.Rows(x).Item(3) = startDate.ToString("dd-MM-yyyy")
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
                table_pmt.Rows(x).Item(4) = endDate.ToString("dd-MM-yyyy")
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
            table_pmt.Rows(x).Item(5) = Convert.ToString(Principal)
            'column Interest
            table_pmt.Rows(x).Item(6) = Convert.ToString(interest_n)
            'column Instalment /Payment
            table_pmt.Rows(x).Item(7) = Convert.ToString(instalment_amount)
        Next

        Dim z As Integer
        Dim get_total_Principal, get_total_Interest, get_interest_daily As Double

        For y As Integer = 0 To tenor
            'default value
            get_total_Principal = 0
            get_total_Interest = 0
            get_interest_daily = 0
            n_days = 0
            interest_daily = 0
            'interest daily (yesterday)             
            If max_tenor = n_tenor Then
                If max_tenor = 1 Then
                    'count day from duedate from yesterday (h-1 today) without workday and holiday
                    yesterdayDate = DateAdd(DateInterval.Day, 0, datePayment)
                    'count duedate start for early payment
                    n_days = DateDiff(DateInterval.Day, dateDisburse, yesterdayDate)
                    If n_days > 30 Then
                        n_days = 30
                    ElseIf n_days = 29 Or n_days = 28 Then
                        If dateDisburse.Month = 2 Then
                            n_days = 30
                        End If
                    Else
                        'If dateDisburse.Month = 5 Then
                        '    If dateDisburse.Day = 30 Then
                        '        n_days = n_days - 1
                        '    End If
                        'End If
                    End If
                    Console.WriteLine(n_days)
                    get_total_Principal = 0
                    get_total_Interest = 0
                    'calculation interest daily from how long is day. to get interest daily from yesterday (h-1)
                    'get total principal and in
                    For j = n_tenor - 1 To tenor
                        'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                        'Set value data table principal and interest
                        If Not j = max_tenor - 1 Then
                            get_total_Principal += Convert.ToDouble(table_pmt.Rows(j).Item(5))
                            'Console.WriteLine("total principal: " + get_total_Principal.ToString)

                            'sum all interest next duedate and today interest harian (calculation)
                            get_total_Interest += Convert.ToDouble(table_pmt.Rows(j).Item(6))
                        End If
                    Next

                    'after Process Calculations PMT with upload (Early Payment File)
                    '=TOTAL PRINCIPAL *(INTEREST/100) * (DAYS/360)
                    interest_daily = (get_total_Principal * (interest_mounth / 100) * (n_days / 360))
                    'round interest daily
                    interest_daily = Math.Round(interest_daily)
                    table_pmt.Rows(y).Item(8) = get_total_Principal.ToString
                    table_pmt.Rows(y).Item(9) = get_total_Interest.ToString
                    table_pmt.Rows(y).Item(10) = n_days.ToString
                    table_pmt.Rows(y).Item(11) = interest_daily.ToString

                Else
                    'not last tenor and not only one tenor
                    If Strings.Equals(y, n_tenor) Then
                        'in tenor
                        Dim getDay As String = Strings.Left(table_pmt.Rows(y).Item(2), 2)
                        Dim getMonth As String = Strings.Mid(table_pmt.Rows(y).Item(2), 4, 2)
                        Dim getYear As String = Strings.Right(table_pmt.Rows(y).Item(2), 4)
                        Dim getDueDate As String

                        getDueDate = getYear + "-" + getMonth + "-" + getDay

                        Dim get_duedate_before As Date = Convert.ToDateTime(getDueDate)

                        get_duedate_before = DateAdd(DateInterval.Month, -1, get_duedate_before)

                        If IsError(get_duedate_before) Then
                            get_duedate_before = DateSerial(Year(get_duedate_before), Month(get_duedate_before) + 1, 0)
                        End If
                        'count day from duedate from yesterday (h-1 today) without workday and holiday
                        'If get_duedate_before.Month = 5 Then
                        '    yesterdayDate = DateAdd(DateInterval.Day, -1, datePayment)
                        'Else
                        '    yesterdayDate = DateAdd(DateInterval.Day, 0, datePayment)
                        'End If
                        yesterdayDate = DateAdd(DateInterval.Day, 0, datePayment)
                        'count duedate start for early payment
                        n_days = DateDiff(DateInterval.Day, get_duedate_before, yesterdayDate)

                        If n_days > 30 Then
                            n_days = 30
                        ElseIf n_days = 29 Or n_days = 28 Then
                            If get_duedate_before.Month = 2 Then
                                n_days = 30
                            End If
                        Else
                            'If get_duedate_before.Month = 5 Then
                            '    If get_duedate_before.Day = 30 Then
                            '        n_days = n_days - 1
                            '    End If
                            'End If
                        End If
                        Console.WriteLine(get_duedate_before)
                        Console.WriteLine(n_days)
                        get_total_Principal = 0
                        get_total_Interest = 0
                        'calculation interest daily from how long is day. to get interest daily from yesterday (h-1)
                        'get total principal and in
                        For j = y To tenor
                            'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                            'Set value data table principal and interest
                            If Not j = max_tenor - 1 Then
                                get_total_Principal += Convert.ToDouble(table_pmt.Rows(j).Item(5))
                                'Console.WriteLine("total principal: " + table_pmt.Rows(j).Item(5).ToString)

                                'sum all interest next duedate and today interest harian (calculation)
                                get_total_Interest += Convert.ToDouble(table_pmt.Rows(j).Item(6))

                                'Console.WriteLine("total principal: " + table_pmt.Rows(j).Item(6).ToString)
                            End If
                        Next

                        'after Process Calculations PMT with upload (Early Payment File)
                        '=TOTAL PRINCIPAL *(INTEREST/100) * (DAYS/360)
                        interest_daily = (get_total_Principal * (interest_mounth / 100) * (n_days / 360))
                        'round interest daily
                        interest_daily = Math.Round(interest_daily)
                        table_pmt.Rows(y).Item(8) = get_total_Principal.ToString
                        table_pmt.Rows(y).Item(9) = get_total_Interest.ToString
                        table_pmt.Rows(y).Item(10) = n_days.ToString
                        table_pmt.Rows(y).Item(11) = interest_daily.ToString

                    Else
                        interest_daily = 0
                    End If
                End If

                'column interest daily for early terminations
                'table_pmt.Rows(y).Item(7) = Convert.ToString(interest_daily)
            Else
                If Strings.Equals(y, n_tenor - 1) Then
                    'in tenor
                    Dim getDay As String = Strings.Left(table_pmt.Rows(y).Item(2), 2)
                    Dim getMonth As String = Strings.Mid(table_pmt.Rows(y).Item(2), 4, 2)
                    Dim getYear As String = Strings.Right(table_pmt.Rows(y).Item(2), 4)
                    Dim getDueDate As String

                    getDueDate = getYear + "-" + getMonth + "-" + getDay
                    Dim getDay2 As String = Strings.Left(table_pmt.Rows(y).Item(4), 2)
                    Dim getMonth2 As String = Strings.Mid(table_pmt.Rows(y).Item(4), 4, 2)
                    Dim getYear2 As String = Strings.Right(table_pmt.Rows(y).Item(4), 4)
                    Dim getEndDate As String

                    getEndDate = getYear2 + "-" + getMonth2 + "-" + getDay2

                    Dim get_duedate_before As Date = Convert.ToDateTime(getDueDate)
                    Dim get_end_date As Date = Convert.ToDateTime(getEndDate)

                    'count day from duedate from yesterday (h-1 today) without workday and holiday
                    get_duedate_before = DateAdd(DateInterval.Month, -1, get_duedate_before)

                    If IsError(get_duedate_before) Then
                        get_duedate_before = DateSerial(Year(get_duedate_before), Month(get_duedate_before) + 1, 0)
                    End If
                    'check range duedate and payment date excution (true or not)
                    If datePayment >= get_duedate_before AndAlso datePayment <= get_end_date Then
                        If y = 0 Then
                            'it is range date
                            'count day from duedate from yesterday (h-1 today) without workday and holiday
                            yesterdayDate = DateAdd(DateInterval.Day, 0, datePayment)
                            'count duedate start for early payment
                            n_days = DateDiff(DateInterval.Day, get_duedate_before, yesterdayDate)
                            If n_days > 30 Then
                                n_days = 30
                            ElseIf n_days = 29 Or n_days = 28 Then
                                If get_duedate_before.Month = 2 Then
                                    n_days = 30
                                End If
                            Else
                                'If get_duedate_before.Month = 5 Then
                                '    If get_duedate_before.Day = 30 Then
                                '        n_days = n_days - 1
                                '    End If
                                'End If
                            End If
                            get_total_Principal = 0
                            get_total_Interest = 0
                            'calculation interest daily from how long is day. to get interest daily from yesterday (h-1)
                            'get total principal and in
                            For j = y To tenor
                                'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                                'Set value data table principal and interest
                                If Not j = max_tenor - 1 Then
                                    get_total_Principal += Convert.ToDouble(table_pmt.Rows(j).Item(5))
                                    'Console.WriteLine("total principal: " + get_total_Principal.ToString)

                                    'sum all interest next duedate and today interest harian (calculation)
                                    get_total_Interest += Convert.ToDouble(table_pmt.Rows(j).Item(6))
                                End If
                            Next
                            'after Process Calculations PMT with upload (Early Payment File)
                            '=TOTAL PRINCIPAL *(INTEREST/100) * (DAYS/360)
                            interest_daily = (get_total_Principal * (interest_mounth / 100) * (n_days / 360))
                            'round interest daily
                            interest_daily = Math.Round(interest_daily)
                            table_pmt.Rows(y).Item(8) = get_total_Principal.ToString
                            table_pmt.Rows(y).Item(9) = get_total_Interest.ToString
                            table_pmt.Rows(y).Item(10) = n_days.ToString
                            table_pmt.Rows(y).Item(11) = interest_daily.ToString


                        Else
                            'it is range date
                            'count day from duedate from yesterday (h-1 today) without workday and holiday
                            yesterdayDate = DateAdd(DateInterval.Day, 0, datePayment)
                            'count duedate start for early payment
                            n_days = DateDiff(DateInterval.Day, get_duedate_before, yesterdayDate)

                            If n_days > 30 Then
                                n_days = 30
                            ElseIf n_days = 29 Or n_days = 28 Then
                                If get_duedate_before.Month = 2 Then
                                    n_days = 30
                                End If
                            Else
                                'If get_duedate_before.Month = 5 Then
                                '    If get_duedate_before.Day = 30 Then
                                '        n_days = n_days - 1
                                '    End If
                                'End If
                            End If

                        End If
                        Console.WriteLine(n_days)
                        get_total_Principal = 0
                        get_total_Interest = 0
                        'calculation interest daily from how long is day. to get interest daily from yesterday (h-1)
                        'get total principal and in
                        For j = y To tenor
                            'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                            'Set value data table principal and interest
                            If Not j = max_tenor - 1 Then
                                get_total_Principal += Convert.ToDouble(table_pmt.Rows(j).Item(5))
                                'Console.WriteLine("total principal: " + get_total_Principal.ToString)

                                'sum all interest next duedate and today interest harian (calculation)
                                get_total_Interest += Convert.ToDouble(table_pmt.Rows(j).Item(6))
                            End If
                        Next
                        Console.WriteLine(n_days)
                        'after Process Calculations PMT with upload (Early Payment File)
                        '=TOTAL PRINCIPAL *(INTEREST/100) * (DAYS/360)
                        interest_daily = (get_total_Principal * (interest_mounth / 100) * (n_days / 360))
                        'round interest daily
                        interest_daily = Math.Round(interest_daily)
                        table_pmt.Rows(y).Item(8) = get_total_Principal.ToString
                        table_pmt.Rows(y).Item(9) = get_total_Interest.ToString
                        table_pmt.Rows(y).Item(10) = n_days.ToString
                        table_pmt.Rows(y).Item(11) = interest_daily.ToString


                    Else
                        'not range
                        'interst daily same with interst month
                        n_days = 30
                        get_total_Principal = 0
                        get_total_Interest = 0
                        'calculation interest daily from how long is day. to get interest daily from yesterday (h-1)
                        'get total principal and in
                        For j = y To tenor
                            'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                            'Set value data table principal and interest
                            If Not j = max_tenor - 1 Then
                                get_total_Principal += Convert.ToDouble(table_pmt.Rows(j).Item(5))
                                'Console.WriteLine("total principal: " + get_total_Principal.ToString)

                                'sum all interest next duedate and today interest harian (calculation)
                                get_total_Interest += Convert.ToDouble(table_pmt.Rows(j).Item(6))
                            End If
                        Next

                        'after Process Calculations PMT with upload (Early Payment File)
                        '=TOTAL PRINCIPAL *(INTEREST/100) * (DAYS/360)
                        interest_daily = (get_total_Principal * (interest_mounth / 100) * (n_days / 360))
                        'round interest daily
                        interest_daily = Math.Round(interest_daily)
                        table_pmt.Rows(y).Item(8) = get_total_Principal.ToString
                        table_pmt.Rows(y).Item(9) = get_total_Interest.ToString
                        table_pmt.Rows(y).Item(10) = n_days.ToString
                        table_pmt.Rows(y).Item(11) = interest_daily.ToString

                    End If
                    'column interest daily for early terminations
                    'table_pmt.Rows(y).Item(7) = Convert.ToString(interest_daily)

                Else
                    'in next tenor
                    Dim getDay As String = Strings.Left(table_pmt.Rows(y).Item(2), 2)
                    Dim getMonth As String = Strings.Mid(table_pmt.Rows(y).Item(2), 4, 2)
                    Dim getYear As String = Strings.Right(table_pmt.Rows(y).Item(2), 4)
                    Dim getDueDate As String

                    getDueDate = getYear + "-" + getMonth + "-" + getDay
                    Dim getDay2 As String = Strings.Left(table_pmt.Rows(y).Item(4), 2)
                    Dim getMonth2 As String = Strings.Mid(table_pmt.Rows(y).Item(4), 4, 2)
                    Dim getYear2 As String = Strings.Right(table_pmt.Rows(y).Item(4), 4)
                    Dim getEndDate As String

                    getEndDate = getYear2 + "-" + getMonth2 + "-" + getDay2

                    Dim get_duedate_before As Date = Convert.ToDateTime(getDueDate)
                    Dim get_end_date As Date = Convert.ToDateTime(getEndDate)

                    get_duedate_before = DateAdd(DateInterval.Month, -1, get_duedate_before)

                    If IsError(get_duedate_before) Then
                        get_duedate_before = DateSerial(Year(get_duedate_before), Month(get_duedate_before) + 1, 0)
                    End If
                    'check range duedate and payment date excution (true or not)
                    If datePayment >= get_duedate_before AndAlso datePayment <= get_end_date Then
                        'it is range date
                        'count day from duedate from yesterday (h-1 today) without workday and holiday
                        yesterdayDate = DateAdd(DateInterval.Day, 0, datePayment)
                        'count duedate start for early payment
                        n_days = DateDiff(DateInterval.Day, get_duedate_before, yesterdayDate)

                        If n_days > 30 Then
                            n_days = 30
                        ElseIf n_days = 29 Or n_days = 28 Then
                            If get_duedate_before.Month = 2 Then
                                n_days = 30
                            End If
                        Else
                            'If get_duedate_before.Month = 5 Then
                            '    If get_duedate_before.Day = 30 Then
                            '        n_days = n_days - 1
                            '    End If
                            'End If
                        End If
                        Console.WriteLine(get_duedate_before.Day)
                        get_total_Principal = 0
                        get_total_Interest = 0
                        'calculation interest daily from how long is day. to get interest daily from yesterday (h-1)
                        'get total principal and in
                        For j = y To tenor
                            'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                            'Set value data table principal and interest
                            If Not j = max_tenor - 1 Then
                                get_total_Principal += Convert.ToDouble(table_pmt.Rows(j).Item(5))
                                'Console.WriteLine("total principal: " + get_total_Principal.ToString)

                                'sum all interest next duedate and today interest harian (calculation)
                                get_total_Interest += Convert.ToDouble(table_pmt.Rows(j).Item(6))
                            End If
                        Next
                        'after Process Calculations PMT with upload (Early Payment File)
                        '=TOTAL PRINCIPAL *(INTEREST/100) * (DAYS/360)
                        interest_daily = (get_total_Principal * (interest_mounth / 100) * (n_days / 360))
                        'round interest daily
                        interest_daily = Math.Round(interest_daily)
                        table_pmt.Rows(y).Item(8) = get_total_Principal.ToString
                        table_pmt.Rows(y).Item(9) = get_total_Interest.ToString
                        table_pmt.Rows(y).Item(10) = n_days.ToString
                        table_pmt.Rows(y).Item(11) = interest_daily.ToString


                    Else
                        If y >= n_tenor - 1 Then
                            'check range duedate and payment date excution (more or less)
                            If datePayment >= get_end_date Then
                                n_days = 30
                                For j = y To tenor
                                    'get_interest_daily += Convert.ToDouble(table_pmt.Rows(j).Item(7))
                                    'Set value data table principal and interest
                                    If Not j = max_tenor - 1 Then
                                        get_total_Principal += Convert.ToDouble(table_pmt.Rows(j).Item(5))
                                        'sum all interest next duedate and today interest harian (calculation)
                                        get_total_Interest += Convert.ToDouble(table_pmt.Rows(j).Item(6))
                                    End If
                                Next
                                '=TOTAL PRINCIPAL *(INTEREST/100) * (DAYS/360)
                                interest_daily = (get_total_Principal * (interest_mounth / 100) * (n_days / 360))
                                'round interest daily
                                interest_daily = Math.Round(interest_daily)
                                'column principal total
                                table_pmt.Rows(y).Item(8) = get_total_Principal.ToString
                                table_pmt.Rows(y).Item(9) = get_total_Interest.ToString
                                table_pmt.Rows(y).Item(10) = n_days.ToString
                                'column Interest daily
                                table_pmt.Rows(y).Item(11) = interest_daily.ToString

                            Else

                            End If
                        End If

                    End If

                End If
            End If


        Next

        'remove row in datatable
        table_pmt.Rows.RemoveAt(Int(max_tenor - 1))
        Return table_pmt

    End Function

    Private Sub btn_pmt_Click(sender As Object, e As EventArgs) Handles btn_pmt.Click
        Dim pmt As New PaymentCount()
        pmt.ShowDialog()
    End Sub

    Private Sub cb_export_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_export.SelectedIndexChanged
        Try
            If Not cb_export.SelectedIndex = 0 Then
                Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                Dim strdate As String
                Dim namePayment As String
                Dim nameProducts As String
                Dim filename1 As String = "Not Found "
                Dim extention As String
                getdate.ToString("ddMMyyyyHHmmss")
                strdate = getdate.ToString("dd") + "" + getdate.ToString("MM") + "" + getdate.ToString("yyyy") + "_" + getdate.ToString("HH") + "" + getdate.ToString("mm") + "" + getdate.ToString("ss")
                nameProducts = cb_product.SelectedItem.ToString
                namePayment = cb_payment.SelectedItem.ToString
                ' Choose Extention Format Export
                If cb_export.SelectedIndex = 1 Then
                    'Excel Files 
                    extention = ".xlsx"
                    If tabcontrol1.SelectedIndex.ToString = 0 Then
                        'data load payment
                        filename1 = "D:\OUTPUT\" + nameProducts + "_" + namePayment + "_Load_" + strdate + extention
                        Call ExportDataToExcel(table_temp, filename1)
                        'Data payment process
                    ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                        filename1 = "D:\OUTPUT\" + nameProducts + "_" + namePayment + "_Process_" + strdate + extention
                        Call ExportDataToExcel(table_process, filename1)
                        'Data payment not process
                    ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                        filename1 = "D:\OUTPUT\" + nameProducts + "_" + namePayment + "_Not_Process_" + strdate + extention
                        Call ExportDataToExcel(table_not_process, filename1)
                    ElseIf tabcontrol1.SelectedIndex.ToString = 3 Then
                        filename1 = "D:\OUTPUT\" + nameProducts + "_" + "_Loan_Balance_" + strdate + extention
                        Call ExportDataToExcel(table_DT, filename1)
                    End If
                ElseIf cb_export.SelectedIndex = 2 Then
                    'text Files 
                    extention = ".txt"
                    If tabcontrol1.SelectedIndex.ToString = 0 Then
                        'data load payment
                        filename1 = "D:\OUTPUT\" + nameProducts + "_" + namePayment + "_Load_" + strdate + extention
                        Call DataTableToExportFile(table_temp, filename1)
                        'Data payment process
                    ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                        filename1 = "D:\OUTPUT\" + nameProducts + "_" + namePayment + "_Process_" + strdate + extention
                        Call DataTableToExportFile(table_process, filename1)
                        'Data payment not process
                    ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                        filename1 = "D:\OUTPUT\" + nameProducts + "_" + namePayment + "_Not_Process_" + strdate + extention
                        Call DataTableToExportFile(table_not_process, filename1)
                    ElseIf tabcontrol1.SelectedIndex.ToString = 3 Then
                        filename1 = "D:\OUTPUT\" + nameProducts + "_" + "_Loan_Balance_" + strdate + extention
                        Call DataTableToExportFile(table_DT, filename1)
                    End If
                End If
                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

    Private Sub btn_balanced_Click(sender As Object, e As EventArgs)
        Dim db As New DetailBalanced() With {.Owner = Me}
        'proccess load data payment parsial from table temp
        'copy data table 
        If table_temp.Rows.Count >= 0 Then
            db.table_temp = table_temp.Copy()
        End If
        db.ShowDialog()
    End Sub


    Private Sub btn_report_Click(sender As Object, e As EventArgs)
        Try
            'generate report to proccess in txt file
            Dim repot_table As New DataTable
            Dim index As Integer = 0
            Dim filename As String
            Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
            Dim strdate As String
            Dim type_payment As String
            Dim extention As String
            Dim arrIndexColumns() As Integer = {0, 39, 4, 5, 6, 8, 31, 14, 32, 37, 38, 40, 41, 42}
            'add new column 
            'EX: Date|Transaction|Multifinance CIF|Multifinance Account|SHBI CIF|SHBI Account|Amount|Repayment Account
            'Selisih Principal	Selisih Interest	Payment	OS Balanced	Balanced Amount	Total Balanced	Flag	Reason	Status	Save Balanced
            'final : Date	Flag	Multifinance	Multifinance Account	SHBI CIF	SHBI Account Amount	Term No 	Repayment Account 	Balanced Amount	Total Balanced	Reason	Status
            repot_table.Columns.Add("Date")
            repot_table.Columns.Add("Flag")
            repot_table.Columns.Add("Multifinance")
            repot_table.Columns.Add("Multifinance Account")
            repot_table.Columns.Add("SHBI CIF")
            repot_table.Columns.Add("SHBI Account")
            repot_table.Columns.Add("Amount")
            repot_table.Columns.Add("Term No")
            repot_table.Columns.Add("Repayment Account")
            repot_table.Columns.Add("Balance Amount")
            repot_table.Columns.Add("Total Balance")
            repot_table.Columns.Add("Reason")
            repot_table.Columns.Add("Status")
            repot_table.Columns.Add("Save Balance")

            getdate.ToString("ddMMyyyyHHmmss")
            strdate = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd") + "_" + getdate.ToString("HH") + "" + getdate.ToString("mm") + "" + getdate.ToString("ss")
            If repot_table.Rows.Count <= 0 Then
                For x As Integer = 0 To arrIndexColumns.Length - 1
                    For j As Integer = 0 To table_temp.Rows.Count - 1
                        'add data new table
                        rowTemp = repot_table.NewRow()
                        repot_table.Rows.Add(rowTemp)
                        repot_table.Rows(j).Item(index) = table_temp.Rows(j).Item(arrIndexColumns(x)).ToString
                        If j = table_temp.Rows.Count - 1 Then
                            GoTo GotoExit
                        End If
                    Next
GotoExit:
                    index += 1
                Next
                type_payment = cb_payment.Text.ToString
                'send to output
                filename = "D:\OUTPUT\KRD_" + type_payment + "_" + strdate + ".xlsx"
                Call ExportDataToExcel(repot_table, filename)
                MessageBox.Show("Export " + filename.ToString + " created.", "Alert")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub cb_payment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_payment.SelectedIndexChanged
        Dim cb_type As Integer = cb_payment.SelectedIndex.ToString
        Call clearDatable()
        '        --Choose Payment--
        '[1] Regular with 1410 File
        '[2] Regular Payment with DT
        '[3] Early Termination 
        '[4] Early Termination with DT
        '[5] Buyback with DT (2month)
        '[6] Buyback with DT (File)
        '[7] Regular Overdue
        If cb_type = 1 Then
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            l_payment1.Text = "Reguler Payment"
            l_payment2.Text = "1410 File"
            l_payment3.Hide()
            l_payment4.Hide()
            l_payment2.Show()
            cb_import_dt.Hide()
            btn_coa_balance.Hide()
            btn_import_ostanding.Show()
        ElseIf cb_type = 2 Then
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            l_payment1.Text = "Regular Payment"
            l_payment3.Text = "DT /9850 File"
            l_payment4.Text = "COA Balance"
            l_payment4.Show()
            cb_import_dt.Show()
            l_payment2.Hide()
            l_payment3.Show()
            btn_coa_balance.Show()
            btn_import_ostanding.Hide()
        ElseIf cb_type = 3 Then
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            l_payment1.Text = "Early Payment"
            l_payment2.Text = "Reguler Payment"
            l_payment4.Text = "COA Balance"
            l_payment3.Text = ""
            l_payment4.Show()
            cb_import_dt.Hide()
            l_payment2.Show()
            l_payment3.Hide()
            btn_coa_balance.Show()
            btn_import_ostanding.Show()
        ElseIf cb_type = 4 Then
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            l_payment4.Show()
            l_payment3.Show()
            l_payment2.Show()
            cb_import_dt.Show()
            btn_coa_balance.Show()
            btn_import_ostanding.Show()
            l_payment1.Text = "Early Payment"
            l_payment2.Text = "Reguler Payment"
            l_payment3.Text = "DT /9850 File"
            l_payment4.Text = "COA Balance"
        ElseIf cb_type = 5 Then
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            l_payment1.Text = "Buyback Payment"
            l_payment3.Text = "DT /9850 File"
            l_payment4.Text = "COA Balance"
            l_payment4.Show()
            l_payment1.Show()
            l_payment2.Hide()
            l_payment3.Show()
            cb_import_dt.Show()
            btn_coa_balance.Show()
            btn_import_ostanding.Hide()
        ElseIf cb_type = 6 Then
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            l_payment1.Text = "Buyback Payment"
            l_payment3.Text = "DT /9850 File"
            l_payment4.Text = "COA Balance"
            l_payment1.Show()
            l_payment2.Hide()
            l_payment3.Show()
            l_payment4.Show()
            cb_import_dt.Show()
            btn_coa_balance.Show()
            btn_import_ostanding.Hide()
        ElseIf cb_type = 7 Then
            C_data1.Text = ""
            C_data2.Text = ""
            C_data3.Text = ""
            l_payment1.Text = "Regular Overdue"
            l_payment1.Show()
            l_payment2.Hide()
            l_payment3.Hide()
            l_payment4.Hide()
            cb_import_dt.Hide()
            btn_coa_balance.Hide()
            btn_import_ostanding.Hide()
        End If
    End Sub

    Private Sub btn_new_file_Click(sender As Object, e As EventArgs)
        Dim db As New PaymentDuedate() With {.Owner = Me}
        db.ShowDialog()
    End Sub
End Class