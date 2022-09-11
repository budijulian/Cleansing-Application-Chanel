
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
Public Class PaymentDuedate
    Private conn As OleDbConnection 'connection odb
    Private dts As DataSet ' create table datasheet virtual
    Private table_payment, table_payment_temp, table_balance_temp, table_oustanding, table_oustanding_temp, table_ostanding_group As New DataTable
    Private checkRowOstanding, checkRowPayment, checkRowPaymentTemp, checkRowOstandPrincipalAmount, rowTemp As DataRow
    Dim openfiledialog As New OpenFileDialog ' open file dialog
    '...............SQL SERVER..............
    'Connection SQL Server
    Public SQL As New ConnectionSQL
    ' Private SQLConnectionString As String = SQL.SQLConnectionStringReconsile
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Private tenor_ostands As String

    Private Function checkDuedatePaymentFromDBBalance() As DataTable
        Dim tableDuedate As DataTable = Nothing
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        '  SQLConnection = New SqlConnection(SQLConnectionString)
        cmd = New SqlCommand("SELECT [Date],[Date update] ,[Dibursement date],[Product code] ,[Product cif],[Product acc no] ,[shbi cif no],[customer name],[shbi acct no] ,[Total Balanced],[auto transfer acno],[Remain Tenor],[Tenor] FROM [DB_RECONSILE].[dbo].[DB_TOTAL_BALANCED_PAYMENTS]", SQLConnection)
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(checktable)
        'Checking data to any rows in table "disburesement
        If checktable.Rows.Count() > 0 Then
            'get data Disburement
            tableDuedate = checktable.Copy()
        Else
            tableDuedate = Nothing
        End If
        Return tableDuedate
    End Function
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

    Private Sub PaymentDuedate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            load_result.DataSource = table_payment
            cb_export.SelectedIndex = 0
            C_data1.Text = ""
            C_data2.Text = ""
        Catch ex As Exception
        Finally
            'disabled 
            For Each a As DataGridViewColumn In load_result.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
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
    Private Sub cb_export_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_export.SelectedIndexChanged
        Try
            If Not cb_export.SelectedIndex = 0 Then
                If table_payment_temp.Rows.Count <= 0 Then
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
                        'data load payment
                        filename1 = "D:\OUTPUT\Reguler_Payment_Duedate_" + strdate + extention
                        Call ExportDataToExcel(table_payment_temp, filename1)

                        'type payment : Reguler Payment
                    ElseIf cb_export.SelectedIndex = 2 Then
                        extention = ".txt"
                        'TXT Files
                        filename1 = "D:\OUTPUT\Reguler_Payment_Duedate_" + strdate + extention
                        Call DataTableToExportFile(table_payment_temp, filename1)

                    End If
                    MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn_checking_Click(sender As Object, e As EventArgs) Handles btn_checking.Click
        Try
            Dim y As Integer = 0
            Dim tb_interest, tb_amount, tb_principal, n_amount As Double
            Dim shbi_acc_no_under As String
            Dim tenor_ostands, product_code, product_cif, product_acc_no, shbi_cif_no, customer_name, shbi_acc_no, auto_tranfer_acno, max_tenor, repayment_type As String
            Dim date_execute, repayment_expired, repayment_schedule_Date, repayment_schedule_business_Date, interest_start_Date, os_duedate As DateTime
            Dim n_tenor, remain_tenor, month_disbure As Integer
            If table_oustanding_temp.Rows.Count > 0 And table_payment.Rows.Count > 0 Then
                '   table_balance_temp = checkDuedatePaymentFromDBBalance()
                If table_balance_temp.Rows.Count > 0 Then
                    'copy columns
                    'copy column to datatable success
                    If table_payment_temp.Columns.Count <= 0 Then
                        'column has 103 column
                        For j = 0 To table_payment.Columns.Count - 1
                            table_payment_temp.Columns.Add(table_payment.Columns(j).ColumnName)
                        Next
                    End If
                    'create new table 
                    For x As Integer = 0 To table_balance_temp.Rows.Count - 1

                        tb_interest = 0
                        tb_principal = 0
                        tb_amount = 0
                        repayment_type = "30"
                        date_execute = Trim(table_balance_temp.Rows(x).Item(2))
                        product_code = Trim(table_balance_temp.Rows(x).Item(3).ToString)
                        product_cif = Trim(table_balance_temp.Rows(x).Item(4).ToString)
                        product_acc_no = Trim(table_balance_temp.Rows(x).Item(5).ToString)
                        shbi_cif_no = Trim(table_balance_temp.Rows(x).Item(6).ToString)
                        customer_name = Trim(table_balance_temp.Rows(x).Item(7).ToString)
                        shbi_acc_no = Trim(table_balance_temp.Rows(x).Item(8).ToString)
                        auto_tranfer_acno = Trim(table_balance_temp.Rows(x).Item(10).ToString)
                        max_tenor = Trim(table_balance_temp.Rows(x).Item(12).ToString)
                        n_amount = table_balance_temp.Rows(x).Item(9)
                        'example shbi_acc_no : 730008629033 convert to 730-008-629033
                        'Dim getfirst As String = Strings.Left(shbi_acc_no, 3)
                        'Dim getmiddle As String = Strings.Mid(shbi_acc_no, 4, 3)
                        'Dim getlast As String = Strings.Right(shbi_acc_no, 6)

                        'shbi_acc_no_under = getfirst + "-" + getmiddle + "-" + getlast
                        'first check datatable ostanding file
                        'checkRowOstanding = table_oustanding_temp.Select("[" + table_oustanding_temp.Columns(3).ColumnName + "] ='" + shbi_acc_no_under.ToString + "'").FirstOrDefault()

                        '----CHECKING REGULER PAYMENT IN LOAN OSTANDING (MAAPING BY SHBI ACC NO)----
                        'check data reguler in ostanding file : principal
                        checkRowOstanding = table_oustanding.Select("[" + table_oustanding.Columns(0).ColumnName + "] ='" + shbi_acc_no.ToString + "'").FirstOrDefault()
                        'check rows after filtering table ostanding
                        If Not IsNothing(checkRowOstanding) Then
                            'get data first rows for interest in amount ostanding file
                            tb_interest = Convert.ToDouble(checkRowOstanding.Item(2).ToString)
                            os_duedate = checkRowOstanding.Item(4)
                            'Dim getDay As String = Strings.Left(tenor_ostands, 2)
                            'Dim getMonth As String = Strings.Mid(tenor_ostands, 3, 2)
                            'Dim getYear As String = Strings.Right(tenor_ostands, 4)
                            'str_duedate = getYear + "-" + getMonth + "-" + getDay
                        End If
                        'data more 1 data : data duplicate is overdue
                        checkRowOstandPrincipalAmount = table_ostanding_group.Select("[" + table_ostanding_group.Columns(0).ColumnName + "] ='" + shbi_acc_no.ToString + "'").FirstOrDefault()
                        'process and maaping in reguler file and ostanding file : for principal
                        If Not IsNothing(checkRowOstandPrincipalAmount) Then
                            tb_principal = Convert.ToDouble(checkRowOstandPrincipalAmount.Item(1).ToString)
                            'get sum amount
                            tb_amount = (tb_principal + tb_interest)
                        End If

                        If Not checkRowOstanding Is Nothing Then
                            'get tenor and shbi account number
                            'calculation get n tenor from (selisih) date
                            n_tenor = DateDiff(DateInterval.Month, date_execute, os_duedate)
                            'second check reguler payment file 
                            checkRowPayment = table_payment.Select("[" + table_payment.Columns(8).ColumnName + "] ='" + shbi_acc_no.ToString + "'").FirstOrDefault()
                            If checkRowPayment Is Nothing Then
                                'if count not 0 so, create new file with tenor and more item

                                'check datatable payment amount is matching or not with balance database
                                If n_amount >= tb_amount Then
                                    'create new file
                                    checkRowPaymentTemp = table_payment_temp.NewRow()
                                    table_payment_temp.Rows.Add(checkRowPaymentTemp)

                                    Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                                    getdate.ToString("ddMMyyyyHHmmss")
                                    Dim strdate As String = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd")
                                    table_payment_temp.Rows(y).Item(0) = strdate.ToString
                                    table_payment_temp.Rows(y).Item(1) = date_execute.ToString("yyyyMMdd")
                                    table_payment_temp.Rows(y).Item(2) = strdate.ToString
                                    table_payment_temp.Rows(y).Item(3) = product_code.ToString
                                    table_payment_temp.Rows(y).Item(4) = product_cif.ToString
                                    table_payment_temp.Rows(y).Item(5) = product_acc_no.ToString
                                    table_payment_temp.Rows(y).Item(6) = shbi_cif_no.ToString
                                    table_payment_temp.Rows(y).Item(7) = customer_name.ToString
                                    table_payment_temp.Rows(y).Item(8) = shbi_acc_no.ToString
                                    'DATE EXECUTION
                                    table_payment_temp.Rows(y).Item(9) = date_execute.ToString("yyyyMMdd")
                                    'DATE EXPIRED : +2 FROM DATE EXCUTE (DISBURESEMENT)
                                    repayment_expired = DateAdd(DateInterval.Month, +n_tenor, date_execute)
                                    repayment_expired = DateAdd(DateInterval.Day, -1, repayment_expired)
                                    table_payment_temp.Rows(y).Item(10) = repayment_expired.ToString("yyyyMMdd")

                                    table_payment_temp.Rows(y).Item(11) = n_tenor.ToString
                                    table_payment_temp.Rows(y).Item(12) = n_tenor.ToString
                                    remain_tenor = Convert.ToInt32(max_tenor) - n_tenor
                                    table_payment_temp.Rows(y).Item(13) = remain_tenor.ToString
                                    table_payment_temp.Rows(y).Item(14) = n_tenor.ToString
                                    table_payment_temp.Rows(y).Item(15) = "30"
                                    repayment_schedule_Date = DateAdd(DateInterval.Month, +n_tenor, date_execute)
                                    table_payment_temp.Rows(y).Item(16) = repayment_schedule_Date.ToString("yyyyMMdd")
                                    repayment_schedule_business_Date = DateAdd(DateInterval.Day, +1, repayment_schedule_Date)
                                    table_payment_temp.Rows(y).Item(17) = repayment_schedule_business_Date.ToString("yyyyMMdd")
                                    interest_start_Date = DateAdd(DateInterval.Month, +n_tenor, date_execute)
                                    interest_start_Date = DateAdd(DateInterval.Month, -1, interest_start_Date)
                                    interest_start_Date = DateAdd(DateInterval.Day, +1, interest_start_Date)
                                    table_payment_temp.Rows(y).Item(18) = interest_start_Date.ToString("yyyyMMdd")
                                    repayment_schedule_Date = DateAdd(DateInterval.Month, +n_tenor, date_execute)
                                    repayment_schedule_Date = DateAdd(DateInterval.Day, -1, repayment_schedule_Date)
                                    table_payment_temp.Rows(y).Item(19) = repayment_schedule_Date.ToString("yyyyMMdd")
                                    table_payment_temp.Rows(y).Item(20) = "30"
                                    table_payment_temp.Rows(y).Item(21) = "N"
                                    table_payment_temp.Rows(y).Item(22) = ""
                                    table_payment_temp.Rows(y).Item(23) = ""
                                    table_payment_temp.Rows(y).Item(24) = ""
                                    table_payment_temp.Rows(y).Item(25) = ""
                                    table_payment_temp.Rows(y).Item(26) = "0"
                                    table_payment_temp.Rows(y).Item(27) = "0"
                                    table_payment_temp.Rows(y).Item(28) = "0"
                                    table_payment_temp.Rows(y).Item(29) = "0"
                                    table_payment_temp.Rows(y).Item(30) = "0"
                                    table_payment_temp.Rows(y).Item(31) = "0"
                                    table_payment_temp.Rows(y).Item(32) = auto_tranfer_acno.ToString

                                    y += 1

                                End If

                            End If
                        End If
                    Next
                    load_result.DataSource = table_payment_temp
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            C_Cells1.Text = load_result.Columns.Count
            C_rows1.Text = load_result.Rows.Count - 1
        End Try
    End Sub

    Private Sub btn_import_payment_Click(sender As Object, e As EventArgs) Handles btn_import_payment.Click
        Try
            Dim strFile As String
            Dim ds As New DataSet()
            C_rows1.Text = 0
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls|Text Files (*.txt)|*.txt"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'before clear datatable
                table_payment.Columns.Clear()
                table_payment.Rows.Clear()
                'create new column in datagridview
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
                'load data
                table_payment.DefaultView.Sort = "[" + table_payment.Columns(14).ColumnName + "] ASC"
                table_payment = table_payment.DefaultView.ToTable()
                table_payment.AcceptChanges()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            If table_payment.Columns.Count = 33 Then
                C_data2.Text = "(OK)"
            Else
                C_data2.Text = "(False)"
            End If
            C_Cells1.Text = load_result.Columns.Count
            C_rows1.Text = load_result.Rows.Count - 1
        End Try
    End Sub

    Private Sub btn_import_ostanding_Click(sender As Object, e As EventArgs) Handles btn_import_ostanding.Click
        Try
            Dim ds As New DataSet()
            Dim strFile As String
            C_rows1.Text = 0
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'before clear datatable
                table_oustanding_temp.Columns.Clear()
                table_oustanding_temp.Rows.Clear()
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                strFile = fi.FullName
                '(1) reader excel to Datatable
                conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFile + ";Extended Properties=Excel 12.0;")
                conn.Open()
                Dim myTableName As String = "sheetname1$"
                Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName 
                Dim da As New OleDbDataAdapter(sqlquery, conn)
                da.Fill(ds)
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

                'close conenction 
                conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            'alert after upload
            If table_oustanding_temp.Columns.Count = 25 Then
                C_data1.Text = "(OK)"
            Else
                C_data1.Text = "(False)"
            End If
        End Try
    End Sub
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
            table_oustanding.Rows(x).Item(2) = Convert.ToDouble(Trim(reInterest))
            'column days
            table_oustanding.Rows(x).Item(3) = Convert.ToInt32(Trim(n_days))
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
End Class