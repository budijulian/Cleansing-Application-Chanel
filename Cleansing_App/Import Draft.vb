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
Public Class Import_Draft
    Private table_file As DataTable
    Public table_import, table_CIF, table_Loan As DataTable
    Private conn As OleDbConnection 'koneksi odb
    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    Private rowCIF, rowTemp As DataRow
    Private excel As String
    Private z As Integer = 1
    Public type_product As String
    Public type_join As String
    Private Sub Import_Draft_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim fm As Loan = CType(Me.Owner, Loan)
            If fm.tableCIF.Rows.Count > 0 Then
                table_CIF = fm.tableCIF.Copy()
            End If
            If fm.tableLoan.Rows.Count > 0 Then
                table_import = fm.tableLoan.Copy()
            End If
            type_join = fm.cb_type_join.Text
            type_product = fm.cb_product.Text
            C_Cells1.Text = 0
            C_rows1.Text = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn_submit_Click(sender As Object, e As EventArgs) Handles btn_submit.Click
        Try
            If table_file.Columns.Count <= 0 Then
                MessageBox.Show("Data is null", "Alert")

            Else
                'send value to another form
                Dim fm As New Loan With {.Owner = Me}
                fm.tableNoCIF = table_import.Copy()
                If table_CIF.Rows.Count > 0 Then
                    fm.tableCIF = table_CIF.Copy()
                End If
                If table_Loan.Rows.Count > 0 Then
                    fm.tableLoan = table_Loan.Copy()
                End If
                fm.cb_type_join.Text = type_join.ToString
                fm.cb_product.Text = type_product.ToString

                fm.C_data1.Text = "(OK)"
                fm.C_data2.Text = "(OK)"

                fm.Show()
                ''MessageBox.Show("Import Success", "Alert")
                fm.MdiParent = Home
                Me.Close()
            End If
        

        Catch ex As Exception

        End Try

        ''Try
        'If Not table_import.Rows.Count > 0 Then
        '    MessageBox.Show("Data is null", "Alert")
        'Else
        '    'send value to another form

        '    Dim fm As New Loan With {.Owner = Loan}
        '    fm.tableCIF = table_CIF.Copy()
        '    fm.C_data2.Text = "(OK)"
        '    fm.tableNoCIF = table_import.Copy()
        '    fm.C_data1.Text = "(OK)"
        '    fm.cb_type_join.Text = type_join.ToString
        '    fm.cb_product.Text = type_product.ToString
        '    fm.Show()
        '    fm.MdiParent = Home
        '    Me.Close()
        'End If


        '    'Catch ex As Exception
        '    '    MessageBox.Show(ex.Message)
        '    'End Try

    End Sub

    Private Sub btn_import_Click(sender As Object, e As EventArgs) Handles btn_import.Click
        Try
            Dim ds As New DataSet()
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "All Files (*.*)|*.*|Excel Files (*.xlsx)|*.xlsx|Xls Files (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then

                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                excel = fi.FullName
                conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
                conn.Open()
                'Name sheet in excel
                'Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                Dim myTableName As String = "sheetname1$"
                Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                Dim da As New OleDbDataAdapter(sqlquery, conn)
                'remove rows index 0
                da.Fill(ds)
                If load_import.Rows.Count > 0 Then
                    Dim row As String() = New String() {z, filename, ds.Tables(0).Rows.Count - 1}
                    load_import.Rows.Add(row)
                    'add data file cif to new row  

                    For x As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        'import row from another datatable
                        table_import.ImportRow(ds.Tables(0).Rows(x))

                    Next
                Else
                    table_import = ds.Tables(0)
                    load_import.ColumnCount = 3
                    load_import.Columns(0).Name = "NO"
                    load_import.Columns(1).Name = "File Name"
                    load_import.Columns(2).Name = "Rows"
                    Dim row As String() = New String() {z, filename, ds.Tables(0).Rows.Count - 1}
                    load_import.Rows.Add(row)
                End If
                z = z + 1
                'set value datatable
                C_Cells1.Text = table_import.Columns.Count
                C_rows1.Text = table_import.Rows.Count - 1
                table_import.AcceptChanges()
                conn.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try
    End Sub

End Class