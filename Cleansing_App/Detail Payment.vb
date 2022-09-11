Public Class Detail_Payment
    Public multi_acc As String
    Public indexRows As Integer
    Private Sub Detail_Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txt_multifinance_customer.Focus()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txt_interest_daily.TextChanged

    End Sub
End Class