Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Sql

Public Class ConnectionSQL
    Public SQLConnection As New Data.SqlClient.SqlConnection
    'conection for Server Aither Budi PC
    Public SQLConnectionString As String = "Server=10.97.240.176,1433;Network Library=DBMSSOCN; Database=DB_MASTER; User Id=sa;Password=shinhan@1"
    ' Public SQLConnectionString As String = "Server=ADMIN-PC\SQLEXPRESS; Database=DB_MASTER; integrated security=true"
    '  Public SQLConnectionStringReconsile As String = "Server=ADMIN-PC\SQLEXPRESS; Database=DB_RECONSILE; integrated security=true"
    ' Public SQLConnectionString As String = "Server=DESKTOP-RES4M7I\SQLEXPRESS; Database=DB_MASTER; integrated security=true"
    'Public SQLConnectionStringReconsile As String = "Server=DESKTOP-RES4M7I\SQLEXPRESS; Database=DB_RECONSILE; integrated security=true"
    'pc dipsy
    ' Public SQLConnectionString As String = "Server=HPQ-7700-0286B\SQLEXPRESS;Database=DB_MASTER;User Id=sa;Password=shinhan@1"
    ' Public SQLConnectionStringReconsile As String = "Server=HPQ-7700-0286B\SQLEXPRESS;Database=DB_RECONSILE;User Id=sa;Password=shinhan@1"
    'pc budi
    'Public SQLConnectionString As String = "Server=HPQ-7700-2176B\SQLEXPRESS;Database=DB_MASTER;User Id=sa;Password=shinhan@1"
    ' Public SQLConnectionStringReconsile As String = "Server=HPQ-7700-2176B\SQLEXPRESS;Database=DB_RECONSILE;User Id=sa;Password=shinhan@1"
    Public Sub ConnSQLDefault()
        Try
            SQLConnection = New SqlConnection(SQLConnectionString)
            If SQLConnection.State = ConnectionState.Closed Then
                SQLConnection.Open()
            End If
        Catch ex As Exception
            MsgBox("Database gagal koneksi karena " & ex.Message)
        End Try
    End Sub
    'Public Sub ConnSQLReconsile()
    '    Try
    '        SQLConnection = New SqlConnection(SQLConnectionStringReconsile)
    '        If SQLConnection.State = ConnectionState.Closed Then
    '            SQLConnection.Open()
    '        End If
    '    Catch ex As Exception
    '        MsgBox("Database gagal koneksi karena " & ex.Message)
    '    End Try
    'End Sub
End Class
