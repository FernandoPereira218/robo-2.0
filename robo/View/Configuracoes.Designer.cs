
namespace robo.View
{
    partial class FormConfiguracoes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSubMenu = new System.Windows.Forms.Panel();
            this.lblconfig = new System.Windows.Forms.Label();
            this.btBackup = new System.Windows.Forms.Button();
            this.btUsuarios = new System.Windows.Forms.Button();
            this.btLogins = new System.Windows.Forms.Button();
            this.panelLogins = new System.Windows.Forms.Panel();
            this.dgvLogins = new System.Windows.Forms.DataGridView();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.btnExcluirLogin = new System.Windows.Forms.Button();
            this.btnModificarLogin = new System.Windows.Forms.Button();
            this.btnAdicionarLogin = new System.Windows.Forms.Button();
            this.panelUsuarios = new System.Windows.Forms.Panel();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.btExcUsuario = new System.Windows.Forms.Button();
            this.btModUsuario = new System.Windows.Forms.Button();
            this.btAddUsuario = new System.Windows.Forms.Button();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.panelBackup = new System.Windows.Forms.Panel();
            this.panelSubMenu.SuspendLayout();
            this.panelLogins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogins)).BeginInit();
            this.panelUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSubMenu
            // 
            this.panelSubMenu.BackColor = System.Drawing.Color.White;
            this.panelSubMenu.Controls.Add(this.lblconfig);
            this.panelSubMenu.Controls.Add(this.btBackup);
            this.panelSubMenu.Controls.Add(this.btUsuarios);
            this.panelSubMenu.Controls.Add(this.btLogins);
            this.panelSubMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSubMenu.Name = "panelSubMenu";
            this.panelSubMenu.Size = new System.Drawing.Size(179, 545);
            this.panelSubMenu.TabIndex = 39;
            // 
            // lblconfig
            // 
            this.lblconfig.AutoSize = true;
            this.lblconfig.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblconfig.Location = new System.Drawing.Point(12, 41);
            this.lblconfig.Name = "lblconfig";
            this.lblconfig.Size = new System.Drawing.Size(118, 20);
            this.lblconfig.TabIndex = 6;
            this.lblconfig.Text = "Configurações";
            // 
            // btBackup
            // 
            this.btBackup.FlatAppearance.BorderSize = 0;
            this.btBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBackup.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBackup.ForeColor = System.Drawing.Color.Black;
            this.btBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBackup.Location = new System.Drawing.Point(0, 153);
            this.btBackup.Name = "btBackup";
            this.btBackup.Size = new System.Drawing.Size(178, 29);
            this.btBackup.TabIndex = 5;
            this.btBackup.Text = "Backup";
            this.btBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBackup.UseVisualStyleBackColor = true;
            this.btBackup.Click += new System.EventHandler(this.btBackup_Click);
            // 
            // btUsuarios
            // 
            this.btUsuarios.FlatAppearance.BorderSize = 0;
            this.btUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUsuarios.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUsuarios.ForeColor = System.Drawing.Color.Black;
            this.btUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btUsuarios.Location = new System.Drawing.Point(0, 122);
            this.btUsuarios.Name = "btUsuarios";
            this.btUsuarios.Size = new System.Drawing.Size(178, 25);
            this.btUsuarios.TabIndex = 4;
            this.btUsuarios.Text = "Usuários";
            this.btUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btUsuarios.UseVisualStyleBackColor = true;
            this.btUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btLogins
            // 
            this.btLogins.FlatAppearance.BorderSize = 0;
            this.btLogins.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLogins.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLogins.ForeColor = System.Drawing.Color.Black;
            this.btLogins.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btLogins.Location = new System.Drawing.Point(0, 89);
            this.btLogins.Name = "btLogins";
            this.btLogins.Size = new System.Drawing.Size(178, 27);
            this.btLogins.TabIndex = 3;
            this.btLogins.Text = "Logins";
            this.btLogins.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btLogins.UseVisualStyleBackColor = true;
            this.btLogins.Click += new System.EventHandler(this.btLogins_Click);
            // 
            // panelLogins
            // 
            this.panelLogins.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelLogins.BackColor = System.Drawing.Color.White;
            this.panelLogins.Controls.Add(this.dgvLogins);
            this.panelLogins.Controls.Add(this.metroLabel8);
            this.panelLogins.Controls.Add(this.btnExcluirLogin);
            this.panelLogins.Controls.Add(this.btnModificarLogin);
            this.panelLogins.Controls.Add(this.btnAdicionarLogin);
            this.panelLogins.ForeColor = System.Drawing.SystemColors.Control;
            this.panelLogins.Location = new System.Drawing.Point(136, 33);
            this.panelLogins.Name = "panelLogins";
            this.panelLogins.Size = new System.Drawing.Size(712, 512);
            this.panelLogins.TabIndex = 40;
            // 
            // dgvLogins
            // 
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLogins.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvLogins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLogins.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLogins.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLogins.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLogins.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvLogins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLogins.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvLogins.GridColor = System.Drawing.Color.Silver;
            this.dgvLogins.Location = new System.Drawing.Point(36, 89);
            this.dgvLogins.Margin = new System.Windows.Forms.Padding(2);
            this.dgvLogins.Name = "dgvLogins";
            this.dgvLogins.ReadOnly = true;
            this.dgvLogins.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLogins.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvLogins.RowHeadersVisible = false;
            this.dgvLogins.RowHeadersWidth = 51;
            this.dgvLogins.ShowCellToolTips = false;
            this.dgvLogins.Size = new System.Drawing.Size(635, 410);
            this.dgvLogins.TabIndex = 6;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.BackColor = System.Drawing.SystemColors.Window;
            this.metroLabel8.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.metroLabel8.Location = new System.Drawing.Point(36, 33);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(162, 25);
            this.metroLabel8.TabIndex = 5;
            this.metroLabel8.Text = "Data Grid de Logins";
            // 
            // btnExcluirLogin
            // 
            this.btnExcluirLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcluirLogin.BackColor = System.Drawing.Color.White;
            this.btnExcluirLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnExcluirLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirLogin.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluirLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnExcluirLogin.Location = new System.Drawing.Point(548, 33);
            this.btnExcluirLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnExcluirLogin.Name = "btnExcluirLogin";
            this.btnExcluirLogin.Size = new System.Drawing.Size(121, 24);
            this.btnExcluirLogin.TabIndex = 3;
            this.btnExcluirLogin.Text = "Excluir";
            this.btnExcluirLogin.UseMnemonic = false;
            this.btnExcluirLogin.UseVisualStyleBackColor = false;
            this.btnExcluirLogin.Click += new System.EventHandler(this.btnExcluirLogin_Click);
            // 
            // btnModificarLogin
            // 
            this.btnModificarLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModificarLogin.BackColor = System.Drawing.Color.White;
            this.btnModificarLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnModificarLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarLogin.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnModificarLogin.Location = new System.Drawing.Point(438, 33);
            this.btnModificarLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnModificarLogin.Name = "btnModificarLogin";
            this.btnModificarLogin.Size = new System.Drawing.Size(110, 24);
            this.btnModificarLogin.TabIndex = 2;
            this.btnModificarLogin.Text = "Modificar Login";
            this.btnModificarLogin.UseVisualStyleBackColor = false;
            this.btnModificarLogin.Click += new System.EventHandler(this.btnModificarLogin_Click);
            // 
            // btnAdicionarLogin
            // 
            this.btnAdicionarLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdicionarLogin.BackColor = System.Drawing.Color.White;
            this.btnAdicionarLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAdicionarLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarLogin.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionarLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnAdicionarLogin.Location = new System.Drawing.Point(308, 33);
            this.btnAdicionarLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdicionarLogin.Name = "btnAdicionarLogin";
            this.btnAdicionarLogin.Size = new System.Drawing.Size(130, 24);
            this.btnAdicionarLogin.TabIndex = 1;
            this.btnAdicionarLogin.Text = "Adicionar Login";
            this.btnAdicionarLogin.UseVisualStyleBackColor = false;
            this.btnAdicionarLogin.Click += new System.EventHandler(this.btnAdicionarLogin_Click);
            // 
            // panelUsuarios
            // 
            this.panelUsuarios.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelUsuarios.BackColor = System.Drawing.Color.White;
            this.panelUsuarios.Controls.Add(this.metroLabel9);
            this.panelUsuarios.Controls.Add(this.btExcUsuario);
            this.panelUsuarios.Controls.Add(this.btModUsuario);
            this.panelUsuarios.Controls.Add(this.btAddUsuario);
            this.panelUsuarios.Controls.Add(this.dgvUsuarios);
            this.panelUsuarios.Location = new System.Drawing.Point(136, 0);
            this.panelUsuarios.Name = "panelUsuarios";
            this.panelUsuarios.Size = new System.Drawing.Size(712, 545);
            this.panelUsuarios.TabIndex = 41;
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel9.Location = new System.Drawing.Point(87, 67);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(178, 25);
            this.metroLabel9.TabIndex = 4;
            this.metroLabel9.Text = "Data Grid de Usuários";
            // 
            // btExcUsuario
            // 
            this.btExcUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btExcUsuario.BackColor = System.Drawing.Color.White;
            this.btExcUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btExcUsuario.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btExcUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExcUsuario.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExcUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btExcUsuario.Location = new System.Drawing.Point(563, 66);
            this.btExcUsuario.Margin = new System.Windows.Forms.Padding(0);
            this.btExcUsuario.Name = "btExcUsuario";
            this.btExcUsuario.Size = new System.Drawing.Size(108, 24);
            this.btExcUsuario.TabIndex = 3;
            this.btExcUsuario.Text = "Deletar";
            this.btExcUsuario.UseMnemonic = false;
            this.btExcUsuario.UseVisualStyleBackColor = false;
            this.btExcUsuario.Click += new System.EventHandler(this.btExcUsuario_Click);
            // 
            // btModUsuario
            // 
            this.btModUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btModUsuario.BackColor = System.Drawing.Color.White;
            this.btModUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btModUsuario.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btModUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btModUsuario.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btModUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btModUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btModUsuario.Location = new System.Drawing.Point(438, 66);
            this.btModUsuario.Margin = new System.Windows.Forms.Padding(0);
            this.btModUsuario.Name = "btModUsuario";
            this.btModUsuario.Size = new System.Drawing.Size(110, 24);
            this.btModUsuario.TabIndex = 2;
            this.btModUsuario.Text = "Modificar";
            this.btModUsuario.UseVisualStyleBackColor = false;
            this.btModUsuario.Click += new System.EventHandler(this.btModUsuario_Click);
            // 
            // btAddUsuario
            // 
            this.btAddUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btAddUsuario.BackColor = System.Drawing.Color.White;
            this.btAddUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btAddUsuario.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btAddUsuario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btAddUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAddUsuario.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAddUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btAddUsuario.Location = new System.Drawing.Point(291, 66);
            this.btAddUsuario.Margin = new System.Windows.Forms.Padding(0);
            this.btAddUsuario.Name = "btAddUsuario";
            this.btAddUsuario.Size = new System.Drawing.Size(130, 24);
            this.btAddUsuario.TabIndex = 1;
            this.btAddUsuario.Text = "Adicionar Usuários";
            this.btAddUsuario.UseVisualStyleBackColor = false;
            this.btAddUsuario.Click += new System.EventHandler(this.btAddUsuario_Click);
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvUsuarios.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUsuarios.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUsuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvUsuarios.GridColor = System.Drawing.SystemColors.Control;
            this.dgvUsuarios.Location = new System.Drawing.Point(87, 122);
            this.dgvUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowHeadersWidth = 51;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(584, 393);
            this.dgvUsuarios.TabIndex = 1;
            // 
            // panelBackup
            // 
            this.panelBackup.Location = new System.Drawing.Point(141, 42);
            this.panelBackup.Margin = new System.Windows.Forms.Padding(2);
            this.panelBackup.Name = "panelBackup";
            this.panelBackup.Size = new System.Drawing.Size(696, 492);
            this.panelBackup.TabIndex = 5;
            // 
            // FormConfiguracoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 545);
            this.Controls.Add(this.panelSubMenu);
            this.Controls.Add(this.panelUsuarios);
            this.Controls.Add(this.panelLogins);
            this.Controls.Add(this.panelBackup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormConfiguracoes";
            this.Text = "Configuracoes";
            this.panelSubMenu.ResumeLayout(false);
            this.panelSubMenu.PerformLayout();
            this.panelLogins.ResumeLayout(false);
            this.panelLogins.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogins)).EndInit();
            this.panelUsuarios.ResumeLayout(false);
            this.panelUsuarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSubMenu;
        private System.Windows.Forms.Panel panelLogins;
        private System.Windows.Forms.DataGridView dgvLogins;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private System.Windows.Forms.Button btnExcluirLogin;
        private System.Windows.Forms.Button btnModificarLogin;
        private System.Windows.Forms.Button btnAdicionarLogin;
        private System.Windows.Forms.Button btUsuarios;
        private System.Windows.Forms.Panel panelUsuarios;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private System.Windows.Forms.Button btExcUsuario;
        private System.Windows.Forms.Button btModUsuario;
        private System.Windows.Forms.Button btAddUsuario;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Button btBackup;
        private System.Windows.Forms.Panel panelBackup;
        private System.Windows.Forms.Button btLogins;
        private System.Windows.Forms.Label lblconfig;
    }
}