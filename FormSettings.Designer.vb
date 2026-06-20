<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSettings
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
        Me.grpProfile = New System.Windows.Forms.GroupBox()
        Me.LabelFileSize = New System.Windows.Forms.Label()
        Me.cmbFileSize = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblProfileDesc = New System.Windows.Forms.Label()
        Me.cmbProfile = New System.Windows.Forms.ComboBox()
        Me.grpConfig = New System.Windows.Forms.GroupBox()
        Me.nudCoolDown = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.nudWarmUp = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.nudDuration = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.nudReadPercent = New System.Windows.Forms.NumericUpDown()
        Me.lblReadPercent = New System.Windows.Forms.Label()
        Me.cmbTestType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPattern = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbThreads = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbQueueDepth = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbBlockSize = New System.Windows.Forms.ComboBox()
        Me.LabelBs = New System.Windows.Forms.Label()
        Me.btnAddTest = New System.Windows.Forms.Button()
        Me.btnUpdateTest = New System.Windows.Forms.Button()
        Me.grpTestList = New System.Windows.Forms.GroupBox()
        Me.lvTests = New System.Windows.Forms.ListView()
        Me.pnlListButtons = New System.Windows.Forms.Panel()
        Me.btnDuplicate = New System.Windows.Forms.Button()
        Me.btnClearAll = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnResetDefault = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpProfile.SuspendLayout()
        Me.grpConfig.SuspendLayout()
        CType(Me.nudCoolDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWarmUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudReadPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTestList.SuspendLayout()
        Me.pnlListButtons.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpProfile
        '
        Me.grpProfile.Controls.Add(Me.Label1)
        Me.grpProfile.Controls.Add(Me.LabelFileSize)
        Me.grpProfile.Controls.Add(Me.cmbFileSize)
        Me.grpProfile.Controls.Add(Me.Label2)
        Me.grpProfile.Controls.Add(Me.lblProfileDesc)
        Me.grpProfile.Controls.Add(Me.cmbProfile)
        Me.grpProfile.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpProfile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpProfile.Location = New System.Drawing.Point(0, 0)
        Me.grpProfile.Name = "grpProfile"
        Me.grpProfile.Size = New System.Drawing.Size(698, 78)
        Me.grpProfile.TabIndex = 0
        Me.grpProfile.TabStop = False
        Me.grpProfile.Text = "Profile Preset"
        '
        'LabelFileSize
        '
        Me.LabelFileSize.AutoSize = True
        Me.LabelFileSize.BackColor = System.Drawing.Color.Transparent
        Me.LabelFileSize.Location = New System.Drawing.Point(12, 50)
        Me.LabelFileSize.Name = "LabelFileSize"
        Me.LabelFileSize.Size = New System.Drawing.Size(57, 15)
        Me.LabelFileSize.TabIndex = 4
        Me.LabelFileSize.Text = "File Size:"
        '
        'cmbFileSize
        '
        Me.cmbFileSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFileSize.Items.AddRange(New Object() {"16 MB", "32 MB", "64 MB", "128 MB", "256 MB", "512 MB", "1 GB", "2 GB", "4 GB", "8 GB", "16 GB", "32 GB", "64 GB"})
        Me.cmbFileSize.Location = New System.Drawing.Point(87, 47)
        Me.cmbFileSize.Name = "cmbFileSize"
        Me.cmbFileSize.Size = New System.Drawing.Size(155, 23)
        Me.cmbFileSize.TabIndex = 3
        Me.cmbFileSize.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Drive Type:"
        '
        'lblProfileDesc
        '
        Me.lblProfileDesc.AutoSize = True
        Me.lblProfileDesc.BackColor = System.Drawing.Color.Transparent
        Me.lblProfileDesc.Location = New System.Drawing.Point(248, 22)
        Me.lblProfileDesc.Name = "lblProfileDesc"
        Me.lblProfileDesc.Size = New System.Drawing.Size(414, 15)
        Me.lblProfileDesc.TabIndex = 1
        Me.lblProfileDesc.Text = "Select a preset profile for your drive type. Tests will be loaded automatically."
        '
        'cmbProfile
        '
        Me.cmbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProfile.Location = New System.Drawing.Point(87, 19)
        Me.cmbProfile.Name = "cmbProfile"
        Me.cmbProfile.Size = New System.Drawing.Size(155, 23)
        Me.cmbProfile.TabIndex = 0
        Me.cmbProfile.TabStop = False
        '
        'grpConfig
        '
        Me.grpConfig.Controls.Add(Me.nudCoolDown)
        Me.grpConfig.Controls.Add(Me.Label9)
        Me.grpConfig.Controls.Add(Me.nudWarmUp)
        Me.grpConfig.Controls.Add(Me.Label8)
        Me.grpConfig.Controls.Add(Me.nudDuration)
        Me.grpConfig.Controls.Add(Me.Label7)
        Me.grpConfig.Controls.Add(Me.nudReadPercent)
        Me.grpConfig.Controls.Add(Me.lblReadPercent)
        Me.grpConfig.Controls.Add(Me.cmbTestType)
        Me.grpConfig.Controls.Add(Me.Label6)
        Me.grpConfig.Controls.Add(Me.cmbPattern)
        Me.grpConfig.Controls.Add(Me.Label5)
        Me.grpConfig.Controls.Add(Me.cmbThreads)
        Me.grpConfig.Controls.Add(Me.Label4)
        Me.grpConfig.Controls.Add(Me.cmbQueueDepth)
        Me.grpConfig.Controls.Add(Me.Label3)
        Me.grpConfig.Controls.Add(Me.cmbBlockSize)
        Me.grpConfig.Controls.Add(Me.LabelBs)
        Me.grpConfig.Controls.Add(Me.btnAddTest)
        Me.grpConfig.Controls.Add(Me.btnUpdateTest)
        Me.grpConfig.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpConfig.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpConfig.Location = New System.Drawing.Point(0, 78)
        Me.grpConfig.Name = "grpConfig"
        Me.grpConfig.Size = New System.Drawing.Size(698, 170)
        Me.grpConfig.TabIndex = 1
        Me.grpConfig.TabStop = False
        Me.grpConfig.Text = "Test Configuration"
        '
        'nudCoolDown
        '
        Me.nudCoolDown.Location = New System.Drawing.Point(640, 130)
        Me.nudCoolDown.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.nudCoolDown.Name = "nudCoolDown"
        Me.nudCoolDown.Size = New System.Drawing.Size(42, 21)
        Me.nudCoolDown.TabIndex = 16
        Me.nudCoolDown.TabStop = False
        Me.nudCoolDown.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(568, 133)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 15)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Cool Down:"
        '
        'nudWarmUp
        '
        Me.nudWarmUp.Location = New System.Drawing.Point(640, 100)
        Me.nudWarmUp.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.nudWarmUp.Name = "nudWarmUp"
        Me.nudWarmUp.Size = New System.Drawing.Size(42, 21)
        Me.nudWarmUp.TabIndex = 14
        Me.nudWarmUp.TabStop = False
        Me.nudWarmUp.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(576, 103)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 15)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Warm Up:"
        '
        'nudDuration
        '
        Me.nudDuration.Location = New System.Drawing.Point(640, 70)
        Me.nudDuration.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.nudDuration.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudDuration.Name = "nudDuration"
        Me.nudDuration.Size = New System.Drawing.Size(42, 21)
        Me.nudDuration.TabIndex = 12
        Me.nudDuration.TabStop = False
        Me.nudDuration.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(580, 73)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 15)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Duration:"
        '
        'nudReadPercent
        '
        Me.nudReadPercent.Enabled = False
        Me.nudReadPercent.Location = New System.Drawing.Point(487, 130)
        Me.nudReadPercent.Name = "nudReadPercent"
        Me.nudReadPercent.Size = New System.Drawing.Size(42, 21)
        Me.nudReadPercent.TabIndex = 10
        Me.nudReadPercent.TabStop = False
        Me.nudReadPercent.Value = New Decimal(New Integer() {70, 0, 0, 0})
        '
        'lblReadPercent
        '
        Me.lblReadPercent.AutoSize = True
        Me.lblReadPercent.BackColor = System.Drawing.Color.Transparent
        Me.lblReadPercent.Enabled = False
        Me.lblReadPercent.Location = New System.Drawing.Point(380, 133)
        Me.lblReadPercent.Name = "lblReadPercent"
        Me.lblReadPercent.Size = New System.Drawing.Size(99, 15)
        Me.lblReadPercent.TabIndex = 9
        Me.lblReadPercent.Text = "Read % (Mixed):"
        '
        'cmbTestType
        '
        Me.cmbTestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTestType.Items.AddRange(New Object() {"Read", "Write", "Mixed"})
        Me.cmbTestType.Location = New System.Drawing.Point(487, 100)
        Me.cmbTestType.Name = "cmbTestType"
        Me.cmbTestType.Size = New System.Drawing.Size(83, 23)
        Me.cmbTestType.TabIndex = 8
        Me.cmbTestType.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(432, 103)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 15)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "R/W:"
        '
        'cmbPattern
        '
        Me.cmbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPattern.Items.AddRange(New Object() {"Sequential", "Random"})
        Me.cmbPattern.Location = New System.Drawing.Point(487, 70)
        Me.cmbPattern.Name = "cmbPattern"
        Me.cmbPattern.Size = New System.Drawing.Size(83, 23)
        Me.cmbPattern.TabIndex = 6
        Me.cmbPattern.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(432, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Pattern:"
        '
        'cmbThreads
        '
        Me.cmbThreads.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbThreads.Items.AddRange(New Object() {"1", "2", "4", "8", "16"})
        Me.cmbThreads.Location = New System.Drawing.Point(339, 70)
        Me.cmbThreads.Name = "cmbThreads"
        Me.cmbThreads.Size = New System.Drawing.Size(60, 23)
        Me.cmbThreads.TabIndex = 4
        Me.cmbThreads.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(289, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Thread:"
        '
        'cmbQueueDepth
        '
        Me.cmbQueueDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQueueDepth.Items.AddRange(New Object() {"1", "2", "4", "8", "16", "32", "64", "128"})
        Me.cmbQueueDepth.Location = New System.Drawing.Point(221, 70)
        Me.cmbQueueDepth.Name = "cmbQueueDepth"
        Me.cmbQueueDepth.Size = New System.Drawing.Size(60, 23)
        Me.cmbQueueDepth.TabIndex = 2
        Me.cmbQueueDepth.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(173, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Queue:"
        '
        'cmbBlockSize
        '
        Me.cmbBlockSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBlockSize.Items.AddRange(New Object() {"512", "1K", "2K", "4K", "8K", "16K", "32K", "64K", "128K", "256K", "512K", "1M"})
        Me.cmbBlockSize.Location = New System.Drawing.Point(80, 70)
        Me.cmbBlockSize.Name = "cmbBlockSize"
        Me.cmbBlockSize.Size = New System.Drawing.Size(80, 23)
        Me.cmbBlockSize.TabIndex = 0
        Me.cmbBlockSize.TabStop = False
        '
        'LabelBs
        '
        Me.LabelBs.AutoSize = True
        Me.LabelBs.BackColor = System.Drawing.Color.Transparent
        Me.LabelBs.Location = New System.Drawing.Point(12, 73)
        Me.LabelBs.Name = "LabelBs"
        Me.LabelBs.Size = New System.Drawing.Size(67, 15)
        Me.LabelBs.TabIndex = 0
        Me.LabelBs.Text = "Block Size:"
        '
        'btnAddTest
        '
        Me.btnAddTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddTest.Location = New System.Drawing.Point(497, 30)
        Me.btnAddTest.Name = "btnAddTest"
        Me.btnAddTest.Size = New System.Drawing.Size(80, 28)
        Me.btnAddTest.TabIndex = 17
        Me.btnAddTest.TabStop = False
        Me.btnAddTest.Text = "Add Test"
        Me.btnAddTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddTest.UseVisualStyleBackColor = True
        '
        'btnUpdateTest
        '
        Me.btnUpdateTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdateTest.Location = New System.Drawing.Point(583, 30)
        Me.btnUpdateTest.Name = "btnUpdateTest"
        Me.btnUpdateTest.Size = New System.Drawing.Size(95, 28)
        Me.btnUpdateTest.TabIndex = 18
        Me.btnUpdateTest.TabStop = False
        Me.btnUpdateTest.Text = "Update Sel."
        Me.btnUpdateTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnUpdateTest.UseVisualStyleBackColor = True
        '
        'grpTestList
        '
        Me.grpTestList.Controls.Add(Me.lvTests)
        Me.grpTestList.Controls.Add(Me.pnlListButtons)
        Me.grpTestList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpTestList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTestList.Location = New System.Drawing.Point(0, 248)
        Me.grpTestList.Name = "grpTestList"
        Me.grpTestList.Size = New System.Drawing.Size(698, 202)
        Me.grpTestList.TabIndex = 2
        Me.grpTestList.TabStop = False
        Me.grpTestList.Text = "Test List"
        '
        'lvTests
        '
        Me.lvTests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTests.FullRowSelect = True
        Me.lvTests.GridLines = True
        Me.lvTests.HideSelection = False
        Me.lvTests.Location = New System.Drawing.Point(3, 17)
        Me.lvTests.MultiSelect = False
        Me.lvTests.Name = "lvTests"
        Me.lvTests.Size = New System.Drawing.Size(692, 151)
        Me.lvTests.TabIndex = 0
        Me.lvTests.UseCompatibleStateImageBehavior = False
        Me.lvTests.View = System.Windows.Forms.View.Details
        '
        'pnlListButtons
        '
        Me.pnlListButtons.Controls.Add(Me.btnDuplicate)
        Me.pnlListButtons.Controls.Add(Me.btnClearAll)
        Me.pnlListButtons.Controls.Add(Me.btnMoveDown)
        Me.pnlListButtons.Controls.Add(Me.btnMoveUp)
        Me.pnlListButtons.Controls.Add(Me.btnRemove)
        Me.pnlListButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlListButtons.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlListButtons.Location = New System.Drawing.Point(3, 168)
        Me.pnlListButtons.Name = "pnlListButtons"
        Me.pnlListButtons.Size = New System.Drawing.Size(692, 31)
        Me.pnlListButtons.TabIndex = 1
        '
        'btnDuplicate
        '
        Me.btnDuplicate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDuplicate.Location = New System.Drawing.Point(365, 3)
        Me.btnDuplicate.Name = "btnDuplicate"
        Me.btnDuplicate.Size = New System.Drawing.Size(77, 24)
        Me.btnDuplicate.TabIndex = 4
        Me.btnDuplicate.TabStop = False
        Me.btnDuplicate.Text = "Duplicate"
        Me.btnDuplicate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDuplicate.UseVisualStyleBackColor = True
        '
        'btnClearAll
        '
        Me.btnClearAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearAll.Location = New System.Drawing.Point(290, 3)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(70, 24)
        Me.btnClearAll.TabIndex = 3
        Me.btnClearAll.TabStop = False
        Me.btnClearAll.Text = "Clear All"
        Me.btnClearAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearAll.UseVisualStyleBackColor = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMoveDown.Location = New System.Drawing.Point(193, 3)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(91, 24)
        Me.btnMoveDown.TabIndex = 2
        Me.btnMoveDown.TabStop = False
        Me.btnMoveDown.Text = "Move Down"
        Me.btnMoveDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMoveUp.Location = New System.Drawing.Point(113, 3)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(75, 24)
        Me.btnMoveUp.TabIndex = 1
        Me.btnMoveUp.TabStop = False
        Me.btnMoveUp.Text = "Move Up"
        Me.btnMoveUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemove.Location = New System.Drawing.Point(12, 3)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(95, 24)
        Me.btnRemove.TabIndex = 0
        Me.btnRemove.TabStop = False
        Me.btnRemove.Text = "Remove Test"
        Me.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnResetDefault)
        Me.pnlBottom.Controls.Add(Me.btnCancel)
        Me.pnlBottom.Controls.Add(Me.btnOK)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 450)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(698, 38)
        Me.pnlBottom.TabIndex = 3
        '
        'btnResetDefault
        '
        Me.btnResetDefault.Location = New System.Drawing.Point(12, 7)
        Me.btnResetDefault.Name = "btnResetDefault"
        Me.btnResetDefault.Size = New System.Drawing.Size(95, 26)
        Me.btnResetDefault.TabIndex = 2
        Me.btnResetDefault.TabStop = False
        Me.btnResetDefault.Text = "Reset Default"
        Me.btnResetDefault.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(570, 7)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(108, 26)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOK.Location = New System.Drawing.Point(480, 7)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(84, 26)
        Me.btnOK.TabIndex = 0
        Me.btnOK.TabStop = False
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(248, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 15)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select the file size used for testing."
        '
        'FormSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 488)
        Me.Controls.Add(Me.grpTestList)
        Me.Controls.Add(Me.grpConfig)
        Me.Controls.Add(Me.grpProfile)
        Me.Controls.Add(Me.pnlBottom)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(500, 400)
        Me.Name = "FormSettings"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Benchmark Settings"
        Me.TopMost = True
        Me.grpProfile.ResumeLayout(False)
        Me.grpProfile.PerformLayout()
        Me.grpConfig.ResumeLayout(False)
        Me.grpConfig.PerformLayout()
        CType(Me.nudCoolDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWarmUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudDuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudReadPercent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTestList.ResumeLayout(False)
        Me.pnlListButtons.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpProfile As System.Windows.Forms.GroupBox
    Friend WithEvents cmbFileSize As System.Windows.Forms.ComboBox
    Friend WithEvents LabelFileSize As System.Windows.Forms.Label
    Friend WithEvents cmbProfile As System.Windows.Forms.ComboBox
    Friend WithEvents lblProfileDesc As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpConfig As System.Windows.Forms.GroupBox
    Friend WithEvents nudCoolDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nudWarmUp As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents nudDuration As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents nudReadPercent As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblReadPercent As System.Windows.Forms.Label
    Friend WithEvents cmbTestType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPattern As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbThreads As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbQueueDepth As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbBlockSize As System.Windows.Forms.ComboBox
    Friend WithEvents LabelBs As System.Windows.Forms.Label
    Friend WithEvents grpTestList As System.Windows.Forms.GroupBox
    Friend WithEvents pnlListButtons As System.Windows.Forms.Panel
    Friend WithEvents btnDuplicate As System.Windows.Forms.Button
    Friend WithEvents btnClearAll As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents lvTests As System.Windows.Forms.ListView
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnResetDefault As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnAddTest As System.Windows.Forms.Button
    Friend WithEvents btnUpdateTest As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class