
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
Public Class SplitForm


    Dim timeStart As Date = Convert.ToDateTime(DateTime.Now)
    Dim spanTime As TimeSpan
    Private st_hours, st_minutes, st_seconds As Double
    Private conn As OleDbConnection 'koneksi odb
    Private dts As DataSet ' membuat table datasheet virtual
    Private excel As String
    Private txt_fileName As String
    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    Private arrString As New ArrayList()
    Private words = New String() {}
    Private table_split, table_import, TableTemp As New DataTable
    Private Sub SplitForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cb_n_split.SelectedIndex = 0
        txt_folder.Text = "SPLIT"
    End Sub

    Private Sub btn_submit_split_Click(sender As Object, e As EventArgs) Handles btn_submit_split.Click
        Try
            ' Process split file
            With load_ouput
                .ColumnCount = 6
                .Columns(0).Name = "NO"
                .Columns(1).Name = "File Name"
                .Columns(2).Name = "Url"
                .Columns(3).Name = "Flag"
                .Columns(4).Name = "Rows*"
                .Columns(5).Name = "Status"
            End With
            If table_import.Rows.Count = Nothing Then
                MessageBox.Show("Please, Insert a data.!", "Alert")
            Else
                If cb_n_split.SelectedIndex.ToString = 0 Then
                    MessageBox.Show("Please, Choose Split Rows.!", "Alert")
                Else
                    Dim pesan As MsgBoxResult
                    Dim x As Integer
                    Dim flag As Integer
                    Dim persen As Integer
                    Dim kel As Integer
                    Dim n_splitRows As Integer
                    Dim n_file_name As String
                    Dim n_rows As Integer = table_import.Rows.Count - 1
                    Dim n_cells As Integer = table_import.Columns.Count
                    Dim url_temp As String
                    Dim extention As String = ".xlsx"
                    'proccess split start 
                    pesan = MsgBox("Starting Split?", MsgBoxStyle.YesNo, "Alert")
                    If pesan = MsgBoxResult.Yes Then
                        flag = 1
                        n_splitRows = Int(cb_n_split.Text)
                        kel = 1
                        n_file_name = ""
                        'copy column and data to  new datatable
                        TableTemp = table_import.Copy()
                        'add new column
                        TableTemp.Columns.Add("Flag")
                        'copy column to datatable
                        If table_split.Rows.Count <= 0 Then
                            For j = 0 To TableTemp.Columns.Count - 2
                                table_split.Columns.Add(TableTemp.Columns(j).ColumnName)
                            Next
                        End If
                        'create new table row
                        For z As Integer = 0 To n_rows
                            'declare filename and temp
                            txt_fileName = System.IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)
                            url_temp = "D:\" + txt_folder.Text.ToString + "\"

                            'export datatable to file excel
                            n_file_name = "[" + flag.ToString + "] " + txt_fileName.ToString
                            url_temp = url_temp + "" + n_file_name.ToString + extention


                            If z = n_rows Then
                                'add new rows data datagridview
                                n_file_name = "[" + flag.ToString + "] " + txt_fileName.ToString
                                load_ouput.Rows.Add(New String() {flag.ToString, txt_fileName.ToString, url_temp.ToString + "" + txt_fileName.ToString, flag.ToString, n_splitRows.ToString, persen.ToString + " %"})
                                'send data to save datable
                                table_split.ImportRow(TableTemp.Rows(z))
                                'export data
                                ExportDataToExcel(table_split, url_temp)
                                'create from datable to export to excel file
                                table_split.Rows.Clear()
                                'send flag output
                                TableTemp.Rows(z).Item(n_cells) = flag.ToString + "NO " + z.ToString
                                'show rows (sisa)
                                load_ouput.Rows(flag - 1).Cells(4).Value = (Int(table_import.Rows.Count) - (Int(cb_n_split.Text) * (kel - 1))).ToString
                                load_ouput.Rows(flag - 1).Cells(5).Value = "100%"
                                load_ouput.Rows(flag).Cells(5).Value = ""
                            Else

                                'the first batch
                                If z < n_splitRows Then
                                    'send flag output
                                    TableTemp.Rows(z).Item(n_cells) = flag.ToString + "NO " + z.ToString
                                    'send data to save datable
                                    table_split.ImportRow(TableTemp.Rows(z))
                                    'the second batch
                                ElseIf z < n_splitRows * kel Then
                                    'send flag output
                                    TableTemp.Rows(z).Item(n_cells) = flag.ToString + "NO " + z.ToString
                                    'send data to save datable
                                    table_split.ImportRow(TableTemp.Rows(z))
                                ElseIf z > n_splitRows * kel Then
                                    'send flag output
                                    TableTemp.Rows(z).Item(n_cells) = flag.ToString + "NO " + z.ToString
                                    'send data to save datable
                                    table_split.ImportRow(TableTemp.Rows(z))

                                Else
                                    'add new rows data datagridview
                                    n_file_name = "[" + flag.ToString + "] " + txt_fileName.ToString

                                    load_ouput.Rows.Add(New String() {flag.ToString, txt_fileName.ToString, url_temp.ToString + "" + txt_fileName.ToString, flag.ToString, n_splitRows.ToString, persen.ToString + " %"})
                                    'export data
                                    ExportDataToExcel(table_split, url_temp)
                                    'create from datable to export to excel file

                                    table_split.Rows.Clear()
                                    'send flag output
                                    TableTemp.Rows(z).Item(n_cells) = flag.ToString + "NO " + z.ToString

                                    If z = n_splitRows * kel Then
                                        'send flag output
                                        TableTemp.Rows(z).Item(n_cells) = flag.ToString + "NO " + z.ToString
                                        'send data to save datable
                                        table_split.ImportRow(TableTemp.Rows(z))
                                    End If

                                    flag += 1
                                    kel += 1
                                    n_file_name = ""
                                    url_temp = ""
                                    persen = 0
                                End If

                                'process
                                persen = 0
                                persen = ((z / Int(cb_n_split.Text)) * 100)
                                'looping process split
                                If z = (n_splitRows * kel) - 1 Then
                                    persen = 100
                                End If
                                If Not z = n_rows Then
                                    load_ouput.Rows(flag - 1).Cells(5).Value = persen.ToString
                                End If
                            End If

                            'event load data
                            Application.DoEvents()
                        Next
                        'refresh update
                        TableTemp.AcceptChanges()

                    End If

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MessageBox.Show("Process is Done.", "Alert")
        End Try

       

    End Sub
    Private Sub ExportDataToExcel(ByRef gettable As DataTable, ByRef filename As String)
        Using out = New XLWorkbook()
            'out.Worksheets.Add(gettable)
            out.Worksheets.Add(gettable, "Sheet1")
            out.SaveAs(filename)
        End Using
    End Sub


    Private Sub btn_import_Click(sender As Object, e As EventArgs) Handles btn_import.Click
        Try
            Dim ds As New DataSet()
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'before clear datatable
                load_ouput.Columns.Clear()
                load_ouput.Rows.Clear()
                'create new column in datagridview
                If load_ouput.Columns.Count <= 0 Then
                    With load_ouput
                        .ColumnCount = 6
                        .Columns(0).Name = "NO"
                        .Columns(1).Name = "File Name"
                        .Columns(2).Name = "Url"
                        .Columns(3).Name = "Flag"
                        .Columns(4).Name = "Rows*"
                        .Columns(5).Name = "Status"
                    End With
                    load_ouput.Columns(0).Width = 50
                    load_ouput.Columns(1).Width = 170
                    load_ouput.Columns(2).Width = 170
                    load_ouput.Columns(3).Width = 70
                    load_ouput.Columns(4).Width = 70
                    load_ouput.Columns(5).Width = 90
                End If

                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                excel = fi.FullName
                txt_fileName = System.IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)
                conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
                conn.Open()
                Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                Dim da As New OleDbDataAdapter(sqlquery, conn)
                da.Fill(ds)
                table_import = ds.Tables(0)
                conn.Close()

                'default after open file
                load_ouput.Rows(0).Cells(0).Value = "0"
                load_ouput.Rows(0).Cells(1).Value = txt_fileName.ToString
                load_ouput.Rows(0).Cells(2).Value = excel.ToString
                load_ouput.Rows(0).Cells(3).Value = "0"
                load_ouput.Rows(0).Cells(4).Value = table_import.Rows.Count
                load_ouput.Rows(0).Cells(5).Value = "Ready"

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            'disabled 
            For Each a As DataGridViewColumn In load_ouput.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub
End Class