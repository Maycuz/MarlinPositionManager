namespace PrinterPositionManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbxStored = new System.Windows.Forms.ListBox();
            this.btRecallPos = new System.Windows.Forms.Button();
            this.btRemove = new System.Windows.Forms.Button();
            this.lbCurrent = new System.Windows.Forms.Label();
            this.btStore = new System.Windows.Forms.Button();
            this.gbPositions = new System.Windows.Forms.GroupBox();
            this.tbCurrentPosVal = new System.Windows.Forms.TextBox();
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.tbPrinterStatus = new System.Windows.Forms.TextBox();
            this.lbPrinterState = new System.Windows.Forms.Label();
            this.btPrinterDisconn = new System.Windows.Forms.Button();
            this.btPrinterConn = new System.Windows.Forms.Button();
            this.cbxBaudrate = new System.Windows.Forms.ComboBox();
            this.lbSelBaud = new System.Windows.Forms.Label();
            this.lbSelComPort = new System.Windows.Forms.Label();
            this.cbxComPort = new System.Windows.Forms.ComboBox();
            this.posRequest = new System.Windows.Forms.Timer(this.components);
            this.comCheck = new System.Windows.Forms.Timer(this.components);
            this.gbEditPos = new System.Windows.Forms.GroupBox();
            this.lbAliasMod = new System.Windows.Forms.Label();
            this.lbPosMod = new System.Windows.Forms.Label();
            this.btPosModStore = new System.Windows.Forms.Button();
            this.tbModPosAlias = new System.Windows.Forms.TextBox();
            this.tbModPosZ = new System.Windows.Forms.TextBox();
            this.tbModPosY = new System.Windows.Forms.TextBox();
            this.tbModPosX = new System.Windows.Forms.TextBox();
            this.gbPositions.SuspendLayout();
            this.gbConnection.SuspendLayout();
            this.gbEditPos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxStored
            // 
            this.lbxStored.FormattingEnabled = true;
            this.lbxStored.Location = new System.Drawing.Point(14, 97);
            this.lbxStored.Name = "lbxStored";
            this.lbxStored.Size = new System.Drawing.Size(189, 147);
            this.lbxStored.TabIndex = 0;
            this.lbxStored.SelectedIndexChanged += new System.EventHandler(this.lbxStored_SelectedIndexChanged);
            // 
            // btRecallPos
            // 
            this.btRecallPos.Enabled = false;
            this.btRecallPos.Location = new System.Drawing.Point(14, 249);
            this.btRecallPos.Name = "btRecallPos";
            this.btRecallPos.Size = new System.Drawing.Size(189, 20);
            this.btRecallPos.TabIndex = 1;
            this.btRecallPos.Text = "Recall";
            this.btRecallPos.UseVisualStyleBackColor = true;
            this.btRecallPos.Click += new System.EventHandler(this.btRecallPos_Click);
            // 
            // btRemove
            // 
            this.btRemove.Enabled = false;
            this.btRemove.Location = new System.Drawing.Point(14, 274);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(189, 20);
            this.btRemove.TabIndex = 2;
            this.btRemove.Text = "Remove";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // lbCurrent
            // 
            this.lbCurrent.AutoSize = true;
            this.lbCurrent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbCurrent.Location = new System.Drawing.Point(58, 19);
            this.lbCurrent.Name = "lbCurrent";
            this.lbCurrent.Size = new System.Drawing.Size(100, 15);
            this.lbCurrent.TabIndex = 3;
            this.lbCurrent.Text = "Current position:";
            // 
            // btStore
            // 
            this.btStore.Enabled = false;
            this.btStore.Location = new System.Drawing.Point(15, 67);
            this.btStore.Name = "btStore";
            this.btStore.Size = new System.Drawing.Size(188, 20);
            this.btStore.TabIndex = 4;
            this.btStore.Text = "Store";
            this.btStore.UseVisualStyleBackColor = true;
            this.btStore.Click += new System.EventHandler(this.btStore_Click);
            // 
            // gbPositions
            // 
            this.gbPositions.Controls.Add(this.tbCurrentPosVal);
            this.gbPositions.Controls.Add(this.lbxStored);
            this.gbPositions.Controls.Add(this.lbCurrent);
            this.gbPositions.Controls.Add(this.btRecallPos);
            this.gbPositions.Controls.Add(this.btRemove);
            this.gbPositions.Controls.Add(this.btStore);
            this.gbPositions.Location = new System.Drawing.Point(171, 5);
            this.gbPositions.Name = "gbPositions";
            this.gbPositions.Size = new System.Drawing.Size(209, 304);
            this.gbPositions.TabIndex = 7;
            this.gbPositions.TabStop = false;
            this.gbPositions.Text = "Positioning";
            // 
            // tbCurrentPosVal
            // 
            this.tbCurrentPosVal.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.tbCurrentPosVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tbCurrentPosVal.Location = new System.Drawing.Point(14, 35);
            this.tbCurrentPosVal.Name = "tbCurrentPosVal";
            this.tbCurrentPosVal.ReadOnly = true;
            this.tbCurrentPosVal.Size = new System.Drawing.Size(189, 23);
            this.tbCurrentPosVal.TabIndex = 8;
            this.tbCurrentPosVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.tbPrinterStatus);
            this.gbConnection.Controls.Add(this.lbPrinterState);
            this.gbConnection.Controls.Add(this.btPrinterDisconn);
            this.gbConnection.Controls.Add(this.btPrinterConn);
            this.gbConnection.Controls.Add(this.cbxBaudrate);
            this.gbConnection.Controls.Add(this.lbSelBaud);
            this.gbConnection.Controls.Add(this.lbSelComPort);
            this.gbConnection.Controls.Add(this.cbxComPort);
            this.gbConnection.Location = new System.Drawing.Point(5, 5);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Size = new System.Drawing.Size(160, 180);
            this.gbConnection.TabIndex = 8;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Printer Connection";
            // 
            // tbPrinterStatus
            // 
            this.tbPrinterStatus.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.tbPrinterStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.tbPrinterStatus.Location = new System.Drawing.Point(5, 35);
            this.tbPrinterStatus.Name = "tbPrinterStatus";
            this.tbPrinterStatus.ReadOnly = true;
            this.tbPrinterStatus.Size = new System.Drawing.Size(144, 20);
            this.tbPrinterStatus.TabIndex = 7;
            this.tbPrinterStatus.Text = "Connected";
            this.tbPrinterStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbPrinterState
            // 
            this.lbPrinterState.AutoSize = true;
            this.lbPrinterState.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbPrinterState.Location = new System.Drawing.Point(39, 19);
            this.lbPrinterState.Name = "lbPrinterState";
            this.lbPrinterState.Size = new System.Drawing.Size(85, 15);
            this.lbPrinterState.TabIndex = 6;
            this.lbPrinterState.Text = "Printer status:";
            // 
            // btPrinterDisconn
            // 
            this.btPrinterDisconn.Enabled = false;
            this.btPrinterDisconn.Location = new System.Drawing.Point(5, 152);
            this.btPrinterDisconn.Name = "btPrinterDisconn";
            this.btPrinterDisconn.Size = new System.Drawing.Size(143, 20);
            this.btPrinterDisconn.TabIndex = 5;
            this.btPrinterDisconn.Text = "Disconnect";
            this.btPrinterDisconn.UseVisualStyleBackColor = true;
            this.btPrinterDisconn.Click += new System.EventHandler(this.btPrinterDisconn_Click);
            // 
            // btPrinterConn
            // 
            this.btPrinterConn.Location = new System.Drawing.Point(5, 127);
            this.btPrinterConn.Name = "btPrinterConn";
            this.btPrinterConn.Size = new System.Drawing.Size(143, 20);
            this.btPrinterConn.TabIndex = 4;
            this.btPrinterConn.Text = "Connect";
            this.btPrinterConn.UseVisualStyleBackColor = true;
            this.btPrinterConn.Click += new System.EventHandler(this.btPrinterConn_Click);
            // 
            // cbxBaudrate
            // 
            this.cbxBaudrate.FormattingEnabled = true;
            this.cbxBaudrate.Location = new System.Drawing.Point(64, 92);
            this.cbxBaudrate.Name = "cbxBaudrate";
            this.cbxBaudrate.Size = new System.Drawing.Size(85, 21);
            this.cbxBaudrate.TabIndex = 3;
            // 
            // lbSelBaud
            // 
            this.lbSelBaud.AutoSize = true;
            this.lbSelBaud.Location = new System.Drawing.Point(5, 94);
            this.lbSelBaud.Name = "lbSelBaud";
            this.lbSelBaud.Size = new System.Drawing.Size(53, 13);
            this.lbSelBaud.TabIndex = 2;
            this.lbSelBaud.Text = "Baudrate:";
            // 
            // lbSelComPort
            // 
            this.lbSelComPort.AutoSize = true;
            this.lbSelComPort.Location = new System.Drawing.Point(5, 69);
            this.lbSelComPort.Name = "lbSelComPort";
            this.lbSelComPort.Size = new System.Drawing.Size(55, 13);
            this.lbSelComPort.TabIndex = 1;
            this.lbSelComPort.Text = "COM port:";
            // 
            // cbxComPort
            // 
            this.cbxComPort.FormattingEnabled = true;
            this.cbxComPort.Location = new System.Drawing.Point(64, 67);
            this.cbxComPort.Name = "cbxComPort";
            this.cbxComPort.Size = new System.Drawing.Size(85, 21);
            this.cbxComPort.TabIndex = 0;
            // 
            // posRequest
            // 
            this.posRequest.Interval = 1000;
            this.posRequest.Tick += new System.EventHandler(this.posRequest_Tick);
            // 
            // comCheck
            // 
            this.comCheck.Tick += new System.EventHandler(this.comCheck_Tick);
            // 
            // gbEditPos
            // 
            this.gbEditPos.Controls.Add(this.lbAliasMod);
            this.gbEditPos.Controls.Add(this.lbPosMod);
            this.gbEditPos.Controls.Add(this.btPosModStore);
            this.gbEditPos.Controls.Add(this.tbModPosAlias);
            this.gbEditPos.Controls.Add(this.tbModPosZ);
            this.gbEditPos.Controls.Add(this.tbModPosY);
            this.gbEditPos.Controls.Add(this.tbModPosX);
            this.gbEditPos.Enabled = false;
            this.gbEditPos.Location = new System.Drawing.Point(5, 191);
            this.gbEditPos.Name = "gbEditPos";
            this.gbEditPos.Size = new System.Drawing.Size(160, 118);
            this.gbEditPos.TabIndex = 9;
            this.gbEditPos.TabStop = false;
            this.gbEditPos.Text = "Modify";
            // 
            // lbAliasMod
            // 
            this.lbAliasMod.AutoSize = true;
            this.lbAliasMod.Location = new System.Drawing.Point(8, 47);
            this.lbAliasMod.Name = "lbAliasMod";
            this.lbAliasMod.Size = new System.Drawing.Size(32, 13);
            this.lbAliasMod.TabIndex = 6;
            this.lbAliasMod.Text = "Alias:";
            // 
            // lbPosMod
            // 
            this.lbPosMod.AutoSize = true;
            this.lbPosMod.Location = new System.Drawing.Point(8, 22);
            this.lbPosMod.Name = "lbPosMod";
            this.lbPosMod.Size = new System.Drawing.Size(31, 13);
            this.lbPosMod.TabIndex = 5;
            this.lbPosMod.Text = "Pos.:";
            // 
            // btPosModStore
            // 
            this.btPosModStore.Location = new System.Drawing.Point(5, 69);
            this.btPosModStore.Name = "btPosModStore";
            this.btPosModStore.Size = new System.Drawing.Size(144, 39);
            this.btPosModStore.TabIndex = 4;
            this.btPosModStore.Text = "Save";
            this.btPosModStore.UseVisualStyleBackColor = true;
            this.btPosModStore.Click += new System.EventHandler(this.btPosModStore_Click);
            // 
            // tbModPosAlias
            // 
            this.tbModPosAlias.Location = new System.Drawing.Point(49, 44);
            this.tbModPosAlias.Name = "tbModPosAlias";
            this.tbModPosAlias.Size = new System.Drawing.Size(101, 20);
            this.tbModPosAlias.TabIndex = 3;
            // 
            // tbModPosZ
            // 
            this.tbModPosZ.Location = new System.Drawing.Point(119, 19);
            this.tbModPosZ.Name = "tbModPosZ";
            this.tbModPosZ.Size = new System.Drawing.Size(31, 20);
            this.tbModPosZ.TabIndex = 2;
            this.tbModPosZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnsureInputIsNumeric);
            // 
            // tbModPosY
            // 
            this.tbModPosY.Location = new System.Drawing.Point(84, 19);
            this.tbModPosY.Name = "tbModPosY";
            this.tbModPosY.Size = new System.Drawing.Size(31, 20);
            this.tbModPosY.TabIndex = 1;
            this.tbModPosY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnsureInputIsNumeric);
            // 
            // tbModPosX
            // 
            this.tbModPosX.Location = new System.Drawing.Point(49, 19);
            this.tbModPosX.Name = "tbModPosX";
            this.tbModPosX.Size = new System.Drawing.Size(31, 20);
            this.tbModPosX.TabIndex = 0;
            this.tbModPosX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnsureInputIsNumeric);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 313);
            this.Controls.Add(this.gbEditPos);
            this.Controls.Add(this.gbConnection);
            this.Controls.Add(this.gbPositions);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = global::MarlinPositionManager.Resource.icon;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(402, 352);
            this.MinimumSize = new System.Drawing.Size(402, 352);
            this.Name = "Form1";
            this.Text = "Marlin Position Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbPositions.ResumeLayout(false);
            this.gbPositions.PerformLayout();
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.gbEditPos.ResumeLayout(false);
            this.gbEditPos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox lbxStored;
        private Button btRecallPos;
        private Button btRemove;
        private Label lbCurrent;
        private Button btStore;
        private GroupBox gbPositions;
        private GroupBox gbConnection;
        private Label lbSelBaud;
        private Label lbSelComPort;
        private ComboBox cbxComPort;
        private Label lbPrinterState;
        private Button btPrinterDisconn;
        private Button btPrinterConn;
        private ComboBox cbxBaudrate;
        private System.Windows.Forms.Timer posRequest;
        private System.Windows.Forms.Timer comCheck;
        private GroupBox gbEditPos;
        private Label lbPosMod;
        private Button btPosModStore;
        private TextBox tbModPosAlias;
        private TextBox tbModPosZ;
        private TextBox tbModPosY;
        private TextBox tbModPosX;
        private Label lbAliasMod;
        private TextBox tbCurrentPosVal;
        private TextBox tbPrinterStatus;
    }
}