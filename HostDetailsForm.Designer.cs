namespace PortScanner;

partial class HostDetailsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.lblIp = new System.Windows.Forms.Label();
        this.txtIp = new System.Windows.Forms.TextBox();
        this.lblHost = new System.Windows.Forms.Label();
        this.txtHost = new System.Windows.Forms.TextBox();
        this.lblMac = new System.Windows.Forms.Label();
        this.txtMac = new System.Windows.Forms.TextBox();
        this.chkHttp = new System.Windows.Forms.CheckBox();
        this.chkHttps = new System.Windows.Forms.CheckBox();
        this.chkFtp = new System.Windows.Forms.CheckBox();
        this.btnStart = new System.Windows.Forms.Button();
        this.btnStop = new System.Windows.Forms.Button();
        this.lblPorts = new System.Windows.Forms.Label();
        this.numPortStart = new System.Windows.Forms.NumericUpDown();
        this.lblDash = new System.Windows.Forms.Label();
        this.numPortEnd = new System.Windows.Forms.NumericUpDown();
        this.cmbStatus = new System.Windows.Forms.ComboBox();
        this.lblStatus = new System.Windows.Forms.Label();
        this.lvServices = new System.Windows.Forms.ListView();
        this.colService = new System.Windows.Forms.ColumnHeader();
        this.colResult = new System.Windows.Forms.ColumnHeader();
        this.colInfo = new System.Windows.Forms.ColumnHeader();
        this.rtbLog = new System.Windows.Forms.RichTextBox();
        ((System.ComponentModel.ISupportInitialize)(this.numPortStart)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numPortEnd)).BeginInit();
        this.SuspendLayout();
        // 
        // lblIp
        // 
        this.lblIp.AutoSize = true;
        this.lblIp.Location = new System.Drawing.Point(12, 19);
        this.lblIp.Name = "lblIp";
        this.lblIp.Size = new System.Drawing.Size(17, 13);
        this.lblIp.TabIndex = 10;
        this.lblIp.Text = "IP";
        // 
        // txtIp
        // 
        this.txtIp.Location = new System.Drawing.Point(35, 16);
        this.txtIp.Name = "txtIp";
        this.txtIp.ReadOnly = true;
        this.txtIp.Size = new System.Drawing.Size(155, 20);
        this.txtIp.TabIndex = 9;
        // 
        // lblHost
        // 
        this.lblHost.AutoSize = true;
        this.lblHost.Location = new System.Drawing.Point(200, 19);
        this.lblHost.Name = "lblHost";
        this.lblHost.Size = new System.Drawing.Size(55, 13);
        this.lblHost.TabIndex = 11;
        this.lblHost.Text = "Hostname";
        // 
        // txtHost
        // 
        this.txtHost.Location = new System.Drawing.Point(261, 16);
        this.txtHost.Name = "txtHost";
        this.txtHost.ReadOnly = true;
        this.txtHost.Size = new System.Drawing.Size(140, 20);
        this.txtHost.TabIndex = 12;
        // 
        // lblMac
        // 
        this.lblMac.AutoSize = true;
        this.lblMac.Location = new System.Drawing.Point(12, 45);
        this.lblMac.Name = "lblMac";
        this.lblMac.Size = new System.Drawing.Size(30, 13);
        this.lblMac.TabIndex = 13;
        this.lblMac.Text = "MAC";
        // 
        // txtMac
        // 
        this.txtMac.Location = new System.Drawing.Point(48, 42);
        this.txtMac.Name = "txtMac";
        this.txtMac.ReadOnly = true;
        this.txtMac.Size = new System.Drawing.Size(142, 20);
        this.txtMac.TabIndex = 14;
        // 
        // chkHttp
        // 
        this.chkHttp.AutoSize = true;
        this.chkHttp.Location = new System.Drawing.Point(200, 45);
        this.chkHttp.Name = "chkHttp";
        this.chkHttp.Size = new System.Drawing.Size(55, 17);
        this.chkHttp.TabIndex = 7;
        this.chkHttp.Text = "HTTP";
        // 
        // chkHttps
        // 
        this.chkHttps.AutoSize = true;
        this.chkHttps.Location = new System.Drawing.Point(282, 45);
        this.chkHttps.Name = "chkHttps";
        this.chkHttps.Size = new System.Drawing.Size(62, 17);
        this.chkHttps.TabIndex = 6;
        this.chkHttps.Text = "HTTPS";
        // 
        // chkFtp
        // 
        this.chkFtp.AutoSize = true;
        this.chkFtp.Location = new System.Drawing.Point(377, 45);
        this.chkFtp.Name = "chkFtp";
        this.chkFtp.Size = new System.Drawing.Size(46, 17);
        this.chkFtp.TabIndex = 5;
        this.chkFtp.Text = "FTP";
        // 
        // btnStart
        // 
        this.btnStart.Location = new System.Drawing.Point(415, 12);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(69, 20);
        this.btnStart.TabIndex = 4;
        this.btnStart.Text = "Start";
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
        // 
        // btnStop
        // 
        this.btnStop.Location = new System.Drawing.Point(490, 12);
        this.btnStop.Name = "btnStop";
        this.btnStop.Size = new System.Drawing.Size(69, 20);
        this.btnStop.TabIndex = 3;
        this.btnStop.Text = "Stop";
        this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
        // 
        // lblPorts
        // 
        this.lblPorts.AutoSize = true;
        this.lblPorts.Location = new System.Drawing.Point(12, 74);
        this.lblPorts.Name = "lblPorts";
        this.lblPorts.Size = new System.Drawing.Size(31, 13);
        this.lblPorts.TabIndex = 15;
        this.lblPorts.Text = "Porty";
        // 
        // numPortStart
        // 
        this.numPortStart.Location = new System.Drawing.Point(51, 72);
        this.numPortStart.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        this.numPortStart.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.numPortStart.Name = "numPortStart";
        this.numPortStart.Size = new System.Drawing.Size(70, 20);
        this.numPortStart.TabIndex = 16;
        this.numPortStart.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lblDash
        // 
        this.lblDash.AutoSize = true;
        this.lblDash.Location = new System.Drawing.Point(126, 74);
        this.lblDash.Name = "lblDash";
        this.lblDash.Size = new System.Drawing.Size(10, 13);
        this.lblDash.TabIndex = 17;
        this.lblDash.Text = "-";
        // 
        // numPortEnd
        // 
        this.numPortEnd.Location = new System.Drawing.Point(142, 72);
        this.numPortEnd.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        this.numPortEnd.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.numPortEnd.Name = "numPortEnd";
        this.numPortEnd.Size = new System.Drawing.Size(70, 20);
        this.numPortEnd.TabIndex = 18;
        this.numPortEnd.Value = new decimal(new int[] { 1024, 0, 0, 0 });
        // 
        // cmbStatus
        // 
        this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbStatus.Items.AddRange(new object[] { "All", "Online", "Offline", "Error" });
        this.cmbStatus.Location = new System.Drawing.Point(51, 98);
        this.cmbStatus.Name = "cmbStatus";
        this.cmbStatus.Size = new System.Drawing.Size(103, 21);
        this.cmbStatus.TabIndex = 1;
        this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
        // 
        // lblStatus
        // 
        this.lblStatus.AutoSize = true;
        this.lblStatus.Location = new System.Drawing.Point(12, 101);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(23, 13);
        this.lblStatus.TabIndex = 2;
        this.lblStatus.Text = "Filtr";
        // 
        // lvServices
        // 
        this.lvServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colService, this.colResult, this.colInfo });
        this.lvServices.FullRowSelect = true;
        this.lvServices.GridLines = true;
        this.lvServices.HideSelection = false;
        this.lvServices.Location = new System.Drawing.Point(10, 125);
        this.lvServices.Name = "lvServices";
        this.lvServices.Size = new System.Drawing.Size(549, 211);
        this.lvServices.TabIndex = 0;
        this.lvServices.UseCompatibleStateImageBehavior = false;
        this.lvServices.View = System.Windows.Forms.View.Details;
        // 
        // colService
        // 
        this.colService.Text = "Služba";
        this.colService.Width = 120;
        // 
        // colResult
        // 
        this.colResult.Text = "Stav";
        this.colResult.Width = 80;
        // 
        // colInfo
        // 
        this.colInfo.Text = "Info";
        this.colInfo.Width = 250;
        // 
        // rtbLog
        // 
        this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.rtbLog.Location = new System.Drawing.Point(10, 342);
        this.rtbLog.Name = "rtbLog";
        this.rtbLog.ReadOnly = true;
        this.rtbLog.Size = new System.Drawing.Size(549, 80);
        this.rtbLog.TabIndex = 19;
        this.rtbLog.Text = "";
        // 
        // HostDetailsForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(571, 434);
        this.Controls.Add(this.rtbLog);
        this.Controls.Add(this.lvServices);
        this.Controls.Add(this.cmbStatus);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.btnStop);
        this.Controls.Add(this.btnStart);
        this.Controls.Add(this.chkFtp);
        this.Controls.Add(this.chkHttps);
        this.Controls.Add(this.chkHttp);
        this.Controls.Add(this.numPortEnd);
        this.Controls.Add(this.lblDash);
        this.Controls.Add(this.numPortStart);
        this.Controls.Add(this.lblPorts);
        this.Controls.Add(this.txtMac);
        this.Controls.Add(this.lblMac);
        this.Controls.Add(this.txtHost);
        this.Controls.Add(this.lblHost);
        this.Controls.Add(this.txtIp);
        this.Controls.Add(this.lblIp);
        this.Name = "HostDetailsForm";
        this.Text = "Detaily hosta";
        ((System.ComponentModel.ISupportInitialize)(this.numPortStart)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numPortEnd)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblIp;
    private System.Windows.Forms.TextBox txtIp;
    private System.Windows.Forms.Label lblHost;
    private System.Windows.Forms.TextBox txtHost;
    private System.Windows.Forms.Label lblMac;
    private System.Windows.Forms.TextBox txtMac;
    private System.Windows.Forms.CheckBox chkHttp;
    private System.Windows.Forms.CheckBox chkHttps;
    private System.Windows.Forms.CheckBox chkFtp;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Label lblPorts;
    private System.Windows.Forms.NumericUpDown numPortStart;
    private System.Windows.Forms.Label lblDash;
    private System.Windows.Forms.NumericUpDown numPortEnd;
    private System.Windows.Forms.ComboBox cmbStatus;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.ListView lvServices;
    private System.Windows.Forms.ColumnHeader colService;
    private System.Windows.Forms.ColumnHeader colResult;
    private System.Windows.Forms.ColumnHeader colInfo;
    private System.Windows.Forms.RichTextBox rtbLog;
}
