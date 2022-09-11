Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Sql

Public Class accountPayments
    Public SQL As New ConnectionSQL
    Private SQLConnectionString As String = SQL.SQLConnectionString
    Private SQLConnection As New Data.SqlClient.SqlConnection
    Public Function typeAccountPayments(ByRef no_acc_auto As String) As String
        Dim type As String
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()
        If String.IsNullOrWhiteSpace(no_acc_auto) Then
            type = ""
        Else
            SQLConnection = New SqlConnection(SQLConnectionString)
            'check sum balanced today
            cmd = New SqlCommand("SELECT [ID_PRODUCT],[PRODUCT_NAME],[CODE] ,[NO_ACCOUNT],[TYPE]
  FROM [DB_MASTER].[dbo].[DB_NO_ACC_PRODUCTS] WHERE [NO_ACCOUNT] = '" + no_acc_auto.ToString + "'", SQLConnection)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(checktable)
            'Checking data to any rows in table 
            If checktable.Rows.Count() > 0 Then
                'get data balanced calculation
                'sum data total balanced
                type = checktable.Rows(0).Item(4).ToString()
            Else
                'set reason
                type = ""
            End If
        End If

        Return type
    End Function
    Public Function getAllProducts() As DataTable
        Dim tb_product As New DataTable
        Dim cmd As New Data.SqlClient.SqlCommand
        Dim checktable As New DataTable()

        SQLConnection = New SqlConnection(SQLConnectionString)
        'check sum balanced today
        cmd = New SqlCommand("SELECT [NO],[NAME_PRODUCT] ,[INTEREST],[KODE_PRODUCT] FROM [DB_MASTER].[dbo].[DB_PRODUCTS] ORDER BY [NO] ASC", SQLConnection)
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(checktable)
        'Checking data to any rows in table 
        If checktable.Rows.Count() > 0 Then
            'get data balanced calculation
            'sum data total balanced
            tb_product = checktable.Copy()
        Else
            'set reason
            tb_product = Nothing
        End If
        Return tb_product
    End Function

End Class
