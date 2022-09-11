<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PaymentDuedate
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_checking = New System.Windows.Forms.Button()
        Me.cb_export = New System.Windows.Forms.ComboBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.C_data2 = New System.Windows.Forms.Label()
        Me.btn_import_ostanding = New System.Windows.Forms.Button()
        Me.C_data1 = New System.Windows.Forms.Label()
        Me.btn_import_payment = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tabcontrol1 = New System.Windows.Forms.TabControl()
        Me.tb_report0 = New System.Windows.Forms.TabPage()
        Me.C_rows1 = New System.Windows.Forms.Label()
        Me.C_Cells1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.load_result = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tabcontrol1.SuspendLayout()
        Me.tb_report0.SuspendLayout()
        CType(Me.load_result, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_checking)
        Me.GroupBox1.Controls.Add(Me.cb_export)
        Me.GroupBox1.Location = New System.Drawing.Point(324, 10)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(239, 85)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Payment Checking"
        '
        'btn_checking
        '
        Me.btn_checking.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_checking.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btn_checking.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_checking.ForeColor = System.Drawing.Color.White
        Me.btn_checking.Location = New System.Drawing.Point(21, 30)
        Me.btn_checking.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_checking.Name = "btn_checking"
        Me.btn_checking.Size = New System.Drawing.Size(85, 31)
        Me.btn_checking.TabIndex = 15
        Me.btn_checking.Text = "Checking"
        Me.btn_checking.UseVisualStyleBackColor = False
        '
        'cb_export
        '
        Me.cb_export.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cb_export.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_export.ForeColor = System.Drawing.Color.Black
        Me.cb_export.FormattingEnabled = True
        Me.cb_export.Items.AddRange(New Object() {"Export", "Excel (.xlsx)", "TXT (.txt)"})
        Me.cb_export.Location = New System.Drawing.Point(134, 32)
        Me.cb_export.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_export.Name = "cb_export"
        Me.cb_export.Size = New System.Drawing.Size(76, 24)
        Me.cb_export.TabIndex = 12
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 9)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(280, 25)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Checking Duedate Payment"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(3, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1377, 44)
        Me.Panel1.TabIndex = 23
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(3, 48)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1280, 108)
        Me.Panel2.TabIndex = 21
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.C_data2)
        Me.GroupBox3.Controls.Add(Me.btn_import_ostanding)
        Me.GroupBox3.Controls.Add(Me.C_data1)
        Me.GroupBox3.Controls.Add(Me.btn_import_payment)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 7)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(304, 87)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "File Upload"
        '
        'C_data2
        '
        Me.C_data2.AutoSize = True
        Me.C_data2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data2.ForeColor = System.Drawing.Color.Green
        Me.C_data2.Location = New System.Drawing.Point(235, 22)
        Me.C_data2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data2.Name = "C_data2"
        Me.C_data2.Size = New System.Drawing.Size(36, 15)
        Me.C_data2.TabIndex = 21
        Me.C_data2.Text = "(OK)"
        '
        'btn_import_ostanding
        '
        Me.btn_import_ostanding.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_ostanding.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_ostanding.Location = New System.Drawing.Point(147, 52)
        Me.btn_import_ostanding.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_ostanding.Name = "btn_import_ostanding"
        Me.btn_import_ostanding.Size = New System.Drawing.Size(85, 30)
        Me.btn_import_ostanding.TabIndex = 1
        Me.btn_import_ostanding.Text = "Import"
        Me.btn_import_ostanding.UseVisualStyleBackColor = False
        '
        'C_data1
        '
        Me.C_data1.AutoSize = True
        Me.C_data1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data1.ForeColor = System.Drawing.Color.Green
        Me.C_data1.Location = New System.Drawing.Point(235, 57)
        Me.C_data1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data1.Name = "C_data1"
        Me.C_data1.Size = New System.Drawing.Size(36, 15)
        Me.C_data1.TabIndex = 19
        Me.C_data1.Text = "(OK)"
        '
        'btn_import_payment
        '
        Me.btn_import_payment.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_payment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_payment.Location = New System.Drawing.Point(147, 16)
        Me.btn_import_payment.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_payment.Name = "btn_import_payment"
        Me.btn_import_payment.Size = New System.Drawing.Size(85, 30)
        Me.btn_import_payment.TabIndex = 20
        Me.btn_import_payment.Text = "Import"
        Me.btn_import_payment.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 57)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Ostanding File"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 26)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 17)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Payment File"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tabcontrol1)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 164)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(1257, 498)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Output"
        '
        'tabcontrol1
        '
        Me.tabcontrol1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabcontrol1.Controls.Add(Me.tb_report0)
        Me.tabcontrol1.Location = New System.Drawing.Point(8, 23)
        Me.tabcontrol1.Margin = New System.Windows.Forms.Padding(4)
        Me.tabcontrol1.Name = "tabcontrol1"
        Me.tabcontrol1.SelectedIndex = 0
        Me.tabcontrol1.Size = New System.Drawing.Size(1241, 468)
        Me.tabcontrol1.TabIndex = 11
        '
        'tb_report0
        '
        Me.tb_report0.Controls.Add(Me.C_rows1)
        Me.tb_report0.Controls.Add(Me.C_Cells1)
        Me.tb_report0.Controls.Add(Me.Label5)
        Me.tb_report0.Controls.Add(Me.load_result)
        Me.tb_report0.Controls.Add(Me.Label7)
        Me.tb_report0.Location = New System.Drawing.Point(4, 25)
        Me.tb_report0.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_report0.Name = "tb_report0"
        Me.tb_report0.Size = New System.Drawing.Size(1233, 439)
        Me.tb_report0.TabIndex = 4
        Me.tb_report0.Text = "Result Report"
        Me.tb_report0.UseVisualStyleBackColor = True
        '
        'C_rows1
        '
        Me.C_rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows1.AutoSize = True
        Me.C_rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows1.Location = New System.Drawing.Point(1193, 412)
        Me.C_rows1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows1.Name = "C_rows1"
        Me.C_rows1.Size = New System.Drawing.Size(16, 17)
        Me.C_rows1.TabIndex = 22
        Me.C_rows1.Text = "0"
        Me.C_rows1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C_Cells1
        '
        Me.C_Cells1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_Cells1.AutoSize = True
        Me.C_Cells1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_Cells1.Location = New System.Drawing.Point(1104, 411)
        Me.C_Cells1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_Cells1.Name = "C_Cells1"
        Me.C_Cells1.Size = New System.Drawing.Size(16, 17)
        Me.C_Cells1.TabIndex = 21
        Me.C_Cells1.Text = "0"
        Me.C_Cells1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1048, 411)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 17)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Cells :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'load_result
        '
        Me.load_result.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_result.BackgroundColor = System.Drawing.Color.Silver
        Me.load_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_result.Location = New System.Drawing.Point(3, 4)
        Me.load_result.Margin = New System.Windows.Forms.Padding(4)
        Me.load_result.Name = "load_result"
        Me.load_result.ReadOnly = True
        Me.load_result.RowHeadersWidth = 51
        Me.load_result.Size = New System.Drawing.Size(1227, 402)
        Me.load_result.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1133, 411)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 17)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Rows :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PaymentDuedate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 677)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "PaymentDuedate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Checking Duedate Payment"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.tabcontrol1.ResumeLayout(False)
        Me.tb_report0.ResumeLayout(False)
        Me.tb_report0.PerformLayout()
        CType(Me.load_result, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents tabcontrol1 As System.Windows.Forms.TabControl
    Friend WithEvents tb_report0 As System.Windows.Forms.TabPage
    Friend WithEvents C_rows1 As System.Windows.Forms.Label
    Friend WithEvents C_Cells1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents load_result As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cb_export As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents C_data2 As System.Windows.Forms.Label
    Friend WithEvents btn_import_ostanding As System.Windows.Forms.Button
    Friend WithEvents C_data1 As System.Windows.Forms.Label
    Friend WithEvents btn_import_payment As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btn_checking As System.Windows.Forms.Button
End Class
