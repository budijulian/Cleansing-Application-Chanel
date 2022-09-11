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

Public Class Checking
    Public SQL As New ConnectionSQL
    Private conn As OleDbConnection 'koneksi odb
    'Public dt As OleDbDataAdapter
    Private dts As DataSet ' membuat table datasheet virtual
    Private excel As String

    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    'create datatable with column and new rows
    Dim SourceDatabase As String
    Private cmd As New Data.SqlClient.SqlCommand
    Private ad As New Data.SqlClient.SqlDataAdapter
    Public table As New DataTable
    'a property
    Public Property dataFound As New DataTable
    Public Property notDataFound As New DataTable

    Private Sub btn_import_Click(sender As Object, e As EventArgs) Handles btn_import.Click
        Try
            'IMPORT DATA FILE EXCEL TO DATAGRIDVIEW

            'load_cif_check.Columns.Clear()
            'clear all data datagridview

            If load_cif_found.Rows.Count > 0 Or load_cif_not_found.Rows.Count > 0 Then
                'clear all data datagridview
                load_cif_found.Columns.Clear()
                load_cif_not_found.Columns.Clear()
                dataFound.Columns.Clear()
                notDataFound.Columns.Clear()

            End If

            'load_cif_found.Columns.Clear()
            'load_cif_not_found.Columns.Clear()
            'dataFound.Columns.Clear()
            'notDataFound.Columns.Clear()
            'T_check1.Text = 0
            Dim ds As New DataSet()

            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            OpenFileDialog1.Filter = "All Files (*.*)|*.*|Excel Files (*.xlsx)|*.xlsx|Xls Files (*.xls)|*.xls"
            If OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
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
                load_cif_check.DataSource = table
                conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            ' Show Count Rows in Datagridview
            C_rows1.Text = load_cif_check.RowCount - 1
            C_Cells1.Text = load_cif_check.Columns.Count - 1
            'Disabled Button before process
            btn_checking.Enabled = True
            btn_export_cif_found.Enabled = True
            btn_export_cif_not_found.Enabled = True

        End Try
    End Sub

    Private Sub InserMultiData()
        Try
            '------Finished Declare variabel--------
            Dim query As String = "INSERT INTO [DB_MASTER].[dbo].[DB_CIF_TEMP] ([Decision],[WLF_Check],[CIF],[CIF_Type],[Customer_Category],[Name],[English_Name],[Name_on_ID],[CIF_Type2],[Customer_Type],[CIF_Status],[Customer_Group],[Relationship_with_Bank],[ID_Type],[ID_Card],[ID_Issuer],[ID_Issuance_Date],[ID_Expiry_Date],[Nationality], [Country_of_Domicile],[Resident_YN],[Foreign_ID],[DM_Correspondent_Type],[Date_of_Birth],[Tax_Restitution],[Phone_Number],[FAX_No],[Customer_Email],[Gender],[Marital_Status_Code],[Lastest_Education],[Religion],[Nationality2],[Mother_Name],[Tax_NPWP_No],[Customer_Birth_Place],[Kab_Name],[Company_Business_Name],[Work_Period],[Occupation],[Sektor_Industri],[Other_Occupation],[Position],[Other_Position],[Guarantor_Name],[Guarantor_Occupation],[Negara_Rumah],[ID_Card_Dati_2],[Phone_Number2],[ID_Card_RT_Neighbourhood],[ID_Card_RW_Hamlet],[ID_Card_Postal_Code],[ID_Card_Kelurahan_Village],[ID_Card_Kecamatan_Subdistrict],[ID_Card_Address_Street_Name_House],[Negara_Kantor],[Company_Shipping_Dati2_City_Regency],[Company_Phone_Number],[Company_Shipping_RT_Neighbourhood],[Company_Shipping_RW_Hamlet],[Company_Shipping_Postal_Code],[Company_Shipping_Kelurahan_Village],[Company_Shipping_Kecamatan_Subdistrict],[Company_Shipping_Address],[Negara_Terkini],[Lokasi_Dati_Terkini],[No_Telpon_Terkini],[RT_Terkini],[RW_Terkini],[Kode_pos_Terkini],[Kelurahan_Terkini], [Kecamatan_Terkini], [Alamat_Terkini],[Income],[Source_of_Fund],[Spouse_ID_Number],[Spouse_Name], [Spouse_Date_of_Birth],[Spouse_Place_of_Birth],[Multifinance_key_no],[Purpose_of_Account_opening],[Ket_Pembukaan_Rekening],[Purpose_of_Deposit_Transfer],[Ket_Untuk_Setoran],[Purpose_of_FX_Transaction],[Ket_Untuk_Penukaran_Valas],[Estimated_Service_Product],[Product_Service_provided_by_Bank]) VALUES (@Decision,@WLF_Check,@CIF,@CIF_Type,@Customer_Category,@Name,@English_Name,@Name_on_ID,@CIF_Type2,@Customer_Type,@CIF_Status,@Customer_Group,@Relationship_with_Bank,@ID_Type,@ID_Card,@ID_Issuer,@ID_Issuance_Date,@ID_Expiry_Date,@Nationality, @Country_of_Domicile,@Resident_YN,@Foreign_ID,@DM_Correspondent_Type,@Date_of_Birth,@Tax_Restitution,@Phone_Number,@FAX_No,@Customer_Email,@Gender,@Marital_Status_Code,@Lastest_Education,@Religion,@Nationality2,@Mother_Name2,@Tax_NPWP_No,@Customer_Birth_Place,@Kab_Name,@Company_Business_Name,@Work_Period,@Occupation,@Sektor_Industri,@Other_Occupation,@Position,@Other_Position,@Guarantor_Name,@Guarantor_Occupation,@Negara_Rumah,@ID_Card_Dati_2,@Phone_Number2,@ID_Card_RT_Neighbourhood,@ID_Card_RW_Hamlet,@ID_Card_Postal_Code,@ID_Card_Kelurahan_Village,@ID_Card_Kecamatan_Subdistrict,@ID_Card_Address_Street_Name_House,@Negara_Kantor,@Company_Shipping_Dati2_City_Regency,@Company_Phone_Number,@Company_Shipping_RT_Neighbourhood,@Company_Shipping_RW_Hamlet,@Company_Shipping_Postal_Code,@Company_Shipping_Kelurahan_Village,@Company_Shipping_Kecamatan_Subdistrict,@Company_Shipping_Address,@Negara_Terkini,@Lokasi_Dati_Terkini,@No_Telpon_Terkini,@RT_Terkini,@RW_Terkini,@Kode_pos_Terkini,@Kelurahan_Terkini,@Kecamatan_Terkini, @Alamat_Terkini,@Income,@Source_of_Fund,@Spouse_ID_Number,@Spouse_Name, @Spouse_Date_of_Birth,@Spouse_Place_of_Birth,@Multifinance_key_no,@Purpose_of_Account_opening,@Ket_Pembukaan_Rekening,@Purpose_of_Deposit_Transfer,@Ket_Untuk_Setoran,@Purpose_of_FX_Transaction,@Ket_Untuk_Penukaran_Valas,@Estimated_Service_Product,@Product_Service_provided_by_Bank)"
            'CALL CLASS CONNECTION
            SQL.ConnSQLDefault()

            cmd = New SqlCommand(query, SQL.SQLConnection)
            cmd.CommandType = CommandType.Text
            ad.InsertCommand = cmd
            'SQLConnection.Open()
            Dim index As Integer
            cmd.Parameters.AddWithValue("@Decision", "")
            cmd.Parameters.AddWithValue("@WLF_Check", "")
            cmd.Parameters.AddWithValue("@CIF", "")
            cmd.Parameters.AddWithValue("@CIF_Type", "")
            cmd.Parameters.AddWithValue("@Customer_Category", "")
            cmd.Parameters.AddWithValue("@Name", "")
            cmd.Parameters.AddWithValue("@English_Name", "")
            cmd.Parameters.AddWithValue("@Name_on_ID", "")
            cmd.Parameters.AddWithValue("@CIF_Type2", "")
            cmd.Parameters.AddWithValue("@Customer_Type", "")
            cmd.Parameters.AddWithValue("@CIF_Status", "")
            cmd.Parameters.AddWithValue("@Customer_Group", "")
            cmd.Parameters.AddWithValue("@Relationship_with_Bank", "")
            cmd.Parameters.AddWithValue("@ID_Type", "")
            cmd.Parameters.AddWithValue("@ID_Card", "")
            cmd.Parameters.AddWithValue("@ID_Issuer", "")
            cmd.Parameters.AddWithValue("@ID_Issuance_Date", "")
            cmd.Parameters.AddWithValue("@ID_Expiry_Date", "")
            cmd.Parameters.AddWithValue("@Nationality", "")
            cmd.Parameters.AddWithValue("@Country_of_Domicile", "")
            cmd.Parameters.AddWithValue("@Resident_YN", "")
            cmd.Parameters.AddWithValue("@Foreign_ID", "")
            cmd.Parameters.AddWithValue("@DM_Correspondent_Type", "")
            cmd.Parameters.AddWithValue("@Date_of_Birth", "")
            cmd.Parameters.AddWithValue("@Tax_Restitution", "")
            cmd.Parameters.AddWithValue("@Phone_Number", "")
            cmd.Parameters.AddWithValue("@FAX_No", "")
            cmd.Parameters.AddWithValue("@Customer_Email", "")
            cmd.Parameters.AddWithValue("@Gender", "")
            cmd.Parameters.AddWithValue("@Marital_Status_Code", "")
            cmd.Parameters.AddWithValue("@Lastest_Education", "")
            cmd.Parameters.AddWithValue("@Religion", "")
            cmd.Parameters.AddWithValue("@Nationality2", "")
            cmd.Parameters.AddWithValue("@Mother_Name2", "")
            cmd.Parameters.AddWithValue("@Tax_NPWP_No", "")
            cmd.Parameters.AddWithValue("@Customer_Birth_Place", "")
            cmd.Parameters.AddWithValue("@Kab_Name", "")
            cmd.Parameters.AddWithValue("@Company_Business_Name", "")
            cmd.Parameters.AddWithValue("@Work_Period", "")
            cmd.Parameters.AddWithValue("@Occupation", "")
            cmd.Parameters.AddWithValue("@Sektor_Industri", "")
            cmd.Parameters.AddWithValue("@Other_Occupation", "")
            cmd.Parameters.AddWithValue("@Position", "")
            cmd.Parameters.AddWithValue("@Other_Position", "")
            cmd.Parameters.AddWithValue("@Guarantor_Name", "")
            cmd.Parameters.AddWithValue("@Guarantor_Occupation", "")
            cmd.Parameters.AddWithValue("@Negara_Rumah", "")
            cmd.Parameters.AddWithValue("@ID_Card_Dati_2", "")
            cmd.Parameters.AddWithValue("@Phone_Number2", "")
            cmd.Parameters.AddWithValue("@ID_Card_RT_Neighbourhood", "")
            cmd.Parameters.AddWithValue("@ID_Card_RW_Hamlet", "")
            cmd.Parameters.AddWithValue("@ID_Card_Postal_Code", "")
            cmd.Parameters.AddWithValue("@ID_Card_Kelurahan_Village", "")
            cmd.Parameters.AddWithValue("@ID_Card_Kecamatan_Subdistrict", "")
            cmd.Parameters.AddWithValue("@ID_Card_Address_Street_Name_House", "")
            cmd.Parameters.AddWithValue("@Negara_Kantor", "")
            cmd.Parameters.AddWithValue("@Company_Shipping_Dati2_City_Regency", "")
            cmd.Parameters.AddWithValue("@Company_Phone_Number", "")
            cmd.Parameters.AddWithValue("@Company_Shipping_RT_Neighbourhood", "")
            cmd.Parameters.AddWithValue("@Company_Shipping_RW_Hamlet", "")
            cmd.Parameters.AddWithValue("@Company_Shipping_Postal_Code", "")
            cmd.Parameters.AddWithValue("@Company_Shipping_Kelurahan_Village", "")
            cmd.Parameters.AddWithValue("@Company_Shipping_Kecamatan_Subdistrict", "")
            cmd.Parameters.AddWithValue("@Company_Shipping_Address", "")
            cmd.Parameters.AddWithValue("@Negara_Terkini", "")
            cmd.Parameters.AddWithValue("@Lokasi_Dati_Terkini", "")
            cmd.Parameters.AddWithValue("@No_Telpon_Terkini", "")
            cmd.Parameters.AddWithValue("@RT_Terkini", "")
            cmd.Parameters.AddWithValue("@RW_Terkini", "")
            cmd.Parameters.AddWithValue("@Kode_pos_Terkini", "")
            cmd.Parameters.AddWithValue("@Kelurahan_Terkini", "")
            cmd.Parameters.AddWithValue("@Kecamatan_Terkini", "")
            cmd.Parameters.AddWithValue("@Alamat_Terkini", "")
            cmd.Parameters.AddWithValue("@Income", "")
            cmd.Parameters.AddWithValue("@Source_of_Fund", "")
            cmd.Parameters.AddWithValue("@Spouse_ID_Number", "")
            cmd.Parameters.AddWithValue("@Spouse_Name", "")
            cmd.Parameters.AddWithValue("@Spouse_Date_of_Birth", "")
            cmd.Parameters.AddWithValue("@Spouse_Place_of_Birth", "")
            cmd.Parameters.AddWithValue("@Multifinance_key_no", "")
            cmd.Parameters.AddWithValue("@Purpose_of_Account_opening", "")
            cmd.Parameters.AddWithValue("@Ket_Pembukaan_Rekening", "")
            cmd.Parameters.AddWithValue("@Purpose_of_Deposit_Transfer", "")
            cmd.Parameters.AddWithValue("@Ket_Untuk_Setoran", "")
            cmd.Parameters.AddWithValue("@Purpose_of_FX_Transaction", "")
            cmd.Parameters.AddWithValue("@Ket_Untuk_Penukaran_Valas", "")
            cmd.Parameters.AddWithValue("@Estimated_Service_Product", "")
            cmd.Parameters.AddWithValue("@Product_Service_provided_by_Bank", "")
            For index = 0 To table.Rows.Count - 1
                cmd.Parameters("@Decision").Value = table.Rows(index).Item(0).ToString.ToUpper().Trim
                cmd.Parameters("@WLF_Check").Value = table.Rows(index).Item(1).ToString.ToUpper().Trim
                cmd.Parameters("@CIF").Value = table.Rows(index).Item(2).ToString.ToUpper().Trim
                cmd.Parameters("@CIF_Type").Value = table.Rows(index).Item(3).ToString.ToUpper().Trim
                cmd.Parameters("@Customer_Category").Value = table.Rows(index).Item(4).ToString.ToUpper().Trim
                cmd.Parameters("@Name").Value = table.Rows(index).Item(5).ToString.ToUpper().Trim
                cmd.Parameters("@English_Name").Value = table.Rows(index).Item(6).ToString.ToUpper().Trim
                cmd.Parameters("@Name_on_ID").Value = table.Rows(index).Item(7).ToString.ToUpper().Trim
                cmd.Parameters("@CIF_Type2").Value = table.Rows(index).Item(8).ToString.ToUpper().Trim
                cmd.Parameters("@Customer_Type").Value = table.Rows(index).Item(9).ToString.ToUpper().Trim
                cmd.Parameters("@CIF_Status").Value = table.Rows(index).Item(10).ToString.ToUpper().Trim
                cmd.Parameters("@Customer_Group").Value = table.Rows(index).Item(11).ToString.ToUpper().Trim
                cmd.Parameters("@Relationship_with_Bank").Value = table.Rows(index).Item(12).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Type").Value = table.Rows(index).Item(13).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Card").Value = table.Rows(index).Item(14).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Issuer").Value = table.Rows(index).Item(15).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Issuance_Date").Value = table.Rows(index).Item(16).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Expiry_Date").Value = table.Rows(index).Item(17).ToString.ToUpper().Trim
                cmd.Parameters("@Nationality").Value = table.Rows(index).Item(18).ToString.ToUpper().Trim
                cmd.Parameters("@Country_of_Domicile").Value = table.Rows(index).Item(19).ToString.ToUpper().Trim
                cmd.Parameters("@Resident_YN").Value = table.Rows(index).Item(20).ToString.ToUpper().Trim
                cmd.Parameters("@Foreign_ID").Value = table.Rows(index).Item(21).ToString.ToUpper().Trim
                cmd.Parameters("@DM_Correspondent_Type").Value = table.Rows(index).Item(22).ToString.ToUpper().Trim
                cmd.Parameters("@Date_of_Birth").Value = table.Rows(index).Item(23).ToString.ToUpper().Trim
                cmd.Parameters("@Tax_Restitution").Value = table.Rows(index).Item(24).ToString.ToUpper().Trim
                cmd.Parameters("@Phone_Number").Value = table.Rows(index).Item(25).ToString.ToUpper().Trim
                cmd.Parameters("@FAX_No").Value = table.Rows(index).Item(26).ToString.ToUpper().Trim
                cmd.Parameters("@Customer_Email").Value = table.Rows(index).Item(27).ToString.ToUpper().Trim
                cmd.Parameters("@Gender").Value = table.Rows(index).Item(28).ToString.ToUpper().Trim
                cmd.Parameters("@Marital_Status_Code").Value = table.Rows(index).Item(29).ToString.ToUpper().Trim
                cmd.Parameters("@Lastest_Education").Value = table.Rows(index).Item(30).ToString.ToUpper().Trim
                cmd.Parameters("@Religion").Value = table.Rows(index).Item(31).ToString.ToUpper().Trim
                cmd.Parameters("@Nationality2").Value = table.Rows(index).Item(32).ToString.ToUpper().Trim
                cmd.Parameters("@Mother_Name2").Value = table.Rows(index).Item(33).ToString.ToUpper().Trim
                cmd.Parameters("@Tax_NPWP_No").Value = table.Rows(index).Item(34).ToString.ToUpper().Trim
                cmd.Parameters("@Customer_Birth_Place").Value = table.Rows(index).Item(35).ToString.ToUpper().Trim
                cmd.Parameters("@Kab_Name").Value = table.Rows(index).Item(36).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Business_Name").Value = table.Rows(index).Item(37).ToString.ToUpper().Trim
                cmd.Parameters("@Work_Period").Value = table.Rows(index).Item(38).ToString.ToUpper().Trim
                cmd.Parameters("@Occupation").Value = table.Rows(index).Item(39).ToString.ToUpper().Trim
                cmd.Parameters("@Sektor_Industri").Value = table.Rows(index).Item(40).ToString.ToUpper().Trim
                cmd.Parameters("@Other_Occupation").Value = table.Rows(index).Item(41).ToString.ToUpper().Trim
                cmd.Parameters("@Position").Value = table.Rows(index).Item(42).ToString.ToUpper().Trim
                cmd.Parameters("@Other_Position").Value = table.Rows(index).Item(43).ToString.ToUpper().Trim
                cmd.Parameters("@Guarantor_Name").Value = table.Rows(index).Item(44).ToString.ToUpper().Trim
                cmd.Parameters("@Guarantor_Occupation").Value = table.Rows(index).Item(45).ToString.ToUpper().Trim
                cmd.Parameters("@Negara_Rumah").Value = table.Rows(index).Item(46).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Card_Dati_2").Value = table.Rows(index).Item(47).ToString.ToUpper().Trim
                cmd.Parameters("@Phone_Number2").Value = table.Rows(index).Item(48).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Card_RT_Neighbourhood").Value = table.Rows(index).Item(49).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Card_RW_Hamlet").Value = table.Rows(index).Item(50).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Card_Postal_Code").Value = table.Rows(index).Item(51).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Card_Kelurahan_Village").Value = table.Rows(index).Item(52).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Card_Kecamatan_Subdistrict").Value = table.Rows(index).Item(53).ToString.ToUpper().Trim
                cmd.Parameters("@ID_Card_Address_Street_Name_House").Value = table.Rows(index).Item(54).ToString.ToUpper().Trim
                cmd.Parameters("@Negara_Kantor").Value = table.Rows(index).Item(55).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Shipping_Dati2_City_Regency").Value = table.Rows(index).Item(56).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Phone_Number").Value = table.Rows(index).Item(57).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Shipping_RT_Neighbourhood").Value = table.Rows(index).Item(58).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Shipping_RW_Hamlet").Value = table.Rows(index).Item(59).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Shipping_Postal_Code").Value = table.Rows(index).Item(60).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Shipping_Kelurahan_Village").Value = table.Rows(index).Item(61).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Shipping_Kecamatan_Subdistrict").Value = table.Rows(index).Item(62).ToString.ToUpper().Trim
                cmd.Parameters("@Company_Shipping_Address").Value = table.Rows(index).Item(63).ToString.ToUpper().Trim
                cmd.Parameters("@Negara_Terkini").Value = table.Rows(index).Item(64).ToString.ToUpper().Trim
                cmd.Parameters("@Lokasi_Dati_Terkini").Value = table.Rows(index).Item(65).ToString.ToUpper().Trim
                cmd.Parameters("@No_Telpon_Terkini").Value = table.Rows(index).Item(66).ToString.ToUpper().Trim
                cmd.Parameters("@RT_Terkini").Value = table.Rows(index).Item(67).ToString.ToUpper().Trim
                cmd.Parameters("@RW_Terkini").Value = table.Rows(index).Item(68).ToString.ToUpper().Trim
                cmd.Parameters("@Kode_pos_Terkini").Value = table.Rows(index).Item(69).ToString.ToUpper().Trim
                cmd.Parameters("@Kelurahan_Terkini").Value = table.Rows(index).Item(70).ToString.ToUpper().Trim
                cmd.Parameters("@Kecamatan_Terkini").Value = table.Rows(index).Item(71).ToString.ToUpper().Trim
                cmd.Parameters("@Alamat_Terkini").Value = table.Rows(index).Item(72).ToString.ToUpper().Trim
                cmd.Parameters("@Income").Value = table.Rows(index).Item(73).ToString.ToUpper().Trim
                cmd.Parameters("@Source_of_Fund").Value = table.Rows(index).Item(74).ToString.ToUpper().Trim
                cmd.Parameters("@Spouse_ID_Number").Value = table.Rows(index).Item(75).ToString.ToUpper().Trim
                cmd.Parameters("@Spouse_Name").Value = table.Rows(index).Item(76).ToString.ToUpper().Trim
                cmd.Parameters("@Spouse_Date_of_Birth").Value = table.Rows(index).Item(77).ToString.ToUpper().Trim
                cmd.Parameters("@Spouse_Place_of_Birth").Value = table.Rows(index).Item(78).ToString.ToUpper().Trim
                cmd.Parameters("@Multifinance_key_no").Value = table.Rows(index).Item(79).ToString.ToUpper().Trim
                cmd.Parameters("@Purpose_of_Account_opening").Value = table.Rows(index).Item(80).ToString.ToUpper().Trim
                cmd.Parameters("@Ket_Pembukaan_Rekening").Value = table.Rows(index).Item(81).ToString.ToUpper().Trim
                cmd.Parameters("@Purpose_of_Deposit_Transfer").Value = table.Rows(index).Item(82).ToString.ToUpper().Trim
                cmd.Parameters("@Ket_Untuk_Setoran").Value = table.Rows(index).Item(83).ToString.ToUpper().Trim
                cmd.Parameters("@Purpose_of_FX_Transaction").Value = table.Rows(index).Item(84).ToString.ToUpper().Trim
                cmd.Parameters("@Ket_Untuk_Penukaran_Valas").Value = table.Rows(index).Item(85).ToString.ToUpper().Trim
                cmd.Parameters("@Estimated_Service_Product").Value = table.Rows(index).Item(86).ToString.ToUpper().Trim
                cmd.Parameters("@Product_Service_provided_by_Bank").Value = table.Rows(index).Item(87).ToString.ToUpper().Trim

                cmd.ExecuteNonQuery()
            Next

            'MsgBox("Successfuly saved!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            SQL.SQLConnection.Close()
        End Try
    End Sub

    Private Sub TruncateTableCIF()
        Try
            'CALL CLASS CONNECTION
            SQL.ConnSQLDefault()
            cmd.Connection = SQL.SQLConnection
            cmd.CommandText = "DELETE FROM [DB_MASTER].[dbo].[DB_CIF_TEMP]"
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'close connection
            SQL.SQLConnection.Close()
        End Try
    End Sub

    Private Function splitAndCheckingData(ByRef query As String, ByRef SQLConnection As SqlConnection)
        Dim result As DataTable = Nothing
        'CALL CLASS CONNECTION
        SQL.ConnSQLDefault()
        cmd.Connection = SQL.SQLConnection
        cmd.CommandText = query
        ad = New SqlDataAdapter(cmd)
        Dim data As New DataTable
        ad.Fill(data)
        If data.Rows.Count() > 0 Then
            result = data
        End If
        'close connection
        SQL.SQLConnection.Close()
        Return result
    End Function

    Private Sub btn_checking_Click(sender As Object, e As EventArgs) Handles btn_checking.Click

        If table.Rows.Count = Nothing Then
            MessageBox.Show("Please, Insert a data.!", "Alert")
        Else
            Application.DoEvents()
            Dim pesan As MsgBoxResult
            Dim setQuery As String
            Dim result As DataTable = Nothing
            Dim temptable As New DataTable()
            Dim timeStart As Date = Convert.ToDateTime(DateTime.Now)
            Dim spanTime As TimeSpan
            Dim st_hours As Double
            Dim st_minutes As Double
            Dim st_seconds As Double
            'copy column to datatable dataFound
            If dataFound.Columns.Count <= 0 Then
                For i = 0 To table.Columns.Count - 1
                    dataFound.Columns.Add(table.Columns(i).ColumnName)
                Next
            End If
            'copy column to datatable notdataFound
            If notDataFound.Columns.Count <= 0 Then
                For j = 0 To table.Columns.Count - 1
                    notDataFound.Columns.Add(table.Columns(j).ColumnName)
                Next
            End If

            pesan = MsgBox("Checking Start?", MsgBoxStyle.YesNo, "Alert")
            If pesan = MsgBoxResult.Yes Then

                Dim numberFound As Integer = 0
                Dim numbernotFound As Integer = 0
                'create new column in other datagridview has copyed
                'If load_cif_not_found.Columns.Count = 0 Then
                '    For Each dgvc As DataGridViewColumn In load_cif_check.Columns
                '        load_cif_not_found.Columns.Add(TryCast(dgvc.Clone(), DataGridViewColumn))
                '    Next
                'End If
                'CALL CLASS CONNECTION
                SQL.ConnSQLDefault()
                'SQLConnection = New SqlConnection(SQLConnectionString)
                cmd = New SqlCommand("SELECT TOP 1 [ID_Card] FROM [DB_MASTER].[dbo].[DB_CIF_TEMP]", SQL.SQLConnection)
                Dim adapter As New SqlDataAdapter(cmd)
                adapter.Fill(temptable)
                If temptable.Rows.Count() > 0 Then
                    Call TruncateTableCIF()
                    'goto next line
                    GoTo CheckingAgain
                Else
                    'MessageBox.Show("tidak ada data")
                    'syntax goto session
CheckingAgain:
                    Call InserMultiData()
                    'get data found from checking database
                    setQuery = "SELECT A.[Decision], A.[WLF_Check], A.[CIF], A.[CIF_Type], A.[Customer_Category], A.[Name], A.[English_Name], A.[Name_on_ID], A.[CIF_Type2], A.[Customer_Type], A.[CIF_Status], A.[Customer_Group], A.[Relationship_with_Bank], A.[ID_Type], A.[ID_Card], A.[ID_Issuer], A.[ID_Issuance_Date], A.[ID_Expiry_Date], A.[Nationality], A.[Country_of_Domicile], A.[Resident_YN], A.[Foreign_ID], A.[DM_Correspondent_Type], A.[Date_of_Birth], A.[Tax_Restitution], B.[Phone_Number], A.[FAX_No], A.[Customer_Email], A.[Gender], A.[Marital_Status_Code], B.[Lastest_Education], A.[Religion], A.[Nationality2], A.[Mother_Name], A.[Tax_NPWP_No], A.[Customer_Birth_Place], A.[Kab_Name], A.[Company_Business_Name], A.[Work_Period], B.[Occupation], A.[Sektor_Industri], A.[Other_Occupation], A.[Position], A.[Other_Position], A.[Guarantor_Name], A.[Guarantor_Occupation], A.[Negara_Rumah], A.[ID_Card_Dati_2], B.[Phone_Number2], A.[ID_Card_RT_Neighbourhood], A.[ID_Card_RW_Hamlet], A.[ID_Card_Postal_Code], A.[ID_Card_Kelurahan_Village], A.[ID_Card_Kecamatan_Subdistrict], A.[ID_Card_Address_Street_Name_House], A.[Negara_Kantor], A.[Company_Shipping_Dati2_City_Regency], B.[Company_Phone_Number], A.[Company_Shipping_RT_Neighbourhood], A.[Company_Shipping_RW_Hamlet], A.[Company_Shipping_Postal_Code], A.[Company_Shipping_Kelurahan_Village], A.[Company_Shipping_Kecamatan_Subdistrict], A.[Company_Shipping_Address], A.[Negara_Terkini], A.[Lokasi_Dati_Terkini], A.[No_Telpon_Terkini], A.[RT_Terkini], A.[RW_Terkini], B.[Kode_pos_Terkini], B.[Kelurahan_Terkini], B.[Kecamatan_Terkini], B.[Alamat_Terkini], B.[Income], B.[Source_of_Fund], A.[Spouse_ID_Number], A.[Spouse_Name], A.[Spouse_Date_of_Birth], A.[Spouse_Place_of_Birth], B.[Multifinance_key_no], A.[Purpose_of_Account_opening], A.[Ket_Pembukaan_Rekening], A.[Purpose_of_Deposit_Transfer], A.[Ket_Untuk_Setoran], A.[Purpose_of_FX_Transaction], A.[Ket_Untuk_Penukaran_Valas], A.[Estimated_Service_Product], A.[Product_Service_provided_by_Bank]  FROM [DB_MASTER].[dbo].[DB_CIF_AKU_HIST] AS A INNER JOIN [DB_MASTER].[dbo].[DB_CIF_TEMP] AS B ON A.ID_Card = B.ID_Card;"

                    dataFound = splitAndCheckingData(setQuery, SQL.SQLConnection)
                    load_cif_found.DataSource = dataFound
                    'get data not found from checking database
                    setQuery = "SELECT A.[Decision], A.[WLF_Check], A.[CIF], A.[CIF_Type], A.[Customer_Category], A.[Name], A.[English_Name], A.[Name_on_ID], A.[CIF_Type2], A.[Customer_Type], A.[CIF_Status], A.[Customer_Group], A.[Relationship_with_Bank], A.[ID_Type], A.[ID_Card], A.[ID_Issuer], A.[ID_Issuance_Date], A.[ID_Expiry_Date], A.[Nationality], A.[Country_of_Domicile], A.[Resident_YN], A.[Foreign_ID], A.[DM_Correspondent_Type], A.[Date_of_Birth], A.[Tax_Restitution], A.[Phone_Number], A.[FAX_No], A.[Customer_Email], A.[Gender], A.[Marital_Status_Code], A.[Lastest_Education], A.[Religion], A.[Nationality2], A.[Mother_Name], A.[Tax_NPWP_No], A.[Customer_Birth_Place], A.[Kab_Name], A.[Company_Business_Name], A.[Work_Period], A.[Occupation], A.[Sektor_Industri], A.[Other_Occupation], A.[Position], A.[Other_Position], A.[Guarantor_Name], A.[Guarantor_Occupation], A.[Negara_Rumah], A.[ID_Card_Dati_2], A.[Phone_Number2], A.[ID_Card_RT_Neighbourhood], A.[ID_Card_RW_Hamlet], A.[ID_Card_Postal_Code], A.[ID_Card_Kelurahan_Village], A.[ID_Card_Kecamatan_Subdistrict], A.[ID_Card_Address_Street_Name_House], A.[Negara_Kantor], A.[Company_Shipping_Dati2_City_Regency], A.[Company_Phone_Number], A.[Company_Shipping_RT_Neighbourhood], A.[Company_Shipping_RW_Hamlet], A.[Company_Shipping_Postal_Code], A.[Company_Shipping_Kelurahan_Village], A.[Company_Shipping_Kecamatan_Subdistrict], A.[Company_Shipping_Address], A.[Negara_Terkini], A.[Lokasi_Dati_Terkini], A.[No_Telpon_Terkini], A.[RT_Terkini], A.[RW_Terkini], A.[Kode_pos_Terkini], A.[Kelurahan_Terkini], A.[Kecamatan_Terkini], A.[Alamat_Terkini], A.[Income], A.[Source_of_Fund], A.[Spouse_ID_Number], A.[Spouse_Name], A.[Spouse_Date_of_Birth], A.[Spouse_Place_of_Birth], A.[Multifinance_key_no], A.[Purpose_of_Account_opening], A.[Ket_Pembukaan_Rekening], A.[Purpose_of_Deposit_Transfer], A.[Ket_Untuk_Setoran], A.[Purpose_of_FX_Transaction], A.[Ket_Untuk_Penukaran_Valas], A.[Estimated_Service_Product], A.[Product_Service_provided_by_Bank] FROM [DB_MASTER].[dbo].[DB_CIF_TEMP] AS A WHERE A.[ID_Card] NOT IN ( SELECT B.[ID_Card] FROM [DB_MASTER].[dbo].[DB_CIF_AKU_HIST] AS B );"
                    notDataFound = splitAndCheckingData(setQuery, SQL.SQLConnection)
                    load_cif_not_found.DataSource = notDataFound
                End If
                'Create Time Duration over 
                Dim timeEnd As Date = Convert.ToDateTime(DateTime.Now)
                spanTime = timeEnd.Subtract(timeStart)
                'inisialisasi variabel span time
                st_hours = spanTime.TotalHours
                st_minutes = spanTime.TotalMinutes
                st_seconds = spanTime.TotalSeconds
                Dim totalSpan As String = " " + Int(st_hours).ToString + " Jam, " + Int(st_minutes).ToString + " Menit," + Int(st_seconds).ToString + " Detik."
                'GiveFeedbackAlert MessageBox
                MessageBox.Show("Duration : " + totalSpan, "Cleansing Done.")
            End If

            C_rows1.Text = load_cif_check.RowCount - 1
            C_rows2.Text = load_cif_found.RowCount - 1
            C_rows3.Text = load_cif_not_found.RowCount - 1
            C_Cells1.Text = load_cif_check.Columns.Count - 1

        End If


    End Sub



    '                'looping cells from datatable

    '                'For loop1 = 0 To 87
    '                '    If loop1 = 5 Then
    '                '        'Datatable have 89 column
    '                '        'Datagridview have 88 column
    '                '        'Column Name 6 
    '                '        row2.Cells(loop1).Value = table.Rows(x).Item(loop1).ToString
    '                '    Else
    '                '        row2.Cells(loop1).Value = table.Rows(x).Item(loop1).ToString
    '                '        ' row2.Cells(loop1).Value = load_cif_check.Rows(x).Cells(loop1).Value
    '                '        ' Console.WriteLine(load_cif_check.Rows(x).Cells(loop1).Value)
    '                '    End If
    '                '    'row2.Cells(loop1).Value = table.Rows(x + 1).Item(loop1).ToString
    '                'Next
    '                'Console.WriteLine(load_cif_check.Rows(x).Cells(0).Value)



    '                'Dim row1 As load_cif_found.NewRow(
    '                '    row["column2"]="column2";
    '                '    row["column6"]="column6";
    '                '    datatable1.Rows.Add(row);

    '                '                    DataGridViewRow row = (DataGridViewRow)load_cif_found.Rows[0].Clone()
    '                'row.Cells["Column2"].Value = "XYZ"
    '                'row.Cells["Column6"].Value = 50.2
    '                'load_cif_found.Rows.Add(row)

    '                'load_cif_found.Rows.Add(String.Format("{0}", getRow("ID_Card").ToString()))

    '                ''Data Found in database will send to other datagridview
    '                ''A Few Data who selected in database

    '                'newtable.Columns.Add("Decision", Type.GetType("System.String"))
    '                'Dim dr As DataRow = dt.NewRow
    '                'dr("Name") = txtName.Text

    '                'For Each getRows In table.AsEnumerable()
    '                '    load_cif_found.Rows.Add(String.Format("{0}", getRows("Decision").ToString()))
    '                '    load_cif_found.Rows.Add(String.Format("{0}", getRows("ID_Card").ToString()))
    '                '    'load_cif_found.Rows.Add(String.Format("{0}", getRows("WLF_Check").ToString()))
    '                '    'Console.WriteLine(String.Format("{0}", getRows("Name").ToString()))
    '                '    'load_cif_found.Rows(0).Cells(0).Value = String.Format("{0}", getRows("Decision").ToString())
    '                '    'Form1.DataGridView1.Rows(counter - 1).Cells(6).Value()
    '                'Next

    '                'Dim newRow As DataRow = table.NewRow()
    '                ''Split datatable 
    '                'Dim dReader As DataTableReader = table.CreateDataReader()
    '                'While dReader.Read()
    '                '    'get new row and manipulation  new data to datagridview (data found) 
    '                '    newRow("Decision") = dReader.GetValue(0)
    '                '    newRow("CIF") = dReader.GetValue(1)
    '                '    newtable.Rows.Add(newRow)
    '                'End While

    '                'load_cif_found.DataSource = table

    '                'load_cif_found.DataSource = newtable

    '            Else
    '                'For Data not in Table into 
    '                'MessageBox.Show("Data Not Found")
    '                ' Transfer Data to another datagridview

    '                row = DirectCast(load_cif_check.Rows(x).Clone(), DataGridViewRow)
    '                Dim intColIndex As Integer = 0
    '                For Each cell As DataGridViewCell In load_cif_check.Rows(x).Cells
    '                    row.Cells(intColIndex).Value = cell.Value
    '                    intColIndex += 1
    '                Next
    '                load_cif_not_found.Rows.Add(row)

    '            End If
    '            'MessageBox.Show("Checking Data : " + number.ToString() + " of " + Convert.ToString(load_cif_check.RowCount - 1))

    '        Next

    '        'GiveFeedbackAlert MessageBox
    '        MessageBox.Show("Data Found : " + number.ToString())
    '    Else
    '    End If


    '    'label count rows
    '    C_rows2.Text = load_cif_found.RowCount - 1
    '    C_rows3.Text = load_cif_not_found.RowCount - 2

    'End Sub
    'Private Sub btn_checking_Click(sender As Object, e As EventArgs) Handles btn_checking.Click
    '    Dim pesan As MsgBoxResult
    '    Dim row As New DataGridViewRow()
    '    Dim number As Integer = 0

    '    Dim DataFound As New DataTable

    '    Dim DataNotFound As New DataTable

    '    'Dim table As New DataTable()

    '    'clear all data datagridview
    '    'load_cif_found.Columns.Clear()
    '    'load_cif_not_found.Columns.Clear()
    '    pesan = MsgBox("Mulai periksa Data?", MsgBoxStyle.YesNo, "Perhatian")
    '    If pesan = MsgBoxResult.Yes Then

    '        'create new column in other datagridview has copyed
    '        If load_cif_not_found.Columns.Count = 0 Then
    '            For Each dgvc As DataGridViewColumn In load_cif_check.Columns
    '                load_cif_not_found.Columns.Add(TryCast(dgvc.Clone(), DataGridViewColumn))
    '            Next
    '        End If
    '        'create new column in other datagridview has copyed
    '        If load_cif_found.Columns.Count = 0 Then
    '            For Each dgvc2 As DataGridViewColumn In load_cif_check.Columns
    '                load_cif_found.Columns.Add(TryCast(dgvc2.Clone(), DataGridViewColumn))
    '            Next
    '        End If

    '        'Dim newRow As DataRow = dt.NewRow()
    '        ''Split datatable 
    '        'Dim dReader As DataTableReader = dt.CreateDataReader()
    '        'While dReader.Read()
    '        '    'get new row and manipulation  new data to datagridview (data found) 
    '        '    newRow("Decision") = dReader.GetValue(0)
    '        '    newRow("CIF") = dReader.GetValue(1)
    '        '    table.Rows.Add(newRow)
    '        'End While

    '        'load_cif_found.DataSource = table





    '        'Dim newRow As DataRow

    '        'newtable.Columns.Add(New DataColumn("IDCard", GetType(String)))
    '        'newtable.Columns.Add(New DataColumn("Decision", GetType(String)))

    '        Dim table As New DataTable()
    '        Console.WriteLine(dt.Rows.Count)
    '        For x As Integer = 0 To dt.Rows.Count - 1
    '            '    T_check1.Text = x + 1
    '            Dim getIDCard As String = dt.Rows(x).Item(14).ToString
    '            '    ' if data is same, will remove index array and not add to array new
    '            SQLConnection = New SqlConnection(SQLConnectionString)
    '            '    'variabel check data who checked
    '            cmd = New SqlCommand("SELECT DISTINCT(ID_Card),* FROM [DB_MASTER].[dbo].[DB_CIF_AKU_HIST] WHERE [ID_Card] = '" + getIDCard + "'", SQLConnection)

    '            Dim adapter As New SqlDataAdapter(cmd)
    '            adapter.Fill(table)
    '            '    'Dim getRow As DataRow()
    '            '    'Checking data to any rows in table "GELAR"
    '            '    'Dim arrayTable As New ArrayList()
    '            '    'Dim loop1 As Integer
    '            '    'new tabel
    '            'newtable.Columns.Add(New DataColumn("IDCard", GetType(String)))
    '            'newtable.Columns.Add(New DataColumn("Decision", GetType(String)))

    '            If table.Rows.Count() > 0 Then

    '                'Dim newRow As DataRow = table.NewRow()
    '                ''Split datatable 
    '                'Dim dReader As DataTableReader = table.CreateDataReader()
    '                'While dReader.Read()
    '                '    'get new row and manipulation  new data to datagridview (data found) 
    '                '    newRow("Decision") = dReader.GetValue(0)
    '                '    newRow("CIF") = dReader.GetValue(1)
    '                '    newtable.Rows.Add(newRow)
    '                'End While



    '                DataFound = table.Copy()


    '                'If table.Columns.Count = 0 Then
    '                '    For Each dgvc2 As DataColumn In table.Columns
    '                '        newtable.Columns.Add(TryCast(dgvc2.Clone(), DataColumn))
    '                '    Next
    '                'End If



    '                'newRow = table.NewRow()


    '                ''Split datatable 
    '                'Dim dReader As DataTableReader = table.CreateDataReader()
    '                'While dReader.Read()
    '                '    'get new row and manipulation  new data to datagridview (data found) 
    '                '    newRow("Decision") = dReader.GetValue(0)
    '                '    newRow("CIF") = dReader.GetValue(1)

    '                '    table.Rows.Add(newRow)

    '                'End While

    '                'load_cif_found.DataSource = newtable



    '                '        load_cif_found.DataSource = table



    '                'looping cells from datatable

    '                'For loop1 = 0 To 87
    '                '    If loop1 = 5 Then
    '                '        'Datatable have 89 column
    '                '        'Datagridview have 88 column
    '                '        'Column Name 6 
    '                '        row2.Cells(loop1).Value = table.Rows(x).Item(loop1).ToString
    '                '    Else
    '                '        row2.Cells(loop1).Value = table.Rows(x).Item(loop1).ToString
    '                '        ' row2.Cells(loop1).Value = load_cif_check.Rows(x).Cells(loop1).Value
    '                '        ' Console.WriteLine(load_cif_check.Rows(x).Cells(loop1).Value)
    '                '    End If
    '                '    'row2.Cells(loop1).Value = table.Rows(x + 1).Item(loop1).ToString
    '                'Next
    '                'Console.WriteLine(load_cif_check.Rows(x).Cells(0).Value)



    '                'Dim row1 As load_cif_found.NewRow(
    '                '    row["column2"]="column2";
    '                '    row["column6"]="column6";
    '                '    datatable1.Rows.Add(row);

    '                '                    DataGridViewRow row = (DataGridViewRow)load_cif_found.Rows[0].Clone()
    '                'row.Cells["Column2"].Value = "XYZ"
    '                'row.Cells["Column6"].Value = 50.2
    '                '                    load_cif_found.Rows.Add(row)

    '                'load_cif_found.Rows.Add(String.Format("{0}", getRow("ID_Card").ToString()))

    '                'Data Found in database will send to other datagridview
    '                'A Few Data who selected in database

    '                'newtable.Columns.Add("Decision", Type.GetType("System.String"))
    '                'Dim dr As DataRow = dt.NewRow
    '                'dr("Name") = txtName.Text

    '                'For Each getRows In table.AsEnumerable()
    '                '    load_cif_found.Rows.Add(String.Format("{0}", getRows("Decision").ToString()))
    '                '    load_cif_found.Rows.Add(String.Format("{0}", getRows("ID_Card").ToString()))
    '                '    'load_cif_found.Rows.Add(String.Format("{0}", getRows("WLF_Check").ToString()))
    '                '    'Console.WriteLine(String.Format("{0}", getRows("Name").ToString()))
    '                '    'load_cif_found.Rows(0).Cells(0).Value = String.Format("{0}", getRows("Decision").ToString())
    '                '    'Form1.DataGridView1.Rows(counter - 1).Cells(6).Value()
    '                'Next

    '                'Dim newRow As DataRow = table.NewRow()
    '                ''Split datatable 
    '                'Dim dReader As DataTableReader = table.CreateDataReader()
    '                'While dReader.Read()
    '                '    'get new row and manipulation  new data to datagridview (data found) 
    '                '    newRow("Decision") = dReader.GetValue(0)
    '                '    newRow("CIF") = dReader.GetValue(1)
    '                '    newtable.Rows.Add(newRow)
    '                'End While

    '                'load_cif_found.DataSource = table

    '                'load_cif_found.DataSource = newtable

    '            Else
    '                'For Data not in Table into 
    '                'MessageBox.Show("Data Not Found")
    '                ' Transfer Data to another datagridview

    '                row = DirectCast(load_cif_check.Rows(x).Clone(), DataGridViewRow)
    '                Dim intColIndex As Integer = 0
    '                For Each cell As DataGridViewCell In load_cif_check.Rows(x).Cells
    '                    row.Cells(intColIndex).Value = cell.Value
    '                    intColIndex += 1
    '                Next
    '                load_cif_not_found.Rows.Add(row)

    '            End If
    '            'MessageBox.Show("Checking Data : " + number.ToString() + " of " + Convert.ToString(load_cif_check.RowCount - 1))

    '        Next
    '        'Console.WriteLine(table.Rows.Count())
    '        Console.WriteLine(DataFound.Rows.Count())
    '        Console.WriteLine(DataNotFound.Rows.Count())
    '        load_cif_found.DataSource = DataFound
    '        load_cif_found.DataSource = DataFound

    '        'Console.WriteLine(newtable.Rows.Count())
    '        '    'GiveFeedbackAlert MessageBox
    '        '    MessageBox.Show("Data Found : " + number.ToString())
    '        'Else
    '    End If


    '    'label count rows
    '    C_rows2.Text = load_cif_found.RowCount - 1
    '    C_rows3.Text = load_cif_not_found.RowCount - 2

    'End Sub

    Private Sub Checking_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'dt_c.Columns.Add("NO")
        'dt_c.Columns.Add("Gelar")
        'dt.Columns.Add("Decsdfision2")
        'dt.Columns.Add("Decsdfision3")
        'dt.Columns.Add("Desdfcision2")
        'dt.Columns.Add("Decis2ion")
        'dt.Columns.Add("Decisio2n")
        'dt.Columns.Add("Decierts2ion")
        'dt.Columns.Add("Deci2dfgsion")
        'dt.Columns.Add("De112dfgsion")

        'dt.Columns.Add("Decisidxfggon")
        'dt.Columns.Add("De2scision")
        'dt.Columns.Add("Decidsion")
        'dt.Columns.Add("Decisiddddon")
        'dt.Columns.Add("Decifdsseion")
        'dt.Columns.Add("Decsdfisi55")
        'dt.Columns.Add("Desdfcision")
        'dt.Columns.Add("Decsdfisi5on")
        'dt.Columns.Add("Decsdision")
        'dt.Columns.Add("Decisis34dfon")

        'dt.Columns.Add("Decs4dfision")
        'dt.Columns.Add("Desdf77cision")
        'dt.Columns.Add("Dejhsrdfcision")
        'dt.Columns.Add("Dfdgecision")
        'dt.Columns.Add("Dechgisi88on")
        'dt.Columns.Add("Dend7ision")
        'dt.Columns.Add("De4c7j5i6sidfgon")
        'dt.Columns.Add("7enh3ci465sion")
        'dt.Columns.Add("Dec56i45snion")
        'dt.Columns.Add("Decdifghsion")

        'dt.Columns.Add("Deceyision")
        'dt.Columns.Add("Deckirsion")
        'dt.Columns.Add("Dec6774ision")
        'dt.Columns.Add("Decildfgsion")
        'dt.Columns.Add("Decx6ddfg7ision")
        'dt.Columns.Add("Dec86isfghion")
        'dt.Columns.Add("Decidfgjhsi7n")
        'dt.Columns.Add("Dec7yisfdion")
        'dt.Columns.Add("Dec45isxion")
        'dt.Columns.Add("Deciewvsion")

        'dt.Columns.Add("Decisdfsion")
        'dt.Columns.Add("Dechlkision")
        'dt.Columns.Add("Decg78h7ision")
        'dt.Columns.Add("Decdfgision")
        'dt.Columns.Add("Dec7l;'78sion")
        'dt.Columns.Add("Decl;'ision")
        'dt.Columns.Add("De7i;l'sion")
        'dt.Columns.Add("De7lgdkision")
        'dt.Columns.Add("Decdfgidfsion")
        'dt.Columns.Add("Dedfg234cision")

        'dt.Columns.Add("Dewci234sion")
        'dt.Columns.Add("Dewe7cision")
        'dt.Columns.Add("Decuu234ision")
        'dt.Columns.Add("Decd677sion")
        'dt.Columns.Add("De5cl;idsion")
        'dt.Columns.Add("Degc7s9;ion")
        'dt.Columns.Add("Decil87sion")
        'dt.Columns.Add("Deci'sion")
        'dt.Columns.Add("Dec;7;tision")
        'dt.Columns.Add("Dec;ision")

        'dt.Columns.Add("Decis8w-sion")
        'dt.Columns.Add("Dec546ision")
        'dt.Columns.Add("Dee570rcision")
        'dt.Columns.Add("Dec7-idfhgsion")
        'dt.Columns.Add("Dec9i8gsion")
        'dt.Columns.Add("Deci=7sion")
        'dt.Columns.Add("Decsdhision")
        'dt.Columns.Add("Decfdgi-5t7ion")
        'dt.Columns.Add("Decdfg-i97ion")
        'dt.Columns.Add("De345cision")

        'dt.Columns.Add("Dec5ision")
        'dt.Columns.Add("Deertc64ision")
        'dt.Columns.Add("Deerci6sion")
        'dt.Columns.Add("Dece-897tpsion")
        'dt.Columns.Add("Deertcision")
        'dt.Columns.Add("Deerpcisi4on")
        'dt.Columns.Add("Dg.e7isi7on")
        'dt.Columns.Add("Demdfcision")
        'dt.Columns.Add("Defgdcision")
        'dt.Columns.Add("Dbe45cision")

        'dt.Columns.Add("Decis906ion")
        'dt.Columns.Add("Decfhision")
        'dt.Columns.Add("Decps5fzision")
        'dt.Columns.Add("Dtegfgci6sion")
        'dt.Columns.Add("De4czxczxfision")
        'dt.Columns.Add("Decz566ision")
        'dt.Columns.Add("Decz45ppision")
        'dt.Columns.Add("Deczpcision")
        'dt.Columns.Add("Deczxc56ision")
        'dt.Columns.Add("Decirx56csion")

        'dt.Columns.Add("Decirt3ysion")
        'dt.Columns.Add("Decitrysion")
        'dt.Columns.Add("D3ecision")
        'dt.Columns.Add("tr")
        'dt.Columns.Add("Decfgifsrsion")
        'dt.Columns.Add("Decdf3xgisdsion")
        'dt.Columns.Add("Dedfg4cision")
        'dt.Columns.Add("Decsd4fision")
        'dt.Columns.Add("Deci5sfsion")


        'Dim row As DataRow
        'For XC1 = 0 To 4
        '    row = dt_c.NewRow
        '    row(0) = XC1
        '    row(1) = XC1 * 2
        '    dt_c.Rows.Add(row)
        'Next
        'load_cif_check.DataSource = dt_c

        'For i = 0 To load_cif_check.Rows.Count - 1
        '    r = dt.NewRow
        '    r("Dicision") = load_cif_check.Item(0, i).Value.ToString
        '    'r("b") = load_cif_check.Item(1, i).Value.ToString
        '    'r("c") = load_cif_check.Item(2, i).Value.ToString
        '    'r("d") = load_cif_check.Item(3, i).Value.ToString
        '    dt.Rows.Add(r)
        'Next
        btn_checking.Enabled = True
        C_rows1.Text = 0
        C_rows2.Text = 0
        C_rows3.Text = 0
        C_Cells1.Text = 0
    End Sub
    Public Sub DataTableToCSV(table As DataTable, filePath As String)
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
                    If Not firstField Then writer.Write(";")
                    cellvalue1 = table.Columns.Item(i).ToString()
                    If cellvalue1.Contains(";") Then
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
                        If Not firstField Then writer.Write(";")
                        If Not row.IsNull(i) Then
                            cellvalue = row.Item(i).ToString
                            If cellvalue.Contains(";") Then
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

    'PR BIKIN SPLIT 1000 ROWS DARI TOTAL ROWS 
    Private Sub btn_export_cif_found_Click(sender As Object, e As EventArgs) Handles btn_export_cif_found.Click
        If load_cif_found.Rows.Count <= 0 Then
            MessageBox.Show("Data is null.!", "Alert")
        Else
            Dim getdate As Date = Date.Today
            Dim strdate As String
            getdate.ToString("ddMMyyyy")
            strdate = getdate.ToString("dd") + "" + getdate.ToString("MM") + "" + getdate.ToString("yyyy") + "_" + getdate.ToString("HH") + "" + getdate.ToString("mm") + "" + getdate.ToString("ss")
            Dim filenameCSV As String
            filenameCSV = "D:\CIF_Found_" + strdate + ".txt"
            Call DataTableToCSV(dataFound, filenameCSV)
            MessageBox.Show("Export has created.""Alert")
        End If
    End Sub

    Private Sub btn_export_cif_not_found_Click(sender As Object, e As EventArgs) Handles btn_export_cif_not_found.Click
        If load_cif_not_found.Rows.Count <= 0 Then
            MessageBox.Show("Data is null.!", "Alert")
        Else
            Dim getdate As Date = Date.Today
            Dim strdate As String
            getdate.ToString("ddMMyyyy")
            strdate = getdate.ToString("dd") + "" + getdate.ToString("MM") + "" + getdate.ToString("yyyy") + "_" + getdate.ToString("HH") + "" + getdate.ToString("mm") + "" + getdate.ToString("ss")
            Dim filenameCSV As String
            filenameCSV = "D:\CIF_Not_Found_" + strdate + ".txt"
            Call DataTableToCSV(notDataFound, filenameCSV)
            MessageBox.Show("Export has created.""Alert")
        End If
    End Sub
    Private Sub btn_summary_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub btn_clear_Click(sender As Object, e As EventArgs) Handles btn_clear.Click
        If load_cif_found.Rows.Count > 0 Or load_cif_not_found.Rows.Count > 0 Then
            'clear all data datagridview
            load_cif_found.Columns.Clear()
            load_cif_not_found.Columns.Clear()
            Call TruncateTableCIF()
        End If
        MessageBox.Show("Done.", "Alert")
    End Sub

    Private Sub btn_cleansing_Click(sender As Object, e As EventArgs) Handles btn_cleansing.Click
        If load_cif_not_found.Rows.Count > 0 Then
            'send value to another form
            Dim fm As New Form1 With {.Owner = Me}
            Try
                fm.Show()
                fm.MdiParent = Home
            Finally
                'fm.Dispose()
            End Try
        Else
            MessageBox.Show("Data not found is null", "Alert")
        End If
        
    End Sub
End Class