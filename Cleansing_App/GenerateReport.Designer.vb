<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GenerateReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btn_import_approved = New System.Windows.Forms.Button()
        Me.btn_generate = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cb_report = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cb_product = New System.Windows.Forms.ComboBox()
        Me.lb_test = New System.Windows.Forms.Label()
        Me.cb_export = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.C_data4 = New System.Windows.Forms.Label()
        Me.btn_import_loan = New System.Windows.Forms.Button()
        Me.l_loan = New System.Windows.Forms.Label()
        Me.l_loan2361 = New System.Windows.Forms.Label()
        Me.C_data1 = New System.Windows.Forms.Label()
        Me.C_data3 = New System.Windows.Forms.Label()
        Me.l_ciftakeout = New System.Windows.Forms.Label()
        Me.C_data2 = New System.Windows.Forms.Label()
        Me.l_loanfinal = New System.Windows.Forms.Label()
        Me.btn_import_loan_final = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btn_save_balance = New System.Windows.Forms.Button()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.tabcontrol1 = New System.Windows.Forms.TabControl()
        Me.tb_report0 = New System.Windows.Forms.TabPage()
        Me.T_Rows1 = New System.Windows.Forms.Label()
        Me.load_result_report = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tb_report1 = New System.Windows.Forms.TabPage()
        Me.T_Rows2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.load_result_report2 = New System.Windows.Forms.DataGridView()
        Me.C_rows1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cb_takedown = New System.Windows.Forms.ComboBox()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.tabcontrol1.SuspendLayout()
        Me.tb_report0.SuspendLayout()
        CType(Me.load_result_report, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_report1.SuspendLayout()
        CType(Me.load_result_report2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btn_import_approved
        '
        Me.btn_import_approved.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_approved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_approved.Location = New System.Drawing.Point(391, 69)
        Me.btn_import_approved.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_approved.Name = "btn_import_approved"
        Me.btn_import_approved.Size = New System.Drawing.Size(77, 30)
        Me.btn_import_approved.TabIndex = 1
        Me.btn_import_approved.Text = "Import"
        Me.btn_import_approved.UseVisualStyleBackColor = False
        '
        'btn_generate
        '
        Me.btn_generate.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_generate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_generate.Location = New System.Drawing.Point(32, 27)
        Me.btn_generate.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_generate.Name = "btn_generate"
        Me.btn_generate.Size = New System.Drawing.Size(85, 31)
        Me.btn_generate.TabIndex = 0
        Me.btn_generate.Text = "Generate "
        Me.btn_generate.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cb_report)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cb_product)
        Me.GroupBox2.Controls.Add(Me.lb_test)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(333, 106)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Type"
        '
        'cb_report
        '
        Me.cb_report.FormattingEnabled = True
        Me.cb_report.Items.AddRange(New Object() {"[1] Report Reject", "[2] Report Early Payment", "[3] Report Reguler Payment", "[4] Report Cancel", "[5] Report Buyback", "[6] Report Partial", "[7] Report Data Ostanding"})
        Me.cb_report.Location = New System.Drawing.Point(103, 57)
        Me.cb_report.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_report.Name = "cb_report"
        Me.cb_report.Size = New System.Drawing.Size(181, 24)
        Me.cb_report.TabIndex = 3
        Me.cb_report.Text = "--Choose Report--"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(23, 62)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Report"
        '
        'cb_product
        '
        Me.cb_product.FormattingEnabled = True
        Me.cb_product.Items.AddRange(New Object() {"--Choose Product--"})
        Me.cb_product.Location = New System.Drawing.Point(104, 25)
        Me.cb_product.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_product.Name = "cb_product"
        Me.cb_product.Size = New System.Drawing.Size(180, 24)
        Me.cb_product.TabIndex = 2
        '
        'lb_test
        '
        Me.lb_test.AutoSize = True
        Me.lb_test.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_test.Location = New System.Drawing.Point(23, 30)
        Me.lb_test.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_test.Name = "lb_test"
        Me.lb_test.Size = New System.Drawing.Size(57, 17)
        Me.lb_test.TabIndex = 5
        Me.lb_test.Text = "Product"
        '
        'cb_export
        '
        Me.cb_export.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cb_export.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_export.ForeColor = System.Drawing.Color.Black
        Me.cb_export.FormattingEnabled = True
        Me.cb_export.Items.AddRange(New Object() {"Export", "Excel (.xlsx)", "TXT (.txt)"})
        Me.cb_export.Location = New System.Drawing.Point(158, 32)
        Me.cb_export.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_export.Name = "cb_export"
        Me.cb_export.Size = New System.Drawing.Size(75, 24)
        Me.cb_export.TabIndex = 19
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cb_takedown)
        Me.GroupBox1.Controls.Add(Me.C_data4)
        Me.GroupBox1.Controls.Add(Me.btn_import_loan)
        Me.GroupBox1.Controls.Add(Me.btn_import_approved)
        Me.GroupBox1.Controls.Add(Me.l_loan)
        Me.GroupBox1.Controls.Add(Me.l_loan2361)
        Me.GroupBox1.Controls.Add(Me.C_data1)
        Me.GroupBox1.Controls.Add(Me.C_data3)
        Me.GroupBox1.Controls.Add(Me.l_ciftakeout)
        Me.GroupBox1.Controls.Add(Me.C_data2)
        Me.GroupBox1.Controls.Add(Me.l_loanfinal)
        Me.GroupBox1.Controls.Add(Me.btn_import_loan_final)
        Me.GroupBox1.Location = New System.Drawing.Point(353, 11)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(534, 106)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "File Upload"
        '
        'C_data4
        '
        Me.C_data4.AutoSize = True
        Me.C_data4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data4.ForeColor = System.Drawing.Color.Green
        Me.C_data4.Location = New System.Drawing.Point(479, 31)
        Me.C_data4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data4.Name = "C_data4"
        Me.C_data4.Size = New System.Drawing.Size(17, 15)
        Me.C_data4.TabIndex = 21
        Me.C_data4.Text = "()"
        '
        'btn_import_loan
        '
        Me.btn_import_loan.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_loan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_loan.Location = New System.Drawing.Point(392, 25)
        Me.btn_import_loan.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_loan.Name = "btn_import_loan"
        Me.btn_import_loan.Size = New System.Drawing.Size(77, 30)
        Me.btn_import_loan.TabIndex = 20
        Me.btn_import_loan.Text = "Import"
        Me.btn_import_loan.UseVisualStyleBackColor = False
        '
        'l_loan
        '
        Me.l_loan.AutoSize = True
        Me.l_loan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_loan.Location = New System.Drawing.Point(273, 31)
        Me.l_loan.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.l_loan.Name = "l_loan"
        Me.l_loan.Size = New System.Drawing.Size(40, 17)
        Me.l_loan.TabIndex = 19
        Me.l_loan.Text = "Loan"
        '
        'l_loan2361
        '
        Me.l_loan2361.AutoSize = True
        Me.l_loan2361.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_loan2361.Location = New System.Drawing.Point(273, 74)
        Me.l_loan2361.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.l_loan2361.Name = "l_loan2361"
        Me.l_loan2361.Size = New System.Drawing.Size(88, 17)
        Me.l_loan2361.TabIndex = 7
        Me.l_loan2361.Text = "2361 Query*"
        '
        'C_data1
        '
        Me.C_data1.AutoSize = True
        Me.C_data1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data1.ForeColor = System.Drawing.Color.Green
        Me.C_data1.Location = New System.Drawing.Point(476, 75)
        Me.C_data1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data1.Name = "C_data1"
        Me.C_data1.Size = New System.Drawing.Size(17, 15)
        Me.C_data1.TabIndex = 13
        Me.C_data1.Text = "()"
        '
        'C_data3
        '
        Me.C_data3.AutoSize = True
        Me.C_data3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data3.ForeColor = System.Drawing.Color.Green
        Me.C_data3.Location = New System.Drawing.Point(219, 31)
        Me.C_data3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data3.Name = "C_data3"
        Me.C_data3.Size = New System.Drawing.Size(17, 15)
        Me.C_data3.TabIndex = 18
        Me.C_data3.Text = "()"
        '
        'l_ciftakeout
        '
        Me.l_ciftakeout.AutoSize = True
        Me.l_ciftakeout.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_ciftakeout.Location = New System.Drawing.Point(19, 32)
        Me.l_ciftakeout.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.l_ciftakeout.Name = "l_ciftakeout"
        Me.l_ciftakeout.Size = New System.Drawing.Size(84, 17)
        Me.l_ciftakeout.TabIndex = 15
        Me.l_ciftakeout.Text = "CIF Takeout"
        '
        'C_data2
        '
        Me.C_data2.AutoSize = True
        Me.C_data2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data2.ForeColor = System.Drawing.Color.Green
        Me.C_data2.Location = New System.Drawing.Point(217, 75)
        Me.C_data2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data2.Name = "C_data2"
        Me.C_data2.Size = New System.Drawing.Size(17, 15)
        Me.C_data2.TabIndex = 14
        Me.C_data2.Text = "()"
        '
        'l_loanfinal
        '
        Me.l_loanfinal.AutoSize = True
        Me.l_loanfinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_loanfinal.Location = New System.Drawing.Point(19, 74)
        Me.l_loanfinal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.l_loanfinal.Name = "l_loanfinal"
        Me.l_loanfinal.Size = New System.Drawing.Size(79, 17)
        Me.l_loanfinal.TabIndex = 11
        Me.l_loanfinal.Text = "Loan Final*"
        '
        'btn_import_loan_final
        '
        Me.btn_import_loan_final.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_loan_final.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_loan_final.Location = New System.Drawing.Point(136, 68)
        Me.btn_import_loan_final.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_loan_final.Name = "btn_import_loan_final"
        Me.btn_import_loan_final.Size = New System.Drawing.Size(77, 30)
        Me.btn_import_loan_final.TabIndex = 2
        Me.btn_import_loan_final.Text = "Import"
        Me.btn_import_loan_final.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel2.Controls.Add(Me.GroupBox4)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(1, 46)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1159, 129)
        Me.Panel2.TabIndex = 9
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btn_save_balance)
        Me.GroupBox4.Controls.Add(Me.cb_export)
        Me.GroupBox4.Controls.Add(Me.btn_generate)
        Me.GroupBox4.Location = New System.Drawing.Point(895, 11)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(252, 106)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Control"
        '
        'btn_save_balance
        '
        Me.btn_save_balance.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_save_balance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save_balance.Location = New System.Drawing.Point(32, 65)
        Me.btn_save_balance.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_save_balance.Name = "btn_save_balance"
        Me.btn_save_balance.Size = New System.Drawing.Size(85, 31)
        Me.btn_save_balance.TabIndex = 20
        Me.btn_save_balance.Text = "Save"
        Me.btn_save_balance.UseVisualStyleBackColor = False
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.FileName = "OpenFileDialog1"
        '
        'tabcontrol1
        '
        Me.tabcontrol1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabcontrol1.Controls.Add(Me.tb_report0)
        Me.tabcontrol1.Controls.Add(Me.tb_report1)
        Me.tabcontrol1.Location = New System.Drawing.Point(9, 21)
        Me.tabcontrol1.Margin = New System.Windows.Forms.Padding(4)
        Me.tabcontrol1.Name = "tabcontrol1"
        Me.tabcontrol1.SelectedIndex = 0
        Me.tabcontrol1.Size = New System.Drawing.Size(1125, 481)
        Me.tabcontrol1.TabIndex = 10
        '
        'tb_report0
        '
        Me.tb_report0.Controls.Add(Me.T_Rows1)
        Me.tb_report0.Controls.Add(Me.load_result_report)
        Me.tb_report0.Controls.Add(Me.Label7)
        Me.tb_report0.Location = New System.Drawing.Point(4, 25)
        Me.tb_report0.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_report0.Name = "tb_report0"
        Me.tb_report0.Size = New System.Drawing.Size(1117, 452)
        Me.tb_report0.TabIndex = 4
        Me.tb_report0.Text = "Result Report 1"
        Me.tb_report0.UseVisualStyleBackColor = True
        '
        'T_Rows1
        '
        Me.T_Rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.T_Rows1.AutoSize = True
        Me.T_Rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.T_Rows1.Location = New System.Drawing.Point(1063, 424)
        Me.T_Rows1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.T_Rows1.Name = "T_Rows1"
        Me.T_Rows1.Size = New System.Drawing.Size(16, 17)
        Me.T_Rows1.TabIndex = 19
        Me.T_Rows1.Text = "0"
        Me.T_Rows1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'load_result_report
        '
        Me.load_result_report.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_result_report.BackgroundColor = System.Drawing.Color.Silver
        Me.load_result_report.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_result_report.Location = New System.Drawing.Point(3, 4)
        Me.load_result_report.Margin = New System.Windows.Forms.Padding(4)
        Me.load_result_report.Name = "load_result_report"
        Me.load_result_report.ReadOnly = True
        Me.load_result_report.RowHeadersWidth = 51
        Me.load_result_report.Size = New System.Drawing.Size(1111, 416)
        Me.load_result_report.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1004, 424)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 17)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Rows :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tb_report1
        '
        Me.tb_report1.Controls.Add(Me.T_Rows2)
        Me.tb_report1.Controls.Add(Me.Label8)
        Me.tb_report1.Controls.Add(Me.load_result_report2)
        Me.tb_report1.Location = New System.Drawing.Point(4, 25)
        Me.tb_report1.Name = "tb_report1"
        Me.tb_report1.Size = New System.Drawing.Size(1117, 452)
        Me.tb_report1.TabIndex = 5
        Me.tb_report1.Text = "Result Report 2"
        Me.tb_report1.UseVisualStyleBackColor = True
        '
        'T_Rows2
        '
        Me.T_Rows2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.T_Rows2.AutoSize = True
        Me.T_Rows2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.T_Rows2.Location = New System.Drawing.Point(1063, 424)
        Me.T_Rows2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.T_Rows2.Name = "T_Rows2"
        Me.T_Rows2.Size = New System.Drawing.Size(16, 17)
        Me.T_Rows2.TabIndex = 26
        Me.T_Rows2.Text = "0"
        Me.T_Rows2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1004, 424)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 17)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Rows :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'load_result_report2
        '
        Me.load_result_report2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_result_report2.BackgroundColor = System.Drawing.Color.Silver
        Me.load_result_report2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_result_report2.Location = New System.Drawing.Point(3, 4)
        Me.load_result_report2.Margin = New System.Windows.Forms.Padding(4)
        Me.load_result_report2.Name = "load_result_report2"
        Me.load_result_report2.ReadOnly = True
        Me.load_result_report2.RowHeadersWidth = 51
        Me.load_result_report2.Size = New System.Drawing.Size(1111, 416)
        Me.load_result_report2.TabIndex = 2
        '
        'C_rows1
        '
        Me.C_rows1.Location = New System.Drawing.Point(0, 0)
        Me.C_rows1.Name = "C_rows1"
        Me.C_rows1.Size = New System.Drawing.Size(100, 23)
        Me.C_rows1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(2256, 44)
        Me.Panel1.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 9)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(170, 25)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Generate Report"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.tabcontrol1)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 179)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(1145, 509)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Output"
        '
        'cb_takedown
        '
        Me.cb_takedown.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.cb_takedown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_takedown.FormattingEnabled = True
        Me.cb_takedown.Location = New System.Drawing.Point(136, 30)
        Me.cb_takedown.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_takedown.Name = "cb_takedown"
        Me.cb_takedown.Size = New System.Drawing.Size(75, 24)
        Me.cb_takedown.TabIndex = 26
        '
        'GenerateReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1161, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "GenerateReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[6] Generate Report"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.tabcontrol1.ResumeLayout(False)
        Me.tb_report0.ResumeLayout(False)
        Me.tb_report0.PerformLayout()
        CType(Me.load_result_report, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_report1.ResumeLayout(False)
        Me.tb_report1.PerformLayout()
        CType(Me.load_result_report2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btn_import_approved As System.Windows.Forms.Button
    Friend WithEvents btn_generate As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_product As System.Windows.Forms.ComboBox
    Friend WithEvents lb_test As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents C_data2 As System.Windows.Forms.Label
    Friend WithEvents C_data1 As System.Windows.Forms.Label
    Friend WithEvents l_loanfinal As System.Windows.Forms.Label
    Friend WithEvents l_loan2361 As System.Windows.Forms.Label
    Friend WithEvents btn_import_loan_final As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents OpenFileDialog2 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tabcontrol1 As System.Windows.Forms.TabControl
    Friend WithEvents C_data3 As System.Windows.Forms.Label
    Friend WithEvents l_ciftakeout As System.Windows.Forms.Label
    Friend WithEvents cb_report As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tb_report0 As System.Windows.Forms.TabPage
    Friend WithEvents load_result_report As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_export As ComboBox
    Friend WithEvents C_data4 As System.Windows.Forms.Label
    Friend WithEvents btn_import_loan As System.Windows.Forms.Button
    Friend WithEvents l_loan As System.Windows.Forms.Label
    Friend WithEvents C_rows1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents tb_report1 As TabPage
    Friend WithEvents load_result_report2 As DataGridView
    Friend WithEvents btn_save_balance As Button
    Friend WithEvents T_Rows2 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents T_Rows1 As Label
    Friend WithEvents cb_takedown As ComboBox
End Class
