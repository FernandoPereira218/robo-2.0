namespace Robo
{
    partial class UsuarioForm
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
            this.panelFormLogin = new System.Windows.Forms.Panel();
            this.cbIES = new System.Windows.Forms.ComboBox();
            this.cbPermissoes = new System.Windows.Forms.ComboBox();
            this.lblFaculdade = new System.Windows.Forms.Label();
            this.txtSenhaUsuario = new System.Windows.Forms.TextBox();
            this.lblSenhaLogin = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPermissoes = new System.Windows.Forms.Label();
            this.panelButtonsLogin = new System.Windows.Forms.Panel();
            this.btnCancelLogin = new System.Windows.Forms.Button();
            this.btnOKLogin = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelFormLogin.SuspendLayout();
            this.panelButtonsLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFormLogin
            // 
            this.panelFormLogin.BackColor = System.Drawing.Color.White;
            this.panelFormLogin.Controls.Add(this.comboBox1);
            this.panelFormLogin.Controls.Add(this.label1);
            this.panelFormLogin.Controls.Add(this.cbIES);
            this.panelFormLogin.Controls.Add(this.cbPermissoes);
            this.panelFormLogin.Controls.Add(this.lblFaculdade);
            this.panelFormLogin.Controls.Add(this.txtSenhaUsuario);
            this.panelFormLogin.Controls.Add(this.lblSenhaLogin);
            this.panelFormLogin.Controls.Add(this.txtUser);
            this.panelFormLogin.Controls.Add(this.lblUser);
            this.panelFormLogin.Controls.Add(this.lblPermissoes);
            this.panelFormLogin.Location = new System.Drawing.Point(13, 13);
            this.panelFormLogin.Name = "panelFormLogin";
            this.panelFormLogin.Size = new System.Drawing.Size(253, 162);
            this.panelFormLogin.TabIndex = 0;
            // 
            // cbIES
            // 
            this.cbIES.FormattingEnabled = true;
            this.cbIES.Location = new System.Drawing.Point(64, 93);
            this.cbIES.Name = "cbIES";
            this.cbIES.Size = new System.Drawing.Size(177, 27);
            this.cbIES.TabIndex = 11;
            // 
            // cbPermissoes
            // 
            this.cbPermissoes.FormattingEnabled = true;
            this.cbPermissoes.Items.AddRange(new object[] {
            "operacoesFinanceiras",
            "CAE"});
            this.cbPermissoes.Location = new System.Drawing.Point(64, 67);
            this.cbPermissoes.Name = "cbPermissoes";
            this.cbPermissoes.Size = new System.Drawing.Size(177, 27);
            this.cbPermissoes.TabIndex = 10;
            // 
            // lblFaculdade
            // 
            this.lblFaculdade.AutoSize = true;
            this.lblFaculdade.Location = new System.Drawing.Point(42, 98);
            this.lblFaculdade.Name = "lblFaculdade";
            this.lblFaculdade.Size = new System.Drawing.Size(30, 19);
            this.lblFaculdade.TabIndex = 7;
            this.lblFaculdade.Text = "IES:";
            this.lblFaculdade.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSenhaUsuario
            // 
            this.txtSenhaUsuario.BackColor = System.Drawing.SystemColors.Control;
            this.txtSenhaUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenhaUsuario.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaUsuario.Location = new System.Drawing.Point(64, 40);
            this.txtSenhaUsuario.Name = "txtSenhaUsuario";
            this.txtSenhaUsuario.Size = new System.Drawing.Size(177, 26);
            this.txtSenhaUsuario.TabIndex = 4;
            // 
            // lblSenhaLogin
            // 
            this.lblSenhaLogin.AutoSize = true;
            this.lblSenhaLogin.Location = new System.Drawing.Point(23, 44);
            this.lblSenhaLogin.Name = "lblSenhaLogin";
            this.lblSenhaLogin.Size = new System.Drawing.Size(49, 19);
            this.lblSenhaLogin.TabIndex = 3;
            this.lblSenhaLogin.Text = "Senha:";
            this.lblSenhaLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.SystemColors.Control;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(64, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(177, 26);
            this.txtUser.TabIndex = 2;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(15, 16);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(59, 19);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Usuário:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPermissoes
            // 
            this.lblPermissoes.AutoSize = true;
            this.lblPermissoes.Location = new System.Drawing.Point(-1, 70);
            this.lblPermissoes.Name = "lblPermissoes";
            this.lblPermissoes.Size = new System.Drawing.Size(79, 19);
            this.lblPermissoes.TabIndex = 9;
            this.lblPermissoes.Text = "Permissões:";
            this.lblPermissoes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelButtonsLogin
            // 
            this.panelButtonsLogin.Controls.Add(this.btnCancelLogin);
            this.panelButtonsLogin.Controls.Add(this.btnOKLogin);
            this.panelButtonsLogin.Location = new System.Drawing.Point(13, 181);
            this.panelButtonsLogin.Name = "panelButtonsLogin";
            this.panelButtonsLogin.Size = new System.Drawing.Size(253, 28);
            this.panelButtonsLogin.TabIndex = 1;
            // 
            // btnCancelLogin
            // 
            this.btnCancelLogin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancelLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelLogin.ForeColor = System.Drawing.Color.White;
            this.btnCancelLogin.Location = new System.Drawing.Point(127, 0);
            this.btnCancelLogin.Name = "btnCancelLogin";
            this.btnCancelLogin.Size = new System.Drawing.Size(126, 28);
            this.btnCancelLogin.TabIndex = 1;
            this.btnCancelLogin.Text = "Cancelar";
            this.btnCancelLogin.UseVisualStyleBackColor = true;
            this.btnCancelLogin.Click += new System.EventHandler(this.btnCancelLogin_Click);
            // 
            // btnOKLogin
            // 
            this.btnOKLogin.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOKLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOKLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOKLogin.ForeColor = System.Drawing.Color.White;
            this.btnOKLogin.Location = new System.Drawing.Point(0, 0);
            this.btnOKLogin.Name = "btnOKLogin";
            this.btnOKLogin.Size = new System.Drawing.Size(126, 28);
            this.btnOKLogin.TabIndex = 0;
            this.btnOKLogin.Text = "Aceitar";
            this.btnOKLogin.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(64, 118);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(177, 27);
            this.comboBox1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Regional";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UsuarioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(37)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(278, 217);
            this.Controls.Add(this.panelButtonsLogin);
            this.Controls.Add(this.panelFormLogin);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UsuarioForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.panelFormLogin.ResumeLayout(false);
            this.panelFormLogin.PerformLayout();
            this.panelButtonsLogin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFormLogin;
        private System.Windows.Forms.Panel panelButtonsLogin;
        private System.Windows.Forms.Button btnCancelLogin;
        private System.Windows.Forms.Button btnOKLogin;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtSenhaUsuario;
        private System.Windows.Forms.Label lblSenhaLogin;
        private System.Windows.Forms.Label lblFaculdade;
        private System.Windows.Forms.ComboBox cbPermissoes;
        private System.Windows.Forms.Label lblPermissoes;
        private System.Windows.Forms.ComboBox cbIES;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}