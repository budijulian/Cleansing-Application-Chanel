
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
Public Class DetailBalanced
    ''progress variabel virtual datatable
    Public table_temp, table_balanced, table_balanced_report As New DataTable
    Private rowTemp As DataRow
    Private conn As OleDbConnection 'connection odb
    '...............SQL SERVER..............
    'Connection SQL Server
    Public SQL As New ConnectionSQL
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Private arrString As New ArrayList()
    'Declare variable for PMT Payment
    'Private table_payment As New DataTable
    Private rowPayment As DataRow
    Sub loadDetailBalanced(ByRef table_temp As DataTable)
        Dim total_balance As Double
        'remove duplicate rows
        'table_balanced = DeleteDuplicateFromDataTable(table_temp, "shbi acct no")
        table_balanced.Columns.Clear()
        table_balanced.Rows.Clear()
        table_balanced = table_temp
        '[Repayment date] ,[Flag],[shbi cif no],[shbi acct no,[Balanced],[Tenor no],[Remain Tenor] ,[auto transfer acno],memo
        Dim arrIndexColumns() As Integer = {0, 39, 6, 8, 37, 14, 13, 32}
        'create new table column
        If table_balanced.Columns.Count > 0 Then
            table_balanced_report.Columns.Clear()
            table_balanced_report.Rows.Clear()
            For z As Integer = 0 To arrIndexColumns.Length - 1
                table_balanced_report.Columns.Add(table_balanced.Columns(arrIndexColumns(z)).ColumnName)
            Next
            table_balanced_report.Columns.Add("Memo")

            'create new table row
            Dim index As Integer = 0
            For j As Integer = 0 To table_balanced.Rows.Count - 1
                If table_balanced.Rows(j).Item(42).ToString = "True" Then
                    'add data new table
                    rowTemp = table_balanced_report.NewRow()
                    table_balanced_report.Rows.Add(rowTemp)
                    table_balanced_report.Rows(index).Item(0) = table_balanced.Rows(j).Item(0).ToString
                    table_balanced_report.Rows(index).Item(1) = table_balanced.Rows(j).Item(39).ToString
                    table_balanced_report.Rows(index).Item(2) = table_balanced.Rows(j).Item(6).ToString
                    table_balanced_report.Rows(index).Item(3) = table_balanced.Rows(j).Item(8).ToString
                    total_balance += Convert.ToDouble(table_balanced.Rows(j).Item(37).ToString)
                    table_balanced_report.Rows(index).Item(4) = table_balanced.Rows(j).Item(37).ToString
                    table_balanced_report.Rows(index).Item(5) = table_balanced.Rows(j).Item(14).ToString
                    table_balanced_report.Rows(index).Item(6) = table_balanced.Rows(j).Item(13).ToString
                    table_balanced_report.Rows(index).Item(7) = table_balanced.Rows(j).Item(32).ToString
                    table_balanced_report.Rows(index).Item(8) = table_balanced.Rows(j).Item(6).ToString + "_" + table_balanced.Rows(j).Item(8).ToString
                    index += 1
                End If
            Next
            'load data balanced 
            load_balanced.DataSource = table_balanced_report
            'size columns
            load_balanced.Columns(0).Width = 100
            load_balanced.Columns(1).Width = 70
            load_balanced.Columns(2).Width = 100
            load_balanced.Columns(3).Width = 100
            load_balanced.Columns(4).Width = 70
            load_balanced.Columns(5).Width = 70
            load_balanced.Columns(6).Width = 70
            load_balanced.Columns(7).Width = 100
            load_balanced.Columns(8).Width = 140
        End If

        'show total balance
        lb_total_balance.Text = "Rp." + total_balance.ToString + ",00"

        'set default
        cb_export.SelectedIndex = 0
        C_rows1.Text = load_balanced.Rows.Count - 1
    End Sub
    Private Sub DetailBalanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'filter data balanced is proccess with filter status = true : save balanced in database
        Try
            table_balanced_report.Rows.Clear()
            table_balanced_report.Columns.Clear()
            If Not table_temp Is Nothing Then
                Call loadDetailBalanced(table_temp)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'disabled sort
            If Not table_temp Is Nothing Then
                For Each b As DataGridViewColumn In load_balanced.Columns
                    b.SortMode = DataGridViewColumnSortMode.NotSortable
                Next
            End If

            cb_export.SelectedIndex = 0
        End Try

    End Sub
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
    Private Function checkCountTotalBalanced(ByRef shbi_acct_no As String) As DataTable
        Dim n_Rows As New DataTable
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(shbi_acct_no) Then
            n_Rows = Nothing
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'check sum balanced today
            cmd = New SqlCommand("SELECT [Total Balanced] FROM [DB_RECONSILE].[dbo].[DB_TOTAL_BALANCED_PAYMENTS] WHERE [shbi acct no] = '" + shbi_acct_no.ToString + "'", SQLConnection)

            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data balanced calculation
                'sum data total balanced
                n_Rows = checktable.Copy()
            Else
                'set alert if not found
                'set reason
                n_Rows = checktable.Copy()
            End If
        End If
        Return n_Rows
    End Function
    Private Function checkCountBalancedHist(ByRef shbi_acct_no As String, ByRef datepayment As String) As Integer
        Dim checkbalanced As Integer
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(shbi_acct_no) Then
            checkbalanced = 0
        Else
            Dim getYear As String = Strings.Left(datepayment, 4)
            Dim getMonth As String = Strings.Mid(datepayment, 5, 2)
            Dim getDay As String = Strings.Right(datepayment, 2)
            Dim str_date_payment As String
            Dim get_date_payment As DateTime
            str_date_payment = getYear + "-" + getMonth + "-" + getDay
            'convert to datetime
            get_date_payment = Convert.ToDateTime(str_date_payment)


            SQLConnection = New SqlConnection(SQLConnectionString)
            'check sum balanced today
            If Not IsNothing(get_date_payment) Then
                cmd = New SqlCommand("SELECT COUNT(*) AS Count_Rows FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] WHERE [shbi acct no] = '" + shbi_acct_no.ToString + "' AND [Repayment date] = '" + get_date_payment.ToString("yyyy-MM-dd") + "' ", SQLConnection)
                'cmd = New SqlCommand("SELECT [Balanced] FROM [DB_MASTER].[dbo].[DB_BALANCED_HIST] WHERE [Product acc no] = '" + multifinance_acc.ToString + "'", SQLConnection)
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
                checkbalanced = 0
            End If
        End If
        Return checkbalanced
    End Function
    Private Function updateBalancedHist(ByRef shbi_acct_no As String, ByRef dataTable As DataTable, ByRef index As Integer) As Integer
        Dim n_updRows As Integer = 0
        Dim tenor As Integer
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(shbi_acct_no) Then
        Else
            Using con As New SqlConnection(SQLConnectionString)
                con.Open()
                Dim query As String = "UPDATE [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] SET [Date update] = @Date_update, [Balanced] = @Balanced, WHERE [shbi acct no] = @shbi_acct_no"
                Using cmd As New SqlCommand(query, con)
                    'add data in param
                    tenor = (Convert.ToInt32(dataTable.Rows(index).Item(13).ToString) + Convert.ToInt32(dataTable.Rows(index).Item(14).ToString))
                    cmd.Parameters.AddWithValue("@Date_update", dataTable.Rows(index).Item(2).ToString)
                    cmd.Parameters.AddWithValue("@Balanced", dataTable.Rows(index).Item(37).ToString)
                    cmd.Parameters.AddWithValue("@shbi_acct_no", shbi_acct_no.ToString)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            n_updRows += 1
        End If
        Return n_updRows
    End Function
    Private Function updateTotalBalanced(ByRef shbi_acct_no As String, ByRef dataTable As DataTable, ByRef TBalance As DataTable, ByRef index As Integer) As Integer
        Dim n_updRows As Integer = 0
        Dim tenor As Integer
        Dim UBalance As Double
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(shbi_acct_no) Then
        Else
            Using con As New SqlConnection(SQLConnectionString)
                con.Open()
                Dim query As String = "UPDATE [DB_RECONSILE].[dbo].[DB_TOTAL_BALANCED_PAYMENTS] SET [Date update] = @Date_update, [Total Balanced] = @SumBalanced, [Remain Tenor] =@remain_tenor WHERE [shbi acct no] = @shbi_acct_no"

                Using cmd As New SqlCommand(query, con)
                    'add data in param
                    ' tenor = (Convert.ToInt32(dataTable.Rows(index).Item(13).ToString) + Convert.ToInt32(dataTable.Rows(index).Item(14).ToString))
                    'Update data total balanced
                    UBalance = Convert.ToDouble(TBalance.Rows(0).Item(0)) + Convert.ToDouble(dataTable.Rows(index).Item(37))

                    cmd.Parameters.AddWithValue("@Date_update", dataTable.Rows(index).Item(2).ToString)
                    cmd.Parameters.AddWithValue("@SumBalanced", UBalance.ToString)
                    cmd.Parameters.AddWithValue("@remain_tenor", dataTable.Rows(index).Item(13).ToString)
                    cmd.Parameters.AddWithValue("@shbi_acct_no", shbi_acct_no.ToString)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            n_updRows += 1
        End If
        Return n_updRows
    End Function
    Private Function addTotalBalanced(ByRef shbi_acct_no As String, ByRef dataTable As DataTable, ByRef index As Integer) As Integer
        Dim tenor As Integer
        Dim memo As String
        Dim n_addRows As Integer = 0
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(shbi_acct_no) Then
            n_addRows = 0
        Else
            Using con As New SqlConnection(SQLConnectionString)
                con.Open()
                Dim query As String = "INSERT INTO [DB_RECONSILE].[dbo].[DB_TOTAL_BALANCED_PAYMENTS]([Date],[Date update],[Dibursement date],[Product code],[Product cif],[Product acc no],[shbi cif no],[customer name],[shbi acct no],[Total Balanced],[auto transfer acno],[Remain Tenor],[Tenor],[Memo]) VALUES(@date,@repayment_date,@execution_date,@Product_code,@Product_cif,@Product_acc_no,@shbi_cif_no,@name,@shbi_acct_no,@Balanced,@auto_transfer_acno,@remain_Tenor,@Tenor,@memo)"

                Using cmd As New SqlCommand(query, con)
                    tenor = (Convert.ToInt32(dataTable.Rows(index).Item(13).ToString) + Convert.ToInt32(dataTable.Rows(index).Item(14).ToString))
                    memo = dataTable.Rows(index).Item(8).ToString + "_" + dataTable.Rows(index).Item(6).ToString

                    Dim date1 As String = dataTable.Rows(index).Item(0).ToString
                    Dim getYear1 As String = Strings.Left(date1, 4)
                    Dim getMonth1 As String = Strings.Mid(date1, 5, 2)
                    Dim getDay1 As String = Strings.Right(date1, 2)
                    Dim str_date_payment1 As String
                    Dim get_date_payment1 As DateTime
                    str_date_payment1 = getYear1 + "-" + getMonth1 + "-" + getDay1
                    'convert to datetime
                    get_date_payment1 = Convert.ToDateTime(str_date_payment1)
                    cmd.Parameters.AddWithValue("@date", get_date_payment1)

                    Dim date2 As String = dataTable.Rows(index).Item(2).ToString
                    Dim getYear2 As String = Strings.Left(date2, 4)
                    Dim getMonth2 As String = Strings.Mid(date2, 5, 2)
                    Dim getDay2 As String = Strings.Right(date2, 2)
                    Dim str_date_payment2 As String
                    Dim get_date_payment2 As DateTime
                    str_date_payment2 = getYear2 + "-" + getMonth2 + "-" + getDay2
                    'convert to datetime
                    get_date_payment2 = Convert.ToDateTime(str_date_payment2)
                    cmd.Parameters.AddWithValue("@repayment_date", get_date_payment2)

                    Dim date3 As String = dataTable.Rows(index).Item(1).ToString
                    Dim getYear3 As String = Strings.Left(date3, 4)
                    Dim getMonth3 As String = Strings.Mid(date3, 5, 2)
                    Dim getDay3 As String = Strings.Right(date3, 2)
                    Dim str_date_payment3 As String
                    Dim get_date_payment3 As DateTime
                    str_date_payment3 = getYear3 + "-" + getMonth3 + "-" + getDay3
                    'convert to datetime
                    get_date_payment3 = Convert.ToDateTime(str_date_payment3)
                    cmd.Parameters.AddWithValue("@execution_date", get_date_payment3)

                    'add data in param
                    cmd.Parameters.AddWithValue("@Product_code", dataTable.Rows(index).Item(3).ToString)
                    cmd.Parameters.AddWithValue("@Product_cif", dataTable.Rows(index).Item(4).ToString)
                    cmd.Parameters.AddWithValue("@Product_acc_no", dataTable.Rows(index).Item(5).ToString)
                    cmd.Parameters.AddWithValue("@shbi_cif_no", dataTable.Rows(index).Item(6).ToString)
                    cmd.Parameters.AddWithValue("@name", dataTable.Rows(index).Item(7).ToString)
                    cmd.Parameters.AddWithValue("@shbi_acct_no", dataTable.Rows(index).Item(8).ToString)
                    cmd.Parameters.AddWithValue("@Balanced", dataTable.Rows(index).Item(37).ToString)
                    cmd.Parameters.AddWithValue("@auto_transfer_acno", dataTable.Rows(index).Item(32).ToString)
                    cmd.Parameters.AddWithValue("@remain_Tenor", dataTable.Rows(index).Item(13).ToString)
                    cmd.Parameters.AddWithValue("@Tenor", tenor.ToString)
                    cmd.Parameters.AddWithValue("@memo", memo.ToString)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            n_addRows += 1
           
        End If
        Return n_addRows
    End Function
    Private Function addBalancedHist(ByRef shbi_acct_no As String, ByRef dataTable As DataTable, ByRef index As Integer) As Integer
        Dim n_addRows As Integer
        Dim addRows As Integer
        Dim checktable As New DataTable()
        If Not String.IsNullOrWhiteSpace(shbi_acct_no) Then
            Using con As New SqlConnection(SQLConnectionString)
                con.Open()
                Dim query As String = "INSERT INTO [DB_RECONSILE].[dbo].[DB_BALANCED_HIST]([Date],[Repayment date],[Execute date],[Product code],[Product cif],[Product acc no],[shbi cif no],[shbi acct no],[Balanced],[auto transfer acno],[Flag],[Tenor no],[Remain Tenor]) VALUES(@date,@repayment_date,@execution_date,@Product_code,@Product_cif,@Product_acc_no,@shbi_cif_no,@shbi_acct_no,@Balanced,@auto_transfer_acno,@Flag,@Tenor_no,@remain_Tenor)"

                Using cmd As New SqlCommand(query, con)

                    Dim date1 As String = dataTable.Rows(index).Item(0).ToString
                    Dim getYear1 As String = Strings.Left(date1, 4)
                    Dim getMonth1 As String = Strings.Mid(date1, 5, 2)
                    Dim getDay1 As String = Strings.Right(date1, 2)
                    Dim str_date_payment1 As String
                    Dim get_date_payment1 As DateTime
                    str_date_payment1 = getYear1 + "-" + getMonth1 + "-" + getDay1
                    'convert to datetime
                    get_date_payment1 = Convert.ToDateTime(str_date_payment1)
                    cmd.Parameters.AddWithValue("@date", get_date_payment1)

                    Dim date2 As String = dataTable.Rows(index).Item(2).ToString
                    Dim getYear2 As String = Strings.Left(date2, 4)
                    Dim getMonth2 As String = Strings.Mid(date2, 5, 2)
                    Dim getDay2 As String = Strings.Right(date2, 2)
                    Dim str_date_payment2 As String
                    Dim get_date_payment2 As DateTime
                    str_date_payment2 = getYear2 + "-" + getMonth2 + "-" + getDay2
                    'convert to datetime
                    get_date_payment2 = Convert.ToDateTime(str_date_payment2)
                    cmd.Parameters.AddWithValue("@repayment_date", get_date_payment2)

                    Dim date3 As String = dataTable.Rows(index).Item(1).ToString
                    Dim getYear3 As String = Strings.Left(date3, 4)
                    Dim getMonth3 As String = Strings.Mid(date3, 5, 2)
                    Dim getDay3 As String = Strings.Right(date3, 2)
                    Dim str_date_payment3 As String
                    Dim get_date_payment3 As DateTime
                    str_date_payment3 = getYear3 + "-" + getMonth3 + "-" + getDay3
                    'convert to datetime
                    get_date_payment3 = Convert.ToDateTime(str_date_payment3)
                    cmd.Parameters.AddWithValue("@execution_date", get_date_payment3)

                    cmd.Parameters.AddWithValue("@Product_code", dataTable.Rows(index).Item(3).ToString)
                    cmd.Parameters.AddWithValue("@Product_cif", dataTable.Rows(index).Item(4).ToString)
                    cmd.Parameters.AddWithValue("@Product_acc_no", dataTable.Rows(index).Item(5).ToString)
                    cmd.Parameters.AddWithValue("@shbi_cif_no", dataTable.Rows(index).Item(6).ToString)
                    cmd.Parameters.AddWithValue("@shbi_acct_no", dataTable.Rows(index).Item(8).ToString)
                    cmd.Parameters.AddWithValue("@Balanced", dataTable.Rows(index).Item(37).ToString)
                    cmd.Parameters.AddWithValue("@auto_transfer_acno", dataTable.Rows(index).Item(32).ToString)
                    cmd.Parameters.AddWithValue("@Flag", dataTable.Rows(index).Item(39).ToString)
                    cmd.Parameters.AddWithValue("@Tenor_no", dataTable.Rows(index).Item(14).ToString)
                    cmd.Parameters.AddWithValue("@remain_Tenor", dataTable.Rows(index).Item(13).ToString)
                    cmd.ExecuteNonQuery()

                End Using
            End Using
            n_addRows = 1
        Else
            n_addRows = 0
        End If
        Return n_addRows
    End Function

    Private Sub btn_save_balanced_Click(sender As Object, e As EventArgs) Handles btn_save_balanced.Click
        Try
            Dim n_tenor As String
            Dim remain_tenor As String
            Dim max_tenor As Integer
            Dim datepayment As String
            Dim shbi_acct_no As String
            Dim n_rowsTBal As DataTable
            Dim n_rowsAddHist, n_rowsUpdHist, n_rowsUpdTBal As Integer
            n_rowsTBal = Nothing
            n_rowsAddHist = 0
            Dim checkBalancedHist As Integer
            'create new table row
            For x As Integer = 0 To table_balanced.Rows.Count - 1
                'check data true for save balanced
                If table_balanced.Rows(x).Item(42).ToString = "True" Then
                    'set parameter
                    shbi_acct_no = table_balanced.Rows(x).Item(8).ToString
                    n_tenor = table_balanced.Rows(x).Item(14).ToString
                    remain_tenor = table_balanced.Rows(x).Item(13).ToString
                    datepayment = table_balanced.Rows(x).Item(2).ToString
                    'check data balanced in history database table balanced
                    'check data history
                    checkBalancedHist = checkCountBalancedHist(shbi_acct_no.ToString, datepayment.ToString)
                    'data not found
                    If checkBalancedHist <= 0 Then
                        'add new data balanced history
                        n_rowsAddHist += addBalancedHist(shbi_acct_no.ToString, table_balanced, x)
                        If n_rowsAddHist > 0 Then
                            'add data total balanced with new amount total balanced 
                            n_rowsTBal = checkCountTotalBalanced(shbi_acct_no.ToString)
                            'if rows is found , output will set "1"
                            If n_rowsTBal.Rows.Count <= 0 Then
                                'insert data to total
                                addTotalBalanced(shbi_acct_no.ToString, table_balanced, x)
                                'updateTotalBalanced(shbi_acct_no.ToString, table_balanced, x)
                            Else
                                updateTotalBalanced(shbi_acct_no.ToString, table_balanced, n_rowsTBal, x)
                            End If
                        End If
                    ElseIf checkBalancedHist > 0 Then
                    End If

                End If
            Next
            MessageBox.Show("Data inserted : " + n_rowsAddHist.ToString, "Alert")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn_import_Click(sender As Object, e As EventArgs) Handles btn_import.Click
        Try

            Dim ds As New DataSet()
            Dim strFile As String
            C_rows1.Text = 0
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'before clear datatable
                table_temp.Columns.Clear()
                table_temp.Rows.Clear()
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
                    table_temp = ds.Tables(0)
                    Call loadDetailBalanced(table_temp)
                    'close conenction 
                    conn.Close()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()

            ' Show Count Rows in Datagridview
            C_rows1.Text = load_balanced.RowCount - 1
            'disabled 
            For Each a As DataGridViewColumn In load_balanced.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub

    Private Sub cb_export_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_export.SelectedIndexChanged
        Dim filename, extention As String
        Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
        Dim strdate As String
        getdate.ToString("ddMMyyyyHHmmss")
        strdate = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd")
        If Not cb_export.SelectedIndex = 0 Then
            If cb_export.SelectedIndex = 1 Then
                If table_balanced_report.Rows.Count > 0 Then
                    'export to excel file
                    extention = ".xlsx"
                    filename = "D:\OUTPUT\KRD_Report_Reconsile_" + strdate + extention
                    Call ExportDataToExcel(table_balanced_report, filename)
                    'Data payment process
                    MessageBox.Show("Export " + filename.ToString + " created.", "Alert")
                End If
            ElseIf cb_export.SelectedIndex = 2 Then
                If table_balanced_report.Rows.Count > 0 Then
                    'export to txt file
                    extention = ".txt"
                    filename = "D:\OUTPUT\KRD_Report_Reconsile_" + strdate + extention
                    Call DataTableToExportFile(table_balanced_report, filename)
                    'Data payment process
                    MessageBox.Show("Export " + filename.ToString + " created.", "Alert")
                End If
            End If
        End If
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
End Class
