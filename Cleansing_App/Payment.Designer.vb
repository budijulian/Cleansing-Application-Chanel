<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Payment
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.load_payment_notprocess = New System.Windows.Forms.DataGridView()
        Me.C_rows3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.C_rows2 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.load_payment_process = New System.Windows.Forms.DataGridView()
        Me.txt_checking_load = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.C_Cells1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.C_rows1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tabcontrol1 = New System.Windows.Forms.TabControl()
        Me.tb_payment_load = New System.Windows.Forms.TabPage()
        Me.load_payment = New System.Windows.Forms.DataGridView()
        Me.tb_payment_process = New System.Windows.Forms.TabPage()
        Me.tb_payment_notprocess = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_summary = New System.Windows.Forms.Button()
        Me.cb_export = New System.Windows.Forms.ComboBox()
        Me.btn_balanced = New System.Windows.Forms.Button()
        Me.txt_interest = New System.Windows.Forms.TextBox()
        Me.btn_pmt = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cb_payment = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_product = New System.Windows.Forms.ComboBox()
        Me.lb_test = New System.Windows.Forms.Label()
        Me.btn_checking = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_import = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.load_payment_notprocess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.load_payment_process, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabcontrol1.SuspendLayout()
        Me.tb_payment_load.SuspendLayout()
        CType(Me.load_payment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_payment_process.SuspendLayout()
        Me.tb_payment_notprocess.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(1, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1019, 36)
        Me.Panel1.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(162, 20)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Reconsile Payment"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'load_payment_notprocess
        '
        Me.load_payment_notprocess.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_payment_notprocess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_payment_notprocess.Location = New System.Drawing.Point(0, 0)
        Me.load_payment_notprocess.Name = "load_payment_notprocess"
        Me.load_payment_notprocess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.load_payment_notprocess.Size = New System.Drawing.Size(959, 553)
        Me.load_payment_notprocess.TabIndex = 19
        '
        'C_rows3
        '
        Me.C_rows3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows3.AutoSize = True
        Me.C_rows3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows3.Location = New System.Drawing.Point(918, 556)
        Me.C_rows3.Name = "C_rows3"
        Me.C_rows3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.C_rows3.Size = New System.Drawing.Size(13, 13)
        Me.C_rows3.TabIndex = 18
        Me.C_rows3.Text = "0"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(875, 556)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Rows :"
        '
        'C_rows2
        '
        Me.C_rows2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows2.AutoSize = True
        Me.C_rows2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows2.Location = New System.Drawing.Point(918, 556)
        Me.C_rows2.Name = "C_rows2"
        Me.C_rows2.Size = New System.Drawing.Size(13, 13)
        Me.C_rows2.TabIndex = 19
        Me.C_rows2.Text = "0"
        Me.C_rows2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(875, 556)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Rows :"
        '
        'load_payment_process
        '
        Me.load_payment_process.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_payment_process.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_payment_process.Location = New System.Drawing.Point(0, 0)
        Me.load_payment_process.Name = "load_payment_process"
        Me.load_payment_process.Size = New System.Drawing.Size(959, 553)
        Me.load_payment_process.TabIndex = 17
        '
        'txt_checking_load
        '
        Me.txt_checking_load.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_checking_load.AutoSize = True
        Me.txt_checking_load.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_checking_load.Location = New System.Drawing.Point(73, 557)
        Me.txt_checking_load.Name = "txt_checking_load"
        Me.txt_checking_load.Size = New System.Drawing.Size(13, 13)
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
        Me.Label5.Location = New System.Drawing.Point(6, 556)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Checking : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C_Cells1
        '
        Me.C_Cells1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_Cells1.AutoSize = True
        Me.C_Cells1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_Cells1.Location = New System.Drawing.Point(853, 556)
        Me.C_Cells1.Name = "C_Cells1"
        Me.C_Cells1.Size = New System.Drawing.Size(13, 13)
        Me.C_Cells1.TabIndex = 17
        Me.C_Cells1.Text = "0"
        Me.C_Cells1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(811, 556)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Cells :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C_rows1
        '
        Me.C_rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows1.AutoSize = True
        Me.C_rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows1.Location = New System.Drawing.Point(918, 556)
        Me.C_rows1.Name = "C_rows1"
        Me.C_rows1.Size = New System.Drawing.Size(13, 13)
        Me.C_rows1.TabIndex = 15
        Me.C_rows1.Text = "0"
        Me.C_rows1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(875, 556)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 13)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Rows :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabcontrol1
        '
        Me.tabcontrol1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabcontrol1.Controls.Add(Me.tb_payment_load)
        Me.tabcontrol1.Controls.Add(Me.tb_payment_process)
        Me.tabcontrol1.Controls.Add(Me.tb_payment_notprocess)
        Me.tabcontrol1.Location = New System.Drawing.Point(0, 151)
        Me.tabcontrol1.Name = "tabcontrol1"
        Me.tabcontrol1.SelectedIndex = 0
        Me.tabcontrol1.Size = New System.Drawing.Size(966, 602)
        Me.tabcontrol1.TabIndex = 19
        '
        'tb_payment_load
        '
        Me.tb_payment_load.Controls.Add(Me.txt_checking_load)
        Me.tb_payment_load.Controls.Add(Me.Label5)
        Me.tb_payment_load.Controls.Add(Me.C_Cells1)
        Me.tb_payment_load.Controls.Add(Me.Label1)
        Me.tb_payment_load.Controls.Add(Me.C_rows1)
        Me.tb_payment_load.Controls.Add(Me.Label12)
        Me.tb_payment_load.Controls.Add(Me.load_payment)
        Me.tb_payment_load.Location = New System.Drawing.Point(4, 22)
        Me.tb_payment_load.Name = "tb_payment_load"
        Me.tb_payment_load.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_payment_load.Size = New System.Drawing.Size(958, 576)
        Me.tb_payment_load.TabIndex = 0
        Me.tb_payment_load.Text = "Payment Load"
        Me.tb_payment_load.UseVisualStyleBackColor = True
        '
        'load_payment
        '
        Me.load_payment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_payment.BackgroundColor = System.Drawing.Color.Silver
        Me.load_payment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_payment.Location = New System.Drawing.Point(0, 0)
        Me.load_payment.Name = "load_payment"
        Me.load_payment.ReadOnly = True
        Me.load_payment.Size = New System.Drawing.Size(959, 553)
        Me.load_payment.TabIndex = 0
        '
        'tb_payment_process
        '
        Me.tb_payment_process.Controls.Add(Me.C_rows2)
        Me.tb_payment_process.Controls.Add(Me.Label2)
        Me.tb_payment_process.Controls.Add(Me.load_payment_process)
        Me.tb_payment_process.Location = New System.Drawing.Point(4, 22)
        Me.tb_payment_process.Name = "tb_payment_process"
        Me.tb_payment_process.Size = New System.Drawing.Size(958, 576)
        Me.tb_payment_process.TabIndex = 3
        Me.tb_payment_process.Text = "Payment Process"
        Me.tb_payment_process.UseVisualStyleBackColor = True
        '
        'tb_payment_notprocess
        '
        Me.tb_payment_notprocess.Controls.Add(Me.load_payment_notprocess)
        Me.tb_payment_notprocess.Controls.Add(Me.C_rows3)
        Me.tb_payment_notprocess.Controls.Add(Me.Label6)
        Me.tb_payment_notprocess.Location = New System.Drawing.Point(4, 22)
        Me.tb_payment_notprocess.Name = "tb_payment_notprocess"
        Me.tb_payment_notprocess.Size = New System.Drawing.Size(958, 576)
        Me.tb_payment_notprocess.TabIndex = 2
        Me.tb_payment_notprocess.Text = "Payment Not Process"
        Me.tb_payment_notprocess.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(0, 38)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(962, 107)
        Me.Panel2.TabIndex = 18
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.btn_summary)
        Me.GroupBox2.Controls.Add(Me.cb_export)
        Me.GroupBox2.Controls.Add(Me.btn_balanced)
        Me.GroupBox2.Controls.Add(Me.txt_interest)
        Me.GroupBox2.Controls.Add(Me.btn_pmt)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.cb_payment)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cb_product)
        Me.GroupBox2.Controls.Add(Me.lb_test)
        Me.GroupBox2.Controls.Add(Me.btn_checking)
        Me.GroupBox2.Location = New System.Drawing.Point(113, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(744, 71)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Control"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(324, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "(%)"
        '
        'btn_summary
        '
        Me.btn_summary.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_summary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_summary.Location = New System.Drawing.Point(445, 40)
        Me.btn_summary.Name = "btn_summary"
        Me.btn_summary.Size = New System.Drawing.Size(58, 24)
        Me.btn_summary.TabIndex = 3
        Me.btn_summary.Text = "Summary"
        Me.btn_summary.UseVisualStyleBackColor = False
        '
        'cb_export
        '
        Me.cb_export.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cb_export.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_export.ForeColor = System.Drawing.Color.Black
        Me.cb_export.FormattingEnabled = True
        Me.cb_export.Items.AddRange(New Object() {"Export", "Excel (.xls)", "TXT (.txt)"})
        Me.cb_export.Location = New System.Drawing.Point(663, 41)
        Me.cb_export.Name = "cb_export"
        Me.cb_export.Size = New System.Drawing.Size(58, 21)
        Me.cb_export.TabIndex = 11
        '
        'btn_balanced
        '
        Me.btn_balanced.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_balanced.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_balanced.Location = New System.Drawing.Point(515, 40)
        Me.btn_balanced.Name = "btn_balanced"
        Me.btn_balanced.Size = New System.Drawing.Size(60, 24)
        Me.btn_balanced.TabIndex = 21
        Me.btn_balanced.Text = "Balanced"
        Me.btn_balanced.UseVisualStyleBackColor = False
        '
        'txt_interest
        '
        Me.txt_interest.Location = New System.Drawing.Point(273, 22)
        Me.txt_interest.Name = "txt_interest"
        Me.txt_interest.Size = New System.Drawing.Size(45, 20)
        Me.txt_interest.TabIndex = 22
        Me.txt_interest.Text = "14"
        '
        'btn_pmt
        '
        Me.btn_pmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_pmt.AutoSize = True
        Me.btn_pmt.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_pmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_pmt.Location = New System.Drawing.Point(592, 40)
        Me.btn_pmt.Name = "btn_pmt"
        Me.btn_pmt.Size = New System.Drawing.Size(58, 24)
        Me.btn_pmt.TabIndex = 20
        Me.btn_pmt.Text = "PMT"
        Me.btn_pmt.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(213, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Interest"
        '
        'cb_payment
        '
        Me.cb_payment.FormattingEnabled = True
        Me.cb_payment.Items.AddRange(New Object() {"--Choose Payment--", "Reguler Payment", "Early Termination"})
        Me.cb_payment.Location = New System.Drawing.Point(76, 44)
        Me.cb_payment.Name = "cb_payment"
        Me.cb_payment.Size = New System.Drawing.Size(119, 21)
        Me.cb_payment.TabIndex = 6
        Me.cb_payment.Text = "--Choose Payment--"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Payment"
        '
        'cb_product
        '
        Me.cb_product.FormattingEnabled = True
        Me.cb_product.Items.AddRange(New Object() {"--Choose Product--", "KREDIVO", "AKULAKU"})
        Me.cb_product.Location = New System.Drawing.Point(76, 19)
        Me.cb_product.Name = "cb_product"
        Me.cb_product.Size = New System.Drawing.Size(119, 21)
        Me.cb_product.TabIndex = 2
        Me.cb_product.Text = "--Choose Product--"
        '
        'lb_test
        '
        Me.lb_test.AutoSize = True
        Me.lb_test.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_test.Location = New System.Drawing.Point(10, 22)
        Me.lb_test.Name = "lb_test"
        Me.lb_test.Size = New System.Drawing.Size(44, 13)
        Me.lb_test.TabIndex = 5
        Me.lb_test.Text = "Product"
        '
        'btn_checking
        '
        Me.btn_checking.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_checking.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_checking.ForeColor = System.Drawing.Color.White
        Me.btn_checking.Location = New System.Drawing.Point(365, 40)
        Me.btn_checking.Name = "btn_checking"
        Me.btn_checking.Size = New System.Drawing.Size(64, 25)
        Me.btn_checking.TabIndex = 0
        Me.btn_checking.Text = "Checking"
        Me.btn_checking.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_import)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(92, 71)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "File Upload"
        '
        'btn_import
        '
        Me.btn_import.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import.Location = New System.Drawing.Point(12, 29)
        Me.btn_import.Name = "btn_import"
        Me.btn_import.Size = New System.Drawing.Size(64, 24)
        Me.btn_import.TabIndex = 1
        Me.btn_import.Text = "Import"
        Me.btn_import.UseVisualStyleBackColor = False
        '
        'Payment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(970, 750)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.tabcontrol1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "Payment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reconsile Payment"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.load_payment_notprocess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.load_payment_process, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabcontrol1.ResumeLayout(False)
        Me.tb_payment_load.ResumeLayout(False)
        Me.tb_payment_load.PerformLayout()
        CType(Me.load_payment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_payment_process.ResumeLayout(False)
        Me.tb_payment_process.PerformLayout()
        Me.tb_payment_notprocess.ResumeLayout(False)
        Me.tb_payment_notprocess.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents load_payment_notprocess As System.Windows.Forms.DataGridView
    Friend WithEvents C_rows3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents C_rows2 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents load_payment_process As System.Windows.Forms.DataGridView
    Friend WithEvents txt_checking_load As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents C_Cells1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents C_rows1 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tabcontrol1 As System.Windows.Forms.TabControl
    Friend WithEvents tb_payment_load As System.Windows.Forms.TabPage
    Friend WithEvents load_payment As System.Windows.Forms.DataGridView
    Friend WithEvents tb_payment_process As System.Windows.Forms.TabPage
    Friend WithEvents tb_payment_notprocess As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_pmt As System.Windows.Forms.Button
    Friend WithEvents cb_product As System.Windows.Forms.ComboBox
    Friend WithEvents lb_test As System.Windows.Forms.Label
    Friend WithEvents btn_checking As System.Windows.Forms.Button
    Friend WithEvents btn_summary As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_import As System.Windows.Forms.Button
    Friend WithEvents cb_payment As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btn_balanced As System.Windows.Forms.Button
    Friend WithEvents cb_export As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_interest As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
