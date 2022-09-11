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
Public Class ImportDatabase
    ''progress variabel virtual datatable
    Public table_import As New DataTable
    Private conn As OleDbConnection 'connection odb
    '...............SQL SERVER..............
    'Connection SQL Server
    Public SQL As New ConnectionSQL
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Public getPdt As New accountPayments
    Private rowImport As DataRow
    Private Sub btn_import_Click(sender As Object, e As EventArgs) Handles btn_import.Click
        Try
            Dim ds As New DataSet()
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Text Files (*.TXT)|*.txt"

            Dim CSVpath As String
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'before clear datatable
                table_import.Columns.Clear()
                table_import.Rows.Clear()
                'load event start
                Application.DoEvents()
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                CSVpath = fi.FullName

                '(2) reader csv to Datatable
                Dim SR As StreamReader = New StreamReader(CSVpath)
                Dim line As String = SR.ReadLine()
                Dim strArray As String() = line.Split("|"c)
                Dim row As DataRow
                'column
                For Each s As String In strArray
                    table_import.Columns.Add(New DataColumn())
                Next

                Do
                    line = SR.ReadLine
                    If Not line = String.Empty Then
                        row = table_import.NewRow()
                        row.ItemArray = line.Split("|"c)
                        table_import.Rows.Add(row)
                    Else
                        Exit Do
                    End If
                Loop
                load_import_data.DataSource = table_import
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()

            ' Show Count Rows in Datagridview
            C_rows1.Text = load_import_data.RowCount - 1
            'disabled 
            For Each a As DataGridViewColumn In load_import_data.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub
    Private Function addNewRowsTable(ByRef type As String, ByRef dataTable As DataTable, ByRef index As Integer) As Integer
        Dim n_addRows As Integer
        Dim addRows As Integer
        Dim query As String
        Dim checktable As New DataTable()
        query = ""
        If Not dataTable Is Nothing Then
            Using con As New SqlConnection(SQLConnectionString)
                con.Open()
                If cb_table.SelectedIndex = 0 Then
                    query = "INSERT INTO [dbo].[DB_EARLY_PAYMENT_KRD] ([Date],[Transaction] ,[Multifinance CIF],[Multifinance Account] ,[SHBI CIF],[SHBI Account] ,[Amount],[Repayment Account]) VALUES(@date,@transaction,@multi_cif_no,@multi_acct_no,@shbi_cif,@shbi_acc,@amount,@auto_transfer_acno)"
                ElseIf cb_table.SelectedIndex = 1 Then
                    query = "INSERT INTO [dbo].[DB_REGULER_PAYMENT_KRD] ([Date],[Transaction],[Multifinance CIF],[Multifinance Account],[SHBI CIF] ,[SHBI Account],[Amount],[Repayment Account]) VALUES(@date,@transaction,@multi_cif_no,@multi_acct_no,@shbi_cif,@shbi_acc,@amount,@auto_transfer_acno)"
                ElseIf cb_table.SelectedIndex = 2 Then
                    query = "INSERT INTO [dbo].[DB_CANCELLATION_KRD] ([Date],[Transaction] ,[Multifinance Account] ,[SHBI CIF] ,[SHBI Account] ,[Amount],[Cancellation Account]) VALUES(@date,@transaction,@multi_acct_no,@shbi_cif,@shbi_acc,@amount,@auto_transfer_acno)"
                ElseIf cb_table.SelectedIndex = 3 Then
                    query = "INSERT INTO [dbo].[DB_DISBURSEMENT_KRD] ([Date] ,[Transaction],[Multifinance CIF] ,[Multifinance Account] ,[SHBI CIF] ,[SHBI Account] ,[Amount],[Trx Type]) VALUES(@date,@transaction,@multi_cif_no,@multi_acct_no,@shbi_cif,@shbi_acc,@amount,@auto_transfer_acno)"
                End If

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@date", dataTable.Rows(index).Item(0).ToString)
                    cmd.Parameters.AddWithValue("@transaction", dataTable.Rows(index).Item(1).ToString)
                    If Not cb_table.SelectedIndex = 2 Then
                        cmd.Parameters.AddWithValue("@multi_cif_no", dataTable.Rows(index).Item(2).ToString)
                        cmd.Parameters.AddWithValue("@multi_acct_no", dataTable.Rows(index).Item(3).ToString)
                        cmd.Parameters.AddWithValue("@shbi_cif", dataTable.Rows(index).Item(4).ToString)
                        cmd.Parameters.AddWithValue("@shbi_acc", dataTable.Rows(index).Item(5).ToString)
                        cmd.Parameters.AddWithValue("@amount", dataTable.Rows(index).Item(6).ToString)
                        cmd.Parameters.AddWithValue("@auto_transfer_acno", dataTable.Rows(index).Item(7).ToString)
                    Else
                        cmd.Parameters.AddWithValue("@multi_acct_no", dataTable.Rows(index).Item(2).ToString)
                        cmd.Parameters.AddWithValue("@shbi_cif", dataTable.Rows(index).Item(3).ToString)
                        cmd.Parameters.AddWithValue("@shbi_acc", dataTable.Rows(index).Item(4).ToString)
                        cmd.Parameters.AddWithValue("@amount", dataTable.Rows(index).Item(5).ToString)
                        cmd.Parameters.AddWithValue("@auto_transfer_acno", dataTable.Rows(index).Item(6).ToString)
                    End If

                    cmd.ExecuteNonQuery()
                End Using
            End Using
            n_addRows = 1
        Else
            n_addRows = 0
        End If
        Return n_addRows
    End Function
    Private Function checkNewRowsTable(ByRef shbi_acct_no As String, ByRef dateUpload As String, ByRef strtable As String) As Integer
        Dim checkNewRows As Integer
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(shbi_acct_no) Then
            checkNewRows = 0
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'check sum balanced today
            If Not IsNothing(dateUpload) Then
                cmd = New SqlCommand("SELECT COUNT(*) AS Count_Rows FROM [DB_MASTER].[dbo]." + strtable + " WHERE [SHBI Account] = '" + shbi_acct_no.ToString + "' AND [Date] = '" + dateUpload + "' ", SQLConnection)
            End If
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data balanced calculation
                'sum data total balanced
                checkNewRows = checktable.Rows(0).Item(0)
            Else
                'data not found 
                checkNewRows = 0
            End If
        End If
        Return checkNewRows
    End Function
    Private Sub ImportDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            cb_product.SelectedIndex = 0
            cb_table.SelectedIndex = 0
            Dim getProducts As New DataTable
            getProducts = getPdt.getAllProducts()
            For a = 0 To getProducts.Rows.Count - 1
                cb_product.Items.Add(getProducts.Rows(a).Item(1).ToString)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_submit_split_Click(sender As Object, e As EventArgs) Handles btn_submit_split.Click
        Try
            Dim n_rows As Integer
            Dim query As String
            Dim n_rowsAddHist As Integer
            If cb_product.SelectedIndex = 1 Then
                If cb_table.SelectedIndex = 0 Then
                    ' --Choose Database--
                    'Report Early Payment
                    'create new table row
                    For x As Integer = 0 To table_import.Rows.Count - 1
                        n_rows = checkNewRowsTable(table_import.Rows(x).Item(5).ToString, table_import.Rows(x).Item(0).ToString, "[DB_EARLY_PAYMENT_KRD]")
                        If n_rows <= 0 Then
                            'add new data balanced history
                            n_rowsAddHist += addNewRowsTable(cb_table.SelectedIndex.ToString, table_import, x)
                        End If
                    Next
                ElseIf cb_table.SelectedIndex = 1 Then
                    'Report Reguler Payment
                    'create new table row
                    For x As Integer = 0 To table_import.Rows.Count - 1
                        n_rows = checkNewRowsTable(table_import.Rows(x).Item(5).ToString, table_import.Rows(x).Item(0).ToString, "[DB_REGULER_PAYMENT_KRD]")
                        If n_rows <= 0 Then
                            'add new data balanced history
                            n_rowsAddHist += addNewRowsTable(cb_table.SelectedIndex.ToString, table_import, x)
                        End If
                    Next                    '            

                ElseIf cb_table.SelectedIndex = 2 Then
                    'Report Cancel
                    'create new table row
                    For x As Integer = 0 To table_import.Rows.Count - 1
                        n_rows = checkNewRowsTable(table_import.Rows(x).Item(4).ToString, table_import.Rows(x).Item(0).ToString, "[DB_CANCELLATION_KRD]")
                        If n_rows <= 0 Then
                            'add new data balanced history
                            n_rowsAddHist += addNewRowsTable(cb_table.SelectedIndex.ToString, table_import, x)
                        End If
                    Next
                ElseIf cb_table.SelectedIndex = 3 Then
                    'Report Disbursement
                    'create new table row
                    For x As Integer = 0 To table_import.Rows.Count - 1
                        n_rows = checkNewRowsTable(table_import.Rows(x).Item(5).ToString, table_import.Rows(x).Item(0).ToString, "[DB_DISBURSEMENT_KRD]")
                        If n_rows <= 0 Then
                            'add new data balanced history
                            n_rowsAddHist += addNewRowsTable(cb_table.SelectedIndex.ToString, table_import, x)
                        End If
                    Next
                End If
            End If
            MessageBox.Show("Data inserted : " + n_rowsAddHist.ToString, "Alert")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class