<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reconsile_Payment
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cb_payment = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_product = New System.Windows.Forms.ComboBox()
        Me.lb_test = New System.Windows.Forms.Label()
        Me.cb_export = New System.Windows.Forms.ComboBox()
        Me.btn_pmt = New System.Windows.Forms.Button()
        Me.btn_checking = New System.Windows.Forms.Button()
        Me.txt_checking_load = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.C_rows1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btn_import_ostanding = New System.Windows.Forms.Button()
        Me.load_payment = New System.Windows.Forms.DataGridView()
        Me.tb_payment_process = New System.Windows.Forms.TabPage()
        Me.C_rows2 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.load_payment_process = New System.Windows.Forms.DataGridView()
        Me.tb_payment_notprocess = New System.Windows.Forms.TabPage()
        Me.load_payment_notprocess = New System.Windows.Forms.DataGridView()
        Me.C_rows3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_coa_balance = New System.Windows.Forms.Button()
        Me.l_payment4 = New System.Windows.Forms.Label()
        Me.C_data4 = New System.Windows.Forms.Label()
        Me.cb_import_dt = New System.Windows.Forms.ComboBox()
        Me.C_data3 = New System.Windows.Forms.Label()
        Me.l_payment3 = New System.Windows.Forms.Label()
        Me.C_data2 = New System.Windows.Forms.Label()
        Me.C_data1 = New System.Windows.Forms.Label()
        Me.btn_import_payment = New System.Windows.Forms.Button()
        Me.l_payment2 = New System.Windows.Forms.Label()
        Me.l_payment1 = New System.Windows.Forms.Label()
        Me.tb_payment_load = New System.Windows.Forms.TabPage()
        Me.tabcontrol1 = New System.Windows.Forms.TabControl()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ct_control2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.load_payment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_payment_process.SuspendLayout()
        CType(Me.load_payment_process, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_payment_notprocess.SuspendLayout()
        CType(Me.load_payment_notprocess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.tb_payment_load.SuspendLayout()
        Me.tabcontrol1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.ct_control2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cb_payment)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cb_product)
        Me.GroupBox2.Controls.Add(Me.lb_test)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 11)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(292, 87)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Type"
        '
        'cb_payment
        '
        Me.cb_payment.FormattingEnabled = True
        Me.cb_payment.Items.AddRange(New Object() {"--Choose Payment--", "[1] Regular with 1410 File", "[2] Regular Payment with DT", "[3] Early Termination ", "[4] Early Termination with DT", "[5] Buyback with DT (2month)", "[6] Buyback with DT (File)", "[7] Regular Overdue"})
        Me.cb_payment.Location = New System.Drawing.Point(101, 53)
        Me.cb_payment.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_payment.Name = "cb_payment"
        Me.cb_payment.Size = New System.Drawing.Size(170, 24)
        Me.cb_payment.TabIndex = 6
        Me.cb_payment.Text = "--Choose Payment--"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 55)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 17)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Payment"
        '
        'cb_product
        '
        Me.cb_product.FormattingEnabled = True
        Me.cb_product.Items.AddRange(New Object() {"--Choose Product--"})
        Me.cb_product.Location = New System.Drawing.Point(101, 22)
        Me.cb_product.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_product.Name = "cb_product"
        Me.cb_product.Size = New System.Drawing.Size(170, 24)
        Me.cb_product.TabIndex = 2
        Me.cb_product.Text = "--Choose Product--"
        '
        'lb_test
        '
        Me.lb_test.AutoSize = True
        Me.lb_test.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_test.Location = New System.Drawing.Point(13, 26)
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
        Me.cb_export.Items.AddRange(New Object() {"Export", "Excel (.xls)", "TXT (.txt)"})
        Me.cb_export.Location = New System.Drawing.Point(129, 38)
        Me.cb_export.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_export.Name = "cb_export"
        Me.cb_export.Size = New System.Drawing.Size(76, 24)
        Me.cb_export.TabIndex = 11
        '
        'btn_pmt
        '
        Me.btn_pmt.AutoSize = True
        Me.btn_pmt.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_pmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_pmt.Location = New System.Drawing.Point(216, 36)
        Me.btn_pmt.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_pmt.Name = "btn_pmt"
        Me.btn_pmt.Size = New System.Drawing.Size(77, 29)
        Me.btn_pmt.TabIndex = 20
        Me.btn_pmt.Text = "PMT"
        Me.btn_pmt.UseVisualStyleBackColor = False
        '
        'btn_checking
        '
        Me.btn_checking.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_checking.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_checking.ForeColor = System.Drawing.Color.White
        Me.btn_checking.Location = New System.Drawing.Point(25, 36)
        Me.btn_checking.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_checking.Name = "btn_checking"
        Me.btn_checking.Size = New System.Drawing.Size(85, 31)
        Me.btn_checking.TabIndex = 0
        Me.btn_checking.Text = "Checking"
        Me.btn_checking.UseVisualStyleBackColor = False
        '
        'txt_checking_load
        '
        Me.txt_checking_load.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_checking_load.AutoSize = True
        Me.txt_checking_load.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_checking_load.Location = New System.Drawing.Point(97, 655)
        Me.txt_checking_load.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txt_checking_load.Name = "txt_checking_load"
        Me.txt_checking_load.Size = New System.Drawing.Size(16, 17)
        Me.txt_checking_load.TabIndex = 19
        Me.txt_checking_load.Text = "0"
        Me.txt_checking_load.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 655)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 17)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Checking : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C_rows1
        '
        Me.C_rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows1.AutoSize = True
        Me.C_rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows1.Location = New System.Drawing.Point(1423, 656)
        Me.C_rows1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows1.Name = "C_rows1"
        Me.C_rows1.Size = New System.Drawing.Size(16, 17)
        Me.C_rows1.TabIndex = 15
        Me.C_rows1.Text = "0"
        Me.C_rows1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1365, 656)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 17)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Rows :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_import_ostanding
        '
        Me.btn_import_ostanding.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_ostanding.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_ostanding.Location = New System.Drawing.Point(147, 53)
        Me.btn_import_ostanding.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_ostanding.Name = "btn_import_ostanding"
        Me.btn_import_ostanding.Size = New System.Drawing.Size(85, 30)
        Me.btn_import_ostanding.TabIndex = 1
        Me.btn_import_ostanding.Text = "Import"
        Me.btn_import_ostanding.UseVisualStyleBackColor = False
        '
        'load_payment
        '
        Me.load_payment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_payment.BackgroundColor = System.Drawing.Color.Silver
        Me.load_payment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_payment.Location = New System.Drawing.Point(3, 4)
        Me.load_payment.Margin = New System.Windows.Forms.Padding(4)
        Me.load_payment.Name = "load_payment"
        Me.load_payment.ReadOnly = True
        Me.load_payment.RowHeadersWidth = 51
        Me.load_payment.Size = New System.Drawing.Size(1499, 647)
        Me.load_payment.TabIndex = 0
        '
        'tb_payment_process
        '
        Me.tb_payment_process.Controls.Add(Me.C_rows2)
        Me.tb_payment_process.Controls.Add(Me.Label2)
        Me.tb_payment_process.Controls.Add(Me.load_payment_process)
        Me.tb_payment_process.Location = New System.Drawing.Point(4, 25)
        Me.tb_payment_process.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_payment_process.Name = "tb_payment_process"
        Me.tb_payment_process.Size = New System.Drawing.Size(1506, 685)
        Me.tb_payment_process.TabIndex = 3
        Me.tb_payment_process.Text = "Payment Process"
        Me.tb_payment_process.UseVisualStyleBackColor = True
        '
        'C_rows2
        '
        Me.C_rows2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows2.AutoSize = True
        Me.C_rows2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows2.Location = New System.Drawing.Point(1401, 656)
        Me.C_rows2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows2.Name = "C_rows2"
        Me.C_rows2.Size = New System.Drawing.Size(16, 17)
        Me.C_rows2.TabIndex = 19
        Me.C_rows2.Text = "0"
        Me.C_rows2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1343, 656)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 17)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Rows :"
        '
        'load_payment_process
        '
        Me.load_payment_process.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_payment_process.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_payment_process.Location = New System.Drawing.Point(4, 4)
        Me.load_payment_process.Margin = New System.Windows.Forms.Padding(4)
        Me.load_payment_process.Name = "load_payment_process"
        Me.load_payment_process.RowHeadersWidth = 51
        Me.load_payment_process.Size = New System.Drawing.Size(1498, 647)
        Me.load_payment_process.TabIndex = 17
        '
        'tb_payment_notprocess
        '
        Me.tb_payment_notprocess.Controls.Add(Me.load_payment_notprocess)
        Me.tb_payment_notprocess.Controls.Add(Me.C_rows3)
        Me.tb_payment_notprocess.Controls.Add(Me.Label6)
        Me.tb_payment_notprocess.Location = New System.Drawing.Point(4, 25)
        Me.tb_payment_notprocess.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_payment_notprocess.Name = "tb_payment_notprocess"
        Me.tb_payment_notprocess.Size = New System.Drawing.Size(1506, 685)
        Me.tb_payment_notprocess.TabIndex = 2
        Me.tb_payment_notprocess.Text = "Payment Not Process"
        Me.tb_payment_notprocess.UseVisualStyleBackColor = True
        '
        'load_payment_notprocess
        '
        Me.load_payment_notprocess.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_payment_notprocess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_payment_notprocess.Location = New System.Drawing.Point(5, 4)
        Me.load_payment_notprocess.Margin = New System.Windows.Forms.Padding(4)
        Me.load_payment_notprocess.Name = "load_payment_notprocess"
        Me.load_payment_notprocess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.load_payment_notprocess.RowHeadersWidth = 51
        Me.load_payment_notprocess.Size = New System.Drawing.Size(1497, 649)
        Me.load_payment_notprocess.TabIndex = 19
        '
        'C_rows3
        '
        Me.C_rows3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows3.AutoSize = True
        Me.C_rows3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows3.Location = New System.Drawing.Point(1401, 656)
        Me.C_rows3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows3.Name = "C_rows3"
        Me.C_rows3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.C_rows3.Size = New System.Drawing.Size(16, 17)
        Me.C_rows3.TabIndex = 18
        Me.C_rows3.Text = "0"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1343, 656)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(50, 17)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Rows :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_coa_balance)
        Me.GroupBox1.Controls.Add(Me.l_payment4)
        Me.GroupBox1.Controls.Add(Me.C_data4)
        Me.GroupBox1.Controls.Add(Me.cb_import_dt)
        Me.GroupBox1.Controls.Add(Me.C_data3)
        Me.GroupBox1.Controls.Add(Me.l_payment3)
        Me.GroupBox1.Controls.Add(Me.C_data2)
        Me.GroupBox1.Controls.Add(Me.btn_import_ostanding)
        Me.GroupBox1.Controls.Add(Me.C_data1)
        Me.GroupBox1.Controls.Add(Me.btn_import_payment)
        Me.GroupBox1.Controls.Add(Me.l_payment2)
        Me.GroupBox1.Controls.Add(Me.l_payment1)
        Me.GroupBox1.Location = New System.Drawing.Point(315, 11)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(582, 87)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "File Upload"
        '
        'btn_coa_balance
        '
        Me.btn_coa_balance.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_coa_balance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_coa_balance.Location = New System.Drawing.Point(422, 53)
        Me.btn_coa_balance.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_coa_balance.Name = "btn_coa_balance"
        Me.btn_coa_balance.Size = New System.Drawing.Size(77, 30)
        Me.btn_coa_balance.TabIndex = 26
        Me.btn_coa_balance.Text = "Import"
        Me.btn_coa_balance.UseVisualStyleBackColor = False
        '
        'l_payment4
        '
        Me.l_payment4.AutoSize = True
        Me.l_payment4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_payment4.Location = New System.Drawing.Point(305, 58)
        Me.l_payment4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.l_payment4.Name = "l_payment4"
        Me.l_payment4.Size = New System.Drawing.Size(92, 17)
        Me.l_payment4.TabIndex = 27
        Me.l_payment4.Text = "COA Balance"
        '
        'C_data4
        '
        Me.C_data4.AutoSize = True
        Me.C_data4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data4.ForeColor = System.Drawing.Color.Green
        Me.C_data4.Location = New System.Drawing.Point(511, 59)
        Me.C_data4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data4.Name = "C_data4"
        Me.C_data4.Size = New System.Drawing.Size(36, 15)
        Me.C_data4.TabIndex = 28
        Me.C_data4.Text = "(OK)"
        '
        'cb_import_dt
        '
        Me.cb_import_dt.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.cb_import_dt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_import_dt.FormattingEnabled = True
        Me.cb_import_dt.Items.AddRange(New Object() {"Import", "9850 File", "DT File"})
        Me.cb_import_dt.Location = New System.Drawing.Point(423, 23)
        Me.cb_import_dt.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_import_dt.Name = "cb_import_dt"
        Me.cb_import_dt.Size = New System.Drawing.Size(77, 24)
        Me.cb_import_dt.TabIndex = 25
        '
        'C_data3
        '
        Me.C_data3.AutoSize = True
        Me.C_data3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data3.ForeColor = System.Drawing.Color.Green
        Me.C_data3.Location = New System.Drawing.Point(512, 27)
        Me.C_data3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data3.Name = "C_data3"
        Me.C_data3.Size = New System.Drawing.Size(36, 15)
        Me.C_data3.TabIndex = 24
        Me.C_data3.Text = "(OK)"
        '
        'l_payment3
        '
        Me.l_payment3.AutoSize = True
        Me.l_payment3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_payment3.Location = New System.Drawing.Point(302, 25)
        Me.l_payment3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.l_payment3.Name = "l_payment3"
        Me.l_payment3.Size = New System.Drawing.Size(93, 17)
        Me.l_payment3.TabIndex = 23
        Me.l_payment3.Text = "DT /9850 File"
        '
        'C_data2
        '
        Me.C_data2.AutoSize = True
        Me.C_data2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data2.ForeColor = System.Drawing.Color.Green
        Me.C_data2.Location = New System.Drawing.Point(235, 27)
        Me.C_data2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data2.Name = "C_data2"
        Me.C_data2.Size = New System.Drawing.Size(36, 15)
        Me.C_data2.TabIndex = 21
        Me.C_data2.Text = "(OK)"
        '
        'C_data1
        '
        Me.C_data1.AutoSize = True
        Me.C_data1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data1.ForeColor = System.Drawing.Color.Green
        Me.C_data1.Location = New System.Drawing.Point(235, 59)
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
        Me.btn_import_payment.Location = New System.Drawing.Point(147, 21)
        Me.btn_import_payment.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_payment.Name = "btn_import_payment"
        Me.btn_import_payment.Size = New System.Drawing.Size(85, 30)
        Me.btn_import_payment.TabIndex = 20
        Me.btn_import_payment.Text = "Import"
        Me.btn_import_payment.UseVisualStyleBackColor = False
        '
        'l_payment2
        '
        Me.l_payment2.AutoSize = True
        Me.l_payment2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_payment2.Location = New System.Drawing.Point(13, 56)
        Me.l_payment2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.l_payment2.Name = "l_payment2"
        Me.l_payment2.Size = New System.Drawing.Size(66, 17)
        Me.l_payment2.TabIndex = 8
        Me.l_payment2.Text = "1410 File"
        '
        'l_payment1
        '
        Me.l_payment1.AutoSize = True
        Me.l_payment1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_payment1.Location = New System.Drawing.Point(13, 24)
        Me.l_payment1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.l_payment1.Name = "l_payment1"
        Me.l_payment1.Size = New System.Drawing.Size(117, 17)
        Me.l_payment1.TabIndex = 9
        Me.l_payment1.Text = "Reguler Payment"
        '
        'tb_payment_load
        '
        Me.tb_payment_load.Controls.Add(Me.txt_checking_load)
        Me.tb_payment_load.Controls.Add(Me.Label5)
        Me.tb_payment_load.Controls.Add(Me.C_rows1)
        Me.tb_payment_load.Controls.Add(Me.Label12)
        Me.tb_payment_load.Controls.Add(Me.load_payment)
        Me.tb_payment_load.Location = New System.Drawing.Point(4, 25)
        Me.tb_payment_load.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_payment_load.Name = "tb_payment_load"
        Me.tb_payment_load.Padding = New System.Windows.Forms.Padding(4)
        Me.tb_payment_load.Size = New System.Drawing.Size(1506, 685)
        Me.tb_payment_load.TabIndex = 0
        Me.tb_payment_load.Text = "Payment Load"
        Me.tb_payment_load.UseVisualStyleBackColor = True
        '
        'tabcontrol1
        '
        Me.tabcontrol1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabcontrol1.Controls.Add(Me.tb_payment_load)
        Me.tabcontrol1.Controls.Add(Me.tb_payment_process)
        Me.tabcontrol1.Controls.Add(Me.tb_payment_notprocess)
        Me.tabcontrol1.Location = New System.Drawing.Point(8, 23)
        Me.tabcontrol1.Margin = New System.Windows.Forms.Padding(4)
        Me.tabcontrol1.Name = "tabcontrol1"
        Me.tabcontrol1.SelectedIndex = 0
        Me.tabcontrol1.Size = New System.Drawing.Size(1514, 714)
        Me.tabcontrol1.TabIndex = 22
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
        Me.Label8.Location = New System.Drawing.Point(5, 9)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(193, 25)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Checking Payment"
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
        Me.Panel1.Size = New System.Drawing.Size(1947, 44)
        Me.Panel1.TabIndex = 23
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel2.Controls.Add(Me.ct_control2)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(1, 48)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1575, 113)
        Me.Panel2.TabIndex = 21
        '
        'ct_control2
        '
        Me.ct_control2.Controls.Add(Me.cb_export)
        Me.ct_control2.Controls.Add(Me.btn_pmt)
        Me.ct_control2.Controls.Add(Me.btn_checking)
        Me.ct_control2.Location = New System.Drawing.Point(905, 11)
        Me.ct_control2.Margin = New System.Windows.Forms.Padding(4)
        Me.ct_control2.Name = "ct_control2"
        Me.ct_control2.Padding = New System.Windows.Forms.Padding(4)
        Me.ct_control2.Size = New System.Drawing.Size(313, 87)
        Me.ct_control2.TabIndex = 7
        Me.ct_control2.TabStop = False
        Me.ct_control2.Text = "Control"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.tabcontrol1)
        Me.GroupBox3.Location = New System.Drawing.Point(11, 166)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(1530, 745)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Output"
        '
        'Reconsile_Payment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1577, 911)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Reconsile_Payment"
        Me.Text = "[4] Checking Payment"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.load_payment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_payment_process.ResumeLayout(False)
        Me.tb_payment_process.PerformLayout()
        CType(Me.load_payment_process, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_payment_notprocess.ResumeLayout(False)
        Me.tb_payment_notprocess.PerformLayout()
        CType(Me.load_payment_notprocess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tb_payment_load.ResumeLayout(False)
        Me.tb_payment_load.PerformLayout()
        Me.tabcontrol1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ct_control2.ResumeLayout(False)
        Me.ct_control2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_export As System.Windows.Forms.ComboBox
    Friend WithEvents btn_pmt As System.Windows.Forms.Button
    Friend WithEvents cb_payment As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_product As System.Windows.Forms.ComboBox
    Friend WithEvents lb_test As System.Windows.Forms.Label
    Friend WithEvents btn_checking As System.Windows.Forms.Button
    Friend WithEvents txt_checking_load As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents C_rows1 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btn_import_ostanding As System.Windows.Forms.Button
    Friend WithEvents load_payment As System.Windows.Forms.DataGridView
    Friend WithEvents tb_payment_process As System.Windows.Forms.TabPage
    Friend WithEvents C_rows2 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents load_payment_process As System.Windows.Forms.DataGridView
    Friend WithEvents tb_payment_notprocess As System.Windows.Forms.TabPage
    Friend WithEvents load_payment_notprocess As System.Windows.Forms.DataGridView
    Friend WithEvents C_rows3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tb_payment_load As System.Windows.Forms.TabPage
    Friend WithEvents tabcontrol1 As System.Windows.Forms.TabControl
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ct_control2 As System.Windows.Forms.GroupBox
    Friend WithEvents l_payment1 As System.Windows.Forms.Label
    Friend WithEvents l_payment2 As System.Windows.Forms.Label
    Friend WithEvents C_data1 As System.Windows.Forms.Label
    Friend WithEvents C_data2 As System.Windows.Forms.Label
    Friend WithEvents btn_import_payment As System.Windows.Forms.Button
    Friend WithEvents C_data3 As Label
    Friend WithEvents l_payment3 As Label
    Friend WithEvents cb_import_dt As ComboBox
    Friend WithEvents btn_coa_balance As Button
    Friend WithEvents l_payment4 As Label
    Friend WithEvents C_data4 As Label
End Class
