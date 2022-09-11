<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Checking
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btn_clear = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btn_checking = New System.Windows.Forms.Button()
        Me.lb_test = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_import = New System.Windows.Forms.Button()
        Me.load_cif_tab = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.C_Cells1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.C_rows1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.load_cif_check = New System.Windows.Forms.DataGridView()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btn_export_cif_found = New System.Windows.Forms.Button()
        Me.load_cif_found = New System.Windows.Forms.DataGridView()
        Me.C_rows2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btn_cleansing = New System.Windows.Forms.Button()
        Me.btn_export_cif_not_found = New System.Windows.Forms.Button()
        Me.load_cif_not_found = New System.Windows.Forms.DataGridView()
        Me.C_rows3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.load_cif_tab.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.load_cif_check, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        CType(Me.load_cif_found, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.load_cif_not_found, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(4, 47)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1231, 95)
        Me.Panel2.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btn_clear)
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.btn_checking)
        Me.GroupBox2.Controls.Add(Me.lb_test)
        Me.GroupBox2.Location = New System.Drawing.Point(145, 9)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(504, 69)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Control"
        '
        'btn_clear
        '
        Me.btn_clear.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_clear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_clear.Location = New System.Drawing.Point(395, 25)
        Me.btn_clear.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_clear.Name = "btn_clear"
        Me.btn_clear.Size = New System.Drawing.Size(85, 30)
        Me.btn_clear.TabIndex = 6
        Me.btn_clear.Text = "Clear"
        Me.btn_clear.UseVisualStyleBackColor = False
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"KREDIVO", "AKULAKU"})
        Me.ComboBox1.Location = New System.Drawing.Point(103, 27)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(157, 24)
        Me.ComboBox1.TabIndex = 2
        Me.ComboBox1.Text = "--Choose Product--"
        '
        'btn_checking
        '
        Me.btn_checking.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_checking.Enabled = False
        Me.btn_checking.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_checking.Location = New System.Drawing.Point(287, 25)
        Me.btn_checking.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_checking.Name = "btn_checking"
        Me.btn_checking.Size = New System.Drawing.Size(84, 31)
        Me.btn_checking.TabIndex = 0
        Me.btn_checking.Text = "Checking"
        Me.btn_checking.UseVisualStyleBackColor = False
        '
        'lb_test
        '
        Me.lb_test.AutoSize = True
        Me.lb_test.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_test.Location = New System.Drawing.Point(13, 30)
        Me.lb_test.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_test.Name = "lb_test"
        Me.lb_test.Size = New System.Drawing.Size(57, 17)
        Me.lb_test.TabIndex = 5
        Me.lb_test.Text = "Product"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_import)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 10)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(116, 69)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Control"
        '
        'btn_import
        '
        Me.btn_import.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import.Location = New System.Drawing.Point(15, 27)
        Me.btn_import.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_import.Name = "btn_import"
        Me.btn_import.Size = New System.Drawing.Size(85, 30)
        Me.btn_import.TabIndex = 1
        Me.btn_import.Text = "Import"
        Me.btn_import.UseVisualStyleBackColor = False
        '
        'load_cif_tab
        '
        Me.load_cif_tab.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_cif_tab.Controls.Add(Me.TabPage3)
        Me.load_cif_tab.Controls.Add(Me.TabPage1)
        Me.load_cif_tab.Controls.Add(Me.TabPage2)
        Me.load_cif_tab.Location = New System.Drawing.Point(11, 150)
        Me.load_cif_tab.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.load_cif_tab.Name = "load_cif_tab"
        Me.load_cif_tab.SelectedIndex = 0
        Me.load_cif_tab.Size = New System.Drawing.Size(1211, 745)
        Me.load_cif_tab.TabIndex = 3
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.C_Cells1)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.C_rows1)
        Me.TabPage3.Controls.Add(Me.Label5)
        Me.TabPage3.Controls.Add(Me.load_cif_check)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1203, 716)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "CIF Load"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'C_Cells1
        '
        Me.C_Cells1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_Cells1.AutoSize = True
        Me.C_Cells1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_Cells1.Location = New System.Drawing.Point(1074, 691)
        Me.C_Cells1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_Cells1.Name = "C_Cells1"
        Me.C_Cells1.Size = New System.Drawing.Size(16, 17)
        Me.C_Cells1.TabIndex = 25
        Me.C_Cells1.Text = "0"
        Me.C_Cells1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1018, 691)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 17)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Cells :"
        '
        'C_rows1
        '
        Me.C_rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows1.AutoSize = True
        Me.C_rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows1.Location = New System.Drawing.Point(1141, 692)
        Me.C_rows1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows1.Name = "C_rows1"
        Me.C_rows1.Size = New System.Drawing.Size(16, 17)
        Me.C_rows1.TabIndex = 21
        Me.C_rows1.Text = "0"
        Me.C_rows1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1085, 691)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 17)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Rows :"
        '
        'load_cif_check
        '
        Me.load_cif_check.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.load_cif_check.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.load_cif_check.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.load_cif_check.DefaultCellStyle = DataGridViewCellStyle2
        Me.load_cif_check.Location = New System.Drawing.Point(0, 0)
        Me.load_cif_check.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.load_cif_check.Name = "load_cif_check"
        Me.load_cif_check.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.load_cif_check.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.load_cif_check.RowHeadersWidth = 51
        Me.load_cif_check.Size = New System.Drawing.Size(1199, 686)
        Me.load_cif_check.TabIndex = 19
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btn_export_cif_found)
        Me.TabPage1.Controls.Add(Me.load_cif_found)
        Me.TabPage1.Controls.Add(Me.C_rows2)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage1.Size = New System.Drawing.Size(1220, 724)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "CIF Found"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btn_export_cif_found
        '
        Me.btn_export_cif_found.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_export_cif_found.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_export_cif_found.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_export_cif_found.Location = New System.Drawing.Point(1135, 1)
        Me.btn_export_cif_found.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_export_cif_found.Name = "btn_export_cif_found"
        Me.btn_export_cif_found.Size = New System.Drawing.Size(81, 30)
        Me.btn_export_cif_found.TabIndex = 24
        Me.btn_export_cif_found.Text = "Export"
        Me.btn_export_cif_found.UseVisualStyleBackColor = False
        '
        'load_cif_found
        '
        Me.load_cif_found.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.load_cif_found.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.load_cif_found.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.load_cif_found.DefaultCellStyle = DataGridViewCellStyle5
        Me.load_cif_found.Location = New System.Drawing.Point(1, 31)
        Me.load_cif_found.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.load_cif_found.Name = "load_cif_found"
        Me.load_cif_found.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.load_cif_found.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.load_cif_found.RowHeadersWidth = 51
        Me.load_cif_found.Size = New System.Drawing.Size(1216, 661)
        Me.load_cif_found.TabIndex = 18
        '
        'C_rows2
        '
        Me.C_rows2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows2.AutoSize = True
        Me.C_rows2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows2.Location = New System.Drawing.Point(1161, 695)
        Me.C_rows2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows2.Name = "C_rows2"
        Me.C_rows2.Size = New System.Drawing.Size(16, 17)
        Me.C_rows2.TabIndex = 17
        Me.C_rows2.Text = "0"
        Me.C_rows2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1103, 695)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 17)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Rows :"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btn_cleansing)
        Me.TabPage2.Controls.Add(Me.btn_export_cif_not_found)
        Me.TabPage2.Controls.Add(Me.load_cif_not_found)
        Me.TabPage2.Controls.Add(Me.C_rows3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage2.Size = New System.Drawing.Size(1220, 724)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "CIF Not Found"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btn_cleansing
        '
        Me.btn_cleansing.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_cleansing.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_cleansing.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cleansing.Location = New System.Drawing.Point(1041, 0)
        Me.btn_cleansing.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_cleansing.Name = "btn_cleansing"
        Me.btn_cleansing.Size = New System.Drawing.Size(85, 30)
        Me.btn_cleansing.TabIndex = 7
        Me.btn_cleansing.Text = "Cleansing"
        Me.btn_cleansing.UseVisualStyleBackColor = False
        '
        'btn_export_cif_not_found
        '
        Me.btn_export_cif_not_found.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_export_cif_not_found.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_export_cif_not_found.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_export_cif_not_found.Location = New System.Drawing.Point(1135, 0)
        Me.btn_export_cif_not_found.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_export_cif_not_found.Name = "btn_export_cif_not_found"
        Me.btn_export_cif_not_found.Size = New System.Drawing.Size(81, 30)
        Me.btn_export_cif_not_found.TabIndex = 23
        Me.btn_export_cif_not_found.Text = "Export"
        Me.btn_export_cif_not_found.UseVisualStyleBackColor = False
        '
        'load_cif_not_found
        '
        Me.load_cif_not_found.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.load_cif_not_found.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.load_cif_not_found.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.load_cif_not_found.DefaultCellStyle = DataGridViewCellStyle8
        Me.load_cif_not_found.Location = New System.Drawing.Point(-3, 30)
        Me.load_cif_not_found.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.load_cif_not_found.Name = "load_cif_not_found"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.load_cif_not_found.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.load_cif_not_found.RowHeadersWidth = 51
        Me.load_cif_not_found.Size = New System.Drawing.Size(1220, 662)
        Me.load_cif_not_found.TabIndex = 20
        '
        'C_rows3
        '
        Me.C_rows3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows3.AutoSize = True
        Me.C_rows3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows3.Location = New System.Drawing.Point(1156, 694)
        Me.C_rows3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows3.Name = "C_rows3"
        Me.C_rows3.Size = New System.Drawing.Size(16, 17)
        Me.C_rows3.TabIndex = 19
        Me.C_rows3.Text = "0"
        Me.C_rows3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1103, 694)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 17)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Rows :"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
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
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1359, 44)
        Me.Panel1.TabIndex = 21
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
        Me.Label8.Size = New System.Drawing.Size(236, 25)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Checking CIF Nasabah"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Checking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1231, 898)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.load_cif_tab)
        Me.Controls.Add(Me.Panel2)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Checking"
        Me.Text = "(1) Checking Data"
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.load_cif_tab.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.load_cif_check, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.load_cif_found, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.load_cif_not_found, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_checking As System.Windows.Forms.Button
    Friend WithEvents btn_import As System.Windows.Forms.Button
    Friend WithEvents load_cif_tab As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents C_rows2 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents C_rows3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents load_cif_found As System.Windows.Forms.DataGridView
    Friend WithEvents load_cif_not_found As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents C_rows1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents load_cif_check As System.Windows.Forms.DataGridView
    Friend WithEvents btn_export_cif_found As System.Windows.Forms.Button
    Friend WithEvents btn_export_cif_not_found As System.Windows.Forms.Button
    Friend WithEvents btn_clear As System.Windows.Forms.Button
    Friend WithEvents btn_cleansing As System.Windows.Forms.Button
    Friend WithEvents C_Cells1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents lb_test As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
