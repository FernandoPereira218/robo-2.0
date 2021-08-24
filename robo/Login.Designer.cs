
namespace robo
{
    partial class Login
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
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.lblSenha = new System.Windows.Forms.Label();
            this.btConfirma = new System.Windows.Forms.Button();
            this.cbManterLogado = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelMenuBar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(196, 149);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(191, 21);
            this.txtUsuario.TabIndex = 0;
            // 
            // txtSenha
            // 
            this.txtSenha.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(196, 196);
            this.txtSenha.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(191, 21);
            this.txtSenha.TabIndex = 1;
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lbUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbUsuario.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsuario.ForeColor = System.Drawing.Color.White;
            this.lbUsuario.Location = new System.Drawing.Point(193, 131);
            this.lbUsuario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(47, 16);
            this.lbUsuario.TabIndex = 2;
            this.lbUsuario.Text = "Usuário";
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.BackColor = System.Drawing.Color.Transparent;
            this.lblSenha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSenha.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenha.ForeColor = System.Drawing.Color.White;
            this.lblSenha.Location = new System.Drawing.Point(193, 178);
            this.lblSenha.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(43, 16);
            this.lblSenha.TabIndex = 3;
            this.lblSenha.Text = "Senha";
            // 
            // btConfirma
            // 
            this.btConfirma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btConfirma.FlatAppearance.BorderSize = 0;
            this.btConfirma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btConfirma.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConfirma.ForeColor = System.Drawing.Color.White;
            this.btConfirma.Location = new System.Drawing.Point(196, 269);
            this.btConfirma.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btConfirma.Name = "btConfirma";
            this.btConfirma.Size = new System.Drawing.Size(191, 32);
            this.btConfirma.TabIndex = 4;
            this.btConfirma.Text = "Confirma";
            this.btConfirma.UseVisualStyleBackColor = false;
            this.btConfirma.Click += new System.EventHandler(this.btConfirma_Click);
            // 
            // cbManterLogado
            // 
            this.cbManterLogado.AutoSize = true;
            this.cbManterLogado.BackColor = System.Drawing.Color.Transparent;
            this.cbManterLogado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbManterLogado.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbManterLogado.ForeColor = System.Drawing.Color.White;
            this.cbManterLogado.Location = new System.Drawing.Point(196, 221);
            this.cbManterLogado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbManterLogado.Name = "cbManterLogado";
            this.cbManterLogado.Size = new System.Drawing.Size(12, 11);
            this.cbManterLogado.TabIndex = 6;
            this.cbManterLogado.UseVisualStyleBackColor = false;
            this.cbManterLogado.CheckedChanged += new System.EventHandler(this.cbManterLogado_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(555, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(27, 26);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelMenuBar
            // 
            this.panelMenuBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelMenuBar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMenuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(37)))), ((int)(((byte)(34)))));
            this.panelMenuBar.Controls.Add(this.label1);
            this.panelMenuBar.Controls.Add(this.btnMinimize);
            this.panelMenuBar.Controls.Add(this.label6);
            this.panelMenuBar.Controls.Add(this.btnClose);
            this.panelMenuBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelMenuBar.Location = new System.Drawing.Point(-5, -1);
            this.panelMenuBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelMenuBar.Name = "panelMenuBar";
            this.panelMenuBar.Size = new System.Drawing.Size(582, 82);
            this.panelMenuBar.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(265, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "SIGN-IN";
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(533, 1);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(22, 24);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(219, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Universidade Ritter dos Reis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(212, 219);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Lembre de mim";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::robo.Properties.Resources.fachada_fapa;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(570, 346);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelMenuBar);
            this.Controls.Add(this.cbManterLogado);
            this.Controls.Add(this.btConfirma);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.lbUsuario);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.txtUsuario);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Shown += new System.EventHandler(this.Login_Shown);
            this.panelMenuBar.ResumeLayout(false);
            this.panelMenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.Button btConfirma;
        private System.Windows.Forms.CheckBox cbManterLogado;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelMenuBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
    }
}