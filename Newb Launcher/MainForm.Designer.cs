namespace Newb_Launcher
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.cbPassword = new System.Windows.Forms.CheckBox();
            this.bStartMaple = new System.Windows.Forms.Button();
            this.bAddAccount = new System.Windows.Forms.Button();
            this.bRemoveUser = new System.Windows.Forms.Button();
            this.lbAccounts = new System.Windows.Forms.ListBox();
            this.cbNXLauncher = new System.Windows.Forms.CheckBox();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.lStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lAccounts = new System.Windows.Forms.Label();
            this.lUsername = new System.Windows.Forms.Label();
            this.lPassword = new System.Windows.Forms.Label();
            this.ssStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(155, 25);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(137, 20);
            this.tbUsername.TabIndex = 0;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(155, 67);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(137, 20);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyUp);
            // 
            // cbPassword
            // 
            this.cbPassword.AutoSize = true;
            this.cbPassword.Location = new System.Drawing.Point(12, 126);
            this.cbPassword.Name = "cbPassword";
            this.cbPassword.Size = new System.Drawing.Size(102, 17);
            this.cbPassword.TabIndex = 2;
            this.cbPassword.Text = "Show Password";
            this.cbPassword.UseVisualStyleBackColor = true;
            this.cbPassword.CheckedChanged += new System.EventHandler(this.cbPassword_CheckedChanged);
            // 
            // bStartMaple
            // 
            this.bStartMaple.Location = new System.Drawing.Point(155, 126);
            this.bStartMaple.Name = "bStartMaple";
            this.bStartMaple.Size = new System.Drawing.Size(137, 40);
            this.bStartMaple.TabIndex = 3;
            this.bStartMaple.Text = "Start";
            this.bStartMaple.UseVisualStyleBackColor = true;
            this.bStartMaple.Click += new System.EventHandler(this.bStartMaple_Click);
            // 
            // bAddAccount
            // 
            this.bAddAccount.Location = new System.Drawing.Point(155, 93);
            this.bAddAccount.Name = "bAddAccount";
            this.bAddAccount.Size = new System.Drawing.Size(66, 27);
            this.bAddAccount.TabIndex = 4;
            this.bAddAccount.Text = "Save";
            this.bAddAccount.UseVisualStyleBackColor = true;
            this.bAddAccount.Click += new System.EventHandler(this.bAddAccount_Click);
            // 
            // bRemoveUser
            // 
            this.bRemoveUser.Location = new System.Drawing.Point(227, 93);
            this.bRemoveUser.Name = "bRemoveUser";
            this.bRemoveUser.Size = new System.Drawing.Size(65, 27);
            this.bRemoveUser.TabIndex = 5;
            this.bRemoveUser.Text = "Remove";
            this.bRemoveUser.UseVisualStyleBackColor = true;
            this.bRemoveUser.Click += new System.EventHandler(this.bRemoveUser_Click);
            // 
            // lbAccounts
            // 
            this.lbAccounts.FormattingEnabled = true;
            this.lbAccounts.Location = new System.Drawing.Point(12, 25);
            this.lbAccounts.Name = "lbAccounts";
            this.lbAccounts.Size = new System.Drawing.Size(137, 95);
            this.lbAccounts.TabIndex = 6;
            this.lbAccounts.SelectedIndexChanged += new System.EventHandler(this.lbAccounts_SelectedIndexChanged);
            // 
            // cbNXLauncher
            // 
            this.cbNXLauncher.AutoSize = true;
            this.cbNXLauncher.Location = new System.Drawing.Point(12, 150);
            this.cbNXLauncher.Name = "cbNXLauncher";
            this.cbNXLauncher.Size = new System.Drawing.Size(127, 17);
            this.cbNXLauncher.TabIndex = 7;
            this.cbNXLauncher.Text = "Use Nexon Launcher";
            this.cbNXLauncher.UseVisualStyleBackColor = true;
            // 
            // ssStatus
            // 
            this.ssStatus.AutoSize = false;
            this.ssStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lStatus});
            this.ssStatus.Location = new System.Drawing.Point(2, 177);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(300, 22);
            this.ssStatus.SizingGrip = false;
            this.ssStatus.TabIndex = 8;
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = false;
            this.lStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lStatus.Name = "lStatus";
            this.lStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.lStatus.Size = new System.Drawing.Size(301, 17);
            this.lStatus.Text = "Ready!";
            this.lStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // lAccounts
            // 
            this.lAccounts.Location = new System.Drawing.Point(12, 9);
            this.lAccounts.Name = "lAccounts";
            this.lAccounts.Size = new System.Drawing.Size(137, 13);
            this.lAccounts.TabIndex = 9;
            this.lAccounts.Text = "Accounts";
            this.lAccounts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lUsername
            // 
            this.lUsername.Location = new System.Drawing.Point(155, 9);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(137, 13);
            this.lUsername.TabIndex = 10;
            this.lUsername.Text = "Username";
            this.lUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lPassword
            // 
            this.lPassword.Location = new System.Drawing.Point(155, 48);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(137, 16);
            this.lPassword.TabIndex = 11;
            this.lPassword.Text = "Password";
            this.lPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 199);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.lUsername);
            this.Controls.Add(this.lAccounts);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.cbNXLauncher);
            this.Controls.Add(this.lbAccounts);
            this.Controls.Add(this.bRemoveUser);
            this.Controls.Add(this.bAddAccount);
            this.Controls.Add(this.bStartMaple);
            this.Controls.Add(this.cbPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Newb Launcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.CheckBox cbPassword;
        private System.Windows.Forms.Button bStartMaple;
        private System.Windows.Forms.Button bAddAccount;
        private System.Windows.Forms.Button bRemoveUser;
        private System.Windows.Forms.ListBox lbAccounts;
        private System.Windows.Forms.CheckBox cbNXLauncher;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel lStatus;
        private System.Windows.Forms.Label lAccounts;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.Label lPassword;
    }
}

