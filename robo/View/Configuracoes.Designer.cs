
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSubMenu = new System.Windows.Forms.Panel();
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
            this.panelMenuBar = new System.Windows.Forms.Panel();
            this.lbTitulo = new System.Windows.Forms.Label();
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
            this.panelSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelSubMenu.Controls.Add(this.btBackup);
            this.panelSubMenu.Controls.Add(this.btUsuarios);
            this.panelSubMenu.Controls.Add(this.btLogins);
            this.panelSubMenu.Location = new System.Drawing.Point(2, 63);
            this.panelSubMenu.Margin = new System.Windows.Forms.Padding(4);
            this.panelSubMenu.Name = "panelSubMenu";
            this.panelSubMenu.Size = new System.Drawing.Size(206, 551);
            this.panelSubMenu.TabIndex = 39;
            // 
            // btBackup
            // 
            this.btBackup.FlatAppearance.BorderSize = 0;
            this.btBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBackup.ForeColor = System.Drawing.Color.Silver;
            this.btBackup.Image = global::robo.Properties.Resources.silhueta_negra_de_casa_sem_porta;
            this.btBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBackup.Location = new System.Drawing.Point(4, 154);
            this.btBackup.Margin = new System.Windows.Forms.Padding(4);
            this.btBackup.Name = "btBackup";
            this.btBackup.Size = new System.Drawing.Size(198, 77);
            this.btBackup.TabIndex = 5;
            this.btBackup.Text = "Backup";
            this.btBackup.UseVisualStyleBackColor = true;
            // 
            // btUsuarios
            // 
            this.btUsuarios.FlatAppearance.BorderSize = 0;
            this.btUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUsuarios.ForeColor = System.Drawing.Color.Silver;
            this.btUsuarios.Image = global::robo.Properties.Resources.silhueta_negra_de_casa_sem_porta;
            this.btUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btUsuarios.Location = new System.Drawing.Point(4, 79);
            this.btUsuarios.Margin = new System.Windows.Forms.Padding(4);
            this.btUsuarios.Name = "btUsuarios";
            this.btUsuarios.Size = new System.Drawing.Size(198, 77);
            this.btUsuarios.TabIndex = 4;
            this.btUsuarios.Text = "Usuários";
            this.btUsuarios.UseVisualStyleBackColor = true;
            this.btUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btLogins
            // 
            this.btLogins.FlatAppearance.BorderSize = 0;
            this.btLogins.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLogins.ForeColor = System.Drawing.Color.Silver;
            this.btLogins.Image = global::robo.Properties.Resources.silhueta_negra_de_casa_sem_porta;
            this.btLogins.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btLogins.Location = new System.Drawing.Point(4, 4);
            this.btLogins.Margin = new System.Windows.Forms.Padding(4);
            this.btLogins.Name = "btLogins";
            this.btLogins.Size = new System.Drawing.Size(198, 77);
            this.btLogins.TabIndex = 3;
            this.btLogins.Text = "Logins";
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
            this.panelLogins.Location = new System.Drawing.Point(212, 63);
            this.panelLogins.Margin = new System.Windows.Forms.Padding(4);
            this.panelLogins.Name = "panelLogins";
            this.panelLogins.Size = new System.Drawing.Size(863, 551);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLogins.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLogins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ScrollBar;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLogins.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLogins.GridColor = System.Drawing.Color.Black;
            this.dgvLogins.Location = new System.Drawing.Point(-7, 70);
            this.dgvLogins.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvLogins.Name = "dgvLogins";
            this.dgvLogins.ReadOnly = true;
            this.dgvLogins.RowHeadersWidth = 51;
            this.dgvLogins.Size = new System.Drawing.Size(870, 523);
            this.dgvLogins.TabIndex = 6;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.BackColor = System.Drawing.SystemColors.Window;
            this.metroLabel8.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.metroLabel8.Location = new System.Drawing.Point(6, 9);
            this.metroLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(171, 25);
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
            this.btnExcluirLogin.Location = new System.Drawing.Point(688, 31);
            this.btnExcluirLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnExcluirLogin.Name = "btnExcluirLogin";
            this.btnExcluirLogin.Size = new System.Drawing.Size(161, 30);
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
            this.btnModificarLogin.Location = new System.Drawing.Point(542, 31);
            this.btnModificarLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnModificarLogin.Name = "btnModificarLogin";
            this.btnModificarLogin.Size = new System.Drawing.Size(147, 30);
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
            this.btnAdicionarLogin.Location = new System.Drawing.Point(368, 31);
            this.btnAdicionarLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdicionarLogin.Name = "btnAdicionarLogin";
            this.btnAdicionarLogin.Size = new System.Drawing.Size(173, 30);
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
            this.panelUsuarios.Location = new System.Drawing.Point(212, 63);
            this.panelUsuarios.Margin = new System.Windows.Forms.Padding(4);
            this.panelUsuarios.Name = "panelUsuarios";
            this.panelUsuarios.Size = new System.Drawing.Size(859, 551);
            this.panelUsuarios.TabIndex = 41;
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel9.Location = new System.Drawing.Point(8, 25);
            this.metroLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(185, 25);
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
            this.btExcUsuario.Location = new System.Drawing.Point(732, 31);
            this.btExcUsuario.Margin = new System.Windows.Forms.Padding(0);
            this.btExcUsuario.Name = "btExcUsuario";
            this.btExcUsuario.Size = new System.Drawing.Size(117, 30);
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
            this.btModUsuario.Location = new System.Drawing.Point(635, 31);
            this.btModUsuario.Margin = new System.Windows.Forms.Padding(0);
            this.btModUsuario.Name = "btModUsuario";
            this.btModUsuario.Size = new System.Drawing.Size(97, 30);
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
            this.btAddUsuario.Location = new System.Drawing.Point(515, 31);
            this.btAddUsuario.Margin = new System.Windows.Forms.Padding(0);
            this.btAddUsuario.Name = "btAddUsuario";
            this.btAddUsuario.Size = new System.Drawing.Size(120, 30);
            this.btAddUsuario.TabIndex = 1;
            this.btAddUsuario.Text = "Adicionar Usuários";
            this.btAddUsuario.UseVisualStyleBackColor = false;
            this.btAddUsuario.Click += new System.EventHandler(this.btAddUsuario_Click);
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUsuarios.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUsuarios.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvUsuarios.GridColor = System.Drawing.Color.Black;
            this.dgvUsuarios.Location = new System.Drawing.Point(3, 70);
            this.dgvUsuarios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowHeadersWidth = 51;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvUsuarios.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(856, 472);
            this.dgvUsuarios.TabIndex = 1;
            // 
            // panelMenuBar
            // 
            this.panelMenuBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelMenuBar.Controls.Add(this.lbTitulo);
            this.panelMenuBar.Controls.Add(this.btnMinimize);
            this.panelMenuBar.Controls.Add(this.btnClose);
            this.panelMenuBar.Location = new System.Drawing.Point(0, 0);
            this.panelMenuBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMenuBar.Name = "panelMenuBar";
            this.panelMenuBar.Size = new System.Drawing.Size(1075, 53);
            this.panelMenuBar.TabIndex = 42;
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbTitulo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.ForeColor = System.Drawing.SystemColors.Control;
            this.lbTitulo.Location = new System.Drawing.Point(9, 17);
            this.lbTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(109, 19);
            this.lbTitulo.TabIndex = 11;
            this.lbTitulo.Text = "Configurações";
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(1017, 0);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(29, 31);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1045, -1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 32);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // FormConfiguracoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1074, 616);
            this.Controls.Add(this.panelUsuarios);
            this.Controls.Add(this.panelLogins);
            this.Controls.Add(this.panelMenuBar);
            this.Controls.Add(this.panelSubMenu);
            this.Name = "FormConfiguracoes";
            this.Text = "Configuracoes";
            this.panelSubMenu.ResumeLayout(false);
            this.panelLogins.ResumeLayout(false);
            this.panelLogins.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogins)).EndInit();
            this.panelUsuarios.ResumeLayout(false);
            this.panelUsuarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.panelMenuBar.ResumeLayout(false);
            this.panelMenuBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSubMenu;
        private System.Windows.Forms.Button btLogins;
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
        private System.Windows.Forms.Panel panelMenuBar;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnClose;
    }
}