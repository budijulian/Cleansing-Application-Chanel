<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportDatabase
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
        Me.load_import_data = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cb_table = New System.Windows.Forms.ComboBox()
        Me.btn_submit_split = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cb_product = New System.Windows.Forms.ComboBox()
        Me.lb_test = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btn_import = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.C_Rows1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1.SuspendLayout()
        CType(Me.load_import_data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.Panel1.Location = New System.Drawing.Point(6, 3)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1359, 44)
        Me.Panel1.TabIndex = 22
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
        Me.Label8.Size = New System.Drawing.Size(170, 25)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Import Database"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'load_import_data
        '
        Me.load_import_data.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.load_import_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_import_data.Location = New System.Drawing.Point(9, 22)
        Me.load_import_data.Margin = New System.Windows.Forms.Padding(4)
        Me.load_import_data.Name = "load_import_data"
        Me.load_import_data.RowHeadersWidth = 51
        Me.load_import_data.Size = New System.Drawing.Size(883, 484)
        Me.load_import_data.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Location = New System.Drawing.Point(2, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(936, 96)
        Me.Panel2.TabIndex = 21
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cb_table)
        Me.GroupBox2.Controls.Add(Me.btn_submit_split)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cb_product)
        Me.GroupBox2.Controls.Add(Me.lb_test)
        Me.GroupBox2.Location = New System.Drawing.Point(153, 11)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(767, 69)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Control"
        '
        'cb_table
        '
        Me.cb_table.FormattingEnabled = True
        Me.cb_table.Items.AddRange(New Object() {"[1] Report Early Payment", "[2] Report Reguler Payment", "[3] Report Cancel", "[4] Report Disbursement", "[5] Report Buyback"})
        Me.cb_table.Location = New System.Drawing.Point(330, 28)
        Me.cb_table.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_table.Name = "cb_table"
        Me.cb_table.Size = New System.Drawing.Size(193, 24)
        Me.cb_table.TabIndex = 25
        Me.cb_table.Text = "--Choose Report--"
        '
        'btn_submit_split
        '
        Me.btn_submit_split.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_submit_split.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_submit_split.ForeColor = System.Drawing.Color.White
        Me.btn_submit_split.Location = New System.Drawing.Point(561, 25)
        Me.btn_submit_split.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_submit_split.Name = "btn_submit_split"
        Me.btn_submit_split.Size = New System.Drawing.Size(85, 31)
        Me.btn_submit_split.TabIndex = 0
        Me.btn_submit_split.Text = "Submit"
        Me.btn_submit_split.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(257, 30)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 17)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Report"
        '
        'cb_product
        '
        Me.cb_product.FormattingEnabled = True
        Me.cb_product.Items.AddRange(New Object() {"--Choose Product--"})
        Me.cb_product.Location = New System.Drawing.Point(99, 27)
        Me.cb_product.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_product.Name = "cb_product"
        Me.cb_product.Size = New System.Drawing.Size(124, 24)
        Me.cb_product.TabIndex = 24
        '
        'lb_test
        '
        Me.lb_test.AutoSize = True
        Me.lb_test.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_test.Location = New System.Drawing.Point(18, 30)
        Me.lb_test.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_test.Name = "lb_test"
        Me.lb_test.Size = New System.Drawing.Size(57, 17)
        Me.lb_test.TabIndex = 26
        Me.lb_test.Text = "Product"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btn_import)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 11)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(123, 69)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "File Upload"
        '
        'btn_import
        '
        Me.btn_import.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import.Location = New System.Drawing.Point(19, 25)
        Me.btn_import.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_import.Name = "btn_import"
        Me.btn_import.Size = New System.Drawing.Size(77, 30)
        Me.btn_import.TabIndex = 1
        Me.btn_import.Text = "Import"
        Me.btn_import.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.C_Rows1)
        Me.GroupBox1.Controls.Add(Me.load_import_data)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 155)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 537)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Load Data"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(796, 513)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 17)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Rows"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C_Rows1
        '
        Me.C_Rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_Rows1.AutoSize = True
        Me.C_Rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_Rows1.Location = New System.Drawing.Point(857, 515)
        Me.C_Rows1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_Rows1.Name = "C_Rows1"
        Me.C_Rows1.Size = New System.Drawing.Size(16, 17)
        Me.C_Rows1.TabIndex = 24
        Me.C_Rows1.Text = "0"
        Me.C_Rows1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ImportDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 705)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ImportDatabase"
        Me.Text = "[2] ImportDatabase"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.load_import_data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents load_import_data As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btn_submit_split As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btn_import As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents cb_table As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cb_product As ComboBox
    Friend WithEvents lb_test As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents C_Rows1 As Label
End Class
