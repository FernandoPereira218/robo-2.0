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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsuarioForm));
            this.panelFormLogin = new System.Windows.Forms.Panel();
            this.labelId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.cbRegional = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbIES = new System.Windows.Forms.ComboBox();
            this.cbPermissoes = new System.Windows.Forms.ComboBox();
            this.lblFaculdade = new System.Windows.Forms.Label();
            this.txtSenhaUsuario = new System.Windows.Forms.TextBox();
            this.lblSenhaLogin = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPermissoes = new System.Windows.Forms.Label();
            this.btnCancelLogin = new System.Windows.Forms.Button();
            this.btnOKLogin = new System.Windows.Forms.Button();
            this.panelFormLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFormLogin
            // 
            this.panelFormLogin.BackColor = System.Drawing.Color.White;
            this.panelFormLogin.Controls.Add(this.labelId);
            this.panelFormLogin.Controls.Add(this.txtId);
            this.panelFormLogin.Controls.Add(this.cbRegional);
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
            this.panelFormLogin.Size = new System.Drawing.Size(253, 196);
            this.panelFormLogin.TabIndex = 0;
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.labelId.Location = new System.Drawing.Point(38, 16);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(21, 13);
            this.labelId.TabIndex = 15;
            this.labelId.Text = "ID:";
            this.labelId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.SystemColors.Control;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(64, 11);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(177, 22);
            this.txtId.TabIndex = 14;
            // 
            // cbRegional
            // 
            this.cbRegional.FormattingEnabled = true;
            this.cbRegional.Location = new System.Drawing.Point(64, 145);
            this.cbRegional.Name = "cbRegional";
            this.cbRegional.Size = new System.Drawing.Size(177, 21);
            this.cbRegional.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label1.Location = new System.Drawing.Point(8, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Regional";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbIES
            // 
            this.cbIES.FormattingEnabled = true;
            this.cbIES.Location = new System.Drawing.Point(64, 120);
            this.cbIES.Name = "cbIES";
            this.cbIES.Size = new System.Drawing.Size(177, 21);
            this.cbIES.TabIndex = 11;
            // 
            // cbPermissoes
            // 
            this.cbPermissoes.FormattingEnabled = true;
            this.cbPermissoes.Items.AddRange(new object[] {
            "operacoesFinanceiras",
            "CAE"});
            this.cbPermissoes.Location = new System.Drawing.Point(64, 94);
            this.cbPermissoes.Name = "cbPermissoes";
            this.cbPermissoes.Size = new System.Drawing.Size(177, 21);
            this.cbPermissoes.TabIndex = 10;
            // 
            // lblFaculdade
            // 
            this.lblFaculdade.AutoSize = true;
            this.lblFaculdade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblFaculdade.Location = new System.Drawing.Point(42, 125);
            this.lblFaculdade.Name = "lblFaculdade";
            this.lblFaculdade.Size = new System.Drawing.Size(25, 13);
            this.lblFaculdade.TabIndex = 7;
            this.lblFaculdade.Text = "IES:";
            this.lblFaculdade.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSenhaUsuario
            // 
            this.txtSenhaUsuario.BackColor = System.Drawing.SystemColors.Control;
            this.txtSenhaUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenhaUsuario.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaUsuario.Location = new System.Drawing.Point(64, 67);
            this.txtSenhaUsuario.Name = "txtSenhaUsuario";
            this.txtSenhaUsuario.Size = new System.Drawing.Size(177, 22);
            this.txtSenhaUsuario.TabIndex = 4;
            // 
            // lblSenhaLogin
            // 
            this.lblSenhaLogin.AutoSize = true;
            this.lblSenhaLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblSenhaLogin.Location = new System.Drawing.Point(23, 71);
            this.lblSenhaLogin.Name = "lblSenhaLogin";
            this.lblSenhaLogin.Size = new System.Drawing.Size(42, 13);
            this.lblSenhaLogin.TabIndex = 3;
            this.lblSenhaLogin.Text = "Senha:";
            this.lblSenhaLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.SystemColors.Control;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(64, 39);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(177, 22);
            this.txtUser.TabIndex = 2;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblUser.Location = new System.Drawing.Point(15, 43);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(50, 13);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Usuário:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPermissoes
            // 
            this.lblPermissoes.AutoSize = true;
            this.lblPermissoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblPermissoes.Location = new System.Drawing.Point(-1, 97);
            this.lblPermissoes.Name = "lblPermissoes";
            this.lblPermissoes.Size = new System.Drawing.Size(66, 13);
            this.lblPermissoes.TabIndex = 9;
            this.lblPermissoes.Text = "Permissões:";
            this.lblPermissoes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancelLogin
            // 
            this.btnCancelLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelLogin.ForeColor = System.Drawing.Color.Silver;
            this.btnCancelLogin.Location = new System.Drawing.Point(145, 215);
            this.btnCancelLogin.Name = "btnCancelLogin";
            this.btnCancelLogin.Size = new System.Drawing.Size(126, 28);
            this.btnCancelLogin.TabIndex = 1;
            this.btnCancelLogin.Text = "Cancelar";
            this.btnCancelLogin.UseVisualStyleBackColor = true;
            this.btnCancelLogin.Click += new System.EventHandler(this.btnCancelLogin_Click);
            // 
            // btnOKLogin
            // 
            this.btnOKLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOKLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOKLogin.ForeColor = System.Drawing.Color.Silver;
            this.btnOKLogin.Location = new System.Drawing.Point(13, 215);
            this.btnOKLogin.Name = "btnOKLogin";
            this.btnOKLogin.Size = new System.Drawing.Size(126, 28);
            this.btnOKLogin.TabIndex = 0;
            this.btnOKLogin.Text = "Aceitar";
            this.btnOKLogin.UseVisualStyleBackColor = true;
            // 
            // UsuarioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(278, 271);
            this.Controls.Add(this.btnCancelLogin);
            this.Controls.Add(this.btnOKLogin);
            this.Controls.Add(this.panelFormLogin);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UsuarioForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.panelFormLogin.ResumeLayout(false);
            this.panelFormLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFormLogin;
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
        private System.Windows.Forms.ComboBox cbRegional;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox txtId;
    }
}