namespace PortScanner;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.toolStripMain = new System.Windows.Forms.ToolStrip();
        this.ddScan = new System.Windows.Forms.ToolStripDropDownButton();
        this.tsmiStart = new System.Windows.Forms.ToolStripMenuItem();
        this.tsmiStop = new System.Windows.Forms.ToolStripMenuItem();
        this.ddData = new System.Windows.Forms.ToolStripDropDownButton();
        this.tsmiClearTable = new System.Windows.Forms.ToolStripMenuItem();
        this.tsmiClearLog = new System.Windows.Forms.ToolStripMenuItem();
        this.ddExport = new System.Windows.Forms.ToolStripDropDownButton();
        this.tsmiExportCsv = new System.Windows.Forms.ToolStripMenuItem();
        this.tsmiExportJson = new System.Windows.Forms.ToolStripMenuItem();
        this.tsmiExportXml = new System.Windows.Forms.ToolStripMenuItem();
        this.tabInput = new System.Windows.Forms.TabControl();
        this.tabRange = new System.Windows.Forms.TabPage();
        this.lblStartRange = new System.Windows.Forms.Label();
        this.txtStartIpRange = new System.Windows.Forms.TextBox();
        this.lblEndRange = new System.Windows.Forms.Label();
        this.txtEndIpRange = new System.Windows.Forms.TextBox();
        this.tabSubnet = new System.Windows.Forms.TabPage();
        this.lblNet = new System.Windows.Forms.Label();
        this.txtNetIp = new System.Windows.Forms.TextBox();
        this.lblMask = new System.Windows.Forms.Label();
        this.txtMask = new System.Windows.Forms.TextBox();
        this.lblWorkers = new System.Windows.Forms.Label();
        this.numWorkers = new System.Windows.Forms.NumericUpDown();
        this.chkDns = new System.Windows.Forms.CheckBox();
        this.grpRun = new System.Windows.Forms.GroupBox();
        this.grpFilter = new System.Windows.Forms.GroupBox();
        this.lblStatusFilter = new System.Windows.Forms.Label();
        this.cmbStatus = new System.Windows.Forms.ComboBox();
        this.btnApplyFilter = new System.Windows.Forms.Button();
        this.btnClearFilter = new System.Windows.Forms.Button();
        this.lblIpFilter = new System.Windows.Forms.Label();
        this.txtFilterIp = new System.Windows.Forms.TextBox();
        this.lblHostFilter = new System.Windows.Forms.Label();
        this.txtFilterHost = new System.Windows.Forms.TextBox();
        this.lblRttMin = new System.Windows.Forms.Label();
        this.txtRttMin = new System.Windows.Forms.TextBox();
        this.lblRttMax = new System.Windows.Forms.Label();
        this.txtRttMax = new System.Windows.Forms.TextBox();
        this.lvResults = new System.Windows.Forms.ListView();
        this.colIp = new System.Windows.Forms.ColumnHeader();
        this.colStatus = new System.Windows.Forms.ColumnHeader();
        this.colRtt = new System.Windows.Forms.ColumnHeader();
        this.colHostname = new System.Windows.Forms.ColumnHeader();
        this.colMac = new System.Windows.Forms.ColumnHeader();
        this.rtbLog = new System.Windows.Forms.RichTextBox();
        this.toolStripMain.SuspendLayout();
        this.tabInput.SuspendLayout();
        this.tabRange.SuspendLayout();
        this.tabSubnet.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numWorkers)).BeginInit();
        this.grpRun.SuspendLayout();
        this.grpFilter.SuspendLayout();
        this.SuspendLayout();
        // 
        // toolStripMain
        // 
        this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
        this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.ddScan, this.ddData, this.ddExport });
        this.toolStripMain.Location = new System.Drawing.Point(0, 0);
        this.toolStripMain.Name = "toolStripMain";
        this.toolStripMain.Size = new System.Drawing.Size(982, 25);
        this.toolStripMain.TabIndex = 200;
        // 
        // ddScan
        // 
        this.ddScan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tsmiStart, this.tsmiStop });
        this.ddScan.Name = "ddScan";
        this.ddScan.Size = new System.Drawing.Size(45, 22);
        this.ddScan.Text = "Scan";
        // 
        // tsmiStart
        // 
        this.tsmiStart.Name = "tsmiStart";
        this.tsmiStart.Size = new System.Drawing.Size(98, 22);
        this.tsmiStart.Text = "Start";
        this.tsmiStart.Click += new System.EventHandler(this.btnStart_Click);
        // 
        // tsmiStop
        // 
        this.tsmiStop.Name = "tsmiStop";
        this.tsmiStop.Size = new System.Drawing.Size(98, 22);
        this.tsmiStop.Text = "Stop";
        this.tsmiStop.Click += new System.EventHandler(this.btnStop_Click);
        // 
        // ddData
        // 
        this.ddData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tsmiClearTable, this.tsmiClearLog });
        this.ddData.Name = "ddData";
        this.ddData.Size = new System.Drawing.Size(44, 22);
        this.ddData.Text = "Data";
        // 
        // tsmiClearTable
        // 
        this.tsmiClearTable.Name = "tsmiClearTable";
        this.tsmiClearTable.Size = new System.Drawing.Size(132, 22);
        this.tsmiClearTable.Text = "Clear Table";
        this.tsmiClearTable.Click += new System.EventHandler(this.btnClearOutput_Click);
        // 
        // tsmiClearLog
        // 
        this.tsmiClearLog.Name = "tsmiClearLog";
        this.tsmiClearLog.Size = new System.Drawing.Size(132, 22);
        this.tsmiClearLog.Text = "Clear Log";
        this.tsmiClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
        // 
        // ddExport
        // 
        this.ddExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tsmiExportCsv, this.tsmiExportJson, this.tsmiExportXml });
        this.ddExport.Name = "ddExport";
        this.ddExport.Size = new System.Drawing.Size(53, 22);
        this.ddExport.Text = "Export";
        // 
        // tsmiExportCsv
        // 
        this.tsmiExportCsv.Name = "tsmiExportCsv";
        this.tsmiExportCsv.Size = new System.Drawing.Size(102, 22);
        this.tsmiExportCsv.Text = "CSV";
        this.tsmiExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
        // 
        // tsmiExportJson
        // 
        this.tsmiExportJson.Name = "tsmiExportJson";
        this.tsmiExportJson.Size = new System.Drawing.Size(102, 22);
        this.tsmiExportJson.Text = "JSON";
        this.tsmiExportJson.Click += new System.EventHandler(this.btnExportJson_Click);
        // 
        // tsmiExportXml
        // 
        this.tsmiExportXml.Name = "tsmiExportXml";
        this.tsmiExportXml.Size = new System.Drawing.Size(102, 22);
        this.tsmiExportXml.Text = "XML";
        this.tsmiExportXml.Click += new System.EventHandler(this.btnExportXml_Click);
        // 
        // tabInput
        // 
        this.tabInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.tabInput.Controls.Add(this.tabRange);
        this.tabInput.Controls.Add(this.tabSubnet);
        this.tabInput.Location = new System.Drawing.Point(14, 31);
        this.tabInput.Margin = new System.Windows.Forms.Padding(2);
        this.tabInput.Name = "tabInput";
        this.tabInput.SelectedIndex = 0;
        this.tabInput.Size = new System.Drawing.Size(956, 81);
        this.tabInput.TabIndex = 0;
        // 
        // tabRange
        // 
        this.tabRange.Controls.Add(this.lblStartRange);
        this.tabRange.Controls.Add(this.txtStartIpRange);
        this.tabRange.Controls.Add(this.lblEndRange);
        this.tabRange.Controls.Add(this.txtEndIpRange);
        this.tabRange.Location = new System.Drawing.Point(4, 22);
        this.tabRange.Margin = new System.Windows.Forms.Padding(2);
        this.tabRange.Name = "tabRange";
        this.tabRange.Padding = new System.Windows.Forms.Padding(7);
        this.tabRange.Size = new System.Drawing.Size(948, 55);
        this.tabRange.TabIndex = 0;
        this.tabRange.Text = "Rozsah";
        // 
        // lblStartRange
        // 
        this.lblStartRange.AutoSize = true;
        this.lblStartRange.Location = new System.Drawing.Point(7, 9);
        this.lblStartRange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblStartRange.Name = "lblStartRange";
        this.lblStartRange.Size = new System.Drawing.Size(42, 13);
        this.lblStartRange.TabIndex = 0;
        this.lblStartRange.Text = "Start IP";
        // 
        // txtStartIpRange
        // 
        this.txtStartIpRange.Location = new System.Drawing.Point(68, 6);
        this.txtStartIpRange.Margin = new System.Windows.Forms.Padding(2);
        this.txtStartIpRange.Name = "txtStartIpRange";
        this.txtStartIpRange.Size = new System.Drawing.Size(138, 20);
        this.txtStartIpRange.TabIndex = 1;
        this.txtStartIpRange.Text = "172.0.0.0";
        // 
        // lblEndRange
        // 
        this.lblEndRange.AutoSize = true;
        this.lblEndRange.Location = new System.Drawing.Point(7, 35);
        this.lblEndRange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblEndRange.Name = "lblEndRange";
        this.lblEndRange.Size = new System.Drawing.Size(39, 13);
        this.lblEndRange.TabIndex = 2;
        this.lblEndRange.Text = "End IP";
        // 
        // txtEndIpRange
        // 
        this.txtEndIpRange.Location = new System.Drawing.Point(68, 32);
        this.txtEndIpRange.Margin = new System.Windows.Forms.Padding(2);
        this.txtEndIpRange.Name = "txtEndIpRange";
        this.txtEndIpRange.Size = new System.Drawing.Size(138, 20);
        this.txtEndIpRange.TabIndex = 2;
        this.txtEndIpRange.Text = "172.0.0.255";
        // 
        // tabSubnet
        // 
        this.tabSubnet.Controls.Add(this.lblNet);
        this.tabSubnet.Controls.Add(this.txtNetIp);
        this.tabSubnet.Controls.Add(this.lblMask);
        this.tabSubnet.Controls.Add(this.txtMask);
        this.tabSubnet.Location = new System.Drawing.Point(4, 22);
        this.tabSubnet.Margin = new System.Windows.Forms.Padding(2);
        this.tabSubnet.Name = "tabSubnet";
        this.tabSubnet.Padding = new System.Windows.Forms.Padding(7);
        this.tabSubnet.Size = new System.Drawing.Size(948, 55);
        this.tabSubnet.TabIndex = 1;
        this.tabSubnet.Text = "Síť/Maska";
        // 
        // lblNet
        // 
        this.lblNet.AutoSize = true;
        this.lblNet.Location = new System.Drawing.Point(7, 9);
        this.lblNet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblNet.Name = "lblNet";
        this.lblNet.Size = new System.Drawing.Size(35, 13);
        this.lblNet.TabIndex = 0;
        this.lblNet.Text = "Síť IP";
        // 
        // txtNetIp
        // 
        this.txtNetIp.Location = new System.Drawing.Point(68, 6);
        this.txtNetIp.Margin = new System.Windows.Forms.Padding(2);
        this.txtNetIp.Name = "txtNetIp";
        this.txtNetIp.Size = new System.Drawing.Size(138, 20);
        this.txtNetIp.TabIndex = 1;
        this.txtNetIp.Text = "172.0.0.0";
        // 
        // lblMask
        // 
        this.lblMask.AutoSize = true;
        this.lblMask.Location = new System.Drawing.Point(7, 35);
        this.lblMask.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblMask.Name = "lblMask";
        this.lblMask.Size = new System.Drawing.Size(39, 13);
        this.lblMask.TabIndex = 2;
        this.lblMask.Text = "Maska";
        // 
        // txtMask
        // 
        this.txtMask.Location = new System.Drawing.Point(68, 32);
        this.txtMask.Margin = new System.Windows.Forms.Padding(2);
        this.txtMask.Name = "txtMask";
        this.txtMask.Size = new System.Drawing.Size(138, 20);
        this.txtMask.TabIndex = 2;
        this.txtMask.Text = "255.255.255.0";
        // 
        // lblWorkers
        // 
        this.lblWorkers.AutoSize = true;
        this.lblWorkers.Location = new System.Drawing.Point(10, 19);
        this.lblWorkers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblWorkers.Name = "lblWorkers";
        this.lblWorkers.Size = new System.Drawing.Size(47, 13);
        this.lblWorkers.TabIndex = 4;
        this.lblWorkers.Text = "Workery";
        // 
        // numWorkers
        // 
        this.numWorkers.Location = new System.Drawing.Point(83, 17);
        this.numWorkers.Margin = new System.Windows.Forms.Padding(2);
        this.numWorkers.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
        this.numWorkers.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.numWorkers.Name = "numWorkers";
        this.numWorkers.Size = new System.Drawing.Size(68, 20);
        this.numWorkers.TabIndex = 5;
        this.numWorkers.Value = new decimal(new int[] { 100, 0, 0, 0 });
        // 
        // chkDns
        // 
        this.chkDns.AutoSize = true;
        this.chkDns.Checked = true;
        this.chkDns.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkDns.Location = new System.Drawing.Point(10, 40);
        this.chkDns.Margin = new System.Windows.Forms.Padding(2);
        this.chkDns.Name = "chkDns";
        this.chkDns.Size = new System.Drawing.Size(84, 17);
        this.chkDns.TabIndex = 6;
        this.chkDns.Text = "DNS lookup";
        this.chkDns.UseVisualStyleBackColor = true;
        // 
        // grpRun
        // 
        this.grpRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.grpRun.Controls.Add(this.lblWorkers);
        this.grpRun.Controls.Add(this.numWorkers);
        this.grpRun.Controls.Add(this.chkDns);
        this.grpRun.Location = new System.Drawing.Point(14, 116);
        this.grpRun.Margin = new System.Windows.Forms.Padding(2);
        this.grpRun.Name = "grpRun";
        this.grpRun.Padding = new System.Windows.Forms.Padding(2);
        this.grpRun.Size = new System.Drawing.Size(955, 80);
        this.grpRun.TabIndex = 100;
        this.grpRun.TabStop = false;
        this.grpRun.Text = "Sken";
        // 
        // grpFilter
        // 
        this.grpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.grpFilter.Controls.Add(this.lblStatusFilter);
        this.grpFilter.Controls.Add(this.cmbStatus);
        this.grpFilter.Controls.Add(this.btnApplyFilter);
        this.grpFilter.Controls.Add(this.btnClearFilter);
        this.grpFilter.Controls.Add(this.lblIpFilter);
        this.grpFilter.Controls.Add(this.txtFilterIp);
        this.grpFilter.Controls.Add(this.lblHostFilter);
        this.grpFilter.Controls.Add(this.txtFilterHost);
        this.grpFilter.Controls.Add(this.lblRttMin);
        this.grpFilter.Controls.Add(this.txtRttMin);
        this.grpFilter.Controls.Add(this.lblRttMax);
        this.grpFilter.Controls.Add(this.txtRttMax);
        this.grpFilter.Location = new System.Drawing.Point(14, 200);
        this.grpFilter.Margin = new System.Windows.Forms.Padding(2);
        this.grpFilter.Name = "grpFilter";
        this.grpFilter.Padding = new System.Windows.Forms.Padding(2);
        this.grpFilter.Size = new System.Drawing.Size(955, 47);
        this.grpFilter.TabIndex = 102;
        this.grpFilter.TabStop = false;
        this.grpFilter.Text = "Filtry";
        // 
        // lblStatusFilter
        // 
        this.lblStatusFilter.AutoSize = true;
        this.lblStatusFilter.Location = new System.Drawing.Point(19, 22);
        this.lblStatusFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblStatusFilter.Name = "lblStatusFilter";
        this.lblStatusFilter.Size = new System.Drawing.Size(29, 13);
        this.lblStatusFilter.TabIndex = 18;
        this.lblStatusFilter.Text = "Stav";
        // 
        // cmbStatus
        // 
        this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbStatus.FormattingEnabled = true;
        this.cmbStatus.Items.AddRange(new object[] { "All", "Online", "Offline", "Timeout", "Error" });
        this.cmbStatus.Location = new System.Drawing.Point(52, 18);
        this.cmbStatus.Margin = new System.Windows.Forms.Padding(2);
        this.cmbStatus.Name = "cmbStatus";
        this.cmbStatus.Size = new System.Drawing.Size(104, 21);
        this.cmbStatus.TabIndex = 19;
        // 
        // btnApplyFilter
        // 
        this.btnApplyFilter.Location = new System.Drawing.Point(883, 17);
        this.btnApplyFilter.Margin = new System.Windows.Forms.Padding(2);
        this.btnApplyFilter.Name = "btnApplyFilter";
        this.btnApplyFilter.Size = new System.Drawing.Size(68, 20);
        this.btnApplyFilter.TabIndex = 20;
        this.btnApplyFilter.Text = "Filtrovat";
        this.btnApplyFilter.UseVisualStyleBackColor = true;
        this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
        // 
        // btnClearFilter
        // 
        this.btnClearFilter.Location = new System.Drawing.Point(811, 17);
        this.btnClearFilter.Margin = new System.Windows.Forms.Padding(2);
        this.btnClearFilter.Name = "btnClearFilter";
        this.btnClearFilter.Size = new System.Drawing.Size(68, 20);
        this.btnClearFilter.TabIndex = 21;
        this.btnClearFilter.Text = "Reset filt.";
        this.btnClearFilter.UseVisualStyleBackColor = true;
        this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
        // 
        // lblIpFilter
        // 
        this.lblIpFilter.AutoSize = true;
        this.lblIpFilter.Location = new System.Drawing.Point(160, 22);
        this.lblIpFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblIpFilter.Name = "lblIpFilter";
        this.lblIpFilter.Size = new System.Drawing.Size(33, 13);
        this.lblIpFilter.TabIndex = 22;
        this.lblIpFilter.Text = "IP filtr";
        // 
        // txtFilterIp
        // 
        this.txtFilterIp.Location = new System.Drawing.Point(200, 19);
        this.txtFilterIp.Margin = new System.Windows.Forms.Padding(2);
        this.txtFilterIp.Name = "txtFilterIp";
        this.txtFilterIp.Size = new System.Drawing.Size(120, 20);
        this.txtFilterIp.TabIndex = 23;
        // 
        // lblHostFilter
        // 
        this.lblHostFilter.AutoSize = true;
        this.lblHostFilter.Location = new System.Drawing.Point(327, 22);
        this.lblHostFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblHostFilter.Name = "lblHostFilter";
        this.lblHostFilter.Size = new System.Drawing.Size(71, 13);
        this.lblHostFilter.TabIndex = 24;
        this.lblHostFilter.Text = "Hostname filtr";
        // 
        // txtFilterHost
        // 
        this.txtFilterHost.Location = new System.Drawing.Point(402, 19);
        this.txtFilterHost.Margin = new System.Windows.Forms.Padding(2);
        this.txtFilterHost.Name = "txtFilterHost";
        this.txtFilterHost.Size = new System.Drawing.Size(146, 20);
        this.txtFilterHost.TabIndex = 25;
        // 
        // lblRttMin
        // 
        this.lblRttMin.AutoSize = true;
        this.lblRttMin.Location = new System.Drawing.Point(552, 22);
        this.lblRttMin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblRttMin.Name = "lblRttMin";
        this.lblRttMin.Size = new System.Drawing.Size(48, 13);
        this.lblRttMin.TabIndex = 26;
        this.lblRttMin.Text = "RTT min";
        // 
        // txtRttMin
        // 
        this.txtRttMin.Location = new System.Drawing.Point(604, 19);
        this.txtRttMin.Margin = new System.Windows.Forms.Padding(2);
        this.txtRttMin.Name = "txtRttMin";
        this.txtRttMin.Size = new System.Drawing.Size(52, 20);
        this.txtRttMin.TabIndex = 27;
        // 
        // lblRttMax
        // 
        this.lblRttMax.AutoSize = true;
        this.lblRttMax.Location = new System.Drawing.Point(660, 22);
        this.lblRttMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblRttMax.Name = "lblRttMax";
        this.lblRttMax.Size = new System.Drawing.Size(51, 13);
        this.lblRttMax.TabIndex = 28;
        this.lblRttMax.Text = "RTT max";
        // 
        // txtRttMax
        // 
        this.txtRttMax.Location = new System.Drawing.Point(715, 19);
        this.txtRttMax.Margin = new System.Windows.Forms.Padding(2);
        this.txtRttMax.Name = "txtRttMax";
        this.txtRttMax.Size = new System.Drawing.Size(52, 20);
        this.txtRttMax.TabIndex = 29;
        // 
        // lvResults
        // 
        this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colIp, this.colStatus, this.colRtt, this.colHostname, this.colMac });
        this.lvResults.FullRowSelect = true;
        this.lvResults.GridLines = true;
        this.lvResults.HideSelection = false;
        this.lvResults.Location = new System.Drawing.Point(10, 251);
        this.lvResults.Margin = new System.Windows.Forms.Padding(2);
        this.lvResults.Name = "lvResults";
        this.lvResults.Size = new System.Drawing.Size(960, 345);
        this.lvResults.TabIndex = 16;
        this.lvResults.UseCompatibleStateImageBehavior = false;
        this.lvResults.View = System.Windows.Forms.View.Details;
        this.lvResults.VirtualMode = true;
        this.lvResults.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvResults_ColumnClick);
        this.lvResults.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvResults_RetrieveVirtualItem);
        this.lvResults.DoubleClick += new System.EventHandler(this.lvResults_DoubleClick);
        // 
        // colIp
        // 
        this.colIp.Text = "IP adresa";
        this.colIp.Width = 160;
        // 
        // colStatus
        // 
        this.colStatus.Text = "Stav";
        this.colStatus.Width = 100;
        // 
        // colRtt
        // 
        this.colRtt.Text = "RTT (ms)";
        this.colRtt.Width = 100;
        // 
        // colHostname
        // 
        this.colHostname.Text = "Hostname";
        this.colHostname.Width = 300;
        // 
        // colMac
        // 
        this.colMac.Text = "MAC";
        this.colMac.Width = 160;
        // 
        // rtbLog
        // 
        this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.rtbLog.Location = new System.Drawing.Point(10, 601);
        this.rtbLog.Margin = new System.Windows.Forms.Padding(2);
        this.rtbLog.Name = "rtbLog";
        this.rtbLog.ReadOnly = true;
        this.rtbLog.Size = new System.Drawing.Size(960, 70);
        this.rtbLog.TabIndex = 17;
        this.rtbLog.Text = "";
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(982, 682);
        this.Controls.Add(this.toolStripMain);
        this.Controls.Add(this.grpFilter);
        this.Controls.Add(this.tabInput);
        this.Controls.Add(this.grpRun);
        this.Controls.Add(this.rtbLog);
        this.Controls.Add(this.lvResults);
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "MainForm";
        this.Text = "Paralelní LAN Scanner";
        this.toolStripMain.ResumeLayout(false);
        this.toolStripMain.PerformLayout();
        this.tabInput.ResumeLayout(false);
        this.tabRange.ResumeLayout(false);
        this.tabRange.PerformLayout();
        this.tabSubnet.ResumeLayout(false);
        this.tabSubnet.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numWorkers)).EndInit();
        this.grpRun.ResumeLayout(false);
        this.grpRun.PerformLayout();
        this.grpFilter.ResumeLayout(false);
        this.grpFilter.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.ToolStrip toolStripMain;
    private System.Windows.Forms.ToolStripDropDownButton ddScan;
    private System.Windows.Forms.ToolStripMenuItem tsmiStart;
    private System.Windows.Forms.ToolStripMenuItem tsmiStop;
    private System.Windows.Forms.ToolStripDropDownButton ddData;
    private System.Windows.Forms.ToolStripMenuItem tsmiClearTable;
    private System.Windows.Forms.ToolStripMenuItem tsmiClearLog;
    private System.Windows.Forms.ToolStripDropDownButton ddExport;
    private System.Windows.Forms.ToolStripMenuItem tsmiExportCsv;
    private System.Windows.Forms.ToolStripMenuItem tsmiExportJson;
    private System.Windows.Forms.ToolStripMenuItem tsmiExportXml;
    private System.Windows.Forms.TabControl tabInput;
    private System.Windows.Forms.TabPage tabRange;
    private System.Windows.Forms.Label lblStartRange;
    private System.Windows.Forms.TextBox txtStartIpRange;
    private System.Windows.Forms.Label lblEndRange;
    private System.Windows.Forms.TextBox txtEndIpRange;
    private System.Windows.Forms.TabPage tabSubnet;
    private System.Windows.Forms.Label lblNet;
    private System.Windows.Forms.TextBox txtNetIp;
    private System.Windows.Forms.Label lblMask;
    private System.Windows.Forms.TextBox txtMask;
    private System.Windows.Forms.Label lblWorkers;
    private System.Windows.Forms.NumericUpDown numWorkers;
    private System.Windows.Forms.CheckBox chkDns;
    private System.Windows.Forms.GroupBox grpRun;
    private System.Windows.Forms.GroupBox grpFilter;
    private System.Windows.Forms.Label lblStatusFilter;
    private System.Windows.Forms.ComboBox cmbStatus;
    private System.Windows.Forms.Button btnApplyFilter;
    private System.Windows.Forms.Button btnClearFilter;
    private System.Windows.Forms.Label lblIpFilter;
    private System.Windows.Forms.TextBox txtFilterIp;
    private System.Windows.Forms.Label lblHostFilter;
    private System.Windows.Forms.TextBox txtFilterHost;
    private System.Windows.Forms.Label lblRttMin;
    private System.Windows.Forms.TextBox txtRttMin;
    private System.Windows.Forms.Label lblRttMax;
    private System.Windows.Forms.TextBox txtRttMax;
    private System.Windows.Forms.ListView lvResults;
    private System.Windows.Forms.ColumnHeader colIp;
    private System.Windows.Forms.ColumnHeader colStatus;
    private System.Windows.Forms.ColumnHeader colRtt;
    private System.Windows.Forms.ColumnHeader colHostname;
    private System.Windows.Forms.ColumnHeader colMac;
    private System.Windows.Forms.RichTextBox rtbLog;
}
