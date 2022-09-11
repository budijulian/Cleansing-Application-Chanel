
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

Public Class Form1

    Private conn As OleDbConnection 'koneksi odb
    Private table As New DataTable
    Private dts As DataSet ' membuat table datasheet virtual
    Private excel, excel2 As String

    Private dt_collec As New DataTable
    'Private SQLConnection As New Data.SqlClient.SqlConnection
    Private cmd, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7 As New Data.SqlClient.SqlCommand
    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    '...............SQL SERVER..............
    'Connection SQL Server
    'Private SQLConnectionString As String = "Server=ADMIN-PC\SQLEXPRESS; Database=DB_MASTER; integrated security=true"
    Public SQL As New ConnectionSQL
    Public getPdt As New accountPayments
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Private arrString As New ArrayList()
    Private words = New String() {}
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'get data product
            Dim getProducts As New DataTable
            Dim strProduct As String
            getProducts = getPdt.getAllProducts()
            For a = 0 To getProducts.Rows.Count - 1
                strProduct = "[" + (a + 1).ToString + "] " + getProducts.Rows(a).Item(1).ToString
                cb_product.Items.Add(strProduct.ToString)
            Next

            'default
            cb_export.SelectedIndex = 0
            cb_accurate.SelectedIndex = 0
            Dim MyParentValue As Checking = CType(Me.Owner, Checking)
            If Not IsNothing(MyParentValue) Then
                If table.Rows.Count = Nothing Then
                    If load_cif.Columns.Count <= 0 Then
                        With load_cif
                            .ColumnCount = 6
                            .Columns(0).Name = "MotherName*"
                            .Columns(1).Name = "IDCard*"
                            .Columns(2).Name = "PlaceBirth*"
                            .Columns(3).Name = "Dati*"
                            .Columns(4).Name = "Name*"
                            .Columns(5).Name = "Summary*"
                        End With
                    End If
                    'copy column to datatable
                    If table.Columns.Count = 0 Then
                        For j = 0 To MyParentValue.notDataFound.Columns.Count - 1
                            table.Columns.Add(MyParentValue.notDataFound.Columns(j).ColumnName)
                            'load_cif.Columns.Add(MyParentValue.notDataFound.Columns(j).ColumnName, j)
                        Next
                        For x = 0 To MyParentValue.notDataFound.Rows.Count - 1
                            'import row from another datatable
                            table.ImportRow(MyParentValue.notDataFound.Rows(x))
                        Next
                        load_cif.DataSource = table

                        ' Show Count Rows in Datagridview
                        C_rows1.Text = load_cif.RowCount - 1
                        C_Cells1.Text = load_cif.ColumnCount
                    End If
                End If
            Else
                C_rows1.Text = 0
                C_Cells1.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            ''get data not found from checking result
            btn_summary.Enabled = True
            btn_cleansing.Enabled = True
        End Try


    End Sub

    Private Sub btn_import_Click(sender As Object, e As EventArgs) Handles btn_import.Click
        Try
            Dim ds As New DataSet()
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                C_rows1.Text = 0
                'before clear datatable
                load_cif_failed.Columns.Clear()
                load_cif_success.Columns.Clear()
                'create new column in datagridview
                If load_cif.Columns.Count <= 0 Then
                    With load_cif
                        .ColumnCount = 6
                        .Columns(0).Name = "MotherName*"
                        .Columns(1).Name = "IDCard*"
                        .Columns(2).Name = "PlaceBirth*"
                        .Columns(3).Name = "Dati*"
                        .Columns(4).Name = "Name*"
                        .Columns(5).Name = "Summary*"
                    End With
                End If
                Dim fi As New IO.FileInfo(OpenFileDialog1.FileName)
                Dim filename As String = OpenFileDialog1.FileName
                excel = fi.FullName
                conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
                conn.Open()
                Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                Dim da As New OleDbDataAdapter(sqlquery, conn)
                da.Fill(ds)
                table = ds.Tables(0)
                load_cif.DataSource = table
                conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
            ' Disabled Button before process
            btn_cleansing.Enabled = True
            ' Show Count Rows in Datagridview
            C_rows1.Text = load_cif.RowCount - 1
            C_Cells1.Text = load_cif.ColumnCount - 1
            'disabled sort
            For Each a As DataGridViewColumn In load_cif.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub

    Public TableFinal, TableTakeDown As New DataTable
    Public TableTemp As New DataTable
    Private rowTemp, rowFound, rowNotFound As DataRow
    Private reason As New ArrayList
    Private takedown, colec As Boolean
    Private st_hours, st_minutes, st_seconds As Double
    Private poinDati, poinGender, poinName, poinIdBirth, poinMother, poinPlaceBirth, poinOccup As Integer
    Private Sub btn_cleansing_Click(sender As Object, e As EventArgs) Handles btn_cleansing.Click
        Try
            If table.Rows.Count = Nothing Then
                MessageBox.Show("Please, Insert a data.!", "Alert")
            Else
                If cb_product.SelectedIndex.ToString = 0 Then
                    MessageBox.Show("Please, Choose a product.!", "Alert")
                Else
                    SQLConnection = New SqlConnection(SQLConnectionString)
                    'Get Rows from load_cif for cleansing any rows
                    Dim pesan As MsgBoxResult
                    Dim timeStart As Date = Convert.ToDateTime(DateTime.Now)
                    Dim spanTime As TimeSpan
                    Dim company_name As String
                    Dim resultCollect As DataRow
                    Dim IDCard, BirthDay, Gender, Place_Birth, CountStrMother, StrMother, newStrMother, newName As String
                    'Declare variabel 
                    pesan = MsgBox("Cleansing Start?", MsgBoxStyle.YesNo, "Alert")
                    If pesan = MsgBoxResult.Yes Then
                        TableFinal.Reset()
                        TableTakeDown.Reset()
                        TableTemp.Reset()
                        'copy column to datatable
                        If TableTemp.Columns.Count = 0 Then
                            For j = 0 To table.Columns.Count - 1
                                TableTemp.Columns.Add(table.Columns(j).ColumnName)
                            Next
                            'add new column
                            TableTemp.Columns.Add("Reason")
                        End If
                        'copy column to datatable
                        If TableFinal.Rows.Count <= 0 Then
                            For j = 0 To table.Columns.Count - 1
                                TableFinal.Columns.Add(table.Columns(j).ColumnName)
                            Next
                        End If
                        'copy column to datatable
                        If TableTakeDown.Rows.Count <= 0 Then
                            'last column not include from TableTemp
                            For j = 0 To TableTemp.Columns.Count - 1
                                TableTakeDown.Columns.Add(TableTemp.Columns(j).ColumnName)
                            Next
                        End If

                        'create new table row
                        For x As Integer = 0 To table.Rows.Count - 1
                            '-------Split Data Value-------
                            'BirthDay
                            Dim dt As String = ""
                            Dim mt As String = ""
                            Dim yr As String = ""
                            Gender = ""
                            IDCard = ""
                            newName = ""
                            'Set Default
                            takedown = True

StartProccessCleansing:
                            rowTemp = TableTemp.NewRow()
                            TableTemp.Rows.Add(rowTemp)
                            '-------Set Variable Value-------
                            'ID_Card() : Column 17 (+ 3 column )
                            '3673012508790002:
                            If String.IsNullOrWhiteSpace(table.Rows(x).Item(14).ToString()) Then
                                IDCard = 0
                                poinIdBirth += 1
                                takedown = False
                                'set reason
                                reason.Add("Take Out-ID Card kosong ")
                            Else
                                IDCard = table.Rows(x).Item(14).ToString()
                                Gender = table.Rows(x).Item(28).ToString()
                                'Gender() : Column 31 (+ 3 column )
                                'M()
                                'Selections Cleansing for (Jenis Kelamin) 
                                'ID Card , index 7,8 must more 40 is M and less 40 is 
                                If Not String.IsNullOrWhiteSpace(Gender) Then
                                    If Strings.Mid(IDCard, 7, 2) > 40 Then
                                        Gender = "F"
                                    ElseIf Strings.Mid(IDCard, 7, 2) <= 40 Then
                                        Gender = "M"
                                    End If
                                End If
                                'Get IDCard
                                IDCard = Strings.Mid(IDCard, 7, 6)
                            End If
                            'Date_of_Birth() : Column 26 (+ 3 column )
                            '19790825:
                            If String.IsNullOrWhiteSpace(table.Rows(x).Item(23).ToString()) Then
                                BirthDay = ""
                                takedown = False
                                'set reason
                                reason.Add("Take Out-BirthDay kosong ")
                            Else
                                BirthDay = table.Rows(x).Item(23).ToString()
                                '-------Split Data Value-------
                                'BirthDay : 19790405
                                dt = Strings.Right(BirthDay, 2)
                                mt = Strings.Mid(BirthDay, 5, 2)
                                yr = Strings.Mid(BirthDay, 3, 2)
                                'Join Character
                                BirthDay = String.Join("", dt, mt, yr)
                            End If

                            'Console.WriteLine(IDCard)
                            'Mother_Name()  : Column 36 (+ 3 column )
                            'YENI HERMIATI
                            'Convert DataType 
                            StrMother = table.Rows(x).Item(33).ToString()
                            CountStrMother = Len(StrMother)
                            'Add new Data Temp to New Data After Cleansing
                            TableTemp.Rows(x).Item(0) = table.Rows(x).Item(0).ToString.ToUpper()
                            TableTemp.Rows(x).Item(1) = table.Rows(x).Item(1).ToString.ToUpper()
                            TableTemp.Rows(x).Item(2) = table.Rows(x).Item(2).ToString.ToUpper()
                            TableTemp.Rows(x).Item(3) = table.Rows(x).Item(3).ToString.ToUpper()
                            TableTemp.Rows(x).Item(4) = table.Rows(x).Item(4).ToString.ToUpper()
                            '(1). Column Full Name
                            newName = RemoveMultiCharacter(table.Rows(x).Item(5).ToString, "")
                            newName = StrCleansingGelar(Trim(newName).ToUpper(), x, "").Trim()
                            TableTemp.Rows(x).Item(5) = newName
                            '(2). Column Full English Name
                            TableTemp.Rows(x).Item(6) = newName
                            '(3). Column Full Name ID
                            TableTemp.Rows(x).Item(7) = newName
                            TableTemp.Rows(x).Item(8) = table.Rows(x).Item(8).ToString.ToUpper()
                            TableTemp.Rows(x).Item(9) = table.Rows(x).Item(9).ToString.ToUpper()
                            TableTemp.Rows(x).Item(10) = table.Rows(x).Item(10).ToString.ToUpper()

                            TableTemp.Rows(x).Item(11) = table.Rows(x).Item(11).ToString.ToUpper()
                            TableTemp.Rows(x).Item(12) = table.Rows(x).Item(12).ToString.ToUpper()
                            TableTemp.Rows(x).Item(13) = table.Rows(x).Item(13).ToString.ToUpper()
                            TableTemp.Rows(x).Item(14) = table.Rows(x).Item(14).ToString.ToUpper()
                            TableTemp.Rows(x).Item(15) = table.Rows(x).Item(15).ToString.ToUpper()
                            TableTemp.Rows(x).Item(16) = table.Rows(x).Item(16).ToString.ToUpper()
                            TableTemp.Rows(x).Item(17) = table.Rows(x).Item(17).ToString.ToUpper()
                            TableTemp.Rows(x).Item(18) = table.Rows(x).Item(18).ToString.ToUpper()
                            TableTemp.Rows(x).Item(19) = table.Rows(x).Item(19).ToString.ToUpper()
                            TableTemp.Rows(x).Item(20) = table.Rows(x).Item(20).ToString.ToUpper()

                            TableTemp.Rows(x).Item(21) = table.Rows(x).Item(21).ToString.ToUpper()
                            TableTemp.Rows(x).Item(22) = table.Rows(x).Item(22).ToString.ToUpper()
                            'check birthday
                            TableTemp.Rows(x).Item(23) = RemoveMultiCharacter(table.Rows(x).Item(23).ToString, "")
                            TableTemp.Rows(x).Item(24) = table.Rows(x).Item(24).ToString.ToUpper()
                            '(4). Column Full Phone Number
                            TableTemp.Rows(x).Item(24) = RemoveMultiCharacter(table.Rows(x).Item(24).ToString, "")
                            TableTemp.Rows(x).Item(25) = table.Rows(x).Item(25).ToString.ToUpper()
                            TableTemp.Rows(x).Item(26) = table.Rows(x).Item(26).ToString.ToUpper()
                            TableTemp.Rows(x).Item(27) = table.Rows(x).Item(27).ToString
                            ' (5) Column Gender
                            TableTemp.Rows(x).Item(28) = Gender.ToUpper()
                            TableTemp.Rows(x).Item(29) = table.Rows(x).Item(29).ToString.ToUpper()
                            TableTemp.Rows(x).Item(30) = table.Rows(x).Item(30).ToString.ToUpper()

                            TableTemp.Rows(x).Item(31) = table.Rows(x).Item(31).ToString.ToUpper()
                            TableTemp.Rows(x).Item(32) = table.Rows(x).Item(32).ToString.ToUpper()
                            '(4). Column Mother Name
                            newStrMother = RemoveMultiCharacter(StrMother, " ")
                            TableTemp.Rows(x).Item(33) = StrCleansingGelar(Trim(newStrMother), x, "Ibu").ToString.ToUpper.Trim()
                            '(5) Check Tax Code  : must lenght 15 or null
                            TableTemp.Rows(x).Item(34) = checkTaxIDNumber(Trim(table.Rows(x).Item(34).ToString))
                            '(5). Column Place Birth
                            Place_Birth = Trim(RemoveMultiCharacter(table.Rows(x).Item(35).ToString(), " "))
                            checkBirthPlace(Place_Birth, x, 35)
                            TableTemp.Rows(x).Item(36) = table.Rows(x).Item(36).ToString.ToUpper()
                            '(6). Column Company Name
                            company_name = RemoveMultiCharacter(table.Rows(x).Item(37).ToString, " ")
                            If Set_Company_Name(company_name) > 0 Then
                                'words is detected and must replace to be "NA"
                                company_name = "NA"
                            End If
                            TableTemp.Rows(x).Item(37) = company_name.ToString.ToUpper()
                            TableTemp.Rows(x).Item(38) = table.Rows(x).Item(38).ToString.ToUpper()
                            '(7) Check Occupation And Change Source of Fund
                            'Column Occupation (39) and Column Source of Fund (74)
                            If cb_product.SelectedIndex.ToString = 2 Then
                                TableTemp.Rows(x).Item(39) = table.Rows(x).Item(39).ToString.ToUpper()
                                TableTemp.Rows(x).Item(74) = table.Rows(x).Item(74).ToString.ToUpper()
                            Else
                                'Remove few word in string
                                Dim strOccup As String = table.Rows(x).Item(39).ToString
                                Call checkOccupation(strOccup, x, 39, 74)
                            End If
                            TableTemp.Rows(x).Item(40) = table.Rows(x).Item(40).ToString.ToUpper()

                            TableTemp.Rows(x).Item(41) = table.Rows(x).Item(41).ToString.ToUpper()
                            TableTemp.Rows(x).Item(42) = table.Rows(x).Item(42).ToString.ToUpper()
                            TableTemp.Rows(x).Item(43) = table.Rows(x).Item(43).ToString.ToUpper()
                            TableTemp.Rows(x).Item(44) = table.Rows(x).Item(44).ToString.ToUpper()
                            TableTemp.Rows(x).Item(45) = table.Rows(x).Item(45).ToString.ToUpper()
                            TableTemp.Rows(x).Item(46) = table.Rows(x).Item(46).ToString.ToUpper()
                            TableTemp.Rows(x).Item(47) = table.Rows(x).Item(47).ToString.ToUpper()
                            '(7). Column Full Phone Number
                            TableTemp.Rows(x).Item(48) = RemoveMultiCharacter(table.Rows(x).Item(48).ToString, "")
                            TableTemp.Rows(x).Item(49) = table.Rows(x).Item(49).ToString.ToUpper()
                            TableTemp.Rows(x).Item(50) = table.Rows(x).Item(50).ToString.ToUpper()
                            TableTemp.Rows(x).Item(51) = table.Rows(x).Item(51).ToString.ToUpper()
                            '(8) Column Dati1 and Kode Pos1 (KTP)
                            ' Filter by Column Kelurahan and Kecamatan
                            'Param : ByRef kelurahan As String, ByRef kecamatan As String, ByRef dati As String, ByRef pos As String, ByRef x As Integer, ByRef celKel As Integer, ByRef celKec As Integer
                            'Call Procedure for Check data Dati1 and Post
                            Call checkKodeDatiPos(RemoveMultiCharacter(table.Rows(x).Item(52).ToString, ""), RemoveMultiCharacter(table.Rows(x).Item(53).ToString, ""), RemoveMultiCharacter(table.Rows(x).Item(47).ToString, ""), RemoveMultiCharacter(table.Rows(x).Item(51).ToString, ""), x, 52, 53, 47, 51, "IDCard")
                            '(9). Column Full ID Card Address
                            TableTemp.Rows(x).Item(54) = RemoveMultiCharacter(table.Rows(x).Item(54).ToString.ToUpper(), "")
                            TableTemp.Rows(x).Item(55) = table.Rows(x).Item(55).ToString.ToUpper()
                            'Dati : 56
                            'pos: 60
                            'kel : 61
                            'kec : 62
                            '(10) Column Dati1 and Kode Pos1 (Company)
                            ' Filter by Column Kelurahan and Kecamatan 
                            'Condition product
                            'Private Sub checkKodeDatiPos(ByRef kelurahan As String, ByRef kecamatan As String, ByRef dati As String, ByRef pos As String, ByRef x As Integer, ByRef celKel As Integer, ByRef celKec As Integer, ByRef celldati As Integer, ByRef cellpos As Integer, ByRef ket As String)
                            If cb_product.SelectedIndex.ToString = 2 Then
                                TableTemp.Rows(x).Item(56) = table.Rows(x).Item(56).ToString.ToUpper()
                                TableTemp.Rows(x).Item(60) = table.Rows(x).Item(60).ToString.ToUpper()
                                TableTemp.Rows(x).Item(61) = table.Rows(x).Item(61).ToString.ToUpper()
                                TableTemp.Rows(x).Item(62) = table.Rows(x).Item(62).ToString.ToUpper()
                            Else
                                'product kredivo
                                Call checkKodeDatiPos(RemoveMultiCharacter(table.Rows(x).Item(61).ToString, ""), RemoveMultiCharacter(table.Rows(x).Item(62).ToString, ""), RemoveMultiCharacter(table.Rows(x).Item(56).ToString, ""), RemoveMultiCharacter(table.Rows(x).Item(60).ToString, ""), x, 61, 62, 56, 60, "Company")
                            End If
                            TableTemp.Rows(x).Item(57) = table.Rows(x).Item(57).ToString.ToUpper()
                            TableTemp.Rows(x).Item(58) = table.Rows(x).Item(58).ToString.ToUpper()
                            TableTemp.Rows(x).Item(59) = table.Rows(x).Item(59).ToString.ToUpper()
                            '(11). Column Full Company Shipping Address
                            TableTemp.Rows(x).Item(63) = RemoveMultiCharacter(table.Rows(x).Item(63).ToString.ToUpper(), " ")
                            TableTemp.Rows(x).Item(64) = table.Rows(x).Item(64).ToString.ToUpper()
                            'Copy Kode DATI ID Card Terkini
                            TableTemp.Rows(x).Item(65) = TableTemp.Rows(x).Item(47).ToString.ToUpper()
                            '(12). Column Full Phone Number 3
                            TableTemp.Rows(x).Item(66) = RemoveMultiCharacter(table.Rows(x).Item(66).ToString.ToUpper(), "")
                            TableTemp.Rows(x).Item(66) = table.Rows(x).Item(66).ToString.ToUpper()
                            TableTemp.Rows(x).Item(67) = table.Rows(x).Item(67).ToString.ToUpper()
                            TableTemp.Rows(x).Item(68) = RemoveMultiCharacter(table.Rows(x).Item(68).ToString.ToUpper(), "")
                            'Copy Kode POS ID Card
                            TableTemp.Rows(x).Item(69) = TableTemp.Rows(x).Item(51).ToString.ToUpper()
                            '(6) Column Dati1 and Kode Pos2 (Terkini)
                            'Copy data Kelurahan dan Kecamatan Terkini
                            TableTemp.Rows(x).Item(70) = TableTemp.Rows(x).Item(52).ToString.ToUpper()
                            TableTemp.Rows(x).Item(71) = TableTemp.Rows(x).Item(53).ToString.ToUpper()
                            '(10). Column Full New Address Terkini
                            TableTemp.Rows(x).Item(72) = TableTemp.Rows(x).Item(54).ToString.ToUpper()
                            TableTemp.Rows(x).Item(73) = table.Rows(x).Item(73).ToString.ToUpper()
                            'Source of Foud is change (74)
                            TableTemp.Rows(x).Item(75) = table.Rows(x).Item(75).ToString.ToUpper()
                            TableTemp.Rows(x).Item(76) = table.Rows(x).Item(76).ToString.ToUpper()
                            TableTemp.Rows(x).Item(77) = table.Rows(x).Item(77).ToString.ToUpper()
                            TableTemp.Rows(x).Item(78) = table.Rows(x).Item(78).ToString.ToUpper()
                            TableTemp.Rows(x).Item(79) = table.Rows(x).Item(79).ToString.ToUpper()
                            TableTemp.Rows(x).Item(80) = table.Rows(x).Item(80).ToString.ToUpper()

                            TableTemp.Rows(x).Item(81) = table.Rows(x).Item(81).ToString.ToUpper()
                            TableTemp.Rows(x).Item(82) = table.Rows(x).Item(82).ToString.ToUpper()
                            TableTemp.Rows(x).Item(83) = table.Rows(x).Item(83).ToString.ToUpper()
                            TableTemp.Rows(x).Item(84) = table.Rows(x).Item(84).ToString.ToUpper()
                            TableTemp.Rows(x).Item(85) = table.Rows(x).Item(85).ToString
                            TableTemp.Rows(x).Item(86) = table.Rows(x).Item(86).ToString.ToUpper()
                            TableTemp.Rows(x).Item(87) = table.Rows(x).Item(87).ToString.ToUpper()

                            'Correction Condition 1 : Remove Lenght Character MotherName less than 3
                            If CountStrMother > 2 Then
                                'Set value in Rows
                                load_cif.Rows(x).Cells(0).Value = "Pass"
                                'More than 3 Character and Pass Selection 1
                                '....
                                'Correction Condition 2 : Matching Condition ID Card with BirthDay
                                '....
                                'Correction Condition Gender = Female(F)
                                'True is get point 1 
                                'False is get point 0
                                If Gender.ToUpper() = "F" Then
                                    'Correction Condition Date of Birthday
                                    'tanggal lahir =  Strings.Mid(IDCard, 7, 2) 
                                    '....
                                    dt = Convert.ToUInt32(dt)
                                    dt = (40 + dt)
                                    BirthDay = String.Join("", dt, mt, yr)
                                    '...
                                    'checking len string in ID Card
                                    If Len(table.Rows(x).Item(14).ToString()) = 16 Then
                                        'Matching Condition ID Card with BirthDay
                                        'Pass the Filtering
                                        If Strings.Equals(IDCard, BirthDay) Then
                                            load_cif.Rows(x).Cells(1).Value = "Pass"
                                        Else
                                            If cb_accurate.SelectedIndex = 0 Then
                                                ''Set Point
                                                takedown = False
                                                poinIdBirth += 1
                                                load_cif.Rows(x).Cells(1).Value = "Not Pass"
                                                'load_cif.Rows(x).Cells(1).Value = "Pass"
                                                'set reason
                                                reason.Add("Take Out-IDCard BirthDay berbeda ")
                                            Else
                                                load_cif.Rows(x).Cells(1).Value = "Pass"
                                            End If

                                        End If
                                    Else
                                        poinIdBirth += 1
                                        takedown = False
                                        load_cif.Rows(x).Cells(1).Value = "Not Pass"
                                        'set reason
                                        reason.Add("Take Out-ID Card less 16 Character ")
                                    End If
                                ElseIf Gender.ToUpper() = "M" Then
                                    'Correction Condition Gender = Female(M)
                                    'Join Character
                                    BirthDay = String.Join("", dt, mt, yr)
                                    '...
                                    'checking len string in ID Card
                                    If Len(table.Rows(x).Item(14).ToString()) = 16 Then
                                        'Matching Condition ID Card with BirthDay
                                        'Pass the Filtering
                                        If Strings.Equals(IDCard, BirthDay) Then
                                            load_cif.Rows(x).Cells(1).Value = "Pass"
                                        Else
                                            If cb_accurate.SelectedIndex = 0 Then
                                                ''Set Point
                                                takedown = False
                                                poinIdBirth += 1
                                                load_cif.Rows(x).Cells(1).Value = "Not Pass"
                                                'load_cif.Rows(x).Cells(1).Value = "Pass"
                                                'set reason
                                                reason.Add("Take Out-IDCard BirthDay berbeda ")
                                            Else
                                                load_cif.Rows(x).Cells(1).Value = "Pass"
                                            End If
                                        End If
                                    Else
                                        poinIdBirth += 1
                                        takedown = False
                                        load_cif.Rows(x).Cells(1).Value = "Not Pass"
                                        'set reason
                                        reason.Add("Take Out-ID Card less 16 Character ")
                                    End If
                                Else
                                    'Set Point
                                    takedown = False
                                    'load_cif.Rows(x).Cells(1).Value = "Gender is Null"
                                    poinGender += 1
                                    reason.Add("Take Out-Gender kosong ")
                                End If
                                ''...
                                'End Column Mother Name
                            Else
                                'less 3 character 
                                'Set Point 
                                takedown = False
                                poinMother += 1
                                'Set value in Rows
                                load_cif.Rows(x).Cells(0).Value = "Not Pass"
                                reason.Add("Take Out-Nama Ibu Kandung ")
                                '....
                            End If

EndProccesCleansing:

                            'Add new Rows for Reason
                            TableTemp.Rows(x).Item(88) = String.Join(" ", TryCast(reason.ToArray(GetType(String)), String()))
                            'clone or copy data to other datable  
                            If takedown = True Then
                                If dt_collec.Rows.Count <= 0 Then
                                    'GoTo StartProccessCleansing
                                Else
                                    'Expected Column [Multifinace Key] ,[Kolektivitas]
                                    If dt_collec.Rows.Count > 0 Then
                                        resultCollect = dt_collec.Select("[" + dt_collec.Columns(0).ColumnName + "] ='" + table.Rows(x).Item(79).ToString + "' ").FirstOrDefault()
                                        'add info
                                        'add table takedown                  
                                        TableTakeDown.ImportRow(TableTemp.Rows(x))
                                        load_cif.Rows(x).Cells(5).Value = "Take Down"
                                        load_cif.Rows(x).Cells(5).Style.BackColor = Color.Tomato
                                        'add data summary to table temp
                                        TableTemp.Rows(x).Item(88) = "Take Out-" + resultCollect.Item(1).ToString
                                    End If
                                End If
                                TableFinal.ImportRow(TableTemp.Rows(x))
                                'send to datagridview
                                load_cif_success.DataSource = TableFinal
                                takedown = True
                                load_cif.Rows(x).Cells(5).Value = "Success"
                                load_cif.Rows(x).Cells(5).Style.BackColor = Color.LightGreen

                            ElseIf takedown = False Then
                                TableTakeDown.ImportRow(TableTemp.Rows(x))
                                'send to datagridview
                                load_cif_failed.DataSource = TableTakeDown
                                load_cif.Rows(x).Cells(5).Value = "Take Down"
                                load_cif.Rows(x).Cells(5).Style.BackColor = Color.Tomato
                            End If

                            'Clear Point 
                            takedown = Nothing
                            'reset array list
                            reason.Clear()
                            'event load data
                            Application.DoEvents()
                            'process loading persen
                            Dim persen As Integer
                            persen = (x / Int(load_cif.RowCount - 1)) * 100
                            If x = Int(load_cif.RowCount - 2) Then
                                persen = 100
                            End If
                            txt_checking_load.Text = " ( " + Int(persen).ToString + "% ) "

                            C_rows2.Text = load_cif_success.RowCount - 1
                            C_rows3.Text = load_cif_failed.RowCount - 1
                        Next x

                        'add new rows null
                        If TableTakeDown.Rows.Count <= 0 Then
                            'insert new blank
                            Dim rowBlank As DataRow
                            rowBlank = TableTakeDown.NewRow
                            TableTakeDown.Rows.InsertAt(rowBlank, 0)
                            'send to datagridview
                            load_cif_failed.DataSource = TableTakeDown
                        End If
                        If TableFinal.Rows.Count <= 0 Then
                            'insert new blank
                            Dim rowBlank As DataRow
                            rowBlank = TableFinal.NewRow
                            TableFinal.Rows.InsertAt(rowBlank, 0)
                            'send to datagridview
                            load_cif_success.DataSource = TableFinal
                        End If

                        'enable button
                        btn_summary.Enabled = True
                        btn_cleansing.Enabled = True
                        ' Show Count Rows in Datagridview
                        C_rows1.Text = load_cif.RowCount - 1
                        C_rows2.Text = TableFinal.Rows.Count
                        C_rows3.Text = TableTakeDown.Rows.Count
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
                        MessageBox.Show("Duration : " + totalSpan, "Cleansing Done.")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Alert")
        Finally
            'disabled sort
            For Each b As DataGridViewColumn In load_cif_failed.Columns
                b.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            For Each c As DataGridViewColumn In load_cif_success.Columns
                c.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub
    Private Function checkTaxIDNumber(ByRef taxCode As String)
        'tax code
        If Not Len(taxCode.ToString()) = 15 And Not Len(taxCode.ToString()) = 0 Then
            ''Set Point
            takedown = False
            'set reason
            reason.Add("Take Out-Tax ID Number ")
        Else
            If Strings.Left(Trim(taxCode.ToString), 9) = "123456789" Then
                takedown = False
                'set reason
                reason.Add("Take Out-Tax ID Number ")
            ElseIf Strings.Left(Trim(taxCode.ToString), 8) = "12345678" Then
                takedown = False
                'set reason
                reason.Add("Take Out-Tax ID Number ")
            ElseIf Strings.Left(Trim(taxCode.ToString), 5) = "12345" Then
                takedown = False
                'set reason
                reason.Add("Take Out-Tax ID Number ")
            ElseIf Strings.Left(Trim(taxCode.ToString), 8) = "00000000" Then
                takedown = False
                'set reason
                reason.Add("Take Out-Tax ID Number ")
            End If
        End If
        Return taxCode
    End Function


    Private Function checkTakeOutCity(ByRef word As String) As String
        Dim result As String
        Dim checktable As New DataTable()
        poinOccup += 0
        If String.IsNullOrWhiteSpace(word) Then
            result = ""
        Else
            cmd = New SqlCommand("SELECT TOP 1 [NO],[TAKE_OUT_CITY] FROM [DB_MASTER].[dbo].[DB_TAKE_OUT_CITY] WHERE [TAKE_OUT_CITY] = '" + word.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data Occupation
                result = ""
            Else
                result = word
            End If
        End If
        Return result
    End Function
    Private Function checkTakeOutName(ByRef str As String) As Integer
        Dim result As Integer
        Dim checktable As New DataTable()
        poinOccup += 0
        If String.IsNullOrWhiteSpace(str) Then
            result = 0
        Else
            'SELECT [NO], [Code_Occupation], [Code_CIF_Source_of_Fund] FROM [DB_MASTER].[dbo].[DB_OCCUPATION_KDV]
            cmd = New SqlCommand("SELECT TOP 1 [NO],[TAKE_OUT_NAME] FROM [DB_MASTER].[dbo].[DB_TAKE_OUT_WORD] WHERE [TAKE_OUT_NAME] = '" + str.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            result = checktable.Rows.Count()
        End If
        Return result
    End Function
    Private Sub checkOccupation(ByRef occupation As String, ByRef x As Integer, ByRef cellOccup As Integer, ByRef cellFund As Integer)
        Dim checktable As New DataTable()
        poinOccup += 0
        If String.IsNullOrWhiteSpace(occupation) Then
            takedown = False
            poinOccup += 1
            'set alert if not found
            'load_cif.Rows(x).Cells(2).Value = "Not Pass"
            'set reason
            reason.Add("Take Out-Occupation kosong ")
        Else
            'SELECT [NO], [Code_Occupation], [Code_CIF_Source_of_Fund] FROM [DB_MASTER].[dbo].[DB_OCCUPATION_KDV]
            cmd = New SqlCommand(" SELECT [NO], [Code_Occupation_True], [Code_CIF_Source_of_Fund] FROM [DB_MASTER].[dbo].[DB_OCCUPATION_KDV] WHERE [Code_Occupation_False] = '" + occupation.ToString + "' ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data Occupation
                TableTemp.Rows(x).Item(cellOccup) = checktable.Rows(0).Item(1).ToString.ToUpper().Trim()
                'get data Source of fund
                TableTemp.Rows(x).Item(cellFund) = checktable.Rows(0).Item(2).ToString.ToUpper().Trim()
                'set alert if found
                'load_cif.Rows(x).Cells(2).Value = "pass"
            Else
                takedown = False
                poinOccup += 1
                'set alert if not found
                'load_cif.Rows(x).Cells(2).Value = "Not Pass"
                'set reason
                reason.Add("Take Out-Occupation salah ")
            End If

        End If
    End Sub
    Private Sub checkBirthPlace(ByRef place As String, ByRef x As Integer, ByRef y As Integer)
        Dim arrPlace As New ArrayList()
        Dim takeOutCity As Integer
        Dim checktable, checktable2, checktable3 As New DataTable()
        poinPlaceBirth += 0
        'Dim number As Integer
        ' if data is same, will remove index array and not add to array new
        SQLConnection = New SqlConnection(SQLConnectionString)
        'CALL CLASS CONNECTION
        'SQL.ConnSQL()
        If String.IsNullOrWhiteSpace(place) Then
            'place
            TableTemp.Rows(x).Item(y) = "Null"
            takedown = False
            poinPlaceBirth += 1
            'set alert if not found
            load_cif.Rows(x).Cells(2).Value = "Not Pass"
            'set reason
            reason.Add("Take Out-PlaceBirth kosong ")
        Else
            If place.Length > 3 Then
                If String.IsNullOrWhiteSpace(place.Substring(1, 1).ToArray()) And String.IsNullOrWhiteSpace(place.Substring(3, 1).ToArray()) Then
                    'Console.WriteLine("kosong")
                    ''Inisial Found
                    place = place.Replace(" ", "")
                    arrPlace.Add(place)
                    'output in array list
                    place = String.Join(" ", TryCast(arrPlace.ToArray(GetType(String)), String()))
                    arrPlace.Clear()
                End If
            End If
            'Remove few word in string
            words = place.Split(New Char() {" "c})
            'foun word who same in string
            For j As Integer = 0 To words.Length - 1
                arrString.Add(checkTakeOutCity(words(j)))
            Next
            place = String.Join(" ", TryCast(arrString.ToArray(GetType(String)), String()))
            arrString.Clear()
            'Remove few word in string
            'foun word who same in string
            takeOutCity = checkTakeOutName(Trim(place))
            If takeOutCity > 0 Then
                'found is takedown
                'Set Point
                takedown = False
                poinPlaceBirth += 1
                reason.Add("Take Out-Tempat Lahir ")
                load_cif.Rows(x).Cells(2).Value = "Not Pass"
                GoTo cleansingCityFinish
            End If

            cmd = New SqlCommand("SELECT TOP 1 [NO] ,[True City] ,[False City] FROM [DB_MASTER].[dbo].[DB_CORRECTION_CITY] WHERE [False City] = '" + Trim(place.ToString) + "'", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "DATI","POS"
            If checktable.Rows.Count() > 0 Then
                'place
                TableTemp.Rows(x).Item(y) = checktable.Rows(0).Item(1).ToString.ToUpper()
                'set alert if found
                load_cif.Rows(x).Cells(2).Value = "Pass"
            Else
                cmd2 = New SqlCommand("SELECT TOP 1 [Nama], [Kelurahan], [Kecamatan] FROM [DB_MASTER].[dbo].[DB_DATI2] WHERE [Nama] LIKE '%" + Trim(place.ToString) + "%' OR [Kelurahan] LIKE '%" + Trim(place.ToString) + "%' OR [Kecamatan] LIKE '%" + Trim(place.ToString) + "%'  ", SQLConnection)
                Dim adapter2 As New SqlDataAdapter(cmd2)
                adapter2.Fill(checktable2)

                'Checking data to any rows in table "DATI","POS"
                If checktable2.Rows.Count() > 0 Then
                    'place
                    TableTemp.Rows(x).Item(y) = Trim(place.ToUpper)
                    'set alert if found
                    load_cif.Rows(x).Cells(2).Value = "Pass"
                Else
                    'Data not found in database or new record must be uploaded again
                    'set point
                    takedown = False
                    poinPlaceBirth += 1
                    'place
                    TableTemp.Rows(x).Item(y) = Trim(place.ToUpper)
                    'set alert if not found
                    load_cif.Rows(x).Cells(2).Value = "Not Pass"
                    'set reason
                    reason.Add("Take Out-PlaceBirth tidak ditemukan ")
                End If
            End If
        End If
        place = ""
cleansingCityFinish:
    End Sub

    Private Sub checkKodeDatiPos(ByRef kelurahan As String, ByRef kecamatan As String, ByRef dati As String, ByRef pos As String, ByRef x As Integer, ByRef celKel As Integer, ByRef celKec As Integer, ByRef celldati As Integer, ByRef cellpos As Integer, ByRef ket As String)
        'Dim number As Integer
        'default
        'load_cif.Rows(x).Cells(3).Style.BackColor = Color.White
        poinDati += 0
        ' if data is same, will remove index array and not add to array new
        SQLConnection = New SqlConnection(SQLConnectionString)
        'CALL CLASS CONNECTION
ProcessNol:
        If String.IsNullOrWhiteSpace(kelurahan) And String.IsNullOrWhiteSpace(kecamatan) And String.IsNullOrWhiteSpace(dati) And String.IsNullOrWhiteSpace(pos) Then
            'kelurahan
            TableTemp.Rows(x).Item(celKel) = kelurahan
            'kecamatan
            TableTemp.Rows(x).Item(celKec) = kecamatan
            'dati
            TableTemp.Rows(x).Item(celldati) = dati
            'po
            TableTemp.Rows(x).Item(cellpos) = pos
            takedown = False
            poinDati += 1
            'set alert if not found
            load_cif.Rows(x).Cells(3).Value = "Not Pass"
            'set reason
            reason.Add("Take Out-Dati " + ket + " kosong ")
        Else
            'check param is null
            If String.IsNullOrWhiteSpace(kelurahan) Then
                kelurahan = ""
            End If
            If String.IsNullOrWhiteSpace(kecamatan) Then
                kecamatan = ""
            End If
            If String.IsNullOrWhiteSpace(dati) Then
                dati = ""
            End If
            If String.IsNullOrWhiteSpace(pos) Then
                pos = ""
            End If
            'Process Cleansing Dati
            'Process If Satu       
            cmd = New SqlCommand("SELECT TOP 1 [NO],[KEL_TRUE],[KEL_FALSE] FROM [DB_MASTER].[dbo].[DB_CORRECTION_DATI] WHERE [KEL_FALSE] = '" + kelurahan.ToString + "'", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim checktable As New DataTable()
            adapter.Fill(checktable)
            'Checking data to any rows in table "DATI","POS"
            If Int(checktable.Rows.Count()) > 0 Then
                'kelurahan
                kelurahan = checktable.Rows(0).Item(1).ToString.ToUpper().Trim()
            End If
            cmd2 = New SqlCommand("SELECT TOP 1 [NO], [KEC_TRUE],[KEC_FALSE] FROM [DB_MASTER].[dbo].[DB_CORRECTION_DATI] WHERE  [KEC_FALSE] = '" + kecamatan.ToString + "'", SQLConnection)
            Dim adapter1 As New SqlDataAdapter(cmd2)
            Dim checktable1 As New DataTable()
            adapter1.Fill(checktable1)
            'Checking data to any rows in table "DATI","POS"
            If Int(checktable1.Rows.Count()) > 0 Then
                'Kecamatan
                kecamatan = checktable1.Rows(0).Item(1).ToString.ToUpper().Trim()
            End If
            'Process If Dua    
            cmd3 = New SqlCommand("SELECT TOP 1 [Kelurahan],[Kecamatan],[Kode Pos], [Dati 2] FROM [DB_MASTER].[dbo].[DB_DATI2] WHERE  ([Kelurahan] = '" + kelurahan.ToString + "' AND [Kecamatan] = '" + kecamatan.ToString + "')AND ([Kode Pos] = '" + pos + "' AND [Dati 2] = '" + dati + "')", SQLConnection)
            Dim adapter2 As New SqlDataAdapter(cmd3)
            Dim checktable2 As New DataTable()
            adapter2.Fill(checktable2)
            If Int(checktable2.Rows.Count()) > 0 Then
                'kelurahan
                TableTemp.Rows(x).Item(celKel) = checktable2.Rows(0).Item(0).ToString.ToUpper().Trim()
                'kecamatan
                TableTemp.Rows(x).Item(celKec) = checktable2.Rows(0).Item(1).ToString.ToUpper().Trim()
                'pos
                TableTemp.Rows(x).Item(cellpos) = checktable2.Rows(0).Item(2).ToString.ToUpper().Trim()
                'dati
                TableTemp.Rows(x).Item(celldati) = checktable2.Rows(0).Item(3).ToString.ToUpper().Trim()
                'set alert if  found
                load_cif.Rows(x).Cells(3).Value = "Pass"
            Else
                'Process If Tiga    
                cmd4 = New SqlCommand("SELECT TOP 1 [Kelurahan],[Kecamatan],[Kode Pos], [Dati 2] FROM  [DB_MASTER].[dbo].[DB_DATI2] WHERE  ([Kelurahan]  LIKE '%" + kelurahan.ToString + "%' AND [Kecamatan] LIKE '%" + kecamatan.ToString + "%') AND ([Kode Pos] = '" + pos + "' AND [Dati 2] = '" + dati + "')", SQLConnection)
                Dim adapter3 As New SqlDataAdapter(cmd4)
                Dim checktable3 As New DataTable()
                adapter3.Fill(checktable3)
                If Int(checktable3.Rows.Count()) > 0 Then
                    'kelurahan
                    TableTemp.Rows(x).Item(celKel) = checktable3.Rows(0).Item(0).ToString.ToUpper().Trim()
                    'kecamatan
                    TableTemp.Rows(x).Item(celKec) = checktable3.Rows(0).Item(1).ToString.ToUpper().Trim()
                    'pos
                    TableTemp.Rows(x).Item(cellpos) = checktable3.Rows(0).Item(2).ToString.ToUpper().Trim()
                    'dati
                    TableTemp.Rows(x).Item(celldati) = checktable3.Rows(0).Item(3).ToString.ToUpper().Trim()
                    'set alert if  found
                    load_cif.Rows(x).Cells(3).Value = "Pass"
                Else
                    'Process If Empat
                    'For Data not in Table 
                    cmd5 = New SqlCommand("SELECT TOP 1 [Kelurahan],[Kecamatan],[Kode Pos], [Dati 2] FROM  [DB_MASTER].[dbo].[DB_DATI2] WHERE ([Kelurahan]  LIKE '%" + kelurahan.ToString + "%' AND [Kecamatan] LIKE '%" + kecamatan.ToString + "%') AND ([Kode Pos] = '" + pos + "' OR [Dati 2] = '" + dati + "')", SQLConnection)
                    Dim adapter4 As New SqlDataAdapter(cmd5)
                    Dim checktable4 As New DataTable()
                    adapter4.Fill(checktable4)
                    If Int(checktable4.Rows.Count()) > 0 Then
                        'kelurahan
                        TableTemp.Rows(x).Item(celKel) = checktable4.Rows(0).Item(0).ToString.ToUpper().Trim()
                        'kecamatan
                        TableTemp.Rows(x).Item(celKec) = checktable4.Rows(0).Item(1).ToString.ToUpper().Trim()
                        'pos
                        TableTemp.Rows(x).Item(cellpos) = checktable4.Rows(0).Item(2).ToString.ToUpper().Trim()
                        'dati
                        TableTemp.Rows(x).Item(celldati) = checktable4.Rows(0).Item(3).ToString.ToUpper().Trim()
                        'set alert if  found
                        load_cif.Rows(x).Cells(3).Value = "Pass"

                    Else
                        'Process If Empat
                        'For Data not in Table 
                        cmd6 = New SqlCommand("SELECT TOP 1 [Kelurahan],[Kecamatan],[Kode Pos], [Dati 2] FROM  [DB_MASTER].[dbo].[DB_DATI2] WHERE ([Kelurahan] LIKE '%" + kelurahan.ToString + "%' AND [Kecamatan] LIKE '%" + kecamatan.ToString + "%')", SQLConnection)
                        Dim adapter5 As New SqlDataAdapter(cmd6)
                        Dim checktable5 As New DataTable()
                        adapter4.Fill(checktable5)
                        If Int(checktable5.Rows.Count()) > 0 Then
                            'kelurahan
                            TableTemp.Rows(x).Item(celKel) = checktable5.Rows(0).Item(0).ToString.ToUpper().Trim()
                            'kecamatan
                            TableTemp.Rows(x).Item(celKec) = checktable5.Rows(0).Item(1).ToString.ToUpper().Trim()
                            'pos
                            TableTemp.Rows(x).Item(cellpos) = checktable5.Rows(0).Item(2).ToString.ToUpper().Trim()
                            'dati
                            TableTemp.Rows(x).Item(celldati) = checktable5.Rows(0).Item(3).ToString.ToUpper().Trim()
                            'set alert if  found
                            load_cif.Rows(x).Cells(3).Value = "Pass"
                            load_cif.Rows(x).Cells(3).Style.BackColor = Color.Aqua

                        Else
                            ' Last Chooce for Accurate Cleansing Dati

                            If cb_accurate.SelectedIndex.ToString = 0 Then
                                'Normal Accurate
                                'For Data not in Table 
                                cmd7 = New SqlCommand("SELECT TOP 1 [Kelurahan],[Kecamatan],[Kode Pos], [Dati 2] FROM  [DB_MASTER].[dbo].[DB_DATI2] WHERE ([Kecamatan] LIKE '%" + kecamatan.ToString + "%' AND [Dati 2] = '" + dati + "') AND ([Kelurahan] LIKE '%" + kelurahan + "%' OR [Kode Pos] = '" + pos + "')", SQLConnection)

                            ElseIf cb_accurate.SelectedIndex.ToString = 1 Then
                                'Adjust 50%
                                'For Data not in Table 
                                cmd7 = New SqlCommand("SELECT TOP 1 [Kelurahan],[Kecamatan],[Kode Pos], [Dati 2] FROM  [DB_MASTER].[dbo].[DB_DATI2] WHERE ([Kecamatan] LIKE '%" + kecamatan.ToString + "%' AND [Kode Pos] = '" + pos + "') OR ([Dati 2] = '" + dati + "' AND [Kelurahan] LIKE '%" + kelurahan + "%')", SQLConnection)

                            ElseIf cb_accurate.SelectedIndex.ToString = 2 Then
                                'Adjust 75%
                                'For Data not in Table 
                                cmd7 = New SqlCommand("SELECT TOP 1 [Kelurahan],[Kecamatan],[Kode Pos], [Dati 2] FROM  [DB_MASTER].[dbo].[DB_DATI2] WHERE ([Kecamatan] LIKE '%" + kecamatan.ToString + "%' AND [Dati 2] = '" + dati + "') OR ([Dati 2] = '" + dati + "' AND [Kode Pos] = '" + pos + "')", SQLConnection)
                            Else
                                'Adjust 100%
                                'For Data not in Table 
                                cmd7 = New SqlCommand("SELECT TOP 1 [Kelurahan],[Kecamatan],[Kode Pos], [Dati 2] FROM  [DB_MASTER].[dbo].[DB_DATI2] WHERE [Kecamatan] LIKE '%" + kecamatan.ToString + "%' OR [Dati 2] = '" + dati + "'", SQLConnection)
                            End If
                            Dim adapter6 As New SqlDataAdapter(cmd7)
                            Dim checktable6 As New DataTable()
                            adapter6.Fill(checktable6)
                            If Int(checktable6.Rows.Count()) > 0 Then
                                'kelurahan
                                TableTemp.Rows(x).Item(celKel) = checktable6.Rows(0).Item(0).ToString.ToUpper().Trim()
                                'kecamatan
                                TableTemp.Rows(x).Item(celKec) = checktable6.Rows(0).Item(1).ToString.ToUpper().Trim()
                                'pos
                                TableTemp.Rows(x).Item(cellpos) = checktable6.Rows(0).Item(2).ToString.ToUpper().Trim()
                                'dati
                                TableTemp.Rows(x).Item(celldati) = checktable6.Rows(0).Item(3).ToString.ToUpper().Trim()
                                'set alert if  found
                                load_cif.Rows(x).Cells(3).Value = "Pass"
                                load_cif.Rows(x).Cells(3).Style.BackColor = Color.Gray
                            Else
                                'NOT FOUND
                                takedown = False
                                poinDati += 1
                                'kelurahan
                                TableTemp.Rows(x).Item(celKel) = kelurahan
                                'kecamatan
                                TableTemp.Rows(x).Item(celKec) = kecamatan
                                'pos
                                TableTemp.Rows(x).Item(cellpos) = pos
                                'dati
                                TableTemp.Rows(x).Item(celldati) = dati
                                'set alert if not found
                                load_cif.Rows(x).Cells(3).Value = "Not Pass"
                                'set reason
                                reason.Add("Take Out-Dati " + ket + " tidak ditemukan ")
                            End If

                        End If

                    End If

                End If
            End If
            'clear 
            kecamatan = ""
            kelurahan = ""
            pos = ""
            dati = ""
        End If
    End Sub
    Private Function checkDegree(ByRef degree As String, ByRef column As String)
        Dim output As String = ""
        ' if data is same, will remove index array and not add to array new
        SQLConnection = New SqlConnection(SQLConnectionString)
        cmd = New SqlCommand("SELECT * FROM [DB_MASTER].[dbo].[DB_GELAR] WHERE " + column + " = '" + degree + "'", SQLConnection)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim checktable As New DataTable()
        adapter.Fill(checktable)

        'Checking data to any rows in table "GELAR"
        If checktable.Rows.Count() > 0 Then
            output = ""
        Else
            'For Data not in Table (Gelar)
            output = degree
        End If
        Return output
    End Function
    Function StrCleansingGelar(ByRef str As String, ByRef x As Integer, ByRef ket As String) As String
        Dim FinalWord As String = ""
        Dim arrNamePoint As Integer = 0
        poinName += 0
        'string is null
        If String.IsNullOrWhiteSpace(str) Then
            FinalWord = str
            'Set Point
            poinName += 1
            takedown = False
            reason.Add("Take Out-Nama " + ket + " kosong ")
            load_cif.Rows(x).Cells(4).Value = "Not Pass"
            'string is less than 2 char
        ElseIf str.Length <= 2 Then
            FinalWord = str
            'Set Point
            takedown = False
            poinName += 1
            reason.Add("Take Out-Nama  " + ket + " kurang dari 2 karakter ")
            load_cif.Rows(x).Cells(4).Value = "Not Pass"
            'string is more than 2 char
        ElseIf str.Length >= 3 Then
            If load_cif.Rows(x).Cells(4).Value = "Not Pass" Then
                load_cif.Rows(x).Cells(4).Value = "Not Pass"
            Else
                load_cif.Rows(x).Cells(4).Value = "Pass"
            End If

            'Split String to char for condition 
            'Dim charArray() As Char = str.ToCharArray
            'Example Word : N A N
            If str.Length = 5 Then
                If String.IsNullOrWhiteSpace(str.Substring(1, 1).ToArray()) And String.IsNullOrWhiteSpace(str.Substring(3, 1).ToArray()) Then
                    ''Inisial Found
                    str = str.Replace(" ", "")
                    arrString.Add(str)
                    'output in array list
                    FinalWord = String.Join(" ", TryCast(arrString.ToArray(GetType(String)), String()))
                    arrString.Clear()
                    GoTo nextFinish
                End If
            End If

            'Remove few word in string
            'foun word who same in string
            arrNamePoint = checkTakeOutName(Trim(str))
            If arrNamePoint > 0 Then
                'found is takedown
                'Set Point
                takedown = False
                poinName += 1
                reason.Add("Take Out-Nama " + ket + " tidak ada ")
                load_cif.Rows(x).Cells(4).Value = "Not Pass"
                FinalWord = str.ToString.ToUpper()
                GoTo nextFinish
            Else
                GoTo nextCleansing
            End If

nextCleansing:
            ' Example Word : A N D I 
            If str.Length > 5 Then
                If String.IsNullOrWhiteSpace(str.Substring(1, 1).ToArray()) And String.IsNullOrWhiteSpace(str.Substring(3, 1).ToArray()) And String.IsNullOrWhiteSpace(str.Substring(5, 1).ToArray()) Then
                    'Console.WriteLine("kosong")
                    ''Inisial Found
                    str = str.Replace(" ", "")
                    arrString.Add(str)
                    'output in array list
                    FinalWord = String.Join(" ", TryCast(arrString.ToArray(GetType(String)), String()))
                    arrString.Clear()
                    GoTo nextFinish
                End If
            End If
            'Step 2 : Split String and add in array
            '- we want to split this input string.
            '- split string based on spaces.
            words = str.Split(New Char() {" "c})
            Dim degree As String
            'check one word or not
            If words.Length > 3 Then
                For j As Integer = 0 To words.Length - 1
                    '1 characher
                    Dim index As Integer = Array.IndexOf(words, words(j))
                    Dim length As Integer = words.Length - 1
                    'last word in str                    
                    'first word
                    If index = 0 Then
                        If Not Len(words(j)) = 1 Then
                            If Not arrString.Contains(j) Then
                                arrString.Add(checkDegree(words(j), "[GelarDepan]").ToString)
                            End If
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If
                    ElseIf index = 1 Then
                        If length > 1 Then
                            If Not Len(words(j)) = 1 Then
                                If Not arrString.Contains(j) Then
                                    arrString.Add(checkDegree(words(j), "[GelarDepan]").ToString)
                                End If
                            Else
                                If Not arrString.Contains(j) Then
                                    arrString.Add(words(j))
                                End If
                            End If
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If
                        'last 2 word in str
                    ElseIf index = length - 1 Then
                        If length > 2 Then
                            ' word has 1 character
                            If Len(words(j)) = 1 Then
                                If words(j).ToString.ToUpper() = "S" Or words(j).ToString.ToUpper() = "M" Or words(j).ToString.ToUpper() = "A" Or words(j).ToString.ToUpper() = "B" Or words(j).ToString.ToUpper() = "G" Then
                                    'Console.WriteLine(words(j))
                                    'check degree in database
                                    'A ST
                                    degree = words(j).ToString + " " + words(j + 1).ToString
                                    Dim str1 As String = checkDegree(degree, "[GelarSM]").ToString
                                    'NOT FOUND DATA
                                    If Not str1 = "" Then
                                        Dim str2 As String = checkDegree(words(j + 1), "[GelarBelakang]").ToString
                                        'NOT FOUND DATA
                                        If Not str2 = "" Then
                                            If Not arrString.Contains(j) Then
                                                arrString.Add(degree)
                                            Else
                                                arrString.Add(words(j).ToString)
                                            End If
                                        End If
                                    Else
                                        arrString.Add(str1)
                                    End If
                                    'done selections
                                    GoTo NextFinalOutput
                                Else
                                    If Not arrString.Contains(j) Then
                                        arrString.Add(words(j))
                                    End If
                                End If
                            Else
                                'more 1 characther
                                If words(j).ToString.ToUpper() = "AMD" Or words(j).ToString.ToUpper() = "STR" Or words(j).ToString.ToUpper() = "SP" Or words(j).ToString.ToUpper() = "TR" Or words(j).ToString.ToUpper() = "SI" Or words(j).ToString.ToUpper() = "TH" Or words(j).ToString.ToUpper() = "MD" Or words(j).ToString.ToUpper() = "AP" Then
                                    'check degree in database
                                    degree = words(j).ToString + " " + words(j + 1).ToString
                                    'NOT FOUND DATA
                                    Dim str2 As String = checkDegree(degree, "[GelarSM]").ToString
                                    If Not str2 = "" Then
                                        If Not arrString.Contains(j) Then
                                            arrString.Add(checkDegree(words(j), "[GelarBelakang]").ToString)
                                            arrString.Add(checkDegree(words(j + 1), "[GelarBelakang]").ToString)
                                        End If
                                    Else
                                        If Not arrString.Contains(j) Then
                                            arrString.Add(str2)
                                        End If
                                        'done selections
                                        GoTo NextFinalOutput
                                    End If
                                Else
                                    If Not arrString.Contains(j) Then
                                        arrString.Add(checkDegree(words(j), "[GelarBelakang]").ToString)
                                    End If
                                End If
                            End If
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If
                        'kata 3 terakhir
                    ElseIf index = length - 2 Then
                        If length > 2 Then
                            If words(j).ToString.ToUpper() = "S" Or words(j).ToString.ToUpper() = "M" Or words(j).ToString.ToUpper() = "A" Or words(j).ToString.ToUpper() = "AM" Or words(j).ToString.ToUpper() = "AM" Then
                                'Console.WriteLine(words(length - 1).ToString)
                                'inisial index j + 1  
                                If Not Len(words(length - 1)) = 1 Then
                                    'check degree in database
                                    degree = words(j).ToString + " " + words(j + 1).ToString + " " + words(j + 2).ToString
                                    'not found
                                    If Not arrString.Contains(j) Then
                                        Dim str1 As String = checkDegree(degree, "[GelarSM]").ToString
                                        If Not str1 = "" Then
                                            Dim degree2 As String = words(j).ToString + " " + words(j + 1).ToString
                                            If Not arrString.Contains(j) Then
                                                arrString.Add(checkDegree(degree2, "[GelarSM]").ToString)
                                                arrString.Add(checkDegree(words(j + 2), "[GelarBelakang]").ToString)
                                            End If
                                        Else
                                            arrString.Add(str1)
                                        End If
                                    End If
                                    'Console.WriteLine(words(j))
                                    'done selections
                                    GoTo NextFinalOutput
                                Else
                                    If Not arrString.Contains(j) Then
                                        arrString.Add(words(j))
                                    End If
                                End If
                            Else
                                Console.WriteLine(words(j))
                                If Not arrString.Contains(j) Then
                                    arrString.Add(checkDegree(words(j), "[GelarBelakang]").ToString)
                                End If
                            End If
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If
                        'Single Word in 'Gelar'
CleansingSingleGelar:
                        'word is Not Inisial Gelar
                    ElseIf index = length Then
                        If Not Len(words(j)) = 1 Then
                            If Not arrString.Contains(j) Then
                                arrString.Add(checkDegree(words(j), "[GelarBelakang]").ToString)
                            End If
                            'done selections
                            GoTo NextFinalOutput
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If
                    Else
                        If Not arrString.Contains(j) Then
                            arrString.Add(words(j))
                        End If
                    End If
                Next
                'output final word after cleansing
                GoTo NextFinalOutput
                'str has just 2 and 3 word
            ElseIf words.Length = 2 Or words.Length = 1 Then
                'has just 2 word
                For j As Integer = 0 To words.Length - 1
                    Dim index As Integer = Array.IndexOf(words, words(j))
                    Dim length As Integer = words.Length - 1
                    'first word
                    If index = 0 Then
                        If Not Len(words(j)) = 1 Then
                            If Not arrString.Contains(j) Then
                                arrString.Add(checkDegree(words(j), "[GelarDepan]").ToString)
                            End If
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If
                        'last word
                    ElseIf index = length Then
                        If Not Len(words(1)) = 1 Then
                            If Not arrString.Contains(j) Then
                                arrString.Add(checkDegree(words(j), "[GelarBelakang]").ToString)
                            End If
                            'done selections
                            GoTo NextFinalOutput
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If
                    End If
                Next
                'output final word after cleansing
                GoTo NextFinalOutput
            ElseIf words.Length = 3 Then
                'has just 2 word
                For j As Integer = 0 To words.Length - 1
                    Dim index As Integer = Array.IndexOf(words, words(j))
                    Dim length As Integer = words.Length - 1
                    'first word
                    If index = 0 Then
                        If Not Len(words(j)) = 1 Then
                            If Not arrString.Contains(j) Then
                                arrString.Add(checkDegree(words(j), "[GelarDepan]").ToString)
                            End If
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If

                    ElseIf index = 1 Then
                        If Len(words(j)) = 1 Then
                            If words(j).ToString.ToUpper() = "S" Or words(j).ToString.ToUpper() = "M" Or words(j).ToString.ToUpper() = "A" Or words(j).ToString.ToUpper() = "B" Or words(j).ToString.ToUpper() = "G" Then
                                'check degree in database
                                'A ST
                                degree = words(j).ToString + " " + words(j + 1).ToString
                                Dim str1 As String = checkDegree(degree, "[GelarSM]").ToString
                                'NOT FOUND DATA
                                If Not str1 = "" Then
                                    Dim str2 As String = checkDegree(words(j + 1), "[GelarBelakang]").ToString
                                    'NOT FOUND DATA
                                    If Not str2 = "" Then
                                        If Not arrString.Contains(j) Then
                                            arrString.Add(degree)
                                        Else
                                            arrString.Add(words(j).ToString)
                                        End If
                                    End If
                                Else
                                    arrString.Add(str1)
                                End If
                                'done selections
                                GoTo NextFinalOutput
                            Else
                                If Not arrString.Contains(j) Then
                                    arrString.Add(words(j))
                                End If
                            End If
                        Else
                            'more 1 characther
                            If words(j).ToString.ToUpper() = "AMD" Or words(j).ToString.ToUpper() = "STR" Or words(j).ToString.ToUpper() = "SP" Or words(j).ToString.ToUpper() = "TR" Or words(j).ToString.ToUpper() = "SI" Or words(j).ToString.ToUpper() = "TH" Or words(j).ToString.ToUpper() = "MD" Or words(j).ToString.ToUpper() = "AP" Then
                                'check degree in database
                                degree = words(j).ToString + " " + words(j + 1).ToString
                                'NOT FOUND DATA
                                Dim str2 As String = checkDegree(degree, "[GelarSM]").ToString
                                If Not str2 = "" Then
                                    If Not arrString.Contains(j) Then
                                        arrString.Add(checkDegree(words(j), "[GelarBelakang]").ToString)
                                        arrString.Add(checkDegree(words(j + 1), "[GelarBelakang]").ToString)
                                    End If
                                Else
                                    If Not arrString.Contains(j) Then
                                        arrString.Add(str2)
                                    End If
                                    'done selections
                                    GoTo NextFinalOutput
                                End If
                            Else
                                If Not arrString.Contains(j) Then
                                    arrString.Add(checkDegree(words(j), "[GelarBelakang]").ToString)
                                End If
                            End If
                        End If
                        'last word
                    ElseIf index = length Then
                        If Not Len(words(1)) = 1 Then
                            If Not arrString.Contains(j) Then
                                arrString.Add(checkDegree(words(j), "[GelarBelakang]").ToString)
                            End If
                            'done selections
                            GoTo NextFinalOutput
                        Else
                            If Not arrString.Contains(j) Then
                                arrString.Add(words(j))
                            End If
                        End If
                    End If
                Next
                'output final word after cleansing
                GoTo NextFinalOutput
            Else
                FinalWord = str
                GoTo nextFinish
                'Console.WriteLine("Kurang dari 2 kata")
            End If

NextFinalOutput:
            'output final word after cleansing
            FinalWord = String.Join(" ", TryCast(arrString.ToArray(GetType(String)), String()))
            arrString.Clear()

        End If

nextFinish:
        'result string
        Return FinalWord.Trim()
    End Function
    Private company_words = New String() {}
    Function Set_Company_Name(ByRef company_name As String) As Integer
        Dim checktable As New DataTable()
        Dim value_check As Integer
        'inisialisasi value
        value_check = 0
        If String.IsNullOrWhiteSpace(company_name) Then
            value_check = 1
        Else
            'Remove few word in string
            company_words = company_name.Split(New Char() {" "c})
            'foun word who same in string
            For j As Integer = 0 To company_words.Length - 1
                SQLConnection = New SqlConnection(SQLConnectionString)
                cmd = New SqlCommand("SELECT TOP 1 [NO],[TAKE_OUT_COMPANY] FROM [DB_MASTER].[dbo].[DB_TAKEOUT_COMPANY_NAME] WHERE [TAKE_OUT_COMPANY] = '" + company_words(j).ToString + "' ", SQLConnection)
                Dim adapter As New SqlDataAdapter(cmd)
                adapter.Fill(checktable)
                'Checking data to any rows in table "Occupation
                If checktable.Rows.Count() > 0 Then
                    value_check += 1
                Else
                    value_check += 0
                End If
            Next
        End If
        Return value_check
    End Function
    Private Sub btn_summary_Click(sender As Object, e As EventArgs) Handles btn_summary.Click
        Dim sm As New Summary
        'set value to summary
        'data 
        If TableTemp.Rows.Count <= 0 Then
            sm.n_baris.Text = 0
            sm.n_kolom.Text = 0
            sm.n_t_records.Text = 0
        Else
            sm.txt_product.Text = cb_product.Text + " (Product)"
            sm.n_baris.Text = Int(TableTemp.Rows.Count).ToString
            sm.n_kolom.Text = Int(TableTemp.Columns.Count - 2).ToString
            sm.n_t_records.Text = Int(TableTemp.Rows.Count).ToString
            sm.n_success.Text = Int(TableFinal.Rows.Count).ToString
            sm.n_takedown.Text = Int(TableTakeDown.Rows.Count).ToString
            sm.n_minute.Text = Int(st_minutes.ToString)
            sm.n_seconds.Text = Strings.Left(Int(st_seconds), 2).ToString

            'Records Reason
            sm.n_Dati.Text = poinDati.ToString
            sm.n_IdCardBirth.Text = poinIdBirth.ToString
            sm.n_mothername.Text = poinMother.ToString
            sm.n_name.Text = poinName.ToString
            sm.n_PlaceBirth.Text = poinPlaceBirth.ToString
            sm.n_occup.Text = poinOccup.ToString
        End If
        sm.ShowDialog()

    End Sub

    Private Function DataTable() As DataTable
        Throw New NotImplementedException
    End Function

    Function RemoveMultiCharacter(ByRef str As String, ByRef add As String) As String
        Dim getStr As String = str
        'Dim result As String
        'Step 1 : cleansing multi symbol
        'Remove multi characther or symbol
        Dim arrChar() As String = {",", ".", "&", "#", "%", "$", "*", "!", "<", ">", "^", "(", ")", "?", "\", "!", "'", "/", "+", "@", "=", "-"}
        'Dim newWord As ArrayList
        For i As Integer = 0 To arrChar.Length - 1
            getStr = Replace(getStr, arrChar(i), add)
        Next i
        'result step 1
        Return getStr
    End Function

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

    Private arrPlace As New ArrayList()
    Private Sub btn_cleansing_loan_Click(sender As Object, e As EventArgs) Handles btn_cleansing_loan.Click
        If load_cif_success.Rows.Count > 0 Then
            'send value to another form
            Try
                Dim fm As New Loan With {.Owner = Me}
                fm.Show()
                fm.MdiParent = Home
            Finally
            End Try
        Else
            MessageBox.Show("Data success is null", "Alert")
        End If
    End Sub

    Private Sub load_cif_DoubleClick(sender As Object, e As EventArgs) Handles load_cif.DoubleClick
        MessageBox.Show("Data can't edit", "Alert")
    End Sub

    Private Sub load_cif_failed_DoubleClick(sender As Object, e As EventArgs) Handles load_cif_failed.DoubleClick
        Try
            Dim fm As New FormEdit With {.Owner = Me}
            Dim rowIndex As Integer = load_cif_failed.CurrentRow.Index
            'send index
            fm.indexRows = rowIndex
            'multifinance code
            fm.txt_multifinance_customer.Text = load_cif_failed.Rows(rowIndex).Cells(79).Value.ToString
            'data nasabah
            fm.txt_name.Text = load_cif_failed.Rows(rowIndex).Cells(5).Value.ToString
            fm.txt_english_name.Text = load_cif_failed.Rows(rowIndex).Cells(6).Value.ToString
            fm.txt_name_id.Text = load_cif_failed.Rows(rowIndex).Cells(7).Value.ToString
            fm.txt_id_card.Text = load_cif_failed.Rows(rowIndex).Cells(14).Value.ToString
            fm.txt_date_birth.Text = load_cif_failed.Rows(rowIndex).Cells(23).Value.ToString
            fm.txt_gender.Text = load_cif_failed.Rows(rowIndex).Cells(28).Value.ToString
            fm.txt_mother_name.Text = load_cif_failed.Rows(rowIndex).Cells(33).Value.ToString
            fm.txt_place_birth.Text = load_cif_failed.Rows(rowIndex).Cells(35).Value.ToString
            fm.txt_occupation.Text = load_cif_failed.Rows(rowIndex).Cells(39).Value.ToString
            fm.txt_source_of_fund.Text = load_cif_failed.Rows(rowIndex).Cells(74).Value.ToString
            fm.txt_tax_number.Text = load_cif_failed.Rows(rowIndex).Cells(34).Value.ToString
            'data wilayah home
            fm.txt_dati_home.Text = load_cif_failed.Rows(rowIndex).Cells(47).Value.ToString
            fm.txt_post_code_home.Text = load_cif_failed.Rows(rowIndex).Cells(51).Value.ToString
            fm.txt_village_home.Text = load_cif_failed.Rows(rowIndex).Cells(52).Value.ToString
            fm.txt_sub_district_home.Text = load_cif_failed.Rows(rowIndex).Cells(53).Value.ToString

            'data wilaya office
            fm.txt_dati_office.Text = load_cif_failed.Rows(rowIndex).Cells(56).Value.ToString
            fm.txt_post_code_office.Text = load_cif_failed.Rows(rowIndex).Cells(60).Value.ToString
            fm.txt_village_office.Text = load_cif_failed.Rows(rowIndex).Cells(61).Value.ToString
            fm.txt_sub_district_office.Text = load_cif_failed.Rows(rowIndex).Cells(62).Value.ToString
            fm.txt_reason.Text = load_cif_failed.Rows(rowIndex).Cells(88).Value.ToString
            fm.ShowDialog()

        Catch
        End Try


    End Sub

    Private Sub load_cif_success_DoubleClick(sender As Object, e As EventArgs) Handles load_cif_success.DoubleClick
        Try
            Dim fm As New FormEdit With {.Owner = Me}
            Dim rowIndex As Integer = load_cif_success.CurrentRow.Index
            'send index
            fm.indexRows = rowIndex
            'multifinance code
            fm.txt_multifinance_customer.Text = load_cif_success.Rows(rowIndex).Cells(79).Value.ToString
            'data nasabah
            fm.txt_name.Text = load_cif_success.Rows(rowIndex).Cells(5).Value.ToString
            fm.txt_english_name.Text = load_cif_success.Rows(rowIndex).Cells(6).Value.ToString
            fm.txt_name_id.Text = load_cif_success.Rows(rowIndex).Cells(7).Value.ToString
            fm.txt_id_card.Text = load_cif_success.Rows(rowIndex).Cells(14).Value.ToString
            fm.txt_date_birth.Text = load_cif_success.Rows(rowIndex).Cells(23).Value.ToString
            fm.txt_gender.Text = load_cif_success.Rows(rowIndex).Cells(28).Value.ToString
            fm.txt_mother_name.Text = load_cif_success.Rows(rowIndex).Cells(33).Value.ToString
            fm.txt_place_birth.Text = load_cif_success.Rows(rowIndex).Cells(35).Value.ToString
            fm.txt_occupation.Text = load_cif_success.Rows(rowIndex).Cells(39).Value.ToString
            fm.txt_source_of_fund.Text = load_cif_success.Rows(rowIndex).Cells(74).Value.ToString
            fm.txt_tax_number.Text = load_cif_success.Rows(rowIndex).Cells(34).Value.ToString
            'data wilayah home
            fm.txt_dati_home.Text = load_cif_success.Rows(rowIndex).Cells(47).Value.ToString
            fm.txt_post_code_home.Text = load_cif_success.Rows(rowIndex).Cells(51).Value.ToString
            fm.txt_village_home.Text = load_cif_success.Rows(rowIndex).Cells(52).Value.ToString
            fm.txt_sub_district_home.Text = load_cif_success.Rows(rowIndex).Cells(53).Value.ToString

            'data wilaya office
            fm.txt_dati_office.Text = load_cif_success.Rows(rowIndex).Cells(56).Value.ToString
            fm.txt_post_code_office.Text = load_cif_success.Rows(rowIndex).Cells(60).Value.ToString
            fm.txt_village_office.Text = load_cif_success.Rows(rowIndex).Cells(61).Value.ToString
            fm.txt_sub_district_office.Text = load_cif_success.Rows(rowIndex).Cells(62).Value.ToString

            fm.ShowDialog()
        Catch
        End Try

    End Sub

    Private Sub cb_export_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_export.SelectedIndexChanged
        Try
            If Not cb_export.SelectedIndex = 0 Then
                If table.Rows.Count <= 0 Then
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
                    If tabcontrol1.SelectedIndex.ToString = 0 Then
                        MessageBox.Show("Data can't export", "Alert")
                    ElseIf tabcontrol1.SelectedIndex.ToString = 1 Then
                        If cb_export.SelectedIndex = 1 Then
                            'Data CIF Success
                            filename1 = "D:\OUTPUT\" + nameProducts + "_CIF_Final_" + strdate + extention
                            Call ExportDataToExcel(TableFinal, filename1)
                        Else
                            filename1 = "D:\OUTPUT\" + nameProducts + "_CIF_Final_" + strdate + extention
                            Call DataTableToExportFile(TableFinal, filename1)
                        End If
                        MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                    ElseIf tabcontrol1.SelectedIndex.ToString = 2 Then
                        'Data CIF Takedown
                        If cb_export.SelectedIndex = 1 Then
                            'Data CIF Success
                            filename1 = "D:\OUTPUT\" + nameProducts + "_CIF_Takedown_" + strdate + extention
                            Call ExportDataToExcel(TableTakeDown, filename1)
                        Else
                            filename1 = "D:\OUTPUT\" + nameProducts + "_CIF_Takedown_" + strdate + extention
                            Call DataTableToExportFile(TableTakeDown, filename1)
                        End If
                        MessageBox.Show("Export " + filename1.ToString + " created.", "Alert")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub ExportDataToExcel(ByRef gettable As DataTable, ByRef filename As String)
        Using out = New XLWorkbook()
            'out.Worksheets.Add(gettable)
            out.Worksheets.Add(gettable, "Sheet1")
            out.SaveAs(filename)
        End Using
    End Sub
    Private Sub btn_import_collect_Click(sender As Object, e As EventArgs)
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
                Dim myTableName = conn.GetSchema("Tables").Rows(0)("TABLE_NAME")
                Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
                Dim da As New OleDbDataAdapter(sqlquery, conn)
                da.Fill(ds)
                dt_collec = ds.Tables(0)
                'load_cif.DataSource = dt_collec
                conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'event load data
            Application.DoEvents()
        End Try
    End Sub
End Class

