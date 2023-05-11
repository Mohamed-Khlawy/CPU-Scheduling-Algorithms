namespace OS_Project_2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProcessNum = new System.Windows.Forms.TextBox();
            this.ProcessesData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResetButton = new System.Windows.Forms.Button();
            this.btnFCFS = new System.Windows.Forms.Button();
            this.btnSJFp = new System.Windows.Forms.Button();
            this.btnSJFnp = new System.Windows.Forms.Button();
            this.btnRR = new System.Windows.Forms.Button();
            this.txtQuantum = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnScheduleReset = new System.Windows.Forms.Button();
            this.SchedulerTable = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AvgWaiting = new System.Windows.Forms.Label();
            this.AvgTurnaround = new System.Windows.Forms.Label();
            this.IdealInformation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessesData)).BeginInit();
            this.SuspendLayout();
            // 
            // ProcessNum
            // 
            this.ProcessNum.Location = new System.Drawing.Point(12, 12);
            this.ProcessNum.Name = "ProcessNum";
            this.ProcessNum.Size = new System.Drawing.Size(129, 24);
            this.ProcessNum.TabIndex = 0;
            this.ProcessNum.Text = "Number of Processes";
            this.ProcessNum.TextChanged += new System.EventHandler(this.ProcessNum_TextChanged);
            // 
            // ProcessesData
            // 
            this.ProcessesData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProcessesData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.ProcessesData.Location = new System.Drawing.Point(12, 55);
            this.ProcessesData.Name = "ProcessesData";
            this.ProcessesData.RowHeadersWidth = 51;
            this.ProcessesData.RowTemplate.Height = 26;
            this.ProcessesData.Size = new System.Drawing.Size(465, 380);
            this.ProcessesData.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Process Name";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Arrival Time";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Burst Time";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(646, 12);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(131, 32);
            this.ResetButton.TabIndex = 2;
            this.ResetButton.Text = "ResetTheProgram";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // btnFCFS
            // 
            this.btnFCFS.Location = new System.Drawing.Point(646, 114);
            this.btnFCFS.Name = "btnFCFS";
            this.btnFCFS.Size = new System.Drawing.Size(92, 23);
            this.btnFCFS.TabIndex = 3;
            this.btnFCFS.Text = "ApplyFCFS";
            this.btnFCFS.UseVisualStyleBackColor = true;
            this.btnFCFS.Click += new System.EventHandler(this.btnFCFS_Click);
            // 
            // btnSJFp
            // 
            this.btnSJFp.Location = new System.Drawing.Point(646, 174);
            this.btnSJFp.Name = "btnSJFp";
            this.btnSJFp.Size = new System.Drawing.Size(92, 23);
            this.btnSJFp.TabIndex = 4;
            this.btnSJFp.Text = "ApplySJFp";
            this.btnSJFp.UseVisualStyleBackColor = true;
            this.btnSJFp.Click += new System.EventHandler(this.btnSJFp_Click);
            // 
            // btnSJFnp
            // 
            this.btnSJFnp.Location = new System.Drawing.Point(646, 233);
            this.btnSJFnp.Name = "btnSJFnp";
            this.btnSJFnp.Size = new System.Drawing.Size(92, 23);
            this.btnSJFnp.TabIndex = 5;
            this.btnSJFnp.Text = "ApplySJFnp";
            this.btnSJFnp.UseVisualStyleBackColor = true;
            this.btnSJFnp.Click += new System.EventHandler(this.btnSJFnp_Click);
            // 
            // btnRR
            // 
            this.btnRR.Location = new System.Drawing.Point(646, 290);
            this.btnRR.Name = "btnRR";
            this.btnRR.Size = new System.Drawing.Size(92, 23);
            this.btnRR.TabIndex = 6;
            this.btnRR.Text = "ApplyRR";
            this.btnRR.UseVisualStyleBackColor = true;
            this.btnRR.Click += new System.EventHandler(this.btnRR_Click);
            // 
            // txtQuantum
            // 
            this.txtQuantum.Location = new System.Drawing.Point(500, 290);
            this.txtQuantum.Name = "txtQuantum";
            this.txtQuantum.Size = new System.Drawing.Size(119, 24);
            this.txtQuantum.TabIndex = 7;
            this.txtQuantum.Text = "EnterQuantumValue";
            this.txtQuantum.TextChanged += new System.EventHandler(this.txtQuantum_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(173, 455);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 32);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "SaveData";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnScheduleReset
            // 
            this.btnScheduleReset.Location = new System.Drawing.Point(1002, 403);
            this.btnScheduleReset.Name = "btnScheduleReset";
            this.btnScheduleReset.Size = new System.Drawing.Size(141, 32);
            this.btnScheduleReset.TabIndex = 10;
            this.btnScheduleReset.Text = "ResetSchedulerData";
            this.btnScheduleReset.UseVisualStyleBackColor = true;
            this.btnScheduleReset.Click += new System.EventHandler(this.btnScheduleReset_Click);
            // 
            // SchedulerTable
            // 
            this.SchedulerTable.ColumnCount = 2;
            this.SchedulerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.SchedulerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.SchedulerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.SchedulerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.SchedulerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.SchedulerTable.Location = new System.Drawing.Point(873, 103);
            this.SchedulerTable.Name = "SchedulerTable";
            this.SchedulerTable.RowCount = 1;
            this.SchedulerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SchedulerTable.Size = new System.Drawing.Size(351, 49);
            this.SchedulerTable.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(870, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "AVG WaitingTime=>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(870, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "AVG TurnaroundTime=>";
            // 
            // AvgWaiting
            // 
            this.AvgWaiting.AutoSize = true;
            this.AvgWaiting.Location = new System.Drawing.Point(1091, 180);
            this.AvgWaiting.Name = "AvgWaiting";
            this.AvgWaiting.Size = new System.Drawing.Size(34, 17);
            this.AvgWaiting.TabIndex = 14;
            this.AvgWaiting.Text = "0ms";
            // 
            // AvgTurnaround
            // 
            this.AvgTurnaround.AutoSize = true;
            this.AvgTurnaround.Location = new System.Drawing.Point(1091, 239);
            this.AvgTurnaround.Name = "AvgTurnaround";
            this.AvgTurnaround.Size = new System.Drawing.Size(34, 17);
            this.AvgTurnaround.TabIndex = 15;
            this.AvgTurnaround.Text = "0ms";
            // 
            // IdealInformation
            // 
            this.IdealInformation.AutoSize = true;
            this.IdealInformation.Location = new System.Drawing.Point(966, 314);
            this.IdealInformation.Name = "IdealInformation";
            this.IdealInformation.Size = new System.Drawing.Size(214, 17);
            this.IdealInformation.TabIndex = 16;
            this.IdealInformation.Text = "Information about CPU Ideal Time";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 509);
            this.Controls.Add(this.IdealInformation);
            this.Controls.Add(this.AvgTurnaround);
            this.Controls.Add(this.AvgWaiting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SchedulerTable);
            this.Controls.Add(this.btnScheduleReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtQuantum);
            this.Controls.Add(this.btnRR);
            this.Controls.Add(this.btnSJFnp);
            this.Controls.Add(this.btnSJFp);
            this.Controls.Add(this.btnFCFS);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.ProcessesData);
            this.Controls.Add(this.ProcessNum);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CPU_Scheduler";
            ((System.ComponentModel.ISupportInitialize)(this.ProcessesData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProcessNum;
        private System.Windows.Forms.DataGridView ProcessesData;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btnFCFS;
        private System.Windows.Forms.Button btnSJFp;
        private System.Windows.Forms.Button btnSJFnp;
        private System.Windows.Forms.Button btnRR;
        private System.Windows.Forms.TextBox txtQuantum;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnScheduleReset;
        private System.Windows.Forms.TableLayoutPanel SchedulerTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label AvgWaiting;
        private System.Windows.Forms.Label AvgTurnaround;
        private System.Windows.Forms.Label IdealInformation;
    }
}

