namespace PortScanner;

partial class Form1
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
        this.chkPortScan = new System.Windows.Forms.CheckBox();
        this.txtPorts = new System.Windows.Forms.TextBox();
        this.btnStart = new System.Windows.Forms.Button();
        this.btnStop = new System.Windows.Forms.Button();
        this.grpRun = new System.Windows.Forms.GroupBox();
        this.grpExport = new System.Windows.Forms.GroupBox();
        this.btnExportCsv = new System.Windows.Forms.Button();
        this.btnExportJson = new System.Windows.Forms.Button();
        this.btnExportXml = new System.Windows.Forms.Button();
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
        this.btnClearOutput = new System.Windows.Forms.Button();
        this.btnClearLog = new System.Windows.Forms.Button();
        this.lvResults = new System.Windows.Forms.ListView();
        this.colIp = new System.Windows.Forms.ColumnHeader();
        this.colStatus = new System.Windows.Forms.ColumnHeader();
        this.colRtt = new System.Windows.Forms.ColumnHeader();
        this.colHostname = new System.Windows.Forms.ColumnHeader();
        this.colMac = new System.Windows.Forms.ColumnHeader();
        this.rtbLog = new System.Windows.Forms.RichTextBox();
        this.tabInput.SuspendLayout();
        this.tabRange.SuspendLayout();
        this.tabSubnet.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numWorkers)).BeginInit();
        this.grpRun.SuspendLayout();
        this.grpExport.SuspendLayout();
        this.grpFilter.SuspendLayout();
        this.SuspendLayout();
        // 
        // tabInput
        // 
        this.tabInput.Controls.Add(this.tabRange);
        this.tabInput.Controls.Add(this.tabSubnet);
        this.tabInput.Location = new System.Drawing.Point(14, 11);
        this.tabInput.Name = "tabInput";
        this.tabInput.SelectedIndex = 0;
        this.tabInput.Size = new System.Drawing.Size(530, 100);
        this.tabInput.TabIndex = 0;
        // 
        // tabRange
        // 
        this.tabRange.Controls.Add(this.lblStartRange);
        this.tabRange.Controls.Add(this.txtStartIpRange);
        this.tabRange.Controls.Add(this.lblEndRange);
        this.tabRange.Controls.Add(this.txtEndIpRange);
        this.tabRange.Location = new System.Drawing.Point(4, 25);
        this.tabRange.Name = "tabRange";
        this.tabRange.Padding = new System.Windows.Forms.Padding(9);
        this.tabRange.Size = new System.Drawing.Size(522, 71);
        this.tabRange.TabIndex = 0;
        this.tabRange.Text = "Rozsah";
        // 
        // lblStartRange
        // 
        this.lblStartRange.AutoSize = true;
        this.lblStartRange.Location = new System.Drawing.Point(9, 11);
        this.lblStartRange.Name = "lblStartRange";
        this.lblStartRange.Size = new System.Drawing.Size(54, 17);
        this.lblStartRange.TabIndex = 0;
        this.lblStartRange.Text = "Start IP";
        // 
        // txtStartIpRange
        // 
        this.txtStartIpRange.Location = new System.Drawing.Point(91, 7);
        this.txtStartIpRange.Name = "txtStartIpRange";
        this.txtStartIpRange.Size = new System.Drawing.Size(182, 22);
        this.txtStartIpRange.TabIndex = 1;
        this.txtStartIpRange.Text = "172.0.0.0";
        // 
        // lblEndRange
        // 
        this.lblEndRange.AutoSize = true;
        this.lblEndRange.Location = new System.Drawing.Point(9, 43);
        this.lblEndRange.Name = "lblEndRange";
        this.lblEndRange.Size = new System.Drawing.Size(49, 17);
        this.lblEndRange.TabIndex = 2;
        this.lblEndRange.Text = "End IP";
        // 
        // txtEndIpRange
        // 
        this.txtEndIpRange.Location = new System.Drawing.Point(91, 39);
        this.txtEndIpRange.Name = "txtEndIpRange";
        this.txtEndIpRange.Size = new System.Drawing.Size(182, 22);
        this.txtEndIpRange.TabIndex = 2;
        this.txtEndIpRange.Text = "172.0.0.255";
        // 
        // tabSubnet
        // 
        this.tabSubnet.Controls.Add(this.lblNet);
        this.tabSubnet.Controls.Add(this.txtNetIp);
        this.tabSubnet.Controls.Add(this.lblMask);
        this.tabSubnet.Controls.Add(this.txtMask);
        this.tabSubnet.Location = new System.Drawing.Point(4, 25);
        this.tabSubnet.Name = "tabSubnet";
        this.tabSubnet.Padding = new System.Windows.Forms.Padding(9);
        this.tabSubnet.Size = new System.Drawing.Size(403, 71);
        this.tabSubnet.TabIndex = 1;
        this.tabSubnet.Text = "Síť/Maska";
        // 
        // lblNet
        // 
        this.lblNet.AutoSize = true;
        this.lblNet.Location = new System.Drawing.Point(9, 11);
        this.lblNet.Name = "lblNet";
        this.lblNet.Size = new System.Drawing.Size(41, 17);
        this.lblNet.TabIndex = 0;
        this.lblNet.Text = "Síť IP";
        // 
        // txtNetIp
        // 
        this.txtNetIp.Location = new System.Drawing.Point(91, 7);
        this.txtNetIp.Name = "txtNetIp";
        this.txtNetIp.Size = new System.Drawing.Size(182, 22);
        this.txtNetIp.TabIndex = 1;
        this.txtNetIp.Text = "172.0.0.0";
        // 
        // lblMask
        // 
        this.lblMask.AutoSize = true;
        this.lblMask.Location = new System.Drawing.Point(9, 43);
        this.lblMask.Name = "lblMask";
        this.lblMask.Size = new System.Drawing.Size(49, 17);
        this.lblMask.TabIndex = 2;
        this.lblMask.Text = "Maska";
        // 
        // txtMask
        // 
        this.txtMask.Location = new System.Drawing.Point(91, 39);
        this.txtMask.Name = "txtMask";
        this.txtMask.Size = new System.Drawing.Size(182, 22);
        this.txtMask.TabIndex = 2;
        this.txtMask.Text = "255.255.255.0";
        // 
        // lblWorkers
        // 
        this.lblWorkers.AutoSize = true;
        this.lblWorkers.Location = new System.Drawing.Point(14, 23);
        this.lblWorkers.Name = "lblWorkers";
        this.lblWorkers.Size = new System.Drawing.Size(61, 17);
        this.lblWorkers.TabIndex = 4;
        this.lblWorkers.Text = "Workery";
        // 
        // numWorkers
        // 
        this.numWorkers.Location = new System.Drawing.Point(111, 21);
        this.numWorkers.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
        this.numWorkers.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.numWorkers.Name = "numWorkers";
        this.numWorkers.Size = new System.Drawing.Size(91, 22);
        this.numWorkers.TabIndex = 5;
        this.numWorkers.Value = new decimal(new int[] { 100, 0, 0, 0 });
        // 
        // chkDns
        // 
        this.chkDns.AutoSize = true;
        this.chkDns.Checked = true;
        this.chkDns.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkDns.Location = new System.Drawing.Point(14, 49);
        this.chkDns.Name = "chkDns";
        this.chkDns.Size = new System.Drawing.Size(105, 21);
        this.chkDns.TabIndex = 6;
        this.chkDns.Text = "DNS lookup";
        this.chkDns.UseVisualStyleBackColor = true;
        // 
        // chkPortScan
        // 
        this.chkPortScan.AutoSize = true;
        this.chkPortScan.Location = new System.Drawing.Point(14, 76);
        this.chkPortScan.Name = "chkPortScan";
        this.chkPortScan.Size = new System.Drawing.Size(99, 21);
        this.chkPortScan.TabIndex = 7;
        this.chkPortScan.Text = "Scan portů";
        this.chkPortScan.UseVisualStyleBackColor = true;
        // 
        // txtPorts
        // 
        this.txtPorts.Location = new System.Drawing.Point(119, 73);
        this.txtPorts.Name = "txtPorts";
        this.txtPorts.Size = new System.Drawing.Size(91, 22);
        this.txtPorts.TabIndex = 8;
        this.txtPorts.Text = "80,443";
        // 
        // btnStart
        // 
        this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnStart.Location = new System.Drawing.Point(14, 673);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(91, 25);
        this.btnStart.TabIndex = 9;
        this.btnStart.Text = "Start";
        this.btnStart.UseVisualStyleBackColor = true;
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
        // 
        // btnStop
        // 
        this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnStop.Location = new System.Drawing.Point(116, 673);
        this.btnStop.Name = "btnStop";
        this.btnStop.Size = new System.Drawing.Size(91, 25);
        this.btnStop.TabIndex = 10;
        this.btnStop.Text = "Stop";
        this.btnStop.UseVisualStyleBackColor = true;
        this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
        // 
        // grpRun
        // 
        this.grpRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.grpRun.Controls.Add(this.lblWorkers);
        this.grpRun.Controls.Add(this.numWorkers);
        this.grpRun.Controls.Add(this.chkDns);
        this.grpRun.Controls.Add(this.chkPortScan);
        this.grpRun.Controls.Add(this.txtPorts);
        this.grpRun.Location = new System.Drawing.Point(550, 12);
        this.grpRun.Name = "grpRun";
        this.grpRun.Size = new System.Drawing.Size(548, 99);
        this.grpRun.TabIndex = 100;
        this.grpRun.TabStop = false;
        this.grpRun.Text = "Sken";
        // 
        // grpExport
        // 
        this.grpExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.grpExport.Controls.Add(this.btnExportCsv);
        this.grpExport.Controls.Add(this.btnExportJson);
        this.grpExport.Controls.Add(this.btnExportXml);
        this.grpExport.Location = new System.Drawing.Point(550, 121);
        this.grpExport.Name = "grpExport";
        this.grpExport.Size = new System.Drawing.Size(548, 116);
        this.grpExport.TabIndex = 101;
        this.grpExport.TabStop = false;
        this.grpExport.Text = "Export";
        // 
        // btnExportCsv
        // 
        this.btnExportCsv.Location = new System.Drawing.Point(14, 85);
        this.btnExportCsv.Name = "btnExportCsv";
        this.btnExportCsv.Size = new System.Drawing.Size(91, 25);
        this.btnExportCsv.TabIndex = 13;
        this.btnExportCsv.Text = "Export CSV";
        this.btnExportCsv.UseVisualStyleBackColor = true;
        this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
        // 
        // btnExportJson
        // 
        this.btnExportJson.Location = new System.Drawing.Point(111, 85);
        this.btnExportJson.Name = "btnExportJson";
        this.btnExportJson.Size = new System.Drawing.Size(114, 25);
        this.btnExportJson.TabIndex = 14;
        this.btnExportJson.Text = "Export JSON";
        this.btnExportJson.UseVisualStyleBackColor = true;
        this.btnExportJson.Click += new System.EventHandler(this.btnExportJson_Click);
        // 
        // btnExportXml
        // 
        this.btnExportXml.Location = new System.Drawing.Point(231, 85);
        this.btnExportXml.Name = "btnExportXml";
        this.btnExportXml.Size = new System.Drawing.Size(114, 25);
        this.btnExportXml.TabIndex = 15;
        this.btnExportXml.Text = "Export XML";
        this.btnExportXml.UseVisualStyleBackColor = true;
        this.btnExportXml.Click += new System.EventHandler(this.btnExportXml_Click);
        // 
        // grpFilter
        // 
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
        this.grpFilter.Location = new System.Drawing.Point(14, 113);
        this.grpFilter.Name = "grpFilter";
        this.grpFilter.Size = new System.Drawing.Size(530, 126);
        this.grpFilter.TabIndex = 102;
        this.grpFilter.TabStop = false;
        this.grpFilter.Text = "Filtry";
        // 
        // lblStatusFilter
        // 
        this.lblStatusFilter.AutoSize = true;
        this.lblStatusFilter.Location = new System.Drawing.Point(7, 23);
        this.lblStatusFilter.Name = "lblStatusFilter";
        this.lblStatusFilter.Size = new System.Drawing.Size(36, 17);
        this.lblStatusFilter.TabIndex = 18;
        this.lblStatusFilter.Text = "Stav";
        // 
        // cmbStatus
        // 
        this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbStatus.FormattingEnabled = true;
        this.cmbStatus.Items.AddRange(new object[] { "All", "Online", "Offline", "Timeout", "Error" });
        this.cmbStatus.Location = new System.Drawing.Point(163, 16);
        this.cmbStatus.Name = "cmbStatus";
        this.cmbStatus.Size = new System.Drawing.Size(137, 24);
        this.cmbStatus.TabIndex = 19;
        // 
        // btnApplyFilter
        // 
        this.btnApplyFilter.Location = new System.Drawing.Point(433, 95);
        this.btnApplyFilter.Name = "btnApplyFilter";
        this.btnApplyFilter.Size = new System.Drawing.Size(91, 25);
        this.btnApplyFilter.TabIndex = 20;
        this.btnApplyFilter.Text = "Filtrovat";
        this.btnApplyFilter.UseVisualStyleBackColor = true;
        this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
        // 
        // btnClearFilter
        // 
        this.btnClearFilter.Location = new System.Drawing.Point(336, 95);
        this.btnClearFilter.Name = "btnClearFilter";
        this.btnClearFilter.Size = new System.Drawing.Size(91, 25);
        this.btnClearFilter.TabIndex = 21;
        this.btnClearFilter.Text = "Reset filt.";
        this.btnClearFilter.UseVisualStyleBackColor = true;
        this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
        // 
        // lblIpFilter
        // 
        this.lblIpFilter.AutoSize = true;
        this.lblIpFilter.Location = new System.Drawing.Point(6, 46);
        this.lblIpFilter.Name = "lblIpFilter";
        this.lblIpFilter.Size = new System.Drawing.Size(43, 17);
        this.lblIpFilter.TabIndex = 22;
        this.lblIpFilter.Text = "IP filtr";
        // 
        // txtFilterIp
        // 
        this.txtFilterIp.Location = new System.Drawing.Point(141, 46);
        this.txtFilterIp.Name = "txtFilterIp";
        this.txtFilterIp.Size = new System.Drawing.Size(159, 22);
        this.txtFilterIp.TabIndex = 23;
        // 
        // lblHostFilter
        // 
        this.lblHostFilter.AutoSize = true;
        this.lblHostFilter.Location = new System.Drawing.Point(6, 74);
        this.lblHostFilter.Name = "lblHostFilter";
        this.lblHostFilter.Size = new System.Drawing.Size(95, 17);
        this.lblHostFilter.TabIndex = 24;
        this.lblHostFilter.Text = "Hostname filtr";
        // 
        // txtFilterHost
        // 
        this.txtFilterHost.Location = new System.Drawing.Point(107, 74);
        this.txtFilterHost.Name = "txtFilterHost";
        this.txtFilterHost.Size = new System.Drawing.Size(194, 22);
        this.txtFilterHost.TabIndex = 25;
        // 
        // lblRttMin
        // 
        this.lblRttMin.AutoSize = true;
        this.lblRttMin.Location = new System.Drawing.Point(7, 101);
        this.lblRttMin.Name = "lblRttMin";
        this.lblRttMin.Size = new System.Drawing.Size(62, 17);
        this.lblRttMin.TabIndex = 26;
        this.lblRttMin.Text = "RTT min";
        // 
        // txtRttMin
        // 
        this.txtRttMin.Location = new System.Drawing.Point(81, 101);
        this.txtRttMin.Name = "txtRttMin";
        this.txtRttMin.Size = new System.Drawing.Size(68, 22);
        this.txtRttMin.TabIndex = 27;
        // 
        // lblRttMax
        // 
        this.lblRttMax.AutoSize = true;
        this.lblRttMax.Location = new System.Drawing.Point(161, 103);
        this.lblRttMax.Name = "lblRttMax";
        this.lblRttMax.Size = new System.Drawing.Size(65, 17);
        this.lblRttMax.TabIndex = 28;
        this.lblRttMax.Text = "RTT max";
        // 
        // txtRttMax
        // 
        this.txtRttMax.Location = new System.Drawing.Point(232, 104);
        this.txtRttMax.Name = "txtRttMax";
        this.txtRttMax.Size = new System.Drawing.Size(68, 22);
        this.txtRttMax.TabIndex = 29;
        // 
        // btnClearOutput
        // 
        this.btnClearOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnClearOutput.Location = new System.Drawing.Point(910, 673);
        this.btnClearOutput.Name = "btnClearOutput";
        this.btnClearOutput.Size = new System.Drawing.Size(91, 25);
        this.btnClearOutput.TabIndex = 11;
        this.btnClearOutput.Text = "Clear";
        this.btnClearOutput.UseVisualStyleBackColor = true;
        this.btnClearOutput.Click += new System.EventHandler(this.btnClearOutput_Click);
        // 
        // btnClearLog
        // 
        this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnClearLog.Location = new System.Drawing.Point(1007, 673);
        this.btnClearLog.Name = "btnClearLog";
        this.btnClearLog.Size = new System.Drawing.Size(91, 25);
        this.btnClearLog.TabIndex = 12;
        this.btnClearLog.Text = "Clear Log";
        this.btnClearLog.UseVisualStyleBackColor = true;
        this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
        // 
        // lvResults
        // 
        this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colIp, this.colStatus, this.colRtt, this.colHostname, this.colMac });
        this.lvResults.FullRowSelect = true;
        this.lvResults.GridLines = true;
        this.lvResults.HideSelection = false;
        this.lvResults.Location = new System.Drawing.Point(14, 245);
        this.lvResults.Name = "lvResults";
        this.lvResults.Size = new System.Drawing.Size(1084, 350);
        this.lvResults.TabIndex = 16;
        this.lvResults.UseCompatibleStateImageBehavior = false;
        this.lvResults.View = System.Windows.Forms.View.Details;
        this.lvResults.VirtualMode = true;
        this.lvResults.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvResults_ColumnClick);
        this.lvResults.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvResults_RetrieveVirtualItem);
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
        this.rtbLog.Location = new System.Drawing.Point(14, 602);
        this.rtbLog.Name = "rtbLog";
        this.rtbLog.ReadOnly = true;
        this.rtbLog.Size = new System.Drawing.Size(1084, 65);
        this.rtbLog.TabIndex = 17;
        this.rtbLog.Text = "";
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1114, 701);
        this.Controls.Add(this.grpFilter);
        this.Controls.Add(this.grpExport);
        this.Controls.Add(this.btnClearLog);
        this.Controls.Add(this.btnClearOutput);
        this.Controls.Add(this.tabInput);
        this.Controls.Add(this.btnStop);
        this.Controls.Add(this.btnStart);
        this.Controls.Add(this.grpRun);
        this.Controls.Add(this.rtbLog);
        this.Controls.Add(this.lvResults);
        this.Name = "Form1";
        this.Text = "Paralelní LAN Scanner";
        this.tabInput.ResumeLayout(false);
        this.tabRange.ResumeLayout(false);
        this.tabRange.PerformLayout();
        this.tabSubnet.ResumeLayout(false);
        this.tabSubnet.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numWorkers)).EndInit();
        this.grpRun.ResumeLayout(false);
        this.grpRun.PerformLayout();
        this.grpExport.ResumeLayout(false);
        this.grpFilter.ResumeLayout(false);
        this.grpFilter.PerformLayout();
        this.ResumeLayout(false);
    }

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
    private System.Windows.Forms.CheckBox chkPortScan;
    private System.Windows.Forms.TextBox txtPorts;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.GroupBox grpRun;
    private System.Windows.Forms.GroupBox grpExport;
    private System.Windows.Forms.GroupBox grpFilter;
    private System.Windows.Forms.Button btnClearOutput;
    private System.Windows.Forms.Button btnClearLog;
    private System.Windows.Forms.Button btnExportCsv;
    private System.Windows.Forms.Button btnExportJson;
    private System.Windows.Forms.Button btnExportXml;
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
