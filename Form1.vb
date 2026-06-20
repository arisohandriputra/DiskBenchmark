Imports System
Imports System.IO
Imports System.Diagnostics
Imports System.Threading
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Management

Public Class Form1
    Private _testThread As Thread
    Private _currentProcess As Process
    Private _stopRequested As Boolean = False
    Private _diskspdPath As String = Path.Combine(Application.StartupPath, "DiskSpd/DiskSpd64.exe")
    Private _testFilePath As String = ""
    Private _resultsFolder As String = Application.StartupPath
    Private _testFileSize As String = "64M"

    ' --- Test definitions ---
    Private _tests As New List(Of TestConfig)

    ' --- Stored results for report generation ---
    Private Structure TestResultEntry
        Public Config As TestConfig
        Public Result As BenchmarkResult
    End Structure
    Private _testResults As New List(Of TestResultEntry)

    ' --- Drive info for reports ---
    Private Structure DriveInfoEx
        Public Model As String
        Public SerialNumber As String
        Public InterfaceType As String
        Public MediaType As String
        Public SizeGB As String
        Public FirmwareRevision As String
    End Structure
    Private _driveInfoEx As DriveInfoEx

    ' =========================================================
    '  TEST CONFIG STRUCTURE
    ' =========================================================
    Public Structure TestConfig
        Public Name As String
        Public BlockSize As String
        Public QueueDepth As Integer
        Public Threads As Integer
        Public IsSequential As Boolean
        Public TestType As String       ' "Read", "Write", "Mixed"
        Public ReadPercent As Integer   ' 0-100 (for Mixed; 100=all read, 0=all write)
        Public Duration As Integer
        Public WarmUp As Integer
        Public CoolDown As Integer
        Public Category As String

        Public Function BuildArgs() As String
            Dim sb As New System.Text.StringBuilder()
            sb.Append("-b" & BlockSize)
            sb.Append(" -d" & Duration.ToString())
            sb.Append(" -W" & WarmUp.ToString())
            sb.Append(" -C" & CoolDown.ToString())
            sb.Append(" -t" & Threads.ToString())
            sb.Append(" -o" & QueueDepth.ToString())
            If Not IsSequential Then
                sb.Append(" -r")
            End If
            If TestType = "Write" Then
                sb.Append(" -w100 -Zr")
            ElseIf TestType = "Mixed" Then
                Dim writePct As Integer = 100 - ReadPercent
                sb.Append(" -w" & writePct.ToString() & " -Zr")
            End If
            sb.Append(" -Sh -L")
            Return sb.ToString()
        End Function

        Public Function BuildDisplayName() As String
            Dim sb As New System.Text.StringBuilder()
            If IsSequential Then
                sb.Append("Seq")
            Else
                sb.Append("Rnd")
            End If
            sb.Append(" ")
            If TestType = "Read" Then
                sb.Append("Read ")
            ElseIf TestType = "Write" Then
                sb.Append("Write ")
            ElseIf TestType = "Mixed" Then
                sb.Append(ReadPercent.ToString() & "R/" & (100 - ReadPercent).ToString() & "W ")
            End If
            sb.Append(BlockSize & " Q" & QueueDepth.ToString() & "T" & Threads.ToString())
            Return sb.ToString()
        End Function
    End Structure

    ' Parsed result structure
    Private Structure BenchmarkResult
        Public MBps As Double
        Public IOPS As Double
        Public AvgLatencyMs As Double
        Public ReadLatencyMs As Double
        Public WriteLatencyMs As Double
        Public ReadMBps As Double
        Public WriteMBps As Double
        Public ReadIOPS As Double
        Public WriteIOPS As Double
        Public Valid As Boolean
    End Structure

    ' =========================================================
    '  FORM LOAD
    ' =========================================================
    Sub ui()
        Dim requiredFiles() As String = {
       "settings\play.dll",
       "settings\stop.dll",
       "settings\refresh.dll",
       "settings\txt.dll",
       "settings\html.dll",
       "settings\settings.dll",
       "settings\texture1.dll",
       "settings\texture2.dll"
   }

        Dim missingFile As String = ""

        For Each file As String In requiredFiles
            If Not IO.File.Exists(IO.Path.Combine(Application.StartupPath, file)) Then
                missingFile = file
                Exit For
            End If
        Next

        If missingFile <> "" Then
            MsgBox("Required file not found:" & vbCrLf & vbCrLf & _
                   missingFile & vbCrLf & vbCrLf & _
                   "Please reinstall the application.",
                   MsgBoxStyle.Critical, "Missing File")
            Application.Exit()
            Exit Sub
        End If

        btnStartTest.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/play.dll"))
        btnStopTest.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/stop.dll"))
        StartTestToolStripMenuItem.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/play.dll"))
        StopTestToolStripMenuItem.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/stop.dll"))
        RefreshToolStripMenuItem.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/refresh.dll"))
        btnSaveTxt.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/txt.dll"))
        btnSaveHtml.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/html.dll"))
        btnRefresh.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/refresh.dll"))
        btnSettings.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/settings.dll"))
        SaveTXTToolStripMenuItem.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/txt.dll"))
        SaveHTMLToolStripMenuItem.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/html.dll"))
        SettingsToolStripMenuItem.Image = Image.FromFile(Path.Combine(Application.StartupPath, "settings/settings.dll"))

        MenuStrip1.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        MenuStrip1.BackgroundImageLayout = ImageLayout.Tile

        GroupBox1.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        GroupBox1.BackgroundImageLayout = ImageLayout.Tile

        GroupBox2.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        GroupBox2.BackgroundImageLayout = ImageLayout.Tile

        Me.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        Me.BackgroundImageLayout = ImageLayout.Stretch

        btnSaveTxt.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        btnSaveHtml.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        btnRefresh.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        btnSettings.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        btnStartTest.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
        btnStopTest.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "settings/texture2.dll"))
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Disk Benchmark v1.0"
        ui()
        LoadDefaultTests()
        RefreshDriveList()
        CreateResultsFolder()
        btnStopTest.Enabled = False
        StopTestToolStripMenuItem.Enabled = False
        Log("Ready. Select a drive and click Start Test.")
        Log("Click [Settings] to configure tests or load a profile.")
        Log("After testing, use [Save TXT] or [Save HTML] to export reports.")
        FormSettings.ShowDialog()
    End Sub

    Private Sub CreateResultsFolder()
        Try
            If Not Directory.Exists(_resultsFolder) Then
                Directory.CreateDirectory(_resultsFolder)
            End If
        Catch
        End Try
    End Sub

    ' =========================================================
    '  DEFAULT / PROFILE TESTS
    ' =========================================================
    Public Sub LoadDefaultTests()
        LoadProfileHDD()
    End Sub

    Public Sub LoadProfileHDD()

        _tests.Clear()

        _tests.Add(MakeTC("Seq Read  128K Q1T1", "128K", 1, 1, True, "Read", 100, 30, 5, 5, "Sequential"))
        _tests.Add(MakeTC("Seq Write 128K Q1T1", "128K", 1, 1, True, "Write", 0, 30, 5, 5, "Sequential"))

        _tests.Add(MakeTC("Rnd Read  4K  Q1T1", "4K", 1, 1, False, "Read", 100, 30, 5, 5, "Random"))
        _tests.Add(MakeTC("Rnd Write 4K  Q1T1", "4K", 1, 1, False, "Write", 0, 30, 5, 5, "Random"))

        _tests.Add(MakeTC("Mixed 70R/30W 4K Q1T1", "4K", 1, 1, False, "Mixed", 70, 30, 5, 5, "Mixed"))

        RefreshListViewRows()

    End Sub


    Public Sub SetCustomTests(ByVal tests As System.Collections.Generic.List(Of TestConfig))
        _tests.Clear()
        Dim tc As TestConfig
        For Each tc In tests
            _tests.Add(tc)
        Next
        RefreshListViewRows()
    End Sub

    Public Function GetTests() As System.Collections.Generic.List(Of TestConfig)
        Dim copy As New System.Collections.Generic.List(Of TestConfig)
        Dim tc As TestConfig
        For Each tc In _tests
            copy.Add(tc)
        Next
        Return copy
    End Function

    Private Function MakeTC(ByVal name As String, ByVal blockSize As String, ByVal qd As Integer, ByVal thr As Integer, ByVal seq As Boolean, ByVal ttype As String, ByVal readPct As Integer, ByVal dur As Integer, ByVal warmup As Integer, ByVal cooldown As Integer, ByVal cat As String) As TestConfig
        Dim tc As New TestConfig
        tc.Name = name
        tc.BlockSize = blockSize
        tc.QueueDepth = qd
        tc.Threads = thr
        tc.IsSequential = seq
        tc.TestType = ttype
        tc.ReadPercent = readPct
        tc.Duration = dur
        tc.WarmUp = warmup
        tc.CoolDown = cooldown
        tc.Category = cat
        Return tc
    End Function

    ' =========================================================
    '  LISTVIEW MANAGEMENT
    ' =========================================================
    Private Sub RefreshListViewRows()
        lstResults.Items.Clear()
        lstResults.Columns.Clear()
        lstResults.Columns.Add("Test", 210)
        lstResults.Columns.Add("MB/s", 80)
        lstResults.Columns.Add("IOPS", 80)
        lstResults.Columns.Add("Latency (ms)", 120)
        lstResults.Columns.Add("R MB/s", 70)
        lstResults.Columns.Add("W MB/s", 70)
        lstResults.Columns.Add("Status", 156)

        Dim tc As TestConfig
        For Each tc In _tests
            Dim lvi As New ListViewItem(tc.Name)
            lvi.SubItems.Add("-")
            lvi.SubItems.Add("-")
            lvi.SubItems.Add("-")
            lvi.SubItems.Add("-")
            lvi.SubItems.Add("-")
            lvi.SubItems.Add("Waiting")
            lstResults.Items.Add(lvi)
        Next
    End Sub

    ' =========================================================
    '  DRIVE SELECTION
    ' =========================================================
    Private Sub RefreshDriveList()
        cmbDrive.Items.Clear()
        Dim drive As DriveInfo
        For Each drive In DriveInfo.GetDrives()
            If drive.IsReady Then
                Try
                    Dim label As String = drive.VolumeLabel
                    If label = "" Then label = "Local Disk"
                    Dim freeGB As Double = Math.Round(drive.TotalFreeSpace / 1073741824.0, 1)
                    Dim totalGB As Double = Math.Round(drive.TotalSize / 1073741824.0, 1)
                    cmbDrive.Items.Add(drive.RootDirectory.ToString() & "  [" & label & "]  " & freeGB & " GB free / " & totalGB & " GB")
                Catch
                    cmbDrive.Items.Add(drive.RootDirectory.ToString())
                End Try
            End If
        Next
        If cmbDrive.Items.Count > 0 Then cmbDrive.SelectedIndex = 0
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.ActiveControl = Nothing
        RefreshDriveList()
    End Sub

    ' =========================================================
    '  WMI LOW-LEVEL DRIVE INFO
    ' =========================================================
    Private Function GetDriveInfoLowLevel(ByVal driveLetter As String) As DriveInfoEx
        Dim info As New DriveInfoEx
        info.Model = "N/A"
        info.SerialNumber = "N/A"
        info.InterfaceType = "N/A"
        info.MediaType = "N/A"
        info.SizeGB = "N/A"
        info.FirmwareRevision = "N/A"

        Try
            ' Step 1: LogicalDisk -> DiskPartition (ASSOCIATORS OF)
            Dim letter As String = driveLetter.TrimEnd("\"c).ToUpper()
            Dim query1 As String = "ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" & letter & "'} WHERE ResultClass=Win32_DiskPartition"

            Dim searcher1 As New ManagementObjectSearcher(query1)
            Dim partitionDeviceID As String = ""
            Dim obj1 As ManagementObject
            For Each obj1 In searcher1.Get()
                If obj1("DeviceID") IsNot Nothing Then
                    partitionDeviceID = obj1("DeviceID").ToString()
                    Exit For
                End If
            Next
            searcher1.Dispose()

            If partitionDeviceID = "" Then Return info

            ' Escape backslashes in DeviceID for WQL (e.g. "Disk #0, Partition #1")
            Dim escapedPartID As String = partitionDeviceID.Replace("\", "\\")

            ' Step 2: DiskPartition -> DiskDrive
            Dim query2 As String = "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" & escapedPartID & "'} WHERE ResultClass=Win32_DiskDrive"

            Dim searcher2 As New ManagementObjectSearcher(query2)
            Dim obj2 As ManagementObject
            For Each obj2 In searcher2.Get()
                If obj2("Model") IsNot Nothing Then info.Model = obj2("Model").ToString().Trim()
                If obj2("SerialNumber") IsNot Nothing Then info.SerialNumber = obj2("SerialNumber").ToString().Trim()
                If obj2("InterfaceType") IsNot Nothing Then info.InterfaceType = obj2("InterfaceType").ToString().Trim()
                If obj2("MediaType") IsNot Nothing Then info.MediaType = obj2("MediaType").ToString().Trim()
                If obj2("FirmwareRevision") IsNot Nothing Then info.FirmwareRevision = obj2("FirmwareRevision").ToString().Trim()
                If obj2("Size") IsNot Nothing Then
                    Dim sizeBytes As Double = CDbl(obj2("Size"))
                    info.SizeGB = Math.Round(sizeBytes / 1073741824.0, 1).ToString("#,##0.0", CultureInfo.InvariantCulture) & " GB"
                End If
                Exit For
            Next
            searcher2.Dispose()

        Catch ex As Exception
            Log("WARNING: Could not retrieve WMI drive info: " & ex.Message)
        End Try

        Return info
    End Function

    ' =========================================================
    '  BUTTONS: Settings / Start / Stop / Save
    ' =========================================================
    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettings.Click
        Me.ActiveControl = Nothing
        Dim dlg As New FormSettings()
        dlg.SetTests(GetTests())
        dlg.SetFileSize(_testFileSize)
        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SetCustomTests(dlg.GetTests())
            _testFileSize = dlg.TestFileSize
            Log("Settings applied. " & _tests.Count.ToString() & " test(s) configured. File size: " & _testFileSize)
        End If
        dlg.Dispose()
    End Sub

    Private Sub btnStartTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartTest.Click
        Me.ActiveControl = Nothing
        If cmbDrive.SelectedIndex < 0 Then
            MsgBox("Please select a drive first.", MsgBoxStyle.Exclamation)
            Return
        End If
        If _tests.Count = 0 Then
            MsgBox("No tests configured. Click Settings to add tests.", MsgBoxStyle.Exclamation)
            Return
        End If
        If Not File.Exists(_diskspdPath) Then
            MsgBox("Something error:" & vbCrLf & _diskspdPath, MsgBoxStyle.Critical)
            Return
        End If

        Dim driveLetter As String = cmbDrive.Text.Substring(0, 3).Trim()
        _testFilePath = driveLetter & "TemporaryFileTest.dat"

        ' Retrieve low-level drive info via WMI
        Log("Retrieving drive information ...")
        _driveInfoEx = GetDriveInfoLowLevel(driveLetter)
        Log("  Drive Model  : " & _driveInfoEx.Model)
        Log("  Serial Number: " & _driveInfoEx.SerialNumber)
        Log("  Interface    : " & _driveInfoEx.InterfaceType)
        Log("  Media Type   : " & _driveInfoEx.MediaType)
        Log("  Capacity     : " & _driveInfoEx.SizeGB)
        Log("  Firmware     : " & _driveInfoEx.FirmwareRevision)

        ' Clear stored results
        _testResults.Clear()

        ' Reset rows
        Dim lvi As ListViewItem
        For Each lvi In lstResults.Items
            Dim s As Integer
            For s = 1 To 6
                lvi.SubItems(s).Text = "-"
            Next
            lvi.SubItems(6).Text = "Waiting"
            lvi.BackColor = System.Drawing.Color.White
        Next

        _stopRequested = False
        btnStartTest.Enabled = False
        StartTestToolStripMenuItem.Enabled = False
        btnStopTest.Enabled = True
        StopTestToolStripMenuItem.Enabled = True
        SettingsToolStripMenuItem.Enabled = False
        SaveTXTToolStripMenuItem.Enabled = False
        SaveHTMLToolStripMenuItem.Enabled = False
        RefreshToolStripMenuItem.Enabled = False
        btnSettings.Enabled = False
        btnSaveTxt.Enabled = False
        btnSaveHtml.Enabled = False
        btnRefresh.Enabled = False
        cmbDrive.Enabled = False
        ProgressBar1.Value = 0

        _testThread = New Thread(AddressOf RunAllTests)
        _testThread.IsBackground = True
        _testThread.Start()
    End Sub

    Private Sub btnStopTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopTest.Click
        Me.ActiveControl = Nothing
        _stopRequested = True
        If _currentProcess IsNot Nothing Then
            Try
                _currentProcess.Kill()
            Catch
            End Try
        End If
        SetStatus("Stopped by user.")
        Log("--- Test stopped by user ---")
    End Sub

    Private Sub btnSaveTxt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveTxt.Click
        Me.ActiveControl = Nothing

        If _testResults.Count = 0 Then
            MsgBox("No test results to save. Run a benchmark first.", MsgBoxStyle.Information)
            Return
        End If
        Dim dlg As New SaveFileDialog()
        dlg.Title = "Save Report (TXT)"
        dlg.Filter = "Text Files|*.txt|All Files|*.*"
        dlg.DefaultExt = "txt"
        dlg.FileName = "DiskBenchmark_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".txt"
        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim txt As String = GenerateTxtReport()
                Dim sw As New StreamWriter(dlg.FileName, False, System.Text.Encoding.UTF8)
                sw.Write(txt)
                sw.Close()
                Log("TXT report saved: " & dlg.FileName)
                MsgBox("Report saved to:" & vbCrLf & dlg.FileName, MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox("Error saving file: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
        dlg.Dispose()
    End Sub

    Private Sub btnSaveHtml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveHtml.Click
        Me.ActiveControl = Nothing
        If _testResults.Count = 0 Then
            MsgBox("No test results to save. Run a benchmark first.", MsgBoxStyle.Information)
            Return
        End If
        Dim dlg As New SaveFileDialog()
        dlg.Title = "Save Report (HTML)"
        dlg.Filter = "HTML Files|*.html|All Files|*.*"
        dlg.DefaultExt = "html"
        dlg.FileName = "DiskBenchmark_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".html"
        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim html As String = GenerateHtmlReport()
                Dim sw As New StreamWriter(dlg.FileName, False, System.Text.Encoding.UTF8)
                sw.Write(html)
                sw.Close()
                Log("HTML report saved: " & dlg.FileName)
                MsgBox("Report saved to:" & vbCrLf & dlg.FileName, MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox("Error saving file: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
        dlg.Dispose()
    End Sub

    ' =========================================================
    '  RUN ALL TESTS (background thread)
    ' =========================================================
    Private Sub RunAllTests()
        SetStatus("Creating test file (" & _testFileSize & ")...")
        SetProgress(0)
        Log("Creating test file: " & _testFilePath & " (" & _testFileSize & ")")

        Dim createArgs As String = "-c" & _testFileSize & " """ & _testFilePath & """"
        Dim createOutput As String = RunDiskSpd(createArgs)
        If _stopRequested Then GoTo Cleanup

        Log("Test file ready.")

        Dim i As Integer
        For i = 0 To _tests.Count - 1
            If _stopRequested Then Exit For

            Dim tc As TestConfig = _tests(i)
            SetStatus("Running: " & tc.Name & " (" & (i + 1) & "/" & _tests.Count & ")")
            SetRowStatus(i, "Running...", System.Drawing.Color.LightCyan)
            Log("")
            Log("========================================")
            Log("  TEST " & (i + 1) & "/" & _tests.Count & ": " & tc.Name)
            Log("========================================")

            Dim fullArgs As String = tc.BuildArgs() & " """ & _testFilePath & """"
            Log("  Running test file... " & _testFileSize.ToString)
            Dim output As String = RunDiskSpd(fullArgs)

            If _stopRequested Then
                SetRowStatus(i, "Stopped", System.Drawing.Color.LightGray)
                Exit For
            End If

            ' Save raw output
            Try
                Dim rawPath As String = _resultsFolder & "\raw_test" & (i + 1).ToString() & ".txt"
                Dim sw As New System.IO.StreamWriter(rawPath, False, System.Text.Encoding.UTF8)
                sw.Write(output)
                sw.Close()
            Catch
            End Try

            ' Parse result
            Dim result As BenchmarkResult = ParseTextOutput(output)

            If result.Valid Then
                Log("  Throughput : " & FormatMBps(result.MBps))
                Log("  IOPS      : " & FormatIOPS(result.IOPS))
                Log("  Avg Lat   : " & FormatLatency(result.AvgLatencyMs))
                If result.ReadMBps > 0 Or result.ReadIOPS > 0 Then
                    Log("  Read MB/s : " & FormatMBps(result.ReadMBps) & "  |  Read IOPS: " & FormatIOPS(result.ReadIOPS))
                    Log("  Write MB/s: " & FormatMBps(result.WriteMBps) & "  |  Write IOPS: " & FormatIOPS(result.WriteIOPS))
                End If
                If result.ReadLatencyMs > 0 Or result.WriteLatencyMs > 0 Then
                    Log("  Read Lat  : " & FormatLatency(result.ReadLatencyMs))
                    Log("  Write Lat : " & FormatLatency(result.WriteLatencyMs))
                End If

                ' Store result for reports
                Dim entry As New TestResultEntry
                entry.Config = tc
                entry.Result = result
                _testResults.Add(entry)

                UpdateRow(i, result)
            Else
                Log("  WARNING: Could not parse output.")
                Dim debugArea As String = ExtractTotalArea(output)
                If debugArea <> "" Then
                    Log("  Area around 'total:' line:")
                    Log("  " & debugArea.Replace(vbCrLf, vbCrLf & "  "))
                Else
                    Log("  'total:' line NOT found in output!")
                End If
                SetRowStatus(i, "Parse Error", System.Drawing.Color.OrangeRed)
            End If

            Dim pct As Integer = CInt(((i + 1) / _tests.Count) * 100)
            SetProgress(pct)
        Next

Cleanup:
        Log("")
        Log("----------------------------------------")
        Log("Cleaning up test file...")
        Try
            If File.Exists(_testFilePath) Then File.Delete(_testFilePath)
            Log("Test file deleted.")
        Catch ex As Exception
            Log("Could not delete test file: " & ex.Message)
        End Try

        If _testResults.Count > 0 Then
            Log("")
            Log("========================================")
            Log("  BENCHMARK COMPLETE")
            Log("  Tests completed: " & _testResults.Count.ToString() & "/" & _tests.Count.ToString())
            Log("  Use [Save TXT] or [Save HTML] to export report.")
            Log("========================================")
        End If

        If _stopRequested Then
            SetStatus("Stopped.")
        Else
            SetStatus("All tests complete!")
            SetProgress(100)
        End If

        SafeInvoke(Sub()
                       StopTestToolStripMenuItem.Enabled = False
                       StartTestToolStripMenuItem.Enabled = True
                       SettingsToolStripMenuItem.Enabled = True
                       SaveTXTToolStripMenuItem.Enabled = True
                       SaveHTMLToolStripMenuItem.Enabled = True
                       RefreshToolStripMenuItem.Enabled = True
                       btnStartTest.Enabled = True
                       btnStopTest.Enabled = False
                       btnSettings.Enabled = True
                       btnSaveTxt.Enabled = True
                       btnSaveHtml.Enabled = True
                       btnRefresh.Enabled = True
                       cmbDrive.Enabled = True
                       ProgressBar1.Value = 0
                       lblProgress.Text = "0%"
                   End Sub)
    End Sub

    ' =========================================================
    '  RUN DISKSPD
    ' =========================================================
    Private Function RunDiskSpd(ByVal args As String) As String
        Dim psi As New ProcessStartInfo()
        psi.FileName = _diskspdPath
        psi.Arguments = args
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.UseShellExecute = False
        psi.CreateNoWindow = True

        Dim output As New System.Text.StringBuilder()
        _currentProcess = New Process()
        _currentProcess.StartInfo = psi

        Try
            _currentProcess.Start()
            output.Append(_currentProcess.StandardOutput.ReadToEnd())
            _currentProcess.WaitForExit()
        Catch ex As Exception
            Log("ERROR running: " & ex.Message)
        Finally
            _currentProcess = Nothing
        End Try

        Return output.ToString()
    End Function

    ' =========================================================
    '  PARSER
    ' =========================================================
    Private Function ParseTextOutput(ByVal output As String) As BenchmarkResult
        Dim result As New BenchmarkResult
        result.Valid = False
        result.MBps = 0
        result.IOPS = 0
        result.AvgLatencyMs = 0
        result.ReadLatencyMs = 0
        result.WriteLatencyMs = 0
        result.ReadMBps = 0
        result.WriteMBps = 0
        result.ReadIOPS = 0
        result.WriteIOPS = 0

        If String.IsNullOrEmpty(output) Then Return result

        Dim crlf() As Char = {Chr(10), Chr(13)}
        Dim lines() As String = output.Split(crlf, StringSplitOptions.RemoveEmptyEntries)
        Dim nfi As NumberFormatInfo = CultureInfo.InvariantCulture.NumberFormat
        Dim pipeSep() As Char = {"|"c}

        Dim currentSection As String = ""
        Dim i As Integer
        For i = 0 To lines.Length - 1
            Dim trimmed As String = lines(i).Trim()
            Dim lower As String = trimmed.ToLower()

            If lower = "total io" Then
                currentSection = "total"
            ElseIf lower = "read io" Then
                currentSection = "read"
            ElseIf lower = "write io" Then
                currentSection = "write"
            ElseIf lower.Contains("latency distribution") OrElse lower.Contains("cpu count") OrElse lower.Contains("system information") OrElse lower.Contains("results for timespan") Then
                currentSection = ""
            End If

            If lower.StartsWith("total:") AndAlso trimmed.Contains("|") AndAlso currentSection <> "" Then
                Dim cells() As String = trimmed.Split(pipeSep, StringSplitOptions.RemoveEmptyEntries)
                If cells.Length >= 5 Then
                    Dim cell0 As String = cells(0).Trim()
                    If cell0.ToLower().StartsWith("total:") Then
                        cell0 = cell0.Substring(6).Trim()
                    End If

                    Dim bytesVal As Double = 0
                    Dim iosVal As Double = 0
                    Dim mbpsVal As Double = 0
                    Dim iopsVal As Double = 0
                    Dim avgLatVal As Double = 0
                    Dim stdDevVal As Double = 0

                    Double.TryParse(cell0, NumberStyles.Any, nfi, bytesVal)
                    If cells.Length >= 2 Then Double.TryParse(cells(1).Trim(), NumberStyles.Any, nfi, iosVal)
                    If cells.Length >= 3 Then Double.TryParse(cells(2).Trim(), NumberStyles.Any, nfi, mbpsVal)
                    If cells.Length >= 4 Then Double.TryParse(cells(3).Trim(), NumberStyles.Any, nfi, iopsVal)
                    If cells.Length >= 5 Then Double.TryParse(cells(4).Trim(), NumberStyles.Any, nfi, avgLatVal)
                    If cells.Length >= 6 Then Double.TryParse(cells(5).Trim(), NumberStyles.Any, nfi, stdDevVal)

                    Select Case currentSection
                        Case "total"
                            result.MBps = mbpsVal
                            result.IOPS = iopsVal
                            result.AvgLatencyMs = avgLatVal
                        Case "read"
                            result.ReadMBps = mbpsVal
                            result.ReadIOPS = iopsVal
                            result.ReadLatencyMs = avgLatVal
                        Case "write"
                            result.WriteMBps = mbpsVal
                            result.WriteIOPS = iopsVal
                            result.WriteLatencyMs = avgLatVal
                    End Select

                    If result.MBps > 0 Or result.IOPS > 0 Then
                        result.Valid = True
                    End If
                End If
            End If
        Next

        If result.AvgLatencyMs = 0 Then
            Dim fbLine As String
            For Each fbLine In lines
                Dim trimmed As String = fbLine.Trim()
                Dim lower As String = trimmed.ToLower()
                If lower.StartsWith("averagelatency:") Then
                    Dim valStr As String = trimmed.Substring(14).Trim()
                    Dim val As Double = 0
                    If Double.TryParse(valStr, NumberStyles.Any, nfi, val) Then
                        result.AvgLatencyMs = val
                    End If
                End If
            Next
        End If

        Return result
    End Function

    Private Function ExtractTotalArea(ByVal output As String) As String
        Dim crlf() As Char = {Chr(10), Chr(13)}
        Dim lines() As String = output.Split(crlf, StringSplitOptions.RemoveEmptyEntries)
        Dim startIdx As Integer = -1
        Dim li As Integer
        For li = 0 To lines.Length - 1
            If lines(li).Trim().ToLower().StartsWith("total:") Then
                startIdx = li
                Exit For
            End If
        Next
        If startIdx < 0 Then Return ""
        Dim fromLine As Integer = Math.Max(0, startIdx - 2)
        Dim toLine As Integer = Math.Min(lines.Length - 1, startIdx + 8)
        Dim sb As New System.Text.StringBuilder()
        Dim j As Integer
        For j = fromLine To toLine
            sb.AppendLine(lines(j))
        Next
        Return sb.ToString().Trim()
    End Function

    ' =========================================================
    '  FORMATTING HELPERS
    ' =========================================================
    Private Function FormatMBps(ByVal value As Double) As String
        Return value.ToString("#,##0.00", CultureInfo.InvariantCulture) & " MB/s"
    End Function

    Private Function FormatIOPS(ByVal value As Double) As String
        Return value.ToString("#,##0", CultureInfo.InvariantCulture) & " IOPS"
    End Function

    Private Function FormatLatency(ByVal value As Double) As String
        Return value.ToString("0.000", CultureInfo.InvariantCulture) & " ms"
    End Function

    Private Function FormatCellMBps(ByVal value As Double) As String
        If value <= 0 Then Return "-"
        Return value.ToString("#,##0.00", CultureInfo.InvariantCulture)
    End Function

    Private Function FormatCellIOPS(ByVal value As Double) As String
        If value <= 0 Then Return "-"
        Return value.ToString("#,##0", CultureInfo.InvariantCulture)
    End Function

    Private Function FormatCellLat(ByVal value As Double) As String
        If value <= 0 Then Return "-"
        Return value.ToString("0.000", CultureInfo.InvariantCulture)
    End Function

    ' =========================================================
    '  UI HELPERS (thread-safe)
    ' =========================================================
    Private Sub UpdateRow(ByVal index As Integer, ByVal result As BenchmarkResult)
        SafeInvoke(Sub()
                       If index < lstResults.Items.Count Then
                           Dim lvi As ListViewItem = lstResults.Items(index)
                           lvi.SubItems(1).Text = FormatCellMBps(result.MBps)
                           lvi.SubItems(2).Text = FormatCellIOPS(result.IOPS)
                           lvi.SubItems(3).Text = FormatCellLat(result.AvgLatencyMs)
                           lvi.SubItems(4).Text = FormatCellMBps(result.ReadMBps)
                           lvi.SubItems(5).Text = FormatCellMBps(result.WriteMBps)
                           lvi.SubItems(6).Text = "Done"
                           lvi.BackColor = Color.LightGreen
                       End If
                   End Sub)
    End Sub

    Private Sub SetRowStatus(ByVal index As Integer, ByVal status As String, ByVal color As System.Drawing.Color)
        SafeInvoke(Sub()
                       If index < lstResults.Items.Count Then
                           lstResults.Items(index).SubItems(6).Text = status
                           lstResults.Items(index).BackColor = color
                       End If
                   End Sub)
    End Sub

    Private Sub SetStatus(ByVal msg As String)
        SafeInvoke(Sub() lblStatus.Text = msg)
    End Sub

    Private Sub SetProgress(ByVal value As Integer)
        SafeInvoke(Sub()
                       If value < 0 Then value = 0
                       If value > 100 Then value = 100
                       ProgressBar1.Value = value
                       lblProgress.Text = value & "%"
                   End Sub)
    End Sub

    Private Sub Log(ByVal msg As String)
        SafeInvoke(Sub()
                       txtLog.AppendText(msg & vbCrLf)
                       txtLog.ScrollToCaret()
                   End Sub)
    End Sub

    Private Sub SafeInvoke(ByVal action As MethodInvoker)
        If Me.InvokeRequired Then
            Me.Invoke(action)
        Else
            action()
        End If
    End Sub

    ' =========================================================
    '  REPORT GENERATION - TXT
    ' =========================================================
    Private Function GenerateTxtReport() As String
        Dim sb As New System.Text.StringBuilder()
        Dim now As DateTime = DateTime.Now
        Dim driveInfo As String = "N/A"
        If cmbDrive.SelectedIndex >= 0 Then
            driveInfo = cmbDrive.Text
        End If

        sb.AppendLine("================================================")
        sb.AppendLine("         DISK BENCHMARK REPORT")
        sb.AppendLine("================================================")
        sb.AppendLine()
        sb.AppendLine("Date          : " & now.ToString("dd MMMM yyyy HH:mm:ss"))
        sb.AppendLine("Test Count    : " & _testResults.Count.ToString())
        sb.AppendLine("Test File Size: " & _testFileSize)

        ' Drive info section
        sb.AppendLine()
        sb.AppendLine("------------------------------------------------")
        sb.AppendLine("  DRIVE INFORMATION")
        sb.AppendLine("------------------------------------------------")
        sb.AppendLine("  Drive Letter   : " & driveInfo.Substring(0, 3).Trim())
        sb.AppendLine("  Drive Name     : " & _driveInfoEx.Model)
        sb.AppendLine("  Serial Number  : " & _driveInfoEx.SerialNumber)
        sb.AppendLine("  Interface Type : " & _driveInfoEx.InterfaceType)
        sb.AppendLine("  Media Type     : " & _driveInfoEx.MediaType)
        sb.AppendLine("  Capacity       : " & _driveInfoEx.SizeGB)
        sb.AppendLine("  Firmware       : " & _driveInfoEx.FirmwareRevision)

        ' Detail results
        sb.AppendLine()
        sb.AppendLine("================================================")
        sb.AppendLine("  DETAILS OF TEST RESULTS PER TEST")
        sb.AppendLine("================================================")
        sb.AppendLine()

        Dim idx As Integer = 0
        Dim e2 As TestResultEntry
        For Each e2 In _testResults
            idx += 1
            sb.AppendLine("  #" & idx.ToString() & "  " & e2.Config.Name)
            sb.AppendLine("  " & New String("-"c, 50))
            sb.AppendLine("  Throughput      : " & FormatMBps(e2.Result.MBps))
            sb.AppendLine("  IOPS            : " & FormatIOPS(e2.Result.IOPS))
            sb.AppendLine("  Avg Latency     : " & FormatLatency(e2.Result.AvgLatencyMs))
            If e2.Result.ReadMBps > 0 Or e2.Result.ReadIOPS > 0 Then
                sb.AppendLine("  Read  MB/s      : " & FormatMBps(e2.Result.ReadMBps))
                sb.AppendLine("  Read  IOPS      : " & FormatIOPS(e2.Result.ReadIOPS))
                sb.AppendLine("  Write MB/s      : " & FormatMBps(e2.Result.WriteMBps))
                sb.AppendLine("  Write IOPS      : " & FormatIOPS(e2.Result.WriteIOPS))
                sb.AppendLine("  Read  Latency   : " & FormatLatency(e2.Result.ReadLatencyMs))
                sb.AppendLine("  Write Latency   : " & FormatLatency(e2.Result.WriteLatencyMs))
            End If
            sb.AppendLine()
        Next

        sb.AppendLine("================================================")
        sb.AppendLine("  Generated by Disk Benchmark v1.0")
        sb.AppendLine("================================================")

        Return sb.ToString()
    End Function

    ' =========================================================
    '  REPORT GENERATION - HTML
    ' =========================================================
    Private Function GenerateHtmlReport() As String
        Dim now As DateTime = DateTime.Now
        Dim driveInfo As String = "N/A"
        If cmbDrive.SelectedIndex >= 0 Then
            driveInfo = cmbDrive.Text
        End If

        Dim sb As New System.Text.StringBuilder()

        ' Begin HTML
        sb.AppendLine("<!DOCTYPE html>")
        sb.AppendLine("<html>")
        sb.AppendLine("<head>")
        sb.AppendLine("<meta charset=""UTF-8"">")
        sb.AppendLine("<title>Disk Benchmark Report</title>")
        sb.AppendLine("<style>")
        sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, Arial, sans-serif; margin: 0; padding: 20px; background: #f0f2f5; color: #2c3e50; }")
        sb.AppendLine(".container { max-width: 920px; margin: 0 auto; background: #ffffff; padding: 35px 40px; border-radius: 10px; box-shadow: 0 4px 15px rgba(0,0,0,0.08); }")
        sb.AppendLine("h1 { color: #1a252f; font-size: 26px; margin: 0 0 5px 0; border-bottom: 3px solid #3498db; padding-bottom: 12px; }")
        sb.AppendLine(".subtitle { color: #7f8c8d; font-size: 13px; margin-bottom: 25px; }")
        sb.AppendLine("")
        sb.AppendLine(".info-grid { display: table; width: 100%; margin-bottom: 20px; }")
        sb.AppendLine(".info-row { display: table-row; }")
        sb.AppendLine(".info-label { display: table-cell; padding: 4px 10px 4px 0; color: #7f8c8d; font-size: 14px; white-space: nowrap; }")
        sb.AppendLine(".info-value { display: table-cell; padding: 4px 0; font-size: 14px; font-weight: 600; }")
        sb.AppendLine("")
        sb.AppendLine(".drive-section { background: #f8f9fa; border: 1px solid #e9ecef; border-radius: 10px; padding: 25px 30px; margin: 25px 0; }")
        sb.AppendLine(".drive-section h2 { margin: 0 0 18px 0; font-size: 18px; color: #2c3e50; border-bottom: 2px solid #3498db; padding-bottom: 10px; }")
        sb.AppendLine(".drive-grid { display: table; width: 100%; }")
        sb.AppendLine(".drive-row { display: table-row; }")
        sb.AppendLine(".drive-label { display: table-cell; padding: 5px 15px 5px 0; color: #7f8c8d; font-size: 14px; white-space: nowrap; width: 160px; }")
        sb.AppendLine(".drive-val { display: table-cell; padding: 5px 0; font-size: 14px; font-weight: 600; color: #2c3e50; }")
        sb.AppendLine(".drive-val.mono { font-family: 'Consolas', 'Courier New', monospace; letter-spacing: 0.5px; }")
        sb.AppendLine("")
        sb.AppendLine("table.results { width: 100%; border-collapse: collapse; margin: 25px 0; font-size: 14px; }")
        sb.AppendLine("table.results th { background: #2c3e50; color: #ffffff; padding: 12px 10px; text-align: left; font-weight: 600; }")
        sb.AppendLine("table.results th:first-child { border-radius: 6px 0 0 0; }")
        sb.AppendLine("table.results th:last-child { border-radius: 0 6px 0 0; }")
        sb.AppendLine("table.results td { padding: 10px; border-bottom: 1px solid #ecf0f1; }")
        sb.AppendLine("table.results tr:nth-child(even) { background: #f8f9fa; }")
        sb.AppendLine("table.results tr:hover { background: #eef3f8; }")
        sb.AppendLine("")
        sb.AppendLine(".footer { text-align: center; color: #bdc3c7; font-size: 12px; margin-top: 30px; padding-top: 15px; border-top: 1px solid #ecf0f1; }")
        sb.AppendLine("</style>")
        sb.AppendLine("</head>")
        sb.AppendLine("<body>")
        sb.AppendLine("<div class=""container"">")

        ' Header
        sb.AppendLine("<h1>Disk Benchmark Report</h1>")
        ' Basic info grid
        sb.AppendLine("<div class=""info-grid"">")
        sb.AppendLine("  <div class=""info-row"">")
        sb.AppendLine("    <div class=""info-label"">Date:</div>")
        sb.AppendLine("    <div class=""info-value"">" & now.ToString("dd MMMM yyyy, HH:mm:ss") & "</div>")
        sb.AppendLine("  </div>")
        sb.AppendLine("  <div class=""info-row"">")
        sb.AppendLine("    <div class=""info-label"">Test count:</div>")
        sb.AppendLine("    <div class=""info-value"">" & _testResults.Count.ToString() & "</div>")
        sb.AppendLine("  </div>")
        sb.AppendLine("  <div class=""info-row"">")
        sb.AppendLine("    <div class=""info-label"">Test File Size:</div>")
        sb.AppendLine("    <div class=""info-value"">" & HtmlEncode(_testFileSize) & "</div>")
        sb.AppendLine("  </div>")
        sb.AppendLine("</div>")

        ' Drive info section (WMI)
        sb.AppendLine("<div class=""drive-section"">")
        sb.AppendLine("  <h2>Drive Info</h2>")
        sb.AppendLine("  <div class=""drive-grid"">")
        sb.AppendLine("    <div class=""drive-row"">")
        sb.AppendLine("      <div class=""drive-label"">Drive Letter</div>")
        sb.AppendLine("      <div class=""drive-val"">" & HtmlEncode(driveInfo.Substring(0, 3).Trim()) & "</div>")
        sb.AppendLine("    </div>")
        sb.AppendLine("    <div class=""drive-row"">")
        sb.AppendLine("      <div class=""drive-label"">Drive Name (Model)</div>")
        sb.AppendLine("      <div class=""drive-val"">" & HtmlEncode(_driveInfoEx.Model) & "</div>")
        sb.AppendLine("    </div>")
        sb.AppendLine("    <div class=""drive-row"">")
        sb.AppendLine("      <div class=""drive-label"">Serial Number</div>")
        sb.AppendLine("      <div class=""drive-val mono"">" & HtmlEncode(_driveInfoEx.SerialNumber) & "</div>")
        sb.AppendLine("    </div>")
        sb.AppendLine("    <div class=""drive-row"">")
        sb.AppendLine("      <div class=""drive-label"">Interface Type</div>")
        sb.AppendLine("      <div class=""drive-val"">" & HtmlEncode(_driveInfoEx.InterfaceType) & "</div>")
        sb.AppendLine("    </div>")
        sb.AppendLine("    <div class=""drive-row"">")
        sb.AppendLine("      <div class=""drive-label"">Media Type</div>")
        sb.AppendLine("      <div class=""drive-val"">" & HtmlEncode(_driveInfoEx.MediaType) & "</div>")
        sb.AppendLine("    </div>")
        sb.AppendLine("    <div class=""drive-row"">")
        sb.AppendLine("      <div class=""drive-label"">Capacity</div>")
        sb.AppendLine("      <div class=""drive-val"">" & HtmlEncode(_driveInfoEx.SizeGB) & "</div>")
        sb.AppendLine("    </div>")
        sb.AppendLine("    <div class=""drive-row"">")
        sb.AppendLine("      <div class=""drive-label"">Firmware Revision</div>")
        sb.AppendLine("      <div class=""drive-val mono"">" & HtmlEncode(_driveInfoEx.FirmwareRevision) & "</div>")
        sb.AppendLine("    </div>")
        sb.AppendLine("  </div>")
        sb.AppendLine("</div>")

        ' Results table
        sb.AppendLine("<table class=""results"">")
        sb.AppendLine("  <tr>")
        sb.AppendLine("    <th>#</th>")
        sb.AppendLine("    <th>Test</th>")
        sb.AppendLine("    <th>MB/s</th>")
        sb.AppendLine("    <th>IOPS</th>")
        sb.AppendLine("    <th>Latency (ms)</th>")
        sb.AppendLine("    <th>Read MB/s</th>")
        sb.AppendLine("    <th>Write MB/s</th>")
        sb.AppendLine("  </tr>")

        Dim tidx As Integer = 0
        Dim ent2 As TestResultEntry
        For Each ent2 In _testResults
            tidx += 1

            sb.AppendLine("  <tr>")
            sb.AppendLine("    <td>" & tidx.ToString() & "</td>")
            sb.AppendLine("    <td><strong>" & HtmlEncode(ent2.Config.Name) & "</strong></td>")
            sb.AppendLine("    <td>" & FormatCellMBps(ent2.Result.MBps) & "</td>")
            sb.AppendLine("    <td>" & FormatCellIOPS(ent2.Result.IOPS) & "</td>")
            sb.AppendLine("    <td>" & FormatCellLat(ent2.Result.AvgLatencyMs) & "</td>")
            sb.AppendLine("    <td>" & FormatCellMBps(ent2.Result.ReadMBps) & "</td>")
            sb.AppendLine("    <td>" & FormatCellMBps(ent2.Result.WriteMBps) & "</td>")
            sb.AppendLine("  </tr>")
        Next

        sb.AppendLine("</table>")

        ' Footer
        sb.AppendLine("<div class=""footer"">")
        sb.AppendLine("  Generated by <strong>Disk Benchmark v1.0</strong> | " & now.ToString("dd MMMM yyyy HH:mm:ss"))
        sb.AppendLine("</div>")

        sb.AppendLine("</div>")
        sb.AppendLine("</body>")
        sb.AppendLine("</html>")

        Return sb.ToString()
    End Function

    Private Function HtmlEncode(ByVal text As String) As String
        If text Is Nothing Then Return ""
        Dim s As String = text.Replace("&", "&amp;")
        s = s.Replace("<", "&lt;")
        s = s.Replace(">", "&gt;")
        s = s.Replace("""", "&quot;")
        Return s
    End Function

    ' =========================================================
    '  FORM CLOSING
    ' =========================================================
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        _stopRequested = True
        If _currentProcess IsNot Nothing Then
            Try
                _currentProcess.Kill()
            Catch
            End Try
        End If
        Try
            If _testFilePath <> "" AndAlso File.Exists(_testFilePath) Then
                File.Delete(_testFilePath)
            End If
        Catch
        End Try
    End Sub

    Private Sub SaveTXTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveTXTToolStripMenuItem.Click
        btnSaveTxt.PerformClick()
    End Sub

    Private Sub SaveHTMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveHTMLToolStripMenuItem.Click
        btnSaveHtml.PerformClick()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        btnRefresh.PerformClick()
    End Sub

    Private Sub StartTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartTestToolStripMenuItem.Click
        btnStartTest.PerformClick()
    End Sub

    Private Sub StopTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopTestToolStripMenuItem.Click
        btnStopTest.PerformClick()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
        btnSettings.PerformClick()
    End Sub

    Private Sub cmbDrive_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDrive.SelectedIndexChanged
        Me.ActiveControl = Nothing
    End Sub
    
    Private Sub HelpToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem1.Click
        FormHelp.ShowDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        FormAbout.ShowDialog()
    End Sub

    Private Sub DonateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DonateToolStripMenuItem.Click
        Try
            Process.Start("https://github.com/sponsors/arisohandriputra")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub
End Class