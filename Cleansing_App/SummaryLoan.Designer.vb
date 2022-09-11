<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SummaryLoan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.n_cif_final = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.n_no_cif_gagal = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txt_product = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.n_total_loan = New System.Windows.Forms.Label()
        Me.n_total_amount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.n_loan_takedown_amount = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.n_loan_takedown = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.n_loan_final_amount = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.n_loan_final = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(158, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Rows"
        '
        'n_cif_final
        '
        Me.n_cif_final.AutoSize = True
        Me.n_cif_final.ForeColor = System.Drawing.Color.Black
        Me.n_cif_final.Location = New System.Drawing.Point(106, 29)
        Me.n_cif_final.Name = "n_cif_final"
        Me.n_cif_final.Size = New System.Drawing.Size(13, 13)
        Me.n_cif_final.TabIndex = 3
        Me.n_cif_final.Text = "0"
        Me.n_cif_final.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 29)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 13)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "CIF Final"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.n_no_cif_gagal)
        Me.GroupBox2.Controls.Add(Me.n_cif_final)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 40)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(210, 91)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data CIF "
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(158, 57)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(34, 13)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = "Rows"
        '
        'n_no_cif_gagal
        '
        Me.n_no_cif_gagal.AutoSize = True
        Me.n_no_cif_gagal.ForeColor = System.Drawing.Color.Black
        Me.n_no_cif_gagal.Location = New System.Drawing.Point(106, 57)
        Me.n_no_cif_gagal.Name = "n_no_cif_gagal"
        Me.n_no_cif_gagal.Size = New System.Drawing.Size(13, 13)
        Me.n_no_cif_gagal.TabIndex = 20
        Me.n_no_cif_gagal.Text = "0"
        Me.n_no_cif_gagal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(9, 57)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(71, 13)
        Me.Label27.TabIndex = 19
        Me.Label27.Text = "No CIF Gagal"
        '
        'txt_product
        '
        Me.txt_product.AutoSize = True
        Me.txt_product.Location = New System.Drawing.Point(18, 11)
        Me.txt_product.Name = "txt_product"
        Me.txt_product.Size = New System.Drawing.Size(56, 13)
        Me.txt_product.TabIndex = 5
        Me.txt_product.Text = "- (Product)"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.n_total_loan)
        Me.GroupBox4.Controls.Add(Me.n_total_amount)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.n_loan_takedown_amount)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.n_loan_takedown)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.n_loan_final_amount)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.n_loan_final)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Location = New System.Drawing.Point(242, 11)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(424, 120)
        Me.GroupBox4.TabIndex = 10
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Data Loan "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 94)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Total "
        '
        'n_total_loan
        '
        Me.n_total_loan.AutoSize = True
        Me.n_total_loan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.n_total_loan.ForeColor = System.Drawing.Color.Black
        Me.n_total_loan.Location = New System.Drawing.Point(106, 94)
        Me.n_total_loan.Name = "n_total_loan"
        Me.n_total_loan.Size = New System.Drawing.Size(13, 13)
        Me.n_total_loan.TabIndex = 31
        Me.n_total_loan.Text = "0"
        Me.n_total_loan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'n_total_amount
        '
        Me.n_total_amount.AutoSize = True
        Me.n_total_amount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.n_total_amount.ForeColor = System.Drawing.Color.Black
        Me.n_total_amount.Location = New System.Drawing.Point(301, 94)
        Me.n_total_amount.Name = "n_total_amount"
        Me.n_total_amount.Size = New System.Drawing.Size(13, 13)
        Me.n_total_amount.TabIndex = 30
        Me.n_total_amount.Text = "0"
        Me.n_total_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(182, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Total "
        '
        'n_loan_takedown_amount
        '
        Me.n_loan_takedown_amount.AutoSize = True
        Me.n_loan_takedown_amount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.n_loan_takedown_amount.ForeColor = System.Drawing.Color.Black
        Me.n_loan_takedown_amount.Location = New System.Drawing.Point(301, 65)
        Me.n_loan_takedown_amount.Name = "n_loan_takedown_amount"
        Me.n_loan_takedown_amount.Size = New System.Drawing.Size(13, 13)
        Me.n_loan_takedown_amount.TabIndex = 28
        Me.n_loan_takedown_amount.Text = "0"
        Me.n_loan_takedown_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(244, 65)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(43, 13)
        Me.Label24.TabIndex = 27
        Me.Label24.Text = "Amount"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(182, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Rows"
        '
        'n_loan_takedown
        '
        Me.n_loan_takedown.AutoSize = True
        Me.n_loan_takedown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.n_loan_takedown.ForeColor = System.Drawing.Color.Black
        Me.n_loan_takedown.Location = New System.Drawing.Point(106, 64)
        Me.n_loan_takedown.Name = "n_loan_takedown"
        Me.n_loan_takedown.Size = New System.Drawing.Size(13, 13)
        Me.n_loan_takedown.TabIndex = 25
        Me.n_loan_takedown.Text = "0"
        Me.n_loan_takedown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(10, 65)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(85, 13)
        Me.Label22.TabIndex = 24
        Me.Label22.Text = "Loan Takedown"
        '
        'n_loan_final_amount
        '
        Me.n_loan_final_amount.AutoSize = True
        Me.n_loan_final_amount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.n_loan_final_amount.ForeColor = System.Drawing.Color.Black
        Me.n_loan_final_amount.Location = New System.Drawing.Point(301, 30)
        Me.n_loan_final_amount.Name = "n_loan_final_amount"
        Me.n_loan_final_amount.Size = New System.Drawing.Size(13, 13)
        Me.n_loan_final_amount.TabIndex = 23
        Me.n_loan_final_amount.Text = "0"
        Me.n_loan_final_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(244, 30)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(43, 13)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "Amount"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(182, 30)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Rows"
        '
        'n_loan_final
        '
        Me.n_loan_final.AutoSize = True
        Me.n_loan_final.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.n_loan_final.ForeColor = System.Drawing.Color.Black
        Me.n_loan_final.Location = New System.Drawing.Point(106, 30)
        Me.n_loan_final.Name = "n_loan_final"
        Me.n_loan_final.Size = New System.Drawing.Size(13, 13)
        Me.n_loan_final.TabIndex = 20
        Me.n_loan_final.Text = "0"
        Me.n_loan_final.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 30)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(56, 13)
        Me.Label19.TabIndex = 19
        Me.Label19.Text = "Loan Final"
        '
        'SummaryLoan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(682, 143)
        Me.Controls.Add(Me.txt_product)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "SummaryLoan"
        Me.Text = "Summary Loan"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents n_cif_final As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_product As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents n_no_cif_gagal As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents n_loan_takedown_amount As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents n_loan_takedown As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents n_loan_final_amount As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents n_loan_final As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents n_total_loan As System.Windows.Forms.Label
    Friend WithEvents n_total_amount As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
