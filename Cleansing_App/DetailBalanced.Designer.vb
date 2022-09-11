<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DetailBalanced
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.C_rows1 = New System.Windows.Forms.Label()
        Me.load_balanced = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cb_export = New System.Windows.Forms.ComboBox()
        Me.btn_save_balanced = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btn_import = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lb_total_balance = New System.Windows.Forms.Label()
        Me.lb_test = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.load_balanced, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(0, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1205, 43)
        Me.Panel1.TabIndex = 36
        '
        'Label8
        '
        Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 9)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(235, 25)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Detail Balanced Parsial"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.C_rows1)
        Me.GroupBox2.Controls.Add(Me.load_balanced)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 130)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(1181, 459)
        Me.GroupBox2.TabIndex = 44
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Payment"
        '
        'C_rows1
        '
        Me.C_rows1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C_rows1.AutoSize = True
        Me.C_rows1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_rows1.Location = New System.Drawing.Point(1145, 432)
        Me.C_rows1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.C_rows1.Name = "C_rows1"
        Me.C_rows1.Size = New System.Drawing.Size(16, 17)
        Me.C_rows1.TabIndex = 49
        Me.C_rows1.Text = "0"
        Me.C_rows1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'load_balanced
        '
        Me.load_balanced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.load_balanced.Location = New System.Drawing.Point(15, 28)
        Me.load_balanced.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.load_balanced.Name = "load_balanced"
        Me.load_balanced.RowHeadersWidth = 51
        Me.load_balanced.Size = New System.Drawing.Size(1155, 395)
        Me.load_balanced.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1085, 432)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 17)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Rows :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cb_export)
        Me.GroupBox1.Controls.Add(Me.btn_save_balanced)
        Me.GroupBox1.Location = New System.Drawing.Point(172, 54)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(260, 70)
        Me.GroupBox1.TabIndex = 45
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Control"
        '
        'cb_export
        '
        Me.cb_export.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cb_export.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_export.ForeColor = System.Drawing.Color.Black
        Me.cb_export.FormattingEnabled = True
        Me.cb_export.Items.AddRange(New Object() {"Export", "Excel (.xls)", "TXT (.txt)"})
        Me.cb_export.Location = New System.Drawing.Point(160, 25)
        Me.cb_export.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cb_export.Name = "cb_export"
        Me.cb_export.Size = New System.Drawing.Size(76, 24)
        Me.cb_export.TabIndex = 48
        '
        'btn_save_balanced
        '
        Me.btn_save_balanced.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_save_balanced.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save_balanced.Location = New System.Drawing.Point(21, 20)
        Me.btn_save_balanced.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_save_balanced.Name = "btn_save_balanced"
        Me.btn_save_balanced.Size = New System.Drawing.Size(119, 38)
        Me.btn_save_balanced.TabIndex = 1
        Me.btn_save_balanced.Text = "Save Balanced"
        Me.btn_save_balanced.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btn_import)
        Me.GroupBox4.Location = New System.Drawing.Point(16, 55)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Size = New System.Drawing.Size(141, 69)
        Me.GroupBox4.TabIndex = 47
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "File Upload"
        '
        'btn_import
        '
        Me.btn_import.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btn_import.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_import.Location = New System.Drawing.Point(29, 23)
        Me.btn_import.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_import.Name = "btn_import"
        Me.btn_import.Size = New System.Drawing.Size(85, 30)
        Me.btn_import.TabIndex = 1
        Me.btn_import.Text = "Import"
        Me.btn_import.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lb_total_balance)
        Me.GroupBox3.Controls.Add(Me.lb_test)
        Me.GroupBox3.Location = New System.Drawing.Point(452, 55)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(745, 70)
        Me.GroupBox3.TabIndex = 49
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Detail"
        '
        'lb_total_balance
        '
        Me.lb_total_balance.AutoSize = True
        Me.lb_total_balance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_total_balance.Location = New System.Drawing.Point(161, 30)
        Me.lb_total_balance.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_total_balance.Name = "lb_total_balance"
        Me.lb_total_balance.Size = New System.Drawing.Size(40, 18)
        Me.lb_total_balance.TabIndex = 7
        Me.lb_total_balance.Text = "0,00"
        '
        'lb_test
        '
        Me.lb_test.AutoSize = True
        Me.lb_test.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_test.Location = New System.Drawing.Point(12, 31)
        Me.lb_test.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_test.Name = "lb_test"
        Me.lb_test.Size = New System.Drawing.Size(92, 17)
        Me.lb_test.TabIndex = 6
        Me.lb_test.Text = "Total Amount"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'DetailBalanced
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 598)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "DetailBalanced"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DetailBalanced"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.load_balanced, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents load_balanced As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_save_balanced As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_import As System.Windows.Forms.Button
    Friend WithEvents cb_export As System.Windows.Forms.ComboBox
    Friend WithEvents C_rows1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lb_total_balance As System.Windows.Forms.Label
    Friend WithEvents lb_test As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
