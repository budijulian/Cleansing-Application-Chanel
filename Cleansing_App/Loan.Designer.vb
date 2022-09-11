<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Loan
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.C_data4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btn_import_collect = New System.Windows.Forms.Button()
        Me.cb_import_cif = New System.Windows.Forms.ComboBox()
        Me.C_data3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btn_import_cif = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.C_data1 = New System.Windows.Forms.Label()
        Me.C_data2 = New System.Windows.Forms.Label()
        Me.btn_import_loan = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cb_export = New System.Windows.Forms.ComboBox()
        Me.btn_cleansing = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cb_type_join = New System.Windows.Forms.ComboBox()
        Me.cb_product = New System.Windows.Forms.ComboBox()
        Me.tabcontrol1 = New System.Windows.Forms.TabControl()
        Me.tb_report1 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.C_Rows1 = New System.Windows.Forms.Label()
        Me.load_cif_final = New System.Windows.Forms.DataGridView()
        Me.tb_report2 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.C_Rows2 = New System.Windows.Forms.Label()
        Me.load_cif_gagal = New System.Windows.Forms.DataGridView()
        Me.tb_report3 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.C_rows3 = New System.Windows.Forms.Label()
        Me.load_loan_success = New System.Windows.Forms.DataGridView()
        Me.tb_report4 = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.C_rows4 = New System.Windows.Forms.Label()
        Me.load_loan_takedown = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Panel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tabcontrol1.SuspendLayout()
        Me.tb_report1.SuspendLayout()
        CType(Me.load_cif_final, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_report2.SuspendLayout()
        CType(Me.load_cif_gagal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_report3.SuspendLayout()
        CType(Me.load_loan_success, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_report4.SuspendLayout()
        CType(Me.load_loan_takedown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(-3, -4)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1333, 171)
        Me.Panel2.TabIndex = 11
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.C_data4)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.btn_import_collect)
        Me.GroupBox3.Controls.Add(Me.cb_import_cif)
        Me.GroupBox3.Controls.Add(Me.C_data3)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.btn_import_cif)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.C_data1)
        Me.GroupBox3.Controls.Add(Me.C_data2)
        Me.GroupBox3.Controls.Add(Me.btn_import_loan)
        Me.GroupBox3.Location = New System.Drawing.Point(192, 62)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(409, 92)
        Me.GroupBox3.TabIndex = 17
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Control"
        '
        'C_data4
        '
        Me.C_data4.AutoSize = True
        Me.C_data4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data4.ForeColor = System.Drawing.Color.Green
        Me.C_data4.Location = New System.Drawing.Point(365, 65)
        Me.C_data4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data4.Name = "C_data4"
        Me.C_data4.Size = New System.Drawing.Size(0, 15)
        Me.C_data4.TabIndex = 28
        Me.C_data4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(209, 62)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 17)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Collect File"
        '
        'btn_import_collect
        '
        Me.btn_import_collect.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_collect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_collect.Location = New System.Drawing.Point(292, 57)
        Me.btn_import_collect.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_collect.Name = "btn_import_collect"
        Me.btn_import_collect.Size = New System.Drawing.Size(67, 29)
        Me.btn_import_collect.TabIndex = 26
        Me.btn_import_collect.Text = "Import"
        Me.btn_import_collect.UseVisualStyleBackColor = False
        '
        'cb_import_cif
        '
        Me.cb_import_cif.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.cb_import_cif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_import_cif.FormattingEnabled = True
        Me.cb_import_cif.Items.AddRange(New Object() {"Import ", "022 File", "CS File"})
        Me.cb_import_cif.Location = New System.Drawing.Point(95, 20)
        Me.cb_import_cif.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_import_cif.Name = "cb_import_cif"
        Me.cb_import_cif.Size = New System.Drawing.Size(69, 24)
        Me.cb_import_cif.TabIndex = 19
        '
        'C_data3
        '
        Me.C_data3.AutoSize = True
        Me.C_data3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data3.ForeColor = System.Drawing.Color.Green
        Me.C_data3.Location = New System.Drawing.Point(364, 26)
        Me.C_data3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data3.Name = "C_data3"
        Me.C_data3.Size = New System.Drawing.Size(0, 15)
        Me.C_data3.TabIndex = 18
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 25)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 17)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "No CIF File"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 57)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 17)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "CIF Final"
        '
        'btn_import_cif
        '
        Me.btn_import_cif.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_cif.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_cif.Location = New System.Drawing.Point(95, 53)
        Me.btn_import_cif.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_cif.Name = "btn_import_cif"
        Me.btn_import_cif.Size = New System.Drawing.Size(71, 30)
        Me.btn_import_cif.TabIndex = 1
        Me.btn_import_cif.Text = "Import"
        Me.btn_import_cif.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(210, 25)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 17)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Loan "
        '
        'C_data1
        '
        Me.C_data1.AutoSize = True
        Me.C_data1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data1.ForeColor = System.Drawing.Color.Green
        Me.C_data1.Location = New System.Drawing.Point(169, 26)
        Me.C_data1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data1.Name = "C_data1"
        Me.C_data1.Size = New System.Drawing.Size(0, 15)
        Me.C_data1.TabIndex = 13
        Me.C_data1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'C_data2
        '
        Me.C_data2.AutoSize = True
        Me.C_data2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_data2.ForeColor = System.Drawing.Color.Green
        Me.C_data2.Location = New System.Drawing.Point(170, 60)
        Me.C_data2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_data2.Name = "C_data2"
        Me.C_data2.Size = New System.Drawing.Size(0, 15)
        Me.C_data2.TabIndex = 14
        '
        'btn_import_loan
        '
        Me.btn_import_loan.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import_loan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import_loan.Location = New System.Drawing.Point(292, 18)
        Me.btn_import_loan.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import_loan.Name = "btn_import_loan"
        Me.btn_import_loan.Size = New System.Drawing.Size(67, 30)
        Me.btn_import_loan.TabIndex = 2
        Me.btn_import_loan.Text = "Import"
        Me.btn_import_loan.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1323, 44)
        Me.Panel1.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(253, 25)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "JOIN DATA LOAN && CIF"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cb_export)
        Me.GroupBox2.Controls.Add(Me.btn_cleansing)
        Me.GroupBox2.Location = New System.Drawing.Point(621, 62)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(239, 92)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Control"
        '
        'cb_export
        '
        Me.cb_export.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cb_export.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_export.ForeColor = System.Drawing.Color.Black
        Me.cb_export.FormattingEnabled = True
        Me.cb_export.Items.AddRange(New Object() {"Export", "Excel (.xls)", "CSV (.csv)", "TXT (.txt)"})
        Me.cb_export.Location = New System.Drawing.Point(137, 36)
        Me.cb_export.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_export.Name = "cb_export"
        Me.cb_export.Size = New System.Drawing.Size(76, 24)
        Me.cb_export.TabIndex = 10
        '
        'btn_cleansing
        '
        Me.btn_cleansing.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_cleansing.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cleansing.ForeColor = System.Drawing.Color.White
        Me.btn_cleansing.Location = New System.Drawing.Point(27, 34)
        Me.btn_cleansing.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_cleansing.Name = "btn_cleansing"
        Me.btn_cleansing.Size = New System.Drawing.Size(87, 30)
        Me.btn_cleansing.TabIndex = 0
        Me.btn_cleansing.Text = "Submit"
        Me.btn_cleansing.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cb_type_join)
        Me.GroupBox1.Controls.Add(Me.cb_product)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 62)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(161, 92)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Type"
        '
        'cb_type_join
        '
        Me.cb_type_join.FormattingEnabled = True
        Me.cb_type_join.Items.AddRange(New Object() {"Join CIF", "Join LOAN"})
        Me.cb_type_join.Location = New System.Drawing.Point(12, 25)
        Me.cb_type_join.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_type_join.Name = "cb_type_join"
        Me.cb_type_join.Size = New System.Drawing.Size(136, 24)
        Me.cb_type_join.TabIndex = 16
        Me.cb_type_join.Text = "--Choose Join--"
        '
        'cb_product
        '
        Me.cb_product.FormattingEnabled = True
        Me.cb_product.Items.AddRange(New Object() {"--Choose Product--"})
        Me.cb_product.Location = New System.Drawing.Point(12, 57)
        Me.cb_product.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_product.Name = "cb_product"
        Me.cb_product.Size = New System.Drawing.Size(136, 24)
        Me.cb_product.TabIndex = 88
        '
        'tabcontrol1
        '
        Me.tabcontrol1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabcontrol1.Controls.Add(Me.tb_report1)
        Me.tabcontrol1.Controls.Add(Me.tb_report2)
        Me.tabcontrol1.Controls.Add(Me.tb_report3)
        Me.tabcontrol1.Controls.Add(Me.tb_report4)
        Me.tabcontrol1.Location = New System.Drawing.Point(12, 21)
        Me.tabcontrol1.Margin = New System.Windows.Forms.Padding(4)
        Me.tabcontrol1.Name = "tabcontrol1"
        Me.tabcontrol1.SelectedIndex = 0
        Me.tabcontrol1.Size = New System.Drawing.Size(1299, 716)
        Me.tabcontrol1.TabIndex = 12
        '
        'tb_report1
        '
        Me.tb_report1.Controls.Add(Me.Label2)
        Me.tb_report1.Controls.Add(Me.C_Rows1)
        Me.tb_report1.Controls.Add(Me.load_cif_final)
        Me.tb_report1.Location = New System.Drawing.Point(4, 25)
        Me.tb_report1.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_report1.Name = "tb_report1"
        Me.tb_report1.Size = New System.Drawing.Size(1291, 687)
        Me.tb_report1.TabIndex = 5
        Me.tb_report1.Text = "CIF Final"
        Me.tb_report1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1153, 658)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 17)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Rows"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C_Rows1
        '
        Me.C_Rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_Rows1.AutoSize = True
        Me.C_Rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_Rows1.Location = New System.Drawing.Point(1213, 660)
        Me.C_Rows1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_Rows1.Name = "C_Rows1"
        Me.C_Rows1.Size = New System.Drawing.Size(16, 17)
        Me.C_Rows1.TabIndex = 22
        Me.C_Rows1.Text = "0"
        Me.C_Rows1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'load_cif_final
        '
        Me.load_cif_final.AllowUserToOrderColumns = True
        Me.load_cif_final.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_cif_final.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.load_cif_final.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_cif_final.Location = New System.Drawing.Point(3, 2)
        Me.load_cif_final.Margin = New System.Windows.Forms.Padding(4)
        Me.load_cif_final.Name = "load_cif_final"
        Me.load_cif_final.ReadOnly = True
        Me.load_cif_final.RowHeadersWidth = 51
        Me.load_cif_final.Size = New System.Drawing.Size(1287, 652)
        Me.load_cif_final.TabIndex = 19
        '
        'tb_report2
        '
        Me.tb_report2.Controls.Add(Me.Label4)
        Me.tb_report2.Controls.Add(Me.C_Rows2)
        Me.tb_report2.Controls.Add(Me.load_cif_gagal)
        Me.tb_report2.Location = New System.Drawing.Point(4, 25)
        Me.tb_report2.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_report2.Name = "tb_report2"
        Me.tb_report2.Size = New System.Drawing.Size(1291, 687)
        Me.tb_report2.TabIndex = 4
        Me.tb_report2.Text = "CIF Gagal"
        Me.tb_report2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1159, 658)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 17)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Rows"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C_Rows2
        '
        Me.C_Rows2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_Rows2.AutoSize = True
        Me.C_Rows2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_Rows2.Location = New System.Drawing.Point(1225, 660)
        Me.C_Rows2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_Rows2.Name = "C_Rows2"
        Me.C_Rows2.Size = New System.Drawing.Size(16, 17)
        Me.C_Rows2.TabIndex = 24
        Me.C_Rows2.Text = "0"
        Me.C_Rows2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'load_cif_gagal
        '
        Me.load_cif_gagal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_cif_gagal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_cif_gagal.Location = New System.Drawing.Point(1, 2)
        Me.load_cif_gagal.Margin = New System.Windows.Forms.Padding(4)
        Me.load_cif_gagal.Name = "load_cif_gagal"
        Me.load_cif_gagal.ReadOnly = True
        Me.load_cif_gagal.RowHeadersWidth = 51
        Me.load_cif_gagal.Size = New System.Drawing.Size(1289, 652)
        Me.load_cif_gagal.TabIndex = 18
        '
        'tb_report3
        '
        Me.tb_report3.Controls.Add(Me.Label11)
        Me.tb_report3.Controls.Add(Me.C_rows3)
        Me.tb_report3.Controls.Add(Me.load_loan_success)
        Me.tb_report3.Location = New System.Drawing.Point(4, 25)
        Me.tb_report3.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_report3.Name = "tb_report3"
        Me.tb_report3.Padding = New System.Windows.Forms.Padding(4)
        Me.tb_report3.Size = New System.Drawing.Size(1291, 687)
        Me.tb_report3.TabIndex = 0
        Me.tb_report3.Text = "Loan Final"
        Me.tb_report3.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1154, 660)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 17)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Rows"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C_rows3
        '
        Me.C_rows3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows3.AutoSize = True
        Me.C_rows3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows3.Location = New System.Drawing.Point(1220, 661)
        Me.C_rows3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows3.Name = "C_rows3"
        Me.C_rows3.Size = New System.Drawing.Size(16, 17)
        Me.C_rows3.TabIndex = 15
        Me.C_rows3.Text = "0"
        Me.C_rows3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'load_loan_success
        '
        Me.load_loan_success.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_loan_success.BackgroundColor = System.Drawing.Color.Silver
        Me.load_loan_success.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_loan_success.Location = New System.Drawing.Point(3, 2)
        Me.load_loan_success.Margin = New System.Windows.Forms.Padding(4)
        Me.load_loan_success.Name = "load_loan_success"
        Me.load_loan_success.ReadOnly = True
        Me.load_loan_success.RowHeadersWidth = 51
        Me.load_loan_success.Size = New System.Drawing.Size(1285, 655)
        Me.load_loan_success.TabIndex = 0
        '
        'tb_report4
        '
        Me.tb_report4.Controls.Add(Me.Label9)
        Me.tb_report4.Controls.Add(Me.C_rows4)
        Me.tb_report4.Controls.Add(Me.load_loan_takedown)
        Me.tb_report4.Location = New System.Drawing.Point(4, 25)
        Me.tb_report4.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_report4.Name = "tb_report4"
        Me.tb_report4.Size = New System.Drawing.Size(1291, 687)
        Me.tb_report4.TabIndex = 3
        Me.tb_report4.Text = "Loan Takedown"
        Me.tb_report4.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1158, 660)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 17)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Rows"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C_rows4
        '
        Me.C_rows4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows4.AutoSize = True
        Me.C_rows4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows4.Location = New System.Drawing.Point(1224, 661)
        Me.C_rows4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows4.Name = "C_rows4"
        Me.C_rows4.Size = New System.Drawing.Size(16, 17)
        Me.C_rows4.TabIndex = 19
        Me.C_rows4.Text = "0"
        Me.C_rows4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'load_loan_takedown
        '
        Me.load_loan_takedown.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_loan_takedown.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_loan_takedown.Location = New System.Drawing.Point(0, 2)
        Me.load_loan_takedown.Margin = New System.Windows.Forms.Padding(4)
        Me.load_loan_takedown.Name = "load_loan_takedown"
        Me.load_loan_takedown.RowHeadersWidth = 51
        Me.load_loan_takedown.Size = New System.Drawing.Size(1289, 655)
        Me.load_loan_takedown.TabIndex = 17
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.tabcontrol1)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 175)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(1319, 745)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Output"
        '
        'Loan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1329, 923)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Loan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "(3) Join Loan & CIF"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.tabcontrol1.ResumeLayout(False)
        Me.tb_report1.ResumeLayout(False)
        Me.tb_report1.PerformLayout()
        CType(Me.load_cif_final, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_report2.ResumeLayout(False)
        Me.tb_report2.PerformLayout()
        CType(Me.load_cif_gagal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_report3.ResumeLayout(False)
        Me.tb_report3.PerformLayout()
        CType(Me.load_loan_success, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_report4.ResumeLayout(False)
        Me.tb_report4.PerformLayout()
        CType(Me.load_loan_takedown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_product As System.Windows.Forms.ComboBox
    Friend WithEvents btn_cleansing As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents C_data2 As System.Windows.Forms.Label
    Friend WithEvents C_data1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_import_loan As System.Windows.Forms.Button
    Friend WithEvents btn_import_cif As System.Windows.Forms.Button
    Friend WithEvents tabcontrol1 As System.Windows.Forms.TabControl
    Friend WithEvents tb_report3 As System.Windows.Forms.TabPage
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents load_loan_success As System.Windows.Forms.DataGridView
    Friend WithEvents tb_report4 As System.Windows.Forms.TabPage
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents C_rows4 As System.Windows.Forms.Label
    Friend WithEvents load_loan_takedown As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents C_rows3 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents C_data3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tb_report2 As System.Windows.Forms.TabPage
    Friend WithEvents load_cif_gagal As System.Windows.Forms.DataGridView
    Friend WithEvents tb_report1 As System.Windows.Forms.TabPage
    Friend WithEvents load_cif_final As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_type_join As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents C_Rows1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents C_Rows2 As System.Windows.Forms.Label
    Friend WithEvents cb_export As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_import_cif As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btn_import_collect As Button
    Friend WithEvents C_data4 As Label
End Class
