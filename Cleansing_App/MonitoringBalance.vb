
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
Public Class MonitoringBalance
    Private conn As OleDbConnection 'koneksi odb
    Private table As New DataTable
    Private dts As DataSet ' membuat table datasheet virtual
    Private excel, excel2 As String

    Private dt_report As New DataTable
    'Private SQLConnection As New Data.SqlClient.SqlConnection
    Private cmd, cmd2 As New Data.SqlClient.SqlCommand
    Dim openfiledialog As New OpenFileDialog ' membuka file dialog
    '...............SQL SERVER..............
    'Connection SQL Server
    'Private SQLConnectionString As String = "Server=ADMIN-PC\SQLEXPRESS; Database=DB_MASTER; integrated security=true"
    Public SQL As New ConnectionSQL
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Private total_balance_nasabah As Double

    Private Sub MonitoringBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txt_total_balance.Text = "Rp. 0,00"
    End Sub

    Private Sub btn_cari2_Click(sender As Object, e As EventArgs) Handles btn_cari2.Click
        Try
            If Not dt_report Is Nothing Then
                dt_report.Rows.Clear()
                dt_report.Columns.Clear()
            End If
            SQLConnection = New SqlConnection(SQLConnectionString)
            Dim shbi_no As String = Trim(txt_shbi_no.Text)
            Dim startDate As DateTime = dt_startDate.Value
            Dim endDate As DateTime = dt_endDate.Value
            dt_report = checkDateHistoryBalancedNasabah(shbi_no, startDate, endDate)
            txt_total_balance.Text = "Rp. " + checkDateHistTotalBalancedNasabah(shbi_no, startDate, endDate).ToString + ",00"
            load_output.DataSource = dt_report
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            C_Cells1.Text = load_output.Columns.Count
            C_rows1.Text = load_output.Rows.Count - 1
            For Each a As DataGridViewColumn In load_output.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub

    Private Sub btn_cari1_Click(sender As Object, e As EventArgs) Handles btn_cari1.Click
        Try
            If Not dt_report Is Nothing Then
                dt_report.Rows.Clear()
                dt_report.Columns.Clear()
            End If
            SQLConnection = New SqlConnection(SQLConnectionString)
            Dim shbi_no As String = Trim(txt_shbi_no.Text)
            dt_report = checkHistoryBalancedNasabah(shbi_no)
            txt_total_balance.Text = "Rp. " + checkTotalBalancedNasabah(shbi_no).ToString + ",00"
            load_output.DataSource = dt_report
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            C_Cells1.Text = load_output.Columns.Count
            C_rows1.Text = load_output.Rows.Count - 1
            For Each a As DataGridViewColumn In load_output.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try
    End Sub
    Private Function checkTotalBalancedNasabah(ByRef shbi_acc_no As String) As Double
        Dim result As Double
        Dim checktable As New DataTable()
        If Not String.IsNullOrWhiteSpace(shbi_acc_no) Then
            cmd = New SqlCommand("SELECT [Total Balanced] FROM [DB_RECONSILE].[dbo].[DB_TOTAL_BALANCED_PAYMENTS] WHERE [shbi acct no] = '" + shbi_acc_no.ToString + "'", SQLConnection)

        Else
            cmd = New SqlCommand("SELECT SUM([Total Balanced]) FROM [DB_RECONSILE].[dbo].[DB_TOTAL_BALANCED_PAYMENTS]", SQLConnection)

        End If
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(checktable)
        'Checking data to any rows in table balance
        If checktable.Rows.Count() > 0 Then
            'get data balance
            If String.IsNullOrWhiteSpace(Convert.ToString(checktable.Rows(0).Item(0))) Then
                result = 0
            Else
                result = checktable.Rows(0).Item(0)
            End If
        Else
            result = 0
        End If
        Return result
    End Function
    Private Function checkDateHistTotalBalancedNasabah(ByRef shbi_acc_no As String, ByRef startDate As Date, ByRef endDate As Date) As Double
        Dim result As Double
        Dim checktable As New DataTable()
        If Not String.IsNullOrWhiteSpace(shbi_acc_no) Then
            cmd = New SqlCommand("SELECT SUM([Balanced]) FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] WHERE [shbi acct no] = '" + shbi_acc_no.ToString + "' AND Date BETWEEN '" + startDate + "' AND '" + endDate + "' ", SQLConnection)
        Else
            cmd = New SqlCommand("SELECT SUM([Balanced]) FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] WHERE Date BETWEEN '" + startDate + "' AND '" + endDate + "' ", SQLConnection)
        End If
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(checktable)
        'Checking data to any rows in table balance
        If checktable.Rows.Count() > 0 Then
            'get data balance
            If String.IsNullOrWhiteSpace(Convert.ToString(checktable.Rows(0).Item(0))) Then
                result = 0
            Else
                result = checktable.Rows(0).Item(0)
            End If

        Else
            result = 0
        End If
        Return result
    End Function

    Private Function checkDateHistoryBalancedNasabah(ByRef shbi_acc_no As String, ByRef startDate As DateTime, ByRef endDate As DateTime) As DataTable
        Dim result As New DataTable
        Dim checktable As New DataTable()
        If Not String.IsNullOrWhiteSpace(shbi_acc_no) Then
            cmd = New SqlCommand("SELECT [Date] ,[Repayment date] ,[Execute date],[Product code]  ,[Product cif] ,[Product acc no],[shbi cif no],[shbi acct no] ,[Balanced],[auto transfer acno] ,[Flag],[Tenor no],[Remain Tenor] FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] WHERE [shbi acct no] = '" + shbi_acc_no.ToString + "' AND [Repayment date] BETWEEN '" + startDate + "' AND '" + endDate + "' ORDER BY [Repayment date] ASC ", SQLConnection)
        Else
            cmd = New SqlCommand("SELECT [Date] ,[Repayment date] ,[Execute date],[Product code]  ,[Product cif] ,[Product acc no],[shbi cif no],[shbi acct no] ,[Balanced],[auto transfer acno] ,[Flag],[Tenor no],[Remain Tenor] FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] WHERE [Repayment date] BETWEEN '" + startDate + "' AND '" + endDate + "' ORDER BY [Repayment date] ASC ", SQLConnection)
        End If
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(checktable)
        'Checking data to any rows in table "Occupation
        If checktable.Rows.Count() > 0 Then
            'get data Occupation
            result = checktable.Copy()
        Else
            result = checktable.Copy()
        End If
        Return result
    End Function
    Private Function checkHistoryBalancedNasabah(ByRef key As String) As DataTable
        Dim result As New DataTable
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(key) Then
            result = Nothing
        Else
            cmd = New SqlCommand("SELECT [Date] ,[Repayment date] ,[Execute date],[Product code]  ,[Product cif] ,[Product acc no],[shbi cif no],[shbi acct no] ,[Balanced],[auto transfer acno] ,[Flag],[Tenor no],[Remain Tenor] FROM [DB_RECONSILE].[dbo].[DB_BALANCED_HIST] WHERE [shbi acct no] = '" + key.ToString + "'ORDER BY [Repayment date] ASC ", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table "Occupation
            If checktable.Rows.Count() > 0 Then
                'get data Occupation
                result = checktable.Copy()
            Else
                result = checktable.Copy()
            End If
        End If
        Return result
    End Function
End Class