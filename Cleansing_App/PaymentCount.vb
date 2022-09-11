Imports System.Math
Public Class PaymentCount

    Private table_payment As New DataTable
    Private rowPayment As DataRow
    Private loan As Double
    Private interest_percent As Double
    Private tenor As Double
    Private instalment_amount As Double
    Private interest_n As Double
    Private OS_Balance As Double
    Private Principal As Double
    Private sumPrincipal As Double
    Private sumPayments As Double
    Private sumInstalment As Double
    Private datePayment As String
    Private getdate, setdate As DateTime
    Private dateDisbure, startDate, endDate As DateTime
    Private strDateDisbure As String
    Private arrDueDate(60) As String
    'Private arrDueDate = New String() {}
    Private Sub btn_checking_Click(sender As Object, e As EventArgs) Handles btn_checking.Click

        Try
            If Not String.IsNullOrWhiteSpace(txt_tenor.Text) Or Not String.IsNullOrWhiteSpace(txt_loan_amount.Text) Then
                'clear before
                table_payment.Rows.Clear()
                table_payment.Columns.Clear()
                sumInstalment = 0
                sumPayments = 0
                sumPrincipal = 0
                loan = 0
                interest_percent = 0
                tenor = 0
                instalment_amount = 0
                interest_n = 0
                OS_Balance = 0
                Principal = 0

                'start process
                interest_percent = Convert.ToDouble(txt_interest.Text)
                loan = Convert.ToDouble(txt_loan_amount.Text)
                tenor = Convert.ToDouble(txt_tenor.Text)
                'CountPMT('Payment loan','interest rate','month tenor')
                instalment_amount = CountPMT(loan, interest_percent, tenor)
                txt_instalment_amount.Text = "RP. " + Convert.ToString(instalment_amount) + ".00"
                'excute oustanding/ disbursement

                '------Start set count due date any month------
                'strdate is Date Disburesement
                dateDisbure = dt_disburse.Text


                'Dim getdate As Date = Convert.ToDateTime(strdate)
                'strdate = getdate.ToString("dd-MM-yyyy")
                'Dim getYear As String = Strings.Left(strDateDisbure, 4)
                'Dim getMonth As String = Strings.Mid(strDateDisbure, 5, 2)
                'Dim getDay As String = Strings.Right(strDateDisbure, 2)

                'strDateDisbure = getYear + "-" + getMonth + "-" + getDay
                'dateDisbure = Convert.ToDateTime(strDateDisbure.ToString)

                'Dim format As String = "dd-MM-yyyy"
                'getdate = getdate.ToString(format)

                '------End set count due date any month------

                'show interest, payment in mothly until payment end tenor
                'add new column
                table_payment.Columns.Add("Month")
                table_payment.Columns.Add("Due Date")
                table_payment.Columns.Add("Start Payment")
                table_payment.Columns.Add("End Payment")
                table_payment.Columns.Add("Principal")
                table_payment.Columns.Add("Interest")
                table_payment.Columns.Add("Payment")
                table_payment.Columns.Add("OS Balance")
                'add date payment
                'Dim getdate As Date = DateTimePicker1.Value
                'Dim datePayment, day, mounth, year As String
                'add datarow
                For x As Integer = 0 To tenor
                    Dim getNoDays As Integer
                    'add new row
                    rowPayment = table_payment.NewRow()
                    table_payment.Rows.Add(rowPayment)
                    'process formula 
                    'interest
                    If x = 0 Then
                        Dim loan_percent As Double = interest_percent * loan
                        interest_n = (loan_percent / 12)
                        interest_n = Math.Round(interest_n)

                    ElseIf x = tenor Then
                        interest_n = interest_n
                    Else
                        Dim loan_percent As Double = interest_percent * OS_Balance
                        interest_n = (loan_percent / 12)
                        interest_n = Math.Round(interest_n)
                    End If

                    'principal             
                    If x = tenor - 1 Then
                        Principal = instalment_amount - interest_n
                    ElseIf x = tenor Then
                        Principal = Principal + OS_Balance
                    Else
                        Principal = instalment_amount - interest_n
                    End If
                    'Payment             
                    If x = tenor Then
                        instalment_amount = Principal + interest_n
                    End If
                    'OS Balance
                    If x = 0 Then
                        OS_Balance = loan - Principal
                    ElseIf x = tenor Then
                        OS_Balance = Convert.ToString(0) + ".00"
                    Else
                        OS_Balance = OS_Balance - Principal
                    End If

                    'end formula
                    'calculation sum
                    If Not x = Int(tenor - 1) Then
                        sumPrincipal += Principal
                        sumInstalment += interest_n
                        sumPayments += instalment_amount
                    End If
                    '-----Output Table Payment-----
                    'row data column Month
                    If x = tenor Then
                        table_payment.Rows(x).Item(0) = Convert.ToString(x)
                    Else
                        table_payment.Rows(x).Item(0) = x + 1
                    End If

                    'due date payment any month
                    'condition calculations Day (WorksDay)
                    'Add 1 Month from Date Disbursement

                    If x = tenor Then
                        setdate = DateAdd(DateInterval.Month, x, dateDisbure)
                    Else
                        setdate = DateAdd(DateInterval.Month, x + 1, dateDisbure)
                    End If
                    '--Due Date Month 1 --

                    'check max date if not day in month
                    'get duedate last month
                    If IsError(setdate) Then
                        setdate = DateSerial(Year(setdate), Month(setdate) + 1, 0)
                        'add to array
                        arrDueDate(x) = setdate

                        table_payment.Rows(x).Item(1) = setdate.ToString("dd-MM-yyyy")
                    Else
                        'add to array
                        arrDueDate(x) = setdate
                        table_payment.Rows(x).Item(1) = setdate.ToString("dd-MM-yyyy")
                    End If

                    'start payment any month
                    'Start payment is same the duedate on month
                    'inisialisasi value variabel startdate from last dutdate month
                    If Not x = 0 Then
                        'get duedate last month
                        startDate = arrDueDate(x - 1)
                        'Console.WriteLine(startDate)
checkLoopStartPayment1:
                        getNoDays = checkDayofWeeks(startDate)

                        If getNoDays = 1 Or getNoDays = 7 Then
                            'add next day
                            startDate = DateAdd(DateInterval.Day, 1, startDate)
                            GoTo checkLoopStartPayment1
                        Else
                            If x = tenor Then
                                startDate = arrDueDate(x)
                                startDate = DateAdd(DateInterval.Month, -1, startDate)
                                table_payment.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                                'end check wordays
                            Else

                                Console.WriteLine(startDate)
                                'check workdays
                                table_payment.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                                'end check wordays
                            End If
                        End If
                    ElseIf x = 0 Then
                        startDate = dateDisbure
checkLoopStartPayment2:
                        getNoDays = checkDayofWeeks(startDate)

                        Console.WriteLine(getNoDays)
                        If getNoDays = 1 Or getNoDays = 7 Then
                            'add next day
                            startDate = DateAdd(DateInterval.Day, 1, startDate)
                            GoTo checkLoopStartPayment2
                        Else

                            Console.WriteLine(startDate)
                            table_payment.Rows(x).Item(2) = startDate.ToString("dd-MM-yyyy")
                        End If

                        ' startDate = arrDueDate(x - 1)
                        'startDate = DateAdd(DateInterval.Month, -1, startDate)
                    End If


checkLoopEndPayment:
                    getNoDays = checkDayofWeeks(setdate)
                    If getNoDays = 1 And getNoDays = 7 Then
                        'add next day
                        endDate = DateAdd(DateInterval.Day, -1, setdate)
                        GoTo checkLoopEndPayment
                    Else
                        endDate = DateAdd(DateInterval.Day, -1, setdate)
                        table_payment.Rows(x).Item(3) = endDate.ToString("dd-MM-yyyy")
                    End If



                    'column Principal
                    table_payment.Rows(x).Item(4) = Convert.ToString(Principal) + ".00"
                    'column Interest
                    table_payment.Rows(x).Item(5) = Convert.ToString(interest_n) + ".00"
                    'column Instalment /Payment
                    table_payment.Rows(x).Item(6) = Convert.ToString(instalment_amount) + ".00"
                    'column OS Balance
                    If x = tenor Then
                        table_payment.Rows(x).Item(7) = Convert.ToString(Int(OS_Balance)) + ".00"
                    Else
                        table_payment.Rows(x).Item(7) = Convert.ToString(OS_Balance) + ".00"
                    End If

                Next
                'remove row in datatable
                table_payment.Rows.RemoveAt(Int(tenor - 1))
                load_payment_history.DataSource = table_payment
                'send sum output
                txt_sum_instalment.Text = "RP. " + Convert.ToString(sumInstalment) + ".00"
                txt_sum_payment.Text = "RP. " + Convert.ToString(sumPayments) + ".00"
                txt_sum_principal.Text = "RP. " + Convert.ToString(sumPrincipal) + ".00"

                'size datagridview
                load_payment_history.Columns(0).Width = 50
                load_payment_history.Columns(1).Width = 70
                load_payment_history.Columns(2).Width = 70
                load_payment_history.Columns(3).Width = 70
                load_payment_history.Columns(4).Width = 70
                load_payment_history.Columns(5).Width = 70
                load_payment_history.Columns(6).Width = 70
                load_payment_history.Columns(7).Width = 70
            Else
                MessageBox.Show("Please, Insert Amount or Tenor.!", "Alert")
            End If
        Catch ex As Exception

        Finally
            'disabled 
            For Each a As DataGridViewColumn In load_payment_history.Columns
                a.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End Try


    End Sub
    Private Function checkDayofWeeks(ByRef getdate As Date) As Integer
        Dim noDay As Integer
        'Count Condition Day tobe Workday or Weekend
        Select Case Weekday(getdate)
            Case 1
                noDay = 1
            Case 7
                noDay = 7
            Case Else
                'check database holiday this year
                noDay = Weekday(getdate)

        End Select
        Return noDay
    End Function


    Private Function CountPMT(ByRef PVal As Double, ByRef APR As Double, ByRef TotPmts As Integer) As Double
        Dim Payment As Double
        Dim PayType As DueDate
        'Dim Response As MsgBoxResult
        ' Define money format.
        ' Usually 0 for a loan.
        Dim FVal As Double = 0
        'PVal = CDbl(InputBox("How much do you want to borrow?"))
        'APR = CDbl(InputBox("What is the annual percentage rate of your loan?"))
        If APR > 1 Then APR = APR / 100 ' Ensure proper form.
        'TotPmts = CDbl(InputBox("How many monthly payments will you make?"))
        'Response = MsgBox("Do you make payments at the end of month?", MsgBoxStyle.YesNo)
        'If Response = MsgBoxResult.No Then
        '    PayType = DueDate.BegOfPeriod
        'Else
        '    PayType = DueDate.EndOfPeriod
        'End If
        PayType = dueDate.EndOfPeriod
        Payment = Pmt(APR / 12, TotPmts, -PVal, FVal, PayType)
        'Pembulatan
        Payment = Math.Round(Payment)
        Return Payment
    End Function

    

    Private Function Format(Payment As Double, Fmt As String) As String
        Throw New NotImplementedException
    End Function

    Private Sub btn_clear_Click(sender As Object, e As EventArgs)
        Try
            'Me.Dispose()
            'txt_instalment_amount.Text = 0
            'txt_sum_instalment.Text = 0
            'txt_sum_payment.Text = 0
            'txt_sum_principal.Text = 0
            'table_payment.Columns.Clear()
            'table_payment.Rows.Clear()

            'load_payment_history.Columns.Clear()
            'load_payment_history.Rows.Clear()
            'table_payment.AcceptChanges()
        Catch ex As Exception

        End Try

    End Sub

   
    Private Sub PaymentCount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txt_loan_amount.Focus()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs)

        'Dim strdate As String = "20211201"
        'Dim getYear As String = Strings.Left(strdate, 4)
        'Dim getMonth As String = Strings.Mid(strdate, 5, 2)
        'Dim getDay As String = Strings.Right(strdate, 2)

        '' strdate = getDay + "-" + getMonth + "-" + getYear
        'strDateDisbure = getDay.ToString() + "-" + getMonth.ToString() + "-" + getYear.ToString()
        'strDateDisbure = Convert.ToString(strDateDisbure)
        'Dim setDueDate As DateTime = DateTime.Parse(strDateDisbure)
        ''Dim setDate As Date = Convert.ToDateTime(strdate)

        ''Console.WriteLine(DateAdd(DateInterval.Month, 1, setDate))
        ''setDate.Month.ToString()

        'Console.WriteLine(setDueDate)
        ''Console.WriteLine(getMonth)
        ''Console.WriteLine(getDay)
        ''strdate = setDate.ToString("dd-MM-yyyy")
        ''Console.WriteLine(setDate)

    End Sub
End Class