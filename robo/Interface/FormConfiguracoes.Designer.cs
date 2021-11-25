
namespace robo.Interface
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfiguracoes));
            this.panelSubMenu = new System.Windows.Forms.Panel();
            this.btBackup = new System.Windows.Forms.Button();
            this.btUsuarios = new System.Windows.Forms.Button();
            this.btLogins = new System.Windows.Forms.Button();
            this.panelLogins = new System.Windows.Forms.Panel();
            this.dgvLogins = new System.Windows.Forms.DataGridView();
            this.lblTituloLogins = new MetroFramework.Controls.MetroLabel();
            this.btnExcluirLogin = new System.Windows.Forms.Button();
            this.btnModificarLogin = new System.Windows.Forms.Button();
            this.btnAdicionarLogin = new System.Windows.Forms.Button();
            this.panelUsuarios = new System.Windows.Forms.Panel();
            this.lblTituloUsuarios = new MetroFramework.Controls.MetroLabel();
            this.btExcUsuario = new System.Windows.Forms.Button();
            this.btModUsuario = new System.Windows.Forms.Button();
            this.btAddUsuario = new System.Windows.Forms.Button();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.panelBackup = new System.Windows.Forms.Panel();
            this.panelMenuBar = new System.Windows.Forms.Panel();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelSubMenu.SuspendLayout();
            this.panelLogins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogins)).BeginInit();
            this.panelUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.panelMenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSubMenu
            // 
            this.panelSubMenu.BackColor = System.Drawing.Color.White;
            this.panelSubMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSubMenu.Controls.Add(this.btBackup);
            this.panelSubMenu.Controls.Add(this.btUsuarios);
            this.panelSubMenu.Controls.Add(this.btLogins);
            this.panelSubMenu.Location = new System.Drawing.Point(-3, -6);
            this.panelSubMenu.Name = "panelSubMenu";
            this.panelSubMenu.Size = new System.Drawing.Size(182, 734);
            this.panelSubMenu.TabIndex = 39;
            // 
            // btBackup
            // 
            this.btBackup.FlatAppearance.BorderSize = 0;
            this.btBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBackup.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBackup.ForeColor = System.Drawing.Color.Black;
            this.btBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBackup.Location = new System.Drawing.Point(1, 170);
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
            this.btUsuarios.Location = new System.Drawing.Point(1, 139);
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
            this.btLogins.Location = new System.Drawing.Point(1, 106);
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
            this.panelLogins.Controls.Add(this.lblTituloLogins);
            this.panelLogins.Controls.Add(this.btnExcluirLogin);
            this.panelLogins.Controls.Add(this.btnModificarLogin);
            this.panelLogins.Controls.Add(this.btnAdicionarLogin);
            this.panelLogins.ForeColor = System.Drawing.SystemColors.Control;
            this.panelLogins.Location = new System.Drawing.Point(174, 29);
            this.panelLogins.Name = "panelLogins";
            this.panelLogins.Size = new System.Drawing.Size(650, 691);
            this.panelLogins.TabIndex = 40;
            // 
            // dgvLogins
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLogins.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLogins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLogins.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLogins.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLogins.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLogins.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLogins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLogins.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLogins.GridColor = System.Drawing.Color.Silver;
            this.dgvLogins.Location = new System.Drawing.Point(91, 191);
            this.dgvLogins.Margin = new System.Windows.Forms.Padding(2);
            this.dgvLogins.Name = "dgvLogins";
            this.dgvLogins.ReadOnly = true;
            this.dgvLogins.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLogins.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLogins.RowHeadersVisible = false;
            this.dgvLogins.RowHeadersWidth = 51;
            this.dgvLogins.ShowCellToolTips = false;
            this.dgvLogins.Size = new System.Drawing.Size(459, 475);
            this.dgvLogins.TabIndex = 6;
            // 
            // lblTituloLogins
            // 
            this.lblTituloLogins.AutoSize = true;
            this.lblTituloLogins.BackColor = System.Drawing.SystemColors.Window;
            this.lblTituloLogins.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTituloLogins.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTituloLogins.Location = new System.Drawing.Point(246, 89);
            this.lblTituloLogins.Name = "lblTituloLogins";
            this.lblTituloLogins.Size = new System.Drawing.Size(137, 25);
            this.lblTituloLogins.TabIndex = 5;
            this.lblTituloLogins.Text = "Tabela de Logins";
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
            this.btnExcluirLogin.Location = new System.Drawing.Point(406, 150);
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
            this.btnModificarLogin.Location = new System.Drawing.Point(273, 150);
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
            this.btnAdicionarLogin.Location = new System.Drawing.Point(116, 150);
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
            this.panelUsuarios.Controls.Add(this.lblTituloUsuarios);
            this.panelUsuarios.Controls.Add(this.btExcUsuario);
            this.panelUsuarios.Controls.Add(this.btModUsuario);
            this.panelUsuarios.Controls.Add(this.btAddUsuario);
            this.panelUsuarios.Controls.Add(this.dgvUsuarios);
            this.panelUsuarios.Location = new System.Drawing.Point(171, -9);
            this.panelUsuarios.Name = "panelUsuarios";
            this.panelUsuarios.Size = new System.Drawing.Size(630, 729);
            this.panelUsuarios.TabIndex = 41;
            // 
            // lblTituloUsuarios
            // 
            this.lblTituloUsuarios.AutoSize = true;
            this.lblTituloUsuarios.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTituloUsuarios.Location = new System.Drawing.Point(233, 102);
            this.lblTituloUsuarios.Name = "lblTituloUsuarios";
            this.lblTituloUsuarios.Size = new System.Drawing.Size(153, 25);
            this.lblTituloUsuarios.TabIndex = 4;
            this.lblTituloUsuarios.Text = "Tabela de Usuários";
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
            this.btExcUsuario.Location = new System.Drawing.Point(399, 164);
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
            this.btModUsuario.Location = new System.Drawing.Point(276, 164);
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
            this.btAddUsuario.Location = new System.Drawing.Point(128, 164);
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvUsuarios.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUsuarios.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUsuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvUsuarios.GridColor = System.Drawing.SystemColors.Control;
            this.dgvUsuarios.Location = new System.Drawing.Point(94, 214);
            this.dgvUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowHeadersWidth = 51;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(459, 504);
            this.dgvUsuarios.TabIndex = 1;
            // 
            // panelBackup
            // 
            this.panelBackup.Location = new System.Drawing.Point(141, 29);
            this.panelBackup.Margin = new System.Windows.Forms.Padding(2);
            this.panelBackup.Name = "panelBackup";
            this.panelBackup.Size = new System.Drawing.Size(696, 691);
            this.panelBackup.TabIndex = 5;
            // 
            // panelMenuBar
            // 
            this.panelMenuBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenuBar.BackColor = System.Drawing.Color.White;
            this.panelMenuBar.Controls.Add(this.btnMinimize);
            this.panelMenuBar.Controls.Add(this.btnClose);
            this.panelMenuBar.ForeColor = System.Drawing.Color.White;
            this.panelMenuBar.Location = new System.Drawing.Point(0, 0);
            this.panelMenuBar.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenuBar.Name = "panelMenuBar";
            this.panelMenuBar.Size = new System.Drawing.Size(801, 34);
            this.panelMenuBar.TabIndex = 5;
            this.panelMenuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMenuBar_MouseDown);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnMinimize.Location = new System.Drawing.Point(715, -2);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(45, 34);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnClose.Location = new System.Drawing.Point(756, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormConfiguracoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 720);
            this.Controls.Add(this.panelMenuBar);
            this.Controls.Add(this.panelSubMenu);
            this.Controls.Add(this.panelLogins);
            this.Controls.Add(this.panelUsuarios);
            this.Controls.Add(this.panelBackup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormConfiguracoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracoes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConfiguracoes_FormClosing);
            this.panelSubMenu.ResumeLayout(false);
            this.panelLogins.ResumeLayout(false);
            this.panelLogins.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogins)).EndInit();
            this.panelUsuarios.ResumeLayout(false);
            this.panelUsuarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.panelMenuBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSubMenu;
        private System.Windows.Forms.Panel panelLogins;
        private System.Windows.Forms.DataGridView dgvLogins;
        private MetroFramework.Controls.MetroLabel lblTituloLogins;
        private System.Windows.Forms.Button btnExcluirLogin;
        private System.Windows.Forms.Button btnModificarLogin;
        private System.Windows.Forms.Button btnAdicionarLogin;
        private System.Windows.Forms.Button btUsuarios;
        private System.Windows.Forms.Panel panelUsuarios;
        private MetroFramework.Controls.MetroLabel lblTituloUsuarios;
        private System.Windows.Forms.Button btExcUsuario;
        private System.Windows.Forms.Button btModUsuario;
        private System.Windows.Forms.Button btAddUsuario;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Button btBackup;
        private System.Windows.Forms.Panel panelBackup;
        private System.Windows.Forms.Button btLogins;
        private System.Windows.Forms.Panel panelMenuBar;
        private System.Windows.Forms.Button btnMinimize;
        public System.Windows.Forms.Button btnClose;
    }
}