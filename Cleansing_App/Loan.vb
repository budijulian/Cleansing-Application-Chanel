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

Public Class Loan
    Private cmd, cmd2 As New Data.SqlClient.SqlCommand
    Private ad As New Data.SqlClient.SqlDataAdapter
    Public SQL As New ConnectionSQL
    Private conn As OleDbConnection 'koneksi odb
    Private tableTemp As New DataTable
    Public tableNoCIF, tableCIF, tableCIFFinal As New DataTable
    Public tableLoan, tableCIFGagal As New DataTable
    Public tableSuccessLoan, tableTakedownLoan As New DataTable
    Private excel As String
    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    Private rowCIF, rowTemp, rowLoan, rowSuccesLoan, rowTakedownLoan, rowNoCIF, rowCIFGagal, resultCollect As DataRow
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Public getPdt As New accountPayments
    Dim takedownTemp As New DataTable
    Private amount_loan_final As Double
    Private amount_loan_takedown As Double
    Public dt_collec As New DataTable

    Private Sub Loan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'default
            cb_export.SelectedIndex = 0
            cb_import_cif.SelectedIndex = 0
            'get data product
            Dim getProducts As New DataTable
            Dim strProduct As String
            getProducts = getPdt.getAllProducts()
            For a = 0 To getProducts.Rows.Count - 1
                strProduct = "[" + (a + 1).ToString + "] " + getProducts.Rows(a).Item(1).ToString
                cb_product.Items.Add(strProduct.ToString)
            Next
            'select item join
            'process no cif
            If tableNoCIF.Rows.Count <= 0 Then
                cb_type_join.SelectedIndex = 0
                cb_product.SelectedIndex = 0
                tabcontrol1.TabPages.Remove(tb_report3)
                tabcontrol1.TabPages.Remove(tb_report4)
            Else
                Dim fm As New Loan With {.Owner = Me}
                cb_type_join.Text = fm.cb_type_join.Text
                cb_product.Text = fm.cb_product.Text
                tabcontrol1.TabPages.Remove(tb_report3)
                tabcontrol1.TabPages.Remove(tb_report4)
            End If

            Dim MyParentValue As Form1 = CType(Me.Owner, Form1)
            If Not IsNothing(MyParentValue) Then
                If tableCIF.Rows.Count = Nothing Then
                    'copy column to datatable
                    If tableCIF.Columns.Count <= 0 Then
                        For j = 0 To MyParentValue.TableFinal.Columns.Count - 1
                            tableCIF.Columns.Add(MyParentValue.TableFinal.Columns(j).ColumnName)
                            'load_cif.Columns.Add(MyParentValue.notDataFound.Columns(j).ColumnName, j)
                        Next
                        For x = 0 To MyParentValue.TableFinal.Rows.Count - 1
                            'import row from another datatable
                            tableCIF.ImportRow(MyParentValue.TableFinal.Rows(x))
                        Next
                        'change column name by index
                        tableCIF.Columns(79).ColumnName = "Multifinance_key_no"
                        tableCIF.AcceptChanges()
                        ' Disabled Button before process
                        ' Show Count Rows in Datagridview
                        If tableCIF.Columns.Count = 88 Then
                            C_data2.Text = "(OK)"
                        Else
                            C_data2.Text = "(False)"
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private RowCountCIF As Integer
    Private Sub btn_import_cif_Click(sender As Object, e As EventArgs) Handles btn_import_cif.Click

        Try
            Dim strFile As String
            Dim ds As New DataSet()
            'before clear datatable
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            'OpenFileDialog1.Filter = "Excel Files (*.xls)|*.xlsx|Text Files (*.txt)|*.txt"
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                'load event start
                Application.DoEvents()

                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                strFile = fi.FullName
                Dim extStr As String = Path.GetExtension(OpenFileDialog1.FileName)

                If extStr = ".xls" Or extStr = ".xlsx" Then
                    '(2) reader excel to Datatable
                    conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFile + ";Extended Properties=Excel 12.0;")
                    conn.Open()
                    Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                    Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                    Dim da As New OleDbDataAdapter(sqlquery, conn)
                    da.Fill(ds)

                    If tableCIF.Rows.Count > 0 Then
                        'add data file cif to new row  
                        For x As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            'import row from another datatable
                            tableCIF.ImportRow(ds.Tables(0).Rows(x))
                        Next
                    Else
                        tableCIF = ds.Tables(0)
                    End If

                    RowCountCIF += ds.Tables(0).Rows.Count
                    C_data2.Text = "(" + RowCountCIF.ToString + ")"
                    tableCIF.AcceptChanges()
                End If
                'change column name by index
                tableCIF.Columns(79).ColumnName = "Multifinance_key_no"
                tableCIF.AcceptChanges()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
            ' Disabled Button before process
            ' Show Count Rows in Datagridview
            If tableCIF.Columns.Count = 88 Then
                'C_data2.Text = "(OK)"
                'C_data2.Text = "(" + RowCountCIF.ToString + ")"
            Else
                C_data2.Text = "(False)"
                tableCIF.Reset()
            End If

        End Try
    End Sub

    Private Sub btn_import_loan_Click(sender As Object, e As EventArgs) Handles btn_import_loan.Click

        Try
            Dim ds As New DataSet()
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "CSV Files (*.csv)|*.csv"

            Dim CSVpath As String
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                tableLoan.Reset()
                'load event start
                Application.DoEvents()
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                CSVpath = fi.FullName

                '(2) reader csv to Datatable
                Dim SR As StreamReader = New StreamReader(CSVpath)
                Dim line As String = SR.ReadLine()
                Dim strArray As String() = line.Split(","c)
                Dim row As DataRow
                'column
                'For Each s As String In strArray
                '    tableLoan.Columns.Add(New DataColumn())
                'Next
                'call header Loan Column
                Call HeaderLoanColumn()

                Do
                    line = SR.ReadLine
                    If Not line = String.Empty Then
                        row = tableLoan.NewRow()
                        row.ItemArray = line.Split(","c)
                        tableLoan.Rows.Add(row)
                    Else
                        Exit Do
                    End If
                Loop
                'load_loan_takedown.DataSource = tableLoan
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            ' Disabled Button before process
            ' Show Count Rows in Datagridview
            If tableLoan.Columns.Count = 104 Then
                C_data3.Text = "(OK)"
            Else
                C_data3.Text = "(False)"
            End If

        End Try
    End Sub
    Private Sub HeaderLoanColumn()
        'column loan
        tableLoan.Columns.Add("BSI Code")
        tableLoan.Columns.Add("Approval Application Date")
        tableLoan.Columns.Add("Transaction Sequence No")
        tableLoan.Columns.Add("No Cif")
        tableLoan.Columns.Add("Scoring Model Type")
        tableLoan.Columns.Add("Application Type")
        tableLoan.Columns.Add("ID type")
        tableLoan.Columns.Add("ID NO")
        tableLoan.Columns.Add("Name")
        tableLoan.Columns.Add("Nationality")
        tableLoan.Columns.Add("Birth of Date")
        tableLoan.Columns.Add("Address")
        tableLoan.Columns.Add("Work Period")
        tableLoan.Columns.Add("Cell Number")
        tableLoan.Columns.Add("Marital Status Code")
        tableLoan.Columns.Add("Housing Type")
        tableLoan.Columns.Add("House Ownership")
        tableLoan.Columns.Add("Occupation Code 1 (large)")
        tableLoan.Columns.Add("Occupation Code 2 (middle)")
        tableLoan.Columns.Add("Occupation Code 3 (small)")
        tableLoan.Columns.Add("Position")
        tableLoan.Columns.Add("Division")
        tableLoan.Columns.Add("Company Establishment Date")
        tableLoan.Columns.Add("Employee Contract Type")
        tableLoan.Columns.Add("Product Type")
        tableLoan.Columns.Add("Currency")
        tableLoan.Columns.Add("Loan Application")
        tableLoan.Columns.Add("USD Converted Amount")
        tableLoan.Columns.Add("Loan Periode Code")
        tableLoan.Columns.Add("Period")
        tableLoan.Columns.Add("Expected Booking Date")
        tableLoan.Columns.Add("Grace Period")
        tableLoan.Columns.Add("Maturity Date")
        tableLoan.Columns.Add("Payment Method")
        tableLoan.Columns.Add("Multifinance companys BSI Customer Number")
        tableLoan.Columns.Add("Loan Plafond Type")
        tableLoan.Columns.Add("Loan Revolving Yn")
        tableLoan.Columns.Add("Interest Type")
        tableLoan.Columns.Add("Payment Method2")
        tableLoan.Columns.Add("Interest Type2")
        tableLoan.Columns.Add("Interest Period Type")
        tableLoan.Columns.Add("Interest Rate")
        tableLoan.Columns.Add("Loan Condition Changes")
        tableLoan.Columns.Add("Fixed Term Loan Maturity Code")
        tableLoan.Columns.Add("Fixed Loan Maturity Number")
        tableLoan.Columns.Add("First Fixed Interest Rate")
        tableLoan.Columns.Add("Restructuring YN")
        tableLoan.Columns.Add("Take Over YN")
        tableLoan.Columns.Add("Reference Source")
        tableLoan.Columns.Add("Economy Sector LBU")
        tableLoan.Columns.Add("LBU Use Reference")
        tableLoan.Columns.Add("LBU Loan Type")
        tableLoan.Columns.Add("LBU Debtor Group")
        tableLoan.Columns.Add("Loan Type Loan Application (LBU)")
        tableLoan.Columns.Add("LBU Use Type")
        tableLoan.Columns.Add("LBU Rating Agency")
        tableLoan.Columns.Add("Debtor Rating")
        tableLoan.Columns.Add("LBU Measurement Category")
        tableLoan.Columns.Add("LBU Portfolio Category")
        tableLoan.Columns.Add("LBU Debtor Category")
        tableLoan.Columns.Add("SID Economy Sector")
        tableLoan.Columns.Add("SID Use Type")
        tableLoan.Columns.Add("SID Guarantor Type")
        tableLoan.Columns.Add("SID Use Orientation")
        tableLoan.Columns.Add("SID Loan Characteristic")
        tableLoan.Columns.Add("SID Relationship with Bank")
        tableLoan.Columns.Add("SID Loan Group")
        tableLoan.Columns.Add("SID Debtor Group")
        tableLoan.Columns.Add("SID Debtor Status")
        tableLoan.Columns.Add("Company Project Location")
        tableLoan.Columns.Add("SID Debtor Group2")
        tableLoan.Columns.Add("LBU Relationship with Bank")
        tableLoan.Columns.Add("Total Income")
        tableLoan.Columns.Add("Employee Income")
        tableLoan.Columns.Add("Other Employee Income")
        tableLoan.Columns.Add("Total Expense")
        tableLoan.Columns.Add("Loan Installment Credit Card")
        tableLoan.Columns.Add("Loan Installment")
        tableLoan.Columns.Add("Credit Card Payment")
        tableLoan.Columns.Add("Daily Expense")
        tableLoan.Columns.Add("Other Expenses")
        tableLoan.Columns.Add("Net Income")
        tableLoan.Columns.Add("Loan Authority")
        tableLoan.Columns.Add("Credit Officer ID")
        tableLoan.Columns.Add("Representive Loan Application No")
        tableLoan.Columns.Add("Loan Application No")
        tableLoan.Columns.Add("Branch Code")
        tableLoan.Columns.Add("KPO Branch Head ID")
        tableLoan.Columns.Add("Master Status")
        tableLoan.Columns.Add("Teller ID")
        tableLoan.Columns.Add("Registered Date")
        tableLoan.Columns.Add("Registered Time")
        tableLoan.Columns.Add("Information Changer")
        tableLoan.Columns.Add("Information Changes Date")
        tableLoan.Columns.Add("Information Changes Time")
        tableLoan.Columns.Add("information Register Date")
        tableLoan.Columns.Add("Lastest Information Change Date")
        tableLoan.Columns.Add("Information Approval")
        tableLoan.Columns.Add("Multifinance Customer Key No")
        tableLoan.Columns.Add("Multifinance ACCOUNT Key No")
        tableLoan.Columns.Add("No Of Deed")
        tableLoan.Columns.Add("Multifinance Loan Starting Date")
        tableLoan.Columns.Add("Status")
        tableLoan.Columns.Add("Reason")
    End Sub

    Private Function checkDisbursement(ByRef multifinance_acc As String) As String
        Dim No_Cif As String = ""
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance_acc) Then
            No_Cif = "0"
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            cmd = New SqlCommand("SELECT TOP 1 [Date] ,[Transaction] ,[Multifinance CIF],[Multifinance Account],[SHBI CIF],[SHBI Account],[Amount],[Trx Type] FROM [DB_MASTER].[dbo].[DB_DISBURSEMENT_KRD] WHERE [Multifinance Account] = '" + multifinance_acc.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data Disburement
                No_Cif = checktable.Rows(0).Item(4).ToString()
            Else
                No_Cif = "0"
            End If
        End If
        Return No_Cif
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

    Protected Function GetDataCIFFromDisbursement(ByRef multifinance As String) As String
        Dim NOCIF As String = ""
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(multifinance) Then
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'Choose product for choose table
            If cb_product.SelectedIndex.ToString = 0 Then
                cmd2 = New SqlCommand("SELECT TOP 1 [SHBI CIF] FROM [DB_MASTER].[dbo].[DB_DISBURSEMENT_KRD] WHERE [Multifinance CIF] = '" + multifinance.ToString + "' ", SQLConnection)
            ElseIf cb_product.SelectedIndex.ToString = 1 Then
                GoTo ReturnOutput
            Else
                NOCIF = ""
                GoTo ReturnOutput
            End If
            Dim adapter As New SqlDataAdapter(cmd2)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data NO CIF 
                NOCIF = checktable.Rows(0).Item(0).ToString
                GoTo ReturnOutput
            Else
                NOCIF = ""
            End If
        End If
ReturnOutput:
        Return NOCIF
    End Function
    Private Sub btn_cleansing_Click(sender As Object, e As EventArgs) Handles btn_cleansing.Click
        Try
            Dim multiFinanceCode, IDCard, NoCIF As String
            Dim getDataMappingLoan As DataTable
            Dim result As DataRow
            Dim y As Integer = 0
            result = Nothing
            NoCIF = 0
            multiFinanceCode = ""
            If cb_product.Text = "--Choose Product--" Then
                MessageBox.Show("Please, Choose a product.!", "Alert")
            ElseIf cb_type_join.Text = "--Choose Join--" Then
                MessageBox.Show("Please, Choose a Type Join.!", "Alert")
            Else
                Dim pesan As MsgBoxResult
                'Declare variabel 
                pesan = MsgBox("Cleansing Start?", MsgBoxStyle.YesNo, "Alert")
                If pesan = MsgBoxResult.Yes Then
                    If tableCIF.Rows.Count > 0 Then
                        If cb_type_join.SelectedIndex.ToString = 0 Then
                            'join just NO CIF
                            load_cif_final.Columns.Clear()
                            load_cif_gagal.Columns.Clear()
                            'copy column to datatable cif final
                            If tableCIFFinal.Columns.Count <= 0 Then
                                For j = 0 To tableCIF.Columns.Count - 1
                                    tableCIFFinal.Columns.Add(tableCIF.Columns(j).ColumnName)
                                Next
                            End If
                            'copy column to datatable cif gagal
                            If tableCIFGagal.Columns.Count <= 0 Then
                                For j = 0 To tableCIF.Columns.Count - 1
                                    tableCIFGagal.Columns.Add(tableCIF.Columns(j).ColumnName)
                                Next
                            End If
                            'create new table row
                            'Dim z As Integer = 0
                            For x As Integer = 0 To tableCIF.Rows.Count - 1
                                'event load data
                                Application.DoEvents()
                                'Filter Data in table
                                'With index selected
                                If cb_product.SelectedIndex.ToString = 0 Then
                                    '----  FROM FILE CUSTOMERS SERVICE ---
                                    'GET MULTIFINANCE CODE IN TABLE CIF
                                    'file from 022 query
                                    If cb_import_cif.SelectedIndex.ToString = 1 Then
                                        multiFinanceCode = tableCIF.Rows(x).Item(79).ToString
                                        If Not String.IsNullOrWhiteSpace(multiFinanceCode) Then
                                            'select and filter data row
                                            result = tableNoCIF.Select("[" + tableNoCIF.Columns(133).ColumnName + "] ='" + multiFinanceCode.ToString + "' ").FirstOrDefault()
                                        Else
                                            IDCard = ""
                                        End If

                                    ElseIf cb_import_cif.SelectedIndex.ToString = 2 Then
                                        'file from CS Team
                                        multiFinanceCode = tableCIF.Rows(x).Item(79).ToString
                                        tableNoCIF.DefaultView.Sort = "[" + tableNoCIF.Columns(1).ColumnName + "] DESC"
                                        tableNoCIF = tableNoCIF.DefaultView.ToTable()
                                        'select and filter data row
                                        result = tableNoCIF.Select("[" + tableNoCIF.Columns(78).ColumnName + "] ='" + multiFinanceCode.ToString + "' ").FirstOrDefault()

                                    End If

                                    'check data row is fill or not
                                    If Not result Is Nothing Then
                                        'file from 022 query
                                        If cb_import_cif.SelectedIndex.ToString = 1 Then
                                            NoCIF = result.Item(134).ToString()
                                        ElseIf cb_import_cif.SelectedIndex.ToString = 2 Then
                                            'file from CS Team
                                            NoCIF = result.Item(1).ToString()
                                        End If

                                        If Not NoCIF = "0" Then
                                            'send to new row in table cif
                                            tableCIF.Rows(x).Item(2) = NoCIF
                                            tableCIFFinal.ImportRow(tableCIF.Rows(x))
                                        ElseIf NoCIF = "0" Or NoCIF = "" Then
                                            'check data disbursement historycals
                                            'file from CS Team
                                            If cb_import_cif.SelectedIndex.ToString = 2 Then
                                                Dim getNOCIF As String = GetDataCIFFromDisbursement(multiFinanceCode)
                                                If Not getNOCIF = "" Then
                                                    tableCIF.Rows(x).Item(2) = getNOCIF
                                                    tableCIFFinal.ImportRow(tableCIF.Rows(x))
                                                Else
                                                    'cleansing row not found
                                                    'set to datable takedown after cleansing
                                                    tableCIFGagal.ImportRow(tableCIF.Rows(x))
                                                End If
                                            Else
                                                'cleansing row not found
                                                'set to datable takedown after cleansing
                                                tableCIFGagal.ImportRow(tableCIF.Rows(x))
                                            End If
                                        End If
                                    Else
                                        'cleansing row not found
                                        'set to datable takedown after cleansing
                                        tableCIFGagal.ImportRow(tableCIF.Rows(x))
                                    End If

                                Else
                                    '---- FROM MENU 7023 QUERY 022 ----
                                    ' INDEX ID CARD : 137 in FILE HEADER (0)
                                    ' INDEX NO CIF : 135 in FILE HEADER (0)
                                    If cb_import_cif.SelectedIndex.ToString = 1 Then
                                        multiFinanceCode = tableCIF.Rows(x).Item(79).ToString
                                        result = tableNoCIF.Select("[" + tableNoCIF.Columns(133).ColumnName + "] ='" + multiFinanceCode.ToString + "'").FirstOrDefault()

                                    ElseIf cb_import_cif.SelectedIndex.ToString = 2 Then
                                        'file from CS Team
                                        multiFinanceCode = tableCIF.Rows(x).Item(79).ToString
                                        tableNoCIF.DefaultView.Sort = "[" + tableNoCIF.Columns(1).ColumnName + "] DESC"
                                        tableNoCIF = tableNoCIF.DefaultView.ToTable()
                                        'select and filter data row
                                        result = tableNoCIF.Select("[" + tableNoCIF.Columns(78).ColumnName + "] ='" + multiFinanceCode.ToString + "' ").FirstOrDefault()
                                    End If

                                    If Not result Is Nothing Then
                                        'file from 022 query
                                        If cb_import_cif.SelectedIndex.ToString = 1 Then
                                            NoCIF = result.Item(134).ToString()
                                        ElseIf cb_import_cif.SelectedIndex.ToString = 2 Then
                                            'file from CS Team
                                            NoCIF = result.Item(1).ToString()
                                        End If

                                        If Not NoCIF = "0" Then
                                            'send to new row in table cif
                                            tableCIF.Rows(x).Item(2) = NoCIF
                                            'check data collection for takeout loan
                                            If dt_collec.Rows.Count <= 0 Then
                                                tableCIFFinal.ImportRow(tableCIF.Rows(x))
                                            Else
                                                tableCIFFinal.ImportRow(tableCIF.Rows(x))
                                            End If
                                        ElseIf NoCIF = "0" And NoCIF = "" Then
                                            'cleansing row not found
                                            'set to datable takedown after cleansing
                                            tableCIFGagal.ImportRow(tableCIF.Rows(x))
                                        End If
                                    Else
                                        'cleansing row not found
                                        'set to datable takedown after cleansing
                                        tableCIFGagal.ImportRow(tableCIF.Rows(x))
                                    End If
                                End If

                                'remove row duplicate in datable because it will cif to reupload again
                                tableCIFGagal = DeleteDuplicateFromDataTable(tableCIFGagal, "Multifinance_key_no")
                                'send to datagridview cif final
                                load_cif_final.DataSource = tableCIFFinal
                                'send to datagridview cif gagal
                                load_cif_gagal.DataSource = tableCIFGagal

                                ' Show Count Rows in Datagridview
                                C_Rows1.Text = load_cif_final.RowCount - 1
                                C_Rows2.Text = load_cif_gagal.RowCount - 1
                            Next


                            'notif after cleansing process
                            'MessageBox.Show("Join CIF is Done.", "Alert")
                            'select tab
                            tabcontrol1.SelectedTab = tb_report1
                        ElseIf cb_type_join.SelectedIndex.ToString = 1 Then
                            'join NOCIF to Loan

                            'clear rows
                            load_loan_takedown.Columns.Clear()
                            load_loan_success.Columns.Clear()

                            'copy column to datatable success
                            If tableSuccessLoan.Columns.Count <= 0 Then
                                'column has 103 column
                                For j = 0 To tableLoan.Columns.Count - 2
                                    tableSuccessLoan.Columns.Add(tableLoan.Columns(j).ColumnName)
                                Next
                            End If
                            'copy column to datatable takedown loan
                            If tableTakedownLoan.Columns.Count <= 0 Then
                                For j = 0 To tableLoan.Columns.Count - 1
                                    tableTakedownLoan.Columns.Add(tableLoan.Columns(j).ColumnName)
                                Next
                            End If
                            'copy column to datatable cif gagal
                            If tableCIFGagal.Columns.Count <= 0 Then
                                For j = 0 To tableCIF.Columns.Count - 1
                                    tableCIFGagal.Columns.Add(tableCIF.Columns(j).ColumnName)
                                Next
                            End If

                            'create new table row
                            'Dim z As Integer = 0
                            For x As Integer = 0 To tableLoan.Rows.Count - 1
                                If Not String.IsNullOrWhiteSpace(tableLoan.Rows(x).Item(98).ToString()) Then
                                    'Filter Data in table
                                    'With index selected
                                    multiFinanceCode = tableLoan.Rows(x).Item(98).ToString
                                    result = tableCIF.Select("Multifinance_key_no = '" + multiFinanceCode.ToString + "'").FirstOrDefault()
                                    'check data row is fill or not
                                    If Not result Is Nothing Then

                                        'send to new row in table cif                           
                                        'send to new row in table success
                                        rowTemp = tableSuccessLoan.NewRow()
                                        tableSuccessLoan.Rows.Add(rowTemp)
                                        'Add new Data Temp to New Data After Cleansing
                                        tableSuccessLoan.Rows(y).Item(0) = tableLoan.Rows(x).Item(0).ToString
                                        'change date 
                                        Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                                        Dim strdate As String = getdate.ToString("ddMMyyyyHHmmss")
                                        strdate = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + getdate.ToString("dd")
                                        '(1) Change Column Approved Application Date is today
                                        tableSuccessLoan.Rows(y).Item(1) = strdate.ToString
                                        tableSuccessLoan.Rows(y).Item(2) = tableLoan.Rows(x).Item(2).ToString.ToUpper()
                                        '(2) Change and Run to NO CIF to TableSuccessLoan is not nothing

                                        ' File NO CIF is found
                                        'Column NO CIF : 1, Column Multifinance : 78                        
                                        'select data in datatable CIF with No CIF
                                        '(3) without NO CIF
                                        If tableNoCIF.Rows.Count <= 0 Then
                                            NoCIF = ""
                                        Else
                                            If cb_import_cif.SelectedIndex.ToString = 1 Then
                                                '---- FROM MENU 7023 QUERY 022 ----
                                                ' INDEX ID CARD : 137 in FILE HEADER (0)
                                                ' INDEX NO CIF : 135 in FILE HEADER (0)
                                                rowNoCIF = tableNoCIF.Select("[" + tableNoCIF.Columns(133).ColumnName + "] ='" + multiFinanceCode.ToString + "'").FirstOrDefault()

                                            ElseIf cb_import_cif.SelectedIndex.ToString = 2 Then
                                                '----  FROM FILE CUSTOMERS SERVICE ---
                                                'check no CIF in file CIF to send to loan
                                                rowNoCIF = tableNoCIF.Select("[" + tableNoCIF.Columns(78).ColumnName + "] ='" + multiFinanceCode.ToString + "'").FirstOrDefault()
                                            End If

                                            If IsNothing(rowNoCIF) Then
                                                'if  have not data multi minance in table NO CIF, send to "table Gagal CIF
                                                NoCIF = "0"
                                                'send rows data CIF to data CIF Gagal
                                                rowCIFGagal = tableCIF.Select("[" + tableCIF.Columns(79).ColumnName + "] ='" + multiFinanceCode.ToString + "'").FirstOrDefault()
                                                tableCIFGagal.ImportRow(rowCIFGagal)
                                            Else
                                                If cb_import_cif.SelectedIndex.ToString = 1 Then
                                                    '---- FROM MENU 7023 QUERY 022 ----
                                                    ' INDEX ID CARD : 137 in FILE HEADER (0)
                                                    ' INDEX NO CIF : 135 in FILE HEADER (0)
                                                    'set NO CIF in Data Loan
                                                    If rowNoCIF.Item(134).ToString = "0" Or rowNoCIF.Item(134).ToString = "" Then
                                                        'check to database disbure CIF
                                                        '(1) database with product
                                                        'set NO CIF not FOund
                                                        'select data in datatable CIF
                                                        rowCIFGagal = tableCIF.Select("[" + tableCIF.Columns(79).ColumnName + "] ='" + multiFinanceCode.ToString + "'").FirstOrDefault()
                                                        tableCIFGagal.ImportRow(rowCIFGagal)
                                                        NoCIF = "0"
                                                    Else
                                                        NoCIF = rowNoCIF.Item(134).ToString
                                                    End If

                                                ElseIf cb_import_cif.SelectedIndex.ToString = 2 Then
                                                    If rowNoCIF.Item(1).ToString = "0" Or rowNoCIF.Item(1).ToString = "" Then
                                                        'check to database disbure CIF
                                                        '(1) database with product
                                                        If cb_product.SelectedIndex.ToString = 0 Then
                                                            'get data from disubrement historical
                                                            NoCIF = GetDataCIFFromDisbursement(multiFinanceCode.ToString)
                                                            'NOCIF Not FOUND
                                                            If NoCIF = "" Then
                                                                'set NO CIF not FOund
                                                                'select data in datatable CIF
                                                                rowCIFGagal = tableCIF.Select("[" + tableCIF.Columns(79).ColumnName + "] ='" + multiFinanceCode.ToString + "'").FirstOrDefault()
                                                                tableCIFGagal.ImportRow(rowCIFGagal)
                                                                NoCIF = "0"
                                                            End If
                                                        ElseIf cb_product.SelectedIndex.ToString = 1 Then
                                                            ''NOCIF Not FOUND
                                                            rowCIFGagal = tableCIF.Select("[" + tableCIF.Columns(79).ColumnName + "] ='" + multiFinanceCode.ToString + "'").FirstOrDefault()
                                                            tableCIFGagal.ImportRow(rowCIFGagal)
                                                            NoCIF = "0"
                                                        End If

                                                    Else
                                                        'nocif found
                                                        NoCIF = rowNoCIF.Item(1).ToString

                                                    End If
                                                End If
                                            End If
                                        End If

                                        'check data loan kolek in file
                                        'check data collection for takeout loan
                                        If Not dt_collec.Rows.Count <= 0 Then
                                            'Expected Column [CIF] ,[Kolektivitas]
                                            resultCollect = dt_collec.Select("[" + dt_collec.Columns(0).ColumnName + "] ='" + NoCIF.ToString + "' ").FirstOrDefault()
                                            'add info
                                            'add table takedown                  
                                            If Not resultCollect Is Nothing Then
                                                'add data summary to table temp
                                                tableLoan.Rows(x).Item(103) = "Take Out-Kolektibilitas " + resultCollect.Item(1).ToString
                                                tableTakedownLoan.ImportRow(tableLoan.Rows(x))
                                                Console.WriteLine("koelek:" + NoCIF.ToString)
                                                'jump to process
                                                GoTo endJoinLoanToCIF
                                            Else
                                                tableSuccessLoan.Rows(y).Item(3) = NoCIF
                                                tableSuccessLoan.Rows(y).Item(4) = tableLoan.Rows(x).Item(4)
                                                tableSuccessLoan.Rows(y).Item(5) = tableLoan.Rows(x).Item(5)
                                                tableSuccessLoan.Rows(y).Item(6) = tableLoan.Rows(x).Item(6)
                                                '(3) Change Column IDCard from CIF File
                                                tableSuccessLoan.Rows(y).Item(7) = result.Item(14).ToString.ToUpper()
                                                '(4) Change Column Name IDCard from CIF File
                                                tableSuccessLoan.Rows(y).Item(8) = result.Item(5).ToString.ToUpper()

                                                tableSuccessLoan.Rows(y).Item(9) = tableLoan.Rows(x).Item(9).ToString
                                                tableSuccessLoan.Rows(y).Item(10) = tableLoan.Rows(x).Item(10).ToString
                                                tableSuccessLoan.Rows(y).Item(11) = tableLoan.Rows(x).Item(11).ToString
                                                tableSuccessLoan.Rows(y).Item(12) = tableLoan.Rows(x).Item(12).ToString
                                                tableSuccessLoan.Rows(y).Item(13) = tableLoan.Rows(x).Item(13).ToString
                                                tableSuccessLoan.Rows(y).Item(14) = tableLoan.Rows(x).Item(14).ToString
                                                tableSuccessLoan.Rows(y).Item(15) = tableLoan.Rows(x).Item(15).ToString
                                                tableSuccessLoan.Rows(y).Item(16) = tableLoan.Rows(x).Item(16).ToString
                                                '(5) Change Column Occupation to Devision from Table CIF Select Filter
                                                If cb_product.SelectedIndex.ToString = 2 Then
                                                    'akulaku product
                                                    tableSuccessLoan.Rows(y).Item(17) = tableLoan.Rows(x).Item(17).ToString.ToUpper()
                                                    tableSuccessLoan.Rows(y).Item(18) = tableLoan.Rows(x).Item(18).ToString.ToUpper()
                                                    tableSuccessLoan.Rows(y).Item(19) = tableLoan.Rows(x).Item(19).ToString.ToUpper()
                                                    tableSuccessLoan.Rows(y).Item(20) = tableLoan.Rows(x).Item(20).ToString.ToUpper()
                                                    tableSuccessLoan.Rows(y).Item(21) = tableLoan.Rows(x).Item(21).ToString.ToUpper()
                                                Else
                                                    'product kredivo 
                                                    Call SetOccupation(result.Item(39).ToString.ToUpper(), y, x, 17, 18, 19, 20, 21)
                                                End If

                                                tableSuccessLoan.Rows(y).Item(22) = tableLoan.Rows(x).Item(22).ToString.ToUpper()
                                                tableSuccessLoan.Rows(y).Item(23) = tableLoan.Rows(x).Item(23).ToString.ToUpper()
                                                tableSuccessLoan.Rows(y).Item(24) = tableLoan.Rows(x).Item(24).ToString.ToUpper()
                                                tableSuccessLoan.Rows(y).Item(25) = tableLoan.Rows(x).Item(25).ToString.ToUpper()
                                                'Insert Amount 
                                                tableSuccessLoan.Rows(y).Item(26) = tableLoan.Rows(x).Item(26).ToString
                                                'Insert amount
                                                amount_loan_final += Convert.ToDouble(tableLoan.Rows(x).Item(26))
                                                tableSuccessLoan.Rows(y).Item(27) = tableLoan.Rows(x).Item(27).ToString
                                                tableSuccessLoan.Rows(y).Item(28) = tableLoan.Rows(x).Item(28).ToString
                                                tableSuccessLoan.Rows(y).Item(29) = tableLoan.Rows(x).Item(29).ToString
                                                '(6) Change Column Expected Booking Date is today
                                                If cb_product.SelectedIndex.ToString = 2 Then
                                                    'product akulaku 
                                                    'expected akulaku always on 28 any month
                                                    Dim strdate2 As String = getdate.ToString("MMyyyyHHmmss")
                                                    strdate2 = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + "28"
                                                    tableSuccessLoan.Rows(y).Item(30) = strdate2.ToString
                                                Else
                                                    tableSuccessLoan.Rows(y).Item(30) = strdate.ToString
                                                End If
                                                tableSuccessLoan.Rows(y).Item(31) = tableLoan.Rows(x).Item(31).ToString
                                                tableSuccessLoan.Rows(y).Item(32) = tableLoan.Rows(x).Item(32).ToString
                                                tableSuccessLoan.Rows(y).Item(33) = tableLoan.Rows(x).Item(33).ToString
                                                tableSuccessLoan.Rows(y).Item(34) = tableLoan.Rows(x).Item(34).ToString
                                                tableSuccessLoan.Rows(y).Item(35) = tableLoan.Rows(x).Item(35).ToString
                                                tableSuccessLoan.Rows(y).Item(36) = tableLoan.Rows(x).Item(36).ToString
                                                tableSuccessLoan.Rows(y).Item(37) = Int(tableLoan.Rows(x).Item(37)).ToString
                                                tableSuccessLoan.Rows(y).Item(38) = tableLoan.Rows(x).Item(38).ToString
                                                tableSuccessLoan.Rows(y).Item(39) = tableLoan.Rows(x).Item(39).ToString
                                                tableSuccessLoan.Rows(y).Item(40) = tableLoan.Rows(x).Item(40).ToString
                                                'GET INTEREST PRODUCT
                                                tableSuccessLoan.Rows(y).Item(41) = getInterestProduct(cb_product.SelectedIndex.ToString).ToString

                                                tableSuccessLoan.Rows(y).Item(42) = tableLoan.Rows(x).Item(42).ToString
                                                tableSuccessLoan.Rows(y).Item(43) = tableLoan.Rows(x).Item(43).ToString
                                                tableSuccessLoan.Rows(y).Item(44) = tableLoan.Rows(x).Item(44).ToString
                                                tableSuccessLoan.Rows(y).Item(45) = tableLoan.Rows(x).Item(45).ToString
                                                tableSuccessLoan.Rows(y).Item(46) = tableLoan.Rows(x).Item(46).ToString
                                                tableSuccessLoan.Rows(y).Item(47) = tableLoan.Rows(x).Item(47).ToString

                                                '(7) Change Column Reference Source until SID Debtor Status And SID Debtor Group, LBU Relationship with Bank
                                                'And Branch Code, KPO Branch Head ID
                                                '25 Column 
                                                'Call Function SetDefaultDataMappingLoan
                                                'type akulaku product
                                                If cb_product.SelectedIndex.ToString = 2 Then
                                                    tableSuccessLoan.Rows(y).Item(48) = tableLoan.Rows(x).Item(48).ToString
                                                    tableSuccessLoan.Rows(y).Item(49) = tableLoan.Rows(x).Item(49).ToString
                                                    tableSuccessLoan.Rows(y).Item(50) = tableLoan.Rows(x).Item(50).ToString
                                                    tableSuccessLoan.Rows(y).Item(51) = tableLoan.Rows(x).Item(51).ToString
                                                    tableSuccessLoan.Rows(y).Item(52) = tableLoan.Rows(x).Item(52).ToString
                                                    tableSuccessLoan.Rows(y).Item(53) = tableLoan.Rows(x).Item(53).ToString
                                                    tableSuccessLoan.Rows(y).Item(54) = tableLoan.Rows(x).Item(54).ToString
                                                    tableSuccessLoan.Rows(y).Item(55) = tableLoan.Rows(x).Item(55).ToString
                                                    tableSuccessLoan.Rows(y).Item(56) = tableLoan.Rows(x).Item(56).ToString
                                                    tableSuccessLoan.Rows(y).Item(57) = tableLoan.Rows(x).Item(57).ToString
                                                    tableSuccessLoan.Rows(y).Item(58) = tableLoan.Rows(x).Item(58).ToString
                                                    tableSuccessLoan.Rows(y).Item(59) = tableLoan.Rows(x).Item(59).ToString
                                                    tableSuccessLoan.Rows(y).Item(60) = tableLoan.Rows(x).Item(60).ToString
                                                    tableSuccessLoan.Rows(y).Item(61) = tableLoan.Rows(x).Item(61).ToString
                                                    tableSuccessLoan.Rows(y).Item(62) = tableLoan.Rows(x).Item(62).ToString
                                                    tableSuccessLoan.Rows(y).Item(63) = tableLoan.Rows(x).Item(63).ToString
                                                    tableSuccessLoan.Rows(y).Item(64) = tableLoan.Rows(x).Item(64).ToString
                                                    tableSuccessLoan.Rows(y).Item(65) = tableLoan.Rows(x).Item(65).ToString
                                                    tableSuccessLoan.Rows(y).Item(66) = tableLoan.Rows(x).Item(66).ToString
                                                    tableSuccessLoan.Rows(y).Item(67) = tableLoan.Rows(x).Item(67).ToString
                                                    tableSuccessLoan.Rows(y).Item(68) = tableLoan.Rows(x).Item(68).ToString
                                                    'Column [SID Debtor Group1] ,[LBU Relationship with Bank]
                                                    tableSuccessLoan.Rows(y).Item(70) = tableLoan.Rows(x).Item(70).ToString
                                                    tableSuccessLoan.Rows(y).Item(71) = tableLoan.Rows(x).Item(71).ToString
                                                    'Column [Branch Code] ,[KPO Branch Head ID]
                                                    tableSuccessLoan.Rows(y).Item(86) = tableLoan.Rows(x).Item(86).ToString
                                                    tableSuccessLoan.Rows(y).Item(87) = tableLoan.Rows(x).Item(87).ToString
                                                    '(8) Change Column Company Project Location 
                                                    tableSuccessLoan.Rows(y).Item(69) = tableLoan.Rows(x).Item(69).ToString
                                                Else
                                                    getDataMappingLoan = SetDefaultDataMappingLoan(cb_product.SelectedIndex.ToString)
                                                    If getDataMappingLoan.Rows.Count > 0 Then
                                                        '[Reference Source] ,[Economy Sector LBU] ,[LBU Use Reference] ,[LBU Loan Type] ,[LBU Debtor Group],[Loan Type - LBU] ,[LBU Use Type] ,[LBU Rating Agency] ,[Debtor Rating],[LBU Measurement Category],[LBU Portfolio Category],[LBU Debtor Category],[SID Economy Sector] ,[SID Use Type],[SID Guarantor Type] ,[SID Use Orientation], [SID Loan Characteristic],[SID Relationship with Bank],[SID Loan Group],[SID Debtor Group],[SID Debtor Status] ,[SID Debtor Group1] ,[LBU Relationship with Bank] ,[Branch Code] ,[KPO Branch Head ID]
                                                        'Column [Reference Source] until [SID Debtor Status]
                                                        tableSuccessLoan.Rows(y).Item(48) = getDataMappingLoan.Rows(0).Item(0).ToString
                                                        tableSuccessLoan.Rows(y).Item(49) = getDataMappingLoan.Rows(0).Item(1).ToString
                                                        tableSuccessLoan.Rows(y).Item(50) = getDataMappingLoan.Rows(0).Item(2).ToString
                                                        tableSuccessLoan.Rows(y).Item(51) = getDataMappingLoan.Rows(0).Item(3).ToString
                                                        tableSuccessLoan.Rows(y).Item(52) = getDataMappingLoan.Rows(0).Item(4).ToString
                                                        tableSuccessLoan.Rows(y).Item(53) = getDataMappingLoan.Rows(0).Item(5).ToString
                                                        tableSuccessLoan.Rows(y).Item(54) = getDataMappingLoan.Rows(0).Item(6).ToString
                                                        tableSuccessLoan.Rows(y).Item(55) = getDataMappingLoan.Rows(0).Item(7).ToString
                                                        tableSuccessLoan.Rows(y).Item(56) = getDataMappingLoan.Rows(0).Item(8).ToString
                                                        tableSuccessLoan.Rows(y).Item(57) = getDataMappingLoan.Rows(0).Item(9).ToString
                                                        tableSuccessLoan.Rows(y).Item(58) = getDataMappingLoan.Rows(0).Item(10).ToString
                                                        tableSuccessLoan.Rows(y).Item(59) = getDataMappingLoan.Rows(0).Item(11).ToString
                                                        tableSuccessLoan.Rows(y).Item(60) = getDataMappingLoan.Rows(0).Item(12).ToString
                                                        tableSuccessLoan.Rows(y).Item(61) = getDataMappingLoan.Rows(0).Item(13).ToString
                                                        tableSuccessLoan.Rows(y).Item(62) = getDataMappingLoan.Rows(0).Item(14).ToString
                                                        tableSuccessLoan.Rows(y).Item(63) = getDataMappingLoan.Rows(0).Item(15).ToString
                                                        tableSuccessLoan.Rows(y).Item(64) = getDataMappingLoan.Rows(0).Item(16).ToString
                                                        tableSuccessLoan.Rows(y).Item(65) = getDataMappingLoan.Rows(0).Item(17).ToString
                                                        tableSuccessLoan.Rows(y).Item(66) = getDataMappingLoan.Rows(0).Item(18).ToString
                                                        tableSuccessLoan.Rows(y).Item(67) = getDataMappingLoan.Rows(0).Item(19).ToString
                                                        tableSuccessLoan.Rows(y).Item(68) = getDataMappingLoan.Rows(0).Item(20).ToString
                                                        'Column [SID Debtor Group1] ,[LBU Relationship with Bank]
                                                        tableSuccessLoan.Rows(y).Item(70) = getDataMappingLoan.Rows(0).Item(21).ToString
                                                        tableSuccessLoan.Rows(y).Item(71) = getDataMappingLoan.Rows(0).Item(22)
                                                        'Column [Branch Code] ,[KPO Branch Head ID]
                                                        tableSuccessLoan.Rows(y).Item(86) = getDataMappingLoan.Rows(0).Item(23).ToString
                                                        tableSuccessLoan.Rows(y).Item(87) = getDataMappingLoan.Rows(0).Item(24).ToString
                                                    End If
                                                    '(8) Change Column Company Project Location is Dati Office from Data CIF
                                                    tableSuccessLoan.Rows(y).Item(69) = result.Item(56).ToString

                                                    tableSuccessLoan.Rows(y).Item(72) = tableLoan.Rows(x).Item(72).ToString
                                                    tableSuccessLoan.Rows(y).Item(73) = tableLoan.Rows(x).Item(73).ToString
                                                    tableSuccessLoan.Rows(y).Item(74) = tableLoan.Rows(x).Item(74).ToString
                                                    tableSuccessLoan.Rows(y).Item(75) = tableLoan.Rows(x).Item(75).ToString
                                                    tableSuccessLoan.Rows(y).Item(76) = tableLoan.Rows(x).Item(76).ToString
                                                    tableSuccessLoan.Rows(y).Item(77) = tableLoan.Rows(x).Item(77).ToString
                                                    tableSuccessLoan.Rows(y).Item(78) = tableLoan.Rows(x).Item(78).ToString
                                                    tableSuccessLoan.Rows(y).Item(79) = tableLoan.Rows(x).Item(79).ToString
                                                    tableSuccessLoan.Rows(y).Item(80) = tableLoan.Rows(x).Item(80).ToString
                                                    tableSuccessLoan.Rows(y).Item(81) = tableLoan.Rows(x).Item(81).ToString
                                                    tableSuccessLoan.Rows(y).Item(82) = tableLoan.Rows(x).Item(82).ToString
                                                    tableSuccessLoan.Rows(y).Item(83) = tableLoan.Rows(x).Item(83).ToString
                                                    tableSuccessLoan.Rows(y).Item(84) = tableLoan.Rows(x).Item(84).ToString
                                                    tableSuccessLoan.Rows(y).Item(85) = tableLoan.Rows(x).Item(85).ToString
                                                    '(9) change Column data from CIF 86 and 87
                                                    tableSuccessLoan.Rows(y).Item(88) = tableLoan.Rows(x).Item(88).ToString
                                                    tableSuccessLoan.Rows(y).Item(89) = tableLoan.Rows(x).Item(89).ToString
                                                    tableSuccessLoan.Rows(y).Item(90) = tableLoan.Rows(x).Item(90).ToString
                                                    tableSuccessLoan.Rows(y).Item(91) = tableLoan.Rows(x).Item(91).ToString
                                                    tableSuccessLoan.Rows(y).Item(92) = tableLoan.Rows(x).Item(92).ToString
                                                    tableSuccessLoan.Rows(y).Item(93) = tableLoan.Rows(x).Item(93).ToString
                                                    tableSuccessLoan.Rows(y).Item(94) = tableLoan.Rows(x).Item(94).ToString
                                                    tableSuccessLoan.Rows(y).Item(95) = tableLoan.Rows(x).Item(95).ToString
                                                    tableSuccessLoan.Rows(y).Item(96) = tableLoan.Rows(x).Item(96).ToString
                                                    tableSuccessLoan.Rows(y).Item(97) = tableLoan.Rows(x).Item(97).ToString
                                                    tableSuccessLoan.Rows(y).Item(98) = tableLoan.Rows(x).Item(98).ToString
                                                    tableSuccessLoan.Rows(y).Item(99) = tableLoan.Rows(x).Item(99).ToString
                                                    tableSuccessLoan.Rows(y).Item(100) = tableLoan.Rows(x).Item(100).ToString
                                                    tableSuccessLoan.Rows(y).Item(101) = tableLoan.Rows(x).Item(101).ToString
                                                    tableSuccessLoan.Rows(y).Item(102) = tableLoan.Rows(x).Item(102).ToString

                                                    'plus point
                                                    y += 1

                                                End If
                                            End If
                                        Else

                                            tableSuccessLoan.Rows(y).Item(3) = NoCIF
                                            tableSuccessLoan.Rows(y).Item(4) = tableLoan.Rows(x).Item(4)
                                            tableSuccessLoan.Rows(y).Item(5) = tableLoan.Rows(x).Item(5)
                                            tableSuccessLoan.Rows(y).Item(6) = tableLoan.Rows(x).Item(6)
                                            '(3) Change Column IDCard from CIF File
                                            tableSuccessLoan.Rows(y).Item(7) = result.Item(14).ToString.ToUpper()
                                            '(4) Change Column Name IDCard from CIF File
                                            tableSuccessLoan.Rows(y).Item(8) = result.Item(5).ToString.ToUpper()

                                            tableSuccessLoan.Rows(y).Item(9) = tableLoan.Rows(x).Item(9).ToString
                                            tableSuccessLoan.Rows(y).Item(10) = tableLoan.Rows(x).Item(10).ToString
                                            tableSuccessLoan.Rows(y).Item(11) = tableLoan.Rows(x).Item(11).ToString
                                            tableSuccessLoan.Rows(y).Item(12) = tableLoan.Rows(x).Item(12).ToString
                                            tableSuccessLoan.Rows(y).Item(13) = tableLoan.Rows(x).Item(13).ToString
                                            tableSuccessLoan.Rows(y).Item(14) = tableLoan.Rows(x).Item(14).ToString
                                            tableSuccessLoan.Rows(y).Item(15) = tableLoan.Rows(x).Item(15).ToString
                                            tableSuccessLoan.Rows(y).Item(16) = tableLoan.Rows(x).Item(16).ToString
                                            '(5) Change Column Occupation to Devision from Table CIF Select Filter
                                            If cb_product.SelectedIndex.ToString = 2 Then
                                                tableSuccessLoan.Rows(y).Item(17) = tableLoan.Rows(x).Item(17).ToString.ToUpper()
                                                tableSuccessLoan.Rows(y).Item(18) = tableLoan.Rows(x).Item(18).ToString.ToUpper()
                                                tableSuccessLoan.Rows(y).Item(19) = tableLoan.Rows(x).Item(19).ToString.ToUpper()
                                                tableSuccessLoan.Rows(y).Item(20) = tableLoan.Rows(x).Item(20).ToString.ToUpper()
                                                tableSuccessLoan.Rows(y).Item(21) = tableLoan.Rows(x).Item(21).ToString.ToUpper()
                                            Else
                                                'product kredivo 
                                                Call SetOccupation(result.Item(39).ToString.ToUpper(), y, x, 17, 18, 19, 20, 21)
                                            End If

                                            tableSuccessLoan.Rows(y).Item(22) = tableLoan.Rows(x).Item(22).ToString.ToUpper()
                                            tableSuccessLoan.Rows(y).Item(23) = tableLoan.Rows(x).Item(23).ToString.ToUpper()
                                            tableSuccessLoan.Rows(y).Item(24) = tableLoan.Rows(x).Item(24).ToString.ToUpper()
                                            tableSuccessLoan.Rows(y).Item(25) = tableLoan.Rows(x).Item(25).ToString.ToUpper()
                                            'Insert Amount 
                                            tableSuccessLoan.Rows(y).Item(26) = tableLoan.Rows(x).Item(26).ToString
                                            'Insert amount
                                            amount_loan_final += Convert.ToDouble(tableLoan.Rows(x).Item(26))
                                            tableSuccessLoan.Rows(y).Item(27) = tableLoan.Rows(x).Item(27).ToString
                                            tableSuccessLoan.Rows(y).Item(28) = tableLoan.Rows(x).Item(28).ToString
                                            tableSuccessLoan.Rows(y).Item(29) = tableLoan.Rows(x).Item(29).ToString
                                            '(6) Change Column Expected Booking Date is today
                                            If cb_product.SelectedIndex.ToString = 2 Then
                                                'product akulaku 
                                                'expected akulaku always on 28 any month
                                                Dim strdate2 As String = getdate.ToString("MMyyyyHHmmss")
                                                strdate2 = getdate.ToString("yyyy") + "" + getdate.ToString("MM") + "" + "28"
                                                tableSuccessLoan.Rows(y).Item(30) = strdate2.ToString
                                            Else

                                                tableSuccessLoan.Rows(y).Item(30) = strdate.ToString
                                            End If
                                            tableSuccessLoan.Rows(y).Item(31) = tableLoan.Rows(x).Item(31).ToString
                                            tableSuccessLoan.Rows(y).Item(32) = tableLoan.Rows(x).Item(32).ToString
                                            tableSuccessLoan.Rows(y).Item(33) = tableLoan.Rows(x).Item(33).ToString
                                            tableSuccessLoan.Rows(y).Item(34) = tableLoan.Rows(x).Item(34).ToString
                                            tableSuccessLoan.Rows(y).Item(35) = tableLoan.Rows(x).Item(35).ToString
                                            tableSuccessLoan.Rows(y).Item(36) = tableLoan.Rows(x).Item(36).ToString
                                            tableSuccessLoan.Rows(y).Item(37) = Int(tableLoan.Rows(x).Item(37)).ToString
                                            tableSuccessLoan.Rows(y).Item(38) = tableLoan.Rows(x).Item(38).ToString
                                            tableSuccessLoan.Rows(y).Item(39) = tableLoan.Rows(x).Item(39).ToString
                                            tableSuccessLoan.Rows(y).Item(40) = tableLoan.Rows(x).Item(40).ToString
                                            'GET INTEREST PRODUCT
                                            tableSuccessLoan.Rows(y).Item(41) = getInterestProduct(cb_product.SelectedIndex.ToString).ToString

                                            tableSuccessLoan.Rows(y).Item(42) = tableLoan.Rows(x).Item(42).ToString
                                            tableSuccessLoan.Rows(y).Item(43) = tableLoan.Rows(x).Item(43).ToString
                                            tableSuccessLoan.Rows(y).Item(44) = tableLoan.Rows(x).Item(44).ToString
                                            tableSuccessLoan.Rows(y).Item(45) = tableLoan.Rows(x).Item(45).ToString
                                            tableSuccessLoan.Rows(y).Item(46) = tableLoan.Rows(x).Item(46).ToString
                                            tableSuccessLoan.Rows(y).Item(47) = tableLoan.Rows(x).Item(47).ToString

                                            '(7) Change Column Reference Source until SID Debtor Status And SID Debtor Group, LBU Relationship with Bank
                                            'And Branch Code, KPO Branch Head ID
                                            '25 Column 
                                            'Call Function SetDefaultDataMappingLoan
                                            'type akulaku product
                                            If cb_product.SelectedIndex.ToString = 2 Then
                                                tableSuccessLoan.Rows(y).Item(48) = tableLoan.Rows(x).Item(48).ToString
                                                tableSuccessLoan.Rows(y).Item(49) = tableLoan.Rows(x).Item(49).ToString
                                                tableSuccessLoan.Rows(y).Item(50) = tableLoan.Rows(x).Item(50).ToString
                                                tableSuccessLoan.Rows(y).Item(51) = tableLoan.Rows(x).Item(51).ToString
                                                tableSuccessLoan.Rows(y).Item(52) = tableLoan.Rows(x).Item(52).ToString
                                                tableSuccessLoan.Rows(y).Item(53) = tableLoan.Rows(x).Item(53).ToString
                                                tableSuccessLoan.Rows(y).Item(54) = tableLoan.Rows(x).Item(54).ToString
                                                tableSuccessLoan.Rows(y).Item(55) = tableLoan.Rows(x).Item(55).ToString
                                                tableSuccessLoan.Rows(y).Item(56) = tableLoan.Rows(x).Item(56).ToString
                                                tableSuccessLoan.Rows(y).Item(57) = tableLoan.Rows(x).Item(57).ToString
                                                tableSuccessLoan.Rows(y).Item(58) = tableLoan.Rows(x).Item(58).ToString
                                                tableSuccessLoan.Rows(y).Item(59) = tableLoan.Rows(x).Item(59).ToString
                                                tableSuccessLoan.Rows(y).Item(60) = tableLoan.Rows(x).Item(60).ToString
                                                tableSuccessLoan.Rows(y).Item(61) = tableLoan.Rows(x).Item(61).ToString
                                                tableSuccessLoan.Rows(y).Item(62) = tableLoan.Rows(x).Item(62).ToString
                                                tableSuccessLoan.Rows(y).Item(63) = tableLoan.Rows(x).Item(63).ToString
                                                tableSuccessLoan.Rows(y).Item(64) = tableLoan.Rows(x).Item(64).ToString
                                                tableSuccessLoan.Rows(y).Item(65) = tableLoan.Rows(x).Item(65).ToString
                                                tableSuccessLoan.Rows(y).Item(66) = tableLoan.Rows(x).Item(66).ToString
                                                tableSuccessLoan.Rows(y).Item(67) = tableLoan.Rows(x).Item(67).ToString
                                                tableSuccessLoan.Rows(y).Item(68) = tableLoan.Rows(x).Item(68).ToString
                                                'Column [SID Debtor Group1] ,[LBU Relationship with Bank
                                                tableSuccessLoan.Rows(y).Item(70) = tableLoan.Rows(x).Item(70).ToString
                                                tableSuccessLoan.Rows(y).Item(71) = tableLoan.Rows(x).Item(71).ToString
                                                'Column [Branch Code] ,[KPO Branch Head ID]
                                                tableSuccessLoan.Rows(y).Item(86) = tableLoan.Rows(x).Item(86).ToString
                                                tableSuccessLoan.Rows(y).Item(87) = tableLoan.Rows(x).Item(87).ToString
                                                '(8) Change Column Company Project Location 
                                                tableSuccessLoan.Rows(y).Item(69) = tableLoan.Rows(x).Item(69).ToString
                                            Else
                                                getDataMappingLoan = SetDefaultDataMappingLoan(cb_product.SelectedIndex.ToString)
                                                If getDataMappingLoan.Rows.Count > 0 Then
                                                    '[Reference Source] ,[Economy Sector LBU] ,[LBU Use Reference] ,[LBU Loan Type] ,[LBU Debtor Group],[Loan Type - LBU] ,[LBU Use Type] ,[LBU Rating Agency] ,[Debtor Rating],[LBU Measurement Category],[LBU Portfolio Category],[LBU Debtor Category],[SID Economy Sector] ,[SID Use Type],[SID Guarantor Type] ,[SID Use Orientation], [SID Loan Characteristic],[SID Relationship with Bank],[SID Loan Group],[SID Debtor Group],[SID Debtor Status] ,[SID Debtor Group1] ,[LBU Relationship with Bank] ,[Branch Code] ,[KPO Branch Head ID]
                                                    'Column [Reference Source] until [SID Debtor Status]
                                                    tableSuccessLoan.Rows(y).Item(48) = getDataMappingLoan.Rows(0).Item(0).ToString
                                                    tableSuccessLoan.Rows(y).Item(49) = getDataMappingLoan.Rows(0).Item(1).ToString
                                                    tableSuccessLoan.Rows(y).Item(50) = getDataMappingLoan.Rows(0).Item(2).ToString
                                                    tableSuccessLoan.Rows(y).Item(51) = getDataMappingLoan.Rows(0).Item(3).ToString
                                                    tableSuccessLoan.Rows(y).Item(52) = getDataMappingLoan.Rows(0).Item(4).ToString
                                                    tableSuccessLoan.Rows(y).Item(53) = getDataMappingLoan.Rows(0).Item(5).ToString
                                                    tableSuccessLoan.Rows(y).Item(54) = getDataMappingLoan.Rows(0).Item(6).ToString
                                                    tableSuccessLoan.Rows(y).Item(55) = getDataMappingLoan.Rows(0).Item(7).ToString
                                                    tableSuccessLoan.Rows(y).Item(56) = getDataMappingLoan.Rows(0).Item(8).ToString
                                                    tableSuccessLoan.Rows(y).Item(57) = getDataMappingLoan.Rows(0).Item(9).ToString
                                                    tableSuccessLoan.Rows(y).Item(58) = getDataMappingLoan.Rows(0).Item(10).ToString
                                                    tableSuccessLoan.Rows(y).Item(59) = getDataMappingLoan.Rows(0).Item(11).ToString
                                                    tableSuccessLoan.Rows(y).Item(60) = getDataMappingLoan.Rows(0).Item(12).ToString
                                                    tableSuccessLoan.Rows(y).Item(61) = getDataMappingLoan.Rows(0).Item(13).ToString
                                                    tableSuccessLoan.Rows(y).Item(62) = getDataMappingLoan.Rows(0).Item(14).ToString
                                                    tableSuccessLoan.Rows(y).Item(63) = getDataMappingLoan.Rows(0).Item(15).ToString
                                                    tableSuccessLoan.Rows(y).Item(64) = getDataMappingLoan.Rows(0).Item(16).ToString
                                                    tableSuccessLoan.Rows(y).Item(65) = getDataMappingLoan.Rows(0).Item(17).ToString
                                                    tableSuccessLoan.Rows(y).Item(66) = getDataMappingLoan.Rows(0).Item(18).ToString
                                                    tableSuccessLoan.Rows(y).Item(67) = getDataMappingLoan.Rows(0).Item(19).ToString
                                                    tableSuccessLoan.Rows(y).Item(68) = getDataMappingLoan.Rows(0).Item(20).ToString
                                                    'Column [SID Debtor Group1] ,[LBU Relationship with Bank]
                                                    tableSuccessLoan.Rows(y).Item(70) = getDataMappingLoan.Rows(0).Item(21).ToString
                                                    tableSuccessLoan.Rows(y).Item(71) = getDataMappingLoan.Rows(0).Item(22)
                                                    'Column [Branch Code] ,[KPO Branch Head ID]
                                                    tableSuccessLoan.Rows(y).Item(86) = getDataMappingLoan.Rows(0).Item(23).ToString
                                                    tableSuccessLoan.Rows(y).Item(87) = getDataMappingLoan.Rows(0).Item(24).ToString
                                                End If
                                                '(8) Change Column Company Project Location is Dati Office from Data CIF
                                                tableSuccessLoan.Rows(y).Item(69) = result.Item(56).ToString
                                            End If

                                            tableSuccessLoan.Rows(y).Item(72) = tableLoan.Rows(x).Item(72).ToString
                                            tableSuccessLoan.Rows(y).Item(73) = tableLoan.Rows(x).Item(73).ToString
                                            tableSuccessLoan.Rows(y).Item(74) = tableLoan.Rows(x).Item(74).ToString
                                            tableSuccessLoan.Rows(y).Item(75) = tableLoan.Rows(x).Item(75).ToString
                                            tableSuccessLoan.Rows(y).Item(76) = tableLoan.Rows(x).Item(76).ToString
                                            tableSuccessLoan.Rows(y).Item(77) = tableLoan.Rows(x).Item(77).ToString
                                            tableSuccessLoan.Rows(y).Item(78) = tableLoan.Rows(x).Item(78).ToString
                                            tableSuccessLoan.Rows(y).Item(79) = tableLoan.Rows(x).Item(79).ToString
                                            tableSuccessLoan.Rows(y).Item(80) = tableLoan.Rows(x).Item(80).ToString
                                            tableSuccessLoan.Rows(y).Item(81) = tableLoan.Rows(x).Item(81).ToString
                                            tableSuccessLoan.Rows(y).Item(82) = tableLoan.Rows(x).Item(82).ToString
                                            tableSuccessLoan.Rows(y).Item(83) = tableLoan.Rows(x).Item(83).ToString
                                            tableSuccessLoan.Rows(y).Item(84) = tableLoan.Rows(x).Item(84).ToString
                                            tableSuccessLoan.Rows(y).Item(85) = tableLoan.Rows(x).Item(85).ToString
                                            '(9) change Column data from CIF 86 and 87
                                            tableSuccessLoan.Rows(y).Item(88) = tableLoan.Rows(x).Item(88).ToString
                                            tableSuccessLoan.Rows(y).Item(89) = tableLoan.Rows(x).Item(89).ToString
                                            tableSuccessLoan.Rows(y).Item(90) = tableLoan.Rows(x).Item(90).ToString
                                            tableSuccessLoan.Rows(y).Item(91) = tableLoan.Rows(x).Item(91).ToString
                                            tableSuccessLoan.Rows(y).Item(92) = tableLoan.Rows(x).Item(92).ToString
                                            tableSuccessLoan.Rows(y).Item(93) = tableLoan.Rows(x).Item(93).ToString
                                            tableSuccessLoan.Rows(y).Item(94) = tableLoan.Rows(x).Item(94).ToString
                                            tableSuccessLoan.Rows(y).Item(95) = tableLoan.Rows(x).Item(95).ToString
                                            tableSuccessLoan.Rows(y).Item(96) = tableLoan.Rows(x).Item(96).ToString
                                            tableSuccessLoan.Rows(y).Item(97) = tableLoan.Rows(x).Item(97).ToString
                                            tableSuccessLoan.Rows(y).Item(98) = tableLoan.Rows(x).Item(98).ToString
                                            tableSuccessLoan.Rows(y).Item(99) = tableLoan.Rows(x).Item(99).ToString
                                            tableSuccessLoan.Rows(y).Item(100) = tableLoan.Rows(x).Item(100).ToString
                                            tableSuccessLoan.Rows(y).Item(101) = tableLoan.Rows(x).Item(101).ToString
                                            tableSuccessLoan.Rows(y).Item(102) = tableLoan.Rows(x).Item(102).ToString

                                            'plus point
                                            y += 1
                                        End If

                                    Else
                                        'Insert amount
                                        amount_loan_takedown += Convert.ToDouble(tableLoan.Rows(x).Item(26))
                                        'cleansing row not found
                                        'set to datable takedown after cleansing
                                        tableTakedownLoan.ImportRow(tableLoan.Rows(x))
                                    End If
                                Else
                                    'takeout loan
                                    tableTakedownLoan.ImportRow(tableLoan.Rows(x))
                                    End If
endJoinLoanToCIF:
                                    'event load data
                                    Application.DoEvents()

                                    'send to datagridview success loan
                                    load_loan_success.DataSource = tableSuccessLoan
                                    'send to datagridview takedown loan
                                    load_loan_takedown.DataSource = tableTakedownLoan

                                    'remove row duplicate in datable because it will cif to reupload again
                                    tableCIFGagal = DeleteDuplicateFromDataTable(tableCIFGagal, "Multifinance_key_no")
                                    'send to datagridview CIF gagal
                                    load_cif_gagal.DataSource = tableCIFGagal

                                    ' Show Count Rows in Datagridview
                                    C_rows3.Text = load_loan_success.RowCount - 1
                                    C_rows4.Text = load_loan_takedown.RowCount - 1
                                    ' Show Count Rows in Datagridview
                                    C_Rows2.Text = load_cif_gagal.RowCount - 1

                            Next

                            'notif after cleansing process
                            'focus tabpages
                            tabcontrol1.SelectedTab = tb_report3

                        End If

                    End If
                    MessageBox.Show("Processing is Done.", "Alert")
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'disabled sort
            For Each a As DataGridViewColumn In load_cif_final.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            'disabled sort
            For Each b As DataGridViewColumn In load_cif_gagal.Columns
                b.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            'disabled sort
            For Each c As DataGridViewColumn In load_loan_success.Columns
                c.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            'disabled sort
            For Each d As DataGridViewColumn In load_loan_takedown.Columns
                d.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try

    End Sub
    'Occupation Code 1 (large)	Occupation Code 2 (middle)	Occupation Code 3 (small)	Position	Division
    Private Sub SetOccupation(ByRef occupation As String, ByRef y As Integer, ByRef x As Integer, ByRef cellOccupCode1 As Integer, ByRef cellOccupCode2 As Integer, ByRef cellOccupCode3 As Integer, ByRef cellPOsition As Integer, ByRef cellDivision As Integer)

        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(occupation) Then
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'SELECT [NO], [Code_Occupation], [Code_CIF_Source_of_Fund] FROM [DB_MASTER].[dbo].[DB_OCCUPATION_KDV]
            cmd = New SqlCommand(" SELECT TOP 1 [NO], [LOAN_occupation_1] ,[LOAN_occupation_2] ,[LOAN_occupation_3] ,[LOAN_position] ,[LOAN_division] FROM [DB_MASTER].[dbo].[DB_OCCUPATION_KDV] WHERE [Code_Occupation_False] = '" + occupation.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data Occupation 1
                tableSuccessLoan.Rows(y).Item(cellOccupCode1) = checktable.Rows(0).Item(1).ToString.ToUpper().Trim()
                'get data  Occupation 2
                tableSuccessLoan.Rows(y).Item(cellOccupCode2) = checktable.Rows(0).Item(2).ToString.ToUpper().Trim()
                'get data  Occupation 3
                tableSuccessLoan.Rows(y).Item(cellOccupCode3) = checktable.Rows(0).Item(3).ToString.ToUpper().Trim()
                'get data  Position
                tableSuccessLoan.Rows(y).Item(cellPOsition) = checktable.Rows(0).Item(4).ToString.ToUpper().Trim()
                'get data  Devision
                tableSuccessLoan.Rows(y).Item(cellDivision) = checktable.Rows(0).Item(5).ToString.ToUpper().Trim()
            Else

            End If
        End If
    End Sub


    Function SetDefaultDataMappingLoan(ByRef productType As String) As DataTable
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(productType) Then
            MessageBox.Show("Type Product is null.", "Alert")
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            cmd = New SqlCommand("SELECT TOP 1 [Reference Source] ,[Economy Sector LBU] ,[LBU Use Reference] ,[LBU Loan Type] ,[LBU Debtor Group],[Loan Type - LBU] ,[LBU Use Type] ,[LBU Rating Agency] ,[Debtor Rating],[LBU Measurement Category],[LBU Portfolio Category],[LBU Debtor Category],[SID Economy Sector] ,[SID Use Type],[SID Guarantor Type] ,[SID Use Orientation], [SID Loan Characteristic],[SID Relationship with Bank],[SID Loan Group],[SID Debtor Group],[SID Debtor Status] ,[SID Debtor Group1] ,[LBU Relationship with Bank] ,[Branch Code] ,[KPO Branch Head ID] FROM [DB_MASTER].[dbo].[DB_MAPPING_LOAN_KDV] WHERE [NO] = '" + productType.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                checktable = checktable
            End If
        End If
        Return checktable
    End Function

    Private Sub btn_import_collect_Click(sender As Object, e As EventArgs) Handles btn_import_collect.Click
        Try
            Dim ds As New DataSet()
            Dim excel2 As String
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                excel2 = fi.FullName
                conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel2 + ";Extended Properties=Excel 12.0;")
                conn.Open()
                Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                Dim da As New OleDbDataAdapter(sqlquery, conn)
                da.Fill(ds)
                dt_collec = ds.Tables(0)
                conn.Close()
                C_data4.Text = "(OK)"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
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
                    If Not firstField Then writer.Write(",")
                    cellvalue1 = table.Columns.Item(i).ToString()
                    If cellvalue1.Contains(",") Then
                        writer.Write(ControlChars.Quote)
                        writer.Write(cellvalue1)
                        writer.Write(ControlChars.Quote)
                    Else
                        writer.Write(cellvalue1)
                    End If
                    'For space "," In any word 
                    firstField = False
                Next
                'For next rows in data
                firstRow = False
                'lopping column rows
                For Each row As DataRow In table.Rows
                    If Not firstRow Then writer.WriteLine()
                    firstField = True
                    For i = 0 To lastColIndex - 1
                        If Not firstField Then writer.Write(",")
                        If Not row.IsNull(i) Then
                            cellvalue = row.Item(i).ToString
                            If cellvalue.Contains(",") Then
                                writer.Write(ControlChars.Quote)
                                writer.Write("'" + cellvalue)
                                writer.Write(ControlChars.Quote)
                            Else
                                writer.Write(cellvalue)
                            End If
                        End If
                        firstField = False
                    Next
                    firstRow = False
                Next
            End Using
        End If

    End Sub
    Private Sub ExportDataToExcel(ByRef table As DataTable, ByRef filename As String)
        Using out = New XLWorkbook()
            out.Worksheets.Add(table, "Sheet1")
            out.SaveAs(filename)
        End Using

    End Sub


    Private Sub load_cif_gagal_DoubleClick(sender As Object, e As EventArgs) Handles load_cif_gagal.DoubleClick
        Try
            If Not load_cif_gagal.Rows.Count <= 0 Then
                Dim da As New DetailCIF With {.Owner = Me}
                Dim rowIndex As Integer = load_cif_gagal.CurrentRow.Index
                'send index
                da.indexRows = rowIndex
                'no cif
                da.txt_no_cif.Text = load_cif_gagal.Rows(rowIndex).Cells(2).Value.ToString
                'multifinance code
                da.txt_multifinance_customer.Text = load_cif_gagal.Rows(rowIndex).Cells(79).Value.ToString
                da.multifinance_customer = load_cif_gagal.Rows(rowIndex).Cells(79).Value.ToString
                'data nasabah
                da.txt_name.Text = load_cif_gagal.Rows(rowIndex).Cells(5).Value.ToString
                da.txt_english_name.Text = load_cif_gagal.Rows(rowIndex).Cells(6).Value.ToString
                da.txt_name_id.Text = load_cif_gagal.Rows(rowIndex).Cells(7).Value.ToString
                da.txt_id_card.Text = load_cif_gagal.Rows(rowIndex).Cells(14).Value.ToString
                da.txt_date_birth.Text = load_cif_gagal.Rows(rowIndex).Cells(23).Value.ToString
                da.txt_gender.Text = load_cif_gagal.Rows(rowIndex).Cells(28).Value.ToString
                da.txt_mother_name.Text = load_cif_gagal.Rows(rowIndex).Cells(33).Value.ToString
                da.txt_place_birth.Text = load_cif_gagal.Rows(rowIndex).Cells(35).Value.ToString
                da.txt_occupation.Text = load_cif_gagal.Rows(rowIndex).Cells(39).Value.ToString
                da.txt_source_of_fund.Text = load_cif_gagal.Rows(rowIndex).Cells(74).Value.ToString

                'data wilayah home
                da.txt_dati_home.Text = load_cif_gagal.Rows(rowIndex).Cells(47).Value.ToString
                da.txt_post_code_home.Text = load_cif_gagal.Rows(rowIndex).Cells(51).Value.ToString
                da.txt_village_home.Text = load_cif_gagal.Rows(rowIndex).Cells(52).Value.ToString
                da.txt_sub_district_home.Text = load_cif_gagal.Rows(rowIndex).Cells(53).Value.ToString

                'data wilaya office
                da.txt_dati_office.Text = load_cif_gagal.Rows(rowIndex).Cells(56).Value.ToString
                da.txt_post_code_office.Text = load_cif_gagal.Rows(rowIndex).Cells(60).Value.ToString
                da.txt_village_office.Text = load_cif_gagal.Rows(rowIndex).Cells(61).Value.ToString
                da.txt_sub_district_office.Text = load_cif_gagal.Rows(rowIndex).Cells(62).Value.ToString
                da.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try




    End Sub

    Private Sub load_loan_success_DoubleClick(sender As Object, e As EventArgs) Handles load_loan_success.DoubleClick
        Try
            If Not load_loan_success.Rows.Count <= 0 Then
                Dim da As New DetailLoan With {.Owner = Me}
                'data success
                Dim rowIndex As Integer = load_loan_success.CurrentRow.Index
                'no cif
                da.txt_no_cif.Text = load_loan_success.Rows(rowIndex).Cells(3).Value.ToString
                'multifinance customer
                da.txt_multifinance_customer.Text = load_loan_success.Rows(rowIndex).Cells(98).Value.ToString
                'multifinance account
                da.txt_multi_acc.Text = load_loan_success.Rows(rowIndex).Cells(99).Value.ToString
                'data nasabah
                da.txt_name.Text = load_loan_success.Rows(rowIndex).Cells(8).Value.ToString
                da.txt_id_card.Text = load_loan_success.Rows(rowIndex).Cells(7).Value.ToString
                da.txt_birthdays.Text = load_loan_success.Rows(rowIndex).Cells(10).Value.ToString
                'data loan
                da.txt_approved_date.Text = load_loan_success.Rows(rowIndex).Cells(1).Value.ToString
                da.txt_amount.Text = load_loan_success.Rows(rowIndex).Cells(26).Value.ToString
                da.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub btn_summary_Click(sender As Object, e As EventArgs)
        Dim sm As New SummaryLoan With {.Owner = Me}

        sm.txt_product.Text = cb_product.Text + " Product"
        sm.n_cif_final.Text = tableCIF.Rows.Count
        sm.n_no_cif_gagal.Text = tableCIFGagal.Rows.Count
        sm.n_loan_takedown.Text = tableTakedownLoan.Rows.Count
        sm.n_loan_final.Text = tableLoan.Rows.Count
        sm.n_loan_final_amount.Text = "Rp. " + amount_loan_final.ToString
        sm.n_loan_takedown_amount.Text = "Rp. " + amount_loan_takedown.ToString
        sm.n_total_amount.Text = "Rp. " + (amount_loan_final + amount_loan_takedown).ToString
        sm.n_total_loan.Text = (tableLoan.Rows.Count + tableTakedownLoan.Rows.Count).ToString
        sm.ShowDialog()
    End Sub

    Private Sub cb_type_join_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_type_join.SelectedIndexChanged
        Dim cb_type As Integer = cb_type_join.SelectedIndex.ToString
        If cb_type = 0 Then
            tabcontrol1.TabPages.Remove(tb_report1)
            tabcontrol1.TabPages.Remove(tb_report3)
            tabcontrol1.TabPages.Remove(tb_report4)
            tabcontrol1.TabPages.Add(tb_report1)
            Label7.Hide()
            Label10.Hide()
            C_data3.Text = ""
            C_data4.Text = ""
            btn_import_loan.Hide()
            btn_import_collect.Hide()

        ElseIf cb_type = 1 Then
            tabcontrol1.TabPages.Remove(tb_report1)
            tabcontrol1.TabPages.Remove(tb_report3)
            tabcontrol1.TabPages.Remove(tb_report4)
            tabcontrol1.TabPages.Add(tb_report3)
            tabcontrol1.TabPages.Add(tb_report4)
            Label10.Show()
            Label7.Show()
            btn_import_loan.Show()
            btn_import_collect.Show()
        Else
        End If

    End Sub

    Private Sub load_cif_final_DoubleClick(sender As Object, e As EventArgs) Handles load_cif_final.DoubleClick
        Try
            If Not load_cif_final.Rows.Count <= 0 Then
                Dim da As New DetailCIF With {.Owner = Me}
                Dim rowIndex As Integer = load_cif_final.CurrentRow.Index
                'send index
                da.indexRows = rowIndex
                'no cif
                da.txt_no_cif.Text = load_cif_final.Rows(rowIndex).Cells(2).Value.ToString
                'multifinance code
                da.txt_multifinance_customer.Text = load_cif_final.Rows(rowIndex).Cells(79).Value.ToString
                da.multifinance_customer = load_cif_final.Rows(rowIndex).Cells(79).Value.ToString
                'data nasabah
                da.txt_name.Text = load_cif_final.Rows(rowIndex).Cells(5).Value.ToString
                da.txt_english_name.Text = load_cif_final.Rows(rowIndex).Cells(6).Value.ToString
                da.txt_name_id.Text = load_cif_final.Rows(rowIndex).Cells(7).Value.ToString
                da.txt_id_card.Text = load_cif_final.Rows(rowIndex).Cells(14).Value.ToString
                da.txt_date_birth.Text = load_cif_final.Rows(rowIndex).Cells(23).Value.ToString
                da.txt_gender.Text = load_cif_final.Rows(rowIndex).Cells(28).Value.ToString
                da.txt_mother_name.Text = load_cif_final.Rows(rowIndex).Cells(33).Value.ToString
                da.txt_place_birth.Text = load_cif_final.Rows(rowIndex).Cells(35).Value.ToString
                da.txt_occupation.Text = load_cif_final.Rows(rowIndex).Cells(39).Value.ToString
                da.txt_source_of_fund.Text = load_cif_final.Rows(rowIndex).Cells(74).Value.ToString
                da.txt_tax_number.Text = load_cif_final.Rows(rowIndex).Cells(34).Value.ToString

                'data wilayah home
                da.txt_dati_home.Text = load_cif_final.Rows(rowIndex).Cells(47).Value.ToString
                da.txt_post_code_home.Text = load_cif_final.Rows(rowIndex).Cells(51).Value.ToString
                da.txt_village_home.Text = load_cif_final.Rows(rowIndex).Cells(52).Value.ToString
                da.txt_sub_district_home.Text = load_cif_final.Rows(rowIndex).Cells(53).Value.ToString

                'data wilaya office
                da.txt_dati_office.Text = load_cif_final.Rows(rowIndex).Cells(56).Value.ToString
                da.txt_post_code_office.Text = load_cif_final.Rows(rowIndex).Cells(60).Value.ToString
                da.txt_village_office.Text = load_cif_final.Rows(rowIndex).Cells(61).Value.ToString
                da.txt_sub_district_office.Text = load_cif_final.Rows(rowIndex).Cells(62).Value.ToString
                da.btn_success.Hide()
                da.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function getInterestProduct(ByRef no_product As String) As String
        Dim no As String
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(no_product) Then
            no = "0"
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'check sum balanced today
            cmd = New SqlCommand("SELECT [NO],[ID_PRODUCT] ,[NAME_PRODUCT],[INTEREST] FROM [DB_MASTER].[dbo].[DB_PRODUCTS] WHERE [NO] = '" + no_product.ToString + "'", SQLConnection)
            'End If
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data balanced calculation
                'sum data total balanced
                no = Trim(checktable.Rows(0).Item(3).ToString)
                'Console.WriteLine(no)
            Else
                'set alert if not found
                no = "0"
                'Console.WriteLine(no)
            End If
        End If
        Return no
    End Function
    Private Sub cb_export_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_export.SelectedIndexChanged

        Try
            If Not cb_export.SelectedIndex = 0 Then
                If tableNoCIF.Rows.Count <= 0 Then
                    MessageBox.Show("Data is null.", "Alert")
                Else
                    Dim getdate As Date = Convert.ToDateTime(DateTime.Now)
                    Dim nameProducts As String = ""
                    Dim strdate As String = ""
                    Dim filename1 As String = ""
                    Dim extention As String = ""
                    getdate.ToString("ddMMyyyyHHmmss")
                    strdate = getdate.ToString("dd") + "" + getdate.ToString("MM") + "" + getdate.ToString("yyyy") + "_" + getdate.ToString("HH") + "" + getdate.ToString("mm") + "" + getdate.ToString("ss")
                    nameProducts = cb_product.SelectedItem.ToString
                    ' Choose Extention Format Export
                    If cb_export.SelectedIndex = 1 Then
                        'Excel Files 
                        extention = ".xlsx"
                    ElseIf cb_export.SelectedIndex = 2 Then
                        'text Files 
                        extention = ".csv"
                    ElseIf cb_export.SelectedIndex = 3 Then
                        'text Files 
                        extention = ".txt"
                    End If
                    'join cif 
                    If cb_type_join.SelectedIndex.ToString = 0 Then
                        'for cif final or gagal
                        If tabcontrol1.SelectedIndex.ToString = 0 Then
                            filename1 = "D:\OUTPUT\" + nameProducts.ToString + "_CIF_Gagal_" + strdate + extention
                            ' Choose Extention Format Export
                            If cb_export.SelectedIndex = 1 Then
                                'Excel Files
                                Call ExportDataToExcel(tableCIFGagal, filename1)
                            ElseIf cb_export.SelectedIndex = 2 Then
                                'text Files 
                                Call DataTableToExportFile(tableCIFGagal, filename1)
                            ElseIf cb_export.SelectedIndex = 3 Then
                                'text Files 
                                Call DataTableToExportFile(tableCIFGagal, filename1)
                            End If
                            'Data CIF final
                        ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                            filename1 = "D:\OUTPUT\" + nameProducts.ToString + "_CIF_Final_" + strdate + extention
                            ' Choose Extention Format Export
                            If cb_export.SelectedIndex = 1 Then
                                'Excel Files
                                Call ExportDataToExcel(tableCIFFinal, filename1)
                            ElseIf cb_export.SelectedIndex = 2 Then
                                'text Files 
                                Call DataTableToExportFile(tableCIFFinal, filename1)
                            ElseIf cb_export.SelectedIndex = 3 Then
                                'text Files 
                                Call DataTableToExportFile(tableCIFFinal, filename1)
                            End If
                        End If
                    ElseIf cb_type_join.SelectedIndex.ToString = 1 Then
                        If tabcontrol1.SelectedIndex.ToString = 0 Then
                            'Data CIF Gagal
                            filename1 = "D:\OUTPUT\" + nameProducts.ToString + "_CIF_Gagal_" + strdate + extention
                            ' Choose Extention Format Export
                            If cb_export.SelectedIndex = 1 Then
                                'Excel Files
                                Call ExportDataToExcel(tableCIFGagal, filename1)
                            ElseIf cb_export.SelectedIndex = 2 Then
                                'text Files 
                                Call DataTableToExportFile(tableCIFGagal, filename1)
                            ElseIf cb_export.SelectedIndex = 3 Then
                                'text Files 
                                Call DataTableToExportFile(tableCIFGagal, filename1)
                            End If
                            'Data Loan Final
                        ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                            filename1 = "D:\OUTPUT\" + nameProducts.ToString + "_LOAN_Final_" + strdate + extention
                            ' Choose Extention Format Export
                            If cb_export.SelectedIndex = 1 Then
                                'Excel Files
                                Call ExportDataToExcel(tableSuccessLoan, filename1)
                            ElseIf cb_export.SelectedIndex = 2 Then
                                'text Files 
                                Call DataTableToExportFile(tableSuccessLoan, filename1)
                            ElseIf cb_export.SelectedIndex = 3 Then
                                'text Files 
                                Call DataTableToExportFile(tableSuccessLoan, filename1)
                            End If
                        ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                            'Data Loan Takedown
                            filename1 = "D:\OUTPUT\" + nameProducts.ToString + "_LOAN_Takedown_" + strdate + extention
                            ' Choose Extention Format Export
                            If cb_export.SelectedIndex = 1 Then
                                'Excel Files
                                Call ExportDataToExcel(tableTakedownLoan, filename1)
                            ElseIf cb_export.SelectedIndex = 2 Then
                                'text Files 
                                Call DataTableToExportFile(tableTakedownLoan, filename1)
                            ElseIf cb_export.SelectedIndex = 3 Then
                                'text Files 
                                Call DataTableToExportFile(tableTakedownLoan, filename1)
                            End If
                        End If
                    End If
                    MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private RowCountNOCIF As Integer
    Private Sub cb_import_cif_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_import_cif.SelectedIndexChanged
        Try
            If Not cb_import_cif.SelectedIndex.ToString = 0 Then
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
                    If cb_import_cif.SelectedIndex.ToString = 1 Then
                        myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                    ElseIf cb_import_cif.SelectedIndex.ToString = 2 Then
                        myTableName = "sheetname1$"
                    End If
                    Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                    Dim da As New OleDbDataAdapter(sqlquery, conn)
                    'remove rows index 0
                    da.Fill(ds)
                    'event load data
                    Application.DoEvents()

                    If tableNoCIF.Rows.Count > 0 Then
                        'add data file cif to new row  
                        For x As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            'import row from another datatable
                            tableNoCIF.ImportRow(ds.Tables(0).Rows(x))
                        Next
                    Else
                        tableNoCIF = ds.Tables(0)
                    End If

                    'sort data table in desc : menghindari data 0 paling atas 
                    ' file from CS
                    If cb_import_cif.SelectedIndex.ToString = 2 Then
                        tableNoCIF.DefaultView.Sort = "[" + tableNoCIF.Columns(1).ColumnName + "] DESC"
                        tableNoCIF = tableNoCIF.DefaultView.ToTable()
                    End If

                    RowCountNOCIF += ds.Tables(0).Rows.Count
                    C_data1.Text = "(" + RowCountNOCIF.ToString + ")"
                    tableNoCIF.AcceptChanges()
                    conn.Close()

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try
    End Sub
End Class