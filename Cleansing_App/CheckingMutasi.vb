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
Public Class CheckingMutasi
    Private excel, excel2 As String
    Private dt_import_mutasi, dt_import_mutasi_temp, dt_import_mutasi_cancel, dt_import_mutasi_not_cancel, dt_import_payment, dt_table_temp As New DataTable
    Private dt_mutasi_row_principal, dt_mutasi_row_principal_can, dt_mutasi_row_intereset_can, dt_mutasi_row_intereset As DataRow
    Public SQL As New ConnectionSQL
    Private conn As OleDbConnection 'koneksi odb
    Private tableTemp As New DataTable
    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    Private RowCountPaymentFile, RowCountMutasiFile As Integer
    Private Sub ExportDataToExcel(ByRef gettable As DataTable, ByRef filename As String)
        Using out = New XLWorkbook()
            'out.Worksheets.Add(gettable)
            out.Worksheets.Add(gettable, "Sheet1")
            out.SaveAs(filename)
        End Using
    End Sub

    Private Sub CheckingMutasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cb_export.SelectedIndex = 0
    End Sub

    Private Sub cb_export_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_export.SelectedIndexChanged
        Try
            Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
            Dim strdate As String
            Dim filename1 As String = ""
            Dim extention As String = ".xlsx"
            getdate.ToString("ddMMyyyyHHmmss")
            strdate = getdate.ToString("dd") + "" + getdate.ToString("MM") + "" + getdate.ToString("yyyy") + "_" + getdate.ToString("HH") + "" + getdate.ToString("mm") + "" + getdate.ToString("ss")
            If cb_export.SelectedIndex.ToString = 0 Then
                'MessageBox.Show("Data can't export", "Alert")
            ElseIf cb_export.SelectedIndex.ToString = 1 Then
                'Data CIF Gagal
                filename1 = "D:\OUTPUT\Checking_Mutasi_Early_Payment" + strdate + extention
                Call ExportDataToExcel(dt_table_temp, filename1)
                MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public paymentCountColumns As Integer
    Private Sub btn_cleansing_Click(sender As Object, e As EventArgs) Handles btn_cleansing.Click
        Try
            'cleansing payment 
            'Dim tempMutasiTable As New DataTable
            Dim principal_can, interest_can As String
            'Dim date_process As String
            Dim reason_count As Integer
            Dim n, n_cancel As Integer
            If cb_product.SelectedIndex = 1 Then
                'checking payment early
                If dt_table_temp.Columns.Count <= 0 Then
                    For j = 0 To dt_import_payment.Columns.Count - 1
                        dt_table_temp.Columns.Add(dt_import_payment.Columns(j).ColumnName)
                    Next
                    dt_table_temp.Columns.Add("Check Principal")
                    dt_table_temp.Columns.Add("Principal Amount")

                    dt_table_temp.Columns.Add("Check Interest")
                    dt_table_temp.Columns.Add("Interest Amount")

                    dt_table_temp.Columns.Add("Status")
                    dt_table_temp.Columns.Add("Reason")
                    'dt_table_temp.Columns.Add("Date Process")
                End If
                For x As Integer = 0 To dt_import_payment.Rows.Count - 1
                    'count column 
                    paymentCountColumns = dt_import_payment.Columns.Count - 1
                    reason_count = 0
                    n = 0
                    'date_process = ""
                    n_cancel = 0
                    principal_can = ""
                    interest_can = ""
                    dt_table_temp.ImportRow(dt_import_payment.Rows(x))
                    If Not dt_import_mutasi_not_cancel Is Nothing Then
                        'checking payment to other file mutasi
                        'principal 28 column
                        dt_mutasi_row_principal = dt_import_mutasi_not_cancel.Select("[" + dt_import_mutasi_not_cancel.Columns(0).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(8).ToString + "' AND [" + dt_import_mutasi_not_cancel.Columns(1).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(28).ToString + "'").FirstOrDefault()

                        'check principal    
                        If Not dt_mutasi_row_principal Is Nothing Then
                            reason_count += 1
                            dt_table_temp.Rows(x).Item(paymentCountColumns + 1) = "OK"
                            dt_table_temp.Rows(x).Item(paymentCountColumns + 2) = dt_mutasi_row_principal.Item(1).ToString
                            'check cancelation amaount
                            principal_can = "-" + dt_mutasi_row_principal.Item(1).ToString
                        Else
                            dt_import_mutasi_not_cancel.DefaultView.Sort = "[" + dt_import_mutasi_not_cancel.Columns(1).ColumnName + "] DESC"
                            dt_import_mutasi_not_cancel = dt_import_mutasi_not_cancel.DefaultView.ToTable()
                            'filter and found row
                            dt_mutasi_row_principal = dt_import_mutasi_not_cancel.Select("[" + dt_import_mutasi_not_cancel.Columns(0).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(8).ToString + "'").FirstOrDefault()
                            If Not dt_mutasi_row_principal Is Nothing Then
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 1) = "NOT OK"
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 2) = dt_mutasi_row_principal.Item(1).ToString
                                'check cancelation amaount
                                principal_can = "-" + dt_mutasi_row_principal.Item(1).ToString
                            Else
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 1) = ""
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 2) = ""
                            End If
                        End If

                        'interest 29 column
                        dt_mutasi_row_intereset = dt_import_mutasi_not_cancel.Select("[" + dt_import_mutasi_not_cancel.Columns(0).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(8).ToString + "' AND [" + dt_import_mutasi_not_cancel.Columns(1).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(29).ToString + "' ").FirstOrDefault()
                        'check interet
                        If Not dt_mutasi_row_intereset Is Nothing Then
                            reason_count += 1
                            dt_table_temp.Rows(x).Item(paymentCountColumns + 3) = "OK"
                            dt_table_temp.Rows(x).Item(paymentCountColumns + 4) = dt_mutasi_row_intereset.Item(1).ToString
                            'check cancelation amaount
                            interest_can = "-" + dt_mutasi_row_intereset.Item(1).ToString
                        Else
                            dt_import_mutasi_not_cancel.DefaultView.Sort = "[" + dt_import_mutasi_not_cancel.Columns(1).ColumnName + "] ASC"
                            dt_import_mutasi_not_cancel = dt_import_mutasi_not_cancel.DefaultView.ToTable()
                            dt_mutasi_row_intereset = dt_import_mutasi_not_cancel.Select("[" + dt_import_mutasi_not_cancel.Columns(0).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(8).ToString + "'").FirstOrDefault()
                            If Not dt_mutasi_row_intereset Is Nothing Then
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 3) = "NOT OK"
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 4) = dt_mutasi_row_intereset.Item(1).ToString
                                'check cancelation amaount
                                interest_can = "-" + dt_mutasi_row_intereset.Item(1).ToString
                            Else
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 3) = ""
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 4) = ""
                            End If

                        End If
                        'date_process = dt_import_mutasi_not_cancel.Rows(x).Item(3).ToString
                        'dt_table_temp.Rows(x).Item(paymentCountColumns + 7) = "Processed on " + date_process.ToString
                    Else
                        'dt_table_temp.Rows(x).Item(paymentCountColumns + 7) = ""
                    End If

                    If reason_count > 1 Then
                        'check count checking 
                        For z As Integer = 0 To dt_import_mutasi_not_cancel.Rows.Count - 1
                            If String.Equals(dt_import_mutasi_not_cancel.Rows(z).Item(0).ToString, dt_import_payment.Rows(x).Item(8).ToString) Then
                                n += 1
                            End If
                        Next
                        dt_table_temp.Rows(x).Item(paymentCountColumns + 5) = "Matching(" + n.ToString + ")"
                        If n > 2 Then
                            If Not dt_import_mutasi_cancel Is Nothing Then
                                'check filter cancel amount
                                dt_mutasi_row_principal_can = dt_import_mutasi_cancel.Select("[" + dt_import_mutasi_cancel.Columns(0).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(8).ToString + "' AND [" + dt_import_mutasi_cancel.Columns(1).ColumnName + "] ='" + principal_can.ToString + "'").FirstOrDefault()
                                If Not dt_mutasi_row_principal_can Is Nothing Then
                                    'ada is aman
                                    For z As Integer = 0 To dt_import_mutasi_cancel.Rows.Count - 1
                                        If String.Equals(dt_import_mutasi_cancel.Rows(z).Item(0).ToString, dt_import_payment.Rows(x).Item(8).ToString) Then
                                            n_cancel += 1
                                        End If
                                    Next
                                Else
                                    'tidak ada is tidak aman
                                    n_cancel += 0
                                End If

                                dt_mutasi_row_intereset_can = dt_import_mutasi_cancel.Select("[" + dt_import_mutasi_cancel.Columns(0).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(8).ToString + "' AND [" + dt_import_mutasi_cancel.Columns(1).ColumnName + "] ='" + interest_can.ToString + "'").FirstOrDefault()

                                If Not dt_mutasi_row_intereset_can Is Nothing Then
                                    'ada is aman
                                    For z As Integer = 0 To dt_import_mutasi_cancel.Rows.Count - 1
                                        If String.Equals(dt_import_mutasi_cancel.Rows(z).Item(0).ToString, dt_import_payment.Rows(x).Item(8).ToString) Then
                                            n_cancel += 1
                                        End If
                                    Next
                                Else
                                    'tidak ada is tidak aman
                                    n_cancel += 0
                                End If
                            Else
                                'date_process = ""
                            End If
                            'date_process = dt_import_mutasi_cancel.Rows(x).Item(3).ToString
                            'dt_table_temp.Rows(x).Item(paymentCountColumns + 7) = "Cancelled on " + date_process.ToString
                        End If
                        If n = 3 Then
                            If n_cancel > 0 Then
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = "Cancelled"
                            Else
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = "Not Cancelled"
                            End If
                        ElseIf n > 3 Then
                            If n_cancel > 1 Then
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = "Cancelled"
                            Else
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = "Not Cancelled"
                            End If
                        Else
                            dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = ""
                        End If
                    ElseIf reason_count = 1 Then
                        'check count checking 
                        For z As Integer = 0 To dt_import_mutasi_not_cancel.Rows.Count - 1
                            If String.Equals(dt_import_mutasi_not_cancel.Rows(z).Item(0).ToString, dt_import_payment.Rows(x).Item(8).ToString) Then
                                n += 1
                            End If
                        Next

                        dt_table_temp.Rows(x).Item(paymentCountColumns + 5) = "Not Matching(" + n.ToString + ")"
                        If n > 2 Then
                            If Not dt_import_mutasi_cancel Is Nothing Then
                                'check filter cancel amount
                                dt_mutasi_row_principal_can = dt_import_mutasi_cancel.Select("[" + dt_import_mutasi_cancel.Columns(0).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(8).ToString + "' AND [" + dt_import_mutasi_cancel.Columns(1).ColumnName + "] ='" + principal_can.ToString + "'").FirstOrDefault()
                                If Not dt_mutasi_row_principal_can Is Nothing Then
                                    'ada is aman
                                    'check count checking 
                                    For z As Integer = 0 To dt_import_mutasi_cancel.Rows.Count - 1
                                        If String.Equals(dt_import_mutasi_cancel.Rows(z).Item(0).ToString, dt_import_payment.Rows(x).Item(8).ToString) Then
                                            n_cancel += 1
                                        End If
                                    Next
                                Else
                                    'tidak ada is tidak aman
                                    n_cancel += 0
                                End If

                                dt_mutasi_row_intereset_can = dt_import_mutasi_cancel.Select("[" + dt_import_mutasi_cancel.Columns(0).ColumnName + "] ='" + dt_import_payment.Rows(x).Item(8).ToString + "' AND [" + dt_import_mutasi_cancel.Columns(1).ColumnName + "] ='" + interest_can.ToString + "'").FirstOrDefault()

                                If Not dt_mutasi_row_intereset_can Is Nothing Then
                                    'ada is aman
                                    'check count checking 
                                    For z As Integer = 0 To dt_import_mutasi_cancel.Rows.Count - 1
                                        If String.Equals(dt_import_mutasi_cancel.Rows(z).Item(0).ToString, dt_import_payment.Rows(x).Item(8).ToString) Then
                                            n_cancel += 1
                                        End If
                                    Next
                                Else
                                    'tidak ada is tidak aman
                                    n_cancel += 0
                                End If
                            End If

                        End If
                        If n = 3 Then
                            If n_cancel > 0 Then
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = "Cancelled"
                            Else
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = "Not Cancelled"
                            End If
                        ElseIf n > 3 Then
                            If n_cancel > 1 Then
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = "Cancelled"
                            Else
                                dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = "Not Cancelled"
                            End If
                        Else
                            dt_table_temp.Rows(x).Item(paymentCountColumns + 6) = ""
                        End If
                    Else
                        dt_table_temp.Rows(x).Item(paymentCountColumns + 5) = "Not Processed"
                        'dt_table_temp.Rows(x).Item(paymentCountColumns + 7) = ""
                    End If

                    load_output.DataSource = dt_table_temp

                    C_rows1.Text = load_output.Rows.Count - 1
                    C_Cells1.Text = load_output.Columns.Count - 1
                Next


            ElseIf cb_product.SelectedIndex = 2 Then
                'checking cancellation
            ElseIf cb_product.SelectedIndex = 2 Then
                'checking byback
            End If
            MessageBox.Show("Checking Process is Done. ", "Alert")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn_file_import_Click(sender As Object, e As EventArgs) Handles btn_file_import.Click
        Try
            Dim ds As New DataSet()

            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                excel = fi.FullName
                conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
                conn.Open()
                'Name sheet in excel
                'Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                Dim myTableName As String = ""
                myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                Dim da As New OleDbDataAdapter(sqlquery, conn)
                'remove rows index 0
                da.Fill(ds)
                'event load data
                Application.DoEvents()
                If dt_import_payment.Rows.Count > 0 Then
                    'create new checking
                    'add data file cif to new row  
                    For x As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        'import row from another datatable
                        dt_import_payment.ImportRow(ds.Tables(0).Rows(x))
                    Next
                Else
                    dt_import_payment = ds.Tables(0)
                End If

                'sort data table in desc : menghindari data 0 paling atas 
                ' file from CS

                RowCountPaymentFile += ds.Tables(0).Rows.Count
                C_data1.Text = "(" + RowCountPaymentFile.ToString + ")"
                dt_import_payment.AcceptChanges()
                'load_output.DataSource = dt_import_payment
                conn.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub btn_mutasi_file_Click(sender As Object, e As EventArgs) Handles btn_mutasi_file.Click
        Try
            Dim ds As New DataSet()
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                excel2 = fi.FullName
                conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel2 + ";Extended Properties=Excel 12.0;")
                conn.Open()
                'Name sheet in excel
                'Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                Dim myTableName As String = ""
                'myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                myTableName = "sheetname1$"
                Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                Dim da As New OleDbDataAdapter(sqlquery, conn)
                'remove rows index 0
                da.Fill(ds)
                'event load data
                Application.DoEvents()
                If dt_import_mutasi.Rows.Count > 0 Then
                    'add data file cif to new row  
                    For x As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        'import row from another datatable
                        dt_import_mutasi.ImportRow(ds.Tables(0).Rows(x))
                    Next
                Else
                    dt_import_mutasi = ds.Tables(0)
                End If
                RowCountMutasiFile += ds.Tables(0).Rows.Count
                C_data2.Text = "(" + RowCountMutasiFile.ToString + ")"

                'filter data row in table
                Dim filterProcess As String = String.Format("[" + dt_import_mutasi.Columns(14).ColumnName + "] LIKE '*{0}*'", "N")
                Dim filter1 As DataRow() = dt_import_mutasi.Select(filterProcess)
                If Not filter1.Length = 0 Then
                    'load data process filter
                    dt_import_mutasi_not_cancel = filter1.CopyToDataTable()
                    dt_import_mutasi_not_cancel = createcleanPyament(dt_import_mutasi_not_cancel)
                    'cleansing row
                    dt_import_mutasi_not_cancel.AcceptChanges()
                Else
                    dt_import_mutasi_not_cancel = Nothing
                End If
                Dim filterProcessCancel As String = String.Format("[" + dt_import_mutasi.Columns(14).ColumnName + "] LIKE '*{0}*'", "Y")
                Dim filter2 As DataRow() = dt_import_mutasi.Select(filterProcessCancel)
                If Not filter2.Length = 0 Then
                    'load data cancel filter
                    dt_import_mutasi_cancel = filter2.CopyToDataTable()
                    dt_import_mutasi_cancel = createcleanCancelPayment(dt_import_mutasi_cancel)
                    dt_import_mutasi_cancel.AcceptChanges()
                Else
                    dt_import_mutasi_cancel = Nothing
                End If
                conn.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub
    Protected Function createcleanCancelPayment(ByRef table As DataTable) As DataTable
        'creat new datable ostanding
        Dim dt_import_mutasi_clean_can As New DataTable
        dt_import_mutasi_clean_can.Columns.Add("No_Rekening", GetType(String))
        dt_import_mutasi_clean_can.Columns.Add("Amount_Terhutang", GetType(Double))
        dt_import_mutasi_clean_can.Columns.Add("Key", GetType(String))
        'dt_import_mutasi_clean_can.Columns.Add("Date_Process", GetType(String))
        Dim reNoRekening As String
        Dim str_key, amount_key As String
        Dim rowTemp As DataRow
        reNoRekening = ""
        str_key = ""
        amount_key = ""
        'create new table row
        For x As Integer = 0 To table.Rows.Count - 1
            '-------NeW data row-------
            rowTemp = dt_import_mutasi_clean_can.NewRow()
            dt_import_mutasi_clean_can.Rows.Add(rowTemp)
            'cleansing no rekening
            Dim arrSymbol() As String = {",00", "-"}
            For i As Integer = 0 To arrSymbol.Length - 1
                reNoRekening = Trim(Replace(Convert.ToString(table.Rows(x).Item(13)), arrSymbol(i), ""))

                amount_key = Trim(Replace(table.Rows(x).Item(7), ",00", ""))
                amount_key = Trim(Replace(amount_key, ".", ""))
                amount_key = Convert.ToSingle(amount_key)
                str_key = reNoRekening + "" + amount_key.ToString()
            Next i
            dt_import_mutasi_clean_can.Rows(x).Item(0) = reNoRekening
            dt_import_mutasi_clean_can.Rows(x).Item(1) = amount_key
            dt_import_mutasi_clean_can.Rows(x).Item(2) = str_key
            'dt_import_mutasi_clean_can.Rows(x).Item(3) = Trim(Replace(table.Rows(x).Item(1), "-", ""))
        Next
        Return dt_import_mutasi_clean_can
    End Function
    Protected Function createcleanPyament(ByRef table As DataTable) As DataTable
        'creat new datable ostanding
        Dim dt_import_mutasi_clean As New DataTable
        dt_import_mutasi_clean.Columns.Add("No_Rekening", GetType(String))
        dt_import_mutasi_clean.Columns.Add("Amount_Terhutang", GetType(Double))
        dt_import_mutasi_clean.Columns.Add("Key", GetType(String))
        'dt_import_mutasi_clean.Columns.Add("Date_Process", GetType(String))
        Dim reNoRekening As String
        Dim str_key, amount_key As String
        Dim rowTemp As DataRow
        reNoRekening = ""
        str_key = ""
        amount_key = ""
        'create new table row
        For x As Integer = 0 To table.Rows.Count - 1
            '-------NeW data row-------
            rowTemp = dt_import_mutasi_clean.NewRow()
            dt_import_mutasi_clean.Rows.Add(rowTemp)
            'cleansing no rekening
            Dim arrSymbol() As String = {",00", "-"}
            For i As Integer = 0 To arrSymbol.Length - 1
                reNoRekening = Trim(Replace(Convert.ToString(table.Rows(x).Item(13)), arrSymbol(i), ""))

                amount_key = Trim(Replace(table.Rows(x).Item(7), ",00", ""))
                amount_key = Trim(Replace(amount_key, ".", ""))
                amount_key = Convert.ToSingle(amount_key)
                str_key = reNoRekening + "" + amount_key.ToString()
            Next i
            dt_import_mutasi_clean.Rows(x).Item(0) = reNoRekening
            dt_import_mutasi_clean.Rows(x).Item(1) = amount_key
            dt_import_mutasi_clean.Rows(x).Item(2) = str_key
            'dt_import_mutasi_clean.Rows(x).Item(3) = Trim(Replace(table.Rows(x).Item(1), "-", ""))
        Next
        Return dt_import_mutasi_clean
    End Function
End Class