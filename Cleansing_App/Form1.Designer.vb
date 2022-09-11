<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cb_accurate = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btn_summary = New System.Windows.Forms.Button()
        Me.cb_export = New System.Windows.Forms.ComboBox()
        Me.btn_cleansing_loan = New System.Windows.Forms.Button()
        Me.cb_product = New System.Windows.Forms.ComboBox()
        Me.lb_test = New System.Windows.Forms.Label()
        Me.btn_cleansing = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_import = New System.Windows.Forms.Button()
        Me.load_cif = New System.Windows.Forms.DataGridView()
        Me.tabcontrol1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txt_checking_load = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.C_Cells1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.C_rows1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cif_success = New System.Windows.Forms.TabPage()
        Me.C_rows2 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.load_cif_success = New System.Windows.Forms.DataGridView()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.load_cif_failed = New System.Windows.Forms.DataGridView()
        Me.C_rows3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.load_cif, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabcontrol1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.cif_success.SuspendLayout()
        CType(Me.load_cif_success, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.load_cif_failed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(-9, 49)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1307, 101)
        Me.Panel2.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cb_accurate)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btn_summary)
        Me.GroupBox2.Controls.Add(Me.cb_export)
        Me.GroupBox2.Controls.Add(Me.btn_cleansing_loan)
        Me.GroupBox2.Controls.Add(Me.cb_product)
        Me.GroupBox2.Controls.Add(Me.lb_test)
        Me.GroupBox2.Controls.Add(Me.btn_cleansing)
        Me.GroupBox2.Location = New System.Drawing.Point(225, 11)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(915, 69)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Control"
        '
        'cb_accurate
        '
        Me.cb_accurate.FormattingEnabled = True
        Me.cb_accurate.Items.AddRange(New Object() {"Normal", "Adjust (50%)", "Adjust (75%)", "Adjust (100%)"})
        Me.cb_accurate.Location = New System.Drawing.Point(369, 30)
        Me.cb_accurate.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_accurate.Name = "cb_accurate"
        Me.cb_accurate.Size = New System.Drawing.Size(107, 24)
        Me.cb_accurate.TabIndex = 21
        Me.cb_accurate.Text = "Normal"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(276, 32)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 17)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Accurate"
        '
        'btn_summary
        '
        Me.btn_summary.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_summary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_summary.Location = New System.Drawing.Point(819, 26)
        Me.btn_summary.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_summary.Name = "btn_summary"
        Me.btn_summary.Size = New System.Drawing.Size(77, 30)
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
        Me.cb_export.Items.AddRange(New Object() {"Export", "Excel (.xls)", "CSV (.csv)", "TXT (.txt)"})
        Me.cb_export.Location = New System.Drawing.Point(725, 30)
        Me.cb_export.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_export.Name = "cb_export"
        Me.cb_export.Size = New System.Drawing.Size(76, 24)
        Me.cb_export.TabIndex = 11
        '
        'btn_cleansing_loan
        '
        Me.btn_cleansing_loan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_cleansing_loan.AutoSize = True
        Me.btn_cleansing_loan.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_cleansing_loan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cleansing_loan.Location = New System.Drawing.Point(629, 27)
        Me.btn_cleansing_loan.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_cleansing_loan.Name = "btn_cleansing_loan"
        Me.btn_cleansing_loan.Size = New System.Drawing.Size(75, 31)
        Me.btn_cleansing_loan.TabIndex = 20
        Me.btn_cleansing_loan.Text = "(3) Join"
        Me.btn_cleansing_loan.UseVisualStyleBackColor = False
        '
        'cb_product
        '
        Me.cb_product.FormattingEnabled = True
        Me.cb_product.Items.AddRange(New Object() {"--Choose Product--"})
        Me.cb_product.Location = New System.Drawing.Point(103, 27)
        Me.cb_product.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_product.Name = "cb_product"
        Me.cb_product.Size = New System.Drawing.Size(157, 24)
        Me.cb_product.TabIndex = 2
        Me.cb_product.Text = "--Choose Product--"
        '
        'lb_test
        '
        Me.lb_test.AutoSize = True
        Me.lb_test.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_test.Location = New System.Drawing.Point(9, 30)
        Me.lb_test.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_test.Name = "lb_test"
        Me.lb_test.Size = New System.Drawing.Size(57, 17)
        Me.lb_test.TabIndex = 5
        Me.lb_test.Text = "Product"
        '
        'btn_cleansing
        '
        Me.btn_cleansing.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_cleansing.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cleansing.ForeColor = System.Drawing.Color.White
        Me.btn_cleansing.Location = New System.Drawing.Point(511, 26)
        Me.btn_cleansing.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_cleansing.Name = "btn_cleansing"
        Me.btn_cleansing.Size = New System.Drawing.Size(85, 31)
        Me.btn_cleansing.TabIndex = 0
        Me.btn_cleansing.Text = "Cleansing"
        Me.btn_cleansing.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btn_import)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 11)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(192, 69)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "File Upload"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 32)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 17)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "CIF File*"
        '
        'btn_import
        '
        Me.btn_import.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import.Location = New System.Drawing.Point(91, 26)
        Me.btn_import.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import.Name = "btn_import"
        Me.btn_import.Size = New System.Drawing.Size(77, 30)
        Me.btn_import.TabIndex = 1
        Me.btn_import.Text = "Import"
        Me.btn_import.UseVisualStyleBackColor = False
        '
        'load_cif
        '
        Me.load_cif.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_cif.BackgroundColor = System.Drawing.Color.Silver
        Me.load_cif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_cif.Location = New System.Drawing.Point(0, 0)
        Me.load_cif.Margin = New System.Windows.Forms.Padding(4)
        Me.load_cif.Name = "load_cif"
        Me.load_cif.ReadOnly = True
        Me.load_cif.RowHeadersWidth = 51
        Me.load_cif.Size = New System.Drawing.Size(1251, 662)
        Me.load_cif.TabIndex = 0
        '
        'tabcontrol1
        '
        Me.tabcontrol1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabcontrol1.Controls.Add(Me.TabPage1)
        Me.tabcontrol1.Controls.Add(Me.cif_success)
        Me.tabcontrol1.Controls.Add(Me.TabPage5)
        Me.tabcontrol1.Location = New System.Drawing.Point(9, 23)
        Me.tabcontrol1.Margin = New System.Windows.Forms.Padding(4)
        Me.tabcontrol1.Name = "tabcontrol1"
        Me.tabcontrol1.SelectedIndex = 0
        Me.tabcontrol1.Size = New System.Drawing.Size(1259, 726)
        Me.tabcontrol1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txt_checking_load)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.C_Cells1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.C_rows1)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.load_cif)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(1251, 697)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "CIF Load"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txt_checking_load
        '
        Me.txt_checking_load.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_checking_load.AutoSize = True
        Me.txt_checking_load.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_checking_load.Location = New System.Drawing.Point(97, 667)
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
        Me.Label5.Location = New System.Drawing.Point(8, 666)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 17)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Cleansing : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C_Cells1
        '
        Me.C_Cells1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_Cells1.AutoSize = True
        Me.C_Cells1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_Cells1.Location = New System.Drawing.Point(1105, 666)
        Me.C_Cells1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_Cells1.Name = "C_Cells1"
        Me.C_Cells1.Size = New System.Drawing.Size(16, 17)
        Me.C_Cells1.TabIndex = 17
        Me.C_Cells1.Text = "0"
        Me.C_Cells1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1049, 666)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 17)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Cells :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'C_rows1
        '
        Me.C_rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows1.AutoSize = True
        Me.C_rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows1.Location = New System.Drawing.Point(1192, 666)
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
        Me.Label12.Location = New System.Drawing.Point(1135, 666)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 17)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Rows :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cif_success
        '
        Me.cif_success.Controls.Add(Me.C_rows2)
        Me.cif_success.Controls.Add(Me.Label2)
        Me.cif_success.Controls.Add(Me.load_cif_success)
        Me.cif_success.Location = New System.Drawing.Point(4, 25)
        Me.cif_success.Margin = New System.Windows.Forms.Padding(4)
        Me.cif_success.Name = "cif_success"
        Me.cif_success.Size = New System.Drawing.Size(1251, 697)
        Me.cif_success.TabIndex = 3
        Me.cif_success.Text = "CIF Final"
        Me.cif_success.UseVisualStyleBackColor = True
        '
        'C_rows2
        '
        Me.C_rows2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows2.AutoSize = True
        Me.C_rows2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows2.Location = New System.Drawing.Point(1192, 666)
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
        Me.Label2.Location = New System.Drawing.Point(1135, 666)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 17)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Rows :"
        '
        'load_cif_success
        '
        Me.load_cif_success.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_cif_success.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_cif_success.Location = New System.Drawing.Point(0, 0)
        Me.load_cif_success.Margin = New System.Windows.Forms.Padding(4)
        Me.load_cif_success.Name = "load_cif_success"
        Me.load_cif_success.RowHeadersWidth = 51
        Me.load_cif_success.Size = New System.Drawing.Size(1247, 662)
        Me.load_cif_success.TabIndex = 17
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.load_cif_failed)
        Me.TabPage5.Controls.Add(Me.C_rows3)
        Me.TabPage5.Controls.Add(Me.Label6)
        Me.TabPage5.Location = New System.Drawing.Point(4, 25)
        Me.TabPage5.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(1251, 697)
        Me.TabPage5.TabIndex = 2
        Me.TabPage5.Text = "CIF Takedown"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'load_cif_failed
        '
        Me.load_cif_failed.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_cif_failed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_cif_failed.Location = New System.Drawing.Point(0, 0)
        Me.load_cif_failed.Margin = New System.Windows.Forms.Padding(4)
        Me.load_cif_failed.Name = "load_cif_failed"
        Me.load_cif_failed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.load_cif_failed.RowHeadersWidth = 51
        Me.load_cif_failed.Size = New System.Drawing.Size(1249, 666)
        Me.load_cif_failed.TabIndex = 19
        '
        'C_rows3
        '
        Me.C_rows3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows3.AutoSize = True
        Me.C_rows3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows3.Location = New System.Drawing.Point(1195, 670)
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
        Me.Label6.Location = New System.Drawing.Point(1137, 670)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(50, 17)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Rows :"
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
        Me.Label8.Size = New System.Drawing.Size(242, 25)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "CLEANSING DATA CIF"
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
        Me.Panel1.Location = New System.Drawing.Point(3, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1359, 44)
        Me.Panel1.TabIndex = 17
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.tabcontrol1)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 155)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(1276, 757)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Output"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(1293, 923)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[2] Cleansing App"
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.load_cif, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabcontrol1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.cif_success.ResumeLayout(False)
        Me.cif_success.PerformLayout()
        CType(Me.load_cif_success, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.load_cif_failed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents load_cif As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btn_cleansing As System.Windows.Forms.Button
    Friend WithEvents btn_import As System.Windows.Forms.Button
    Friend WithEvents tabcontrol1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents C_rows1 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents C_rows3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_summary As System.Windows.Forms.Button
    Friend WithEvents cif_success As System.Windows.Forms.TabPage
    Friend WithEvents load_cif_success As System.Windows.Forms.DataGridView
    Friend WithEvents C_rows2 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents load_cif_failed As System.Windows.Forms.DataGridView
    Friend WithEvents C_Cells1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cb_product As System.Windows.Forms.ComboBox
    Friend WithEvents lb_test As System.Windows.Forms.Label
    Friend WithEvents txt_checking_load As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_cleansing_loan As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cb_export As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_accurate As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
