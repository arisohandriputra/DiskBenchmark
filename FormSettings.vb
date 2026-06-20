Imports System
Imports System.Collections.Generic
Imports System.IO
Public Class FormSettings
    Private _tests As New List(Of Form1.TestConfig)
    Private _loadingProfile As Boolean = False
    Private _testFileSize As String = "1G"

    ' =========================================================
    '  PUBLIC PROPERTIES
    ' =========================================================
    Public Property TestFileSize() As String
        Get
            Return _testFileSize
        End Get
        Set(ByVal value As String)
            _testFileSize = value
        End Set
    End Property

    ' =========================================================
    '  PUBLIC METHODS — called from Form1
    ' =========================================================
    Public Sub SetTests(ByVal tests As List(Of Form1.TestConfig))
        _tests.Clear()
        Dim tc As Form1.TestConfig
        For Each tc In tests
            _tests.Add(tc)
        Next
        RefreshTestList()
    End Sub

    Public Sub SetFileSize(ByVal fileSizeArg As String)
        _testFileSize = fileSizeArg
        cmbFileSize.SelectedIndex = ConvertArgToFileSizeDisplay(fileSizeArg)
    End Sub

    Public Function GetTests() As List(Of Form1.TestConfig)
        Dim result As New List(Of Form1.TestConfig)
        Dim tc As Form1.TestConfig
        For Each tc In _tests
            result.Add(tc)
        Next
        Return result
    End Function

    ' =========================================================
    '  FORM LOAD
    ' =========================================================
    Sub ui()

        Dim requiredFiles() As String = {
            "settings\add.dll",
            "settings\save.dll",
            "settings\ok.dll",
            "settings\cancel.dll",
            "settings\close.dll",
            "settings\up.dll",
            "settings\down.dll",
            "settings\delete.dll",
            "settings\texture2.dll",
            "settings\copy.dll"
        }

        For Each file As String In requiredFiles
            If Not IO.File.Exists(IO.Path.Combine(Application.StartupPath, file)) Then
                MsgBox("Required file not found:" & vbCrLf & vbCrLf & _
                       file & vbCrLf & vbCrLf & _
                       "Please reinstall the application.",
                       MsgBoxStyle.Critical,
                       "Missing File")
                Me.Close()
                Exit Sub
            End If
        Next

        btnAddTest.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\add.dll"))
        btnUpdateTest.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\save.dll"))
        btnOK.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\ok.dll"))
        btnCancel.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\cancel.dll"))
        btnRemove.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\close.dll"))
        btnMoveUp.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\up.dll"))
        btnMoveDown.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\down.dll"))
        btnClearAll.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\delete.dll"))
        btnDuplicate.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings\copy.dll"))
        Me.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings\texture2.dll"))
        grpConfig.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings\texture2.dll"))
        grpProfile.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings\texture2.dll"))
        grpTestList.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings\texture2.dll"))
        pnlListButtons.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings\texture2.dll"))
    End Sub
    Private Sub FormSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Profile combo
        ui()
        cmbProfile.Items.Clear()
        cmbProfile.Items.Add("HDD")
        cmbProfile.Items.Add("SSD SATA/M.2 SATA")
        cmbProfile.Items.Add("SSD NVme Gen(3,4,5)")
        cmbProfile.Items.Add("Flash Drive")
        cmbProfile.Items.Add("SD Card/MicroSD")
        cmbProfile.Items.Add("Custom")
        cmbProfile.SelectedIndex = 0

        ' File size combo
        cmbFileSize.SelectedIndex = 2  ' 1 GB

        ' ListView columns
        lvTests.Columns.Clear()
        lvTests.Columns.Add("#", 30)
        lvTests.Columns.Add("Description", 230)
        lvTests.Columns.Add("Block", 50)
        lvTests.Columns.Add("QD", 40)
        lvTests.Columns.Add("Thr", 40)
        lvTests.Columns.Add("Type", 60)
        lvTests.Columns.Add("Time", 50)

        ' Default config selections
        cmbBlockSize.SelectedIndex = 8   ' 128K
        cmbQueueDepth.SelectedIndex = 3  ' 8
        cmbThreads.SelectedIndex = 2     ' 4
        cmbPattern.SelectedIndex = 0     ' Sequential
        cmbTestType.SelectedIndex = 0    ' Read
    End Sub

    ' =========================================================
    '  PROFILE COMBO
    ' =========================================================
    Private Sub cmbProfile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProfile.SelectedIndexChanged
        If _loadingProfile Then Return
        _loadingProfile = True
        Dim idx As Integer = cmbProfile.SelectedIndex
        Select Case idx
            Case 0
                LoadHDD()
            Case 1
                LoadSataSSD()
            Case 2
                LoadNVMe()
            Case 3
                LoadFlashDrive()
            Case 4
                LoadMemoryCard()
            Case 5
        End Select
        _loadingProfile = False
    End Sub

    ' =========================================================
    '  PROFILE DEFINITIONS
    ' =========================================================
    Private Sub LoadHDD()

        _tests.Clear()

        _tests.Add(MkTC("Seq Read  128K Q1T1", "128K", 1, 1, True, "Read", 100, 30, 5, 5, "Sequential"))
        _tests.Add(MkTC("Seq Write 128K Q1T1", "128K", 1, 1, True, "Write", 0, 30, 5, 5, "Sequential"))

        _tests.Add(MkTC("Rnd Read  4K  Q1T1", "4K", 1, 1, False, "Read", 100, 30, 5, 5, "Random"))
        _tests.Add(MkTC("Rnd Write 4K  Q1T1", "4K", 1, 1, False, "Write", 0, 30, 5, 5, "Random"))

        _tests.Add(MkTC("Mixed 70R/30W 4K Q1T1", "4K", 1, 1, False, "Mixed", 70, 30, 5, 5, "Mixed"))

        RefreshTestList()

    End Sub

    Private Sub LoadSataSSD()

        _tests.Clear()

        _tests.Add(MkTC("Seq Read  1M Q8T1", "1M", 8, 1, True, "Read", 100, 30, 5, 5, "Sequential"))
        _tests.Add(MkTC("Seq Write 1M Q8T1", "1M", 8, 1, True, "Write", 0, 30, 5, 5, "Sequential"))

        _tests.Add(MkTC("Rnd Read  4K Q1T1", "4K", 1, 1, False, "Read", 100, 30, 5, 5, "Random"))
        _tests.Add(MkTC("Rnd Write 4K Q1T1", "4K", 1, 1, False, "Write", 0, 30, 5, 5, "Random"))

        _tests.Add(MkTC("Rnd Read  4K Q32T1", "4K", 32, 1, False, "Read", 100, 30, 5, 5, "Random"))
        _tests.Add(MkTC("Rnd Write 4K Q32T1", "4K", 32, 1, False, "Write", 0, 30, 5, 5, "Random"))

        _tests.Add(MkTC("Mixed 70R/30W 4K Q8T1", "4K", 8, 1, False, "Mixed", 70, 30, 5, 5, "Mixed"))

        RefreshTestList()

    End Sub
    Private Sub LoadNVMe()

        _tests.Clear()

        _tests.Add(MkTC("Seq Read  1M Q32T1", "1M", 32, 1, True, "Read", 100, 30, 5, 5, "Sequential"))
        _tests.Add(MkTC("Seq Write 1M Q32T1", "1M", 32, 1, True, "Write", 0, 30, 5, 5, "Sequential"))

        _tests.Add(MkTC("Rnd Read  4K Q1T1", "4K", 1, 1, False, "Read", 100, 30, 5, 5, "Random"))
        _tests.Add(MkTC("Rnd Write 4K Q1T1", "4K", 1, 1, False, "Write", 0, 30, 5, 5, "Random"))

        _tests.Add(MkTC("Rnd Read  4K Q64T1", "4K", 64, 1, False, "Read", 100, 30, 5, 5, "Random"))
        _tests.Add(MkTC("Rnd Write 4K Q64T1", "4K", 64, 1, False, "Write", 0, 30, 5, 5, "Random"))

        _tests.Add(MkTC("Mixed 70R/30W 4K Q32T1", "4K", 32, 1, False, "Mixed", 70, 30, 5, 5, "Mixed"))

        RefreshTestList()

    End Sub
    Private Sub LoadSSD()

        _tests.Clear()

        _tests.Add(MkTC("Seq Read  1M Q8T1", "1M", 8, 1, True, "Read", 100, 30, 5, 5, "Sequential"))
        _tests.Add(MkTC("Seq Write 1M Q8T1", "1M", 8, 1, True, "Write", 0, 30, 5, 5, "Sequential"))

        _tests.Add(MkTC("Rnd Read  4K Q1T1", "4K", 1, 1, False, "Read", 100, 30, 5, 5, "Random"))
        _tests.Add(MkTC("Rnd Write 4K Q1T1", "4K", 1, 1, False, "Write", 0, 30, 5, 5, "Random"))

        _tests.Add(MkTC("Rnd Read  4K Q32T1", "4K", 32, 1, False, "Read", 100, 30, 5, 5, "Random"))
        _tests.Add(MkTC("Rnd Write 4K Q32T1", "4K", 32, 1, False, "Write", 0, 30, 5, 5, "Random"))

        _tests.Add(MkTC("Mixed 70R/30W 4K Q8T1", "4K", 8, 1, False, "Mixed", 70, 30, 5, 5, "Mixed"))

        RefreshTestList()

    End Sub

    Private Sub LoadFlashDrive()

        _tests.Clear()

        _tests.Add(MkTC("Seq Read  128K Q1T1", "128K", 1, 1, True, "Read", 100, 20, 3, 3, "Sequential"))
        _tests.Add(MkTC("Seq Write 128K Q1T1", "128K", 1, 1, True, "Write", 0, 20, 3, 3, "Sequential"))

        _tests.Add(MkTC("Rnd Read  4K Q1T1", "4K", 1, 1, False, "Read", 100, 20, 3, 3, "Random"))
        _tests.Add(MkTC("Rnd Write 4K Q1T1", "4K", 1, 1, False, "Write", 0, 20, 3, 3, "Random"))

        _tests.Add(MkTC("Mixed 70R/30W 4K Q1T1", "4K", 1, 1, False, "Mixed", 70, 30, 3, 3, "Mixed"))

        RefreshTestList()

    End Sub

    Private Sub LoadMemoryCard()

        _tests.Clear()

        _tests.Add(MkTC("Seq Read  128K Q1T1", "128K", 1, 1, True, "Read", 100, 20, 3, 3, "Sequential"))
        _tests.Add(MkTC("Seq Write 128K Q1T1", "128K", 1, 1, True, "Write", 0, 20, 3, 3, "Sequential"))

        _tests.Add(MkTC("Rnd Read  4K Q1T1", "4K", 1, 1, False, "Read", 100, 20, 3, 3, "Random"))
        _tests.Add(MkTC("Rnd Write 4K Q1T1", "4K", 1, 1, False, "Write", 0, 20, 3, 3, "Random"))

        RefreshTestList()

    End Sub

    Private Function MkTC(ByVal name As String, ByVal bs As String, ByVal qd As Integer, ByVal thr As Integer, ByVal seq As Boolean, ByVal tt As String, ByVal rp As Integer, ByVal dur As Integer, ByVal wu As Integer, ByVal cd As Integer, ByVal cat As String) As Form1.TestConfig
        Dim tc As New Form1.TestConfig
        tc.Name = name
        tc.BlockSize = bs
        tc.QueueDepth = qd
        tc.Threads = thr
        tc.IsSequential = seq
        tc.TestType = tt
        tc.ReadPercent = rp
        tc.Duration = dur
        tc.WarmUp = wu
        tc.CoolDown = cd
        tc.Category = cat
        Return tc
    End Function

    ' =========================================================
    '  TEST LIST (ListView)
    ' =========================================================
    Private Sub RefreshTestList()
        lvTests.Items.Clear()
        Dim idx As Integer = 0
        Dim tc As Form1.TestConfig
        For Each tc In _tests
            idx += 1
            Dim lvi As New ListViewItem(idx.ToString())
            lvi.SubItems.Add(tc.Name)
            lvi.SubItems.Add(tc.BlockSize)
            lvi.SubItems.Add(tc.QueueDepth.ToString())
            lvi.SubItems.Add(tc.Threads.ToString())
            lvi.SubItems.Add(tc.TestType)
            lvi.SubItems.Add(tc.Duration.ToString() & "s")
            lvTests.Items.Add(lvi)
        Next
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If lvTests.SelectedIndices.Count > 0 Then
            Dim idx As Integer = lvTests.SelectedIndices(0)
            If idx >= 0 AndAlso idx < _tests.Count Then
                _tests.RemoveAt(idx)
                RefreshTestList()
            End If
        End If
    End Sub

    Private Sub btnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
        If lvTests.SelectedIndices.Count > 0 Then
            Dim idx As Integer = lvTests.SelectedIndices(0)
            If idx > 0 Then
                Dim temp As Form1.TestConfig = _tests(idx - 1)
                _tests(idx - 1) = _tests(idx)
                _tests(idx) = temp
                RefreshTestList()
                lvTests.Items(idx - 1).Selected = True
                lvTests.EnsureVisible(idx - 1)
            End If
        End If
    End Sub

    Private Sub btnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        If lvTests.SelectedIndices.Count > 0 Then
            Dim idx As Integer = lvTests.SelectedIndices(0)
            If idx < _tests.Count - 1 Then
                Dim temp As Form1.TestConfig = _tests(idx + 1)
                _tests(idx + 1) = _tests(idx)
                _tests(idx) = temp
                RefreshTestList()
                lvTests.Items(idx + 1).Selected = True
                lvTests.EnsureVisible(idx + 1)
            End If
        End If
    End Sub

    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        _tests.Clear()
        RefreshTestList()
        cmbProfile.SelectedIndex = 3 ' Switch to Custom
    End Sub

    ' =========================================================
    '  ADD TEST FROM CURRENT CONFIG
    ' =========================================================
    Private Sub btnAddTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTest.Click
        Dim tc As New Form1.TestConfig
        tc.BlockSize = cmbBlockSize.Text
        tc.QueueDepth = CInt(cmbQueueDepth.Text)
        tc.Threads = CInt(cmbThreads.Text)
        tc.IsSequential = (cmbPattern.Text = "Sequential")
        tc.TestType = cmbTestType.Text
        tc.ReadPercent = CInt(nudReadPercent.Value)
        tc.Duration = CInt(nudDuration.Value)
        tc.WarmUp = CInt(nudWarmUp.Value)
        tc.CoolDown = CInt(nudCoolDown.Value)
        tc.Name = tc.BuildDisplayName()

        If tc.TestType = "Read" Or tc.TestType = "Write" Then
            tc.Category = IIf(tc.IsSequential, "Sequential", "Random")
        Else
            tc.Category = "Mixed"
        End If

        _tests.Add(tc)
        RefreshTestList()
        cmbProfile.SelectedIndex = 3 ' Switch to Custom

        ' Auto-scroll to the new item
        If lvTests.Items.Count > 0 Then
            Dim lastIdx As Integer = lvTests.Items.Count - 1
            lvTests.Items(lastIdx).Selected = True
            lvTests.EnsureVisible(lastIdx)
        End If
    End Sub

    ' =========================================================
    '  TEST TYPE CHANGE — show/hide read percent
    ' =========================================================
    Private Sub cmbTestType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTestType.SelectedIndexChanged
        If cmbTestType.Text = "Mixed" Then
            lblReadPercent.Enabled = True
            nudReadPercent.Enabled = True
        Else
            lblReadPercent.Enabled = False
            nudReadPercent.Enabled = False
        End If
    End Sub

    ' =========================================================
    '  SELECT TEST IN LIST → LOAD INTO CONFIG PANEL
    ' =========================================================
    Private Sub lvTests_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvTests.SelectedIndexChanged
        If lvTests.SelectedIndices.Count = 0 Then Return
        Dim idx As Integer = lvTests.SelectedIndices(0)
        If idx < 0 Or idx >= _tests.Count Then Return

        Dim tc As Form1.TestConfig = _tests(idx)

        ' Block Size
        Dim bsIdx As Integer = cmbBlockSize.FindStringExact(tc.BlockSize)
        If bsIdx >= 0 Then cmbBlockSize.SelectedIndex = bsIdx

        ' Queue Depth
        Dim qdIdx As Integer = cmbQueueDepth.FindStringExact(tc.QueueDepth.ToString())
        If qdIdx >= 0 Then cmbQueueDepth.SelectedIndex = qdIdx

        ' Threads
        Dim thIdx As Integer = cmbThreads.FindStringExact(tc.Threads.ToString())
        If thIdx >= 0 Then cmbThreads.SelectedIndex = thIdx

        ' Pattern
        If tc.IsSequential Then
            cmbPattern.SelectedIndex = 0
        Else
            cmbPattern.SelectedIndex = 1
        End If

        ' Test Type
        Dim ttIdx As Integer = cmbTestType.FindStringExact(tc.TestType)
        If ttIdx >= 0 Then cmbTestType.SelectedIndex = ttIdx

        ' Read Percent
        nudReadPercent.Value = tc.ReadPercent

        ' Duration
        nudDuration.Value = tc.Duration
        nudWarmUp.Value = tc.WarmUp
        nudCoolDown.Value = tc.CoolDown
    End Sub

    ' =========================================================
    '  UPDATE SELECTED TEST (after editing config)
    ' =========================================================
    Private Sub btnUpdateTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateTest.Click
        If lvTests.SelectedIndices.Count = 0 Then
            MsgBox("Select a test in the list first.", MsgBoxStyle.Information)
            Return
        End If
        Dim idx As Integer = lvTests.SelectedIndices(0)
        If idx < 0 Or idx >= _tests.Count Then Return

        Dim tc As New Form1.TestConfig
        tc.BlockSize = cmbBlockSize.Text
        tc.QueueDepth = CInt(cmbQueueDepth.Text)
        tc.Threads = CInt(cmbThreads.Text)
        tc.IsSequential = (cmbPattern.Text = "Sequential")
        tc.TestType = cmbTestType.Text
        tc.ReadPercent = CInt(nudReadPercent.Value)
        tc.Duration = CInt(nudDuration.Value)
        tc.WarmUp = CInt(nudWarmUp.Value)
        tc.CoolDown = CInt(nudCoolDown.Value)
        tc.Name = tc.BuildDisplayName()

        If tc.TestType = "Read" Or tc.TestType = "Write" Then
            tc.Category = IIf(tc.IsSequential, "Sequential", "Random")
        Else
            tc.Category = "Mixed"
        End If

        _tests(idx) = tc
        RefreshTestList()
        lvTests.Items(idx).Selected = True
        cmbProfile.SelectedIndex = 3
    End Sub

    ' =========================================================
    '  DUPLICATE SELECTED TEST
    ' =========================================================
    Private Sub btnDuplicate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDuplicate.Click
        If lvTests.SelectedIndices.Count = 0 Then Return
        Dim idx As Integer = lvTests.SelectedIndices(0)
        If idx < 0 Or idx >= _tests.Count Then Return

        Dim copy As Form1.TestConfig = _tests(idx)
        _tests.Insert(idx + 1, copy)
        RefreshTestList()
        lvTests.Items(idx + 1).Selected = True
        cmbProfile.SelectedIndex = 3
    End Sub

    ' =========================================================
    '  FILE SIZE COMBO
    ' =========================================================
    Private Sub cmbFileSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFileSize.SelectedIndexChanged
        _testFileSize = ConvertFileSizeToArg(cmbFileSize.Text)
    End Sub

    Private Function ConvertFileSizeToArg(ByVal displayText As String) As String
        Select Case displayText
            Case "16 MB" : Return "16M"
            Case "32 MB" : Return "32M"
            Case "64 MB" : Return "64M"
            Case "128 MB" : Return "128M"
            Case "256 MB" : Return "256M"
            Case "512 MB" : Return "512M"
            Case "1 GB" : Return "1G"
            Case "2 GB" : Return "2G"
            Case "4 GB" : Return "4G"
            Case "8 GB" : Return "8G"
            Case "16 GB" : Return "16G"
            Case "32 GB" : Return "32G"
            Case "64 GB" : Return "64G"
            Case Else : Return "64M"
        End Select
    End Function

    Private Function ConvertArgToFileSizeDisplay(ByVal arg As String) As Integer
        Select Case arg
            Case "16M" : Return 0
            Case "32M" : Return 1
            Case "64M" : Return 2
            Case "128M" : Return 3
            Case "256M" : Return 4
            Case "512M" : Return 5
            Case "1G" : Return 6
            Case "2G" : Return 7
            Case "4G" : Return 8
            Case "8G" : Return 9
            Case "16G" : Return 10
            Case "32G" : Return 11
            Case "64G" : Return 12
            Case Else : Return 2
        End Select
    End Function

    ' =========================================================
    '  OK / CANCEL / RESET
    ' =========================================================
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If _tests.Count = 0 Then
            MsgBox("Please add at least one test.", MsgBoxStyle.Exclamation)
            Return
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnResetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetDefault.Click
        cmbProfile.SelectedIndex = 0
        LoadHDD()
    End Sub

    Private Sub grpTestList_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpTestList.Enter

    End Sub
End Class