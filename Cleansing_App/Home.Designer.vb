<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Home
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_checking = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_cleansing = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_cleansingloan = New System.Windows.Forms.ToolStripMenuItem()
        Me.PaymentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_report = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckMutasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckBalancedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MonitoringDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutAppToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem, Me.DatabaseToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1221, 28)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_checking, Me.m_cleansing, Me.m_cleansingloan, Me.PaymentToolStripMenuItem, Me.SplitDataToolStripMenuItem, Me.m_report, Me.CheckMutasiToolStripMenuItem, Me.RestartToolStripMenuItem, Me.m_exit})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(60, 24)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'm_checking
        '
        Me.m_checking.Name = "m_checking"
        Me.m_checking.Size = New System.Drawing.Size(234, 26)
        Me.m_checking.Text = "[1] Checking  CIF"
        '
        'm_cleansing
        '
        Me.m_cleansing.Name = "m_cleansing"
        Me.m_cleansing.Size = New System.Drawing.Size(234, 26)
        Me.m_cleansing.Text = "[2] Cleansing CIF"
        '
        'm_cleansingloan
        '
        Me.m_cleansingloan.Name = "m_cleansingloan"
        Me.m_cleansingloan.Size = New System.Drawing.Size(234, 26)
        Me.m_cleansingloan.Text = "[3] Join Loan && CIF"
        '
        'PaymentToolStripMenuItem
        '
        Me.PaymentToolStripMenuItem.Name = "PaymentToolStripMenuItem"
        Me.PaymentToolStripMenuItem.Size = New System.Drawing.Size(234, 26)
        Me.PaymentToolStripMenuItem.Text = "[4] Checking Payment"
        '
        'SplitDataToolStripMenuItem
        '
        Me.SplitDataToolStripMenuItem.Name = "SplitDataToolStripMenuItem"
        Me.SplitDataToolStripMenuItem.Size = New System.Drawing.Size(234, 26)
        Me.SplitDataToolStripMenuItem.Text = "[5] Split Data"
        '
        'm_report
        '
        Me.m_report.Name = "m_report"
        Me.m_report.Size = New System.Drawing.Size(234, 26)
        Me.m_report.Text = "[6] Report"
        '
        'CheckMutasiToolStripMenuItem
        '
        Me.CheckMutasiToolStripMenuItem.Name = "CheckMutasiToolStripMenuItem"
        Me.CheckMutasiToolStripMenuItem.Size = New System.Drawing.Size(234, 26)
        Me.CheckMutasiToolStripMenuItem.Text = "[7] Check Mutasi"
        '
        'RestartToolStripMenuItem
        '
        Me.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem"
        Me.RestartToolStripMenuItem.Size = New System.Drawing.Size(234, 26)
        Me.RestartToolStripMenuItem.Text = "Restart"
        '
        'm_exit
        '
        Me.m_exit.Name = "m_exit"
        Me.m_exit.Size = New System.Drawing.Size(234, 26)
        Me.m_exit.Text = "Exit"
        '
        'DatabaseToolStripMenuItem
        '
        Me.DatabaseToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CheckBalancedToolStripMenuItem, Me.MonitoringDatabaseToolStripMenuItem})
        Me.DatabaseToolStripMenuItem.Name = "DatabaseToolStripMenuItem"
        Me.DatabaseToolStripMenuItem.Size = New System.Drawing.Size(86, 24)
        Me.DatabaseToolStripMenuItem.Text = "Database"
        '
        'CheckBalancedToolStripMenuItem
        '
        Me.CheckBalancedToolStripMenuItem.Name = "CheckBalancedToolStripMenuItem"
        Me.CheckBalancedToolStripMenuItem.Size = New System.Drawing.Size(319, 26)
        Me.CheckBalancedToolStripMenuItem.Text = "[1] Monitoring Balanced Nasabah "
        '
        'MonitoringDatabaseToolStripMenuItem
        '
        Me.MonitoringDatabaseToolStripMenuItem.Name = "MonitoringDatabaseToolStripMenuItem"
        Me.MonitoringDatabaseToolStripMenuItem.Size = New System.Drawing.Size(319, 26)
        Me.MonitoringDatabaseToolStripMenuItem.Text = "[2] Monitoring Database"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutAppToolStripMenuItem})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'AboutAppToolStripMenuItem
        '
        Me.AboutAppToolStripMenuItem.Name = "AboutAppToolStripMenuItem"
        Me.AboutAppToolStripMenuItem.Size = New System.Drawing.Size(165, 26)
        Me.AboutAppToolStripMenuItem.Text = "About App"
        '
        'Home
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1221, 719)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Home"
        Me.Text = "Chanel Application"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_checking As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_cleansing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_report As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_cleansingloan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PaymentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckBalancedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutAppToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MonitoringDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckMutasiToolStripMenuItem As ToolStripMenuItem
End Class
